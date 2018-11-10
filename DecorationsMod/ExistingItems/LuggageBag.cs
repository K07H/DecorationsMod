using System.Collections.Generic;
using UnityEngine;
using SMLHelper.V2.Crafting;

namespace DecorationsMod.ExistingItems
{
    public class LuggageBag : DecorationItem
    {
        public LuggageBag() // Feeds abstract class
        {
            this.ClassID = "3616e7f3-5079-443d-85b4-9ad68fcbd924";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4";

            this.TechType = TechType.LuggageBag;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechData(new List<Ingredient>(2)
            {
                new Ingredient(TechType.FiberMesh, 2),
                new Ingredient(TechType.Silicone, 1)
            });
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.7f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.8f;

            return prefab;
        }
    }
}
