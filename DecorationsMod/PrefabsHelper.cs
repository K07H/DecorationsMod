using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DecorationsMod
{
    public static class PrefabsHelper
    {
        #region Prefab

        /// <summary>
        /// Returns decoration item TechType giving the list of decoration items and the item's classID.
        /// </summary>
        public static TechType GetTechType(List<IDecorationItem> decorationItems, string classID)
        {
            foreach (IDecorationItem item in decorationItems)
            {
                if (string.Compare(item.ClassID, classID, true, CultureInfo.InvariantCulture) == 0)
                    return item.TechType;
            }
            return 0;
        }

        public static void SetDefaultPickupable(GameObject gameObj, bool isPickupable = true, bool destroyOnDeath = false, bool isValidHandTarget = false, bool attached = false,
            TechType overrideTechType = TechType.None, bool overrideTechUsed = false)
        {
            if (gameObj == null)
                return;
            Pickupable pickupable = gameObj.GetComponent<Pickupable>();
            if (pickupable == null)
                pickupable = gameObj.AddComponent<Pickupable>();
            pickupable.attached = attached;
            pickupable.isPickupable = isPickupable;
            pickupable.isValidHandTarget = isValidHandTarget;
            pickupable.overrideTechType = overrideTechType;
            pickupable.overrideTechUsed = overrideTechUsed;
            pickupable.isLootCube = false;
            pickupable.destroyOnDeath = destroyOnDeath;
            pickupable.version = 0;
            pickupable.isKinematic = PickupableKinematicState.NoKinematicStateSet;
            pickupable.randomizeRotationWhenDropped = true;
#if BELOWZERO
            pickupable.activateRigidbodyWhenDropped = true;
#endif
            pickupable.usePackUpIcon = false;
            pickupable.hideFlags = HideFlags.None;
            pickupable.useGUILayout = true;
            pickupable.enabled = true;
        }

        public static void SetDefaultPlaceTool(GameObject gameObj, Collider mainCollider = null, Pickupable pickupable = null,
            bool onCeiling = false, bool onWalls = false, bool onBase = false, bool alignWithSurface = false)
        {
            if (gameObj == null)
                return;

            if (gameObj.GetComponent<CustomPlaceToolController>() == null)
                gameObj.AddComponent<CustomPlaceToolController>();

            PlaceTool placeTool = gameObj.GetComponent<GenericPlaceTool>(); 
            if (placeTool == null)
                placeTool = gameObj.GetComponent<PlaceTool>();
            if (placeTool == null)
                placeTool = gameObj.AddComponent<GenericPlaceTool>();

            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = onBase;
            placeTool.allowedOnCeiling = onCeiling;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = true;
            placeTool.allowedOnWalls = onWalls;
            placeTool.allowedOutside = ConfigSwitcher.AllowPlaceOutside;
#if BELOWZERO
            placeTool.allowedUnderwater = true;
#endif
            placeTool.reloadMode = PlayerTool.ReloadMode.None;
            placeTool.socket = PlayerTool.Socket.RightHand;
            placeTool.rotationEnabled = true;
            placeTool.hasAnimations = false;
            placeTool.hasBashAnimation = false;
            placeTool.hasFirstUseAnimation = false;
            // Try get collider
            if (mainCollider == null)
                mainCollider = gameObj.GetComponent<Collider>();
            if (mainCollider == null)
                mainCollider = gameObj.GetComponentInChildren<Collider>();
            // Associate collider
            if (mainCollider != null)
                placeTool.mainCollider = mainCollider;
            // Try get pickupable
            if (pickupable == null)
                pickupable = gameObj.GetComponent<Pickupable>();
            // Associate pickupable
            placeTool.pickupable = pickupable;
            placeTool.drawTime = 0.5f;
            placeTool.dropTime = 1f;
            placeTool.holsterTime = 0.35f;
            placeTool.bleederDamage = 3f;
#if BELOWZERO
            placeTool.spikeyTrapDamage = 1f;
#endif
            placeTool.ikAimLeftArm = false;
            placeTool.ikAimRightArm = false;
            placeTool.useLeftAimTargetOnPlayer = false;
            placeTool.alignWithSurface = alignWithSurface;
            placeTool.hideInvalidGhostModel = false;
            placeTool.hideFlags = HideFlags.None;
            placeTool.useGUILayout = true;
            placeTool.enabled = true;
        }

        public static void SetDefaultRigidBody(GameObject gameObj, CollisionDetectionMode collisionDetectionMode = CollisionDetectionMode.Discrete,
            float mass = 1f, float drag = 0f, float angularDrag = 0.05f, bool detectCollisions = true)
        {
            if (gameObj == null)
                return;
            Rigidbody rigidBody = gameObj.GetComponent<Rigidbody>();
            if (rigidBody == null)
                rigidBody = gameObj.AddComponent<Rigidbody>();
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.drag = drag;
            rigidBody.angularDrag = angularDrag;
            rigidBody.mass = mass;
            rigidBody.useGravity = true;
            rigidBody.isKinematic = true;
            rigidBody.freezeRotation = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.collisionDetectionMode = collisionDetectionMode;
            rigidBody.centerOfMass = Vector3.zero;
            rigidBody.inertiaTensorRotation = Quaternion.identity;
            rigidBody.inertiaTensor = Vector3.one;
            rigidBody.detectCollisions = detectCollisions;
            rigidBody.position = Vector3.zero;
            rigidBody.rotation = Quaternion.identity;
            rigidBody.interpolation = RigidbodyInterpolation.None;
            rigidBody.solverIterations = 6;
            rigidBody.sleepThreshold = 0.005f;
            rigidBody.maxAngularVelocity = 7f;
            rigidBody.solverVelocityIterations = 1;
            rigidBody.hideFlags = HideFlags.None;
        }

        public static void SetDefaultLargeWorldEntity(GameObject gameObj, LargeWorldEntity.CellLevel cellLevel = LargeWorldEntity.CellLevel.Near, int updaterIndex = 0)
        {
            if (gameObj == null)
                return;
            LargeWorldEntity lwe = gameObj.GetComponent<LargeWorldEntity>();
            if (lwe == null)
                lwe = gameObj.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = cellLevel;
            lwe.updaterIndex = updaterIndex;
            lwe.hideFlags = HideFlags.None;
            lwe.useGUILayout = true;
            lwe.enabled = true;
        }

        public static void UpdateExistingLargeWorldEntities(GameObject gameObj, LargeWorldEntity.CellLevel cellLevel = LargeWorldEntity.CellLevel.Near, int updaterIndex = -1)
        {
            if (gameObj == null)
                return;
            LargeWorldEntity[] entities = gameObj.GetComponentsInChildren<LargeWorldEntity>();
            if (entities != null)
                foreach (LargeWorldEntity lwe in entities)
                {
                    lwe.cellLevel = cellLevel;
                    if (updaterIndex >= 0)
                        lwe.updaterIndex = updaterIndex;
                }
        }

        public static void ReplaceSkyApplier(GameObject gameObj, bool getInChildren = false)
        {
            if (gameObj == null)
                return;
            SkyApplier sa = getInChildren ? gameObj.GetComponentInChildren<SkyApplier>() : gameObj.GetComponent<SkyApplier>();
            if (sa != null)
                Object.DestroyImmediate(sa);
            sa = gameObj.AddComponent<SkyApplier>();
            sa.renderers = gameObj.GetComponentsInChildren<Renderer>();
            sa.anchorSky = Skies.Auto;
            sa.enabled = true;
        }

        public static void SetDefaultSkyApplier(GameObject gameObj, Renderer[] renderers = null, Skies anchorSky = Skies.Auto,
            bool dynamic = false, bool emissiveFromPower = false)
        {
            if (gameObj == null)
                return;
            SkyApplier applier = gameObj.GetComponent<SkyApplier>();
            if (applier == null)
                applier = gameObj.AddComponent<SkyApplier>();
            if (renderers == null)
                renderers = gameObj.GetComponentsInChildren<Renderer>();
            if (renderers != null)
                applier.renderers = renderers;
            applier.anchorSky = anchorSky;
            applier.dynamic = dynamic;
            applier.emissiveFromPower = emissiveFromPower;
            applier.hideFlags = HideFlags.None;
            applier.useGUILayout = true;
            applier.enabled = true;
        }

        private static GameObject _objPositioner = null;
        public static Vector3 Translate(Vector3 pos, Quaternion rot, Vector3 to, Space space = Space.Self)
        {
            Vector3 result = pos;
            if (_objPositioner == null)
                _objPositioner = new GameObject("dummyPositioner");
            if (_objPositioner != null)
            {
                _objPositioner.transform.position = pos;
                _objPositioner.transform.rotation = rot;
                _objPositioner.transform.Translate(to, space);
                result = _objPositioner.transform.position;
            }
            return result;
        }

        public static bool IsNear(Vector3 a, Vector3 b) => a.x > (b.x - 1.0f) && a.x < (b.x + 1.0f) && a.y > (b.y - 1.0f) && a.y < (b.y + 1.0f) && a.z > (b.z - 1.0f) && a.z < (b.z + 1.0f);

        #endregion

        #region PlaceTools sky appliers

        // object name, model
        private static readonly Dictionary<string, string> _placeToolSAFix = new Dictionary<string, string>()
        {
            { "FirstAidKit", "model" },
            { "Lubricant", "model" },
            { "Bleach", "model" },
            { "DisinfectedWater", "model" },
            { "FilteredWater", "model" },
            { "WiringKit", "model" },
            { "AdvancedWiringKit", "model" },
            { "ComputerChip", "model" },
            { "Battery", "model" },
            { "LithiumIonBattery", "model" },
            { "PrecursorIonBattery", "model" },
            { "Silicone", "model" },
            { "FiberMesh", "model" },
            { "TitaniumIngot", "model" },
            { "PlasteelIngot", "model" },
            { "Glass", "model" },
            { "EnameledGlass", "model" },
            { "CopperWire", "model" },
            { "SeaTreaderPoop", "sea_treader_poop_01" }
        };

        public static void FixPlaceToolSkyAppliers(GameObject go)
        {
#if DEBUG_PLACE_TOOL
            Logger.Log("DEBUG: FIX PT-SA: goName=[" + go.name + "]");
#endif
            foreach (KeyValuePair<string, string> placeTool in _placeToolSAFix)
                if (go.name.StartsWith(placeTool.Key, false, CultureInfo.InvariantCulture))
                {
                    GameObject model = go.FindChild(placeTool.Value);
                    if (model != null)
                    {
                        SkyApplier[] sas = model.GetComponents<SkyApplier>();
                        if (sas == null || sas.Length != 1)
                            sas = model.GetComponentsInParent<SkyApplier>();
                        if (sas != null && sas.Length == 1)
                        {
                            Object.DestroyImmediate(sas[0]); // Prevent accumulation of SkyAppliers
                            SkyApplier sa = model.AddComponent<SkyApplier>();
                            if (sa != null)
                            {
                                sa.anchorSky = Skies.Auto;
                                Renderer[] rends = model.GetAllComponentsInChildren<Renderer>();
                                if (rends == null || rends.Length <= 0)
                                    rends = model.GetComponents<Renderer>();
                                sa.renderers = rends;
                                sa.dynamic = true;
                                sa.updaterIndex = 0;
                                if (placeTool.Key == "PlasteelIngot" || placeTool.Key == "TitaniumIngot" || placeTool.Key == "CopperWire" || placeTool.Key == "Glass" || placeTool.Key == "EnameledGlass" || placeTool.Key == "SeaTreaderPoop")
                                    sa.emissiveFromPower = true;
                                sa.enabled = true;
                                sa.RefreshDirtySky();
                            }
#if DEBUG_PLACE_TOOL
                            else
                            {
                                Logger.Log("DEBUG: FIX PT-SA: SkyApplier 1 do not match! sasLength=[" + (sas != null ? sas.Length.ToString() : "null") + "] goName=[" + go.name + "] modelName=[" + model.name + "]");
                                Logger.PrintTransform(go.transform);
                            }
                        }
                        else
                        {
                            Logger.Log("DEBUG: FIX PT-SA: SkyAppliers 2 do not match! sasLength=[" + (sas != null ? sas.Length.ToString() : "null") + "] goName=[" + go.name + "] modelName=[" + model.name + "]");
                            Logger.PrintTransform(go.transform);
                        }
                    }
                    else
                    {
                        Logger.Log("DEBUG: FIX PT-SA: Could not find model!");
                        Logger.PrintTransform(go.transform);
                    }
#else
                        }
                    }
#endif
                }
        }

