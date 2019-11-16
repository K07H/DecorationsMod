using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class LabContainer2 : DecorationItem
    {
        public LabContainer2() // Feeds abstract class
        {
            this.ClassID = "e3e00261-92fc-4f52-bad2-4f0e5802a43d";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_02";

            this.TechType = TechType.LabContainer2;

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("biodome_lab_containers_close_02").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.50f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
