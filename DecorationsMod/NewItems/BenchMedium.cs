using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class BenchMedium : DecorationItem
    {
        public BenchMedium()
        {
            // Feed DecortionItem interface
            this.ClassID = "BenchMedium";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = Resources.Load<GameObject>("Submarine/Build/Bench");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("BenchMediumName"),
                                                        LanguageHelper.GetFriendlyWord("BenchDescription"),
                                                        true);

            CrafterLogicFixer.BenchMedium = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Add new TechType to the buildables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType, TechType.Bench);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("benchmediumicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;

            // Modify tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Modify prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Add large world entity
            var lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Modify box colliders
            var collider = prefab.FindChild("Collider").GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x * 0.6f, collider.size.y, collider.size.z);
            var builderTrigger = prefab.FindChild("Builder Trigger").GetComponent<BoxCollider>();
            builderTrigger.size = new Vector3(builderTrigger.size.x * 0.6f, builderTrigger.size.y, builderTrigger.size.z);

            // Retrieve model node
            GameObject model = prefab.FindChild("model");

            // Move bench parts
            GameObject benchStart = model.FindChild("Bench_01_start");
            benchStart.transform.localPosition = new Vector3(-0.408f, benchStart.transform.localPosition.y, benchStart.transform.localPosition.z);
            GameObject benchEnd = model.FindChild("Bench_01_end");
            benchEnd.transform.localPosition = new Vector3(0.408f, benchEnd.transform.localPosition.y, benchEnd.transform.localPosition.z);
            GameObject benchTile = model.FindChild("Bench_01_tile");
            benchTile.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f); //new Vector3(99.7f, 99.7f, 99.7f);

            // Update sky applier
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
#if SUBNAUTICA
            var skyapplier = prefab.GetComponent<SkyApplier>();
            skyapplier.renderers = renderers;
            skyapplier.anchorSky = Skies.Auto;
#else
            var skyapplier = model.GetComponent<SkyApplier>();
            skyapplier.renderers = renderers;
            skyapplier.anchorSky = Skies.Auto;
#endif

            // Update contructable
            var constructible = prefab.GetComponent<Constructable>();
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = false;
            constructible.controlModelState = true;
            constructible.deconstructionAllowed = true;
            constructible.rotationEnabled = true;
            constructible.model = model;
            constructible.techType = this.TechType;
            constructible.placeMinDistance = 0.6f;
            constructible.enabled = true;

            // Update constructable bounds
            var constructableBounds = prefab.GetComponent<ConstructableBounds>();
            constructableBounds.bounds = new OrientedBounds(new Vector3(constructableBounds.bounds.position.x, constructableBounds.bounds.position.y, constructableBounds.bounds.position.z),
                new Quaternion(constructableBounds.bounds.rotation.x, constructableBounds.bounds.rotation.y, constructableBounds.bounds.rotation.z, constructableBounds.bounds.rotation.w),
                new Vector3(constructableBounds.bounds.extents.x * 0.6f, constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.z));

            return prefab;
        }
    }
}
