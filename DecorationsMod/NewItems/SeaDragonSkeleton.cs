using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class SeaDragonSkeleton : DecorationItem
    {
        [SetsRequiredMembers]
        public SeaDragonSkeleton() : base("SeaDragonSkeletonDeco", LanguageHelper.GetFriendlyWord("SeaDragonSkeletonName"), LanguageHelper.GetFriendlyWord("LeviathanSkeletonDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("seadragonskeletonicon"))
        {
            this.ClassID = "SeaDragonSkeletonDeco";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("Lost_river_sea_dragon_skeleton");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.SeaDragonSkeleton = this.TechType;
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

        private static GameObject _seaDragonSkeleton = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_seaDragonSkeleton == null)
                    _seaDragonSkeleton = AssetsHelper.Assets.LoadAsset<GameObject>("Lost_river_sea_dragon_skeleton");

                GameObject model = _seaDragonSkeleton.FindChild("Lost_river_sea_dragon_skeleton");

                // Scale model
                model.transform.localScale *= 2.0f;

                // Translate model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.19f, model.transform.localPosition.z);

                // Rotate model
                model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + 90.0f, model.transform.localEulerAngles.z + 35.0f);

                // Set tech tag
                var techTag = _seaDragonSkeleton.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                var prefabId = _seaDragonSkeleton.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add collider
                var collider = _seaDragonSkeleton.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.6f, 0.5f, 0.6f);
                collider.isTrigger = true;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Lost_river_sea_dragon_skeleton_bones_normal");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Lost_river_sea_dragon_skeleton_skull_normal");
                var renderers = _seaDragonSkeleton.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers)
                {
                    foreach (Material tmpMat in rend.materials)
                    {
                        tmpMat.shader = marmosetUber;
                        if (string.Compare(tmpMat.name, "Lost_river_sea_dragon_skeleton_bones (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal);
                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                        else if (string.Compare(tmpMat.name, "Lost_river_sea_dragon_skeleton_skull (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal2);
                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                    }
                }

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(_seaDragonSkeleton);

                // Add rigid body
                PrefabsHelper.SetDefaultRigidBody(_seaDragonSkeleton);

                // Add sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_seaDragonSkeleton, null, renderers);
                
                // We can pick this item
                PrefabsHelper.SetDefaultPickupable(_seaDragonSkeleton);

                // We can place this item
                PrefabsHelper.SetDefaultPlaceTool(_seaDragonSkeleton, collider);

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Hand Equipment type
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seadragonskeletonicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_seaDragonSkeleton);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.8f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0.07f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 0.6f;

            return prefab;
        }
    }
}
