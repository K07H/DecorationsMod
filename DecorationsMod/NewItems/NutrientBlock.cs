using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class NutrientBlock : DecorationItem
    {
        public NutrientBlock() // Feeds abstract class
        {
            this.ClassID = "30373750-1292-4034-9797-387cf576d150";
            this.ResourcePath = "WorldEntities/Food/NutrientBlock";

            this.TechType = TechType.NutrientBlock;
            KnownTechPatcher.unlockedAtStart.Add(this.TechType);

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[7]
                    {
                        new IngredientHelper(TechType.Melon, 1),
                        new IngredientHelper(TechType.HangingFruit, 1),
                        new IngredientHelper(TechType.PurpleVegetable, 1),
                        new IngredientHelper(TechType.BulboTreePiece, 1),
                        new IngredientHelper(TechType.CreepvinePiece, 1),
                        new IngredientHelper(TechType.JellyPlant, 1),
                        new IngredientHelper(TechType.KooshChunk, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
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

            // Retrieve collider
            Collider collider = prefab.GetComponent<BoxCollider>();

            // We can pick this item
            var pickupable = prefab.GetComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            var placeTool = prefab.AddComponent<NutrientBlock_PT>();
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
