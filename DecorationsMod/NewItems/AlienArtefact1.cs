using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class AlienArtefact1 : DecorationItem
    {
        [SetsRequiredMembers]
        public AlienArtefact1() : base("AlienArtefact1", LanguageHelper.GetFriendlyWord("AlienRelic1Name"), LanguageHelper.GetFriendlyWord("AlienRelic1Description"), AssetsHelper.Assets.LoadAsset<Sprite>("relic_01_b")) // Feeds abstract class
        {
            this.ClassID = "AlienArtefact1"; // 8de9be7a-55e5-4487-90f8-79326ccfa066
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.AlienArtefact1 = this.TechType;
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

                // Set item occupies 4 slots
                Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to the hand-equipments
                Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("relic_01_b"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _alienArtefact1 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: alienArtefact1.GetGameObject()");
#endif
            if (_alienArtefact1 == null)
#if SUBNAUTICA
                _alienArtefact1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Precursor/Prison/Relics/alien_relic_01.prefab");
#else
                _alienArtefact1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Precursor/Relics/alien_relic_01.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_alienArtefact1);
            prefab.name = this.ClassID;

            if (!ConfigSwitcher.AlienRelic1Animation)
                prefab.GetComponentInChildren<Animator>().enabled = false;

            // Remove unwanted elements
            //GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(prefab.GetComponent<EntityTag>());
            GameObject.DestroyImmediate(prefab.GetComponent<ImmuneToPropulsioncannon>());
            GameObject.DestroyImmediate(prefab.GetComponent<CapsuleCollider>());

            // Get objects
            //GameObject model = prefab.FindChild("relic_01");
            GameObject relicRot = prefab.FindChild("alien_relic_01_world_rot");
            GameObject relicHlpr = relicRot.FindChild("alien_relic_01_hlpr");
            //GameObject relicCtrl = relicHlpr.FindChild("alien_relic_01_ctrl");
            //GameObject subModel = relicCtrl.FindChild("relic_01");

            // Translate helper
            relicHlpr.transform.localPosition = new Vector3(0f, 1.85f, 0f);

            // Scale prefab
            foreach (Transform tr in prefab.transform)
                tr.localScale *= 0.5f;

            // Update TechTag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update sky applier
            PrefabsHelper.ReplaceSkyApplier(prefab);

            // Scale collider
            var collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.5f, 0.6f, 0.5f);
            collider.center = new Vector3(0f, 0.3f, 0f);
            collider.isTrigger = true;

            // We can pick this item
            var pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            // We can place this item
            prefab.AddComponent<CustomPlaceToolController>();
            var placeTool = prefab.AddComponent<AlienArtefact1_PT>();
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
            placeTool.ghostModelPrefab = prefab;
            placeTool.mainCollider = collider;
            placeTool.pickupable = pickupable;

            // Add fabricating animation
            var fabricating = prefab.AddComponent<VFXFabricating>();
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, -0.06f, 0.02f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.6f;

            return prefab;
        }
    }
}
