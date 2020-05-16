using DecorationsMod.Controllers;
using System.Collections.Generic;
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

        public static bool SetDefaultLargeWorldEntity(GameObject gameObj, LargeWorldEntity.CellLevel cellLevel = LargeWorldEntity.CellLevel.Medium, int updaterIndex = 0)
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

        private static bool _aquariumSkyApplierFixed = false; // This to ensure we fix sky appliers only once.
        public static void FixAquariumSkyApplier()
        {
            if (!PrefabsHelper._aquariumSkyApplierFixed)
            {
                var aquariumPrefab = Resources.Load<GameObject>("Submarine/Build/Aquarium"); 
                var sas = aquariumPrefab.GetComponents<SkyApplier>();

                List<Renderer> rendsA = new List<Renderer>();
                List<Renderer> rendsB = new List<Renderer>();
                int i = 0;
                if (sas != null)
                    foreach (SkyApplier sa in sas)
                    {
                        if (sa.renderers != null)
                        {
                            foreach (Renderer rend in sa.renderers)
                                if (!string.IsNullOrEmpty(rend.name) && rend.name.CompareTo("Aquarium") != 0)
                                {
                                    if (ConfigSwitcher.GlowingAquariumGlass && rend.name.CompareTo("Aquarium_glass") == 0 && rend.materials != null)
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
                                    if (i == 0)
                                        rendsA.Add(rend);
                                    else
                                        rendsB.Add(rend);
                                }
                        }
                        i++;
                    }
                sas[0].renderers = rendsA.ToArray();
                sas[1].renderers = rendsB.ToArray();

                Renderer[] tmpRends = aquariumPrefab.GetAllComponentsInChildren<Renderer>();
                List<Renderer> rends = new List<Renderer>();
                if (tmpRends != null)
                    foreach (Renderer tmpRend in tmpRends)
                        if (!string.IsNullOrEmpty(tmpRend.name) && tmpRend.name.CompareTo("Aquarium") == 0)
                            rends.Add(tmpRend);

                var fixedSa = aquariumPrefab.AddComponent<SkyApplier>();
                fixedSa.anchorSky = Skies.Auto;
                fixedSa.dynamic = true;
                fixedSa.renderers = rends.ToArray();
                fixedSa.updaterIndex = 0;

                PrefabsHelper._aquariumSkyApplierFixed = true;
            }
        }

        // Gets called when adding a new fish into our small aquarium (or when adding a new fish into the regular aquarium if "FixAquariumLighting" is enabled)
        public static void FixAquariumFishesSkyApplier(GameObject aquariumAnimRoot)
        {
            if (aquariumAnimRoot != null)
                foreach (Transform tr in aquariumAnimRoot.transform)
                    foreach (Transform ctr in tr)
                        if (ctr.childCount > 0 && !string.IsNullOrEmpty(ctr.name) && ctr.name.StartsWith("fish_attach"))
                            foreach (Transform fish in ctr)
                                if (fish.gameObject != null)
                                {
                                    SkyApplier sa = fish.GetComponent<SkyApplier>();
                                    if (sa == null)
                                        sa = fish.gameObject.AddComponent<SkyApplier>();
                                    sa.anchorSky = Skies.Auto;
                                    sa.renderers = fish.GetAllComponentsInChildren<Renderer>();
                                    sa.dynamic = true;
                                    sa.updaterIndex = 0;
                                    sa.enabled = true;
                                }
        }
    }
}
