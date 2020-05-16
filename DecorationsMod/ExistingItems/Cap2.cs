using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class Cap2 : DecorationItem
    {
        public Cap2() // Feeds abstract class
        {
            this.ClassID = "3dc40631-2945-4109-acdc-823a9a0a8646";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_03";

            this.TechType = TechType.Cap2;

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.FiberMesh, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.FiberMesh, 1)
                    }),
            };
#endif
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_plaza_shelf_cap_03").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.06f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
