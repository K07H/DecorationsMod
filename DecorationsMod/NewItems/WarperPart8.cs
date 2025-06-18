using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class WarperPart8 : DecorationItem
    {
        [SetsRequiredMembers]
        public WarperPart8() : base("WarperPart8", LanguageHelper.GetFriendlyWord("WarperPartName") + " (4)", LanguageHelper.GetFriendlyWord("WarperPartDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("warper_icon_8"))
        {
            this.ClassID = "WarperPart8";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("warper_part_8");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.WarperPart8 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Silicone, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Silicone, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _warperPart8 = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_warperPart8 == null)
                    _warperPart8 = AssetsHelper.Assets.LoadAsset<GameObject>("warper_part_8");

                GameObject model = _warperPart8.FindChild("Model");

                // Translate model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.08f, model.transform.localPosition.z);

                // Set tech tag
                var techTag = _warperPart8.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _warperPart8.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;

                // Add collider
                var collider = _warperPart8.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.3f, 0.1f, 0.2f);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.05f, collider.center.z);

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("precursor_lab_warper_normal");
                Texture spec1 = AssetsHelper.Assets.LoadAsset<Texture>("precursor_lab_warper_spec");
                Texture illum1 = AssetsHelper.Assets.LoadAsset<Texture>("precursor_lab_warper_illum");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("warper_normal");
                Texture spec2 = AssetsHelper.Assets.LoadAsset<Texture>("warper_spec");
                Texture illum2 = AssetsHelper.Assets.LoadAsset<Texture>("warper_illum");
                Texture normal3 = AssetsHelper.Assets.LoadAsset<Texture>("warper_entrails_normal");
                Texture spec3 = AssetsHelper.Assets.LoadAsset<Texture>("warper_entrails_spec");
                Texture illum3 = AssetsHelper.Assets.LoadAsset<Texture>("warper_entrails_illum");
                var renderers = _warperPart8.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers)
                    if (rend.materials.Length > 0)
                        foreach (Material tmpMat in rend.materials)
                        {
                            if (string.Compare(tmpMat.name, "precursor_lab_warper_tube_01 (Instance)", true, CultureInfo.InvariantCulture) != 0 && 
                                string.Compare(tmpMat.name, "precursor_lab_warper_tube_02 (Instance)", true, CultureInfo.InvariantCulture) != 0 &&
                                string.Compare(tmpMat.name, "precursor_lab_warper_liquid (Instance)", true, CultureInfo.InvariantCulture) != 0)
                            {
                                tmpMat.shader = marmosetUber;
                                if (string.Compare(tmpMat.name, "precursor_lab_warper (Instance)", true, CultureInfo.InvariantCulture) == 0 || 
                                    string.Compare(tmpMat.name, "precursor_lab_warper_box (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_SpecTex", spec1);
                                    tmpMat.SetTexture("_BumpMap", normal1);
                                    tmpMat.SetTexture("_Illum", illum1);
                                    tmpMat.SetFloat("_EmissionLM", 1f);

                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Warper (Instance)", true, CultureInfo.InvariantCulture) == 0 || 
                                    string.Compare(tmpMat.name, "Warper_alpha (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_SpecTex", spec2);
                                    tmpMat.SetTexture("_BumpMap", normal2);
                                    tmpMat.SetTexture("_Illum", illum2);
                                    tmpMat.SetFloat("_EmissionLM", 1f);

                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "warper_entrails (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_SpecTex", spec3);
                                    tmpMat.SetTexture("_BumpMap", normal3);
                                    tmpMat.SetTexture("_Illum", illum3);
                                    tmpMat.SetFloat("_EmissionLM", 1f);

                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                            }
                        }

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(_warperPart8);

                // Add sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_warperPart8, null, renderers);

                // We can pick this item
                PrefabsHelper.SetDefaultPickupable(_warperPart8);

                // We can place this item
                _warperPart8.AddComponent<CustomPlaceToolController>();
                var placeTool = _warperPart8.AddComponent<GenericPlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnBase = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.ghostModelPrefab = _warperPart8;
                placeTool.mainCollider = collider;
                placeTool.pickupable = _warperPart8.GetComponent<Pickupable>();
                placeTool.drawTime = 0.5f;
                placeTool.dropTime = 1;
                placeTool.holsterTime = 0.35f;

                // Define unlock conditions
                if (ConfigSwitcher.AddItemsWhenDiscovered)
                    Nautilus.Handlers.KnownTechHandler.SetAnalysisTechEntry(TechType.PrecursorLostRiverWarperParts, new TechType[] { this.TechType });

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add the new TechType to Hand Equipment type.
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("warper_icon_8"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_warperPart8);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.1f;
            fabricatingA.localMaxY = 0.5f;
            fabricatingA.posOffset = new Vector3(0.01f, 0.18f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(90f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
