﻿using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class CuddleFishDoll : DecorationItem
    {
        [SetsRequiredMembers]
        public CuddleFishDoll() : base("CuddleFishDoll", LanguageHelper.GetFriendlyWord("CuddleFishDollName"), LanguageHelper.GetFriendlyWord("CuddleFishDollDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("cuddlefishicon")) // Feeds abstract class
        {
            this.ClassID = "CuddleFishDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("cuddlefish");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.CuddleFishDoll = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.FiberMesh, 1),
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.FiberMesh, 1),
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _cuddleFishDoll = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_cuddleFishDoll == null)
                    _cuddleFishDoll = AssetsHelper.Assets.LoadAsset<GameObject>("cuddlefish");

                GameObject model = _cuddleFishDoll.FindChild("cutefish");

                // Scale model
                model.transform.localScale *= 1.8f;

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.22f, model.transform.localPosition.z);
                
                // Set tech tag
                var techTag = _cuddleFishDoll.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set prefab identifier
                var prefabId = _cuddleFishDoll.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Set collider
                var collider = _cuddleFishDoll.AddComponent<SphereCollider>();
                collider.radius = 0.2f;
                //collider.size = new Vector3(0.4f, 0.6f, 0.4f);

                // Set large world entity
                var lwe = _cuddleFishDoll.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Cute_fish_normal");
                Texture spec = AssetsHelper.Assets.LoadAsset<Texture>("Cute_fish_spec");
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("Cute_fish_illum");
                var renderers = _cuddleFishDoll.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                tmpMat.shader = marmosetUber;
                                if (string.Compare(tmpMat.name, "Cute_fish (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal);
                                    tmpMat.SetTexture("_SpecTex", spec);
                                    tmpMat.SetTexture("_Illum", illum);
                                    tmpMat.SetFloat("_EmissionLM", 0.8f); // Set always visible // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                            }
                        }
                    }
                }

                // Update sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_cuddleFishDoll, null, renderers);

                // We can pick this item
                var pickupable = _cuddleFishDoll.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _cuddleFishDoll.AddComponent<CustomPlaceToolController>();
                var placeTool = _cuddleFishDoll.AddComponent<GenericPlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
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

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Hand Equipment type.
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("cuddlefishicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_cuddleFishDoll);

            prefab.name = this.ClassID;

            // Add fabricating animation
            /* I disabled crafting animation for this one because it doesn't look good.
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.8f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 0.8f;
            */

            return prefab;
        }
    }
}
