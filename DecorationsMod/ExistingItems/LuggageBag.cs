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
    public class LuggageBag : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public LuggageBag() : base(
            new PrefabInfo("3616e7f3-5079-443d-85b4-9ad68fcbd924", "WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4.prefab", TechType.LuggageBag)
            )
        {
#else
        public LuggageBag() // Feeds abstract class
        {
            this.ClassID = "3616e7f3-5079-443d-85b4-9ad68fcbd924";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4.prefab";

            this.TechType = TechType.LuggageBag;
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
                        new Ingredient(TechType.FiberMesh, 2),
                        new Ingredient(TechType.Silicone, 1)
                    }),
            };
        }

        private static GameObject _luggageBag = null;

        public override GameObject GetGameObject()
        {
            if (_luggageBag == null)
                _luggageBag = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_luggageBag);

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
