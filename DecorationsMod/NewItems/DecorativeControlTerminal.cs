using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class DecorativeControlTerminal : DecorationItem
    {
        public DecorativeControlTerminal() // Feeds abstract class
        {
            this.ClassID = "DecorativeControlTerminal"; // 6ca93e93-5209-4c27-ba60-5f68f36a95fb
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = Resources.Load<GameObject>("Submarine/Build/control_terminal_01");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("DecorativeControlTerminalName"),
                                                        LanguageHelper.GetFriendlyWord("DecorativeControlTerminalDescription"),
                                                        true);

            this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1] { new Ingredient(TechType.Titanium, 2) }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1] { new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 2) }),
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
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("control_terminal_icon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            prefab.name = this.ClassID;

            // Get model
            GameObject model = prefab.FindChild("Starship_control_terminal_01");
            
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

            // Ajust collider
            BoxCollider[] cs = prefab.GetComponentsInChildren<BoxCollider>();
            if (cs != null)
                foreach (BoxCollider c in cs)
                    c.size *= 0.9f;

            // Set as constructible
            Constructable constructible = prefab.AddComponent<Constructable>();
            constructible.techType = this.TechType;
            constructible.allowedOnWall = true;
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = false;
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
            cb.bounds.size *= 0.9f;

            return prefab;
        }
    }
}