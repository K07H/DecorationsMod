using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class Cap1 : DecorationItem
    {
        public Cap1() // Feeds abstract class
        {
            this.ClassID = "5884d27a-8798-4f09-82ec-c7671a604504";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_02.prefab";

            this.TechType = TechType.Cap1;

            this.GameObject = new GameObject(this.PrefabFileName);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.FiberMesh, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.FiberMesh, 1)
                    }),
            };
#endif
        }

        private static GameObject _cap1 = null;

        public override GameObject GetGameObject()
        {
            if (_cap1 == null)
                _cap1 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            Logger.Log("DEBUG: Cap1 prefab is " + (_cap1 == null ? "NULL" : "NOT NULL"));

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_cap1);

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
