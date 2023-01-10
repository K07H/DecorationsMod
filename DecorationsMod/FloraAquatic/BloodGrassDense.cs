using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.FloraAquatic
{
    public class BloodGrassDense : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

        public GameObject PlantBloodGrass;
        public GameObject PlantTiny;
        public GameObject Plant2;
        public GameObject Plant2Tall;
        public GameObject Plant3;
        public GameObject Plant3Tall;

        public BloodGrassDense()
        {
            this.ClassID = "BloodGrassDense";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("RedGrassDenseName") + " (2)",
                                                        LanguageHelper.GetFriendlyWord("RedGrassDescription"),
                                                        true);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                {
                    new SMLHelper.V2.Crafting.Ingredient(ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount)
                }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                {
                    new Ingredient(ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount)
                }),
            };
#endif

            this.Config = ConfigSwitcher.config_BloodGrassDense;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 1 slot
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(1, 1));

                // Add the new TechType to Harvest types
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Change item background to water-plant seed
                SMLHelper.V2.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantWaterSeed);

                // Specify bonus on final cut
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestFinalCutBonus(this.TechType, 1);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("bloodgrassdense2icon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
#if SUBNAUTICA
            this.Plant3Tall = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_red_seaweed_03_tall.prefab"); // 21df8b8f-ae64-4d0e-b838-04f55dd9d72b
            this.PlantBloodGrass = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_grass_01_red.prefab"); // ae210dd4-68f0-4c77-9025-ef7d116948b3
            this.PlantTiny = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_red_seaweed_01.prefab"); // d1dc9afd-ae78-47f5-b30c-a81258325381
            this.Plant2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_red_seaweed_02_short.prefab"); // 269dd19e-437d-4bbe-8727-2e239f0603e9
            this.Plant2Tall = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_red_seaweed_02_tall.prefab"); // 8f489a8d-e612-4ac7-86c6-fa277dd8ee62
            this.Plant3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Coral_reef/Coral_reef_red_seaweed_03_short.prefab"); // 1c8bf0ca-687f-44b9-8942-82feaa800a69
#else
            this.Plant3Tall = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_red_seaweed_03_tall.prefab"); // 21df8b8f-ae64-4d0e-b838-04f55dd9d72b
            this.PlantBloodGrass = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_grass_01_red.prefab"); // ae210dd4-68f0-4c77-9025-ef7d116948b3
            this.PlantTiny = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_red_seaweed_01.prefab"); // d1dc9afd-ae78-47f5-b30c-a81258325381
            this.Plant2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_red_seaweed_02_short.prefab"); // 269dd19e-437d-4bbe-8727-2e239f0603e9
            this.Plant2Tall = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_red_seaweed_02_tall.prefab"); // 8f489a8d-e612-4ac7-86c6-fa277dd8ee62
            this.Plant3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/shared/coral_reef_red_seaweed_03_short.prefab"); // 1c8bf0ca-687f-44b9-8942-82feaa800a69
#endif

            GameObject prefab3Tall = GameObject.Instantiate(this.Plant3Tall);
            GameObject prefab3 = GameObject.Instantiate(this.Plant3);
            GameObject prefab2Tall = GameObject.Instantiate(this.Plant2Tall);
            GameObject prefab2 = GameObject.Instantiate(this.Plant2);
            GameObject prefab2b = GameObject.Instantiate(this.Plant2);
            GameObject prefabTiny = GameObject.Instantiate(this.PlantTiny);
            GameObject prefabBloodGrass = GameObject.Instantiate(this.PlantBloodGrass);

            prefab3Tall.name = this.ClassID; // RedGrass3Tall
            prefab3.name = "RedGrass3";
            prefab2Tall.name = "RedGrass2Tall";
            prefab2.name = "RedGrass2";
            prefab2b.name = "RedGrass2b";
            prefabTiny.name = "RedGrass1";
            prefabBloodGrass.name = "BloodGrassRed";

            PrefabsHelper.AddNewGenericSeed(ref prefab3Tall);
            /*PrefabsHelper.HidePlantAndShowSeed(prefab3Tall.transform);
            PrefabsHelper.HidePlantAndShowSeed(prefab3.transform);
            PrefabsHelper.HidePlantAndShowSeed(prefab2Tall.transform);
            PrefabsHelper.HidePlantAndShowSeed(prefab2.transform);
            PrefabsHelper.HidePlantAndShowSeed(prefab2b.transform);
            PrefabsHelper.HidePlantAndShowSeed(prefabTiny.transform);
            PrefabsHelper.HidePlantAndShowSeed(prefabBloodGrass.transform);*/

            GameObject model3Tall = prefab3Tall.FindChild("Coral_reef_red_seaweed_03_tall");
            GameObject model3 = prefab3.FindChild("Coral_reef_red_seaweed_03_short");
            GameObject model2Tall = prefab2Tall.FindChild("Coral_reef_red_seaweed_02_tall");
            GameObject model2 = prefab2.FindChild("Coral_reef_red_seaweed_02_short");
            GameObject model2b = prefab2b.FindChild("Coral_reef_red_seaweed_02_short");
            GameObject modelTiny = prefabTiny.FindChild("Coral_reef_red_seaweed_01");
            GameObject modelBloodGrass = prefabBloodGrass.FindChild("Coral_reef_grass_01_red");

            // Scale model
            model3Tall.transform.localScale *= 0.8f;
            model3.transform.localScale *= 0.8f;
            model2Tall.transform.localScale *= 1f;
            model2.transform.localScale *= 0.5f;
            model2b.transform.localScale *= 0.4f;
            modelTiny.transform.localScale *= 0.2f;
            modelBloodGrass.transform.localScale *= 0.65f;
            //modelBloodGrass.transform.localEulerAngles = new Vector3(modelBloodGrass.transform.localEulerAngles.x - 90f, modelBloodGrass.transform.localEulerAngles.y, modelBloodGrass.transform.localEulerAngles.z);

            // Turn off crappy shadows
            model3Tall.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            model3.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            model2Tall.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            model2.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            model2b.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            modelTiny.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            modelBloodGrass.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            // Remove unwanted elements
            GameObject.DestroyImmediate(prefab2.GetComponent<PrefabIdentifier>());
            GameObject.DestroyImmediate(prefab2.GetComponent<LargeWorldEntity>());
            GameObject.DestroyImmediate(prefab2b.GetComponent<PrefabIdentifier>());
            GameObject.DestroyImmediate(prefab2b.GetComponent<LargeWorldEntity>());
            GameObject.DestroyImmediate(prefab2Tall.GetComponent<PrefabIdentifier>());
            GameObject.DestroyImmediate(prefab2Tall.GetComponent<LargeWorldEntity>());
            GameObject.DestroyImmediate(prefab3.GetComponent<PrefabIdentifier>());
            GameObject.DestroyImmediate(prefab3.GetComponent<LargeWorldEntity>());
            GameObject.DestroyImmediate(prefabTiny.GetComponent<PrefabIdentifier>());
            GameObject.DestroyImmediate(prefabTiny.GetComponent<LargeWorldEntity>());
            GameObject.DestroyImmediate(prefabBloodGrass.GetComponent<PrefabIdentifier>());
            GameObject.DestroyImmediate(prefabBloodGrass.GetComponent<LargeWorldEntity>());
            //GameObject.DestroyImmediate(prefab3Tall.GetComponent<PrefabIdentifier>());
            //GameObject.DestroyImmediate(prefab3Tall.GetComponent<LargeWorldEntity>());

            // Setup plants relative positions
            prefab2.transform.parent = prefab3Tall.transform;
            prefab2b.transform.parent = prefab3Tall.transform;
            prefab2Tall.transform.parent = prefab3Tall.transform;
            prefab3.transform.parent = prefab3Tall.transform;
            prefabTiny.transform.parent = prefab3Tall.transform;
            prefabBloodGrass.transform.parent = prefab3Tall.transform;
            prefab2.transform.localPosition = new Vector3(0.04f, 0f, -0.04f);
            prefab2b.transform.localPosition = new Vector3(-0.1f, 0f, 0.1f);
            prefab2Tall.transform.localPosition = Vector3.zero;
            prefab3.transform.localPosition = new Vector3(-0.1f, 0f, -0.1f);
            prefabTiny.transform.localPosition = Vector3.zero;
            prefabBloodGrass.transform.localPosition = new Vector3(0.04f, 0f, 0.04f);

            // Add collider
            BoxCollider collider = prefab3Tall.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.2f, 0.2f, 0.2f);
            collider.center = new Vector3(collider.center.x, collider.center.y + 0.1f, collider.center.z);

            // Update rigid body
            var rb = prefab3Tall.GetComponent<Rigidbody>();
            if (rb == null)
                rb = prefab3Tall.AddComponent<Rigidbody>();
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
            var entityTag = prefab3Tall.GetComponent<EntityTag>();
            if (entityTag == null)
                entityTag = prefab3Tall.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Add TechTag
            var techTag = prefab3Tall.GetComponent<TechTag>();
            if (techTag == null)
                techTag = prefab3Tall.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab identifier
            var prefabId = prefab3Tall.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update large world entity
            var lwe = prefab3Tall.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;
            //PrefabsHelper.SetDefaultLargeWorldEntity(prefab3Tall);

            // Update sky applier
            PrefabsHelper.SetDefaultSkyApplier(prefab3Tall, prefab3Tall.GetAllComponentsInChildren<MeshRenderer>(), Skies.Auto, true);

            // Update world forces
            var worldForces = prefab3Tall.GetComponent<WorldForces>();
            if (worldForces == null)
                worldForces = prefab3Tall.AddComponent<WorldForces>();
            worldForces.handleGravity = true;
            worldForces.aboveWaterGravity = 9.81f;
            worldForces.underwaterGravity = 1.0f;
            worldForces.handleDrag = true;
            worldForces.aboveWaterDrag = 0.0f;
            worldForces.underwaterDrag = 10.0f;
            worldForces.useRigidbody = rb;

            // Add pickupable
            PrefabsHelper.SetDefaultPickupable(prefab3Tall, false, true);

            // Add eatable
            Eatable eatable = null;
            if (Config.Eatable)
            {
                eatable = prefab3Tall.AddComponent<Eatable>();
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
            var plantable = prefab3Tall.GetComponent<Plantable>();
            if (plantable == null)
                plantable = prefab3Tall.AddComponent<Plantable>();
            plantable.aboveWater = false;
            plantable.underwater = true;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Small;
            plantable.pickupable = prefab3Tall.GetComponent<Pickupable>();
            plantable.eatable = eatable;
            plantable.model = prefab3Tall;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "BloodGrassDense";

            // Add generic plant controller
            PlantGenericController landPlant1Controller = prefab3Tall.AddComponent<PlantGenericController>();
            landPlant1Controller.GrowthDuration = Config.GrowthDuration;
            landPlant1Controller.Health = Config.Health;
            landPlant1Controller.Knifeable = Config.Knifeable;
            landPlant1Controller.RestoreRadius = true;

            // Add flora serializer/deserializer
            CustomFloraSerializer customSerializer = prefab3Tall.AddComponent<CustomFloraSerializer>();

            // Add live mixin
            var liveMixin = prefab3Tall.AddComponent<LiveMixin>();
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
            PrefabsHelper.HidePlantAndShowSeed(prefab3Tall.transform, this.ClassID);

            return prefab3Tall;
        }
    }
}