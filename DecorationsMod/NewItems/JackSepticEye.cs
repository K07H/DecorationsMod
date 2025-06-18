using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class JackSepticEye : DecorationItem
    {
        [SetsRequiredMembers]
        public JackSepticEye() : base("JackSepticEyeDoll", LanguageHelper.GetFriendlyWord("JackSepticEyeName"), LanguageHelper.GetFriendlyWord("JackSepticEyeDescription"), SpriteManager.Get(TechType.JackSepticEye))
        {
            this.ClassID = "JackSepticEyeDoll"; // 07a05a2f-de55-4c60-bfda-cedb3ab72b88
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;
 
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            if (ConfigSwitcher.JackSepticEye_asBuildable)
                this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1)
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

                if (ConfigSwitcher.JackSepticEye_asBuildable)
                {
                    // Add the new TechType to the buildables
                    Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                    Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                }
                else
                {
                    // Add the new TechType to the hand-equipments
                    Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                    // Set quick slot type.
                    Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
                }

                // Set the custom prefab
                this.Register();

                // Set the custom icon
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.JackSepticEye));
                
                this.IsRegistered = true;
            }
        }

        private static GameObject _jackSepticEye = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: jackSepticEye.GetGameObject()");
#endif
            if (_jackSepticEye == null)
                _jackSepticEye = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/jacksepticeye.prefab");

            GameObject prefab = GameObject.Instantiate(_jackSepticEye);

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
#if !SUBNAUTICA
            BaseModuleLighting bml = prefab.EnsureComponent<BaseModuleLighting>();
#endif
            PrefabsHelper.UpdateOrAddSkyApplier(prefab, null, null, true);

            return prefab;
        }
    }
}
