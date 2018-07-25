using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class ALongPlanter : DecorationItem
    {
        public ALongPlanter() // Feeds abstract class
        {
            this.ClassID = "ALongPlanter"; // 87f5d3e6-e00b-4cf3-be39-0a9c7e951b84

            this.ResourcePath = "Submarine/Build/PlanterBox";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("LongPlanterName"),
                                                        LanguageHelper.GetFriendlyWord("LongPlanterDescription"),
                                                        true);

            this.IsHabitatBuilder = true;

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1]
                    {
                        new IngredientHelper(TechType.Titanium, 2)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Add to the custom buidables
                CraftDataPatcher.customBuildables.Add(this.TechType);
                CraftDataPatcher.AddToCustomGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, $"{DecorationItem.DefaultResourcePath}{this.ClassID}", this.TechType, this.GetPrefab));

                // Set the custom icon
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, new Atlas.Sprite(ImageUtils.LoadTextureFromFile("./QMods/DecorationsMod/Assets/longplanterbox.png"))));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;
            
            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;
            
            // Update StorageContainer
            StorageContainer sc = prefab.GetComponent<StorageContainer>();
            sc.height = 2;
            
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
            model.transform.localRotation = new Quaternion(model.transform.localRotation.x, model.transform.localRotation.y + 20.0f, model.transform.localRotation.z, model.transform.localRotation.w);

            // Update grass
            GameObject tray = model.FindChild("Base_interior_Planter_Tray_01");
            GameObject grass1 = tray.FindChild("pot_generic_plant_03");
            GameObject grass2 = tray.FindChild("pot_generic_plant_04");
            grass1.GetComponent<MeshRenderer>().enabled = false;
            grass2.GetComponent<MeshRenderer>().enabled = false;

            // Translate prefab
            //prefab.transform.localPosition = new Vector3(prefab.transform.localPosition.x + 0.6f, prefab.transform.localPosition.y, prefab.transform.localPosition.z);

            return prefab;
        }
    }
}
