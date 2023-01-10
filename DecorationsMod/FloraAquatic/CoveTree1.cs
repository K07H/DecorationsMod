using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.FloraAquatic
{
    public class CoveTree1 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

        public CoveTree1()
        {
            this.ClassID = "CoveTree1"; // 0e7cc3b9-cdf2-42d9-9c1f-c11b94277c19
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("CoveTreeName"),
                                                        LanguageHelper.GetFriendlyWord("CoveTreeDescription"),
                                                        true);

            CrafterLogicFixer.CoveTree = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                {
                    new SMLHelper.V2.Crafting.Ingredient(ConfigSwitcher.FloraRecipiesResource, Convert.ToInt32((float)ConfigSwitcher.FloraRecipiesResourceAmount * 1.4f))
                }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                {
                    new Ingredient(ConfigSwitcher.FloraRecipiesResource, Convert.ToInt32((float)ConfigSwitcher.FloraRecipiesResourceAmount * 1.4f))
                }),
            };
#endif

            this.Config = ConfigSwitcher.config_CoveTree1;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Harvest types
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Set item background to normal (both land & water plant)
                SMLHelper.V2.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.Normal);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("covetreeicon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _coveTree1 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T1");
#endif
            if (_coveTree1 == null)
#if SUBNAUTICA
                _coveTree1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Lost_river/lost_river_cove_tree_01.prefab");
#else
                _coveTree1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/lostriver/lost_river_cove_tree_01.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_coveTree1);

            prefab.name = this.ClassID;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T2");
#endif
            PrefabsHelper.AddNewGenericSeed(ref prefab);

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T3");
#endif
            // Get sub objects
            GameObject model = prefab.FindChild("lost_river_cove_tree_01");
            GameObject eggs = model.FindChild("lost_river_cove_tree_01_eggs");
            GameObject shells = model.FindChild("lost_river_cove_tree_01_eggs_shells");

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T4");
#endif
            // Scale model
            model.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T5");
#endif
            // Disable colliders
            Collider[] colliders = prefab.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
                collider.enabled = false;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T6");
#endif
            // Hide eggs
            eggs.SetActive(false);
            shells.SetActive(false);

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T7");
#endif
            // Now do the components dancing :^)

            // Update rigid body
            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            if (rb == null)
                rb = prefab.AddComponent<Rigidbody>();
            rb.mass = 40.0f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T8");
#endif
            // Add EntityTag
            EntityTag entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Add TechTag
            TechTag techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T9");
#endif
            // Update prefab identifier
            PrefabIdentifier prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T10");
#endif
            // Update large world entity
            LargeWorldEntity lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T11");
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

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T12");
#endif
            // Add plantable
            Plantable plantable = prefab.AddComponent<Plantable>();
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
            plantable.linkedGrownPlant.seedUID = "CoveTree1";

            // Add cove tree controller (handles show/hide eggs capability)
            CoveTree1Controller coveTreeController = prefab.AddComponent<CoveTree1Controller>();

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T13");
#endif
            // Add ghost leviatan spawner
            if (ConfigSwitcher.GhostLeviatan_enable)
                prefab.AddComponent<GhostLeviatanSpawner>();

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T14");
#endif
            // Add generic plant controller (handles animation)
            PlantGenericController plantGenericController = prefab.AddComponent<PlantGenericController>();
            plantGenericController.GrowthDuration = Config.GrowthDuration;
            plantGenericController.Health = Config.Health;
            plantGenericController.Knifeable = Config.Knifeable;
            plantGenericController.EnableColliders = true; // Restore disabled colliders

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T15");
#endif
            // Handles saving/restoring cove tree state
            CustomFloraSerializer customSerializer = prefab.AddComponent<CustomFloraSerializer>();

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T16");
#endif
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

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T17");
#endif
            // Add atmosphere volume
            AtmosphereVolume aVolume = prefab.AddComponent<AtmosphereVolume>();
            aVolume.highDetail = true;
            aVolume.affectsVisuals = true;

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T18");
#endif
            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

#if DEBUG_COVE_TREE
            Logger.Log("DEBUG: CoveTree1 T19");
#endif
            return prefab;
        }
    }
}
