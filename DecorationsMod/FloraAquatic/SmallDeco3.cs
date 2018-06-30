using DecorationsMod.Controllers;
using SMLHelper;
using SMLHelper.Patchers;
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

        public SmallDeco3()
        {
            this.ClassID = "SmallDeco3"; // 6d9e37de-f808-4621-a762-e0d6340b30dc
            this.ResourcePath = "WorldEntities/Doodads/Coral_reef/Coral_reef_small_deco_03";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SmallDeco3Name"),
                                                        LanguageHelper.GetFriendlyWord("SmallDeco3Description"),
                                                        true);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1] {
                    new IngredientHelper(TechType.PrecursorIonCrystal, 1)
                }),
                _techType = this.TechType
            };

            this.Config = ConfigSwitcher.config_SmallDeco3;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Set item occupies 4 slots
                CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(1, 1);

                // Add the new TechType to Harvest types
                CraftDataPatcher.customHarvestTypeList.Add(this.TechType, HarvestType.DamageAlive);
                CraftDataPatcher.customHarvestOutputList.Add(this.TechType, this.TechType);

                // Change item background to normal (both land & water plant)
                // TODO: Replace with a call to SMLHelper when pull request gets released
                DecorationsMod.CustomBackgroundTypes.Add(this.TechType, CraftData.BackgroundType.Normal);

                // Set item bioreactor charge
                // TODO: Replace with a call to SMLHelper when pull request gets released
                DecorationsMod.CustomCharges.Add(this.TechType, this.Config.Charge);

                // Specify bonus on final cut
                // TODO: Replace with a call to SMLHelper when pull request gets released
                DecorationsMod.CustomFinalCutBonusList.Add(this.TechType, 1);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("flora_smalldeco03icon")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;
            
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
            pickupable.cubeOnPickup = false;
            pickupable.randomizeRotationWhenDropped = true;
            pickupable.usePackUpIcon = false;

            // Add plantable
            var plantable = prefab.AddComponent<Plantable>();
            plantable.aboveWater = true;
            plantable.underwater = true;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Small;
            plantable.pickupable = pickupable;
            plantable.model = prefab;
            plantable.modelEulerAngles = new Vector3(plantable.modelEulerAngles.x - 90.0f, plantable.modelEulerAngles.y, plantable.modelEulerAngles.z);
            //plantable.linkedGrownPlant = new GrownPlant();
            //plantable.linkedGrownPlant.seed = plantable;
            //plantable.linkedGrownPlant.seedUID = "SmallDeco3";

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
            liveMixin.data.explodeOnDestroy = true;
            liveMixin.data.invincibleInCreative = false;
            liveMixin.data.minDamageForSound = 10.0f;
            liveMixin.data.passDamageDataOnDeath = true;
            liveMixin.data.weldable = false;
            liveMixin.data.knifeable = false;
            liveMixin.data.maxHealth = Config.Health;
            //liveMixin.startHealthPercent = 1.0f;

            return prefab;
        }
    }
}
