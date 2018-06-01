using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class LabShelf : DecorationItem
    {
        public LabShelf() // Feeds abstract class
        {
            this.ClassID = "LabShelf"; //33acd899-72fe-4a98-85f9-b6811974fbeb
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_shelf_01";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("LabShelfName"),
                                                        LanguageHelper.GetFriendlyWord("LabShelfDescription"),
                                                        true);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1]
                    {
                        new IngredientHelper(TechType.Titanium, 2)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Delete Cube object to prevent bugs (when holding item while swimming)
                GameObject cube = this.GameObject.FindChild("Cube");
                GameObject.DestroyImmediate(cube);

                // Adjust game object position
                Vector3 currentPos = this.GameObject.transform.position;
                this.GameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + 1.8f);

                // Scale model
                GameObject model = this.GameObject.FindChild("biodome_lab_shelf_01");
                model.GetComponent<Transform>().localScale *= 0.42f;

                // Set TechTag
                this.GameObject.AddComponent<TechTag>().type = this.TechType;

                // Customize rigid body
                var rb = this.GameObject.GetComponent<Rigidbody>();
                rb.detectCollisions = true;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                rb.isKinematic = false;
                rb.mass = 60;
                rb.useGravity = true;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

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
                collider.size = new Vector3(2f, 3f, 1f);

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

                // Set item occupies 4 slots
                CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(2, 2);

                // Add the new TechType to the hand-equipments
                CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("labshelves")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            //GameObject model = prefab.FindChild("biodome_lab_shelf_01");

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.9f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.35f;

            return prefab;
        }
    }
}
