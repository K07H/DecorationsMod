using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class StarshipSouvenir : DecorationItem
    {
        public StarshipSouvenir() // Feeds abstract class
        {
            this.ClassID = "c0d320d2-537e-4128-90ec-ab1466cfbbc3";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir";

            this.TechType = TechType.StarshipSouvenir;

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("starship_souvenir").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.5f;
            fabricating.posOffset = new Vector3(0f, 0.1f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
