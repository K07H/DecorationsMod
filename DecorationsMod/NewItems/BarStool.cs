﻿using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    class BarStool : DecorationItem
    {
        public GameObject barstoolgo = null;

        private Texture metal_normal = null;
        private Texture metal_spec = null;

        [SetsRequiredMembers]
        public BarStool() : base("BarStool", LanguageHelper.GetFriendlyWord("BarStoolName"), LanguageHelper.GetFriendlyWord("BarStoolDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("bar_stool"))
        {
            // Feed DecortionItem interface
            this.ClassID = "BarStool";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            this.barstoolgo = AssetsHelper.Assets.LoadAsset<GameObject>("bar_stool");

            CrafterLogicFixer.Stool = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                metal_spec = AssetsHelper.Assets.LoadAsset<Texture>("Stool_Metal_MetallicSmoothness");
                metal_normal = AssetsHelper.Assets.LoadAsset<Texture>("Stool_Metal_Normal");

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType, TechType.StarshipChair3);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("bar_stool"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _starshipChair = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: starshipChair.GetGameObject()");
#endif
            if (_starshipChair == null)
                _starshipChair = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/StarshipChair.prefab");
#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): A");
#endif
            GameObject prefab = GameObject.Instantiate(_starshipChair);
#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): B");
#endif
            GameObject barstoolPrefab = GameObject.Instantiate(this.barstoolgo);

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): C");
#endif
            prefab.name = this.ClassID;

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): D");
#endif
            // Modify tech tag
            TechTag techTag = prefab.GetComponent<TechTag>();
            if (techTag == null)
                techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;
            
            // Modify prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): E");
#endif
            // Scale
            prefab.transform.localScale *= 0.5f;
            foreach (Transform tr in prefab.transform)
            {
                tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y + 0.3f, tr.localPosition.z);
            }

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): F");
#endif
            // Add large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            if (lwe == null)
                lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): G");
#endif
            // Disable renderers
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
            {
                rend.enabled = false;
            }
            barstoolPrefab.transform.parent = prefab.transform;
            barstoolPrefab.transform.localPosition = new Vector3(0.0f, 0.07f, 0.0f);
            barstoolPrefab.transform.localScale = new Vector3(1000.0f, 1000.0f, 1000.0f);
            barstoolPrefab.transform.localEulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
            barstoolPrefab.SetActive(true);

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): H");
#endif
            // Get bench
            var bench = prefab.GetComponent<Bench>();
            bench.cinematicController.animatedTransform.localPosition = new Vector3(bench.cinematicController.animatedTransform.localPosition.x, bench.cinematicController.animatedTransform.localPosition.y + 1.76f, bench.cinematicController.animatedTransform.localPosition.z - 0.1f);

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): I");
#endif
            // Set proper shaders
            renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material tmpMat in renderer.materials)
                {
                    // Associate MarmosetUBER shader
                    tmpMat.shader = Shader.Find("MarmosetUBER");
                    if (tmpMat.name.StartsWith("Stool_Leather", true, CultureInfo.InvariantCulture))
                    {
                        tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                        tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                    }
                    else if (tmpMat.name.StartsWith("Stool_Metal", true, CultureInfo.InvariantCulture))
                    {
                        tmpMat.SetTexture("_SpecTex", metal_spec);
                        tmpMat.SetTexture("_MetallicGlossMap", metal_spec);
                        tmpMat.SetTexture("_BumpMap", metal_normal);

                        tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                        tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                        tmpMat.EnableKeyword("MARMO_MIP_GLOSS");
                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                    }
                }
            }

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): J");
#endif
            // Update sky applier
            PrefabsHelper.UpdateOrAddSkyApplier(prefab, null, renderers);

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): K");
#endif
            // Update contructable
            var constructible = prefab.GetComponent<Constructable>();
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = true;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = false;
            constructible.controlModelState = true;
            constructible.deconstructionAllowed = true;
            constructible.rotationEnabled = true;
            constructible.model = prefab.FindChild("bar_stool(Clone)");
            constructible.techType = this.TechType;
            constructible.placeMinDistance = 0.6f;
            constructible.enabled = true;

#if DEBUG_STOOL
            Logger.Debug("DEBUG: BarStool->GetGameObject(): L");
#endif
            // Update constructable bounds
            //var constructableBounds = prefab.GetComponent<ConstructableBounds>();
            //constructableBounds.bounds = new OrientedBounds(new Vector3(constructableBounds.bounds.position.x, constructableBounds.bounds.position.y, constructableBounds.bounds.position.z),
            //    new Quaternion(constructableBounds.bounds.rotation.x, constructableBounds.bounds.rotation.y, constructableBounds.bounds.rotation.z, constructableBounds.bounds.rotation.w),
            //    new Vector3(constructableBounds.bounds.extents.x * 0.3f, constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.z * 0.3f));

            return prefab;
        }
    }
}
