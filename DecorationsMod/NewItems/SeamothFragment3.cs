using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class SeamothFragment3 : DecorationItem
    {
        public SeamothFragment3() // Feeds abstract class
        {
            this.ClassID = "SeamothFragment3"; // 284573d8-9a80-4867-a09a-85df573c29ef
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

#if SUBNAUTICA
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Fragments/seamoth_fragment_03");
#else
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Alterra/Fragments/seamoth_fragment_03");
#endif

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SeamothFragmentName") + " (3)",
                                                        LanguageHelper.GetFriendlyWord("SeamothFragmentDescription"),
                                                        true);

            CrafterLogicFixer.SeamothFragment3 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 2)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 2)
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

                // Add the new TechType to the hand-equipments
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set item size to 2x2
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, 2, 2);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seamothfragment3icon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

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
            collider.size = new Vector3(0.4f, 0.7f, 0.4f);
            collider.center = new Vector3(collider.center.x + 0.15f, collider.center.y + 1.25f, collider.center.z + 0.4f);

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
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(0.1f, 0.141f, 0.3f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.2f;

            return prefab;
        }
    }
}
