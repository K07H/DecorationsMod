using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class SpecimenAnalyzer : DecorationItem
    {
        public SpecimenAnalyzer() // Feeds abstract class
        {
            if (ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                this.ClassID = "DecorationSpecimenAnalyzer";
            else
                this.ClassID = "c9bdcc4d-a8c6-43c0-8f7a-f86841cd4493";

            this.ResourcePath = "Submarine/Build/SpecimenAnalyzer";
            
            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            if (ConfigSwitcher.SpecimenAnalyzer_asBuildable)
            {
                this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                            LanguageHelper.GetFriendlyWord("SpecimenAnalyzerName"),
                                                            LanguageHelper.GetFriendlyWord("SpecimenAnalyzerDescription"),
                                                            true);
                this.IsHabitatBuilder = true;
            }
            else
            {
                this.TechType = TechType.SpecimenAnalyzer;
                KnownTechPatcher.unlockedAtStart.Add(this.TechType);
            }

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                    {
                        new IngredientHelper(TechType.WiringKit, 1),
                        new IngredientHelper(TechType.ComputerChip, 1),
                        new IngredientHelper(TechType.Titanium, 2)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                {
                    // Add to the custom buidables
                    CraftDataPatcher.customBuildables.Add(this.TechType);
                    CraftDataPatcher.AddToCustomGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                    // Set the buildable prefab
                    CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));
                }
                else
                {
                    // Add the new TechType to the hand-equipments
                    CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);

                    // Set the buildable prefab
                    CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, this.GetPrefab));
                }
                
                // Set the custom icon
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("specimenanalyzer")));

                if (ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                {
                    // Associate recipe to the new TechType
                    CraftDataPatcher.customTechData[this.TechType] = this.Recipe;
                }

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            
            prefab.name = this.ClassID;

            // Add rigid body
            var rb = prefab.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.mass = 80;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.FreezePosition;

            if (!ConfigSwitcher.SpecimenAnalyzer_asBuildable)
            {
                // Remove "Constructable" possibility
                Constructable construct = prefab.GetComponent<Constructable>();
                GameObject.DestroyImmediate(construct);

                // Retrieve collider
                GameObject basic = prefab.FindChild("logic").FindChild("base");
                Collider collider = basic.GetComponent<Collider>();

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
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = false;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;

                // Add fabricating animation
                var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
                fabricating.localMinY = -0.1f;
                fabricating.localMaxY = 0.9f;
                fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
                fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
                fabricating.scaleFactor = 0.35f;
            }
            else
            {
                // Update TechTag
                var techTag = prefab.GetComponent<TechTag>();
                if (techTag == null)
                    if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                        techTag = prefab.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set as constructible
                var constructible = prefab.GetComponent<Constructable>();
                constructible.allowedOnWall = false;
                constructible.allowedInBase = true;
                constructible.allowedInSub = true; // This is the important one
                constructible.allowedOutside = false;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = false;
                constructible.deconstructionAllowed = true;
                constructible.controlModelState = true;
                constructible.enabled = true;
                constructible.techType = this.TechType; // This was necessary to correctly associate the recipe at building time

                // Update prefab identifier
                var prefabId = prefab.GetComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;
                prefabId.name = LanguageHelper.GetFriendlyWord("SpecimenAnalyzerName");
                prefabId.enabled = true;
            }

            return prefab;
        }
    }
}
