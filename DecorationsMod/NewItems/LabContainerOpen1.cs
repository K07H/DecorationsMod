using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class LabContainerOpen1 : DecorationItem
    {
        [SetsRequiredMembers]
        public LabContainerOpen1() : base("LabContainerOpen1", LanguageHelper.GetFriendlyWord("LargeLabContainerOpenName"), LanguageHelper.GetFriendlyWord("LabContainerOpenDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("labcontaineropen1")) // Feeds abstract class
        {
            this.ClassID = "LabContainerOpen1"; // 1b0b7f6d-9793-469c-9872-dfe690834fee

            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

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

                // Add the new TechType to the hand-equipments
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("labcontaineropen1"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _labContainerOpen1 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: labContainerOpen1.GetGameObject()");
#endif
            if (_labContainerOpen1 == null)
#if SUBNAUTICA
                _labContainerOpen1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_open_01.prefab");
#else
                _labContainerOpen1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Base/biodome_lab_containers_open_01.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_labContainerOpen1);
            GameObject model = prefab.FindChild("biodome_lab_containers_open_01");

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

            // Get rigid body
            var rb = prefab.GetComponent<Rigidbody>();

            // Add world forces
            var forces = prefab.AddComponent<WorldForces>();
            forces.useRigidbody = rb;
            forces.handleGravity = true;
            forces.handleDrag = true;
            forces.aboveWaterGravity = 9.81f;
            forces.underwaterGravity = 1;
            forces.aboveWaterDrag = 0.1f;
            forces.underwaterDrag = 1;

            // Add box collider
            var collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.35f, 0.5f, 0.35f);

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
            var fabricating = model.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.36f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
