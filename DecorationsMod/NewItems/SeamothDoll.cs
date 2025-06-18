using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class SeamothDoll : DecorationItem
    {
        [SetsRequiredMembers]
        public SeamothDoll() : base("SeamothDoll", LanguageHelper.GetFriendlyWord("SeamothDollName"), LanguageHelper.GetFriendlyWord("SeamothDollDescription"), SpriteManager.Get(TechType.Seamoth)) // Feeds abstract class
        {
            this.ClassID = "SeamothDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("seamothpuppet");
            this.GameObject = new GameObject("SeamothDoll");

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.SeamothDoll = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _seamothDoll = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_seamothDoll == null)
                    _seamothDoll = AssetsHelper.Assets.LoadAsset<GameObject>("seamothpuppet");

                GameObject aquarium = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Aquarium.prefab");

                // Move model
                GameObject model = _seamothDoll.FindChild("Model");
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.032f, model.transform.localPosition.z);

                // Add prefab identifier
                var prefabId = _seamothDoll.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(_seamothDoll);

                // Add tech tag
                var techTag = _seamothDoll.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add collider
                var collider = _seamothDoll.AddComponent<BoxCollider>();
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
                
                var renderers = _seamothDoll.GetAllComponentsInChildren<Renderer>();
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
                Constructable constructable = _seamothDoll.AddComponent<Constructable>();
                constructable.allowedInBase = true;
                constructable.allowedInSub = true;
                constructable.allowedOutside = true;
                constructable.allowedOnCeiling = false;
                constructable.allowedOnGround = true;
                constructable.allowedOnConstructables = true;
                constructable.allowedUnderwater = true;
                constructable.controlModelState = true;
                constructable.deconstructionAllowed = true;
                constructable.rotationEnabled = true;
                constructable.model = model;
                constructable.techType = this.TechType;
                constructable.placeMinDistance = 0.6f;
                constructable.enabled = true;
                
                // Add constructable bounds
                ConstructableBounds bounds = _seamothDoll.AddComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.032f, bounds.bounds.position.z);

                // Add sky applier
                BaseModuleLighting bml = _seamothDoll.GetComponent<BaseModuleLighting>();
                if (bml == null)
                    bml = _seamothDoll.GetComponentInChildren<BaseModuleLighting>();
                if (bml == null)
                    bml = _seamothDoll.EnsureComponent<BaseModuleLighting>();

                PrefabsHelper.UpdateOrAddSkyApplier(_seamothDoll, null, renderers);
                SkyApplier[] appliers = _seamothDoll.GetComponentsInChildren<SkyApplier>();
                if (appliers != null && appliers.Length > 0)
                    foreach (SkyApplier ap in appliers)
                    {
                        ap.renderers = renderers;
                        ap.anchorSky = Skies.Auto;
                    }

                // Add lights/model controler
                SeamothDollController controller = _seamothDoll.AddComponent<SeamothDollController>();

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.Seamoth));

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_seamothDoll);

            prefab.name = this.ClassID;
            prefab.transform.localScale *= 5.0f;

            return prefab;
        }
    }
}
