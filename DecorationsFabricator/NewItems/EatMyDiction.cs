using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class EatMyDiction : DecorationItem
    {
        public EatMyDiction()
        {
            this.ClassID = "MarlaCat"; // c96baff4-0993-4893-8345-adb8709901a7
            this.ResourcePath = "Submarine/Build/Eatmydiction";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("MarlaCatName"),
                                                        LanguageHelper.GetFriendlyWord("MarlaCatDescription"),
                                                        true);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1]
                    {
                        new IngredientHelper(TechType.FiberMesh, 2)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Add TechTag
                TechTag techTag = this.GameObject.AddComponent<TechTag>();
                techTag.name = LanguageHelper.GetFriendlyWord("MarlaCatName");
                techTag.type = this.TechType;

                // Add rigid body
                var rb = this.GameObject.AddComponent<Rigidbody>();
                rb.mass = 10;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rb.constraints = RigidbodyConstraints.FreezePosition;

                // Add world forces
                var forces = this.GameObject.AddComponent<WorldForces>();
                forces.useRigidbody = rb;
                forces.handleGravity = true;
                forces.handleDrag = true;
                forces.aboveWaterGravity = 9.81f;
                forces.underwaterGravity = 1;
                forces.aboveWaterDrag = 0.1f;
                forces.underwaterDrag = 1;
                
                // Add box collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.5f, 0.5f, 0.5f);

                // We can pick this item
                var pickupable = this.GameObject.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                var placeTool = this.GameObject.AddComponent<PlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = true;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = false;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = false;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;

                // Add the new TechType to the hand-equipments
                CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, SpriteManager.Get(TechType.EatMyDiction)));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject model = prefab.FindChild("Eatmydiction");

            // Add fabricating animation
            var fabricating = model.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.5f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
            fabricating.scaleFactor = 0.1f;

            return prefab;
        }
    }
}
