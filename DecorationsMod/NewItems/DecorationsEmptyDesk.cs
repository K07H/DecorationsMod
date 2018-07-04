using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class DecorationsEmptyDesk : DecorationItem
    {
        public DecorationsEmptyDesk() // Feeds abstract class
        {
            this.ClassID = "DecorationsEmptyDesk"; // 04a07ec0-e3f4-4285-a087-688215fdb142
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/Starship_work_desk_01_empty";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("DecorationsEmptyDeskName"),
                                                        LanguageHelper.GetFriendlyWord("DecorationsEmptyDeskDescription"),
                                                        true);

            if (ConfigSwitcher.EmptyDesk_asBuildable)
                this.IsHabitatBuilder = true;

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1]
                    {
                        new IngredientHelper(TechType.Titanium, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (ConfigSwitcher.EmptyDesk_asBuildable)
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
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("deskemptyicon")));

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
            techTag.type = this.TechType;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Remove rigid body
            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            GameObject.DestroyImmediate(rb);

            // Update large world entity
            LargeWorldEntity lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Adjust collider
            BoxCollider collider = prefab.GetComponentInChildren<BoxCollider>();
            collider.size = new Vector3(collider.size.x, collider.size.y - 0.02f, collider.size.z);
            collider.center = new Vector3(collider.center.x, collider.center.y - 0.02f, collider.center.z);

            if (!ConfigSwitcher.EmptyDesk_asBuildable)
            {
                // We can pick this item
                Pickupable pickupable = prefab.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                PlaceTool placeTool = prefab.AddComponent<PlaceTool>();
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
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;

                // Add fabricating animation
                VFXFabricating fabricating = prefab.FindChild("Starship_work_desk_01_empty").AddComponent<VFXFabricating>();
                fabricating.localMinY = -0.1f;
                fabricating.localMaxY = 0.9f;
                fabricating.posOffset = new Vector3(-0.06f, 0f, 0.04f);
                fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
                fabricating.scaleFactor = 0.35f;
            }
            else
            {
                // Set as constructible
                Constructable constructible = prefab.AddComponent<Constructable>();
                constructible.techType = this.TechType;
                constructible.allowedOnWall = false;
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = false;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = false;
                constructible.deconstructionAllowed = true;
                constructible.controlModelState = true;
                constructible.model = prefab.FindChild("Starship_work_desk_01_empty");
            }

            return prefab;
        }
    }
}
