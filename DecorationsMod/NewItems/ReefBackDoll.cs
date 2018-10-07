using SMLHelper;
using SMLHelper.Patchers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class ReefBackDoll : DecorationItem
    {
        public ReefBackDoll() // Feeds abstract class
        {
            this.ClassID = "ReefBackDoll";
            this.ResourcePath = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("reefbackleviathan");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("ReefBackDollName"),
                                                        LanguageHelper.GetFriendlyWord("ReefBackDollDescription"),
                                                        true);

            this.Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.FiberMesh, 1),
                        new IngredientHelper(TechType.Silicone, 1)
                    }),
                _techType = this.TechType
            };
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                // Scale model
                GameObject reefbackModel = this.GameObject.FindChild("Pivot");
                reefbackModel.transform.localScale *= 0.5f;

                // Set tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                this.GameObject.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;
                
                // Add collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.8f, 0.5f, 0.5f);

                // Add large world entity
                this.GameObject.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Texture normal1 = AssetsHelper.Assets.LoadAsset<Texture>("coral_reef_grass_04_normal");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("Coral_reef_red_seaweed_03_normal");
                Texture normal3 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_coral_flat_normal");
                Texture normal4 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_reef_tile_normal");
                Texture normal5 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_01_normal");
                Texture illum5 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_01_illum");
                Texture normal6 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_02_normal");
                Texture spec6 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_02_spec");
                Texture illum6 = AssetsHelper.Assets.LoadAsset<Texture>("Reefback_01_02_illum");
                var renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    foreach (Renderer rend in renderers)
                    {
                        if (rend.materials.Length > 0)
                        {
                            foreach (Material tmpMat in rend.materials)
                            {
                                tmpMat.shader = marmosetUber;
                                if (tmpMat.name.CompareTo("coral_reef_grass_04 (Instance)") == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal1);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                }
                                else if (tmpMat.name.CompareTo("Coral_reef_red_seaweed_03 (Instance)") == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal2);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                }
                                else if (tmpMat.name.CompareTo("Reefback_coral_flat (Instance)") == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal3);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                }
                                else if (tmpMat.name.CompareTo("Reefback_reef_tile (Instance)") == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal4);
                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                }
                                else if (tmpMat.name.CompareTo("Reefback_01_01 (Instance)") == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal5);
                                    tmpMat.SetTexture("_Illum", illum5);
                                    tmpMat.SetFloat("_EmissionLM", 0.8f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                }
                                else if (tmpMat.name.CompareTo("Reefback_01_02 (Instance)") == 0)
                                {
                                    tmpMat.SetTexture("_BumpMap", normal6);
                                    tmpMat.SetTexture("_SpecTex", spec6);
                                    tmpMat.SetTexture("_Illum", illum6);
                                    tmpMat.SetFloat("_EmissionLM", 0.8f); // Set always visible

                                    tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                    tmpMat.EnableKeyword("MARMO_EMISSION");
                                }
                            }
                        }
                    }
                }

                // Add sky applier
                var applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;
                
                // We can pick this item
                var pickupable = this.GameObject.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                var placeTool = this.GameObject.AddComponent<PlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = false;
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

                // Set item occupies 4 slots
                CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(2, 2);

                // Add the new TechType to Hand Equipment type.
                CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("reefbackicon")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.6f;
            fabricatingA.posOffset = new Vector3(0.08f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
