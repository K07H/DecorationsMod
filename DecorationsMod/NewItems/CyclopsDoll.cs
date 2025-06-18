using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class CyclopsDoll : DecorationItem
    {
        [SetsRequiredMembers]
        public CyclopsDoll() : base("CyclopsDoll", LanguageHelper.GetFriendlyWord("CyclopsDollName"), LanguageHelper.GetFriendlyWord("CyclopsDollDescription"), SpriteManager.Get(TechType.Cyclops)) // Feeds abstract class
        {
            this.ClassID = "CyclopsDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("CyclopsDollBase");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            this.IsHabitatBuilder = true;

            CrafterLogicFixer.CyclopsDoll = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static readonly Dictionary<string, string> normalnames = new Dictionary<string, string>()
        {
            { "submarine_launch_bay_01_01_outer_100", "submarine_launch_bay_01_01_normal_265" },
            { "Cyclops_exterior_submarine_details_18", "Cyclops_exterior_submarine_details_normal_247" },
            { "submarine_outer_hatch_01_143", "submarine_outer_hatch_01_normal_228" },
            { "submarine_steering_console_02_145", "submarine_steering_console_02_normal_3659" },
            { "submarine_wallmods_01_01_146", "submarine_wallmods_01_01_normal_3814" },
            { "cyclops_submarine_wallmods_01_26", "cyclops_submarine_wallmods_01_normal_3719" },
            { "Cyclops_exterior_submarine_engine_02_20", "Cyclops_exterior_submarine_engine_02_normal_6938" },
            { "Cyclops_exterior_submarine_engine_19", "Cyclops_exterior_submarine_engine_normal_7207" },
            { "cyclops_cabin_23", "cyclops_cabin_normal_204" },
            { "submarine_control_room_steering_console_base_02_48", "submarine_steering_console_base_02_normal_4313" }
        };

        private static Dictionary<string, Texture> normals = null;
        private static GameObject _cyclopsDollAsset = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_cyclopsDollAsset == null)
                {
#if DEBUG_CYCLOPS_DOLL
                    Logger.Debug("DEBUG: CyclopsDoll Loading asset. Asset bundle is " + (AssetsHelper.Assets == null ? "NULL" : "NOT NULL"));
#endif
                    _cyclopsDollAsset = AssetsHelper.Assets.LoadAsset<GameObject>("CyclopsDollBase");
#if DEBUG_CYCLOPS_DOLL
                    Logger.Debug("DEBUG: CyclopsDoll Loaded asset. Result is " + (_cyclopsDollAsset == null ? "NULL" : "NOT NULL"));
#endif
                }

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T1");
#endif
                GameObject model = _cyclopsDollAsset.FindChild("CyclopsDoll");

                // Scale model
                model.transform.localScale *= 0.12f;

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.135f, model.transform.localPosition.z);

                // Add prefab identifier
                var prefabId = _cyclopsDollAsset.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(_cyclopsDollAsset);

                // Add tech tag
                var techTag = _cyclopsDollAsset.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add collider
                var collider = _cyclopsDollAsset.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.06f, 0.148f, 0.42f);
                collider.center = new Vector3(collider.center.x - 0.02f, collider.center.y + 0.135f, collider.center.z - 0.105f);

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T2");
#endif
                // Get shaders/textures
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                if (normals == null)
                {
                    normals = new Dictionary<string, Texture>();
                    foreach (KeyValuePair<string, string> elem in normalnames)
                        normals.Add(elem.Key, AssetsHelper.Assets.LoadAsset<Texture>(elem.Value));
                }

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T3");
#endif
                // Get glass material
                GameObject aquarium = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Aquarium.prefab");

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

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T4 : _cyclopsDollAsset is " + (_cyclopsDollAsset == null ? "NULL" : "NOT NULL"));
#endif
                // Set proper shaders/textures
                Renderer[] renderers = _cyclopsDollAsset.GetComponentsInChildren<Renderer>(true);
#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Regsitration T4b : Renderers Cnt=" + Convert.ToString(renderers.Length));
#endif
                if (renderers.Length > 0)
                {
#if DEBUG_CYCLOPS_DOLL
                    Logger.Debug("DEBUG: Printing renderers:");
#endif
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.name.StartsWith("Cyclops_submarine_exterior_glass", true, CultureInfo.InvariantCulture) ||
                            rend.name.StartsWith("glass", true, CultureInfo.InvariantCulture))
                            rend.material = glass;
                        else if (rend.materials.Length > 0)
                        {
#if DEBUG_CYCLOPS_DOLL
                            Logger.Debug("DEBUG: Found renderer name=[" + rend.name + "] type=[" + rend.GetType().ToString() + "]");
#endif
                            foreach (Material tmpMat in rend.materials)
                            {
#if DEBUG_CYCLOPS_DOLL
                                Logger.Debug("DEBUG: \t=> material name=[" + tmpMat.name + "]");
#endif
                                tmpMat.shader = marmosetUber;
                                if (tmpMat.name.StartsWith("cyclops_submarine_exterior_decals_01_24", false, CultureInfo.InvariantCulture))
                                {
                                    tmpMat.SetFloat("_EnableCutOff", 1.0f);
                                    tmpMat.SetFloat("_Cutoff", 0.1f);
                                    tmpMat.EnableKeyword("MARMO_ALPHA_CLIP");
                                }
                                else if (normals != null)
                                {
                                    foreach (KeyValuePair<string, string> elem in normalnames)
                                        if (tmpMat.name.StartsWith(elem.Key, false, CultureInfo.InvariantCulture))
                                        {
                                            if (elem.Value != null && normals.ContainsKey(elem.Value) && normals[elem.Value] != null)
                                            {
                                                tmpMat.SetTexture("_BumpMap", normals[elem.Value]);
                                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                                tmpMat.EnableKeyword("_ZWRITE_ON");
                                            }
#if DEBUG_CYCLOPS_DOLL
                                            else
                                                Logger.Debug("DEBUG: Warning missing cyclops texture.");
#endif
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T5");
#endif
                // Add contructable
                Constructable constructable = _cyclopsDollAsset.AddComponent<Constructable>();
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
                constructable.surfaceType = VFXSurfaceTypes.metal;
                constructable.placeMinDistance = 0.6f;
                constructable.enabled = true;

                // Add constructable bounds
                ConstructableBounds bounds = _cyclopsDollAsset.AddComponent<ConstructableBounds>();
                bounds.bounds.size *= 0.9f;
                Vector3 pos = bounds.bounds.position;
                bounds.bounds.position = new Vector3(pos.x - 0.02f, pos.y + 0.135f, pos.z - 0.105f);

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T6");
#endif
                // Add sky applier
                BaseModuleLighting bml = _cyclopsDollAsset.GetComponent<BaseModuleLighting>();
                if (bml == null)
                    bml = _cyclopsDollAsset.GetComponentInChildren<BaseModuleLighting>();
                if (bml == null)
                    bml = _cyclopsDollAsset.EnsureComponent<BaseModuleLighting>();
                PrefabsHelper.UpdateOrAddSkyApplier(_cyclopsDollAsset, null, renderers);

                // Add size controler
                CyclopsDollController controller = _cyclopsDollAsset.AddComponent<CyclopsDollController>();

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T7");
#endif
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.Cyclops));

#if DEBUG_CYCLOPS_DOLL
                Logger.Debug("DEBUG: CyclopsDoll Registration T8");
#endif
                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_cyclopsDollAsset);
            prefab.name = this.ClassID;
            return prefab;
        }
    }
}
