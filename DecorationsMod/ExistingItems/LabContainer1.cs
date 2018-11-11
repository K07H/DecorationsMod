using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class LabContainer1 : DecorationItem
    {
        public LabContainer1() // Feeds abstract class
        {
            this.ClassID = "e7f9c5e7-3906-4efd-b239-28783bce17a5";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_01";

            this.TechType = TechType.LabContainer;

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 2)
                    }),
            };
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("biodome_lab_containers_close_01").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.80f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
