using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsFabricator.ExistingItems
{
    public class LuggageBag : DecorationItem
    {
        public LuggageBag() // Feeds abstract class
        {
            this.ClassID = "3616e7f3-5079-443d-85b4-9ad68fcbd924";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4";

            this.TechType = TechType.LuggageBag;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[2]
                    {
                        new IngredientHelper(TechType.FiberMesh, 2),
                        new IngredientHelper(TechType.Silicone, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

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
