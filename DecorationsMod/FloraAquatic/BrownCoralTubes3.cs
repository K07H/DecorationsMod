﻿using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.FloraAquatic
{
    public class BrownCoralTubes3 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

        [SetsRequiredMembers]
        public BrownCoralTubes3() : base("BrownCoralTubes3", LanguageHelper.GetFriendlyWord("BrownCoralTubesName") + " (3)", LanguageHelper.GetFriendlyWord("BrownCoralTubesDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("flora_browncoraltubes01icon"))
        {
            this.ClassID = "BrownCoralTubes3"; // 291856e5-9d72-4cc6-b09f-ac09a5a6206e
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;
            
            CrafterLogicFixer.BrownTubes3 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif

            this.Config = ConfigSwitcher.config_BrownCoralTubes3;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Harvest types
                Nautilus.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                Nautilus.Handlers.CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Change item background to water-plant seed
                Nautilus.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantWaterSeed);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Specify bonus on final cut
                Nautilus.Handlers.CraftDataHandler.SetHarvestFinalCutBonus(this.TechType, 1);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("flora_browncoraltubes01icon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _brownCoralTubes3 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: brownCoralTubes3.GetGameObject()");
#endif
            if (_brownCoralTubes3 == null)
#if SUBNAUTICA
                _brownCoralTubes3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/coral_reef_brown_coral_tubes_01.prefab");
#else
                _brownCoralTubes3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_brown_coral_tubes_01.prefab");
#endif

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T1");
#endif
            GameObject prefab = GameObject.Instantiate(_brownCoralTubes3);

            prefab.name = this.ClassID;

            PrefabsHelper.AddNewGenericSeed(ref prefab);

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T2");
            Logger.PrintTransform(prefab.transform);
            Logger.Debug("DEBUG: BrownCoralTube3 T2b");
#endif
            // Scale models
            prefab.FindChild("coral_reef_brown_coral_tubes_01").transform.localScale *= 0.4f;
            prefab.FindChild("coral_reef_brown_coral_tubes_01_LOD3").transform.localScale *= 0.4f;

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T3");
#endif
            // Scale and shrink colliders
            BoxCollider[] colliders = prefab.GetComponentsInChildren<BoxCollider>();
            if (colliders.Length > 0)
            {
                foreach (BoxCollider collider in colliders)
                {
                    collider.size *= 0.4f;
                    collider.size *= 0.001f;
                }
            }

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T4");
#endif
            // Update rigid body
            var rb = prefab.GetComponent<Rigidbody>();
            rb.mass = 1.0f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T5");
#endif
            // Add EntityTag
            var entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Add TechTag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T6");
#endif
            // Update prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T7");
#endif
            // Update large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Add box collider
            //BoxCollider collider = prefab.AddComponent<BoxCollider>();
            //collider.size = new Vector3(0.2f, 0.2f, 0.2f);

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T8");
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

            // Add pickupable
            var pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = false;
            pickupable.destroyOnDeath = true;
            pickupable.isLootCube = false;
            pickupable.randomizeRotationWhenDropped = true;
            pickupable.usePackUpIcon = false;

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
            plantable.aboveWater = false;
            plantable.underwater = true;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Large;
            plantable.pickupable = pickupable;
            plantable.eatable = eatable;
            plantable.model = prefab;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "BrownCoralTubes3";

            // Add generic plant controller
            PlantGenericController landPlant1Controller = prefab.AddComponent<PlantGenericController>();
            landPlant1Controller.GrowthDuration = Config.GrowthDuration;
            landPlant1Controller.Health = Config.Health;
            landPlant1Controller.Knifeable = Config.Knifeable;
            landPlant1Controller.RestoreBoxColliders = true;

            // Add flora serializer/deserializer
            CustomFloraSerializer customSerializer = prefab.AddComponent<CustomFloraSerializer>();

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

#if DEBUG_CORALS
            Logger.Debug("DEBUG: BrownCoralTube3 T9");
#endif
            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

            return prefab;
        }
    }
}
