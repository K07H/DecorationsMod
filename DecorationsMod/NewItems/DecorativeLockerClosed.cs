using DecorationsMod.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class DecorativeLockerClosed : DecorationItem
    {
        private GameObject CargoCrateContainer = null;

        public DecorativeLockerClosed() // Feeds abstract class
        {
            this.ClassID = "DecorativeLockerClosed"; // cd34fecd-794c-4a0c-8012-dd81b77f2840
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = Resources.Load<GameObject>("Submarine/Build/submarine_locker_04");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("DecorativeLockerName"),
                                                        LanguageHelper.GetFriendlyWord("DecorativeLockerDescription"),
                                                        true);

            this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 2)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 2)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                this.CargoCrateContainer = Resources.Load<GameObject>("Submarine/Build/Locker");

                // Add to the custom buidables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType, TechType.Locker);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom icon
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("decorativelockerclosedicon"));

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject container = GameObject.Instantiate(this.CargoCrateContainer);
            GameObject model = prefab.FindChild("submarine_locker_04");

            prefab.name = this.ClassID;

            // Update container renderers
            GameObject cargoCrateModel = container.FindChild("model");
            Renderer[] cargoCrateRenderers = cargoCrateModel.GetComponentsInChildren<Renderer>();
            container.transform.parent = prefab.transform;
            foreach (Renderer rend in cargoCrateRenderers)
            {
                rend.enabled = false;
            }
            container.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            container.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            container.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            container.SetActive(true);

            // Update colliders
            GameObject builderTrigger = container.FindChild("Builder Trigger");
            GameObject objectTrigger = container.FindChild("Collider");
            BoxCollider builderCollider = builderTrigger.GetComponent<BoxCollider>();
            builderCollider.isTrigger = false;
            builderCollider.enabled = false;
            BoxCollider objectCollider = objectTrigger.GetComponent<BoxCollider>();
            objectCollider.isTrigger = false;
            objectCollider.enabled = false;

            // Delete constructable bounds
            ConstructableBounds cb = container.GetComponent<ConstructableBounds>();
            GameObject.DestroyImmediate(cb);

            // Update TechTag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Remove rigid body
            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            GameObject.DestroyImmediate(rb);

            // Add collider
            BoxCollider collider = model.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.4f, 2.0f, 0.5f);
            collider.center = new Vector3(0.0f, 1.0f, 0.0f);

            // Update sky applier
            PrefabsHelper.SetDefaultSkyApplier(prefab);

            // Update large world entity
            PrefabsHelper.SetDefaultLargeWorldEntity(prefab);

            // Set as constructible
            Constructable constructible = prefab.AddComponent<Constructable>();
            constructible.techType = this.TechType;
            constructible.allowedOnWall = false;
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = false;
            constructible.rotationEnabled = true;
            constructible.deconstructionAllowed = true;
            constructible.controlModelState = true;
            constructible.model = model;

            // Add constructable bounds
            ConstructableBounds bounds = prefab.AddComponent<ConstructableBounds>();

            // Add model controler
            var decorativeLockerController = prefab.AddComponent<DecorativeLockerController>();

            return prefab;
        }
    }
}
