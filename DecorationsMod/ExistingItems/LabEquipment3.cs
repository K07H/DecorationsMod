using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class LabEquipment3 : DecorationItem
    {
        public LabEquipment3() // Feeds abstract class
        {
            this.ClassID = "3fd9050b-4baf-4a78-a883-e774c648887c";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_03.prefab";

            this.TechType = TechType.LabEquipment3;

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
#endif
        }

        private static GameObject _labEquipment3 = null;

        public override GameObject GetGameObject()
        {
            if (_labEquipment3 == null)
                _labEquipment3 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_labEquipment3);

            // Add fabricating animation
            var fabricating = prefab.FindChild("discovery_lab_props_03").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.75f;

            return prefab;
        }
    }
}
