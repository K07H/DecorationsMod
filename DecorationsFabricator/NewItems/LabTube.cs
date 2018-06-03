using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



namespace DecorationsFabricator.NewItems
{
    public class LabTube : DecorationItem
    {
        public LabTube() // Feeds abstract class
        {
            this.ClassID = "DecorationLabTube";

            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_tube_01";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("LabTubeName"),
                                                        LanguageHelper.GetFriendlyWord("LabTubeDescription"),
                                                        true);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[2]
                    {
                        new IngredientHelper(TechType.Titanium, 2),
                        new IngredientHelper(TechType.Glass, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Delete the "Capsule" item as it conflicts with PlaceTool
                // when you hold it while swimming (see https://www.youtube.com/watch?v=mGCpK4Z8yQM)
                GameObject Capsule = this.GameObject.FindChild("Capsule");
                GameObject.DestroyImmediate(Capsule);

                // Scale model
                GameObject model = this.GameObject.FindChild("biodome_lab_tube_01");
                model.GetComponent<Transform>().localScale *= 0.4f;
                
                // Set TechTag
                this.GameObject.AddComponent<TechTag>().type = this.TechType;

                // Remove rigid body to prevent bugs
                var rb = this.GameObject.GetComponent<Rigidbody>();
                GameObject.DestroyImmediate(rb);

                /*
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.detectCollisions = false;
                rb.mass = 80;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                */
                // Add world forces
                /*
                var forces = this.GameObject.AddComponent<WorldForces>();
                forces.useRigidbody = rb;
                forces.handleGravity = true;
                forces.handleDrag = true;
                forces.aboveWaterGravity = 9.81f;
                forces.underwaterGravity = 1;
                forces.aboveWaterDrag = 0.1f;
                forces.underwaterDrag = 1;
                */

                // Add box collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.5f, 2.5f, 0.5f);

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
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = false;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.pickupable = pickupable;
                placeTool.mainCollider = collider;
                
                // Set item occupies 9 slots
                CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(3, 3);

                // Add the new TechType to the hand-equipments
                CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("labtubeicon")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;
                
                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.3f;

            return prefab;
        }
    }
}
