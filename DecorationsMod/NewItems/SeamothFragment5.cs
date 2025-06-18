using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class SeamothFragment5 : DecorationItem
    {
        [SetsRequiredMembers]
        public SeamothFragment5() : base("SeamothFragment5", LanguageHelper.GetFriendlyWord("SeamothFragmentName") + " (5)", LanguageHelper.GetFriendlyWord("SeamothFragmentDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("seamothfragment5icon")) // Feeds abstract class
        {
            this.ClassID = "SeamothFragment5"; // a73218d6-b307-450a-890e-ec2e2c206324
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.SeamothFragment5 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
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

                // Set quick slot type
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set item size to 2x2
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, 2, 2);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seamothfragment5icon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _seamothFragment5 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: seamothFragment5.GetGameObject()");
#endif
            if (_seamothFragment5 == null)
#if SUBNAUTICA
                _seamothFragment5 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Fragments/seamoth_fragment_05.prefab");
#else
                _seamothFragment5 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Fragments/seamoth_fragment_05.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_seamothFragment5);

            prefab.name = this.ClassID;

            // Translate
            foreach (Transform tr in prefab.transform)
                tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y + 0.25f, tr.localPosition.z);

            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            if (techTag == null)
                if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                    techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Remove unwanted components
            EntityTag entityTag = prefab.GetComponent<EntityTag>();
            if (entityTag != null)
                GameObject.DestroyImmediate(entityTag);
            EcoTarget ecoTarget = prefab.GetComponent<EcoTarget>();
            if (ecoTarget != null)
                GameObject.DestroyImmediate(ecoTarget);
            ResourceTracker resourceTracker = prefab.GetComponent<ResourceTracker>();
            if (resourceTracker != null)
                GameObject.DestroyImmediate(resourceTracker);
            WorldForces worldForces = prefab.GetComponent<WorldForces>();
            if (worldForces != null)
                GameObject.DestroyImmediate(worldForces);

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            if (prefabId == null)
                if ((prefabId = prefab.GetComponentInChildren<PrefabIdentifier>()) == null)
                    prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Remove Cube object to prevent physics
            GameObject cube = prefab.FindChild("Cube");
            if (cube != null)
                GameObject.DestroyImmediate(cube);

            // Remove rigid body to prevent physics bugs
            var rb = prefab.GetComponent<Rigidbody>();
            if (rb != null)
                GameObject.DestroyImmediate(rb);

            // Add box collider
            var collider = prefab.GetComponent<BoxCollider>();
            if (collider == null)
            {
                Collider[] colliders = prefab.GetComponentsInChildren<Collider>();
                if (colliders != null)
                    foreach (Collider c in colliders)
                        GameObject.DestroyImmediate(c);
                collider = prefab.AddComponent<BoxCollider>();
            }
            collider.size = new Vector3(0.7f, 0.7f, 0.7f);
            collider.center = new Vector3(collider.center.x + 0.1f, collider.center.y + 0.8f, collider.center.z - 1.25f);
            collider.isTrigger = true;

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

            // Update sky applier
            PrefabsHelper.ReplaceSkyApplier(prefab);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(0.0f, 0.141f, 0.3f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.2f;

            return prefab;
        }
    }
}
