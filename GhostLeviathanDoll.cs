using SMLHelper;
using SMLHelper.Patchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace DecorationsFabricator.NewItems
{
    public class GhostLeviathanDoll : DecorationItem
    {
        public GhostLeviathanDoll() // Feeds abstract class
        {
            this.ClassID = "GhostLeviathanDoll";
            this.ResourcePath = "WorldEntities/Creatures/GhostLeviathan";

            this.GameObject = Resources.Load<GameObject>(this.ResourcePath); // AssetsHelper.Assets.LoadAsset<GameObject>("ghostleviathan");

            this.TechType = TechTypePatcher.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("SmallEmperorName"),
                                                        LanguageHelper.GetFriendlyWord("SmallEmperorDescription"),
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
                // Set item occupies 4 slots
                CraftDataPatcher.customItemSizes[this.TechType] = new Vector2int(2, 2);

                // Add the new TechType to Hand Equipment type.
                CraftDataPatcher.customEquipmentTypes.Add(this.TechType, EquipmentType.Hand);

                // Set the buildable prefab
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(this.ClassID, this.ResourcePath, this.TechType, GetPrefab));

                // Set the custom sprite
                CustomSpriteHandler.customSprites.Add(new CustomSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("emperoricon")));

                // Associate recipe to the new TechType
                CraftDataPatcher.customTechData[this.TechType] = this.Recipe;

                this.IsRegistered = true;
            }
        }

        public override GameObject GetPrefab()
        {
            Logger.Log("DEBUG A");
            GameObject prefab = GameObject.Instantiate(this.GameObject);
            GameObject model = prefab.FindChild("model");
            
            // Get collider
            var collider = prefab.GetComponent<SphereCollider>();

            collider.radius = collider.radius * 0.01f;
            //collider.center = new Vector3(collider.center.x, collider.center.y + 0.4f, collider.center.z);
            //collider.attachedRigidbody.transform.localPosition = new Vector3(collider.attachedRigidbody.transform.localPosition.x, collider.attachedRigidbody.transform.localPosition.y + 0.4f, collider.attachedRigidbody.transform.localPosition.z);
            //collider.contactOffset *= 2;
            Logger.Log("DEBUG B");
            VFXSurface vfxSurface = prefab.GetComponent<VFXSurface>();
            vfxSurface.transform.localScale *= 0.01f;
            
            /*
            VFXSchoolFishRepulsor vfxSFR = prefab.GetComponent<VFXSchoolFishRepulsor>();
            vfxSFR.transform.localScale *= 0.01f;

            VFXController[] vfxControllers = prefab.GetComponents<VFXController>();
            if (vfxControllers.Length > 0)
            {
                foreach (VFXController vfxController in vfxControllers)
                {
                    if (vfxController.transform != null)
                        vfxController.transform.localScale *= 0.01f;
                }
            }
            */

            // Scale
            model.transform.localScale *= 0.01f;

            // Rotate
            model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y + 90.0f, model.transform.localEulerAngles.z);

            //model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.4f, model.transform.localPosition.z);
            
            // Merge submeshes
            /*
            GameObject emperorModel = this.GameObject.FindChild("model");
            GameObject submodel = emperorModel.FindChild("Ghost_Leviathan_anim");
            GameObject subsubmodel = submodel.FindChild("Ghost_Leviathan_geo");
            Mesh emperorMesh = emperorModel.GetComponent<MeshFilter>().mesh;
            if (emperorMesh != null)
            {
                emperorMesh.SetTriangles(emperorMesh.triangles, 0);
                emperorMesh.subMeshCount = 1;
            }
            else
                Logger.Log("DEBUG: Cannot find ghost leviathan model!!!!");
                */
            var entityTag = prefab.GetComponent<EntityTag>();
            GameObject.DestroyImmediate(entityTag);
            
            var aggressiveOnDamage = prefab.GetComponent<AggressiveOnDamage>();
            GameObject.DestroyImmediate(aggressiveOnDamage);

            var creatureDeath = prefab.GetComponent<CreatureDeath>();
            GameObject.DestroyImmediate(creatureDeath);
            Logger.Log("DEBUG C");
            var swimRandom = prefab.GetComponent<SwimRandom>();
            GameObject.DestroyImmediate(swimRandom);

            var stayAtLeashPosition = prefab.GetComponent<StayAtLeashPosition>();
            GameObject.DestroyImmediate(stayAtLeashPosition);
            
            var meleeAttack = prefab.GetComponent<GhostLeviathanMeleeAttack>();
            GameObject.DestroyImmediate(meleeAttack);

            var fleeOnDamage = prefab.GetComponent<FleeOnDamage>();
            GameObject.DestroyImmediate(fleeOnDamage);
            
            var attackLastTarget = prefab.GetComponent<AttackLastTarget>();
            GameObject.DestroyImmediate(attackLastTarget);

            var aggressiveWhenSeeTarget = prefab.GetComponent<AggressiveWhenSeeTarget>();
            GameObject.DestroyImmediate(aggressiveWhenSeeTarget);

            var attackCyclops = prefab.GetComponent<AttackCyclops>();
            GameObject.DestroyImmediate(attackCyclops);
            Logger.Log("DEBUG E");
            var avoidTerrain = prefab.GetComponent<AvoidTerrain>();
            GameObject.DestroyImmediate(avoidTerrain);

            var FMOD_1 = prefab.GetComponent<FMOD_StudioEventEmitter>();
            GameObject.DestroyImmediate(FMOD_1);

            var FMOD_2 = prefab.GetComponent<FMOD_CustomEmitter>();
            GameObject.DestroyImmediate(FMOD_2);

            var FMOD_3 = prefab.GetComponent<FMOD_CustomLoopingEmitter>();
            GameObject.DestroyImmediate(FMOD_3);

            var FMOD_4 = prefab.GetComponent<FMOD_CustomLoopingEmitterWithCallback>();
            GameObject.DestroyImmediate(FMOD_4);
            
            /*
            var creatureUtils = prefab.GetComponent<CreatureUtils>();

            creatureUtils.setupEcoTarget = false;
            creatureUtils.setupEcoBehaviours = false;
            */

            var ecoTarget = prefab.GetComponent<EcoTarget>();
            GameObject.DestroyImmediate(ecoTarget);
            Logger.Log("DEBUG F");
            var ghostLeviathan = prefab.GetComponent<GhostLeviathan>();
            ghostLeviathan.liveMixin = null;
            ghostLeviathan.detectsMotion = false;

            /*Component[] glcomponents = ghostLeviathan.GetComponentsInChildren<Component>();
            foreach (Component component in glcomponents)
            {
                Logger.Log("GhostLeviathan class component: Name=[" + component.GetType().ToString() + "]");
            }*/

            LODGroup lodGroup = prefab.GetComponent<LODGroup>();
            GameObject.DestroyImmediate(lodGroup);

            BehaviourLOD behaviourLOD = prefab.GetComponent<BehaviourLOD>();
            GameObject.DestroyImmediate(behaviourLOD);
            Logger.Log("DEBUG G");
            /*Component[] components = prefab.GetComponents<Component>();
            foreach (Component component in components)
            {
                Logger.Log("GhostLeviathan componant: Name=[" + component.GetType().ToString() + "]");
            }*/

            // Set tech tag
            var techTag = prefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Update prefab identifier
            prefab.GetComponent<PrefabIdentifier>().ClassId = this.ClassID;

            // Add rigid body
            var rb = prefab.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;
            rb.hideFlags = HideFlags.None;

            // Update large world entity
            var lwe = prefab.GetComponent<LargeWorldEntity>();
            lwe.cellLevel = LargeWorldEntity.CellLevel.Near;
            Logger.Log("DEBUG H");
            // Set proper shaders (for crafting animation)
            Shader marmosetUber = Shader.Find("MarmosetUBER");
            var rend = model.GetComponentInChildren<Renderer>();
            if (rend != null)
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
            /*
            // Add sky applier
            var applier = prefab.AddComponent<SkyApplier>();
            applier.renderers = new Renderer[] { rend };
            applier.anchorSky = Skies.Auto;

            // Add world forces
            var forces = prefab.AddComponent<WorldForces>();
            forces.useRigidbody = rb;
            forces.handleGravity = true;
            forces.handleDrag = true;
            forces.aboveWaterGravity = 9.81f;
            forces.underwaterGravity = 1;
            forces.aboveWaterDrag = 0.1f;
            forces.underwaterDrag = 1;
            */
            // We can pick this item
            var pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            Logger.Log("DEBUG I");
            // We can place this item
            var placeTool = prefab.AddComponent<TestMyMonoBehaviour>(); //prefab.AddComponent<PlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = false;
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
            //placeTool.alignWithSurface = false;

            Logger.Log("DEBUG J");
            /*
            FieldInfo fieldInfo = typeof(PlaceTool).GetField("localBounds", BindingFlags.NonPublic | BindingFlags.Static);
            //List<OrientedBounds> localBounds
            fieldInfo.SetValue(placeTool, powerRelay);
            */
            /*
            prefab.transform.localPosition = new Vector3(prefab.transform.localPosition.x, prefab.transform.localPosition.y + 0.4f, prefab.transform.localPosition.z);
            placeTool.ghostModelPrefab = prefab;
            
            Component[] ptcomponents = placeTool.GetComponentsInChildren<Component>();
            foreach (Component component in ptcomponents)
            {
                Logger.Log("GhostLeviathan PlaceTool component: Name=[" + component.GetType().ToString() + "]");
            }
            */
            /*
            FieldInfo fieldInfo = typeof(PlaceTool).GetField("ghostModel", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo fieldInfo2 = typeof(PlaceTool).GetField("ghostStructureMaterial", BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo fieldInfo3 = typeof(PlaceTool).GetField("renderers", BindingFlags.NonPublic | BindingFlags.Instance);

            GameObject original = (!(placeTool.ghostModelPrefab != null)) ? prefab : placeTool.ghostModelPrefab;
            GameObject ghostModel = UnityEngine.Object.Instantiate<GameObject>(original);
            GameObject ghostModelModel = ghostModel.FindChild("model");
            ghostModelModel.transform.localPosition = new Vector3(ghostModelModel.transform.localPosition.x, ghostModelModel.transform.localPosition.y + 0.4f, ghostModelModel.transform.localPosition.z);
            fieldInfo.SetValue(placeTool, ghostModel);
            Material ghostStructureMaterial = new Material(Resources.Load<Material>("Materials/ghostmodel"));
            fieldInfo2.SetValue(placeTool, ghostStructureMaterial);
            fieldInfo3.SetValue(placeTool, ghostModel.GetComponentsInChildren<Renderer>());
            MaterialExtensions.AssignMaterial(ghostModel, ghostStructureMaterial);
            foreach (MonoBehaviour monoBehaviour in ghostModel.GetComponentsInChildren<MonoBehaviour>(true))
            {
                if (monoBehaviour)
                {
                    monoBehaviour.enabled = false;
                }
            }
            foreach (Collider tmpcollider in ghostModel.GetComponentsInChildren<Collider>(true))
            {
                tmpcollider.enabled = true;
            }
            */

            //placeTool.ghostModelPrefab.transform.localScale *= 0.01f;
            //placeTool.ghostModelPrefab.transform.localPosition = new Vector3(placeTool.ghostModelPrefab.transform.localPosition.x, placeTool.ghostModelPrefab.transform.localPosition.y + 0.4f, placeTool.ghostModelPrefab.transform.localPosition.z);

            // Add fabricating animation
            var fabricatingA = prefab.AddComponent<VFXFabricating>();
            fabricatingA.localMinY = -0.2f;
            fabricatingA.localMaxY = 0.6f;
            fabricatingA.posOffset = new Vector3(0.1f, 0f, 0.04f);
            fabricatingA.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricatingA.scaleFactor = 0.1f;

            Logger.Log("DEBUG K");
            return prefab;
        }
    }
}
