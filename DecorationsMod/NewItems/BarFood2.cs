using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class BarFood2 : DecorationItem
    {
        [SetsRequiredMembers]
        public BarFood2() : base("BarFood2", LanguageHelper.GetFriendlyWord("BarFood2Name"), LanguageHelper.GetFriendlyWord("BarFood2Description"), AssetsHelper.Assets.LoadAsset<Sprite>("barfood02icon")) // Feeds abstract class
        {
            this.ClassID = "BarFood2";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("barfood02");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.DisinfectedWater, 1),
                new Ingredient(TechType.CuredReginald, 1),
                new Ingredient(TechType.Melon, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.DisinfectedWater, 1),
                new Ingredient(TechType.CuredReginald, 1),
                new Ingredient(TechType.Melon, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _barFood2 = null;
        

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_barFood2 == null)
                    _barFood2 = AssetsHelper.Assets.LoadAsset<GameObject>("barfood02");

                GameObject model = _barFood2.FindChild("docking_food_01_tray7").FindChild("docking_food_01_tray7");

                // Scale model
                model.transform.localScale *= 40f;

                // Set tech tag
                var techTag = _barFood2.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set prefab identifier
                var prefabId = _barFood2.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Set collider
                var collider = _barFood2.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.4f, 0.09f, 0.3f);

                // Set large world entity
                var lwe = _barFood2.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("docking_food_01_normal");
                var renderer = _barFood2.GetComponentInChildren<Renderer>();
                renderer.material.shader = marmosetUber;
                renderer.material.SetTexture("_BumpMap", normal);
                renderer.material.EnableKeyword("MARMO_NORMALMAP");
                renderer.material.EnableKeyword("_ZWRITE_ON"); // Enable Z write

                // Update sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_barFood2, null, new Renderer[] { renderer });

                // We can pick this item
                var pickupable = _barFood2.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _barFood2.AddComponent<CustomPlaceToolController>();
                var placeTool = _barFood2.AddComponent<GenericPlaceTool>();
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
                placeTool.drawTime = 0.5f;
                placeTool.dropTime = 1;
                placeTool.holsterTime = 0.35f;

                // We can eat this item
                var eatable = _barFood2.AddComponent<Eatable>();
                eatable.foodValue = ConfigSwitcher.BarFood2FoodValue;
                eatable.waterValue = ConfigSwitcher.BarFood2WaterValue;
#if SUBNAUTICA
                eatable.stomachVolume = 10.0f;
                eatable.allowOverfill = false;
#endif
                eatable.decomposes = false;
                eatable.despawns = false;
                eatable.kDecayRate = 0.0f;
                eatable.despawnDelay = 0.0f;

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add the new TechType to Hand Equipment type.
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("barfood02icon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_barFood2);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.1f;
            fabricatingA.localMaxY = 0.6f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1.0f;

            return prefab;
        }
    }
}
