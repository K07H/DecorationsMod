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
    public class BarBottle5 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public BarBottle5() : base("BarBottle5", "BarBottleName", "BarBottleDescription", "barbottle05icon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public BarBottle5() // Feeds abstract class
        {
            this.ClassID = "BarBottle5";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("barbottle05");
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

        private static GameObject _barBottle5 = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_barBottle5 == null)
                    _barBottle5 = AssetsHelper.Assets.LoadAsset<GameObject>("barbottle05");

                GameObject model = _barBottle5.FindChild("docking_bar_bottle_05").FindChild("docking_bar_bottle_05");

                // Scale model
                model.transform.localScale *= 22f;

                // Set tech tag
                var techTag = _barBottle5.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set prefab identifier
                var prefabId = _barBottle5.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Set collider
                var collider = _barBottle5.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.07f, 0.15f, 0.07f);

                // Set large world entity
                var lwe = _barBottle5.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("docking_bar_bottles_01_normal");
                var renderer = _barBottle5.GetComponentInChildren<Renderer>();
                renderer.material.shader = marmosetUber;
                renderer.material.SetTexture("_BumpMap", normal);
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("docking_bar_bottles_02");
                renderer.material.SetTexture("_Illum", illum);
                renderer.material.SetFloat("_EmissionLM", 2.5f); // Set always visible
                renderer.material.EnableKeyword("MARMO_EMISSION");
                renderer.material.EnableKeyword("MARMO_NORMALMAP");
                renderer.material.EnableKeyword("_ZWRITE_ON"); // Enable Z write

                // Update sky applier
                var applier = _barBottle5.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = _barBottle5.AddComponent<SkyApplier>();
                applier.renderers = new Renderer[] { renderer };
                applier.anchorSky = Skies.Auto;
                applier.updaterIndex = 0;

                // We can pick this item
                var pickupable = _barBottle5.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _barBottle5.AddComponent<CustomPlaceToolController>();
                var placeTool = _barBottle5.AddComponent<GenericPlaceTool>();
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
                var eatable = _barBottle5.AddComponent<Eatable>();
                eatable.foodValue = 0.0f;
                eatable.waterValue = ConfigSwitcher.BarBottle5Value;
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
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("barbottle05icon"));
#endif

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_barBottle5);

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
