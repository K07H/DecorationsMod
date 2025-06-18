﻿#if SUBNAUTICA_NAUTILUS
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
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class AlienArtefact1 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public AlienArtefact1() : base("AlienArtefact1", "AlienRelic1Name", "AlienRelic1Description", "relic_01_b")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public AlienArtefact1() // Feeds abstract class
        {
            this.ClassID = "AlienArtefact1"; // 8de9be7a-55e5-4487-90f8-79326ccfa066
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("AlienRelic1Name"),
                                                        LanguageHelper.GetFriendlyWord("AlienRelic1Description"),
                                                        true);
#endif

            CrafterLogicFixer.AlienArtefact1 = this.TechType;
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

                // Set item occupies 4 slots
                CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

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
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("relic_01_b"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _alienArtefact1 = null;

        public override GameObject GetGameObject()
        {
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
