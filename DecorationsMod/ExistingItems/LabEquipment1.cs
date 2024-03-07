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
    public class LabEquipment1 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public LabEquipment1() : base(
            new PrefabInfo("2cee55bc-6136-47c5-a1ed-14c8f3203856", "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_01.prefab", TechType.LabEquipment1)
            )
        {
            this.SetGameObject(this.GetGameObject);
#else
        public LabEquipment1() // Feeds abstract class
        {
            this.ClassID = "2cee55bc-6136-47c5-a1ed-14c8f3203856";
            this.PrefabFileName = "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_01.prefab";

            this.TechType = TechType.LabEquipment1;
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

        private static GameObject _labEquipment1 = null;

        public override GameObject GetGameObject()
        {
            if (_labEquipment1 == null)
                _labEquipment1 = PrefabsHelper.LoadGameObjectFromFilename(this.PrefabFileName);

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_labEquipment1);

            // Add fabricating animation
            var fabricating = prefab.FindChild("discovery_lab_props_01").AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
            fabricating.scaleFactor = 0.75f;

            return prefab;
        }
    }
}
