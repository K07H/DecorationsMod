using DecorationsMod.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.FloraAquatic
{
    class RedGrass2 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

        [SetsRequiredMembers]
        public RedGrass2() : base("RedGrass2", LanguageHelper.GetFriendlyWord("RedGrassName") + " (2)", LanguageHelper.GetFriendlyWord("RedGrassDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("redgrass1icon"))
        {
            this.ClassID = "RedGrass2"; // 269dd19e-437d-4bbe-8727-2e239f0603e9
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

            this.Config = ConfigSwitcher.config_RedGrass2;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Set item occupies 1 slot
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(1, 1));

                // Add the new TechType to Harvest types
                Nautilus.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                Nautilus.Handlers.CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Change item background to water-plant seed
                Nautilus.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantWaterSeed);

                // Specify bonus on final cut
                Nautilus.Handlers.CraftDataHandler.SetHarvestFinalCutBonus(this.TechType, 1);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("redgrass1icon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _redGrass2 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: redGrass2.GetGameObject()");
#endif
            if (_redGrass2 == null)
#if SUBNAUTICA
                _redGrass2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_red_seaweed_02_short.prefab");
#else
                _redGrass2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_red_seaweed_02_short.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_redGrass2);
            prefab.name = this.ClassID;

            PrefabsHelper.AddNewGenericSeed(ref prefab);

            // Get model
            GameObject model = prefab.FindChild("Coral_reef_red_seaweed_02_short");

            // Scale model
            model.transform.localScale *= 0.5f;

            // Turn off crappy shadows
            var renderer = model.GetComponent<MeshRenderer>();
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            // Add collider
            BoxCollider collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.2f, 0.2f, 0.2f);
            collider.center = new Vector3(collider.center.x, collider.center.y + 0.15f, collider.center.z);

            // Update rigid body
            var rb = prefab.GetComponent<Rigidbody>();
            if (rb == null)
                rb = prefab.AddComponent<Rigidbody>();
            rb.mass = 0.75f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.detectCollisions = false;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

            // Add EntityTag
            var entityTag = prefab.GetComponent<EntityTag>();
            if (entityTag == null)
                entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Add TechTag
            var techTag = prefab.GetComponent<TechTag>();
            if (techTag == null)
                techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Set sky applier
            PrefabsHelper.UpdateOrAddSkyApplier(prefab, null, new Renderer[] { renderer }); // dynamic true?

            // Update world forces
            var worldForces = prefab.GetComponent<WorldForces>();
            if (worldForces == null)
                worldForces = prefab.AddComponent<WorldForces>();
            worldForces.handleGravity = true;
            worldForces.aboveWaterGravity = 9.81f;
            worldForces.underwaterGravity = 1.0f;
            worldForces.handleDrag = true;
            worldForces.aboveWaterDrag = 0.0f;
            worldForces.underwaterDrag = 10.0f;
            worldForces.useRigidbody = rb;

            // Add pickupable
            PrefabsHelper.SetDefaultPickupable(prefab, false, true);

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
            var plantable = prefab.GetComponent<Plantable>();
            if (plantable == null)
                plantable = prefab.AddComponent<Plantable>();
            plantable.aboveWater = false;
            plantable.underwater = true;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Small;
            plantable.pickupable = prefab.GetComponent<Pickupable>();
            plantable.eatable = eatable;
            plantable.model = prefab;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "RedGrass2";

            // Add generic plant controller
            PlantGenericController landPlant1Controller = prefab.AddComponent<PlantGenericController>();
            landPlant1Controller.GrowthDuration = Config.GrowthDuration;
            landPlant1Controller.Health = Config.Health;
            landPlant1Controller.Knifeable = Config.Knifeable;
            landPlant1Controller.RestoreRadius = true;

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

            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

            return prefab;
        }
    }
}
