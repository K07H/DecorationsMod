using DecorationsMod.Controllers;
using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class ForkLiftDoll : DecorationItem
    {
        private Texture normal = null;
        private Texture illum = null;

        public ForkLiftDoll()
        {
            // Feed DecortionItem interface
            this.ClassID = "ForkLiftDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("forkliftdoll");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("ForkLiftDollName"),
                                                        LanguageHelper.GetFriendlyWord("ForkLiftDollDescription"),
                                                        true);

            if (ConfigSwitcher.Forklift_asBuidable)
                this.IsHabitatBuilder = true;

            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[3]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Silicone, 1)
                    }),
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                normal = AssetsHelper.Assets.LoadAsset<Texture>("generic_forklift_normal");
                illum = AssetsHelper.Assets.LoadAsset<Texture>("generic_forklift_illum");

                if (ConfigSwitcher.Forklift_asBuidable)
                {
                    // Add new TechType to the buildables
                    SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                    SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);
                }
                else
                {
                    // Set item occupies 4 slots
                    SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                    // Add the new TechType to Hand Equipment type.
                    SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);
                }

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("forklifticon"));

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;

            // Add tech tag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Add prefab identifier
            var prefabId = prefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Retrieve model node
            GameObject model = prefab.FindChild("forklift");
            
            // Add large world entity
            var lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;
            
            // Add box collider
            var collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.05f, 0.064f, 0.04f);
            collider.center = new Vector3(collider.center.x, collider.center.y + 0.032f, collider.center.z);

            // Set proper shaders (for crafting animation)
            Shader shader = Shader.Find("MarmosetUBER");
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material tmpMat in renderer.materials)
                {
                    // Associate MarmosetUBER shader
                    tmpMat.shader = shader;

                    if (tmpMat.name.CompareTo("generic_forklift (Instance)") == 0)
                    {
                        tmpMat.SetTexture("_BumpMap", normal);
                        tmpMat.SetTexture("_Illum", illum);
                        tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                        tmpMat.EnableKeyword("MARMO_EMISSION");
                    }
                }
            }

            // Add sky applier
            var skyapplier = prefab.AddComponent<SkyApplier>();
            skyapplier.renderers = renderers;
            skyapplier.anchorSky = Skies.Auto;

            if (ConfigSwitcher.Forklift_asBuidable)
            {
                // Add contructable
                var constructible = prefab.AddComponent<Constructable>();
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = true;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = true;
                constructible.controlModelState = true;
                constructible.deconstructionAllowed = true;
                constructible.rotationEnabled = true;
                constructible.model = model;
                constructible.techType = this.TechType;
                constructible.enabled = true;

                // Add constructable bounds
                var bounds = prefab.AddComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.003f, bounds.bounds.position.z);

                // Add forklift controller
                var forkliftController = prefab.AddComponent<ForkliftController>();
            }
            else
            {
                // We can pick this item
                var pickupable = prefab.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                var placeTool = prefab.AddComponent<PlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = true;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = true;
                placeTool.rotationEnabled = true;
                placeTool.enabled = true;
                placeTool.hasAnimations = false;
                placeTool.hasBashAnimation = false;
                placeTool.hasFirstUseAnimation = false;
                placeTool.mainCollider = collider;
                placeTool.pickupable = pickupable;
                placeTool.drawTime = 0.5f;
                placeTool.dropTime = 1;
                placeTool.holsterTime = 0.35f;

                // Add fabricating animation
                var fabricatingA = prefab.AddComponent<VFXFabricating>();
                fabricatingA.localMinY = -0.2f;
                fabricatingA.localMaxY = 0.7f;
                fabricatingA.posOffset = new Vector3(0f, 0f, 0f);
                fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
                fabricatingA.scaleFactor = 1f;
            }

            // Scale prefab
            prefab.transform.localScale *= 5.0f;
            
            return prefab;
        }
    }
}
