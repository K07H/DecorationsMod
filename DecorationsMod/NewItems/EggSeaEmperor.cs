using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class EggSeaEmperor : DecorationItem
    {
        public EggSeaEmperor()
        {
            this.ClassID = "EggSeaEmperor"; // fa5327d8-1975-4e5a-93b2-9e5554907d7b
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("EggSeaEmperorName"),
                                                        LanguageHelper.GetFriendlyWord("EggSeaEmperorDescription"),
                                                        true);

            this.GameObject = Resources.Load<GameObject>("WorldEntities/Eggs/EmperorEgg");

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to the hand-equipments
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seaemperoreggicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;

            // Remove unwanted elements
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(prefab.GetComponent<IncubatorEgg>());
            GameObject.DestroyImmediate(prefab.GetComponent<VFXController>());
            GameObject.DestroyImmediate(prefab.GetComponentInChildren<Animator>());
            GameObject.DestroyImmediate(prefab.GetComponentInChildren<IncubatorEggAnimation>());

            GameObject model = prefab.FindChild("Creatures_eggs_11");

            // Rotate model
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + 180f, model.transform.localEulerAngles.z);

            // Scale model
            model.transform.localScale *= 0.2f;

            // Scale colliders
            foreach (SphereCollider c in prefab.GetAllComponentsInChildren<SphereCollider>())
                c.radius *= 0.15f;

            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            if (techTag == null)
                if ((techTag = prefab.GetComponentInChildren<TechTag>()) == null)
                    techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            if (prefabId == null)
                if ((prefabId = prefab.GetComponentInChildren<PrefabIdentifier>()) == null)
                    prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update sky applier
            PrefabsHelper.SetDefaultSkyApplier(prefab);

            // Set large world entity
            PrefabsHelper.SetDefaultLargeWorldEntity(prefab);

            // We can pick this item
            PrefabsHelper.SetDefaultPickupable(prefab);

            // We can place this item
            prefab.AddComponent<EggSeaEmperor_PT>();
            PrefabsHelper.SetDefaultPlaceTool(prefab);

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0.03f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1.15f;

            return prefab;
        }
    }
}
