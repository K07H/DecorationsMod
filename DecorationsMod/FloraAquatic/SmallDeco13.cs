using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.FloraAquatic
{
    public class SmallDeco13 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

        [SetsRequiredMembers]
        public SmallDeco13() : base("SmallDeco13", LanguageHelper.GetFriendlyWord("SmallDeco13Name"), LanguageHelper.GetFriendlyWord("AlienFloraSampleDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("flora_smalldeco13icon"))
        {
            this.ClassID = "SmallDeco13"; // 26940e53-d3eb-4770-ae99-6ce4335445d3
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

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

            this.Config = ConfigSwitcher.config_SmallDeco13;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Set item occupies 4 slots
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Harvest types
                Nautilus.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                Nautilus.Handlers.CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Set item background to normal (both land & water plant)
                Nautilus.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.Normal);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Specify bonus on final cut
                Nautilus.Handlers.CraftDataHandler.SetHarvestFinalCutBonus(this.TechType, 1);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("flora_smalldeco13icon"));

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        private static GameObject _smallDeco13 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: smallDeco13.GetGameObject()");
#endif
            if (_smallDeco13 == null)
#if SUBNAUTICA
                _smallDeco13 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/coral_reef_small_deco_13.prefab");
#else
                _smallDeco13 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_small_deco_13.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_smallDeco13);

            prefab.name = this.ClassID;

            PrefabsHelper.AddNewGenericSeed(ref prefab);

            // Scale model
            prefab.FindChild("coral_reef_small_deco_13").transform.localScale *= 0.5f;

            // Add collider
            BoxCollider collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.5f, 0.5f, 0.5f);
            collider.center = new Vector3(collider.center.x, collider.center.y + 0.25f, collider.center.z);
            collider.size *= 0.001f;

            // Add rigid body
            var rb = prefab.AddComponent<Rigidbody>();
            rb.mass = 0.5f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

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
            plantable.aboveWater = true;
            plantable.underwater = true;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Large;
            plantable.pickupable = pickupable;
            plantable.eatable = eatable;
            plantable.model = prefab;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "SmallDeco13";

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

            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

            return prefab;
        }
    }
}
