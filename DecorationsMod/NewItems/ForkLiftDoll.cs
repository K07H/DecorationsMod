using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class ForkLiftDoll : DecorationItem
    {
        public ForkLiftDoll() // Feeds abstract class
        {
            this.ClassID = "ForkLiftDoll";
            this.ResourcePath = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("forkliftdoll");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("ForkLiftDollName"),
                                                        LanguageHelper.GetFriendlyWord("ForkLiftDollDescription"),
                                                        true);

            if (ConfigSwitcher.Forklift_asBuidable)
                this.IsHabitatBuilder = true;

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.Glass, 1),
                        new IngredientHelper(TechType.Silicone, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Retrieve model node
                GameObject model = this.GameObject.FindChild("forklift");

                // Scale model
                model.transform.localScale *= 5.0f;

                // Move model
                //model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.1f, model.transform.localPosition.z);

                // Add prefab identifier
                var prefabId = this.GameObject.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                var lwe = this.GameObject.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Add tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add box collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.2f, 0.2f, 0.2f);

                // Set proper shaders (for crafting animation)
                Shader shader = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("generic_forklift_normal");
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("generic_forklift_illum");
                Renderer[] renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    foreach (Material tmpMat in renderer.materials)
                    {
                        // Associate MarmosetUBER shader
                        tmpMat.shader = shader;

                        if (tmpMat.name.CompareTo("generic_forklift (Instance)") == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal);
                            tmpMat.SetTexture("_Illum", illum);
                            tmpMat.SetFloat("_EmissionLM", 1.0f);

                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("MARMO_EMISSION");
                        }
                    }
                }

                // Add sky applier
                var skyapplier = this.GameObject.AddComponent<SkyApplier>();
                skyapplier.renderers = renderers;
                skyapplier.anchorSky = Skies.Auto;

                if (ConfigSwitcher.Forklift_asBuidable)
                {
                    // Add contructable
                    var constructible = this.GameObject.AddComponent<Constructable>();
                    constructible.allowedInBase = true;
                    constructible.allowedInSub = true;
                    constructible.allowedOutside = true;
                    constructible.allowedOnCeiling = false;
                    constructible.allowedOnGround = true;
                    constructible.allowedOnConstructables = true;
                    constructible.controlModelState = true;
                    constructible.deconstructionAllowed = true;
                    constructible.rotationEnabled = false;
                    constructible.model = model;
                    constructible.techType = this.TechType;
                    constructible.enabled = true;

                    // Add constructable bounds
                    var bounds = this.GameObject.AddComponent<ConstructableBounds>();

                    // Add new TechType to the buildables
                    CraftDataPatcher.customBuildables.Add(this.TechType);
                    CraftDataPatcher.AddToCustomGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                }
                else
                {
                    // We can pick this item
                    var pickupable = this.GameObject.AddComponent<Pickupable>();
                    pickupable.isPickupable = true;
                    pickupable.randomizeRotationWhenDropped = true;

                    // We can place this item
                    var placeTool = this.GameObject.AddComponent<PlaceTool>();
                    placeTool.allowedInBase = true;
                    placeTool.allowedOnBase = true;
                    placeTool.allowedOnCeiling = false;
                    placeTool.allowedOnConstructable = true;
                    placeTool.allowedOnGround = true;
                    placeTool.allowedOnRigidBody = true;
                    placeTool.allowedOnWalls = false;
                    placeTool.allowedOutside = false;
                    placeTool.rotationEnabled = true;
                    placeTool.enabled = true;
                    placeTool.hasAnimations = false;
                    placeTool.hasBashAnimation = false;
                    placeTool.hasFirstUseAnimation = false;
                    placeTool.mainCollider = collider;
                    placeTool.pickupable = pickupable;
                    placeTool.drawTime = 0.5f;
                    placeTool.dropTime = 1;
                    placeTool.holsterTime = 0.35f;

                    // Set item occupies 4 slots
                    CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(2, 2);

                    // Add the new TechType to Hand Equipment type.
                    CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);
                }

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, this.GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("forklifticon")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            if (!ConfigSwitcher.Forklift_asBuidable)
            {
                // Add fabricating animation
                var fabricatingA = prefab.AddComponent<VFXFabricating>();
                fabricatingA.localMinY = -0.2f;
                fabricatingA.localMaxY = 0.7f;
                fabricatingA.posOffset = new Vector3(0f, 0f, 0f);
                fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
                fabricatingA.scaleFactor = 1f;
            }

            return prefab;
        }
    }
}
