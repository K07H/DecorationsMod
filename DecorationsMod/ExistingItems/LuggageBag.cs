using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class LuggageBag : DecorationItem
    {
        public LuggageBag() // Feeds abstract class
        {
            this.ClassID = "3616e7f3-5079-443d-85b4-9ad68fcbd924";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4.prefab";

            this.TechType = TechType.LuggageBag;

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.FiberMesh, 2),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Silicone, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.FiberMesh, 2),
                        new Ingredient(TechType.Silicone, 1)
                    }),
            };
#endif
        }

        private static GameObject _luggageBag = null;

        public override GameObject GetGameObject()
        {
            if (_luggageBag == null)
                _luggageBag = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_luggageBag);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.7f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.8f;

            return prefab;
        }
    }
}
