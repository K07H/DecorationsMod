using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class WarperPart10 : DecorationItem
    {
        [SetsRequiredMembers]
        public WarperPart10() : base("WarperPart10", LanguageHelper.GetFriendlyWord("WarperPartName") + " (6)", LanguageHelper.GetFriendlyWord("WarperPartDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("warper_icon_10"))
        {
            this.ClassID = "WarperPart10";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("warper_part_10");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.WarperPart10 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Silicone, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _warperPart10 = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_warperPart10 == null)
                    _warperPart10 = AssetsHelper.Assets.LoadAsset<GameObject>("warper_part_10");

                GameObject model = _warperPart10.FindChild("Model");

                // Translate model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.06f, model.transform.localPosition.z);

                // Set tech tag
                var techTag = _warperPart10.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _warperPart10.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;

                // Add collider
                var collider = _warperPart10.AddComponent<BoxCollider>();
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
                var renderers = _warperPart10.GetComponentsInChildren<Renderer>();
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
                PrefabsHelper.SetDefaultLargeWorldEntity(_warperPart10);

                // Add sky applier
                PrefabsHelper.UpdateOrAddSkyApplier(_warperPart10, null, renderers);

                // We can pick this item
                PrefabsHelper.SetDefaultPickupable(_warperPart10);

                // We can place this item
                _warperPart10.AddComponent<CustomPlaceToolController>();
                var placeTool = _warperPart10.AddComponent<GenericPlaceTool>();
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
                placeTool.ghostModelPrefab = _warperPart10;
                placeTool.mainCollider = collider;
                placeTool.pickupable = _warperPart10.GetComponent<Pickupable>();
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("warper_icon_10"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_warperPart10);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.3f;
            fabricatingA.posOffset = new Vector3(0.01f, 0.01f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
