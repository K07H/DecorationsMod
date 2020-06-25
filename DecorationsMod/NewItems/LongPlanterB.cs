using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class LongPlanterB : DecorationItem
    {
        public LongPlanterB() // Feeds abstract class
        {
            this.ClassID = "LongPlanterB"; // 87f5d3e6-e00b-4cf3-be39-0a9c7e951b84

            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = Resources.Load<GameObject>("Submarine/Build/FarmingTray");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("ExteriorLongPlanterName"),
                                                        LanguageHelper.GetFriendlyWord("ExteriorLongPlanterDescription"),
                                                        true);

            CrafterLogicFixer.LongPlanterB = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 2)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 2)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add to the custom buidables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.ExteriorModules, TechCategory.ExteriorOther, this.TechType, TechType.FarmingTray);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom icon
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("farmingtrayicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

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
            SkyApplier sa = prefab.GetComponent<SkyApplier>();
            if (sa == null)
                sa = prefab.GetComponentInChildren<SkyApplier>();
            if (sa == null)
                sa = prefab.AddComponent<SkyApplier>();
            sa.renderers = prefab.GetComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;

            return prefab;
        }
    }
}
