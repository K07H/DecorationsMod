using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class DecorativeControlTerminal : DecorationItem
    {
        [SetsRequiredMembers]
        public DecorativeControlTerminal() : base("DecorativeControlTerminal", LanguageHelper.GetFriendlyWord("DecorativeControlTerminalName"), LanguageHelper.GetFriendlyWord("DecorativeControlTerminalDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("control_terminal_icon")) // Feeds abstract class
        {
            this.ClassID = "DecorativeControlTerminal"; // 6ca93e93-5209-4c27-ba60-5f68f36a95fb
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("control_terminal_icon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _controlTerminal = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: controlTerminal.GetGameObject()");
#endif
            if (_controlTerminal == null)
                _controlTerminal = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/control_terminal_01.prefab");

            GameObject prefab = GameObject.Instantiate(_controlTerminal);
            prefab.name = this.ClassID;

            // Get model
            GameObject model = prefab.FindChild("Starship_control_terminal_01");

            // Add compatibility with SnapBuilder
            PrefabsHelper.SnapBuilderCompatibility(model.transform, new Vector3(-90f, -90f, 0f));

            // Remove rigid body
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Add tech tag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Ajust collider
            BoxCollider[] cs = prefab.GetComponentsInChildren<BoxCollider>();
            if (cs != null)
                foreach (BoxCollider c in cs)
                    c.size *= 0.9f;

            // Set as constructible
            Constructable constructible = prefab.AddComponent<Constructable>();
            constructible.techType = this.TechType;
            constructible.allowedOnWall = true;
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = true;
            constructible.rotationEnabled = true;
            constructible.deconstructionAllowed = true;
            constructible.controlModelState = true;
            constructible.surfaceType = VFXSurfaceTypes.electronic;
            constructible.model = model;
            constructible.placeMinDistance = 0.6f;

            // Add constructable bounds
            ConstructableBounds cb = prefab.AddComponent<ConstructableBounds>();
            cb.bounds.size *= 0.9f;

            return prefab;
        }
    }
}