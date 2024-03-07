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
    public class StarshipSouvenir : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public StarshipSouvenir() : base(
            new PrefabInfo("c0d320d2-537e-4128-90ec-ab1466cfbbc3", "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir.prefab", TechType.StarshipSouvenir)
            )
        {
            this.SetGameObject(this.GetGameObject);
#else
        public StarshipSouvenir() // Feeds abstract class
        {
            this.ClassID = "c0d320d2-537e-4128-90ec-ab1466cfbbc3";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/starship_souvenir.prefab";
#endif

            this.TechType = TechType.StarshipSouvenir;
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
                        new Ingredient(TechType.Glass, 1)
                    }),
            };
        }

        private static GameObject _starshipSouvenir = null;

        public override GameObject GetGameObject()
        {
            if (_starshipSouvenir == null)
                _starshipSouvenir = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_starshipSouvenir);

            // Add fabricating animation
            var fabricating = prefab.FindChild("starship_souvenir").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.5f;
            fabricating.posOffset = new Vector3(0f, 0.1f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
