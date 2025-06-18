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
    public class ReactorLamp : DecorationItem
    {
        [SetsRequiredMembers]
        public ReactorLamp() : base("ReactorLamp", LanguageHelper.GetFriendlyWord("ReactorLampName"), LanguageHelper.GetFriendlyWord("ReactorLampDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("reactorrod_white")) // Feeds abstract class
        {
            this.ClassID = "ReactorLamp";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("nuclearreactorrod_white");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.ReactorLamp = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Diamond, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Diamond, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _reactorLamp = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_reactorLamp == null)
                    _reactorLamp = AssetsHelper.Assets.LoadAsset<GameObject>("nuclearreactorrod_white");

                GameObject aquarium = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Aquarium.prefab");

                // Retrieve model node
                GameObject model = _reactorLamp.FindChild("model");

                // Add compatibility with SnapBuilder
                PrefabsHelper.SnapBuilderCompatibility(model.transform, new Vector3(-90f, -90f, 0f));

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y - 0.04f, model.transform.localPosition.z + 0.03f);

                // Disable light at start
                var reactorRodLight = _reactorLamp.GetComponentInChildren<Light>();
                reactorRodLight.intensity = 1.0f;
                reactorRodLight.range = 10.0f;
                reactorRodLight.color = Color.white;
                reactorRodLight.enabled = false;

                // Add prefab identifier
                var prefabId = _reactorLamp.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                var lwe = _reactorLamp.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Add tech tag
                var techTag = _reactorLamp.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add box collider
                var collider = _reactorLamp.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.18f, 0.36f, 0.18f);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.22f, collider.center.z);

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
                Shader shader = Shader.Find("MarmosetUBER");
                Texture normalTexture = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_normal");
                Texture specTexture = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_spec");
                Texture illumTexture = AssetsHelper.Assets.LoadAsset<Texture>("nuclear_reactor_rod_illum_white");

                List<Renderer> renderers = new List<Renderer>();
                _reactorLamp.GetComponentsInChildren<Renderer>(renderers);
                foreach (Renderer renderer in renderers)
                {
                    if (renderer.name == "nuclear_reactor_rod_mesh")
                    {
                        // Associate MarmosetUBER shader
                        renderer.sharedMaterial.shader = shader;
                        renderer.material.shader = shader;

                        // Update normal map
                        renderer.sharedMaterial.SetTexture("_BumpMap", normalTexture);
                        renderer.material.SetTexture("_BumpMap", normalTexture);

                        // Update spec map
                        renderer.sharedMaterial.SetTexture("_SpecTex", specTexture);
                        renderer.material.SetTexture("_SpecTex", specTexture);

                        // Update emission map
                        renderer.sharedMaterial.SetTexture("_Illum", illumTexture);
                        renderer.material.SetTexture("_Illum", illumTexture);

                        // Increase emission map strength
                        renderer.sharedMaterial.SetColor("_GlowColor", Color.white);
                        renderer.sharedMaterial.SetFloat("_GlowStrength", 1.0f);
                        renderer.material.SetColor("_GlowColor", Color.white);
                        renderer.material.SetFloat("_GlowStrength", 1.0f);

                        // Enable emission
                        renderer.sharedMaterial.EnableKeyword("MARMO_NORMALMAP");
                        renderer.sharedMaterial.EnableKeyword("MARMO_EMISSION");
                        renderer.sharedMaterial.EnableKeyword("MARMO_SPECMAP");
                        renderer.sharedMaterial.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        renderer.material.EnableKeyword("MARMO_NORMALMAP");
                        renderer.material.EnableKeyword("MARMO_EMISSION");
                        renderer.material.EnableKeyword("MARMO_SPECMAP");
                        renderer.material.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                    }
                    else if (renderer.name == "nuclear_reactor_rod_glass" && glass != null)
                        renderer.material = glass;
                }

                // Add sky applier
                BaseModuleLighting bml = _reactorLamp.GetComponent<BaseModuleLighting>();
                if (bml == null)
                    bml = _reactorLamp.GetComponentInChildren<BaseModuleLighting>();
                if (bml == null)
                    bml = _reactorLamp.EnsureComponent<BaseModuleLighting>();
                PrefabsHelper.UpdateOrAddSkyApplier(_reactorLamp);

                // Add contructable
                var constructible = _reactorLamp.AddComponent<Lamp_C>();
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = true;
                constructible.allowedOnCeiling = true;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = true;
                constructible.allowedOnWall = true;
                constructible.allowedUnderwater = true;
                constructible.controlModelState = true;
                constructible.deconstructionAllowed = true;
                constructible.rotationEnabled = true;
                constructible.model = model;
                constructible.techType = this.TechType;
                constructible.enabled = true;

                // Add constructable bounds
                var bounds = _reactorLamp.AddComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.003f, bounds.bounds.position.z);

                // Add lamp brightness controler
                var lampBrightness = _reactorLamp.AddComponent<ReactorLampBrightness>();
                
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.ExteriorModules, TechCategory.ExteriorLight, this.TechType, TechType.Spotlight);

                // Set the buildable prefab
                this.Register();
                
                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("reactorrod_white"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_reactorLamp);
            
            prefab.name = this.ClassID;
            // Scale prefab
            prefab.transform.localScale *= 1.5f;

            return prefab;
        }
    }
}
