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
using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class EggSeaEmperor : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public EggSeaEmperor() : base("EggSeaEmperor", "EggSeaEmperorName", "EggSeaEmperorDescription", "seaemperoreggicon")
        {
#else
        public EggSeaEmperor()
        {
            this.ClassID = "EggSeaEmperor"; // fa5327d8-1975-4e5a-93b2-9e5554907d7b
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("EggSeaEmperorName"),
                                                        LanguageHelper.GetFriendlyWord("EggSeaEmperorDescription"),
                                                        true);
#endif

            CrafterLogicFixer.SeaEmperorEgg = this.TechType;
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
                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Set item occupies 4 slots
                CraftDataHandler.SetItemSize(this.TechType, new Vector2int(3, 3));

                // Add the new TechType to the hand-equipments
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seaemperoreggicon"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _eggSeaEmperor = null;

        public override GameObject GetGameObject()
        {
            if (_eggSeaEmperor == null)
                _eggSeaEmperor = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Eggs/EmperorEgg.prefab");

            GameObject prefab = GameObject.Instantiate(_eggSeaEmperor);

            prefab.name = this.ClassID;

            // Remove unwanted elements
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(prefab.GetComponent<IncubatorEgg>());
            GameObject.DestroyImmediate(prefab.GetComponent<VFXController>());
            GameObject.DestroyImmediate(prefab.GetComponentInChildren<Animator>());
            GameObject.DestroyImmediate(prefab.GetComponentInChildren<IncubatorEggAnimation>());

            GameObject model = prefab.FindChild("Creatures_eggs_11");

            // Rotate model
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + 180f, model.transform.localEulerAngles.z);

            // Scale model
            model.transform.localScale *= 0.2f;

            // Scale colliders
            foreach (SphereCollider c in prefab.GetAllComponentsInChildren<SphereCollider>())
                c.radius *= 0.15f;

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

            // Update sky applier
            PrefabsHelper.SetDefaultSkyApplier(prefab);

            // Update large world entity
            PrefabsHelper.UpdateExistingLargeWorldEntities(prefab);

            // We can pick this item
            PrefabsHelper.SetDefaultPickupable(prefab);

            // We can place this item
            prefab.AddComponent<CustomPlaceToolController>();
            prefab.AddComponent<EggSeaEmperor_PT>();
            PrefabsHelper.SetDefaultPlaceTool(prefab);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0.03f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1.15f;

            return prefab;
        }
    }
}
