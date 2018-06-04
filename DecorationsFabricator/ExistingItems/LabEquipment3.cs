using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsFabricator.ExistingItems
{
    public class LabEquipment3 : DecorationItem
    {
        public LabEquipment3() // Feeds abstract class
        {
            this.ClassID = "3fd9050b-4baf-4a78-a883-e774c648887c";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_03";

            this.TechType = TechType.LabEquipment3;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[2]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.Glass, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("discovery_lab_props_03").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.75f;

            return prefab;
        }
    }
}
