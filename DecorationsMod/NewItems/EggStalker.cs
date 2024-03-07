#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using static CraftData;
#else
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
#endif
using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class EggStalker : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public EggStalker() : base(new PrefabInfo("e2e5bb2d-6427-431c-ac3d-036c22083222",
#if SUBNAUTICA
            "WorldEntities/Eggs/StalkerEgg.prefab",
#else
            "WorldEntities/Eggs/Legacy/StalkerEgg.prefab", 
#endif
            TechType.StalkerEgg
            ))
        {
#else
        public EggStalker()
        {
            this.ClassID = "e2e5bb2d-6427-431c-ac3d-036c22083222";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Eggs/StalkerEgg.prefab";
#else
            this.PrefabFileName = "WorldEntities/Eggs/Legacy/StalkerEgg.prefab";
#endif

            this.TechType = TechType.StalkerEgg;
#endif

            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Set unlock conditions
                if (ConfigSwitcher.EnableEggsAtStart || ConfigSwitcher.EnableEggsWhenCreatureScanned)
                    KnownTechHandler.UnlockOnStart(this.TechType);

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Add the new TechType to the hand-equipments
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);
#endif

                CraftDataHandler.SetEquipmentType(TechType.StalkerEggUndiscovered, EquipmentType.Hand);
                CraftDataHandler.SetQuickSlotType(TechType.StalkerEggUndiscovered, QuickSlotType.Selectable);
                
                this.IsRegistered = true;
            }
        }

        private static GameObject _eggStalker = null;

        public override GameObject GetGameObject()
        {
            if (_eggStalker == null)
                _eggStalker = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            GameObject prefab = GameObject.Instantiate(_eggStalker);

            prefab.name = this.ClassID;

            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            if (techTag == null)
                if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                    techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            if (prefabId == null)
                if ((prefabId = prefab.GetComponentInChildren<PrefabIdentifier>()) == null)
                    prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // We can pick this item
            PrefabsHelper.SetDefaultPickupable(prefab);

            // We can place this item
            PrefabsHelper.SetDefaultPlaceTool(prefab);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0.05f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
