using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using BepInEx.Bootstrap;
using Nautilus.Utility;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using Nautilus.Handlers;
using Nautilus.Crafting;
using DecorationsMod.Controllers;
using static CraftData;

namespace DecorationsMod
{
    public static class OtherItems
    {
        public static TechType _nutrientBlockTechType = TechType.NutrientBlock;
        public static TechType _toyCarTechType = TechType.ToyCar;
        public static TechType _lightSwitchTechType = TechType.None;
        private static GameObject _lightSwitchPrefab = null;

        #region Nutrient Block

        public static CustomPrefab RegisterNutrientBlock()
        {
            if (!ConfigSwitcher.EnableNutrientBlock)
                return null;
#if DEBUG_ITEMS_REGISTRATION
            Logger.Debug("DEBUG: Configuring NutrientBlock.");
#endif
            var nutrientBlock = new CustomPrefab("DecoNutrientBlock", Language.main.Get("NutrientBlock"), Language.main.Get("Tooltip_NutrientBlock"), SpriteManager.Get(TechType.NutrientBlock));
            _nutrientBlockTechType = nutrientBlock.Info.TechType;
            var nutrientBlockPrefab = new CloneTemplate(nutrientBlock.Info, TechType.NutrientBlock)
            {
                ModifyPrefab = prefab =>
                {
                    // Retrieve collider
                    Collider collider = null;
                    GameObject child = prefab.FindChild("Nutrient_block");
                    if (child != null)
                        collider = child.GetComponent<BoxCollider>();

                    // We can pick this item
                    var pickupable = prefab.GetComponent<Pickupable>();
                    if (pickupable == null)
                        pickupable = prefab.GetComponentInChildren<Pickupable>();
                    if (pickupable == null)
                        pickupable = prefab.AddComponent<Pickupable>();
                    if (pickupable != null)
                    {
                        pickupable.isPickupable = true;
                        pickupable.randomizeRotationWhenDropped = true;
                    }

                    // We can place this item
                    prefab.AddComponent<CustomPlaceToolController>();
                    var placeTool = prefab.AddComponent<NutrientBlock_PT>();
                    placeTool.allowedInBase = true;
                    placeTool.allowedOnBase = false;
                    placeTool.allowedOnCeiling = false;
                    placeTool.allowedOnConstructable = true;
                    placeTool.allowedOnGround = true;
                    placeTool.allowedOnRigidBody = true;
                    placeTool.allowedOnWalls = false;
                    placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
                    placeTool.rotationEnabled = true;
                    placeTool.enabled = true;
                    placeTool.hasAnimations = false;
                    placeTool.hasBashAnimation = false;
                    placeTool.hasFirstUseAnimation = false;
                    placeTool.mainCollider = collider;
                    placeTool.pickupable = pickupable;

                    // We can eat this item
                    Eatable eatable = prefab.GetComponent<Eatable>();
                    if (eatable == null)
                    {
#if DEBUG_ITEMS_REGISTRATION
                        Logger.Debug("DEBUG: Eatable component not found nutrient block. Adding it.");
#endif
                        eatable = prefab.AddComponent<Eatable>();
                        eatable.foodValue = 75.0f;
                        eatable.waterValue = 0.0f;
#if SUBNAUTICA
                        eatable.stomachVolume = 10.0f;
                        eatable.allowOverfill = false;
#endif
                        eatable.decomposes = false;
                        eatable.kDecayRate = 0.0f;
                        eatable.despawns = false;
                        eatable.despawnDelay = 0.0f;
                    }

                    // Add fabricating animation
                    var fabricating = child.AddComponent<VFXFabricating>();
                    fabricating.localMinY = -0.2f;
                    fabricating.localMaxY = 0.4f;
                    fabricating.posOffset = new Vector3(0f, 0.12f, 0.04f);
                    fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
                    fabricating.scaleFactor = 0.8f;
                    fabricating.enabled = true;
                }
            };
            _nutrientBlockTechType = nutrientBlock.Info.TechType;
            // Set NutrientBlock recipe
            Nautilus.Crafting.RecipeData recipe = new Nautilus.Crafting.RecipeData(new List<Ingredient>() {
                new Ingredient(TechType.Melon, 1),
                new Ingredient(TechType.HangingFruit, 1),
                new Ingredient(TechType.PurpleVegetable, 1),
                new Ingredient(TechType.BulboTreePiece, 1),
                new Ingredient(TechType.CreepvinePiece, 1),
                new Ingredient(TechType.JellyPlant, 1),
                new Ingredient(TechType.KooshChunk, 1)
            }) { craftAmount = 1 };
            Nautilus.Handlers.CraftDataHandler.SetRecipeData(_nutrientBlockTechType, recipe);
            // Add NutrientBlock to the hand-equipments
            Nautilus.Handlers.CraftDataHandler.SetEquipmentType(_nutrientBlockTechType, EquipmentType.Hand);
            // Set NutrientBlock quick slot type
            Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(_nutrientBlockTechType, QuickSlotType.Selectable);
            // Unlock NutrientBlock at start
            Nautilus.Handlers.KnownTechHandler.UnlockOnStart(_nutrientBlockTechType);
            // Register NutrientBlock
            nutrientBlock.SetGameObject(nutrientBlockPrefab);
            nutrientBlock.Register();

            return nutrientBlock;
        }

