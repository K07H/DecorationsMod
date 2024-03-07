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
    public class SeamothFragment4 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public SeamothFragment4() : base("SeamothFragment4", "SeamothFragmentName", "SeamothFragmentDescription", "seamothfragment4icon", "4")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public SeamothFragment4() // Feeds abstract class
        {
            this.ClassID = "SeamothFragment4"; // b9764db6-1f2a-4cfc-bda0-8a179cb7e155
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SeamothFragmentName") + " (4)",
                                                        LanguageHelper.GetFriendlyWord("SeamothFragmentDescription"),
                                                        true);
#endif

            CrafterLogicFixer.SeamothFragment4 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1)
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

                // Add the new TechType to the hand-equipments
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set item size to 2x2
                CraftDataHandler.SetItemSize(this.TechType, 2, 2);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seamothfragment4icon"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _seamothFragment4 = null;

        public override GameObject GetGameObject()
        {
            if (_seamothFragment4 == null)
#if SUBNAUTICA
                _seamothFragment4 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Fragments/seamoth_fragment_04.prefab");
#else
                _seamothFragment4 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Fragments/seamoth_fragment_04.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_seamothFragment4);

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
            collider.size = new Vector3(0.9f, 0.7f, 0.7f);
            collider.center = new Vector3(collider.center.x + 0.7f, collider.center.y + 0.6f, collider.center.z - 0.2f);
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
            PrefabsHelper.ReplaceSkyApplier(prefab, true);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(-0.1f, 0.141f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.2f;

            return prefab;
        }
    }
}
