﻿#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using static CraftData;
#else
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
#endif
using DecorationsMod.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class BarBottle2 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public BarBottle2() : base("BarBottle2", "BarBottleName", "BarBottleDescription", "barbottle02icon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public BarBottle2() // Feeds abstract class
        {
            this.ClassID = "BarBottle2";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("barbottle02");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("BarBottleName"),
                                                        LanguageHelper.GetFriendlyWord("BarBottleDescription"),
                                                        true);
#endif

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Quartz, 1),
                        new Ingredient(TechType.BigFilteredWater, 1)
                    }),
            };
        }

        private static GameObject _barBottle2 = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_barBottle2 == null)
                    _barBottle2 = AssetsHelper.Assets.LoadAsset<GameObject>("barbottle02");

                GameObject model = _barBottle2.FindChild("docking_bar_bottle_02").FindChild("docking_bar_bottle_02");

                // Scale model
                model.transform.localScale *= 22f;

                // Set tech tag
                var techTag = _barBottle2.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set prefab identifier
                var prefabId = _barBottle2.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Set collider
                var collider = _barBottle2.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.07f, 0.1f, 0.07f);

                // Set large world entity
                var lwe = _barBottle2.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                var renderer = _barBottle2.GetComponentInChildren<Renderer>();
                renderer.material.shader = marmosetUber;
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("docking_bar_bottles_01_normal");
                renderer.material.SetTexture("_BumpMap", normal);
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("docking_bar_bottles_02");
                renderer.material.SetTexture("_Illum", illum);
                renderer.material.SetFloat("_EmissionLM", 1.0f); // Set always visible
                renderer.material.EnableKeyword("MARMO_EMISSION");
                renderer.material.EnableKeyword("MARMO_NORMALMAP");
                renderer.material.EnableKeyword("_ZWRITE_ON"); // Enable Z write

                // Update sky applier
                var applier = _barBottle2.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = _barBottle2.AddComponent<SkyApplier>();
                applier.renderers = new Renderer[] { renderer };
                applier.anchorSky = Skies.Auto;

                // We can pick this item
                var pickupable = _barBottle2.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _barBottle2.AddComponent<CustomPlaceToolController>();
                var placeTool = _barBottle2.AddComponent<GenericPlaceTool>();
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
                var eatable = _barBottle2.AddComponent<Eatable>();
                eatable.foodValue = 0.0f;
                eatable.waterValue = ConfigSwitcher.BarBottle2Value;
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
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Add the new TechType to Hand Equipment type.
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("barbottle02icon"));
#endif

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_barBottle2);

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
