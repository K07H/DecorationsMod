#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Crafting;
using static CraftData;
#else
using SMLHelper.V2.Crafting;
#endif
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.ExistingItems
{
    public class ArcadeGorgetoy : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public ArcadeGorgetoy() : base(
            new PrefabInfo("7ea4a91e-80fc-43aa-8ce3-5d52bd19e278", "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_arcade_gorgetoy_01.prefab", TechType.ArcadeGorgetoy)
            )
        {
#else
        public ArcadeGorgetoy() // Feeds abstract class
        {
            this.ClassID = "7ea4a91e-80fc-43aa-8ce3-5d52bd19e278";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_arcade_gorgetoy_01.prefab";

            this.TechType = TechType.ArcadeGorgetoy;

#endif
            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA
#if SUBNAUTICA_NAUTILUS
            this.Recipe = new RecipeData()
#else
            this.Recipe = new TechData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.FiberMesh, 2)
                    }),
            };
#else
            this.Recipe = new RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.FiberMesh, 2)
                    }),
            };
#endif
        }

        private static GameObject _arcadeGorgetoy = null;

        public override GameObject GetGameObject()
        {
            if (_arcadeGorgetoy == null)
                _arcadeGorgetoy = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            Logger.Debug("ArcadeGorgetoy prefab is " + (_arcadeGorgetoy == null ? "NULL" : "NOT NULL"));

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_arcadeGorgetoy);

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
