using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class EatMyDiction : DecorationItem
    {
        public EatMyDiction()
        {
            this.ClassID = "MarlaCat"; // c96baff4-0993-4893-8345-adb8709901a7
            this.ResourcePath = "Submarine/Build/Eatmydiction";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            if (ConfigSwitcher.EatMyDiction_asBuidable)
            {
                this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("MarlaCatName"),
                                                        LanguageHelper.GetFriendlyWord("MarlaCatDescription"),
                                                        true);
                this.IsHabitatBuilder = true;
            }
            else
            {
                this.TechType = TechType.EatMyDiction;
                KnownTechPatcher.unlockedAtStart.Add(this.TechType);
            }

                this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1]
                    {
                        new IngredientHelper(TechType.FiberMesh, 2)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (ConfigSwitcher.EatMyDiction_asBuidable)
                {
                    // Add the new TechType to the buildables
                    CraftDataPatcher.customBuildables.Add(this.TechType);
                    CraftDataPatcher.AddToCustomGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                    CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));
                }
                else
                {
                    // Add the new TechType to the hand-equipments
                    CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);
                    // Set the buildable prefab
                    CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));
                }
                
                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, SpriteManager.Get(TechType.EatMyDiction)));
                
                if (ConfigSwitcher.EatMyDiction_asBuidable)
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
            GameObject model = prefab.FindChild("Eatmydiction");

            prefab.name = this.ClassID;

            if (!ConfigSwitcher.EatMyDiction_asBuidable)
            {
                // Add box collider
                var collider = prefab.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.5f, 0.5f, 0.5f);

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
                var fabricating = model.AddComponent<VFXFabricating>();
                fabricating.localMinY = -0.1f;
                fabricating.localMaxY = 0.5f;
                fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
                fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
                fabricating.scaleFactor = 0.1f;
            }
            else
            {
                // Update TechTag
                var techTag = prefab.GetComponent<TechTag>();
                if (techTag == null)
                    if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                        techTag = prefab.AddComponent<TechTag>();
                techTag.type = this.TechType;
            }

            return prefab;
        }
    }
}
