using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class ReactorLamp : DecorationItem
    {
        public ReactorLamp() // Feeds abstract class
        {
            this.ClassID = "ReactorLamp";
            this.ResourcePath = $"{DecorationItem.DefaultResourcePath}{this.ClassID}";

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("nuclearreactorrod_yellow");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("ReactorLampName"),
                                                        LanguageHelper.GetFriendlyWord("ReactorLampDescription"),
                                                        true);

            this.IsHabitatBuilder = true;

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[4]
                    {
                        new IngredientHelper(TechType.PrecursorIonCrystal, 1),
                        new IngredientHelper(TechType.Lead, 1),
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.Glass, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Retrieve model node
                GameObject model = this.GameObject.FindChild("model");

                // Scale model
                model.transform.localScale *= 1.5f;

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.1f, model.transform.localPosition.z);

                // Add rigid body
                /*
                var rb = this.GameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.detectCollisions = false;
                rb.useGravity = true;
                rb.mass = 10;
                */

                // Add prefab identifier
                var prefabId = this.GameObject.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                var lwe = this.GameObject.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Add tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add box collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.2f, 0.35f, 0.2f);

                // Set proper shaders (for crafting animation)
                Shader shader = Shader.Find("MarmosetUBER");
                Texture illumTexture = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_yellow");

                List<Renderer> renderers = new List<Renderer>();
                this.GameObject.GetComponentsInChildren<Renderer>(renderers);
                foreach (Renderer renderer in renderers)
                {
                    if (renderer.name.CompareTo("nuclear_reactor_rod_mesh") == 0)
                    {
                        // Associate MarmosetUBER shader
                        renderer.sharedMaterial.shader = shader;

                        // Update the emission map
                        renderer.sharedMaterial.SetTexture("_Illum", illumTexture);

                        // Increase emission map strength
                        renderer.sharedMaterial.SetColor("_GlowColor", new Color(2.5f, 2.5f, 2.5f, 2.5f));
                        renderer.sharedMaterial.SetFloat("_GlowStrength", 1.5f);

                        // Enable emission
                        renderer.sharedMaterial.EnableKeyword("MARMO_EMISSION");
                    }
                }

                // Add sky applier
                var skyapplier = this.GameObject.AddComponent<SkyApplier>();
                skyapplier.renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                skyapplier.anchorSky = Skies.Auto;

                // Add world forces
                //var forces = this.GameObject.AddComponent<WorldForces>();
                //forces.useRigidbody = rb;
                
                // Add contructable
                var constructible = this.GameObject.AddComponent<Constructable>();
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = true;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = true;
                constructible.controlModelState = true;
                constructible.deconstructionAllowed = true;
                constructible.rotationEnabled = false;
                constructible.model = model;
                constructible.techType = this.TechType;
                constructible.enabled = true;

                // Add constructable bounds
                var bounds = this.GameObject.AddComponent<ConstructableBounds>();

                // Add lamp brightness controler
                var lampBrightness = this.GameObject.AddComponent<ReactorLampBrightness>();

                // Add new TechType to the buildables
                CraftDataPatcher.customBuildables.Add(this.TechType);
                CraftDataPatcher.AddToCustomGroup(TechGroup.ExteriorModules, TechCategory.ExteriorOther, this.TechType);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));
                
                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("reactorrod_yellow")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            
            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.7f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1.0f;

            return prefab;
        }
    }
}
