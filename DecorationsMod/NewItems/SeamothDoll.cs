using DecorationsMod.Controllers;
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
                // Move model
                GameObject model = this.GameObject.FindChild("Model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.032f, model.transform.localPosition.z);

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
                collider.size = new Vector3(0.07f, 0.054f, 0.07f);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.027f, collider.center.z);

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("power_cell_01_normal");
                Texture spec1 = AssetsHelper.Assets.LoadAsset<Texture>("power_cell_01_spec");
                Texture illum1 = AssetsHelper.Assets.LoadAsset<Texture>("power_cell_01_illum");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Submersible_SeaMoth_normal");
                Texture spec2 = AssetsHelper.Assets.LoadAsset<Texture>("Submersible_SeaMoth_spec");
                Texture illum2 = AssetsHelper.Assets.LoadAsset<Texture>("Submersible_SeaMoth_illum");
                Texture normal3 = AssetsHelper.Assets.LoadAsset<Texture>("Submersible_SeaMoth_indoor_normal");
                Texture illum3 = AssetsHelper.Assets.LoadAsset<Texture>("Submersible_SeaMoth_indoor_illum");
                Texture normal4 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_storage_01_normal");
                Texture normal5 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_storage_02_normal");
                Texture illum5 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_storage_02_illum");
                Texture normal6 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_power_cell_slot_01_normal");
                Texture spec6 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_power_cell_slot_01_spec");
                Texture normal7 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_torpedo_01_normal");
                Texture spec7 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_torpedo_01_spec");
                Texture normal8 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_torpedo_01_hatch_01_normal");
                Texture spec8 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_torpedo_01_hatch_01_spec");
                
                var renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                if (tmpMat.name.CompareTo("Submersible_SeaMoth_Glass (Instance)") != 0 && tmpMat.name.CompareTo("Submersible_SeaMoth_Glass_interior (Instance)") != 0)
                                {
                                    tmpMat.shader = marmosetUber;
                                    if (tmpMat.name.CompareTo("power_cell_01 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal1);
                                        tmpMat.SetTexture("_SpecTex", spec1);
                                        tmpMat.SetTexture("_Illum", illum1);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                    else if (tmpMat.name.CompareTo("Submersible_SeaMoth (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal2);
                                        tmpMat.SetTexture("_SpecTex", spec2);
                                        tmpMat.SetTexture("_Illum", illum2);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                    else if (tmpMat.name.CompareTo("Submersible_SeaMoth_indoor (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal3);
                                        tmpMat.SetTexture("_Illum", illum3);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                    else if (tmpMat.name.CompareTo("seamoth_storage_01 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal4);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    }
                                    else if (tmpMat.name.CompareTo("seamoth_storage_02 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal5);
                                        tmpMat.SetTexture("_Illum", illum5);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                    }
                                    else if (tmpMat.name.CompareTo("seamoth_power_cell_slot_01 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal6);
                                        tmpMat.SetTexture("_SpecTex", spec6);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    }
                                    else if (tmpMat.name.CompareTo("seamoth_torpedo_01 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal7);
                                        tmpMat.SetTexture("_SpecTex", spec7);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    }
                                    else if (tmpMat.name.CompareTo("seamoth_torpedo_01_hatch_01 (Instance)") == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal8);
                                        tmpMat.SetTexture("_SpecTex", spec8);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    }
                                }
                            }
                        }
                    }
                }

                // Add contructable
                Constructable constructable = this.GameObject.AddComponent<Constructable>();
                constructable.allowedInBase = true;
                constructable.allowedInSub = true;
                constructable.allowedOutside = true;
                constructable.allowedOnCeiling = false;
                constructable.allowedOnGround = true;
                constructable.allowedOnConstructables = true;
                constructable.controlModelState = true;
                constructable.deconstructionAllowed = true;
                constructable.rotationEnabled = true;
                constructable.model = model;
                constructable.techType = this.TechType;
                constructable.enabled = true;
                
                // Add constructable bounds
                ConstructableBounds bounds = this.GameObject.AddComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.032f, bounds.bounds.position.z);

                // Add sky applier
                SkyApplier applier = this.GameObject.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                applier.anchorSky = Skies.Auto;

                // Add lights/model controler
                SeamothDollController controller = this.GameObject.AddComponent<SeamothDollController>();

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

            prefab.name = this.ClassID;
            prefab.transform.localScale *= 5.0f;

            return prefab;
        }
    }
}
