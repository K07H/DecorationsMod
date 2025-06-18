using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using HarmonyLib;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class AlienArtefact6 : DecorationItem
    {
        [SetsRequiredMembers]
        public AlienArtefact6() : base("AlienArtefact6", LanguageHelper.GetFriendlyWord("AlienRelic6Name"), LanguageHelper.GetFriendlyWord("AlienRelic6Description"), AssetsHelper.Assets.LoadAsset<Sprite>("relic_02_b")) // Feeds abstract class
        {
            this.ClassID = "AlienArtefact6"; // 3c5abaf7-b18e-4835-8282-874763343d57
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.AlienArtefact6 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(ConfigSwitcher.RelicRecipiesResource, ConfigSwitcher.RelicRecipiesResourceAmount)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(ConfigSwitcher.RelicRecipiesResource, ConfigSwitcher.RelicRecipiesResourceAmount)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add the new TechType to the hand-equipments
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("relic_02_b"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _alienArtefact6 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: alienArtefact6.GetGameObject()");
#endif
            if (_alienArtefact6 == null)
#if SUBNAUTICA
                _alienArtefact6 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Precursor/Prison/Relics/Alien_relic_06.prefab");
#else
                _alienArtefact6 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Precursor/Relics/Alien_relic_06.prefab");
#endif

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_alienArtefact6);
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
            PrefabsHelper.ReplaceSkyApplier(prefab);

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
