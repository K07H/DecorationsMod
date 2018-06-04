using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsFabricator.ExistingItems
{
    public class PosterExosuit1 : DecorationItem
    {
        public PosterExosuit1() // Feeds abstract class
        {
            this.ClassID = "336f276f-9546-40d0-98cb-974994dee3bf";
            this.ResourcePath = "WorldEntities/Environment/Wrecks/poster_exosuit_01";

            this.TechType = TechType.PosterExoSuit1;

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
