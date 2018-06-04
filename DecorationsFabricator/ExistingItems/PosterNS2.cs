using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsFabricator.ExistingItems
{
    public class PosterNS2 : DecorationItem
    {
        public PosterNS2() // Feeds abstract class
        {
            this.ClassID = "72da21f9-f3e2-4183-ac57-d3679fb09122";
            this.ResourcePath = "WorldEntities/Environment/Wrecks/Poster";

            this.TechType = TechType.Poster;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[2]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.FiberMesh, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.6f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.5f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }
    }
}
