using System.Collections.Generic;
using UnityEngine;
using SMLHelper.V2.Crafting;

namespace DecorationsMod.ExistingItems
{
    public class ArcadeGorgetoy : DecorationItem
    {
        public ArcadeGorgetoy() // Feeds abstract class
        {
            this.ClassID = "7ea4a91e-80fc-43aa-8ce3-5d52bd19e278";
            this.ResourcePath = "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_arcade_gorgetoy_01";

            this.TechType = TechType.ArcadeGorgetoy;

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath);

            this.Recipe = new TechData(new List<Ingredient>(1)
            {
                new Ingredient(TechType.FiberMesh, 2)
            });
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_arcade_gorgetoy_01").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
