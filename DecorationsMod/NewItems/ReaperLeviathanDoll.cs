using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class ReaperLeviathanDoll : DecorationItem
    {
        [SetsRequiredMembers]
        public ReaperLeviathanDoll() : base("ReaperLeviathanDoll", LanguageHelper.GetFriendlyWord("ReaperLeviathanDollName"), LanguageHelper.GetFriendlyWord("ReaperLeviathanDollDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon")) // Feeds abstract class
        {
            this.ClassID = "ReaperLeviathanDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("ReaperLeviathan");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.ReaperLeviathanDoll = this.TechType;
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

        private static GameObject _reaperLeviathanDoll = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_reaperLeviathanDoll == null)
                    _reaperLeviathanDoll = AssetsHelper.Assets.LoadAsset<GameObject>("ReaperLeviathan");

                // Scale
                GameObject model = _reaperLeviathanDoll.FindChild("ReaperLeviathan");
                model.transform.localScale *= 0.53f;
                
                // Set tech tag
                var techTag = _reaperLeviathanDoll.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _reaperLeviathanDoll.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;
                
                // Add collider
                var collider = _reaperLeviathanDoll.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.8f, 0.5f, 0.5f);

                // Add large world entity
                _reaperLeviathanDoll.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Reaper_Leviathan_normal");
                Texture spec = AssetsHelper.Assets.LoadAsset<Texture>("Reaper_Leviathan_spec");
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("Reaper_Leviathan_illum");
                var renderers = _reaperLeviathanDoll.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                tmpMat.shader = marmosetUber;
                                if (string.Compare(tmpMat.name, "Reaper_Leviathan (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal);
                                    tmpMat.SetTexture("_SpecTex", spec);
                                    tmpMat.SetTexture("_Illum", illum);
                                    tmpMat.SetFloat("_EmissionLM", 0.8f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                            }
                        }
                    }
                }

                // Add sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_reaperLeviathanDoll, null, renderers);
                
                // We can pick this item
                var pickupable = _reaperLeviathanDoll.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _reaperLeviathanDoll.AddComponent<CustomPlaceToolController>();
                var placeTool = _reaperLeviathanDoll.AddComponent<GenericPlaceTool>();
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_reaperLeviathanDoll);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.7f;
            fabricatingA.posOffset = new Vector3(0.2f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
