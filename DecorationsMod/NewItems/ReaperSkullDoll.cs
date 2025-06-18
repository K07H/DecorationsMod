using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class ReaperSkullDoll : DecorationItem
    {
        [SetsRequiredMembers]
        public ReaperSkullDoll() : base("ReaperSkullDoll", LanguageHelper.GetFriendlyWord("ReaperSkullDollName"), LanguageHelper.GetFriendlyWord("ReaperSkullDollDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperskullicon"))
        {
            this.ClassID = "ReaperSkullDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("reaperskull");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.ReaperSkullDoll = this.TechType;
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

        private static GameObject _reaperSkullDoll = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_reaperSkullDoll == null)
                    _reaperSkullDoll = AssetsHelper.Assets.LoadAsset<GameObject>("reaperskull");

                GameObject model = _reaperSkullDoll.FindChild("reaperleviathanskull");
                
                // Set tech tag
                var techTag = _reaperSkullDoll.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _reaperSkullDoll.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;

                // Add collider
                var collider = _reaperSkullDoll.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.3f, 0.6f, 0.4f);

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Lost_river_reaper_skeleton_skull_normal");
                var renderers = _reaperSkullDoll.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers)
                {
                    foreach (Material tmpMat in rend.materials)
                    {
                        tmpMat.shader = marmosetUber;
                        if (string.Compare(tmpMat.name, "Lost_river_reaper_skeleton_skull (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal);
                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                    }
                }

                // Add large world entity
                _reaperSkullDoll.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                // Add sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_reaperSkullDoll, null, renderers);

                // We can pick this item
                var pickupable = _reaperSkullDoll.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _reaperSkullDoll.AddComponent<CustomPlaceToolController>();
                var placeTool = _reaperSkullDoll.AddComponent<GenericPlaceTool>();
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("reaperskullicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_reaperSkullDoll);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.9f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
