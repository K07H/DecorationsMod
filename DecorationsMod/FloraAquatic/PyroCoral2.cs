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
using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.FloraAquatic
{
    public class PyroCoral2 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public PyroCoral2() : base("PyroCoral2", "PyroCoralName", "PyroCoralDescription", "pyrocoral2icon", "2")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public PyroCoral2()
        {
            this.ClassID = "PyroCoral2"; // efaf4fac-d240-4f51-b572-072b448ab4de
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("PyroCoralName") + " (2)",
                                                        LanguageHelper.GetFriendlyWord("PyroCoralDescription"),
                                                        true);
#endif

            CrafterLogicFixer.PyroCoral2 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

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

            this.Config = ConfigSwitcher.config_PyroCoral2;
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

                // Change item background to water-plant seed
                CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantWaterSeed);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("pyrocoral2icon"));
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

        private static GameObject _pyroCoral2 = null;

        public override GameObject GetGameObject()
        {
            if (_pyroCoral2 == null)
#if SUBNAUTICA
                _pyroCoral2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Lost_river/lost_river_pillar_02.prefab");
#else
                _pyroCoral2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/environmentprops/lostriver/lost_river_pillar_02.prefab");
#endif

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T1");
#endif
            GameObject prefab = GameObject.Instantiate(_pyroCoral2);

            prefab.name = this.ClassID;

            PrefabsHelper.AddNewGenericSeed(ref prefab);

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T2");
#endif
            // Remove light
            Light light = prefab.GetComponentInChildren<Light>();
            if (light != null)
                GameObject.DestroyImmediate(light);

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T3");
            Logger.PrintTransform(prefab.transform);
            Logger.Debug("PyroCoral2 T3b");
#endif
            // Scale prefab
            prefab.FindChild("lost_river_pillar_02").transform.localScale *= 0.2f;

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T4");
#endif
            // Shrink colliders
            Collider[] colliders = prefab.GetComponentsInChildren<Collider>();
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    collider.transform.localScale *= 0.001f;
                }
            }

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T5");
#endif
            // Update rigid body
            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            if (rb == null)
                rb = prefab.AddComponent<Rigidbody>();
            rb.mass = 1.0f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T6");
#endif
            // Add EntityTag
            EntityTag entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Add TechTag
            TechTag techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T7");
#endif
            // Update prefab identifier
            PrefabIdentifier prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T8");
#endif
            // Update large world entity
            LargeWorldEntity lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

#if DEBUG_CORALS
            Logger.Debug("PyroCoral2 T9");
#endif
            // Add world forces
            WorldForces worldForces = prefab.AddComponent<WorldForces>();
            worldForces.handleGravity = true;
            worldForces.aboveWaterGravity = 9.81f;
            worldForces.underwaterGravity = 1.0f;
            worldForces.handleDrag = true;
            worldForces.aboveWaterDrag = 0.0f;
            worldForces.underwaterDrag = 10.0f;
            worldForces.useRigidbody = rb;

            // Add pickupable
            Pickupable pickupable = prefab.AddComponent<Pickupable>();
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
            Plantable plantable = prefab.AddComponent<Plantable>();
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
            plantable.linkedGrownPlant.seedUID = "PyroCoral2";

            // Add generic plant controller
            PlantGenericController landPlant1Controller = prefab.AddComponent<PlantGenericController>();
            landPlant1Controller.GrowthDuration = Config.GrowthDuration;
            landPlant1Controller.Health = Config.Health;
            landPlant1Controller.Knifeable = Config.Knifeable;
            landPlant1Controller.RestoreColliders = true;

            CustomFloraSerializer customSerializer = prefab.AddComponent<CustomFloraSerializer>();

            // Add live mixin
            LiveMixin liveMixin = prefab.AddComponent<LiveMixin>();
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
            Logger.Debug("PyroCoral2 T10");
#endif
            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

            return prefab;
        }
    }
}
