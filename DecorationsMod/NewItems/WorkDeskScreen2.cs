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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class WorkDeskScreen2 : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public WorkDeskScreen2() : base("WorkDeskScreen2", "WorkDeskScreen2Name", "WorkDeskScreen2Description", "WorkDeskScreenDamagedIcon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public WorkDeskScreen2() // Feeds abstract class
        {
            this.ClassID = "WorkDeskScreen2"; // 91bb3de1-36d5-497a-b27a-86aff784cb5a
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("WorkDeskScreen2Name"),
                                                        LanguageHelper.GetFriendlyWord("WorkDeskScreen2Description"),
                                                        true);
#endif

            this.IsHabitatBuilder = true;

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
            this.Recipe = new TechData()
#else
            this.Recipe = new RecipeData()
#endif
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1] { new Ingredient(TechType.Titanium, 1) }),
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

                // Add to the custom buidables
                CraftDataHandler.AddBuildable(this.TechType);
                CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("WorkDeskScreenDamagedIcon"));
#endif

                this.IsRegistered = true;
            }
        }

        private static GameObject _workDeskScreen2 = null;

        public override GameObject GetGameObject()
        {
            if (_workDeskScreen2 == null)
#if SUBNAUTICA
                _workDeskScreen2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Debris/Wrecks/Decoration/Starship_work_desk_screen_01_damaged.prefab");
#else
                _workDeskScreen2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Alterra/Base/Starship_work_desk_screen_01_damaged.prefab");
#endif

            GameObject prefab = GameObject.Instantiate(_workDeskScreen2);
            prefab.name = this.ClassID;

            // Get model
            GameObject model = prefab.FindChild("Starship_work_desk_screen_01_damaged");

            // Add compatibility with SnapBuilder
            PrefabsHelper.SnapBuilderCompatibility(model.transform, new Vector3(-90f, -90f, 0f));

            // Scale model
            model.transform.localScale *= 0.8f;

            // Remove rigid body
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Update large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Add tech tag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Set as constructible
            Constructable constructible = prefab.AddComponent<Constructable>();
            constructible.techType = this.TechType;
            constructible.allowedOnWall = true;
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = true;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = true;
            constructible.rotationEnabled = true;
            constructible.deconstructionAllowed = true;
            constructible.controlModelState = true;
            constructible.surfaceType = VFXSurfaceTypes.electronic;
            constructible.model = model;
            constructible.placeMinDistance = 0.6f;

            // Add constructable bounds
            prefab.AddComponent<ConstructableBounds>();

            return prefab;
        }
    }
}