        #endregion

        #region Toy Car

        public static CustomPrefab RegisterToyCar()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Debug("DEBUG: Configuring ToyCar.");
#endif
            var toyCar = new CustomPrefab("DecoToyCar", Language.main.Get("ToyCar"), Language.main.Get("Tooltip_ToyCar"), SpriteManager.Get(TechType.ToyCar));
            _toyCarTechType = toyCar.Info.TechType;
            var toyCarPrefab = new CloneTemplate(toyCar.Info, "dfabc84e-c4c5-45d9-8b01-ca0eaeeb8e65")
            {
                ModifyPrefab = prefab =>
                {
                    // Update TechTag
                    var techTag = prefab.GetComponent<TechTag>();
                    if (techTag == null)
                        if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                            techTag = prefab.AddComponent<TechTag>();
                    techTag.type = _toyCarTechType;

                    // Update prefab ID
                    var prefabId = prefab.GetComponent<PrefabIdentifier>();
                    if (prefabId == null)
                        if ((prefabId = prefab.GetComponentInChildren<PrefabIdentifier>()) == null)
                            prefabId = prefab.AddComponent<PrefabIdentifier>();
                    prefabId.ClassId = "DecoToyCar";

                    // Make toy car placeable in more places
                    var pt = prefab.GetComponent<PlaceTool>();
                    if (pt != null)
                    {
                        pt.allowedInBase = true;
                        pt.allowedOnBase = true;
                        pt.allowedOnCeiling = true;
                        pt.allowedOnGround = true;
                        pt.allowedOnRigidBody = true;
                        pt.allowedOutside = true;
                        pt.allowedUnderwater = true;
                    }

                    // Add fabricating animation
                    var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
                    fabricating.localMinY = -0.04f;
                    fabricating.localMaxY = 0.25f;
                    fabricating.posOffset = new Vector3(-0.05f, 0f, -0.06f);
                    fabricating.eulerOffset = new Vector3(0f, 90f, 0f);
                    fabricating.scaleFactor = 1f;
                }
            };
            _toyCarTechType = toyCar.Info.TechType;
            // Set ToyCar recipe
            Nautilus.Crafting.RecipeData recipe = new Nautilus.Crafting.RecipeData(new List<Ingredient>() {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Silicone, 1)
            }) { craftAmount = 1 };
            Nautilus.Handlers.CraftDataHandler.SetRecipeData(_toyCarTechType, recipe);
            // Add ToyCar to the hand-equipments
            Nautilus.Handlers.CraftDataHandler.SetEquipmentType(_toyCarTechType, EquipmentType.Hand);
            // Set ToyCar quick slot type
            Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(_toyCarTechType, QuickSlotType.Selectable);
            // Unlock ToyCar at start
            Nautilus.Handlers.KnownTechHandler.UnlockOnStart(_toyCarTechType);
            // Register ToyCar
            toyCar.SetGameObject(toyCarPrefab);
            toyCar.Register();

