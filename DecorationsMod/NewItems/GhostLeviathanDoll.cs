using DecorationsMod.Controllers;
using DecorationsMod.Fixers;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class GhostLeviathanDoll : DecorationItem
    {
        public GhostLeviathanDoll()
        {
            this.ClassID = "GhostLeviathanDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            //this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("ghostleviathan");
            this.GameObject = new GameObject(this.ClassID);

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("GhostLeviathanDollName"),
                                                        LanguageHelper.GetFriendlyWord("GhostLeviathanDollDescription"),
                                                        true);

            CrafterLogicFixer.GhostLeviathanDoll = this.TechType;
            KnownTechFixer.AddedNotifications.Add((int)this.TechType, false);

#if SUBNAUTICA
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[3]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.FiberMesh, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Silicone, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[3]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.FiberMesh, 1),
                        new Ingredient(TechType.Silicone, 1)
                    }),
            };
#endif
        }

        private static GameObject _ghostLeviathanDoll = null;

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                if (_ghostLeviathanDoll == null)
                    _ghostLeviathanDoll = AssetsHelper.Assets.LoadAsset<GameObject>("ghostleviathan");

                GameObject model = _ghostLeviathanDoll.FindChild("model");

                // Scale
                model.transform.localScale *= 0.47f;

                // Rotate
                model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + -25.0f, model.transform.localEulerAngles.z);
                
                // Set tech tag
                var techTag = _ghostLeviathanDoll.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                _ghostLeviathanDoll.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;
                
                // Add collider
                var collider = _ghostLeviathanDoll.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.7f, 0.08f, 0.08f);
                collider.center = new Vector3(collider.center.x - 0.15f, collider.center.y + 0.1f, collider.center.z);

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("Ghost_Leviathan_inside_normal");
                Texture spec = AssetsHelper.Assets.LoadAsset<Texture>("Ghost_Leviathan_inside_spec");
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("Ghost_Leviathan_inside_illum");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Ghost_Leviathan_shell_normal");
                Texture spec2 = AssetsHelper.Assets.LoadAsset<Texture>("Ghost_Leviathan_shell_spec");
                Texture illum2 = AssetsHelper.Assets.LoadAsset<Texture>("OLD_Ghost_Leviathan_shell_illum");
                var renderers = _ghostLeviathanDoll.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                if (string.Compare(tmpMat.name, "Ghost_Leviathan_inside (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.shader = marmosetUber;
                                    tmpMat.SetTexture("_BumpMap", normal);
                                    tmpMat.SetTexture("_SpecTex", spec);
                                    tmpMat.SetTexture("_Illum", illum);
                                    tmpMat.SetFloat("_EmissionLM", 1.0f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                                else if (string.Compare(tmpMat.name, "Ghost_Leviathan_shell_test (Instance)", true, CultureInfo.InvariantCulture) == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal2);
                                    tmpMat.SetTexture("_SpecTex", spec2);
                                    tmpMat.SetTexture("_Illum", illum2);
                                    tmpMat.SetFloat("_EmissionLM", 1.0f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                    tmpMat.EnableKeyword("MARMO_SPECMAP");
                                    tmpMat.EnableKeyword("_ZWRITE_ON"); // Enable Z write
                                }
                            }
                        }
                    }
                }

                // Add large world entity
                _ghostLeviathanDoll.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
                
                // Add sky applier
                var applier = _ghostLeviathanDoll.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;

                // We can pick this item
                var pickupable = _ghostLeviathanDoll.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                _ghostLeviathanDoll.AddComponent<CustomPlaceToolController>();
                var placeTool = _ghostLeviathanDoll.AddComponent<GenericPlaceTool>();
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

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                // Set item occupies 4 slots
                SMLHelper.V2.Handlers.CraftDataHandler.SetItemSize(this.TechType, new Vector2int(2, 2));

                // Add the new TechType to Hand Equipment type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(this.TechType, EquipmentType.Hand);

                // Set quick slot type.
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType.Selectable);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("ghostleviathanicon"));

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(_ghostLeviathanDoll);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.6f;
            fabricatingA.posOffset = new Vector3(0.22f, -0.03f, 0.12f);
            fabricatingA.eulerOffset = new Vector3(0f, 20f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
