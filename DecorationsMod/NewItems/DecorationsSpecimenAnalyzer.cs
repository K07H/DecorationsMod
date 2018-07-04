using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class DecorationsSpecimenAnalyzer : DecorationItem
    {
        public DecorationsSpecimenAnalyzer() // Feeds abstract class
        {
            this.ClassID = "DecorationsSpecimenAnalyzer"; // c9bdcc4d-a8c6-43c0-8f7a-f86841cd4493
            this.ResourcePath = "Submarine/Build/SpecimenAnalyzer";
            
            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SpecimenAnalyzerName"),
                                                        LanguageHelper.GetFriendlyWord("SpecimenAnalyzerDescription"),
                                                        true);

            if (ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                this.IsHabitatBuilder = true;

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
                }
                else
                {
                    // Set item occupies 4 slots
                    CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(2, 2);

                    // Add the new TechType to the hand-equipments
                    CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);
                }

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));

                // Set the custom icon
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("specimenanalyzer")));
                
                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;

            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            if (techTag == null)
                if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                    techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            if (prefabId == null)
                if ((prefabId = prefab.GetComponentInChildren<PrefabIdentifier>()) == null)
                    prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update storage container size to 18
            SpecimenAnalyzer specimenAnalyzer = prefab.GetComponent<SpecimenAnalyzer>();
            if (specimenAnalyzer.storageContainer != null)
            {
                specimenAnalyzer.storageContainer.height = 6;
                specimenAnalyzer.storageContainer.width = 3;
            }

            if (!ConfigSwitcher.SpecimenAnalyzer_asBuildable)
            {
                // Remove "Constructable" possibility
                Constructable construct = prefab.GetComponent<Constructable>();
                GameObject.DestroyImmediate(construct);
                
                // Disable colliders to prevent physics bug in Cyclops, and add a small one so we can
                // pick the Specimen Analyzer.
                Collider[] colliders = prefab.GetComponentsInChildren<Collider>();
                foreach (Collider coll in colliders)
                {
                    coll.enabled = false;
                }
                SphereCollider newCollider = prefab.AddComponent<SphereCollider>();
                newCollider.radius = 0.9f;

                // We can pick this item
                var pickupable = prefab.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                var placeTool = prefab.AddComponent<PlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = false;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = false;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = newCollider;
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
                // Set as constructible
                var constructible = prefab.GetComponent<Constructable>();
                constructible.allowedOnWall = false;
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = false;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = false;
                constructible.deconstructionAllowed = true;
                constructible.controlModelState = true;
                constructible.enabled = true;
                constructible.techType = this.TechType;
            }

            return prefab;
        }
    }
}
