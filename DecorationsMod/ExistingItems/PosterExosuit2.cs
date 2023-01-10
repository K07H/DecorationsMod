using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class PosterExosuit2 : DecorationItem
    {
        public PosterExosuit2() // Feeds abstract class
        {
            this.ClassID = "d76dd251-492d-4bf9-8adb-25e59d709df2";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Environment/Wrecks/poster_exosuit_02.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/poster_exosuit_02.prefab";
#endif

            this.TechType = TechType.PosterExoSuit2;

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.FiberMesh, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.FiberMesh, 1)
                    }),
            };
#endif
        }

        private static GameObject _posterExosuit2 = null;

        public override GameObject GetGameObject()
        {
            if (_posterExosuit2 == null)
                _posterExosuit2 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_posterExosuit2);

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
