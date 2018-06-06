using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class Cap1 : DecorationItem
    {
        public Cap1() // Feeds abstract class
        {
            this.ClassID = "5884d27a-8798-4f09-82ec-c7671a604504";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_02";

            this.TechType = TechType.Cap1;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1]
                    {
                        new IngredientHelper(TechType.FiberMesh, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_plaza_shelf_cap_02").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.06f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
