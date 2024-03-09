#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using static CraftData;
#else
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
#endif
using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class AlienArtefact6 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public AlienArtefact6() : base("AlienArtefact6", "AlienRelic6Name", "AlienRelic6Description", "relic_02_b")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public AlienArtefact6() // Feeds abstract class
        {
            this.ClassID = "AlienArtefact6"; // 3c5abaf7-b18e-4835-8282-874763343d57
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("AlienRelic6Name"),
                                                        LanguageHelper.GetFriendlyWord("AlienRelic6Description"),
                                                        true);
#endif

            CrafterLogicFixer.AlienArtefact6 = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(ConfigSwitcher.RelicRecipiesResource, ConfigSwitcher.RelicRecipiesResourceAmount)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Add the new TechType to the hand-equipments
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("relic_02_b"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _alienArtefact6 = null;

        public override GameObject GetGameObject()
        {
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
