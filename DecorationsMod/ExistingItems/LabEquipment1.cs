using System.Collections.Generic;
using UnityEngine;
using SMLHelper.V2.Crafting;

namespace DecorationsMod.ExistingItems
{
    public class LabEquipment1 : DecorationItem
    {
        public LabEquipment1() // Feeds abstract class
        {
            this.ClassID = "2cee55bc-6136-47c5-a1ed-14c8f3203856";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_01";

            this.TechType = TechType.LabEquipment1;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechData(new List<Ingredient>(2)
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.Glass, 1)
            });
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("discovery_lab_props_01").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
            fabricating.scaleFactor = 0.75f;

            return prefab;
        }
    }
}
