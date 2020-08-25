using DecorationsMod.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class JackSepticEye : DecorationItem
    {
        public JackSepticEye()
        {
            this.ClassID = "JackSepticEyeDoll"; // 07a05a2f-de55-4c60-bfda-cedb3ab72b88
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;
 
            this.GameObject = Resources.Load<GameObject>("Submarine/Build/jacksepticeye");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("JackSepticEyeName"),
                                                        LanguageHelper.GetFriendlyWord("JackSepticEyeDescription"),
                                                        true);

            if (ConfigSwitcher.JackSepticEye_asBuildable)
                this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
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

                if (ConfigSwitcher.JackSepticEye_asBuildable)
                {
                    // Add the new TechType to the buildables
                    SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                    SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                }
                else
                {
                    // Add the new TechType to the hand-equipments
                    SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                    // Set quick slot type.
                    SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
                }

                // Set the custom prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom icon
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.JackSepticEye));
                
                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

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

            if (!ConfigSwitcher.JackSepticEye_asBuildable)
            {
                // Retrieve collider
                GameObject model = prefab.FindChild("jacksepticeye");
                Collider collider = model.GetComponentInChildren<Collider>();

                // We can pick this item
                var pickupable = prefab.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                prefab.AddComponent<CustomPlaceToolController>();
                var placeTool = prefab.AddComponent<GenericPlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;

                // Add fabricating animation
                var fabricating = prefab.FindChild("jacksepticeye").AddComponent<VFXFabricating>();
                fabricating.localMinY = -0.1f;
                fabricating.localMaxY = 0.6f;
                fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
                fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
                fabricating.scaleFactor = 1f;
            }
            else
            {
                Constructable constructible = prefab.GetComponent<Constructable>();
                constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
                constructible.placeMinDistance = 0.5f;
            }

            // Update sky applier
#if BELOWZERO
            BaseModuleLighting bml = prefab.AddComponent<BaseModuleLighting>();
            SkyApplier sa = prefab.GetComponent<SkyApplier>();
            if (sa == null)
                sa = prefab.GetComponentInChildren<SkyApplier>();
            if (sa == null)
                sa = prefab.AddComponent<SkyApplier>();
            sa.renderers = prefab.GetComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;
#else
            PrefabsHelper.SetDefaultSkyApplier(prefab, null, Skies.Auto, false, true);
#endif

            return prefab;
        }
    }
}
