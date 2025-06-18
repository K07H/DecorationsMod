using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class SeaEmperorDoll : DecorationItem
    {
        [SetsRequiredMembers]
        public SeaEmperorDoll() : base("SeaEmperorDoll", LanguageHelper.GetFriendlyWord("SmallEmperorName"), LanguageHelper.GetFriendlyWord("SmallEmperorDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("emperoricon")) // Feeds abstract class
        {
            this.ClassID = "SeaEmperorDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("seaemperor");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.SeaEmperorDoll = this.TechType;
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

        private static GameObject _seaEmperorDoll = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_seaEmperorDoll == null)
                    _seaEmperorDoll = AssetsHelper.Assets.LoadAsset<GameObject>("seaemperor");

                // Merge submeshes
                GameObject emperorModel = _seaEmperorDoll.FindChild("emperorleviathan");
                Mesh emperorMesh = emperorModel.GetComponent<MeshFilter>().mesh;
                emperorMesh.SetTriangles(emperorMesh.triangles, 0);
                emperorMesh.subMeshCount = 1;

                // Set tech tag
                var techTag = _seaEmperorDoll.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _seaEmperorDoll.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;

                // Delete rigid body to prevent bug
                var rb = _seaEmperorDoll.GetComponent<Rigidbody>();
                if (rb != null)
                    GameObject.DestroyImmediate(rb);

                // Add collider
                var collider = _seaEmperorDoll.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.5f, 0.5f, 0.4f);

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("Leviathan_01_01_normal");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Leviathan_01_02_normal");
                Texture spec1 = AssetsHelper.Assets.LoadAsset<Texture>("Leviathan_01_01_spec");
                Texture spec2 = AssetsHelper.Assets.LoadAsset<Texture>("Leviathan_01_02_spec");
                Texture illum1 = AssetsHelper.Assets.LoadAsset<Texture>("Leviathan_01_01_illum");
                Texture illum2 = AssetsHelper.Assets.LoadAsset<Texture>("Leviathan_01_02_illum");
                var renderers = _seaEmperorDoll.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                tmpMat.shader = marmosetUber;
                                if (string.Compare(tmpMat.name, "leviathan_01_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
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
                                else if (string.Compare(tmpMat.name, "Leviathan_01_02 (Instance)", true, CultureInfo.InvariantCulture) == 0)
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
                PrefabsHelper.UpdateOrAddSkyApplier(_seaEmperorDoll, null, renderers);

                // We can pick this item
                var pickupable = _seaEmperorDoll.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _seaEmperorDoll.AddComponent<CustomPlaceToolController>();
                var placeTool = _seaEmperorDoll.AddComponent<GenericPlaceTool>();
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("emperoricon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_seaEmperorDoll);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.6f;
            fabricatingA.posOffset = new Vector3(0.1f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 0.8f;

            return prefab;
        }
    }
}
