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
            this.PrefabFileName = "WorldEntities/Food/NutrientBlock";

            this.TechType = TechType.NutrientBlock;
            SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(this.TechType);

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[7]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Melon, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.HangingFruit, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.PurpleVegetable, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.BulboTreePiece, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.CreepvinePiece, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.JellyPlant, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.KooshChunk, 1)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Add the new TechType to the hand-equipments
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);
                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);
                // Set the custom icon
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.NutrientBlock));
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
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
            GameObject child = prefab.FindChild("Nutrient_block");
            Collider collider = child.GetComponent<BoxCollider>();

            // We can pick this item
            var pickupable = prefab.GetComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            var placeTool = prefab.AddComponent<NutrientBlock_PT>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = false;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = false;
            placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
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
