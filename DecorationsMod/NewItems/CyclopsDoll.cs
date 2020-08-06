using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class CyclopsDoll : DecorationItem
    {
        public CyclopsDoll() // Feeds abstract class
        {
            this.ClassID = "CyclopsDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("CyclopsDollBase");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("CyclopsDollName"),
                                                        LanguageHelper.GetFriendlyWord("CyclopsDollDescription"),
                                                        true);

            CrafterLogicFixer.CyclopsDoll = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
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

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                GameObject model = this.GameObject.FindChild("CyclopsDoll");

                // Scale model
                model.transform.localScale *= 0.12f;

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.135f, model.transform.localPosition.z);

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
                collider.size = new Vector3(0.06f, 0.148f, 0.42f);
                collider.center = new Vector3(collider.center.x - 0.02f, collider.center.y + 0.135f, collider.center.z - 0.105f);

                // Get shaders/textures
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                if (normals == null)
                {
                    normals = new Dictionary<string, Texture>();
                    foreach (KeyValuePair<string, string> elem in normalnames)
                        normals.Add(elem.Key, AssetsHelper.Assets.LoadAsset<Texture>(elem.Value));
                }

                // Get glass material
                GameObject aquarium = Resources.Load<GameObject>("Submarine/Build/Aquarium");
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

                // Set proper shaders/textures
                var renderers = this.GameObject.GetAllComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
#if DEBUG_CYCLOPS_DOLL
                    Logger.Log("DEBUG: Printing renderers:");
#endif
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.name.StartsWith("Cyclops_submarine_exterior_glass", true, CultureInfo.InvariantCulture) ||
                            rend.name.StartsWith("glass", true, CultureInfo.InvariantCulture))
                            rend.material = glass;
                        else if (rend.materials.Length > 0)
                        {
#if DEBUG_CYCLOPS_DOLL
                            Logger.Log("DEBUG: Found renderer name=[" + rend.name + "] type=[" + rend.GetType().ToString() + "]");
#endif
                            foreach (Material tmpMat in rend.materials)
                            {
#if DEBUG_CYCLOPS_DOLL
                                Logger.Log("DEBUG: \t=> material name=[" + tmpMat.name + "]");
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
                                                Logger.Log("DEBUG: Warning missing cyclops texture.");
#endif
                                            break;
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
                constructable.surfaceType = VFXSurfaceTypes.metal;
                constructable.enabled = true;

                // Add constructable bounds
                ConstructableBounds bounds = this.GameObject.AddComponent<ConstructableBounds>();
                bounds.bounds.size *= 0.9f;
                Vector3 pos = bounds.bounds.position;
                bounds.bounds.position = new Vector3(pos.x - 0.02f, pos.y + 0.135f, pos.z - 0.105f);

                // Add sky applier
#if BELOWZERO
                BaseModuleLighting bml = this.GameObject.GetComponent<BaseModuleLighting>();
                if (bml == null)
                    bml = this.GameObject.GetComponentInChildren<BaseModuleLighting>();
                if (bml == null)
                    bml = this.GameObject.AddComponent<BaseModuleLighting>();
#endif
                SkyApplier applier = this.GameObject.AddComponent<SkyApplier>();
                if (applier != null)
                {
                    applier.renderers = renderers;
                    applier.anchorSky = Skies.Auto;
                    applier.updaterIndex = 0;
                }

                // Add size controler
                CyclopsDollController controller = this.GameObject.AddComponent<CyclopsDollController>();

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.Cyclops));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            prefab.name = this.ClassID;
            return prefab;
        }
    }
}
