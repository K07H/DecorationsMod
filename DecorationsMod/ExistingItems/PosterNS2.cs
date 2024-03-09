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
    public class PosterNS2 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public PosterNS2() : base(
            new PrefabInfo("72da21f9-f3e2-4183-ac57-d3679fb09122", "WorldEntities/Environment/Wrecks/Poster.prefab", TechType.Poster)
            )
        {
#else
        public PosterNS2() // Feeds abstract class
        {
            this.ClassID = "72da21f9-f3e2-4183-ac57-d3679fb09122";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Environment/Wrecks/Poster.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/Poster.prefab";
#endif

            this.TechType = TechType.Poster;
#endif

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[2]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.FiberMesh, 1)
                    }),
            };
        }

        private static GameObject _posterNS2 = null;

        public override GameObject GetGameObject()
        {
            if (_posterNS2 == null)
                _posterNS2 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_posterNS2);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.6f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.5f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }
    }
}
