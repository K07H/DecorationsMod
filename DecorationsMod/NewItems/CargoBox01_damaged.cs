using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class CargoBox01_damaged : DecorationItem
    {
        private GameObject CargoCrateContainer = null;

        [SetsRequiredMembers]
        public CargoBox01_damaged() : base("CargoBox01_damaged", LanguageHelper.GetFriendlyWord("CargoBox1DmgName"), LanguageHelper.GetFriendlyWord("CargoBox1DmgDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("cargobox01damagedicon"))
        {
            this.ClassID = "CargoBox01_damaged";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("cargobox01_damaged");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _cargoBox1Damaged = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_cargoBox1Damaged == null)
                    _cargoBox1Damaged = AssetsHelper.Assets.LoadAsset<GameObject>("cargobox01_damaged");

                this.CargoCrateContainer = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Locker.prefab");

                GameObject model = _cargoBox1Damaged.FindChild("cargobox01_damaged");
                
                // Set tech tag
                var techTag = _cargoBox1Damaged.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _cargoBox1Damaged.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;

                // Add collider
                var collider = _cargoBox1Damaged.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.132f, 0.288f, 0.16f);
                collider.center = new Vector3(collider.center.x, collider.center.y - 0.01f, collider.center.z - 0.04f);
                
                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Starship_cargo_normal");
                var renderers = _cargoBox1Damaged.GetComponentsInChildren<Renderer>();
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
                _cargoBox1Damaged.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                // Add contructable
                var constructible = _cargoBox1Damaged.AddComponent<Constructable>();
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
                var bounds = _cargoBox1Damaged.AddComponent<ConstructableBounds>();

                // Add model controler
                var cargoBoxController = _cargoBox1Damaged.AddComponent<CargoBoxController>();

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.ExteriorModules, TechCategory.ExteriorModule, this.TechType);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("cargobox01damagedicon"));

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_cargoBox1Damaged);
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
            PrefabsHelper.UpdateOrAddSkyApplier(prefab);

            return prefab;
        }
    }
}
