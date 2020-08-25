using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class SeamothDoll : DecorationItem
    {
        public SeamothDoll() // Feeds abstract class
        {
            this.ClassID = "SeamothDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("seamothpuppet");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SeamothDollName"),
                                                        LanguageHelper.GetFriendlyWord("SeamothDollDescription"),
                                                        true);

            CrafterLogicFixer.SeamothDoll = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[3]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1),
                        new Ingredient(TechType.Silicone, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[3]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Silicone, 1)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                GameObject aquarium = Resources.Load<GameObject>("Submarine/Build/Aquarium");

                // Move model
                GameObject model = this.GameObject.FindChild("Model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.032f, model.transform.localPosition.z);

                // Add prefab identifier
                var prefabId = this.GameObject.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(this.GameObject);

                // Add tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.07f, 0.054f, 0.07f);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.027f, collider.center.z);

                // Get glass material
                Material glass = null;
                Renderer[] aRenderers = aquarium.GetComponentsInChildren<Renderer>(true);
                foreach (Renderer aRenderer in aRenderers)
                {
                    foreach (Material aMaterial in aRenderer.materials)
                    {
                        if (aMaterial.name.StartsWith("Aquarium_glass", StringComparison.OrdinalIgnoreCase))
                        {
                            glass = aMaterial;
                            break;
                        }
                    }
                    if (glass != null)
                        break;
                }

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
                
                var renderers = this.GameObject.GetAllComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.name.StartsWith("Submersible_SeaMoth_glass_geo", true, CultureInfo.InvariantCulture))
                            rend.material = glass;
                        else if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                if (string.Compare(tmpMat.name, "Submersible_SeaMoth_Glass_interior (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.EnableKeyword("MARMO_SIMPLE_GLASS");
                                    tmpMat.EnableKeyword("WBOIT");
                                }
                                else if (string.Compare(tmpMat.name, "Submersible_SeaMoth_Glass (Instance)", true, CultureInfo.InvariantCulture) != 0)
                                {
                                    tmpMat.shader = marmosetUber;
                                    if (string.Compare(tmpMat.name, "power_cell_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal1);
                                        tmpMat.SetTexture("_SpecTex", spec1);
                                        tmpMat.SetTexture("_Illum", illum1);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                    }
                                    else if (string.Compare(tmpMat.name, "Submersible_SeaMoth (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal2);
                                        tmpMat.SetTexture("_SpecTex", spec2);
                                        tmpMat.SetTexture("_Illum", illum2);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                    }
                                    else if (string.Compare(tmpMat.name, "Submersible_SeaMoth_indoor (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal3);
                                        tmpMat.SetTexture("_Illum", illum3);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                    }
                                    else if (string.Compare(tmpMat.name, "seamoth_storage_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal4);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                    }
                                    else if (string.Compare(tmpMat.name, "seamoth_storage_02 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal5);
                                        tmpMat.SetTexture("_Illum", illum5);
                                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                    }
                                    else if (string.Compare(tmpMat.name, "seamoth_power_cell_slot_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal6);
                                        tmpMat.SetTexture("_SpecTex", spec6);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                    }
                                    else if (string.Compare(tmpMat.name, "seamoth_torpedo_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal7);
                                        tmpMat.SetTexture("_SpecTex", spec7);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                    }
                                    else if (string.Compare(tmpMat.name, "seamoth_torpedo_01_hatch_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_BumpMap", normal8);
                                        tmpMat.SetTexture("_SpecTex", spec8);

                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
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
#if BELOWZERO
                constructable.allowedUnderwater = true;
#endif
                constructable.controlModelState = true;
                constructable.deconstructionAllowed = true;
                constructable.rotationEnabled = true;
                constructable.model = model;
                constructable.techType = this.TechType;
                constructable.placeMinDistance = 0.6f;
                constructable.enabled = true;
                
                // Add constructable bounds
                ConstructableBounds bounds = this.GameObject.AddComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.032f, bounds.bounds.position.z);

                // Add sky applier
#if BELOWZERO
                BaseModuleLighting bml = this.GameObject.GetComponent<BaseModuleLighting>();
                if (bml == null)
                    bml = this.GameObject.GetComponentInChildren<BaseModuleLighting>();
                if (bml == null)
                    bml = this.GameObject.AddComponent<BaseModuleLighting>();
#endif
                SkyApplier applier = this.GameObject.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = this.GameObject.GetComponentInChildren<SkyApplier>();
                if (applier == null)
                    applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;
                applier.updaterIndex = 0;
                SkyApplier[] appliers = this.GameObject.GetComponentsInChildren<SkyApplier>();
                if (appliers != null && appliers.Length > 0)
                {
                    foreach (SkyApplier ap in appliers)
                    {
                        ap.renderers = renderers;
                        ap.anchorSky = Skies.Auto;
                        ap.updaterIndex = 0;
                    }
                }

                // Add lights/model controler
                SeamothDollController controller = this.GameObject.AddComponent<SeamothDollController>();

                // Add new TechType to the buildables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.Seamoth));

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;
            prefab.transform.localScale *= 5.0f;

            return prefab;
        }
    }
}
