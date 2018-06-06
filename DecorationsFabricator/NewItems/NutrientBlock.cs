using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class NutrientBlock : DecorationItem
    {
        public NutrientBlock() // Feeds abstract class
        {
            this.ClassID = "CustomNutrientBlock";
            this.ResourcePath = "WorldEntities/Food/NutrientBlock";

            this.TechType = TechType.NutrientBlock;
            KnownTechPatcher.unlockedAtStart.Add(this.TechType);

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[4]
                    {
                        new IngredientHelper(TechType.Melon, 1),
                        new IngredientHelper(TechType.HangingFruit, 1),
                        new IngredientHelper(TechType.PurpleVegetable, 1),
                        new IngredientHelper(TechType.BulboTreePiece, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Retrieve collider
                Collider collider = this.GameObject.GetComponent<BoxCollider>();

                // We can pick this item
                var pickupable = this.GameObject.GetComponent<Pickupable>();
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
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;

                // Add the new TechType to the hand-equipments
                CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);
                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, this.GetPrefab));
                // Set the custom icon
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, SpriteManager.Get(TechType.NutrientBlock)));
                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Translate
            GameObject model = this.GameObject.FindChild("Nutrient_block");
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.06f, model.transform.localPosition.z);

            // Add fabricating animation
            var fabricating = prefab.FindChild("Nutrient_block").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.4f;
            fabricating.posOffset = new Vector3(0f, 0.12f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.8f;

            return prefab;
        }
    }
}
