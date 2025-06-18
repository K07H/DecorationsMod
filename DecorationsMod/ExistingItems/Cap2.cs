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
    public class Cap2 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public Cap2() : base(
            new PrefabInfo("3dc40631-2945-4109-acdc-823a9a0a8646", "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_03.prefab", TechType.Cap2)
            )
        {
#else
        public Cap2() // Feeds abstract class
        {
            this.ClassID = "3dc40631-2945-4109-acdc-823a9a0a8646";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_03.prefab";

            this.TechType = TechType.Cap2;
#endif

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.FiberMesh, 1)
                    }),
            };
        }

        private static GameObject _cap2 = null;

        public override GameObject GetGameObject()
        {
            if (_cap2 == null )
                _cap2 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_cap2);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_plaza_shelf_cap_03").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.06f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
