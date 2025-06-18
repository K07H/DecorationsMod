using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class GenericSkeleton2 : DecorationItem
    {
        [SetsRequiredMembers]
        public GenericSkeleton2() : base("GenericSkeleton2", LanguageHelper.GetFriendlyWord("GenericSkeletonName") + " (2)", LanguageHelper.GetFriendlyWord("GenericSkeletonDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("genericskeleton2icon"))
        {
            this.ClassID = "GenericSkeleton2";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("Lost_river_generic_skeleton_02");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.GenericSkeleton2 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

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

        private static GameObject _genericSkeleton2 = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_genericSkeleton2 == null)
                    _genericSkeleton2 = AssetsHelper.Assets.LoadAsset<GameObject>("Lost_river_generic_skeleton_02");

                GameObject model = _genericSkeleton2.FindChild("Lost_river_generic_skeleton_02");

                // Scale model
                model.transform.localScale *= 4.0f;

                // Rotate model
                model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x + 3.0f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z);

                // Translate model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.05f, model.transform.localPosition.z);
                
                // Rotate model
                model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + 90.0f, model.transform.localEulerAngles.z);

                // Set tech tag
                var techTag = _genericSkeleton2.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                var prefabId = _genericSkeleton2.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add collider
                var collider = _genericSkeleton2.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.6f, 0.2f, 0.2f);

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Lost_river_reaper_skeleton_bones_normal");
                var renderers = _genericSkeleton2.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers)
                {
                    foreach (Material tmpMat in rend.materials)
                    {
                        tmpMat.shader = marmosetUber;
                        if (string.Compare(tmpMat.name, "Lost_river_reaper_skeleton_bones (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal);
                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                    }
                }

                // Add large world entity
                var lwe = _genericSkeleton2.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Add sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_genericSkeleton2, null, renderers);

                // We can pick this item
                var pickupable = _genericSkeleton2.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _genericSkeleton2.AddComponent<CustomPlaceToolController>();
                var placeTool = _genericSkeleton2.AddComponent<GenericPlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
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
                placeTool.ghostModelPrefab = _genericSkeleton2;
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("genericskeleton2icon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_genericSkeleton2);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.1f;
            fabricatingA.localMaxY = 0.4f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 0.75f;

            return prefab;
        }
    }
}
