using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class ALongPlanter : DecorationItem
    {
        public ALongPlanter() // Feeds abstract class
        {
            this.ClassID = "ALongPlanter"; // 87f5d3e6-e00b-4cf3-be39-0a9c7e951b84

            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("LongPlanterName"),
                                                        LanguageHelper.GetFriendlyWord("LongPlanterDescription"),
                                                        true);

            CrafterLogicFixer.LongPlanterA = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 2)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 2)
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
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType, TechType.PlanterBox);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom icon
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("longplanterbox"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _longPlanterA = null;

        public override GameObject GetGameObject()
        {
            if (_longPlanterA == null)
                _longPlanterA = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/PlanterBox.prefab");

            GameObject prefab = GameObject.Instantiate(_longPlanterA);

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

            // Update constructable
            Constructable constructable = prefab.GetComponent<Constructable>();
            constructable.placeMinDistance = 0.8f;
            if (ConfigSwitcher.AllowIndoorLongPlanterOutside)
                constructable.allowedOutside = true;

            // Update constructable bounds
            ConstructableBounds bounds = prefab.GetComponent<ConstructableBounds>();
            bounds.bounds.extents = new Vector3(bounds.bounds.extents.x * 0.5f, bounds.bounds.extents.y, bounds.bounds.extents.z);
            bounds.bounds.position = new Vector3(bounds.bounds.position.x + 0.5f, bounds.bounds.position.y, bounds.bounds.position.z);

            // Update box collider
            GameObject builderTrigger = prefab.FindChild("Builder Trigger");
            GameObject objectTrigger = prefab.FindChild("Collider");
            BoxCollider builderCollider = builderTrigger.GetComponent<BoxCollider>();
            BoxCollider objectCollider = objectTrigger.GetComponent<BoxCollider>();
            builderCollider.size = new Vector3(builderCollider.size.x * 0.5f, builderCollider.size.y, builderCollider.size.z);
            objectCollider.size = new Vector3(objectCollider.size.x * 0.5f, objectCollider.size.y, objectCollider.size.z);
            builderCollider.center = new Vector3(builderCollider.center.x + 0.5f, builderCollider.center.y, builderCollider.center.z);
            objectCollider.center = new Vector3(objectCollider.center.x + 0.5f, objectCollider.center.y, objectCollider.center.z);

            // Update model
            GameObject model = prefab.FindChild("model");
            model.transform.localScale = new Vector3(model.transform.localScale.x * 0.5f, model.transform.localScale.y, model.transform.localScale.z);
            model.transform.localPosition = new Vector3(model.transform.localPosition.x + 0.5f, model.transform.localPosition.y, model.transform.localPosition.z);
            //model.transform.localRotation = new Quaternion(model.transform.localRotation.x, model.transform.localRotation.y + 20.0f, model.transform.localRotation.z, model.transform.localRotation.w);

            // Update grass
            GameObject tray = model.FindChild("Base_interior_Planter_Tray_01");
            GameObject grass1 = tray.FindChild("pot_generic_plant_03");
            GameObject grass2 = tray.FindChild("pot_generic_plant_04");
            grass1.GetComponent<MeshRenderer>().enabled = false;
            grass2.GetComponent<MeshRenderer>().enabled = false;

            // Translate prefab
            //prefab.transform.localPosition = new Vector3(prefab.transform.localPosition.x + 0.6f, prefab.transform.localPosition.y, prefab.transform.localPosition.z);

            // Add compatibility with SnapBuilder
            //PrefabsHelper.SnapBuilderCompatibility(model.transform, new Vector3(0f, 7f, 0f));

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
