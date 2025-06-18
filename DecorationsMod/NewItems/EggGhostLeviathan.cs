using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class EggsGhostLeviathan : DecorationItem
    {
        [SetsRequiredMembers]
        public EggsGhostLeviathan() : base("EggsGhostLeviathan", LanguageHelper.GetFriendlyWord("EggsGhostLeviathanName"), LanguageHelper.GetFriendlyWord("EggsGhostLeviathanDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("ghost_leviathan_eggs_icon"))
        {
            this.ClassID = "EggsGhostLeviathan"; // 0e7cc3b9-cdf2-42d9-9c1f-c11b94277c19
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.GhostLeviathanEggs = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to the hand-equipments
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("ghost_leviathan_eggs_icon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _eggGhostLeviathan = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: eggGhostLeviathan.GetGameObject()");
#endif
            if (_eggGhostLeviathan == null)
#if SUBNAUTICA
                _eggGhostLeviathan = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01.prefab");
#else
                _eggGhostLeviathan = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/lostriver/lost_river_cove_tree_01.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_eggGhostLeviathan);
            prefab.name = this.ClassID;

            // Get sub objects
            GameObject model = prefab.FindChild("lost_river_cove_tree_01");
            //GameObject eggs = model.FindChild("lost_river_cove_tree_01_eggs");
            //GameObject shells = model.FindChild("lost_river_cove_tree_01_eggs_shells");

            // Scale model
            model.transform.localScale *= 0.08f;

            // Rotate model
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x + 75f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z + 15f);

            // Translate model
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.02f, model.transform.localPosition.z - 1.6f);

            // Remove unwanted elements
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(prefab.GetComponent<ConstructionObstacle>());
            /*
            foreach (Transform tr in prefab.transform)
            {
                if (string.Compare(tr.name, "lost_river_cove_tree_01", true, CultureInfo.InvariantCulture) != 0)
                {
                    Component[] components = tr.GetComponents<Component>();
                    if (components != null)
                        foreach (Component comp in components)
                        {
#if DEBUG
                            Logger.Debug($"DEBUG: Found component name=[{comp.name ?? "NULL"}] type=[{comp.GetType().ToString() ?? "NULL"}] in lost_river_cove_tree_01 transform.");
#endif
                        }
                    //GameObject.DestroyImmediate(tr);
                }
                else
                    foreach (Transform ctr in tr)
                        if (!ctr.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                        {
                            Component[] components = tr.GetComponents<Component>();
                            if (components != null)
                                foreach (Component comp in components)
                                {
#if DEBUG
                                    Logger.Debug($"DEBUG: Found component name=[{comp.name ?? "NULL"}] type=[{comp.GetType().ToString() ?? "NULL"}] in lost_river_cove_tree_01 transform.");
#endif
                                }
                            //GameObject.DestroyImmediate(ctr);
                        }
            }
            */

            // Disable existing renderers
            Renderer[] renderers = prefab.GetAllComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
                if (!rend.name.StartsWith("lost_river_cove_tree_01_eggs", true, CultureInfo.InvariantCulture))
                    rend.enabled = false;

            // Delete existing colliders
            Collider[] colliders = prefab.GetAllComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
                GameObject.DestroyImmediate(colliders[i]);
            }

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
