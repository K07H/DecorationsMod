using DecorationsMod.Fixers;
using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
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
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/wall_monitor_01_03";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.SignObject = Resources.Load<GameObject>("Submarine/Build/Sign");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("WallMonitor3Name"),
                                                        LanguageHelper.GetFriendlyWord("WallMonitor3Description"),
                                                        true);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[2]
                    {
                        new IngredientHelper(TechType.ComputerChip, 1),
                        new IngredientHelper(TechType.Glass, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                screenMaterial = AssetsHelper.Assets.LoadAsset<Material>("new_wall_monitor_screen_material");
                screenMaterial.shader = Shader.Find("MarmosetUBER");

                // Add the new TechType to the hand-equipments
                CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("computer3")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
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
            var placeTool = prefab.AddComponent<PlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = true;
            placeTool.allowedOutside = false;
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
                if (rend.name.CompareTo("Starship_wall_monitor_01_03") == 0)
                {
                    Material[] tmp = rend.materials;
                    if (tmp.Length >= 2)
                    {
                        if (tmp[0].name.CompareTo("Starship_wall_monitor_01_screen (Instance)") == 0)
                            rend.materials = new Material[] { screenMaterial, tmp[1] };
                        else if (tmp[1].name.CompareTo("Starship_wall_monitor_01_screen (Instance)") == 0)
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
