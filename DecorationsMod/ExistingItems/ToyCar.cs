using System.Collections.Generic;
using UnityEngine;
using SMLHelper.V2.Crafting;

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

            this.Recipe = new TechData(new List<Ingredient>(3)
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1),
                new Ingredient(TechType.Silicone, 1)
            });
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
