#if SUBNAUTICA_NAUTILUS
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

namespace DecorationsMod.FloraAquatic
{
    public class SmallDeco3 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public SmallDeco3() : base("SmallDeco3", "SmallDeco3Name", "SmallDeco3Description", "flora_smalldeco03icon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public SmallDeco3()
        {
            this.ClassID = "SmallDeco3"; // 6d9e37de-f808-4621-a762-e0d6340b30dc
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SmallDeco3Name"),
                                                        LanguageHelper.GetFriendlyWord("SmallDeco3Description"),
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

            this.Config = ConfigSwitcher.config_SmallDeco3;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Set item occupies 1 slot
                CraftDataHandler.SetItemSize(this.TechType, new Vector2int(1, 1));

                // Add the new TechType to Harvest types
                CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Set item background to normal (both land & water plant)
                CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.Normal);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Specify bonus on final cut
                CraftDataHandler.SetHarvestFinalCutBonus(this.TechType, 1);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("flora_smalldeco03icon"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _smallDeco3 = null;

        public override GameObject GetGameObject()
        {
            if (_smallDeco3 == null)
#if SUBNAUTICA
                _smallDeco3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_small_deco_03.prefab");
#else
                _smallDeco3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/Coral_reef_small_deco_03.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_smallDeco3);

            prefab.name = this.ClassID;

            PrefabsHelper.AddNewGenericSeed(ref prefab);

            // Disable and destroy plant behaviour
            PlantBehaviour plantBehaviour = prefab.GetComponent<PlantBehaviour>();
            plantBehaviour.enabled = false;
            GameObject.Destroy(plantBehaviour);
            
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
            plantable.size = Plantable.PlantSize.Small;
            plantable.pickupable = pickupable;
            plantable.eatable = eatable;
            plantable.model = prefab;
            plantable.modelEulerAngles = new Vector3(plantable.modelEulerAngles.x - 90.0f, plantable.modelEulerAngles.y, plantable.modelEulerAngles.z);
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "SmallDeco3";

            // Add generic plant controller
            PlantMonoTransformController plantMonoTransformController = prefab.AddComponent<PlantMonoTransformController>();
            plantMonoTransformController.GrowthDuration = Config.GrowthDuration;
            plantMonoTransformController.Health = Config.Health;
            plantMonoTransformController.Knifeable = Config.Knifeable;
            plantMonoTransformController._origScale = new Vector3(prefab.transform.localScale.x * 0.5f, prefab.transform.localScale.y * 0.5f, prefab.transform.localScale.z * 0.5f);

            // Add flora serialize/deserializer
            CustomFloraSerializer customSerializer = prefab.AddComponent<CustomFloraSerializer>();

            // Add live mixin
            var liveMixin = prefab.GetComponent<LiveMixin>();
            liveMixin.health = Config.Health;
            liveMixin.data = ScriptableObject.CreateInstance<LiveMixinData>();
            liveMixin.data.broadcastKillOnDeath = true;
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
