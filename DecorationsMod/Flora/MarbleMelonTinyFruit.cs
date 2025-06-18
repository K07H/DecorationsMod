using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using Nautilus.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.U2D;
using static CraftData;

namespace DecorationsMod.Flora
{
    public class MarbleMelonTinyFruit : DecorationItem
    {
        [SetsRequiredMembers]
        public MarbleMelonTinyFruit() : base("MarbleMelonTinyFruit", LanguageHelper.GetFriendlyWord("MarbleMelonTinyFruitName"), LanguageHelper.GetFriendlyWord("MarbleMelonTinyFruitDescription"), SpriteManager.Get(TechType.SmallMelon))
        {
            this.ClassID = "MarbleMelonTinyFruit"; // e9445fdf-fbae-49dc-a005-48c05bf9f401
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.MarbleMelonTinyFruit = this.TechType;


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
        }

        private static bool _marbleMelonTinyFruitInitialized = false;
        public static void InitMarbleMelonTinyFruitHarvestOutput(TechType marbleMelonTinyFruit)
        {
            if (!_marbleMelonTinyFruitInitialized)
            {
                if (marbleMelonTinyFruit != TechType.None && CrafterLogicFixer.MarbleMelonTiny != TechType.None)
                {
                    Nautilus.Handlers.CraftDataHandler.SetHarvestOutput(marbleMelonTinyFruit, CrafterLogicFixer.MarbleMelonTiny);
                    _marbleMelonTinyFruitInitialized = true;
                }
            }
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(1, 1));

                // Add the new TechType to Harvest types
                Nautilus.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                InitMarbleMelonTinyFruitHarvestOutput(this.TechType);
                Nautilus.Handlers.CraftDataHandler.SetHarvestFinalCutBonus(this.TechType, 3);

                // Change item background to air-plant seed
                Nautilus.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantAirSeed);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, ConfigSwitcher.config_MarbleMelonTiny.Charge);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.SmallMelon));

                this.IsRegistered = true;
            }
        }

        private static GameObject _marbleMelonTinyFruit = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: marbleMelonTinyFruit.GetGameObject()");
#endif
            if (_marbleMelonTinyFruit == null)
#if SUBNAUTICA
                _marbleMelonTinyFruit = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Land/farming_plant_01_02.prefab");
#else
                _marbleMelonTinyFruit = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Flora/FloatingIslands/farming_plant_01_02.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_marbleMelonTinyFruit);
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
            pickupable.isLootCube = false;
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
            liveMixin.data.invincibleInCreative = false;
            liveMixin.data.minDamageForSound = 10.0f;
            liveMixin.data.passDamageDataOnDeath = true;
            liveMixin.data.weldable = false;
            liveMixin.data.knifeable = false;
            liveMixin.data.maxHealth = ConfigSwitcher.config_MarbleMelonTiny.Health;
            //liveMixin.startHealthPercent = 1.0f;

            // Adjust sky applier
            PrefabsHelper.RefreshSkyApplier(prefab); // dynamic true?

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
