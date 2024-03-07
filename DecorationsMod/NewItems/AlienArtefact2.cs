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
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class AlienArtefact2 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public AlienArtefact2() : base("AlienArtefact2", "AlienRelic2Name", "AlienRelic2Description", "relic_11_b")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public AlienArtefact2() // Feeds abstract class
        {
            this.ClassID = "AlienArtefact2"; // e6e7977e-3639-43b9-978f-9d0b40f17800
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("AlienRelic2Name"),
                                                        LanguageHelper.GetFriendlyWord("AlienRelic2Description"),
                                                        true);
#endif

            CrafterLogicFixer.AlienArtefact2 = this.TechType;
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
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("relic_11_b"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _alienArtefact2 = null;

        public override GameObject GetGameObject()
        {
            if (_alienArtefact2 == null)
#if SUBNAUTICA
                _alienArtefact2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Precursor/Prison/Relics/Alien_relic_02.prefab");
#else
                _alienArtefact2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Precursor/Relics/Alien_relic_02.prefab");
#endif

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_alienArtefact2);
            prefab.name = this.ClassID;

            if (!ConfigSwitcher.AlienRelic2Animation)
                prefab.GetComponentInChildren<Animator>().enabled = false;

            // Get objects
            GameObject relicHlpr = prefab.FindChild("alien_relic_11_hlpr");

            // Move
            relicHlpr.transform.localPosition = new Vector3(relicHlpr.transform.localPosition.x, relicHlpr.transform.localPosition.y + 0.1f, relicHlpr.transform.localPosition.z);
            // Scale
            foreach (Transform tr in prefab.transform)
                tr.transform.localScale *= 0.5f;

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

            // Scale colliders
            var collider = prefab.GetComponent<CapsuleCollider>();
            collider.radius = 0.5f;
            collider.height = 0.1f;
            collider.contactOffset = 0.1f;
            collider.isTrigger = true;
            foreach (BoxCollider c in prefab.GetComponents<BoxCollider>())
            {
                c.size *= 0.5f;
                c.isTrigger = true;
            }

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
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, -0.04f, 0.1f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.7f;

            return prefab;
        }
    }
}
