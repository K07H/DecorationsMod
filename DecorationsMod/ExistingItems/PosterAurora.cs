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
    public class PosterAurora : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public PosterAurora() : base(
            new PrefabInfo("876cbea4-b4bf-4311-8264-5118bfef291c", "WorldEntities/Environment/Wrecks/poster_aurora.prefab", TechType.PosterAurora)
            )
        {
            this.SetGameObject(this.GetGameObject);
#else
        public PosterAurora() // Feeds abstract class
        {
            this.ClassID = "876cbea4-b4bf-4311-8264-5118bfef291c";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Environment/Wrecks/poster_aurora.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/poster_aurora.prefab";
#endif

            this.TechType = TechType.PosterAurora;
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

        private static GameObject _posterAurora = null;
        
        public override GameObject GetGameObject()
        {
            if (_posterAurora == null)
                _posterAurora = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_posterAurora);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.5f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.4f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }
    }
}
