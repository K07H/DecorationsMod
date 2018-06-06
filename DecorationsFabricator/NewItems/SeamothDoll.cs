using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class SeamothDoll : DecorationItem
    {
        public SeamothDoll() // Feeds abstract class
        {
            this.ClassID = "SeamothDoll";
            this.ResourcePath = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("seamothpuppet");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SeamothDollName"),
                                                        LanguageHelper.GetFriendlyWord("SeamothDollDescription"),
                                                        true);

            this.IsHabitatBuilder = true;

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.Glass, 1),
                        new IngredientHelper(TechType.Silicone, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Scale model
                GameObject model = this.GameObject.FindChild("Model");
                model.transform.localScale *= 5f;

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.2f, model.transform.localPosition.z);
                
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

                // Add collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.4f, 0.3f, 0.5f);
                
                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                var renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                if (rend.material.name.CompareTo("Submersible_SeaMoth_Glass (Instance)") != 0 && rend.material.name.CompareTo("Submersible_SeaMoth_Glass_interior (Instance)") != 0)
                                {
                                    tmpMat.shader = marmosetUber;
                                    if (tmpMat.name.CompareTo("power_cell_01 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("power_cell_01_illum"));
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                    if (tmpMat.name.CompareTo("Submersible_SeaMoth (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("Submersible_SeaMoth_illum"));
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                    if (tmpMat.name.CompareTo("Submersible_SeaMoth_indoor (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("Submersible_SeaMoth_indoor_illum"));
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                    if (tmpMat.name.CompareTo("seamoth_storage_02 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_Illum", AssetsHelper.Assets.LoadAsset<Texture>("seamoth_storage_02_illum"));
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                }
                            }
                        }
                    }
                }

                // Add sky applier
                var applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;

                // Add contructable
                var constructible = this.GameObject.AddComponent<Constructable>();
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = false;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = true;
                constructible.controlModelState = true;
                constructible.deconstructionAllowed = true;
                constructible.rotationEnabled = true;
                constructible.model = model;
                constructible.techType = this.TechType;
                constructible.enabled = true;

                // Add constructable bounds
                var bounds = this.GameObject.AddComponent<ConstructableBounds>();
                
                // Add lights/model controler
                var seamothDollControler = this.GameObject.AddComponent<SeamothDollControler>();

                // Add new TechType to the buildables
                CraftDataPatcher.customBuildables.Add(this.TechType);
                CraftDataPatcher.AddToCustomGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, SpriteManager.Get(TechType.Seamoth)));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.8f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