#endregion

        #region Seeds/plants

        private static GameObject _genericSeed = null;
        public static bool AddNewGenericSeed(ref GameObject go)
        {
            if (_genericSeed == null)
                _genericSeed = Resources.Load<GameObject>("WorldEntities/Seeds/fernpalmseed");
            if (_genericSeed != null)
            {
                GameObject newSeed = GameObject.Instantiate(_genericSeed).FindChild("Generic_plant_seed");
                newSeed.transform.parent = go.transform;
                var fabricating = newSeed.AddComponent<VFXFabricating>();
                fabricating.localMinY = -0.09f;
                fabricating.localMaxY = 0.11f;
                fabricating.posOffset = new Vector3(0.0f, 0.07f, 0.0f);
                fabricating.eulerOffset = new Vector3(0.0f, 0.0f, 90.0f);
                fabricating.scaleFactor = 1.0f;
                return true;
            }
            return false;
        }

        private static readonly List<string> RepositionSeeds = new List<string>(new string[3]
        {
            "LandTree1",
            "MarbleMelonTiny",
            "MarbleMelonTinyFruit"
        });

        private static readonly List<string> ShowPlantWhenDropped = new List<string>(new string[1]
        {
            "MarbleMelonTinyFruit"
        });

        private static readonly List<string> EnableSphereColliderWhenDropped = new List<string>(new string[2]
        {
            "MushroomTree1",
            "MushroomTree2"
        });

        public static void ShowPlantAndHideSeed(Transform transform, string classId = null)
        {
            if (transform != null)
            {
                bool isNotSeed;
                foreach (Transform tr in transform)
                {
                    isNotSeed = (string.IsNullOrEmpty(tr.name) || !tr.name.StartsWith("Generic_plant_seed", true, CultureInfo.InvariantCulture));
                    Renderer[] renderers = tr.GetComponents<Renderer>();
                    if (renderers != null)
                        foreach (Renderer renderer in renderers)
                            renderer.enabled = isNotSeed;
                    renderers = tr.GetAllComponentsInChildren<Renderer>();
                    if (renderers != null)
                        foreach (Renderer renderer in renderers)
                            renderer.enabled = isNotSeed;
                }
                Pickupable p = transform.GetComponent<Pickupable>();
                if (p != null)
                    p.isPickupable = false;
                if (EnableSphereColliderWhenDropped.Contains(classId))
                {
                    SphereCollider sc = null;
                    if (p != null && p.gameObject != null)
                        sc = p.gameObject.GetComponent<SphereCollider>();
                    if (sc == null)
                        sc = transform.GetComponent<SphereCollider>();
                    if (sc != null)
                        sc.enabled = false;
                }
            }
        }

        public static void HidePlantAndShowSeed(Transform transform, string classId = null)
        {
#if DEBUG_SEEDS
            Logger.Log("DEBUG: Entering HidePlantAndShowSeed for classId=[" + (classId ?? "?") + "]");
#endif
            if (transform != null)
            {
                bool isSeed;
                foreach (Transform tr in transform)
                {
                    isSeed = (!string.IsNullOrEmpty(tr.name) && tr.name.StartsWith("Generic_plant_seed", true, CultureInfo.InvariantCulture));
                    if (isSeed && RepositionSeeds.Contains(classId))
                        tr.localPosition = Vector3.zero;
                    bool invert = ShowPlantWhenDropped.Contains(classId);
                    Renderer[] renderers = tr.GetComponents<Renderer>();
                    if (renderers != null)
                        foreach (Renderer renderer in renderers)
                            renderer.enabled = invert ? !isSeed : isSeed;
                    renderers = tr.GetAllComponentsInChildren<Renderer>();
                    if (renderers != null)
                        foreach (Renderer renderer in renderers)
                            renderer.enabled = invert ? !isSeed : isSeed;
                }
                Pickupable p = transform.GetComponent<Pickupable>();
                if (p != null)
                    p.isPickupable = true;
                if (EnableSphereColliderWhenDropped.Contains(classId))
                {
                    SphereCollider sc = null;
                    if (p != null && p.gameObject != null)
                        sc = p.gameObject.GetComponent<SphereCollider>();
                    if (sc == null)
                        sc = transform.GetComponent<SphereCollider>();
                    if (sc != null)
                        sc.enabled = true;
                }
            }
        }

        #endregion

        #region Aquariums

        private static bool _aquariumSkyApplierFixed = false; // This to ensure we fix sky appliers only once.
        public static void FixAquariumSkyApplier()
        {
            if (!PrefabsHelper._aquariumSkyApplierFixed)
            {
                Logger.Log("INFO: Applying fix to aquariums lighting...");

                GameObject aquariumPrefab = Resources.Load<GameObject>("Submarine/Build/Aquarium");

#if SUBNAUTICA
                SkyApplier[] sas = aquariumPrefab.GetComponents<SkyApplier>();
                List<Renderer> rendsA = new List<Renderer>();
                List<Renderer> rendsB = new List<Renderer>();
#else
                SkyApplier[] sas = aquariumPrefab.GetComponentsInChildren<SkyApplier>();
#endif

                int i = 0;
                if (sas != null)
                    foreach (SkyApplier sa in sas)
                    {
                        if (sa.renderers != null)
                        {
                            foreach (Renderer rend in sa.renderers)
                                if (!string.IsNullOrEmpty(rend.name) && string.Compare(rend.name, "Aquarium", true, CultureInfo.InvariantCulture) != 0)
                                {
                                    if (ConfigSwitcher.GlowingAquariumGlass && string.Compare(rend.name, "Aquarium_glass", true, CultureInfo.InvariantCulture) == 0 && rend.materials != null)
                                    {
                                        foreach (Material mat in rend.materials)
                                        {
                                            mat.EnableKeyword("MARMO_EMISSION");
                                            mat.EnableKeyword("MARMO_GLOW");
                                            mat.SetFloat("_SpecInt", 16f);
                                            mat.SetFloat("_EnableGlow", 1);
                                            mat.SetColor("_GlowColor", new Color(0.79f, 0.9785799f, 1.0f, 0.08f));
                                            mat.SetFloat("_GlowStrength", 0.18f);
                                        }
                                    }
#if SUBNAUTICA
                                    if (i == 0)
                                        rendsA.Add(rend);
                                    else
                                        rendsB.Add(rend);
#endif
                                }
                        }
                        i++;
                    }
#if SUBNAUTICA
                sas[0].renderers = rendsA.ToArray();
                sas[1].renderers = rendsB.ToArray();
#endif

                Renderer[] tmpRends = aquariumPrefab.GetAllComponentsInChildren<Renderer>();
                List<Renderer> rends = new List<Renderer>();
                if (tmpRends != null)
                    foreach (Renderer tmpRend in tmpRends)
                        if (!string.IsNullOrEmpty(tmpRend.name) && string.Compare(tmpRend.name, "Aquarium", true, CultureInfo.InvariantCulture) == 0)
                            rends.Add(tmpRend);

                var fixedSa = aquariumPrefab.AddComponent<SkyApplier>();
                fixedSa.anchorSky = Skies.Auto;
                fixedSa.dynamic = true;
                fixedSa.renderers = rends.ToArray();
                fixedSa.updaterIndex = 0;

                PrefabsHelper._aquariumSkyApplierFixed = true;
            }
        }

        // Gets called when adding or removing a new fish into our small aquarium (or when adding a new fish into the regular aquarium if "FixAquariumLighting" is enabled)
        public static void FixAquariumFishesSkyApplier(GameObject aquariumAnimRoot, bool remove = false)
        {
            if (aquariumAnimRoot != null)
                foreach (Transform tr in aquariumAnimRoot.transform)
                    foreach (Transform ctr in tr)
                        if (ctr.childCount > 0 && !string.IsNullOrEmpty(ctr.name) && ctr.name.StartsWith("fish_attach", true, CultureInfo.InvariantCulture))
                        {
                            SkyApplier sa = ctr.GetComponent<SkyApplier>();
                            if (ctr.gameObject != null)
                            {
                                if (sa == null)
                                    sa = ctr.gameObject.GetComponent<SkyApplier>();
                                if (sa == null)
                                    sa = ctr.gameObject.AddComponent<SkyApplier>();
                            }
                            if (sa != null)
                            {
                                if (remove)
                                {
                                    sa.anchorSky = Skies.Auto;
                                    sa.renderers = new Renderer[] { };
                                    sa.dynamic = true;
                                    sa.updaterIndex = 0;
                                    sa.enabled = true;
                                }
                                else
                                {
                                    List<Renderer> rends = new List<Renderer>();
                                    foreach (Transform fish in ctr)
                                    {
                                        if (fish.gameObject != null)
                                        {
                                            Renderer[] tmpRends = fish.GetAllComponentsInChildren<Renderer>();
                                            if (tmpRends != null)
                                                foreach (Renderer tmpRend in tmpRends)
                                                    rends.Add(tmpRend);
                                        }
                                    }
                                    sa.anchorSky = Skies.Auto;
                                    sa.renderers = rends.ToArray();
                                    sa.dynamic = true;
                                    sa.updaterIndex = 0;
                                    sa.enabled = true;
                                    sa.RefreshDirtySky();
                                }
                            }
                        }
        }

        #endregion

        #region Degasi bases

        private static readonly List<KeyValuePair<string, Vector3>> DegasiBaseParts = new List<KeyValuePair<string, Vector3>>(new KeyValuePair<string, Vector3>[25]
        {
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-641.4045f, -505.624f, -939.858f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-641.4045f, -505.624f, -939.858f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-640.7137f, -506.242f, -939.9625f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-640.7137f, -506.242f, -939.9625f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-640.0577f, -505.959f, -947.2491f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-640.0577f, -505.959f, -947.2491f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-658.4609f, -513.24f, -956.6729f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-635.91f, -512.54f, -951.6974f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-647.5078f, -507.28f, -940.6493f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -509.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -509.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-635.91f, -502.04f, -951.6974f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-647.97f, -502.04f, -935.7426f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-633.9626f, -512.54f, -937.6899f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -512.54f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -502.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Placeholder)", new Vector3(-641.4045f, -505.624f, -939.858f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Placeholder)", new Vector3(-640.7137f, -506.242f, -939.9625f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Placeholder)", new Vector3(-640.0577f, -505.959f, -947.2491f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -505.54f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -509.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-642.637f, -506.784f, -940.607f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-642.637f, -506.784f, -940.607f)),
            new KeyValuePair<string, Vector3>("SwimChargeFinsDataBox(Clone)", new Vector3(-643.7737f, -509.9037f, -941.8508f)),
            new KeyValuePair<string, Vector3>("CyclopsShieldModuleDataBox(Clone)", new Vector3(-635.1158f, -502.727f, -951.423f))
        });

        private static void HideSwimChargeFinsDataBox(GameObject obj, Vector3 origPos)
        {
            if (ConfigSwitcher.HideDeepGrandReefDegasiBase)
            {
                if (KnownTech.Contains(TechType.SwimChargeFins))
                    UnityEngine.Object.Destroy(obj);
                else
                    obj.transform.position = new Vector3(-643.7737f, -512.1037f, -941.8508f);
            }
            else
                obj.transform.position = origPos;
        }

        private static void HideCyclopsShieldModuleDataBox(GameObject obj, Vector3 origPos)
        {
            if (ConfigSwitcher.HideDeepGrandReefDegasiBase)
            {
                if (KnownTech.Contains(TechType.CyclopsShieldModule))
                    UnityEngine.Object.Destroy(obj);
                else
                    obj.transform.position = new Vector3(-644.7737f, -512.2037f, -941.8508f);
            }
            else
                obj.transform.position = origPos;
        }

        private static void ApplyDegasiVisibility(GameObject obj)
        {
            Vector3 pos;
            string name;
            try { pos = obj.transform.position; name = obj.name; }
            catch { pos = Vector3.zero; name = null; }
            if (name != null && pos != Vector3.zero)
            {
                foreach (KeyValuePair<string, Vector3> part in DegasiBaseParts)
                    if (part.Key == name && IsNear(pos, part.Value))
                    {
                        if (part.Key == "SwimChargeFinsDataBox(Clone)")
                            HideSwimChargeFinsDataBox(obj, part.Value);
                        else if (part.Key == "CyclopsShieldModuleDataBox(Clone)")
                            HideCyclopsShieldModuleDataBox(obj, part.Value);
                        else if ((ConfigSwitcher.HideDeepGrandReefDegasiBase && obj.activeSelf) ||
                            (!ConfigSwitcher.HideDeepGrandReefDegasiBase && !obj.activeSelf))
                            obj.SetActive(!ConfigSwitcher.HideDeepGrandReefDegasiBase);
                    }
            }
        }

        public static void HideDegasiBase()
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
            if (objs != null)
                foreach (GameObject obj in objs)
                    ApplyDegasiVisibility(obj);
        }

        #endregion
    }
}
