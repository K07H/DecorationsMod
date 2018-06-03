using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class WallMonitor1 : DecorationItem
    {
        public WallMonitor1() // Feeds abstract class
        {
            this.ClassID = "WallMonitor1"; // 0782292e-d313-468a-8816-2adba65bfba3
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/wall_monitor_01_01";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("WallMonitor1Name"),
                                                        LanguageHelper.GetFriendlyWord("WallMonitor1Description"),
                                                        true);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                    {
                        new IngredientHelper(TechType.ComputerChip, 1),
                        new IngredientHelper(TechType.WiringKit, 1),
                        new IngredientHelper(TechType.Glass, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                GameObject cube = this.GameObject.FindChild("Cube");
                
                // Update TechTag
                this.GameObject.GetComponent<TechTag>().type = this.TechType;

                // Remove rigid body to prevent bugs
                var rb = this.GameObject.GetComponent<Rigidbody>();
                GameObject.DestroyImmediate(rb);

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

                // Get box collider
                var collider = cube.GetComponent<BoxCollider>();
                //collider.size = new Vector3(0.35f, 0.5f, 0.35f);

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
                placeTool.allowedOnWalls = true;
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
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("computer1")));

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
            fabricating.localMaxY = 0.9f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
