using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsFabricator.ExistingItems
{
    public class PosterAurora : DecorationItem
    {
        public PosterAurora() // Feeds abstract class
        {
            this.ClassID = "876cbea4-b4bf-4311-8264-5118bfef291c";
            this.ResourcePath = "WorldEntities/Environment/Wrecks/poster_aurora";

            this.TechType = TechType.PosterAurora;

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
            fabricating.localMinY = -0.5f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.4f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }
    }
}
