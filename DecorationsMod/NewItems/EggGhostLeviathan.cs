using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class EggsGhostLeviathan : DecorationItem
    {
        public EggsGhostLeviathan()
        {
            this.ClassID = "EggsGhostLeviathan"; // 0e7cc3b9-cdf2-42d9-9c1f-c11b94277c19
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("EggsGhostLeviathanName"),
                                                        LanguageHelper.GetFriendlyWord("EggsGhostLeviathanDescription"),
                                                        true);

            CrafterLogicFixer.GhostLeviathanEggs = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if BELOWZERO
            this.GameObject = Resources.Load<GameObject>("WorldEntities/flora/lostriver/lost_river_cove_tree_01");
#else
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01");
#endif

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to the hand-equipments
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("ghost_leviathan_eggs_icon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            prefab.name = this.ClassID;

            // Get sub objects
            GameObject model = prefab.FindChild("lost_river_cove_tree_01");
            GameObject eggs = model.FindChild("lost_river_cove_tree_01_eggs");
            GameObject shells = model.FindChild("lost_river_cove_tree_01_eggs_shells");

            // Scale model
            model.transform.localScale *= 0.08f;

            // Rotate model
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x + 75f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z + 15f);

            // Translate model
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.02f, model.transform.localPosition.z - 1.6f);

            // Remove unwanted elements
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(prefab.GetComponent<ConstructionObstacle>());
            foreach (Transform tr in prefab.transform)
            {
                if (string.Compare(tr.name, "lost_river_cove_tree_01", true, CultureInfo.InvariantCulture) != 0)
                    GameObject.DestroyImmediate(tr);
                else
                    foreach (Transform ctr in tr)
                        if (!ctr.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                            GameObject.DestroyImmediate(ctr);
            }

            // Disable existing renderers
            Renderer[] renderers = prefab.GetAllComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
                if (!rend.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                    rend.enabled = false;

            // Delete existing colliders
            Collider[] colliders = prefab.GetAllComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
                GameObject.DestroyImmediate(colliders[i]);

            // Add main collider
            BoxCollider c = prefab.AddComponent<BoxCollider>();
            c.size = new Vector3(1f, 0.8f, 1f);
            c.center = new Vector3(c.center.x, c.center.y + 0.4f, c.center.z + 0.3f);

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

            // Update large world entity
            LargeWorldEntity lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // We can pick this item
            PrefabsHelper.SetDefaultPickupable(prefab);

            // We can place this item
            PrefabsHelper.SetDefaultPlaceTool(prefab, c);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0.03f, -0.07f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.2f;

            return prefab;
        }
    }
}
