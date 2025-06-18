using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class WallMonitor2 : DecorationItem
    {
        public GameObject SignObject = null;

        private Material screenMaterial = null;

        [SetsRequiredMembers]
        public WallMonitor2() : base("WallMonitor2", LanguageHelper.GetFriendlyWord("WallMonitor2Name"), LanguageHelper.GetFriendlyWord("WallMonitor2Description"), AssetsHelper.Assets.LoadAsset<Sprite>("computer2")) // Feeds abstract class
        {
            this.ClassID = "WallMonitor2"; //6a5c9533-75e5-47c6-a16e-0f5f71e14f4f
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.CopperWire, 1),
                new Ingredient(TechType.Glass, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.CopperWire, 1),
                new Ingredient(TechType.Glass, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                screenMaterial = AssetsHelper.Assets.LoadAsset<Material>("new_wall_monitor_screen_material");
                screenMaterial.shader = Shader.Find("MarmosetUBER");

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add the new TechType to the hand-equipments
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("computer2"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _wallMonitor2 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: wallMonitor2.GetGameObject()");
#endif
            if (_wallMonitor2 == null)
#if SUBNAUTICA
                _wallMonitor2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/wall_monitor_01_02.prefab");
#else
                _wallMonitor2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/alterra/base/wall_monitor_01_02.prefab");
#endif
            if (this.SignObject == null)
                this.SignObject = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Sign.prefab");

            GameObject prefab = GameObject.Instantiate(_wallMonitor2);
            GameObject signPrefab = GameObject.Instantiate(this.SignObject);
            GameObject cube = prefab.FindChild("Cube");

            prefab.name = this.ClassID;

            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            if (techTag == null)
                if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                    techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            if (prefabId == null)
                if ((prefabId = prefab.GetComponentInChildren<PrefabIdentifier>()) == null)
                    prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Remove rigid body to prevent bugs
            var rb = prefab.GetComponent<Rigidbody>();
            GameObject.DestroyImmediate(rb);

            // Get box collider
            var collider = cube.GetComponent<BoxCollider>();

            // We can pick this item
            var pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            prefab.AddComponent<CustomPlaceToolController>();
            var placeTool = prefab.AddComponent<GenericPlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = true;
            placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
            placeTool.rotationEnabled = true;
            placeTool.enabled = true;
            placeTool.hasAnimations = false;
            placeTool.hasBashAnimation = false;
            placeTool.hasFirstUseAnimation = false;
            placeTool.mainCollider = collider;
            placeTool.pickupable = pickupable;

            // Add editable label
            signPrefab.transform.parent = prefab.transform;
            signPrefab.transform.localPosition = new Vector3(0f, 0.25f, 0.096f);
            signPrefab.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            signPrefab.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            signPrefab.SetActive(true);
            var ssf = prefab.AddComponent<SignSetupFixer>();

            // Hide "no signal" material
            MeshRenderer[] renderers = prefab.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in renderers)
            {
                if (string.Compare(rend.name, "Starship_wall_monitor_01_02", true, CultureInfo.InvariantCulture) == 0)
                {
                    Material[] tmp = rend.materials;
                    if (tmp.Length >= 2)
                    {
                        if (string.Compare(tmp[0].name, "Starship_wall_monitor_01_screen (Instance)", true, CultureInfo.InvariantCulture) == 0)
                            rend.materials = new Material[] { screenMaterial, tmp[1] };
                        else if (string.Compare(tmp[1].name, "Starship_wall_monitor_01_screen (Instance)", true, CultureInfo.InvariantCulture) == 0)
                            rend.materials = new Material[] { tmp[0], screenMaterial };
                    }
                }
            }

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.9f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;
            
            return prefab;
        }
    }
}
