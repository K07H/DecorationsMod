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

        public WallMonitor3() // Feeds abstract class
        {
            this.ClassID = "WallMonitor3"; //cb612e1b-d57a-44f5-a043-a886eb17e5a6
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

#if BELOWZERO
            this.GameObject = Resources.Load<GameObject>("WorldEntities/alterra/base/wall_monitor_01_03");
#else
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/wall_monitor_01_03");
#endif

            this.SignObject = Resources.Load<GameObject>("Submarine/Build/Sign");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("WallMonitor3Name"),
                                                        LanguageHelper.GetFriendlyWord("WallMonitor3Description"),
                                                        true);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.ComputerChip, 1),
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.ComputerChip, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                screenMaterial = AssetsHelper.Assets.LoadAsset<Material>("new_wall_monitor_screen_material");
                screenMaterial.shader = Shader.Find("MarmosetUBER");

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add the new TechType to the hand-equipments
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
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
