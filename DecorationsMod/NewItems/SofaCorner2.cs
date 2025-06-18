using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using static CraftData;

namespace DecorationsMod.NewItems
{
    public class SofaCorner2 : DecorationItem
    {
        public GameObject newsofa = null;

        private Texture normal = null;
        private Texture spec = null;

        [SetsRequiredMembers]
        public SofaCorner2() : base("SofaCorner2", LanguageHelper.GetFriendlyWord("SofaCorner2Name"), LanguageHelper.GetFriendlyWord("SofaCorner2Description"), AssetsHelper.Assets.LoadAsset<Sprite>("sofacorner02icon"))
        {
            // Feed DecortionItem interface
            this.ClassID = "SofaCorner2";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = new GameObject(this.ClassID);

            this.TechType = this.Info.TechType;

            CrafterLogicFixer.SofaCorner = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

            this.newsofa = AssetsHelper.Assets.LoadAsset<GameObject>("descent_bar_sofa_corner_02");

            if (ConfigSwitcher.SofaCorner2_asBuidable)
                this.IsHabitatBuilder = true;

#if SUBNAUTICA
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#else
            Nautilus.Crafting.RecipeData recipeData = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.FiberMesh, 1)
            });
            recipeData.craftAmount = 1;
            this.Recipe = recipeData;
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                normal = AssetsHelper.Assets.LoadAsset<Texture>("descent_bar_sofa_01_normal");
                spec = AssetsHelper.Assets.LoadAsset<Texture>("descent_bar_sofa_01_spec");

                // Associate recipe to the new TechType
                Nautilus.Handlers.CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);

                if (ConfigSwitcher.SofaCorner2_asBuidable)
                {
                    // Add new TechType to the buildables
                    Nautilus.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                    Nautilus.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType, TechType.StarshipChair3);
                }
                else
                {
                    // Set item occupies 4 slots
                    Nautilus.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                    // Add the new TechType to Hand Equipment type.
                    Nautilus.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                    // Set quick slot type.
                    Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);
                }

                // Set the buildable prefab
                this.Register();

                // Set the custom sprite
                Nautilus.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("sofacorner02icon"));

                this.IsRegistered = true;
            }
        }

        private static GameObject _sofaCorner2 = null;

        public override GameObject GetGameObject()
        {
#if DEBUG_ITEMS_REGISTRATION
            Logger.Info("INFO: sofaCorner2.GetGameObject()");
#endif
            if (_sofaCorner2 == null)
                _sofaCorner2 = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Bench.prefab");

            GameObject prefab = GameObject.Instantiate(_sofaCorner2);
            GameObject newsofaPrefab = GameObject.Instantiate(newsofa);

            prefab.name = this.ClassID;

            // Retrieve model node
            GameObject model = prefab.FindChild("model");

            // Scale
            model.transform.localScale *= 0.2f;

            // Modify prefab identifier
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;

            // Add large world entity
            var lwe = prefab.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

            // Modify tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = this.TechType;

            // Modify box colliders
            var collider = prefab.FindChild("Collider").GetComponent<BoxCollider>();
            collider.size = new Vector3(0.75f, 0.43f, 0.75f);
            var builderTrigger = prefab.FindChild("Builder Trigger").GetComponent<BoxCollider>();
            builderTrigger.size = new Vector3(0.75f, 0.43f, 0.75f);

            // Disable renderers
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
                rend.enabled = false;

            // Set proper shaders
            Shader shader = Shader.Find("MarmosetUBER");
            renderers = newsofaPrefab.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material tmpMat in renderer.materials)
                {
                    // Associate MarmosetUBER shader
                    tmpMat.shader = shader;

                    if (string.Compare(tmpMat.name, "descent_bar_sofa_01 (Instance)", true, CultureInfo.InvariantCulture) == 0)
                    {
                        tmpMat.SetTexture("_BumpMap", normal);
                        tmpMat.SetTexture("_SpecTex", spec);
                        tmpMat.EnableKeyword("MARMO_NORMALMAP");
                        tmpMat.EnableKeyword("MARMO_SPECMAP");
                        tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                    }
                }
            }
            newsofaPrefab.transform.parent = prefab.transform;
            newsofaPrefab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            newsofaPrefab.transform.localScale = new Vector3(3.48f, 3.48f, 3.48f);
            newsofaPrefab.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            newsofaPrefab.SetActive(true);

            // Get bench
            var bench = prefab.GetComponent<Bench>();
            bench.cinematicController.animatedTransform.localPosition = new Vector3(bench.cinematicController.animatedTransform.localPosition.x, bench.cinematicController.animatedTransform.localPosition.y, bench.cinematicController.animatedTransform.localPosition.z + 0.31f);
            bench.cinematicController.animatedTransform.localEulerAngles = new Vector3(bench.cinematicController.animatedTransform.localEulerAngles.x, bench.cinematicController.animatedTransform.localEulerAngles.y + 45.0f, bench.cinematicController.animatedTransform.localEulerAngles.z);

            // Set sky applier
            PrefabsHelper.UpdateOrAddSkyApplier(prefab, null, renderers);

            if (ConfigSwitcher.SofaCorner2_asBuidable)
            {
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
                constructible.model = prefab.FindChild("descent_bar_sofa_corner_02(Clone)");
                constructible.techType = this.TechType;
                constructible.placeMinDistance = 0.8f;
                constructible.enabled = true;

                // Update constructable bounds
                var constructableBounds = prefab.GetComponent<ConstructableBounds>();
                constructableBounds.bounds = new OrientedBounds(new Vector3(constructableBounds.bounds.position.x, constructableBounds.bounds.position.y, constructableBounds.bounds.position.z),
                    new Quaternion(constructableBounds.bounds.rotation.x, constructableBounds.bounds.rotation.y, constructableBounds.bounds.rotation.z, constructableBounds.bounds.rotation.w),
                    new Vector3(constructableBounds.bounds.extents.x * 0.3f, constructableBounds.bounds.extents.y, constructableBounds.bounds.extents.z * 0.3f));
            }
            else
            {
                // Remove constructable
                var constructable = prefab.GetComponent<Constructable>();
                var constructBounds = prefab.GetComponent<ConstructableBounds>();
                GameObject.DestroyImmediate(constructable);
                GameObject.DestroyImmediate(constructBounds);
                
                // We can pick this item
                var pickupable = prefab.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                prefab.AddComponent<CustomPlaceToolController>();
                var placeTool = prefab.AddComponent<GenericPlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
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
                fabricatingA.posOffset = new Vector3(0f, 0f, 0.2f);
                fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
                fabricatingA.scaleFactor = 0.4f;
            }

            return prefab;
        }
    }
}
