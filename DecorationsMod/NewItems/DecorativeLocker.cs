﻿using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class DecorativeLocker : DecorationItem
    {
        private GameObject CargoCrateContainer = null;

        [SetsRequiredMembers]
        public DecorativeLocker() : base("DecorativeLocker", LanguageHelper.GetFriendlyWord("DecorativeLockerName"), LanguageHelper.GetFriendlyWord("DecorativeLockerDescription"), AssetsHelper.Assets.LoadAsset<Sprite>("decorativelockericon")) // Feeds abstract class
        {
            this.ClassID = "DecorativeLocker"; // bca9b19c-616d-4948-8742-9bb6f4296dc3
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 2)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                this.CargoCrateContainer = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Locker.prefab");

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                // Add to the custom buidables
                Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType, TechType.Locker);

                // Set the buildable prefab
                this.Register();

                // Set the custom icon
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("decorativelockericon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _decorativeLocker = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: decorativeLocker.GetGameObject()");
#endif
            if (_decorativeLocker == null)
#if SUBNAUTICA
                _decorativeLocker = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/submarine_locker_04_open.prefab");
#else
                _decorativeLocker = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Base/submarine_locker_04_open.prefab");
#endif

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_decorativeLocker);
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

            // Update large world entity
            PrefabsHelper.UpdateExistingLargeWorldEntities(prefab);

            // Update sky applier
#if !SUBNAUTICA
            while (prefab.GetComponentInChildren<BaseModuleLighting>() != null)
                GameObject.DestroyImmediate(prefab.GetComponentInChildren<BaseModuleLighting>());
            if (prefab.GetComponent<BaseModuleLighting>() != null)
                GameObject.DestroyImmediate(prefab.GetComponent<BaseModuleLighting>());
            BaseModuleLighting bml = prefab.EnsureComponent<BaseModuleLighting>();
#endif
            PrefabsHelper.ReplaceSkyApplier(prefab); //PrefabsHelper.UpdateOrAddSkyApplier(prefab);

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
            constructible.placeMinDistance = 0.6f;

            // Add constructable bounds
            ConstructableBounds bounds = prefab.AddComponent<ConstructableBounds>();

            // Add model controler
            var decorativeLockerController = prefab.AddComponent<DecorativeLockerController>();

            return prefab;
        }
    }
}
