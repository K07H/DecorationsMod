using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class LabEquipment3 : DecorationItem
    {
        public LabEquipment3() // Feeds abstract class
        {
            this.ClassID = "3fd9050b-4baf-4a78-a883-e774c648887c";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_03";

            this.TechType = TechType.LabEquipment3;

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[2]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1)
                    }),
            };
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

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
