using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.ExistingItems
{
    public class LabEquipment1 : DecorationItem
    {
        public LabEquipment1() // Feeds abstract class
        {
            this.ClassID = "2cee55bc-6136-47c5-a1ed-14c8f3203856";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_01";

            this.TechType = TechType.LabEquipment1;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[2]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.Glass, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override GameObject GetPrefab()
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
