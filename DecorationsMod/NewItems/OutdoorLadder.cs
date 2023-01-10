using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class OutdoorLadder : DecorationItem
    {
        public OutdoorLadder() // Feeds abstract class
        {
            this.ClassID = "OutdoorLadder";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("OutdoorLadder");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("OutdoorLadderName"),
                                                        LanguageHelper.GetFriendlyWord("OutdoorLadderDescription"),
                                                        true);

            CrafterLogicFixer.OutdoorLadder = this.TechType;

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 2)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 2)
                    }),
            };
#endif
        }

        private static GameObject _outdoorLadder = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_outdoorLadder == null)
                    _outdoorLadder = AssetsHelper.Assets.LoadAsset<GameObject>("OutdoorLadder");

                // Get model
                GameObject model = _outdoorLadder.FindChild("OutdoorLadderModel");

                // Scale model
                model.transform.localScale *= 100.0f;

                // Add prefab identifier
                var prefabId = _outdoorLadder.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(_outdoorLadder, LargeWorldEntity.CellLevel.Far);

                // Add tech tag
                var techTag = _outdoorLadder.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add collider
                var collider = _outdoorLadder.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.3f, 2.7f, 0.07f);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.55f, collider.center.z - 0.24f);

                // Set proper shaders and materials
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture illum1 = AssetsHelper.Assets.LoadAsset<Texture>("base_ladder_01");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("base_ladder_01_normal");
                var renderers = _outdoorLadder.GetComponentsInChildren<Renderer>();
                if (renderers != null && renderers.Length > 0)
                    foreach (Renderer rend in renderers)
                        foreach (Material tmpMat in rend.materials)
                        {
                            tmpMat.shader = marmosetUber;
                            if (tmpMat.name.StartsWith("base_ladder_01"))
                            {
                                tmpMat.SetTexture("_BumpMap", normal1);
#if SUBNAUTICA
                                tmpMat.SetTexture("_Illum", illum1);
                                tmpMat.SetFloat("_EmissionLM", 0.05f);
                                tmpMat.EnableKeyword("MARMO_EMISSION");
#endif
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                        }

                // Add contructable
                Constructable constructable = _outdoorLadder.AddComponent<Constructable>();
                constructable.allowedInBase = false;
                constructable.allowedInSub = false;
                constructable.allowedOutside = true;
                constructable.allowedOnCeiling = true;
                constructable.allowedOnWall = true;
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
                constructable.placeMaxDistance = 10.0f;
                constructable.enabled = true;

                // Add constructable bounds
                ConstructableBounds bounds = _outdoorLadder.AddComponent<ConstructableBounds>();

                // Add sky applier
                BaseModuleLighting bml = _outdoorLadder.GetComponent<BaseModuleLighting>();
                if (bml == null)
                    bml = _outdoorLadder.GetComponentInChildren<BaseModuleLighting>();
                if (bml == null)
                    bml = _outdoorLadder.AddComponent<BaseModuleLighting>();
                SkyApplier applier = _outdoorLadder.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = _outdoorLadder.GetComponentInChildren<SkyApplier>();
                if (applier == null)
                    applier = _outdoorLadder.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;
                applier.updaterIndex = 0;
#if SUBNAUTICA
                applier.emissiveFromPower = true; // Emissive from power
#endif
                applier.enabled = true;

                // Add outdoor ladder controller
                OutdoorLadderController controller = _outdoorLadder.AddComponent<OutdoorLadderController>();

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.ExteriorModules, TechCategory.ExteriorModule, this.TechType);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.BaseLadder));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_outdoorLadder);
            prefab.name = this.ClassID;
            return prefab;
        }
    }
}
