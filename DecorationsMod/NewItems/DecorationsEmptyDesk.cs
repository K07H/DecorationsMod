using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class DecorationsEmptyDesk : DecorationItem
    {
        [SetsRequiredMembers]
        public DecorationsEmptyDesk() : base("DecorationsEmptyDesk", LanguageHelper.GetFriendlyWord("DecorationsEmptyDeskName"), LanguageHelper.GetFriendlyWord("DecorationsEmptyDeskDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("deskemptyicon")) // Feeds abstract class
        {
            this.ClassID = "DecorationsEmptyDesk"; // 04a07ec0-e3f4-4285-a087-688215fdb142
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.EmptyDesk = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            if (ConfigSwitcher.EmptyDesk_asBuildable)
                this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                if (ConfigSwitcher.EmptyDesk_asBuildable)
                {
                    // Add to the custom buidables
                    Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                    Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType, TechType.StarshipDesk);
                }
                else
                {
                    // Set item occupies 4 slots
                    Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                    // Add the new TechType to the hand-equipments
                    Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                    // Set quick slot type.
                    Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
                }

                // Set the buildable prefab
                this.Register();

                // Set the custom icon
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("deskemptyicon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _emptyDesk = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: emptyDesk.GetGameObject()");
#endif
            if (_emptyDesk == null)
#if SUBNAUTICA
                _emptyDesk = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/Starship_work_desk_01_empty.prefab");
#else
                _emptyDesk = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Base/Starship_work_desk_01_empty.prefab");
#endif

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_emptyDesk);

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
            collider.size = new Vector3(collider.size.x, collider.size.y - 0.022f, collider.size.z);
            collider.center = new Vector3(collider.center.x, collider.center.y - 0.011f, collider.center.z);

            if (!ConfigSwitcher.EmptyDesk_asBuildable)
            {
                // We can pick this item
                Pickupable pickupable = prefab.AddComponent<Pickupable>();
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
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;
                placeTool.enabled = true;

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
                constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = false;
                constructible.deconstructionAllowed = true;
                constructible.controlModelState = true;
                constructible.model = prefab.FindChild("Starship_work_desk_01_empty");
                constructible.placeMinDistance = 0.6f;
            }

            return prefab;
        }
    }
}
