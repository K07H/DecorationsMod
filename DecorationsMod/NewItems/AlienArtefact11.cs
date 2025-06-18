using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class AlienArtefact11 : DecorationItem
    {
        [SetsRequiredMembers]
        public AlienArtefact11() : base("AlienArtefact11", LanguageHelper.GetFriendlyWord("AlienRelic11Name"), LanguageHelper.GetFriendlyWord("AlienRelic11Description"), AssetsHelper.Assets.LoadAsset<Sprite>("relic_09_b")) // Feeds abstract class
        {
            this.ClassID = "AlienArtefact11"; // 09bc9a07-7680-4ddf-9ba2-a7da5e7b3287
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.AlienArtefact11 = this.TechType;
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
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("relic_09_b"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _alienArtefact11 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: alienArtefact11.GetGameObject()");
#endif
            if (_alienArtefact11 == null)
#if SUBNAUTICA
                _alienArtefact11 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Precursor/Prison/Relics/Alien_relic_12.prefab");
#else
                _alienArtefact11 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Precursor/Relics/Alien_relic_12.prefab");
#endif

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_alienArtefact11);
            prefab.name = this.ClassID;

            if (!ConfigSwitcher.AlienRelic11Animation)
                prefab.GetComponentInChildren<Animator>().enabled = false;

            // Get objects
            GameObject relicHlpr = prefab.FindChild("alien_relic_09_hlpr");

            // Move
            relicHlpr.transform.localPosition = new Vector3(relicHlpr.transform.localPosition.x, relicHlpr.transform.localPosition.y + 0.3f, relicHlpr.transform.localPosition.z);
            
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

            // Remove rigid body to prevent bugs
            /*
            var rbs = prefab.GetComponentsInChildren<Rigidbody>();
            if (rbs != null && rbs.Length > 0)
                foreach (var rbp in rbs)
                    GameObject.DestroyImmediate(rbp);
            var rb = prefab.GetComponent<Rigidbody>();
            if (rb != null)
                GameObject.DestroyImmediate(rb);
            */

            // Update sky applier
            PrefabsHelper.ReplaceSkyApplier(prefab);

            // Scale collider
            var collider = prefab.GetComponent<CapsuleCollider>();
            collider.radius = 0.1f;
            collider.height = 0.2f;
            collider.contactOffset = 0.1f;
            collider.isTrigger = true;
            var cCollider = prefab.GetComponentInChildren<CapsuleCollider>();
            cCollider.radius *= 0.4f;
            cCollider.height *= 0.4f;
            cCollider.isTrigger = true;

            // We can pick this item
            var pickupable = prefab.GetComponent<Pickupable>();
            if (pickupable == null)
                pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            prefab.AddComponent<CustomPlaceToolController>();
            //var placeTool = prefab.GetComponent<PlaceTool>();
            //if (placeTool != null)
            //    GameObject.DestroyImmediate(placeTool);
            var placeTool = prefab.AddComponent<GenericPlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
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
