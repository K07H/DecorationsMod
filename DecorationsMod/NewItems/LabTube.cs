﻿#if SUBNAUTICA_NAUTILUS
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
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class LabTube : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public LabTube() : base("DecorationLabTube", "LabTubeName", "LabTubeDescription", "labtubeicon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public LabTube() // Feeds abstract class
        {
            this.ClassID = "DecorationLabTube"; // a36047b0-1533-4718-8879-d6ba9229c978
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("LabTubeName"),
                                                        LanguageHelper.GetFriendlyWord("LabTubeDescription"),
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
#if SUBNAUTICA
                        new Ingredient(TechType.Titanium, 2),
#else
                        new Ingredient(TechType.Titanium, 1),
#endif
                        new Ingredient(TechType.Glass, 1)
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

                // Set item occupies 9 slots
                CraftDataHandler.SetItemSize(this.TechType, new Vector2int(3, 3));

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
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("labtubeicon"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _labTube = null;

        public override GameObject GetGameObject()
        {
            if (_labTube == null)
#if SUBNAUTICA
                _labTube = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_tube_01.prefab");
#else
                _labTube = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Base/biodome_lab_tube_01.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_labTube);

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

            // Delete the "Capsule" item as it conflicts with PlaceTool
            // when you hold it while swimming (see https://www.youtube.com/watch?v=mGCpK4Z8yQM)
            GameObject Capsule = prefab.FindChild("Capsule");
            GameObject.DestroyImmediate(Capsule);
            
            // Remove rigid body to prevent bugs
            var rb = prefab.GetComponent<Rigidbody>();
            GameObject.DestroyImmediate(rb);

            // Scale model
            GameObject model = prefab.FindChild("biodome_lab_tube_01");
            model.GetComponent<Transform>().localScale *= 0.4f;

            // Add box collider
            var collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.5f, 2.5f, 0.5f);

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
            placeTool.pickupable = pickupable;
            placeTool.mainCollider = collider;

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.3f;

            return prefab;
        }
    }
}