            return toyCar;
        }

        #endregion

        #region Light Switch

        /// <summary>Small component attached to bases/cyclops to restore their lights state.</summary>
        public class SubRootRestoreLightsState : MonoBehaviour
        {
            public bool IsLightsOn = true;

            /// <summary>Gets called when this MonoBehaviour wakes up.</summary>
            public void Awake()
            {
                if (enabled)
                    Invoke("RestoreLightState", 3.0f); // We add a small delay before restoring lights state because the cyclops needs few frames to complete its initialization
            }

            /// <summary>This function gets called by <see cref="Awake"/> method. It restores light state of current base or submarine.</summary>
            public void RestoreLightState()
            {
                if (!enabled)
                    return;

                // Get current base/cyclops and restore its lights state
                SubRoot subRoot = GetComponent<SubRoot>();
                if (subRoot != null)
                    subRoot.ForceLightingState(this.IsLightsOn);
            }
        }

        public class DecoLightSwitchToggle : HandTarget, IHandTarget
        {
            // Get light state field
            private static readonly FieldInfo _isLightsOnField = typeof(SubRoot).GetField("subLightsOn", BindingFlags.Instance | BindingFlags.NonPublic);

            public DecoLightSwitchToggle() { }

            /// <summary>Returns the SubRoot object (if light switch is in a base, the BaseRoot is casted into a SubRoot).</summary>
            public SubRoot GetSubRoot()
            {
                SubRoot subRoot = GetComponentInParent<SubRoot>(); // Try get SubRoot (if light switch is in a submarine)
                if (subRoot == null) subRoot = gameObject?.transform?.parent?.GetComponent<SubRoot>(); // Try get SubRoot from gameObject's parent (if light switch is in a submarine)
                if (subRoot == null) subRoot = GetComponentInParent<BaseRoot>(); // Try get SubRoot from BaseRoot (if light switch is in a base)
                if (subRoot == null) subRoot = gameObject?.transform?.parent?.GetComponent<BaseRoot>();  // Try get SubRoot from gameObject's parent BaseRoot (if light switch is in a base)
                return subRoot;
            }

            /// <summary>Gets called upon HandClick event.</summary>
            /// <param name="hand">The hand that triggered the click event.</param>
            public void OnHandClick(GUIHand hand)
            {
                if (!enabled) return;

                // Get light switch SubRoot
                var subRoot = GetSubRoot();
                if (subRoot == null) return; // Return if light switch is not in a base or in a submarine

                // Get light switch Constructable
                var constructable = GetComponent<Constructable>();
                if (constructable == null || !constructable.constructed) return; // Return if light switch has not been built

                // Get current light state
                var isLightsOn = (bool)_isLightsOnField.GetValue(subRoot);

                // Set new light state
                isLightsOn = !isLightsOn;
                subRoot.ForceLightingState(isLightsOn);

                // Play sound (depending on new light state). Scraped from : https://github.com/K07H/DecorationsMod/blob/master/Subnautica_AudioAssets.txt
                if (isLightsOn)
                    FMODUWE.PlayOneShot(new FMODAsset() { id = "2103", path = "event:/sub/cyclops/lights_on", name = "5384ec29-f493-4ac1-9f74-2c0b14d61440", hideFlags = HideFlags.None }, MainCamera.camera.transform.position, 1f);
                else
                    FMODUWE.PlayOneShot(new FMODAsset() { id = "2102", path = "event:/sub/cyclops/lights_off", name = "95b877e8-2ccd-451d-ab5f-fc654feab173", hideFlags = HideFlags.None }, MainCamera.camera.transform.position, 1f);
            }

            /// <summary>Gets called upon HandHover event.</summary>
            /// <param name="hand">The hand that triggered the hover event.</param>
            public void OnHandHover(GUIHand hand)
            {
                if (!enabled)
                    return;

                var reticle = HandReticle.main;
                reticle.SetIcon(HandReticle.IconType.Hand, 1f);
                reticle.SetText(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("LightSwitchToggleLights"), false, GameInput.Button.LeftHand);
            }
        }

        private static void LoadLightSwitchPrefab(TechType techType)
        {
            if (_lightSwitchPrefab != null)
                return;

            // Ensure compatibility with BaseLightSwitch mod
            AssetBundle lightSwitchAssets = null;
            var allAssets = AssetBundle.GetAllLoadedAssetBundles();
            if (allAssets != null)
                foreach (var assetBundle in allAssets)
                    if (assetBundle?.name != null && assetBundle.name.Equals("lightswitch"))
                    {
                        lightSwitchAssets = assetBundle;
                        break;
                    }

            // Make sure the AssetBundle has been loaded
            if (lightSwitchAssets == null)
                lightSwitchAssets = AssetBundle.LoadFromFile(FilesHelper.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets/lightswitch.assets"));
            if (lightSwitchAssets == null)
                return;

            // Load GameObject
            var go = lightSwitchAssets.LoadAsset<GameObject>("LightSwitch");

            // Store the prefab
            _lightSwitchPrefab = go.FindChild("model");
        }

        public static GameObject GetDecoLightSwitchGameObject()
        {
            // Make a clone
            var lightSwitch = GameObject.Instantiate(_lightSwitchPrefab);

            // Grab model
            var lightSwitchModel = lightSwitch.FindChild("LIGHTSWITCH");

            // Remove RigidBody
            var rb = lightSwitch.GetComponent<Rigidbody>();
            if (rb != null) MonoBehaviour.DestroyImmediate(rb);

            // Ensure TechTag validity
            var techTag = lightSwitch.EnsureComponent<TechTag>();
            techTag.type = _lightSwitchTechType;

            // Ensure PrefabIdentifier validity
            var prefabId = lightSwitch.EnsureComponent<PrefabIdentifier>();
            prefabId.ClassId = "DecoLightSwitch";

            // Ensure LargeWorldEntity validity
            var lwe = lightSwitch.EnsureComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Apply Subnautica shaders
            MaterialUtils.ApplySNShaders(lightSwitch);

            // Add Constructable
            var constructable = lightSwitch.EnsureComponent<Constructable>();
            constructable.allowedOnWall = true;
            constructable.allowedOnGround = false;
            constructable.allowedInSub = true;
            constructable.allowedInBase = true;
            constructable.allowedOnCeiling = false;
            constructable.allowedOutside = false;
            constructable.model = lightSwitchModel;
            constructable.techType = _lightSwitchTechType;

            // Add ConstructableBounds
            var bounds = lightSwitch.EnsureComponent<ConstructableBounds>();

            // Add BoxCollider
            var collider = lightSwitch.EnsureComponent<BoxCollider>();
            collider.size = new Vector3(0.43f, 0.25f, 0.07f);

            // Add BaseModuleLighting
            BaseModuleLighting bml = lightSwitch.EnsureComponent<BaseModuleLighting>();

            // Add SkyApplier
            var sa = lightSwitch.EnsureComponent<SkyApplier>();
            sa.renderers = lightSwitchModel.GetComponentsInChildren<Renderer>(true);
            sa.anchorSky = Skies.Auto;

            // Add our custom MonoBehaviour controller
            var lightToggle = lightSwitch.AddComponent<DecoLightSwitchToggle>();

            return lightSwitch;
        }

        public static CustomPrefab RegisterLightSwitch()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Debug("DEBUG: Configuring DecoLightSwitch.");
#endif
            var lightSwitch = new CustomPrefab("DecoLightSwitch", LanguageHelper.GetFriendlyWord("LightSwitch"), LanguageHelper.GetFriendlyWord("LightSwitchDescription"), ImageUtils.LoadSpriteFromFile("./BepInEx/plugins/DecorationsMod/Assets/lightswitch.png"));
            _lightSwitchTechType = lightSwitch.Info.TechType;
            // Load light switch prefab
            LoadLightSwitchPrefab(_lightSwitchTechType);
            if (_lightSwitchPrefab == null)
                return null;
            // Set light switch recipe
            CraftDataHandler.SetRecipeData(_lightSwitchTechType, new RecipeData()
            {
                Ingredients = new List<Ingredient>() { new Ingredient(TechType.Titanium, 2) },
                craftAmount = 1
            });
            // Set light switch as buildable
            CraftDataHandler.AddBuildable(_lightSwitchTechType);
            // Add light switch to PDA group category
            CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, _lightSwitchTechType);
            // Unlock light switch at start
            KnownTechHandler.UnlockOnStart(_lightSwitchTechType);
            // Set the buildable prefab
            lightSwitch.SetGameObject(GetDecoLightSwitchGameObject);
            // Register light switch
            lightSwitch.Register();

            return lightSwitch;
        }

        #endregion
    }
}
