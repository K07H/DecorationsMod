using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class CuddleFishDoll : DecorationItem
    {
        public CuddleFishDoll() // Feeds abstract class
        {
            this.ClassID = "CuddleFishDoll";
            this.ResourcePath = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("cuddlefish");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("CuddleFishDollName"),
                                                        LanguageHelper.GetFriendlyWord("CuddleFishDollDescription"),
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
                GameObject model = this.GameObject.FindChild("cutefish");

                // Scale model
                model.transform.localScale *= 1.8f;

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.35f, model.transform.localPosition.z);
                
                // Destroy animation related
                GameObject submodel = model.FindChild("model");
                GameObject subsubmodel = submodel.FindChild("cute_fish_anims");
                GameObject subsubsubmodel = subsubmodel.FindChild("Cute_fish_geo1");

                var animator = subsubmodel.GetComponent<Animator>();
                var animateByVelocity = subsubmodel.GetComponent<AnimateByVelocity>();
                var onPlayerCinematic = subsubmodel.GetComponent<OnPlayerCinematicModeEndForward>();
                if (animator != null)
                    GameObject.DestroyImmediate(animator);
                if (animateByVelocity != null)
                    GameObject.DestroyImmediate(animateByVelocity);
                if (onPlayerCinematic != null)
                    GameObject.DestroyImmediate(onPlayerCinematic);

                // Merge submeshes
                Mesh cuddlefishSubMesh1 = subsubsubmodel.FindChild("qute_fish_eye_L").GetComponent<SkinnedMeshRenderer>().sharedMesh;
                if (cuddlefishSubMesh1 != null)
                {
                    cuddlefishSubMesh1.SetTriangles(cuddlefishSubMesh1.triangles, 0);
                    cuddlefishSubMesh1.subMeshCount = 1;
                }
                Mesh cuddlefishSubMesh2 = subsubsubmodel.FindChild("qute_fish_eye_R").GetComponent<SkinnedMeshRenderer>().sharedMesh;
                if (cuddlefishSubMesh2 != null)
                {
                    cuddlefishSubMesh2.SetTriangles(cuddlefishSubMesh2.triangles, 0);
                    cuddlefishSubMesh2.subMeshCount = 1;
                }
                Mesh cuddlefishMesh = subsubsubmodel.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                if (cuddlefishMesh != null)
                {
                    cuddlefishMesh.SetTriangles(cuddlefishMesh.triangles, 0);
                    cuddlefishMesh.subMeshCount = 1;
                }
                
                // Set tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Set prefab identifier
                var prefabId = this.GameObject.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Set collider
                var collider = this.GameObject.AddComponent<SphereCollider>();
                collider.radius = 0.2f;
                //collider.size = new Vector3(0.4f, 0.6f, 0.4f);

                // Set large world entity
                var lwe = this.GameObject.AddComponent<LargeWorldEntity>();
                lwe.cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                Renderer[] renderers = new Renderer[3]
                {
                    subsubsubmodel.GetComponent<SkinnedMeshRenderer>(),
                    subsubsubmodel.FindChild("qute_fish_eye_L").GetComponent<SkinnedMeshRenderer>(),
                    subsubsubmodel.FindChild("qute_fish_eye_R").GetComponent<SkinnedMeshRenderer>()
                };
                foreach (Renderer rend in renderers)
                {
                    rend.material.shader = marmosetUber;
                    if (rend.materials.Length > 0)
                    {
                        foreach (Material tmpMat in rend.materials)
                        {
                            tmpMat.shader = marmosetUber;
                        }
                    }
                }

                // Update sky applier
                var applier = this.GameObject.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;

                // We can pick this item
                var pickupable = this.GameObject.AddComponent<Pickupable>();
                pickupable.isPickupable = true;
                pickupable.randomizeRotationWhenDropped = true;

                // We can place this item
                var placeTool = this.GameObject.AddComponent<PlaceTool>();
                placeTool.allowedInBase = true;
                placeTool.allowedOnBase = true;
                placeTool.allowedOnCeiling = false;
                placeTool.allowedOnConstructable = true;
                placeTool.allowedOnGround = true;
                placeTool.allowedOnRigidBody = true;
                placeTool.allowedOnWalls = false;
                placeTool.allowedOutside = false;
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
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("cuddlefishicon")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.8f;
            fabricatingA.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 0.8f;

            return prefab;
        }
    }
}
