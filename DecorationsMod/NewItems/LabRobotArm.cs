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
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class LabRobotArm : DecorationItem
    {
#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        public LabRobotArm() : base("LabRobotArm", "LabRobotArmName", "LabRobotArmDescription", "robotarmicon")
        {
            this.GameObject = new GameObject(this.ClassID);
#else
        public LabRobotArm()
        {
            this.ClassID = "LabRobotArm";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("labrobotarm");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("LabRobotArmName"),
                                                        LanguageHelper.GetFriendlyWord("LabRobotArmDescription"),
                                                        true);
#endif

            CrafterLogicFixer.LabRobotArm = this.TechType;
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
                        new Ingredient(TechType.Titanium, 1)
                    }),
            };
        }

        private static GameObject _labRobotArm = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_labRobotArm == null)
                    _labRobotArm = AssetsHelper.Assets.LoadAsset<GameObject>("labrobotarm");

                GameObject model = _labRobotArm.FindChild("robotarm");
                GameObject armModel = model.FindChild("biodome_Robot_Arm");
                GameObject wallModel = model.FindChild("biodome_Robot_Arm_wall_tile");

                // Scale model
                model.transform.localScale *= 12f;
                
                // Set tech tag
                var techTag = _labRobotArm.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set prefab identifier
                var prefabId = _labRobotArm.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Set collider
                var collider = _labRobotArm.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.3f, 0.5f, 0.7f);

                // Set large world entity
                var lwe = _labRobotArm.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("biodome_Robot_Arm_normal");
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("biodome_Robot_Arm_illum");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("biodome_Robot_Arm_wall_normal");
                Renderer[] renderers = _labRobotArm.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    foreach (Material tmpMat in renderer.materials)
                    {
                        tmpMat.shader = marmosetUber;
                        if (string.Compare(tmpMat.name, "biodome_Robot_Arm (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal);
                            tmpMat.SetTexture("_Illum", illum);

                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("MARMO_EMISSION");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                        else if (string.Compare(tmpMat.name, "biodome_Robot_Arm_wall (Instance)", true, CultureInfo.InvariantCulture) == 0)
                        {
                            tmpMat.SetTexture("_BumpMap", normal2);
                            tmpMat.EnableKeyword("MARMO_NORMALMAP");
                            tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                        }
                    }
                }

                // Update sky applier
                var applier = _labRobotArm.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = _labRobotArm.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;
                
                // We can pick this item
                var pickupable = _labRobotArm.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _labRobotArm.AddComponent<CustomPlaceToolController>();
                var placeTool = _labRobotArm.AddComponent<GenericPlaceTool>();
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
                placeTool.drawTime = 0.5f;
                placeTool.dropTime = 1;
                placeTool.holsterTime = 0.35f;

                // Associate recipe to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif

                // Add the new TechType to Hand Equipment type.
                CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("robotarmicon"));
#endif

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_labRobotArm);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.1f;
            fabricatingA.localMaxY = 0.8f;
            fabricatingA.posOffset = new Vector3(-0.3f, 0f, 0f);
            fabricatingA.eulerOffset = new Vector3(0f, 90f, 0f);
            fabricatingA.scaleFactor = 0.8f;

            return prefab;
        }
    }
}
