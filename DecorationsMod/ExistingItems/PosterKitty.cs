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
    public class PosterKitty : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public PosterKitty() : base(
            new PrefabInfo("d809cb15-6784-4f7c-bf5d-f7d0c5bf8546", "WorldEntities/Environment/Wrecks/poster_kitty.prefab", TechType.PosterExoSuit2)
            )
        {
            this.SetGameObject(this.GetGameObject);
#else
        public PosterKitty() // Feeds abstract class
        {
            this.ClassID = "d809cb15-6784-4f7c-bf5d-f7d0c5bf8546";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Environment/Wrecks/poster_kitty.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/poster_kitty.prefab";
#endif

            this.TechType = TechType.PosterKitty;
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

        private static GameObject _posterKitty = null;

        public override GameObject GetGameObject()
        {
            if (_posterKitty == null)
                _posterKitty = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_posterKitty);

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
