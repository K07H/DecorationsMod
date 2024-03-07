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
    public class PosterExosuit1 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public PosterExosuit1() : base(
            new PrefabInfo("336f276f-9546-40d0-98cb-974994dee3bf", "WorldEntities/Environment/Wrecks/poster_exosuit_01.prefab", TechType.PosterExoSuit1)
            )
        {
            this.SetGameObject(this.GetGameObject);
#else
        public PosterExosuit1() // Feeds abstract class
        {
            this.ClassID = "336f276f-9546-40d0-98cb-974994dee3bf";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Environment/Wrecks/poster_exosuit_01.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/poster_exosuit_01.prefab";
#endif

            this.TechType = TechType.PosterExoSuit1;
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

        private static GameObject _posterExosuit1 = null;

        public override GameObject GetGameObject()
        {
            if (_posterExosuit1 == null)
                _posterExosuit1 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_posterExosuit1);

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
