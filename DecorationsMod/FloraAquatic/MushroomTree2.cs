﻿#if SUBNAUTICA_NAUTILUS
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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.FloraAquatic
{
    public class MushroomTree2 : DecorationItem, ICustomFlora
    {
        public CustomFlora Config = new CustomFlora();
        CustomFlora ICustomFlora.Config
        {
            get => this.Config;
            set => this.Config = value;
        }

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public MushroomTree2() : base("MushroomTree2", "MushroomTree2Name", "MushroomTreeDescription", "mushroomtree2icon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public MushroomTree2()
        {
            this.ClassID = "MushroomTree2";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("MyMushroomTree1");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("MushroomTree2Name"),
                                                        LanguageHelper.GetFriendlyWord("MushroomTreeDescription"),
                                                        true);
#endif

            CrafterLogicFixer.MushroomTree2 = this.TechType;
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
                    new Ingredient(ConfigSwitcher.FloraRecipiesResource, Convert.ToInt32((float)ConfigSwitcher.FloraRecipiesResourceAmount * 1.4f))
                }),
            };

            this.Config = ConfigSwitcher.config_MushroomTree2;
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

                // Set item occupies 4 slots
                CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Harvest types
                CraftDataHandler.SetHarvestType(this.TechType, HarvestType.DamageAlive);
                CraftDataHandler.SetHarvestOutput(this.TechType, this.TechType);

                // Set item background to normal (both land & water plant)
                CraftDataHandler.SetBackgroundType(this.TechType, CraftData.BackgroundType.PlantWaterSeed);

                // Set item bioreactor charge
                BaseBioReactorHelper.SetBioReactorCharge(this.TechType, this.Config.Charge);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("mushroomtree2icon"));
