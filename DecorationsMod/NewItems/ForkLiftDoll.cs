﻿using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class ForkLiftDoll : DecorationItem
    {
        private Texture normal = null;
        private Texture illum = null;

        [SetsRequiredMembers]
        public ForkLiftDoll() : base("ForkLiftDoll", LanguageHelper.GetFriendlyWord("ForkLiftDollName"), LanguageHelper.GetFriendlyWord("ForkLiftDollDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("forklifticon"))
        {
            // Feed DecortionItem interface
            this.ClassID = "ForkLiftDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("forkliftdoll");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            if (ConfigSwitcher.Forklift_asBuidable)
                this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                normal = AssetsHelper.Assets.LoadAsset<Texture>("generic_forklift_normal");
                illum = AssetsHelper.Assets.LoadAsset<Texture>("generic_forklift_illum");

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                if (ConfigSwitcher.Forklift_asBuidable)
                {
                    // Add new TechType to the buildables
                    Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                    Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                }
                else
                {
                    // Set item occupies 4 slots
                    Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                    // Add the new TechType to Hand Equipment type.
                    Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                    // Set quick slot type.
                    Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
                }

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("forklifticon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _forkliftDoll = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: forkliftDoll.GetGameObject()");
#endif
            if (_forkliftDoll == null)
                _forkliftDoll = AssetsHelper.Assets.LoadAsset<GameObject>("forkliftdoll");

            GameObject prefab = GameObject.Instantiate(_forkliftDoll);

            prefab.name = this.ClassID;

            // Add tech tag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Add prefab identifier
            var prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Retrieve model node
            GameObject model = prefab.FindChild("forklift");
            
            // Add large world entity
            var lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;
            
            // Add box collider
            var collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.05f, 0.064f, 0.04f);
            collider.center = new Vector3(collider.center.x, collider.center.y + 0.032f, collider.center.z);

            // Set proper shaders (for crafting animation)
            Shader shader = Shader.Find("MarmosetUBER");
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material tmpMat in renderer.materials)
                {
                    // Associate MarmosetUBER shader
                    tmpMat.shader = shader;

                    if (string.Compare(tmpMat.name, "generic_forklift (Instance)", true, CultureInfo.InvariantCulture) == 0)
                    {
                        tmpMat.SetTexture("_BumpMap", normal);
                        tmpMat.SetTexture("_Illum", illum);
                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                        tmpMat.EnableKeyword("MARMO_EMISSION");
                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                    }
                }
            }

            // Add sky applier
            PrefabsHelper.UpdateOrAddSkyApplier(prefab, null, renderers);

            if (ConfigSwitcher.Forklift_asBuidable)
            {
                // Add contructable
                var constructible = prefab.AddComponent<Constructable>();
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = true;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = true;
                constructible.controlModelState = true;
                constructible.deconstructionAllowed = true;
                constructible.rotationEnabled = true;
                constructible.model = model;
                constructible.techType = this.TechType;
                constructible.placeMinDistance = 0.6f;
                constructible.enabled = true;

                // Add constructable bounds
                var bounds = prefab.AddComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.003f, bounds.bounds.position.z);

                // Add forklift controller
                var forkliftController = prefab.AddComponent<ForkliftController>();
            }
            else
            {
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
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = true;
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

                // Add fabricating animation
                var fabricatingA = prefab.AddComponent<VFXFabricating>();
                fabricatingA.localMinY = -0.2f;
                fabricatingA.localMaxY = 0.7f;
                fabricatingA.posOffset = new Vector3(0f, 0f, 0f);
                fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
                fabricatingA.scaleFactor = 1f;
            }

            // Scale prefab
            prefab.transform.localScale *= 5.0f;
            
            return prefab;
        }
    }
}
