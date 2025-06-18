using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class CircuitBox3c : DecorationItem
    {
        [SetsRequiredMembers]
        public CircuitBox3c() : base("CircuitBox3c", LanguageHelper.GetFriendlyWord("CircuitBox3Name"), LanguageHelper.GetFriendlyWord("CircuitBox3Description"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3c")) // Feeds abstract class
        {
            this.ClassID = "CircuitBox3c"; // 5d5fad52-7783-4107-a68c-6a94c473e25e
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Copper, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Copper, 1)
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3c"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _circuitBox3c = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: circuitBox3c.GetGameObject()");
#endif
            if (_circuitBox3c == null)
#if SUBNAUTICA
                _circuitBox3c = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/circuit_box_01_03.prefab");
#else
                _circuitBox3c = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Base/circuit_box_01_03.prefab");
#endif

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_circuitBox3c);

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

            // Horizontal flip
            GameObject model = prefab.FindChild("Starship_circuit_box_01_03");
            model.transform.localScale = new Vector3(model.transform.localScale.x * -1.0f, model.transform.localScale.y, model.transform.localScale.z);

            // Vertical flip
            model.transform.localScale = new Vector3(model.transform.localScale.x, model.transform.localScale.y, model.transform.localScale.z * -1.0f);

            // Remove Cube object to prevent physics bugs
            GameObject cube = prefab.FindChild("Cube");
            if (cube != null)
                GameObject.DestroyImmediate(cube);

            // Remove rigid body to prevent physics bugs
            var rb = prefab.GetComponent<Rigidbody>();
            if (rb != null)
                GameObject.DestroyImmediate(rb);

            // Get box collider
            var collider = prefab.GetComponent<BoxCollider>();
            if (collider == null)
                collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.6f, 0.6f, 0.08f);

            // We can pick this item
            var pickupable = prefab.GetComponent<Pickupable>();
            if (pickupable == null)
                pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            prefab.AddComponent<CustomPlaceToolController>();
            var placeTool = prefab.GetComponent<PlaceTool>();
            if (placeTool != null)
                GameObject.DestroyImmediate(placeTool);
            placeTool = prefab.AddComponent<GenericPlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = true;
            placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
            placeTool.rotationEnabled = true;
            placeTool.enabled = true;
            placeTool.hasAnimations = false;
            placeTool.hasBashAnimation = false;
            placeTool.hasFirstUseAnimation = false;
            placeTool.mainCollider = collider;
            placeTool.pickupable = pickupable;

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.9f;
            fabricating.posOffset = new Vector3(-0.16f, 0.125f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.6f;

            return prefab;
        }
    }
}
