using DecorationsMod.Controllers;
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
            //SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(this.TechType);

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[7]
                    {
                        new Ingredient(TechType.Melon, 1),
                        new Ingredient(TechType.HangingFruit, 1),
                        new Ingredient(TechType.PurpleVegetable, 1),
                        new Ingredient(TechType.BulboTreePiece, 1),
                        new Ingredient(TechType.CreepvinePiece, 1),
                        new Ingredient(TechType.JellyPlant, 1),
                        new Ingredient(TechType.KooshChunk, 1)
                    }),
            };
#else
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

#if BELOWZERO
                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
#endif

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

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
            prefab.AddComponent<CustomPlaceToolController>();
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
