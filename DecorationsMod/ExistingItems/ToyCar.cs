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
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/Goldglove_car_02";

            this.TechType = TechType.ToyCar;

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[3]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Silicone, 1)
                    }),
            };
        }

        public override GameObject GetGameObject()
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
