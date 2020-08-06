using DecorationsMod.Controllers;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod
{
    public static class PrefabsHelper
    {
        /*
        SMLHelper.V2.Handlers.ItemActionHandler.RegisterLeftClickAction(this.TechType, delegate (InventoryItem i)
        {
            //Inventory.Get().debugQuickSlots = true;
            Inventory.Get().quickSlots.Unbind(1);
            Inventory.Get().quickSlots.Bind(1, i);
            Inventory.Get().quickSlots.Select(1);
            Inventory.Get().quickSlots.Update();
        }, "Click here");
        */

        public static bool SetDefaultPickupable(GameObject gameObj, bool isPickupable = true, bool destroyOnDeath = false, bool isValidHandTarget = false, bool attached = false,
            TechType overrideTechType = TechType.None, bool overrideTechUsed = false)
        {
            if (gameObj == null)
                return false;
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
            return true;
        }

        public static bool SetDefaultPlaceTool(GameObject gameObj, Collider mainCollider = null, Pickupable pickupable = null,
            bool onCeiling = false, bool onWalls = false, bool onBase = false, bool alignWithSurface = false)
        {
            if (gameObj == null)
                return false;

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

            return true;
        }

        public static bool SetDefaultRigidBody(GameObject gameObj, CollisionDetectionMode collisionDetectionMode = CollisionDetectionMode.Discrete,
            float mass = 1f, float drag = 0f, float angularDrag = 0.05f, bool detectCollisions = true)
        {
            if (gameObj == null)
                return false;
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
            return true;
        }

        public static bool SetDefaultLargeWorldEntity(GameObject gameObj, LargeWorldEntity.CellLevel cellLevel = LargeWorldEntity.CellLevel.Near, int updaterIndex = 0)
        {
            if (gameObj == null)
                return false;
            LargeWorldEntity lwe = gameObj.GetComponent<LargeWorldEntity>();
            if (lwe == null)
                lwe = gameObj.AddComponent<LargeWorldEntity>();
            lwe.cellLevel = cellLevel;
            lwe.updaterIndex = updaterIndex;
            lwe.hideFlags = HideFlags.None;
            lwe.useGUILayout = true;
            lwe.enabled = true;
            return true;
        }

        public static bool SetDefaultSkyApplier(GameObject gameObj, Renderer[] renderers = null, Skies anchorSky = Skies.Auto,
            bool dynamic = false, bool emissiveFromPower = false)
        {
            if (gameObj == null)
                return false;
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
            return true;
        }

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
#if DEBUG_SEEDS
                Logger.Log("DEBUG: Printing transform for classId=[" + (classId ?? "?") + "]");
                PrefabsHelper.PrintTransform(transform);
#endif
            }
        }

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
            { "PrecursorIonBattery", "model" },
        };

        public static void FixPlaceToolSkyAppliers(GameObject go)
        {
            foreach (KeyValuePair<string, string> placeTool in _placeToolSAFix)
                if (go.name.StartsWith(placeTool.Key, false, CultureInfo.InvariantCulture))
                {
                    GameObject model = go.FindChild(placeTool.Value);
                    if (model != null)
                    {
                        SkyApplier[] sas = model.GetComponents<SkyApplier>();
                        if (sas != null && sas.Length == 1)
                        {
                            sas[0].renderers = new Renderer[] { };
                            SkyApplier sa = model.AddComponent<SkyApplier>();
                            if (sa != null)
                            {
                                sa.anchorSky = Skies.Auto;
                                sa.renderers = model.GetAllComponentsInChildren<MeshRenderer>();
                                sa.dynamic = true;
                                sa.updaterIndex = 0;
                                sa.enabled = true;
                                sa.RefreshDirtySky();
                            }
                        }
                    }
                }
        }

        public static void PrintTransform(Transform tr, string indent = "\t")
        {
            if (tr != null)
            {
                Logger.Log("DEBUG: Transform " + indent + "name=[" + tr.name + "] position=[" + tr.localPosition.x.ToString() + ";" + tr.localPosition.y.ToString() + ";" + tr.localPosition.z.ToString() + "] scale=[" + tr.localScale.x.ToString() + ";" + tr.localScale.y.ToString() + ";" + tr.localScale.z.ToString() + "]");
                foreach (Component c in tr.GetComponents<Component>())
                    if (c.GetType() != typeof(Transform))
                        Logger.Log("DEBUG: Transform " + indent + " => component type=[" + c.GetType().ToString() + "] name=[" + c.name + "]");
                string newIndent = indent + "\t";
                foreach (Transform child in tr)
                    PrintTransform(child, newIndent);
            }
        }
    }
}
