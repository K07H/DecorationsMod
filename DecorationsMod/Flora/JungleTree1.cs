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

namespace DecorationsMod.Flora
{
    public class JungleTree1 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public JungleTree1() : base("JungleTree1", "JungleTree1Name", "JungleTree1Description", "jungletree1icon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public JungleTree1()
        {
            this.ClassID = "JungleTree1"; // abe4426a-5968-40b0-9d99-b06207984aa8
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("JungleTree1Name"),
                                                        LanguageHelper.GetFriendlyWord("JungleTree1Description"),
                                                        true);
#endif

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                {
                    new Ingredient(ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount)
                }),
            };

            this.Config = ConfigSwitcher.config_JungleTree1;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Set item occupies 4 slots
                CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Harvest types
                CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Change item background to air-plant seed
                CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantAirSeed);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("jungletree1icon"));
#endif

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _jungleTree1 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): A");
#endif
            if (_jungleTree1 == null)
#if SUBNAUTICA
                _jungleTree1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Land/Jungle Tree 3a.prefab");
#else
                _jungleTree1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/old/jungle tree 3a.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_jungleTree1);

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): B");
#endif
            prefab.name = this.ClassID;

            PrefabsHelper.AddNewGenericSeed(ref prefab);

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): C");
#endif
            // Scale model
            prefab.FindChild("Jungle_Tree_3a").transform.localScale *= 0.05f;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): D");
#endif
            // Add rigid body
            var rb = prefab.AddComponent<Rigidbody>();
            rb.mass = 10.0f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): E");
#endif
            // Add EntityTag
            var entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): F");
#endif
            // Add TechTag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): G");
#endif
            // Update prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): H");
#endif
            // Add box collider
            var collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.7f, 0.7f, 0.7f);

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): I");
#endif
            // Add large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            if (lwe == null)
                lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): J");
#endif
            // Add world forces
            var worldForces = prefab.AddComponent<WorldForces>();
            worldForces.handleGravity = true;
            worldForces.aboveWaterGravity = 9.81f;
            worldForces.underwaterGravity = 1.0f;
            worldForces.handleDrag = true;
            worldForces.aboveWaterDrag = 0.0f;
            worldForces.underwaterDrag = 10.0f;
            worldForces.useRigidbody = rb;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): K");
#endif
            // Add pickupable
            var pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = false;
            pickupable.destroyOnDeath = true;
            pickupable.isLootCube = false;
            pickupable.randomizeRotationWhenDropped = true;
            pickupable.usePackUpIcon = false;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): L");
#endif

            // Add eatable
            Eatable eatable = null;
            if (Config.Eatable)
            {
                eatable = prefab.AddComponent<Eatable>();
                eatable.foodValue = Config.FoodValue;
                eatable.waterValue = Config.WaterValue;
#if SUBNAUTICA
                eatable.stomachVolume = 10.0f;
                eatable.allowOverfill = false;
#endif
                eatable.decomposes = Config.Decomposes;
                eatable.kDecayRate = Config.KDecayRate;
                eatable.despawns = Config.Despawns;
                eatable.despawnDelay = Config.DespawnDelay;
            }

            // Add plantable
            var plantable = prefab.AddComponent<Plantable>();
            plantable.aboveWater = true;
            plantable.underwater = false;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Large;
            plantable.pickupable = pickupable;
            plantable.eatable = eatable;
            plantable.model = prefab;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "JungleTree1";

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): M");
#endif
            // Add tree 1 controller
            PlantGenericController landPlant1Controller = prefab.AddComponent<PlantGenericController>();
            landPlant1Controller.GrowthDuration = Config.GrowthDuration;
            landPlant1Controller.Health = Config.Health;
            landPlant1Controller.Knifeable = Config.Knifeable;

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): N");
#endif
            CustomFloraSerializer customSerializer = prefab.AddComponent<CustomFloraSerializer>();

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): O");
#endif
            // Add live mixin
            var liveMixin = prefab.AddComponent<LiveMixin>();
            liveMixin.health = Config.Health;
            liveMixin.data = ScriptableObject.CreateInstance<LiveMixinData>();
            liveMixin.data.broadcastKillOnDeath = false;
            liveMixin.data.canResurrect = false;
            liveMixin.data.destroyOnDeath = true;
            liveMixin.data.invincibleInCreative = false;
            liveMixin.data.minDamageForSound = 10.0f;
            liveMixin.data.passDamageDataOnDeath = true;
            liveMixin.data.weldable = false;
            liveMixin.data.knifeable = false;
            liveMixin.data.maxHealth = Config.Health;
            //liveMixin.startHealthPercent = 1.0f;

            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

#if DEBUG_FLORA
            Logger.Debug("JungleTree1->GetGameObject(): P");
#endif
            return prefab;
        }
    }
}
