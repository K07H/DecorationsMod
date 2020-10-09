using DecorationsMod.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class AlienTabletOrange : DecorationItem
    {
        public AlienTabletOrange()// Feeds abstract class
        {
            this.ClassID = "e10ce5d9-9675-4553-bf33-b17e93e3aab4";

#if SUBNAUTICA
            this.PrefabFileName = "WorldEntities/Doodads/Precursor/PrecursorKey_Orange";
#else
            this.PrefabFileName = "WorldEntities/Precursor/Keys/PrecursorKey_Orange";
#endif

            this.TechType = TechType.PrecursorKey_Orange;

            this.GameObject = Resources.Load<GameObject>(this.PrefabFileName);

            /*
#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(ConfigSwitcher.PrecursorKeysResource, ConfigSwitcher.PrecursorKeysResourceAmount)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(ConfigSwitcher.PrecursorKeysResource, ConfigSwitcher.PrecursorKeysResourceAmount)
                    }),
            };
#endif
            */
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                //SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add the new TechType to the hand-equipments
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            prefab.name = this.ClassID;

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

            // Retrieve collider
            Collider collider = prefab.GetComponent<BoxCollider>();
            collider.isTrigger = true;

            // We can pick this item
            var pickupable = prefab.GetComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            var cpt = prefab.GetComponent<CustomPlaceToolController>();
            if (cpt == null)
                cpt = prefab.AddComponent<CustomPlaceToolController>();
            var placeTool = prefab.AddComponent<OrangeKey_PT>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = false;
            placeTool.allowedOutside = true;
            placeTool.rotationEnabled = true;
            placeTool.enabled = true;
            placeTool.hasAnimations = false;
            placeTool.hasBashAnimation = false;
            placeTool.hasFirstUseAnimation = false;
            placeTool.mainCollider = collider;
            placeTool.pickupable = pickupable;

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.3f;
            fabricating.posOffset = new Vector3(0f, 0.05f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.8f;

            // Update sky applier
            SkyApplier sa = prefab.GetComponent<SkyApplier>();
            if (sa == null)
                sa = prefab.GetComponentInChildren<SkyApplier>();
            if (sa == null)
                sa = prefab.AddComponent<SkyApplier>();
            sa.renderers = prefab.GetComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;

            return prefab;
        }
    }
}
