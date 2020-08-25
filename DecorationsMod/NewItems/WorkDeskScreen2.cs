using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class WorkDeskScreen2 : DecorationItem
    {
        public WorkDeskScreen2() // Feeds abstract class
        {
            this.ClassID = "WorkDeskScreen2"; // 91bb3de1-36d5-497a-b27a-86aff784cb5a
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

#if BELOW_ZERO
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Alterra/Base/Starship_work_desk_screen_01_damaged");
#else
            this.GameObject = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/Starship_work_desk_screen_01_damaged");
#endif

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("WorkDeskScreen2Name"),
                                                        LanguageHelper.GetFriendlyWord("WorkDeskScreen2Description"),
                                                        true);

            this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1] { new Ingredient(TechType.Titanium, 1) }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1] { new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1) }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add to the custom buidables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("WorkDeskScreenDamagedIcon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            prefab.name = this.ClassID;

            // Get model
            GameObject model = prefab.FindChild("Starship_work_desk_screen_01_damaged");

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
            ConstructableBounds cb = prefab.AddComponent<ConstructableBounds>();
            //Logger.Log("DEBUG: Constructable bounds WDS2: x=[" + cb.bounds.extents.x.ToString() + "] y=[" + cb.bounds.extents.y.ToString() + "] z=[" + cb.bounds.extents.z.ToString() + "]");
            //cb.bounds.size *= 0.9f;

            return prefab;
        }
    }
}
