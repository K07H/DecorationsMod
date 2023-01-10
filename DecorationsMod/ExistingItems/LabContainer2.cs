using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class LabContainer2 : DecorationItem
    {
        public LabContainer2() // Feeds abstract class
        {
            this.ClassID = "e3e00261-92fc-4f52-bad2-4f0e5802a43d";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_02.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/biodome_lab_containers_close_02.prefab";
#endif

            this.TechType = TechType.LabContainer2;

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
#endif
        }

        private static GameObject _labContainer2 = null;

        public override GameObject GetGameObject()
        {
            if (_labContainer2 == null)
                _labContainer2 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_labContainer2);

            // Add fabricating animation
#if SUBNAUTICA
            var fabricating = prefab.FindChild("biodome_lab_containers_close_02").AddComponent<VFXFabricating>();
#else
            var fabricating = prefab.FindChild("biodome_lab_containers_close_02 1").AddComponent<VFXFabricating>();
#endif
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.50f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
