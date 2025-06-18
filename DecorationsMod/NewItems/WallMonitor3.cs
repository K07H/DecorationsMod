#if SUBNAUTICA_NAUTILUS
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
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class WallMonitor3 : DecorationItem
    {
        public GameObject SignObject = null;

        private Material screenMaterial = null;

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public WallMonitor3() : base("WallMonitor3", "WallMonitor3Name", "WallMonitor3Description", "computer3")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public WallMonitor3() // Feeds abstract class
        {
            this.ClassID = "WallMonitor3"; //cb612e1b-d57a-44f5-a043-a886eb17e5a6
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("WallMonitor3Name"),
                                                        LanguageHelper.GetFriendlyWord("WallMonitor3Description"),
                                                        true);
#endif

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.ComputerChip, 1),
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                screenMaterial = AssetsHelper.Assets.LoadAsset<Material>("new_wall_monitor_screen_material");
                screenMaterial.shader = Shader.Find("MarmosetUBER");

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Add the new TechType to the hand-equipments
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _wallMonitor3 = null;

        public override GameObject GetGameObject()
        {
            if (_wallMonitor3 == null)
#if SUBNAUTICA
                _wallMonitor3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/wall_monitor_01_03.prefab");
#else
                _wallMonitor3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/alterra/base/wall_monitor_01_03.prefab");
#endif
            if (this.SignObject == null)
                this.SignObject = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Sign.prefab");

            GameObject prefab = GameObject.Instantiate(_wallMonitor3);
            GameObject signPrefab = GameObject.Instantiate(this.SignObject);

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
            GameObject cube = prefab.FindChild("Cube");
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
                if (string.Compare(rend.name, "Starship_wall_monitor_01_03", true, CultureInfo.InvariantCulture) == 0)
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
