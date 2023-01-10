using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.Flora
{
    public class LandTree1 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = null;
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

        private GameObject staticPart = null;
        private Shader marmosetUber = null;
        private Texture normal = null;
        private Texture illum = null;
        private Texture normal2 = null;
        private Texture illum2 = null;
        private Texture spec2 = null;

        public LandTree1()
        {
            this.ClassID = "LandTree1"; // 1cc51be0-8ea9-4730-936f-23b562a9256f
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("LandTree1Name"),
                                                        LanguageHelper.GetFriendlyWord("LandTree1Description"),
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

            this.Config = ConfigSwitcher.config_LandTree1;
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Load static part of the model
                this.staticPart = AssetsHelper.Assets.LoadAsset<GameObject>("Land_tree_01_static");

                // Load shaders/textures
                marmosetUber = Shader.Find("MarmosetUBER");
                normal = AssetsHelper.Assets.LoadAsset<Texture>("Land_tree_01_trunk_normal");
                illum = AssetsHelper.Assets.LoadAsset<Texture>("Land_tree_01_trunk_illum");
                normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Land_tree_01_leaves_normal");
                illum2 = AssetsHelper.Assets.LoadAsset<Texture>("Land_tree_01_leaves_illum");
                spec2 = AssetsHelper.Assets.LoadAsset<Texture>("Land_tree_01_leaves_spec");

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Harvest types
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                SMLHelper.V2.Handlers.CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Change item background to air-plant seed
                SMLHelper.V2.Handlers.CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantAirSeed);
                
                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);
                
                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("landtree1seedicon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _landTree1 = null;

        public override GameObject GetGameObject()
        {
            if (_landTree1 == null)
#if SUBNAUTICA
                _landTree1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Land/Land_tree_01.prefab");
#else
                _landTree1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/flora/old/land_tree_01.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_landTree1);
            GameObject staticPrefab = GameObject.Instantiate(this.staticPart);

            prefab.name = this.ClassID;

            PrefabsHelper.AddNewGenericSeed(ref prefab);

            // Scale sub objects
            prefab.FindChild("Land_tree_01").transform.localScale *= 0.33f;
            prefab.FindChild("Capsule").transform.localScale *= 0.34f;
            prefab.FindChild("Land_tree_01_LOD1").transform.localScale *= 0.34f;
            prefab.FindChild("Land_tree_01_LOD2").transform.localScale *= 0.34f;

            // Update static part of the model, border shader and normal/emission maps
            GameObject staticModel = staticPrefab.FindChild("Land_tree_01_static").FindChild("Land_tree_01_static");
            MeshRenderer staticRenderer = staticModel.GetComponent<MeshRenderer>();
            foreach (Material tmpMat in staticRenderer.materials)
            {
                if (string.Compare(tmpMat.name, "Land_tree_01_trunk (Instance)", true, CultureInfo.InvariantCulture) == 0)
                {
                    tmpMat.shader = marmosetUber;
                    tmpMat.SetTexture("_BumpMap", normal); // Set normal map
                    tmpMat.SetTexture("_Illum", illum); // Set illum map
                    tmpMat.SetFloat("_EmissionLM", 1.5f); // Increase brightness
                    tmpMat.EnableKeyword("MARMO_NORMALMAP"); // Enable normal map
                    tmpMat.EnableKeyword("MARMO_EMISSION"); // Enable emission map
                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                }
                else
                {
                    tmpMat.SetTexture("_BumpMap", normal2); // Set normal map
                    tmpMat.SetTexture("_Illum", illum2); // Set illum map
                    tmpMat.SetTexture("_SpecTex", spec2); // Set spec texture
                    tmpMat.SetFloat("_EmissionLM", 2.5f); // Increase brightness

                    // Enable specular
                    tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                    tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                    tmpMat.EnableKeyword("MARMO_MIP_GLOSS");
                    // Enable normal map
                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                    // Enable emission map
                    tmpMat.EnableKeyword("MARMO_EMISSION");
                    // Enable Z write
                    tmpMat.EnableKeyword("_ZWRITE_ON");
                }
            }
            
            // Update rigid body
            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            rb.mass = 20.0f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

            // Add box collider
            BoxCollider collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.26f, 0.07f, 0.26f);

            // Add EntityTag
            EntityTag entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Add TechTag
            TechTag techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab identifier
            PrefabIdentifier prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update large world entity
            LargeWorldEntity lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

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
            plantable.aboveWater = true;
            plantable.underwater = false;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Large;
            plantable.pickupable = pickupable;
            plantable.eatable = eatable;
            plantable.model = prefab;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "LandTree1";

            LandTree1Controller landTree1Controller = prefab.AddComponent<LandTree1Controller>();
            landTree1Controller.GrowthDuration = Config.GrowthDuration;
            landTree1Controller.Health = Config.Health;
            landTree1Controller.Knifeable = Config.Knifeable;
            landTree1Controller.StaticPrefab = staticPrefab;

            CustomFloraSerializer customSerializer = prefab.AddComponent<CustomFloraSerializer>();

            // Add growing plant
            /*
            var growingPlant = prefab.AddComponent<GrowingPlant>();
            growingPlant.grownModelPrefab = prefab;
            growingPlant.growingTransform = prefab.transform;
            growingPlant.growthDuration = 100.0f;
            //growingPlant.growthHeight = new AnimationCurve();
            growingPlant.growthHeightIndoor = new AnimationCurve();
            //growingPlant.growthWidth = new AnimationCurve();
            growingPlant.growthWidthIndoor = new AnimationCurve();
            //growingPlant.heightProgressFactor = 1.5f;
            growingPlant.isPickupable = true;
            growingPlant.seed = plantable;
            growingPlant.EnableIndoorState();
            //growingPlant.SetMaxHeight(2.0f);
            */

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
            liveMixin.data.knifeable = false; // At this stage it's growing
            liveMixin.data.maxHealth = Config.Health;
            //liveMixin.startHealthPercent = 1.0f;

            // Configure static renderer
            staticPrefab.transform.parent = prefab.transform;
            staticPrefab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            staticPrefab.transform.localScale = new Vector3(12f, 12f, 12f);
            staticPrefab.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            // Update sky applier
            SkyApplier skyApplier = prefab.GetComponent<SkyApplier>();
            skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>();
            skyApplier.anchorSky = Skies.Auto;

            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

            return prefab;
        }
    }
}
