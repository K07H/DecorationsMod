using System.Collections.Generic;
using UnityEngine;
using SMLHelper.V2.Crafting;

namespace DecorationsMod.ExistingItems
{
    public class LabContainer3 : DecorationItem
    {
        public LabContainer3() // Feeds abstract class
        {
            this.ClassID = "7f601dd4-0645-414d-bb62-5b0b62985836";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_tube_01";

            this.TechType = TechType.LabContainer3;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechData(new List<Ingredient>(1)
            {
                new Ingredient(TechType.Glass, 1)
            });
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("biodome_lab_containers_tube_01").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.36f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
