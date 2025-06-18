﻿using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class BarBottle3 : DecorationItem
    {
        [SetsRequiredMembers]
        public BarBottle3() : base("BarBottle3", LanguageHelper.GetFriendlyWord("BarBottleName"), LanguageHelper.GetFriendlyWord("BarBottleDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("barbottle03icon")) // Feeds abstract class
        {
            this.ClassID = "BarBottle3";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("barbottle03");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.BigFilteredWater, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Quartz, 1),
                new Ingredient(TechType.BigFilteredWater, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _barBottle3 = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_barBottle3 == null)
                    _barBottle3 = AssetsHelper.Assets.LoadAsset<GameObject>("barbottle03");

                GameObject model = _barBottle3.FindChild("docking_bar_bottle_03").FindChild("docking_bar_bottle_03");

                // Scale model
                model.transform.localScale *= 22f;

                // Set tech tag
                var techTag = _barBottle3.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set prefab identifier
                var prefabId = _barBottle3.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Set collider
                var collider = _barBottle3.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.07f, 0.15f, 0.07f);

                // Set large world entity
                var lwe = _barBottle3.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("docking_bar_bottles_01_normal");
                var renderer = _barBottle3.GetComponentInChildren<Renderer>();
                renderer.material.shader = marmosetUber;
                renderer.material.SetTexture("_BumpMap", normal);
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("docking_bar_bottles_01");
                renderer.material.SetTexture("_Illum", illum);
                renderer.material.SetFloat("_EmissionLM", 1.0f); // Set always visible
                renderer.material.EnableKeyword("MARMO_EMISSION");
                renderer.material.EnableKeyword("MARMO_NORMALMAP");
                renderer.material.EnableKeyword("_ZWRITE_ON"); // Enable Z write

                // Update sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_barBottle3, null, new Renderer[] { renderer });

                // We can pick this item
                var pickupable = _barBottle3.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _barBottle3.AddComponent<CustomPlaceToolController>();
                var placeTool = _barBottle3.AddComponent<GenericPlaceTool>();
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

                // We can drink this item
                var eatable = _barBottle3.AddComponent<Eatable>();
                eatable.foodValue = 0.0f;
                eatable.waterValue = ConfigSwitcher.BarBottle3Value;
#if SUBNAUTICA
                eatable.stomachVolume = 10.0f;
                eatable.allowOverfill = false;
#endif
                eatable.decomposes = false;
                eatable.despawns = false;
                eatable.kDecayRate = 0.0f;
                eatable.despawnDelay = 0.0f;
                CraftData.useEatSound[this.TechType] = "event:/player/drink";

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add the new TechType to Hand Equipment type.
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("barbottle03icon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_barBottle3);

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
