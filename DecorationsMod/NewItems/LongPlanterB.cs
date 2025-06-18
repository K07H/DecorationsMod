using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class LongPlanterB : DecorationItem
    {
        [SetsRequiredMembers]
        public LongPlanterB() : base("LongPlanterB", LanguageHelper.GetFriendlyWord("ExteriorLongPlanterName"), LanguageHelper.GetFriendlyWord("ExteriorLongPlanterDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("farmingtrayicon")) // Feeds abstract class
        {
            this.ClassID = "LongPlanterB"; // 87f5d3e6-e00b-4cf3-be39-0a9c7e951b84

            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.LongPlanterB = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

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
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.ExteriorModules, TechCategory.ExteriorOther, this.TechType, TechType.FarmingTray);

                // Set the buildable prefab
                this.Register();

                // Set the custom icon
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("farmingtrayicon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _longPlanterB = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: longPlanterB.GetGameObject()");
#endif
            if (_longPlanterB == null)
                _longPlanterB = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/FarmingTray.prefab");

            GameObject prefab = GameObject.Instantiate(_longPlanterB);

            prefab.name = this.ClassID;

            // Update TechTag
            TechTag techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab ID
            PrefabIdentifier prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update StorageContainer
            StorageContainer sc = prefab.GetComponent<StorageContainer>();
            sc.height = 2;

            // Update Planter
            var planter = prefab.GetComponent<Planter>();
            if (planter != null)
            {
                planter.isIndoor = false;
                planter.environment = Planter.PlantEnvironment.Water;
            }

            // Update constructable
            Constructable constructable = prefab.GetComponent<Constructable>();
            constructable.allowedOutside = true;
            constructable.allowedInBase = ConfigSwitcher.AllowOutdoorLongPlanterInside;
            constructable.allowedInSub = ConfigSwitcher.AllowOutdoorLongPlanterInside;
            constructable.placeMinDistance = 0.8f;

            // Update constructable bounds
            ConstructableBounds bounds = prefab.GetComponent<ConstructableBounds>();
            bounds.bounds.extents = new Vector3(bounds.bounds.extents.x * 0.3f, bounds.bounds.extents.y * 0.4f, bounds.bounds.extents.z);
            bounds.bounds.position = new Vector3(bounds.bounds.position.x + 0.3f, bounds.bounds.position.y * 0.4f, bounds.bounds.position.z);

            float xPad = 1.22f; //0.5f;
            float yPad = -0.25f;
            float scaleRatio = 0.4f;
            float heightScaleRatio = 0.5f;

            // Update box collider
            GameObject objectTrigger = prefab.FindChild("collider");
            BoxCollider objectCollider = objectTrigger.GetComponent<BoxCollider>();
            objectCollider.size = new Vector3(objectCollider.size.x * scaleRatio, objectCollider.size.y * heightScaleRatio, objectCollider.size.z);
            //objectCollider.center = new Vector3(objectCollider.center.x + pad, objectCollider.center.y, objectCollider.center.z);

            // Update model
            GameObject model = prefab.FindChild("model");
            model.transform.localScale = new Vector3(model.transform.localScale.x * scaleRatio, model.transform.localScale.y * heightScaleRatio, model.transform.localScale.z);
            //model.transform.localPosition = new Vector3(model.transform.localPosition.x + pad, model.transform.localPosition.y, model.transform.localPosition.z);
            //model.transform.localRotation = new Quaternion(model.transform.localRotation.x, model.transform.localRotation.y + 20.0f, model.transform.localRotation.z, model.transform.localRotation.w);

            GameObject slots = prefab.FindChild("slots");
            foreach (Transform tr in slots.transform)
                tr.localPosition = new Vector3(tr.localPosition.x + xPad, tr.localPosition.y + yPad, tr.localPosition.z);
            GameObject slotsSmall = prefab.FindChild("slots_small");
            foreach (Transform tr in slotsSmall.transform)
                tr.localPosition = new Vector3(tr.localPosition.x + xPad, tr.localPosition.y + yPad, tr.localPosition.z);

            // Update sky applier
            PrefabsHelper.UpdateOrAddSkyApplier(prefab);

            return prefab;
        }
    }
}
