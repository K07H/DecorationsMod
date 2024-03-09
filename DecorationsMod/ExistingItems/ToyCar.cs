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
    public class ToyCar : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public ToyCar() : base(
            new PrefabInfo("dfabc84e-c4c5-45d9-8b01-ca0eaeeb8e65", "WorldEntities/Doodads/Debris/Wrecks/Decoration/Goldglove_car_02.prefab", TechType.ToyCar)
            )
        {
#else
            public ToyCar() // Feeds abstract class
        {
            this.ClassID = "dfabc84e-c4c5-45d9-8b01-ca0eaeeb8e65";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/Goldglove_car_02.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/Goldglove_car_02.prefab";
#endif

            this.TechType = TechType.ToyCar;
#endif

            this.GameObject = new GameObject(this.ClassID);

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[3]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1),
                        new Ingredient(TechType.Silicone, 1)
                    }),
            };
        }

        private static GameObject _toyCar = null;

        public override GameObject GetGameObject()
        {
            if (_toyCar == null)
                _toyCar = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            GameObject prefab = GameObject.Instantiate(_toyCar);

            // Make toy car placeable in more places.
            var pt = prefab.GetComponent<PlaceTool>();
            if (pt != null)
            {
                pt.allowedOnGround = true;
                pt.allowedOnRigidBody = true;
                pt.allowedOutside = true;
                pt.allowedUnderwater = true;
            }

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.04f;
            fabricating.localMaxY = 0.25f;
            fabricating.posOffset = new Vector3(-0.05f, 0f, -0.06f);
            fabricating.eulerOffset = new Vector3(0f, 90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