#endif

                // Set unlock condition
                KnownTechHandler.SetAnalysisTechEntry(TechType.TreeMushroomPiece, new TechType[] { this.TechType });

                this.IsRegistered = true;
            }
        }

        private static GameObject _mushroomTree2 = null;

        public override GameObject GetGameObject()
        {
            if (_mushroomTree2 == null)
                _mushroomTree2 = AssetsHelper.Assets.LoadAsset<GameObject>("MyMushroomTree1");

            GameObject prefab = GameObject.Instantiate(_mushroomTree2);
            prefab.name = this.ClassID;

            // Get sub objects
            GameObject model = prefab.FindChild("MyMushroomTree01");

            // Translate model
            model.transform.localPosition = new Vector3(model.transform.localPosition.x + 0.06f, model.transform.localPosition.y - 0.1f, model.transform.localPosition.z + 0.13f);

            // Scale model
            model.transform.localScale = new Vector3(model.transform.localScale.x * 0.24f, model.transform.localScale.y * 0.24f, model.transform.localScale.z * 0.24f);

            // Set proper shaders
            Shader marmosetUber = Shader.Find("MarmosetUBER");
            Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_ball_clusters_normal");
            Texture spec1 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_ball_clusters_spec");
            Texture illum1 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_ball_clusters_illum");
            Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_barnacle_suckers_normals");
            Texture spec2 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_barnacle_suckers_spec");
            Texture illum2 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_barnacle_suckers_illum");
            Texture normal3 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_small_deco_02_normal");
            Texture spec3 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_small_deco_02_spec");
            Texture illum3 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_small_deco_02_illum");
            Texture normal4 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_tree_mushrooms_01_normal");
            Texture illum4 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_tree_mushrooms_01_illum");
            Texture normal5 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_tree_mushrooms_02_normal");
            Texture spec5 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_tree_mushrooms_02_spec");
            Texture illum5 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_tree_mushrooms_02_illum");
            Texture normal6 = AssetsHelper.Assets.LoadAsset<Texture>("texture4normal2");
            Texture spec6 = AssetsHelper.Assets.LoadAsset<Texture>("texture4");
            Texture normal7 = AssetsHelper.Assets.LoadAsset<Texture>("Spiral_blue_thing_normal");
            Texture illum7 = AssetsHelper.Assets.LoadAsset<Texture>("Spiral_blue_thing");
            Texture normal8 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_small_deco_06_normal");
            var renderers = prefab.GetAllComponentsInChildren<Renderer>();
            List<Renderer> saRenderers = new List<Renderer>();
            if (renderers.Length > 0)
                foreach (Renderer rend in renderers)
                {
                    if (rend.name.StartsWith("trunktest8", true, CultureInfo.InvariantCulture) && rend.GetType() == typeof(MeshRenderer))
                        saRenderers.Add(rend);
                    else if (rend.name.StartsWith("Coral_reef_tree_mushrooms", true, CultureInfo.InvariantCulture) && rend.GetType() == typeof(MeshRenderer))
                        saRenderers.Add(rend);

                    if (rend.materials.Length > 0)
                        foreach (Material tmpMat in rend.materials)
                        {
                            tmpMat.shader = marmosetUber;
                            if (tmpMat.name.StartsWith("Coral_reef_ball_clusters"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetTexture("_BumpMap", normal1);
                                tmpMat.SetTexture("_SpecTex", spec1);
                                tmpMat.SetTexture("_Illum", illum1);
                                tmpMat.SetFloat("_EmissionLM", 0.75f);
                            }
                            else if (tmpMat.name.StartsWith("Coral_reef_barnacle_suckers"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetTexture("_BumpMap", normal2);
                                tmpMat.SetTexture("_SpecTex", spec2);
                                tmpMat.SetTexture("_Illum", illum2);
                                tmpMat.SetFloat("_EmissionLM", 0.75f);
                            }
                            else if (tmpMat.name.StartsWith("Coral_reef_small_deco_02"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetTexture("_BumpMap", normal3);
                                tmpMat.SetTexture("_SpecTex", spec3);
                                tmpMat.SetTexture("_Illum", illum3);
                                tmpMat.SetFloat("_EmissionLM", 0.75f);
                            }
                            else if (tmpMat.name.StartsWith("Coral_reef_tree_mushrooms_01"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetTexture("_BumpMap", normal4);
                                tmpMat.SetTexture("_Illum", illum4);
                                //tmpMat.SetFloat("_EmissionLM", 1.0f);
                            }
                            else if (tmpMat.name.StartsWith("Coral_reef_tree_mushrooms_02"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetTexture("_BumpMap", normal5);
                                tmpMat.SetTexture("_SpecTex", spec5);
                                tmpMat.SetTexture("_Illum", illum5);
                                //tmpMat.SetFloat("_EmissionLM", 1.0f);
                            }
                            else if (tmpMat.name.StartsWith("Mushroom_tree_trunk"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetColor("_SpecColor", new Color(0.8f, 0.8f, 0.8f, 0.8f));
                                tmpMat.SetTexture("_SpecTex", spec6);
                                tmpMat.SetTexture("_BumpMap", normal6);
                                tmpMat.SetFloat("_BumpScale", 0.4f);
                                tmpMat.SetFloat("_Fresnel", 0.0f);
                            }
                            else if (tmpMat.name.StartsWith("Spiral_blue_thing_cluster_01_0Mat"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                tmpMat.EnableKeyword("MARMO_GLOW");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetColor("_SpecColor", new Color(0.8f, 0.8f, 0.8f, 0.8f));
                                tmpMat.SetTexture("_SpecTex", illum7);
                                tmpMat.SetTexture("_BumpMap", normal7);
                                tmpMat.SetTexture("_Illum", illum7);
                                tmpMat.SetFloat("_Shininess", 10.0f);
                                tmpMat.SetFloat("_EmissionLM", 2.5f);
                                tmpMat.SetFloat("_EnableGlow", 1);
                                tmpMat.SetColor("_GlowColor", new Color(0.518f, 1.0f, 1.0f, 1.0f));
                                tmpMat.SetFloat("_GlowStrength", 2.5f);
                                tmpMat.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
                            }
                            else if (tmpMat.name.StartsWith("Coral_reef_small_deco_06"))
                            {
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                tmpMat.SetTexture("_BumpMap", normal8);
                            }
                        }
                }

            PrefabsHelper.AddNewGenericSeed(ref prefab);

            // Update prefab identifier
            PrefabIdentifier prefabId = prefab.GetComponent<PrefabIdentifier>();
            if (prefabId == null)
                prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Add rigid body
            Rigidbody rb = prefab.AddComponent<Rigidbody>();
            rb.mass = 1.0f;
            rb.drag = 1.0f;
            rb.angularDrag = 1.0f;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.None;

            // Add EntityTag
            EntityTag entityTag = prefab.AddComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Large;

            // Add TechTag
            TechTag techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Add large world entity
            LargeWorldEntity lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Add colliders
            SphereCollider colliderSmall = prefab.AddComponent<SphereCollider>();
            BoxCollider collider = prefab.AddComponent<BoxCollider>();
            colliderSmall.radius = 0.1f;
            collider.size = new Vector3(0.00035f, 0.0015f, 0.00035f);
            collider.center = new Vector3(collider.center.x - 0.1f, collider.center.y + 1f, collider.center.z + 0.02f);

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
            plantable.underwater = true;
            plantable.isSeedling = true;
            plantable.plantTechType = this.TechType;
            plantable.size = Plantable.PlantSize.Large;
            plantable.pickupable = pickupable;
            plantable.eatable = eatable;
            plantable.model = prefab;
            plantable.linkedGrownPlant = new GrownPlant();
            plantable.linkedGrownPlant.seed = plantable;
            plantable.linkedGrownPlant.seedUID = "MushroomTree2";

            // Add generic plant controller (handles animation)
            PlantGenericController plantGenericController = prefab.AddComponent<PlantGenericController>();
            plantGenericController.GrowthDuration = Config.GrowthDuration;
            plantGenericController.Health = Config.Health;
            plantGenericController.Knifeable = Config.Knifeable;
            plantGenericController.RestoreBoxColliders = true;

            // Handles saving/restoring cove tree state
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

            // Add sky applier
            PrefabsHelper.SetDefaultSkyApplier(prefab, saRenderers.ToArray(), Skies.Auto, true);

            // Hide plant and show seed
            PrefabsHelper.HidePlantAndShowSeed(prefab.transform, this.ClassID);

            return prefab;
        }
    }
}
