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
    public class Cap1 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public Cap1() : base(
            new PrefabInfo("5884d27a-8798-4f09-82ec-c7671a604504", "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_02.prefab", TechType.Cap1)
            )
        {
#else
        public Cap1() // Feeds abstract class
        {
            this.ClassID = "5884d27a-8798-4f09-82ec-c7671a604504";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_02.prefab";

            this.TechType = TechType.Cap1;
#endif

            this.GameObject = new GameObject(this.PrefabFileName);

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

        private static GameObject _cap1 = null;

        public override GameObject GetGameObject()
        {
            if (_cap1 == null)
                _cap1 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            Logger.Debug("Cap1 prefab is " + (_cap1 == null ? "NULL" : "NOT NULL"));

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_cap1);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_plaza_shelf_cap_02").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.06f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
