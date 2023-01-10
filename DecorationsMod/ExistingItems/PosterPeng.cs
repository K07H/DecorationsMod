#if BELOWZERO
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class PosterPeng : DecorationItem
    {
        public PosterPeng() // Feeds abstract class
        {
            this.ClassID = "73756fda-6d9e-4ce6-8d13-ba05f8824b78";
            this.PrefabFileName = "WorldEntities/Alterra/Base/poster_peng.prefab";

            this.TechType = TechType.PosterSpyPenguin;

            this.GameObject = new GameObject(this.ClassID);

            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.FiberMesh, 1)
                    }),
            };
        }

        private static GameObject _posterPeng = null;

        public override GameObject GetGameObject()
        {
            if (_posterPeng == null)
                _posterPeng = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);
            
            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_posterPeng);

            /*
            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.6f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.5f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;
            */

            return prefab;
        }
    }
}
#endif