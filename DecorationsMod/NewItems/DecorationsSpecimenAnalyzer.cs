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
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class DecorationsSpecimenAnalyzer : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public DecorationsSpecimenAnalyzer() : base("DecorationsSpecimenAnalyzer", "SpecimenAnalyzerName", "SpecimenAnalyzerDescription", "specimenanalyzer")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public DecorationsSpecimenAnalyzer() // Feeds abstract class
        {
            this.ClassID = "DecorationsSpecimenAnalyzer"; // c9bdcc4d-a8c6-43c0-8f7a-f86841cd4493
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SpecimenAnalyzerName"),
                                                        LanguageHelper.GetFriendlyWord("SpecimenAnalyzerDescription"),
                                                        true);
#endif

            CrafterLogicFixer.SpecimenAnalyzer = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            if (ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                this.IsHabitatBuilder = true;

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[3]
                    {
                        new Ingredient(TechType.WiringKit, 1),
                        new Ingredient(TechType.ComputerChip, 1),
                        new Ingredient(TechType.Titanium, 2)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                if (ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                {
                    // Add to the custom buidables
                    CraftDataHandler.AddBuildable(this.TechType);
                    CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                }
                else
                {
                    // Set item occupies 4 slots
                    CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                    // Add the new TechType to the hand-equipments
                    CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                    // Set quick slot type.
                    CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
                }

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom icon
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("specimenanalyzer"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _specimenAnalyzer = null;

        public override GameObject GetGameObject()
        {
            if (_specimenAnalyzer == null)
                _specimenAnalyzer = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/SpecimenAnalyzer.prefab");

            GameObject prefab = GameObject.Instantiate(_specimenAnalyzer);

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
                prefab.AddComponent<CustomPlaceToolController>();
                var placeTool = prefab.AddComponent<GenericPlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = false;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
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
                constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = false;
                constructible.deconstructionAllowed = true;
                constructible.controlModelState = true;
                constructible.techType = this.TechType;
                constructible.placeMinDistance = 1.0f;
                constructible.enabled = true;
            }

            return prefab;
        }
    }
}
