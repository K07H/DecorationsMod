using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class SeaTreaderDoll : DecorationItem
    {
        public SeaTreaderDoll() // Feeds abstract class
        {
            this.ClassID = "SeaTreaderDoll";
            this.ResourcePath = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("seatreaderleviathan");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SeaTreaderDollName"),
                                                        LanguageHelper.GetFriendlyWord("SeaTreaderDollDescription"),
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
                GameObject treaderModel = this.GameObject.FindChild("Sea_Treader");
                GameObject treaderSubModel = treaderModel.FindChild("Sea_Treader_Geo");
                GameObject treaderSubSubModel1 = treaderSubModel.FindChild("Sea_Treader 1");
                GameObject treaderSubSubModel2 = treaderSubModel.FindChild("Sea_Treader_LOD1");
                GameObject treaderSubSubModel3 = treaderSubModel.FindChild("Sea_Treader_LOD2");
                GameObject treaderSubSubModel4 = treaderSubModel.FindChild("Sea_Treader_LOD3");

                // Scale model
                treaderModel.transform.localScale *= 0.8f;

                // Merge submeshes
                Mesh treaderMesh1 = treaderSubSubModel1.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                treaderMesh1.SetTriangles(treaderMesh1.triangles, 0);
                treaderMesh1.subMeshCount = 1;
                Mesh treaderMesh2 = treaderSubSubModel2.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                treaderMesh2.SetTriangles(treaderMesh2.triangles, 0);
                treaderMesh2.subMeshCount = 1;
                Mesh treaderMesh3 = treaderSubSubModel3.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                treaderMesh3.SetTriangles(treaderMesh3.triangles, 0);
                treaderMesh3.subMeshCount = 1;
                Mesh treaderMesh4 = treaderSubSubModel4.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                treaderMesh4.SetTriangles(treaderMesh4.triangles, 0);
                treaderMesh4.subMeshCount = 1;
                
                // Set tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add prefab identifier
                this.GameObject.AddComponent<PrefabIdentifier>().ClassId = this.ClassID;

                // Add rigid body
                /*
                var rb = this.GameObject.AddComponent<Rigidbody>();
                rb.mass = 10;
                */

                // Add collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.8f, 0.5f, 0.5f);

                // Add large world entity
                this.GameObject.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                // Set proper shaders (for crafting animation)
                Shader marmosetUber = Shader.Find("MarmosetUBER");
                var renderers = this.GameObject.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
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
                }

                // Add sky applier
                var applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;

                // Add world forces
                /*
                var forces = this.GameObject.AddComponent<WorldForces>();
                forces.useRigidbody = rb;
                forces.handleGravity = true;
                forces.handleDrag = true;
                forces.aboveWaterGravity = 9.81f;
                forces.underwaterGravity = 1;
                forces.aboveWaterDrag = 0.1f;
                forces.underwaterDrag = 1;
                */

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
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("seatreadericon")));

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
            fabricatingA.scaleFactor = 1f;

            return prefab;
        }
    }
}
