﻿#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using static CraftData;
#else
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
#endif
using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class CargoBox01b : DecorationItem
    {
        private GameObject CargoCrateContainer = null;

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public CargoBox01b() : base("CargoBox01b", "CargoBox1bName", "CargoBox1bDescription", "cargobox01bicon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public CargoBox01b()
        {
            this.ClassID = "CargoBox01b";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("cargobox01b");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("CargoBox1bName"),
                                                        LanguageHelper.GetFriendlyWord("CargoBox1bDescription"),
                                                        true);
#endif

            this.IsHabitatBuilder = true;

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 2)
                    }),
            };
        }

        private static GameObject _cargoBox1b = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_cargoBox1b == null)
                    _cargoBox1b = AssetsHelper.Assets.LoadAsset<GameObject>("cargobox01b");

                this.CargoCrateContainer = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Locker.prefab");

                GameObject model = _cargoBox1b.FindChild("cargobox01b");
                
                // Set tech tag
                var techTag = _cargoBox1b.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _cargoBox1b.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;

                // Add collider
                var collider = _cargoBox1b.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.132f, 0.288f, 0.16f);
                collider.center = new Vector3(collider.center.x, collider.center.y - 0.01f, collider.center.z - 0.04f);

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Starship_cargo_normal");
                var renderers = _cargoBox1b.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers)
                {
                    foreach (Material tmpMat in rend.materials)
                    {
                        tmpMat.shader = marmosetUber;
                        if (string.Compare(tmpMat.name, "Starship_cargo (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal);
                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                    }
                }

                // Add large world entity
                _cargoBox1b.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                // Add contructable
                var constructible = _cargoBox1b.AddComponent<Constructable>();
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = true;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = true;
                constructible.controlModelState = true;
                constructible.deconstructionAllowed = true;
                constructible.rotationEnabled = true;
                constructible.model = model;
                constructible.techType = this.TechType;
                constructible.placeMinDistance = 0.6f;
                constructible.enabled = true;

                // Add constructable bounds
                var bounds = _cargoBox1b.AddComponent<ConstructableBounds>();

                // Add model controler
                var cargoBoxController = _cargoBox1b.AddComponent<CargoBoxController>();

                // Add new TechType to the buildables
                CraftDataHandler.AddBuildable(this.TechType);
                CraftDataHandler.AddToGroup(TechGroup.ExteriorModules, TechCategory.ExteriorModule, this.TechType);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("cargobox01bicon"));
#endif

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_cargoBox1b);
            GameObject container = GameObject.Instantiate(this.CargoCrateContainer);

            prefab.name = this.ClassID;

            // Update container renderers
            GameObject cargoCrateModel = container.FindChild("model");
            Renderer[] cargoCrateRenderers = cargoCrateModel.GetComponentsInChildren<Renderer>();
            container.transform.parent = prefab.transform;
            foreach (Renderer rend in cargoCrateRenderers)
            {
                rend.enabled = false;
            }
            container.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            container.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
            container.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            container.SetActive(true);

            // Update colliders
            GameObject builderTrigger = container.FindChild("Builder Trigger");
            GameObject collider = container.FindChild("Collider");
            BoxCollider builderCollider = builderTrigger.GetComponent<BoxCollider>();
            builderCollider.isTrigger = false;
            builderCollider.enabled = false;
            BoxCollider objectCollider = collider.GetComponent<BoxCollider>();
            objectCollider.isTrigger = false;
            objectCollider.enabled = false;

            // Delete constructable bounds
            ConstructableBounds cb = container.GetComponent<ConstructableBounds>();
            GameObject.DestroyImmediate(cb);

            // Update sky applier
            SkyApplier sa = prefab.GetComponent<SkyApplier>();
            if (sa == null)
                sa = prefab.GetComponentInChildren<SkyApplier>();
            if (sa == null)
                sa = prefab.AddComponent<SkyApplier>();
            sa.renderers = prefab.GetComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;

            return prefab;
        }
    }
}
