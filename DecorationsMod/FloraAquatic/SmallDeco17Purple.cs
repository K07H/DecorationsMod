using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.FloraAquatic
{
    public class SmallDeco17Purple : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

        public SmallDeco17Purple()
        {
            this.ClassID = "SmallDeco17Purple"; // c71f41ce-b586-4e85-896e-d25e8b5b9de0
            this.ResourcePath = "WorldEntities/Doodads/Coral_reef/coral_reef_small_deco_17_purple";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SmallDeco17PurpleName"),
                                                        LanguageHelper.GetFriendlyWord("AlienFloraSampleDescription"),
                                                        true);
            
            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1] {
                    new IngredientHelper(ConfigSwitcher.FloraRecipiesResource, 1)
                }),
                _techType = this.TechType
            };

            this.Config = ConfigSwitcher.config_SmallDeco17Purple;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Set item occupies 4 slots
                CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(2, 2);

                // Add the new TechType to Harvest types
                CraftDataPatcher.customHarvestTypeList.Add(this.TechType, HarvestType.DamageAlive);
                CraftDataPatcher.customHarvestOutputList.Add(this.TechType, this.TechType);

                // Set item background to normal (both land & water plant)
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
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("flora_smalldeco17purpleicon")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;
            
            // Scale model
            prefab.FindChild("coral_reef_small_deco_17_purple").transform.localScale *= 0.7f;

            // Add collider
            BoxCollider collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.4f, 0.6f, 0.4f);
            collider.center = new Vector3(collider.center.x, collider.center.y + 0.3f, collider.center.z);
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
            pickupable.cubeOnPickup = false;
            pickupable.randomizeRotationWhenDropped = true;
            pickupable.usePackUpIcon = false;

            // Add plantable
            var plantable = prefab.AddComponent<Plantable>();
            plantable.aboveWater = true;
            plantable.underwater = true;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Large;
            plantable.pickupable = pickupable;
            plantable.model = prefab;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "SmallDeco17Purple";

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
