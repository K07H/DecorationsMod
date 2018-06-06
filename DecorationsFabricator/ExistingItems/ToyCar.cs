using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class ToyCar : DecorationItem
    {
        public ToyCar() // Feeds abstract class
        {
            this.ClassID = "dfabc84e-c4c5-45d9-8b01-ca0eaeeb8e65";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/Goldglove_car_02";

            this.TechType = TechType.ToyCar;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.Glass, 1),
                        new IngredientHelper(TechType.Silicone, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.04f;
            fabricating.localMaxY = 0.25f;
            fabricating.posOffset = new Vector3(-0.05f, 0f, -0.06f);
            fabricating.eulerOffset = new Vector3(0f, 90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
