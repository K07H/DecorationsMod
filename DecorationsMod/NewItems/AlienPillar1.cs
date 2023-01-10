using DecorationsMod.Fixers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class AlienPillar1 : DecorationItem
    {
        public AlienPillar1() // Feeds abstract class
        {
            this.ClassID = "AlienPillar1"; // 78009225-a9fa-4d21-9580-8719a3368373
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("AlienPillar1Name"),
                                                        LanguageHelper.GetFriendlyWord("AlienPillar1Description"),
                                                        true);

            CrafterLogicFixer.AlienPillar = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.IsHabitatBuilder = true;

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 2)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(TechType.Titanium, 2)
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

                // Add to the custom buidables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("alien_pillar_01"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _alienPillar9 = null;

        public override GameObject GetGameObject()
        {
            if (_alienPillar9 == null)
#if SUBNAUTICA
                _alienPillar9 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Doodads/Precursor/precursor_deco_props_01.prefab");
#else
                _alienPillar9 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Precursor/Doodads/precursor_deco_props_01.prefab");
#endif

            //GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject prefab = GameObject.Instantiate(_alienPillar9);
            prefab.name = this.ClassID;

            // Scale
            foreach (Transform tr in prefab.transform)
                tr.transform.localScale *= 0.4f;

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

            // Clean all the crap
            GameObject.DestroyImmediate(prefab.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(prefab.GetComponent<ConstructionObstacle>());

            // Set large world entity
            prefab.GetComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

            // Ajust collider
            BoxCollider c = prefab.GetComponentInChildren<BoxCollider>();
            c.size = new Vector3(c.size.x * 0.5f, c.size.y, c.size.z * 0.5f);
            // Set as constructible
            Constructable constructible = prefab.AddComponent<Constructable>();
            constructible.techType = this.TechType;
            constructible.allowedOnWall = false;
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = true;
            constructible.allowedOnConstructables = true;
            constructible.rotationEnabled = true;
            constructible.deconstructionAllowed = true;
            constructible.controlModelState = true;
            constructible.model = prefab.FindChild("precursor_deco_props_01");
            constructible.placeMinDistance = 0.6f;
            constructible.enabled = true;

            // Add constructable bounds
            prefab.AddComponent<ConstructableBounds>();

            // Update sky applier
            BaseModuleLighting bml = prefab.GetComponent<BaseModuleLighting>();
            if (bml == null)
                bml = prefab.GetComponentInChildren<BaseModuleLighting>();
            if (bml == null)
                bml = prefab.AddComponent<BaseModuleLighting>();
            SkyApplier sa = prefab.GetComponent<SkyApplier>();
            if (sa == null)
                sa = prefab.GetComponentInChildren<SkyApplier>();
            if (sa == null)
                sa = prefab.AddComponent<SkyApplier>();
            sa.renderers = prefab.GetComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;

            return prefab;
        }
    }
}
