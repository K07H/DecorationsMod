using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class WarperPart1 : DecorationItem
    {
        [SetsRequiredMembers]
        public WarperPart1() : base("WarperPart1", LanguageHelper.GetFriendlyWord("BigWarperPartName"), LanguageHelper.GetFriendlyWord("BigWarperPartDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("warper_icon_1")) // Feeds abstract class
        {
            this.ClassID = "WarperPart1";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("Precursor_LostRiverBase_Warper");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.WarperPart1 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2),
                new Ingredient(TechType.Silicone, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Lubricant, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2),
                new Ingredient(TechType.Silicone, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Lubricant, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        private static GameObject _warperPart1 = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_warperPart1 == null)
                    _warperPart1 = AssetsHelper.Assets.LoadAsset<GameObject>("Precursor_LostRiverBase_Warper");

                // Get objects
                GameObject model = _warperPart1.FindChild("Model");

                // Add compatibility with SnapBuilder
                PrefabsHelper.SnapBuilderCompatibility(model.transform, new Vector3(-90f, -164.5f, 0f));

                // Rotate model
                //model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x - 90f, model.transform.localEulerAngles.y, model.transform.localEulerAngles.z);

                // Move model
                //model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.032f, model.transform.localPosition.z);

                // Apply shaders and materials
                var renderers = _warperPart1.GetAllComponentsInChildren<Renderer>();
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("precursor_lab_warper_normal");
                Texture spec1 = AssetsHelper.Assets.LoadAsset<Texture>("precursor_lab_warper_spec");
                Texture illum1 = AssetsHelper.Assets.LoadAsset<Texture>("precursor_lab_warper_illum");
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                if (string.Compare(tmpMat.name, "precursor_lab_warper_liquid (Instance)", true, CultureInfo.InvariantCulture) != 0 && !tmpMat.name.StartsWith("precursor_lab_warper_tube_", true, CultureInfo.InvariantCulture))
                                {
                                    tmpMat.shader = marmosetUber;
                                    if (string.Compare(tmpMat.name, "precursor_lab_warper (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_SpecTex", spec1);
                                        tmpMat.SetTexture("_BumpMap", normal1);
                                        tmpMat.SetTexture("_Illum", illum1);
                                        tmpMat.SetFloat("_EmissionLM", 1f);

                                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                        tmpMat.EnableKeyword("_ZWRITE_ON");
                                    }
                                    else if (string.Compare(tmpMat.name, "precursor_lab_warper_box (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                    {
                                        tmpMat.SetTexture("_SpecTex", spec1);
                                        tmpMat.SetTexture("_BumpMap", normal1);
                                        tmpMat.SetTexture("_Illum", illum1);
                                        tmpMat.SetFloat("_EmissionLM", 1f);

                                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                        tmpMat.EnableKeyword("MARMO_EMISSION");
                                        tmpMat.EnableKeyword("_ZWRITE_ON");
                                    }
                                }
                            }
                        }
                    }
                }

                // Add prefab identifier
                var prefabId = _warperPart1.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(_warperPart1);

                // Add tech tag
                var techTag = _warperPart1.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add collider
                var collider = _warperPart1.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.0075f, 0.0075f, 0.038f);
                collider.center = new Vector3(collider.center.x, collider.center.y, collider.center.z + 0.019f);

                // Add sky applier
                BaseModuleLighting bml = _warperPart1.GetComponent<BaseModuleLighting>();
                if (bml == null)
                    bml = _warperPart1.GetComponentInChildren<BaseModuleLighting>();
                if (bml == null)
                    bml = _warperPart1.EnsureComponent<BaseModuleLighting>();
                PrefabsHelper.UpdateOrAddSkyApplier(_warperPart1, null, renderers);

                // Add contructable
                Constructable constructable = _warperPart1.AddComponent<Constructable>();
                constructable.allowedInBase = true;
                constructable.allowedInSub = true;
                constructable.allowedOnCeiling = true;
                constructable.allowedOnWall = true;
                constructable.allowedOnConstructables = true;
                constructable.allowedOutside = false;
                constructable.allowedOnGround = false;
                constructable.allowedUnderwater = true;
                constructable.controlModelState = true;
                constructable.deconstructionAllowed = true;
                constructable.rotationEnabled = true;
                constructable.model = model;
                constructable.techType = this.TechType;
                constructable.placeMinDistance = 0.6f;
                constructable.enabled = true;

                // Add constructable bounds
                ConstructableBounds bounds = _warperPart1.AddComponent<ConstructableBounds>();
                //bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.032f, bounds.bounds.position.z);

                // Add warper specimen controller
                var warperSpecimenController = _warperPart1.AddComponent<WarperSpecimenController>();

                // Define unlock conditions
                if (ConfigSwitcher.AddItemsWhenDiscovered)
                    Nautilus.Handlers.KnownTechHandler.SetAnalysisTechEntry(TechType.PrecursorWarper, new TechType[] { this.TechType });

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("warper_icon_1"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_warperPart1);

            prefab.name = this.ClassID;

            return prefab;
        }
    }
}
