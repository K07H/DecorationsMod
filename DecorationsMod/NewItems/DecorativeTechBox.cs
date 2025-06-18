using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class DecorativeTechBox : DecorationItem
    {
        [SetsRequiredMembers]
        public DecorativeTechBox() : base("DecorativeTechBox", LanguageHelper.GetFriendlyWord("DecorativeTechBoxName"), LanguageHelper.GetFriendlyWord("DecorativeTechBoxDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("techboxicon2")) // Feeds abstract class
        {
            this.ClassID = "DecorativeTechBox"; // 4f045c69-1539-4c53-b157-767df47c1aa6
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add to the custom buidables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("techboxicon2"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _decorativeTechBox = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: decorativeTechBox.GetGameObject()");
#endif
            if (_decorativeTechBox == null)
                _decorativeTechBox = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Starship_tech_box_01_01.prefab");

            GameObject prefab = GameObject.Instantiate(_decorativeTechBox);
            prefab.name = this.ClassID;

            // Get model
            GameObject model = prefab.FindChild("Starship_tech_box_01_01");

            // Add compatibility with SnapBuilder
            PrefabsHelper.SnapBuilderCompatibility(model.transform, new Vector3(-90f, -90f, 0f));

            // Remove rigid body
            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            if (rb != null)
                GameObject.DestroyImmediate(rb);

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Add tech tag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Set as constructible
            Constructable constructible = prefab.AddComponent<Constructable>();
            constructible.techType = this.TechType;
            constructible.allowedOnWall = true;
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = true;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = true;
            constructible.rotationEnabled = true;
            constructible.deconstructionAllowed = true;
            constructible.controlModelState = true;
            constructible.surfaceType = VFXSurfaceTypes.electronic;
            constructible.model = model;
            constructible.placeMinDistance = 0.6f;

            // Add constructable bounds
            prefab.AddComponent<ConstructableBounds>();

            return prefab;
        }
    }
}
