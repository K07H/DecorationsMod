using DecorationsMod.Controllers;
using Harmony;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class AlienArtefact6 : DecorationItem
    {
        public AlienArtefact6() // Feeds abstract class
        {
            this.ClassID = "AlienArtefact6"; // 3c5abaf7-b18e-4835-8282-874763343d57
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = Resources.Load<GameObject>("WorldEntities/Doodads/Precursor/Prison/Relics/alien_relic_06");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("AlienRelic6Name"),
                                                        LanguageHelper.GetFriendlyWord("AlienRelic6Description"),
                                                        true);

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(ConfigSwitcher.RelicRecipiesResource, ConfigSwitcher.RelicRecipiesResourceAmount)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(ConfigSwitcher.RelicRecipiesResource, ConfigSwitcher.RelicRecipiesResourceAmount)
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

                // Add the new TechType to the hand-equipments
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("relic_02_b"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            prefab.name = this.ClassID;

            if (!ConfigSwitcher.AlienRelic6Animation)
                prefab.GetComponentInChildren<Animator>().enabled = false;

            // Get objects
            GameObject relicCtrl = prefab.FindChild("alien_relic_02_core_ctrl");

            // Move
            relicCtrl.transform.localPosition = new Vector3(relicCtrl.transform.localPosition.x, relicCtrl.transform.localPosition.y + 0.22f, relicCtrl.transform.localPosition.z);

            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Update EntityTag
            var entityTag = prefab.GetComponent<EntityTag>();
            entityTag.slotType = EntitySlot.Type.Small;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Remove rigid body to prevent bugs
            //GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            //var rb = prefab.GetComponent<Rigidbody>();
            //rb.mass = 0.0f;
            //rb.detectCollisions = false;

            // Update sky applier
            var applier = prefab.GetComponent<SkyApplier>();
            applier.anchorSky = Skies.Auto;
            applier.updaterIndex = 0;

            // Adjust colliders
            var collider = prefab.GetComponent<CapsuleCollider>();
            collider.radius = 0.1f;
            collider.height = 0.16f;
            collider.center = new Vector3(0f, 0.08f, 0f);
            collider.isTrigger = true;
            var sCollider = prefab.GetComponentInChildren<SphereCollider>();
            sCollider.radius *= 0.5f;
            sCollider.isTrigger = true;
            var bCollider = prefab.GetComponentInChildren<BoxCollider>();
            bCollider.size = new Vector3(2f, 2f, 2f);
            bCollider.center = Vector3.zero;
            bCollider.isTrigger = true;
            Targeting.AddToIgnoreList(bCollider.transform.parent);

            // We can pick this item
            var pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            prefab.AddComponent<CustomPlaceToolController>();
            var placeTool = prefab.AddComponent<GenericPlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = true;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = true;
            placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
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
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0f, 0f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.7f;

            return prefab;
        }
    }
}
