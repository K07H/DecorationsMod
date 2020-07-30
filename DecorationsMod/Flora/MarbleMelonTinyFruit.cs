using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.Flora
{
    public class MarbleMelonTinyFruit : DecorationItem
    {
        public MarbleMelonTinyFruit()
        {
            this.ClassID = "MarbleMelonTinyFruit"; // e9445fdf-fbae-49dc-a005-48c05bf9f401
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

#if BELOWZERO
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Flora/FloatingIslands/farming_plant_01_02");
#else
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Doodads/Land/farming_plant_01_02");
#endif

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("MarbleMelonTinyFruitName"),
                                                        LanguageHelper.GetFriendlyWord("MarbleMelonTinyFruitDescription"),
                                                        true);

            CrafterLogicFixer.MarbleMelonTinyFruit = this.TechType;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1] {
                    new SMLHelper.V2.Crafting.Ingredient(ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount)
                }),
            };
#endif
        }

        private static bool _marbleMelonTinyFruitInitialized = false;
        public static void InitMarbleMelonTinyFruitHarvestOutput(TechType marbleMelonTinyFruit)
        {
            if (!_marbleMelonTinyFruitInitialized)
            {
                if (marbleMelonTinyFruit != TechType.None && CrafterLogicFixer.MarbleMelonTiny != TechType.None)
                {
                    SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestOutput(marbleMelonTinyFruit, CrafterLogicFixer.MarbleMelonTiny);
                    _marbleMelonTinyFruitInitialized = true;
                }
            }
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(1, 1));

                // Add the new TechType to Harvest types
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                InitMarbleMelonTinyFruitHarvestOutput(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestFinalCutBonus(this.TechType, 3);

                // Change item background to air-plant seed
                SMLHelper.V2.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantAirSeed);

                // Set item bioreactor charge
                SMLHelper.V2.Handlers.BioReactorHandler.SetBioReactorCharge(this.TechType, ConfigSwitcher.config_MarbleMelonTiny.Charge);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.SmallMelon));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            prefab.name = this.ClassID;

            GameObject.DestroyImmediate(prefab.GetComponent<PickPrefab>());
            GameObject.DestroyImmediate(prefab.GetComponent<LiveMixin>());

            PrefabsHelper.AddNewGenericSeed(ref prefab);

            var model = prefab.FindChild("farming_plant_01_02");

            // Scale
            float scaleRatio = 0.75f;
            model.transform.localScale *= scaleRatio;

            // Update rigid body
            var rb = prefab.GetComponent<Rigidbody>();

            // Add EntityTag
            var entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Add TechTag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

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
            pickupable.isPickupable = true;
            pickupable.destroyOnDeath = true;
#if BELOWZERO
            pickupable.isLootCube = false;
#else
            pickupable.cubeOnPickup = false;
#endif
            pickupable.randomizeRotationWhenDropped = true;
            pickupable.usePackUpIcon = false;

            // Add eatable
            Eatable eatable = null;
            if (ConfigSwitcher.config_MarbleMelonTiny.Eatable)
            {
                eatable = prefab.AddComponent<Eatable>();
                eatable.foodValue = ConfigSwitcher.config_MarbleMelonTiny.FoodValue;
                eatable.waterValue = ConfigSwitcher.config_MarbleMelonTiny.WaterValue;
#if SUBNAUTICA
                eatable.stomachVolume = 10.0f;
                eatable.allowOverfill = false;
#endif
                eatable.decomposes = ConfigSwitcher.config_MarbleMelonTiny.Decomposes;
                eatable.kDecayRate = ConfigSwitcher.config_MarbleMelonTiny.KDecayRate;
                eatable.despawns = ConfigSwitcher.config_MarbleMelonTiny.Despawns;
                eatable.despawnDelay = ConfigSwitcher.config_MarbleMelonTiny.DespawnDelay;
            }

            // Add plantable
            var plantable = prefab.AddComponent<Plantable>();
            plantable.aboveWater = true;
            plantable.underwater = false;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Small;
            plantable.pickupable = pickupable;
            plantable.eatable = eatable;
            plantable.model = prefab; // prefab.FindChild("farming_plant_01_02");
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "MarbleMelonTinyFruit";

            // Add live mixin
            var liveMixin = prefab.AddComponent<LiveMixin>();
            liveMixin.health = ConfigSwitcher.config_MarbleMelonTiny.Health;
            liveMixin.data = ScriptableObject.CreateInstance<LiveMixinData>();
            liveMixin.data.broadcastKillOnDeath = false;
            liveMixin.data.canResurrect = false;
            liveMixin.data.destroyOnDeath = true;
#if SUBNAUTICA
            liveMixin.data.explodeOnDestroy = false;
#endif
            liveMixin.data.invincibleInCreative = false;
            liveMixin.data.minDamageForSound = 10.0f;
            liveMixin.data.passDamageDataOnDeath = true;
            liveMixin.data.weldable = false;
            liveMixin.data.knifeable = false;
            liveMixin.data.maxHealth = ConfigSwitcher.config_MarbleMelonTiny.Health;
            //liveMixin.startHealthPercent = 1.0f;

            // Adjust sky applier
            SkyApplier sa = prefab.GetComponent<SkyApplier>();
            sa.renderers = prefab.GetComponentsInChildren<MeshRenderer>();
            sa.anchorSky = Skies.Auto;
            sa.dynamic = true;

            // Adjust colliders
            CapsuleCollider cc = prefab.GetComponentInChildren<CapsuleCollider>();
            cc.radius *= scaleRatio;
            cc.height *= scaleRatio;
            SphereCollider sc = prefab.GetComponentInChildren<SphereCollider>();
            sc.radius *= scaleRatio;

            // Add generic plant controller
            PlantGenericController plantController = prefab.AddComponent<PlantGenericController>();
            plantController.GrowthDuration = ConfigSwitcher.config_MarbleMelonTiny.GrowthDuration;
            plantController.Health = ConfigSwitcher.config_MarbleMelonTiny.Health;
            plantController.Knifeable = ConfigSwitcher.config_MarbleMelonTiny.Knifeable;

            CustomFloraSerializer customSerializer = prefab.AddComponent<CustomFloraSerializer>();

            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

            return prefab;
        }

    }
}
