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
    public class LabContainer1 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public LabContainer1() : base(
            new PrefabInfo("e7f9c5e7-3906-4efd-b239-28783bce17a5", "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_01.prefab", TechType.LabContainer)
            )
        {
            this.SetGameObject(this.GetGameObject);
#else
        public LabContainer1() // Feeds abstract class
        {
            this.ClassID = "e7f9c5e7-3906-4efd-b239-28783bce17a5";
#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_01.prefab";
#else
            this.PrefabFileName = "WorldEntities/Alterra/Base/biodome_lab_containers_close_01.prefab";
#endif

            this.TechType = TechType.LabContainer;
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
                        new Ingredient(TechType.Glass, 2)
                    }),
            };
        }

        private static GameObject _labContainer1 = null;

        public override GameObject GetGameObject()
        {
            if (_labContainer1 == null)
                _labContainer1 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_labContainer1);

            // Add fabricating animation
            var fabricating = prefab.FindChild("biodome_lab_containers_close_01").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.80f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
    }
}
