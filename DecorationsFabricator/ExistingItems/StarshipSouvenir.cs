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
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir";

            this.TechType = TechType.StarshipSouvenir;

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
