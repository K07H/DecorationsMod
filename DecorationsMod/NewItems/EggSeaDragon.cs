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
    public class EggSeaDragon : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public EggSeaDragon() : base("EggSeaDragon", "EggSeaDragonName", "EggSeaDragonDescription", "seadragoneggicon")
        {
            this.SetGameObject(this.GetGameObject());
#else
        public EggSeaDragon()
        {
            this.ClassID = "EggSeaDragon"; // 0be47375-933b-4b60-9bab-11b727e48447
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("EggSeaDragonName"),
                                                        LanguageHelper.GetFriendlyWord("EggSeaDragonDescription"),
                                                        true);
#endif

            CrafterLogicFixer.SeaDragonEgg = this.TechType;
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

        private Shader marmosetUber = null;
        private Texture normal = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                marmosetUber = Shader.Find("MarmosetUBER");
                normal = AssetsHelper.Assets.LoadAsset<Texture>("Creatures_eggs_17_normal");

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Set item occupies 4 slots
                CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

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
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seadragoneggicon"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _eggSeaDragon = null;

        public override GameObject GetGameObject()
        {
            if (_eggSeaDragon == null)
#if SUBNAUTICA
                _eggSeaDragon = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Environment/Precursor/LostRiverBase/Precursor_LostRiverBase_SeaDragonEggShell.prefab");
#else
                _eggSeaDragon = AssetsHelper.Assets.LoadAsset<GameObject>("Precursor_LostRiverBase_SeaDragonEggShell");
#endif

            GameObject prefab = GameObject.Instantiate(_eggSeaDragon);

            prefab.name = this.ClassID;

            // Remove unwanted elements
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(prefab.GetComponent<ImmuneToPropulsioncannon>());

            // Rotate model
            GameObject model = prefab.FindChild("Creatures_eggs_17");
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x - 90f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z);
            // Scale model
            model.transform.localScale *= 0.8f;

#if !SUBNAUTICA
            MeshRenderer[] renderers = model.GetComponents<MeshRenderer>();
            if (renderers != null)
                foreach (MeshRenderer rend in renderers)
                    if (rend.materials != null)
                        foreach (Material mat in rend.materials)
                        {
                            mat.shader = marmosetUber;
                            if (mat.name.StartsWith("Creatures_eggs_17"))
                            {
                                mat.SetTexture("_BumpMap", normal);
                                mat.EnableKeyword("MARMO_NORMALMAP");
                                mat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                            }
                        }
#endif

            // Scale collider
            CapsuleCollider c = prefab.GetComponentInChildren<CapsuleCollider>();
            c.radius *= 0.5f;
            c.height *= 0.5f;

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
            //prefab.AddComponent<EggSeaDragon_PT>();
            PrefabsHelper.SetDefaultPlaceTool(prefab);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0.03f, -0.15f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.35f;

            return prefab;
        }
    }
}
