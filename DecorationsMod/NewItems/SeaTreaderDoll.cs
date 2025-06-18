using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class SeaTreaderDoll : DecorationItem
    {
        [SetsRequiredMembers]
        public SeaTreaderDoll() : base("SeaTreaderDoll", LanguageHelper.GetFriendlyWord("SeaTreaderDollName"), LanguageHelper.GetFriendlyWord("SeaTreaderDollDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("seatreadericon")) // Feeds abstract class
        {
            this.ClassID = "SeaTreaderDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("seatreaderleviathan");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.SeaTreaderDoll = this.TechType;
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

        private static GameObject _seaTreaderDoll = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_seaTreaderDoll == null)
                    _seaTreaderDoll = AssetsHelper.Assets.LoadAsset<GameObject>("seatreaderleviathan");

                // Scale model
                GameObject treaderModel = _seaTreaderDoll.FindChild("Sea_Treader");
                treaderModel.transform.localScale *= 0.8f;
                
                // Set tech tag
                var techTag = _seaTreaderDoll.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _seaTreaderDoll.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;
                
                // Add collider
                var collider = _seaTreaderDoll.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.8f, 0.5f, 0.5f);

                // Add large world entity
                _seaTreaderDoll.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
                
                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("Sea_Treader_01_01_normal");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Sea_Treader_01_02_normal");
                Texture spec1 = AssetsHelper.Assets.LoadAsset<Texture>("Sea_Treader_01_01_spec");
                Texture spec2 = AssetsHelper.Assets.LoadAsset<Texture>("Sea_Treader_01_02_spec");
                Texture illum1 = AssetsHelper.Assets.LoadAsset<Texture>("Sea_Treader_01_01_illum");
                Texture illum2 = AssetsHelper.Assets.LoadAsset<Texture>("Sea_Treader_01_02_illum");
                var renderers = _seaTreaderDoll.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                tmpMat.shader = marmosetUber;
                                if (string.Compare(tmpMat.name, "Sea_Treader_01_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal1);
                                    tmpMat.SetTexture("_SpecTex", spec1);
                                    tmpMat.SetTexture("_Illum", illum1);
                                    tmpMat.SetFloat("_EmissionLM", 0.8f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Sea_Treader_01_02 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal2);
                                    tmpMat.SetTexture("_SpecTex", spec2);
                                    tmpMat.SetTexture("_Illum", illum2);
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
                PrefabsHelper.UpdateOrAddSkyApplier(_seaTreaderDoll, null, renderers);
                
                // We can pick this item
                var pickupable = _seaTreaderDoll.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _seaTreaderDoll.AddComponent<CustomPlaceToolController>();
                var placeTool = _seaTreaderDoll.AddComponent<GenericPlaceTool>();
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seatreadericon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_seaTreaderDoll);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.8f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
