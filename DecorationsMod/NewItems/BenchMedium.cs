using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class BenchMedium : DecorationItem
    {
        [SetsRequiredMembers]
        public BenchMedium() : base("BenchMedium", LanguageHelper.GetFriendlyWord("BenchMediumName"), LanguageHelper.GetFriendlyWord("BenchDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("benchmediumicon"))
        {
            // Feed DecortionItem interface
            this.ClassID = "BenchMedium";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.BenchMedium = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

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

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType, TechType.Bench);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("benchmediumicon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _bench = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: bench.GetGameObject()");
#endif
            if (_bench == null)
                _bench = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Bench.prefab");

            GameObject prefab = GameObject.Instantiate(_bench);

            prefab.name = this.ClassID;

            // Modify tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Modify prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Add large world entity
            var lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Modify box colliders
            var collider = prefab.FindChild("Collider").GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x * 0.6f, collider.size.y, collider.size.z);
            var builderTrigger = prefab.FindChild("Builder Trigger").GetComponent<BoxCollider>();
            builderTrigger.size = new Vector3(builderTrigger.size.x * 0.6f, builderTrigger.size.y, builderTrigger.size.z);

            // Retrieve model node
            GameObject model = prefab.FindChild("model");

            // Move bench parts
            GameObject benchStart = model.FindChild("Bench_01_start");
            benchStart.transform.localPosition = new Vector3(-0.408f, benchStart.transform.localPosition.y, benchStart.transform.localPosition.z);
            GameObject benchEnd = model.FindChild("Bench_01_end");
            benchEnd.transform.localPosition = new Vector3(0.408f, benchEnd.transform.localPosition.y, benchEnd.transform.localPosition.z);
            GameObject benchTile = model.FindChild("Bench_01_tile");
            benchTile.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f); //new Vector3(99.7f, 99.7f, 99.7f);

            // Update sky applier
            PrefabsHelper.UpdateOrAddSkyApplier(prefab);

            // Update contructable
            var constructible = prefab.GetComponent<Constructable>();
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = false;
            constructible.controlModelState = true;
            constructible.deconstructionAllowed = true;
            constructible.rotationEnabled = true;
            constructible.model = model;
            constructible.techType = this.TechType;
            constructible.placeMinDistance = 0.6f;
            constructible.enabled = true;

            // Update constructable bounds
            var constructableBounds = prefab.GetComponent<ConstructableBounds>();
            constructableBounds.bounds = new OrientedBounds(new Vector3(constructableBounds.bounds.position.x, constructableBounds.bounds.position.y, constructableBounds.bounds.position.z),
                new Quaternion(constructableBounds.bounds.rotation.x, constructableBounds.bounds.rotation.y, constructableBounds.bounds.rotation.z, constructableBounds.bounds.rotation.w),
                new Vector3(constructableBounds.bounds.extents.x * 0.6f, constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.z));

            return prefab;
        }
    }
}
