using DecorationsMod.Controllers;

namespace DecorationsMod.Fixers
{
    public class GrownPlantFixer
    {
        public static void OnHandHover_Postfix(GrownPlant __instance, GUIHand hand)
        {
#if DEBUG_GROWNPLANT_FIXER
            Logger.Log("DEBUG: TEST A");
#endif
            // If current plant is valid
            if (__instance.seed != null && __instance.seed.pickupable != null && __instance.seed.currentPlanter != null)
            {
                // Get prefab ID
                PrefabIdentifier prefabID = __instance.seed.gameObject.GetComponent<PrefabIdentifier>();
                // If current plant is one of our custom plants
                if (prefabID != null && CustomFlora.AllPlants.Contains(prefabID.ClassId))
                {
                    LiveMixin liveMixin = null;
                    bool componentEnabled = false;
                    float progress = -1.0f;
                    PlantGenericController controllerA = __instance.gameObject.GetComponent<PlantGenericController>();
                    if (controllerA == null)
                    {
                        controllerA = __instance.seed.gameObject.GetComponent<PlantGenericController>();
                        if (controllerA != null)
                        {
#if DEBUG_GROWNPLANT_FIXER
                            Logger.Log("DEBUG: CONTROLLER IN SEED A");
#endif
                            liveMixin = __instance.seed.gameObject.GetComponent<LiveMixin>();
                        }
                    }
                    else
                    {
#if DEBUG_GROWNPLANT_FIXER
                        Logger.Log("DEBUG: CONTROLLER IN A");
#endif
                        liveMixin = __instance.gameObject.GetComponent<LiveMixin>();
                    }
                    PlantMonoTransformController controllerB = __instance.gameObject.GetComponent<PlantMonoTransformController>();
                    if (controllerB == null)
                    {
                        controllerB = __instance.seed.gameObject.GetComponent<PlantMonoTransformController>();
                        if (controllerB != null)
                        {
#if DEBUG_GROWNPLANT_FIXER
                            Logger.Log("DEBUG: CONTROLLER IN SEED B");
#endif
                            liveMixin = __instance.seed.gameObject.GetComponent<LiveMixin>();
                        }
                    }
                    else
                    {
#if DEBUG_GROWNPLANT_FIXER
                        Logger.Log("DEBUG: CONTROLLER IN B");
#endif
                        liveMixin = __instance.gameObject.GetComponent<LiveMixin>();
                    }
                    LandTree1Controller controllerC = __instance.gameObject.GetComponent<LandTree1Controller>();
                    if (controllerC == null)
                    {
                        controllerC = __instance.seed.gameObject.GetComponent<LandTree1Controller>();
                        if (controllerC != null)
                        {
#if DEBUG_GROWNPLANT_FIXER
                            Logger.Log("DEBUG: CONTROLLER IN SEED C");
#endif
                            liveMixin = __instance.seed.gameObject.GetComponent<LiveMixin>();
                        }
                    }
                    else
                    {
#if DEBUG_GROWNPLANT_FIXER
                        Logger.Log("DEBUG: CONTROLLER IN C");
#endif
                        liveMixin = __instance.gameObject.GetComponent<LiveMixin>();
                    }

                    if (controllerA != null)
                    {
#if DEBUG_GROWNPLANT_FIXER
                        Logger.Log("DEBUG: TEST A1 enabled=[" + controllerA.enabled + "] progress=[" + controllerA._progress + "] passedProgress=[" + controllerA._passedProgress + "]");
#endif
                        progress = controllerA._progress;
                        componentEnabled = controllerA.enabled;
                    }
                    else if (controllerB != null)
                    {
#if DEBUG_GROWNPLANT_FIXER
                        Logger.Log("DEBUG: TEST A2 enabled=[" + controllerB.enabled + "] progress=[" + controllerB._progress + "] passedProgress=[" + controllerB._passedProgress + "]");
#endif
                        progress = controllerB._progress;
                        componentEnabled = controllerB.enabled;
                    }
                    else if (controllerC != null)
                    {
#if DEBUG_GROWNPLANT_FIXER
                        Logger.Log("DEBUG: TEST A3 enabled=[" + controllerC.enabled + "] progress=[" + controllerC._progress + "] passedProgress=[" + controllerC._passedProgress + "]");
#endif
                        progress = controllerC._progress;
                        componentEnabled = controllerC.enabled;
                    }
#if DEBUG_GROWNPLANT_FIXER
                    else
                        Logger.Log("DEBUG: TEST A4");
#endif

                    if (componentEnabled && progress >= 0.0f && progress < 1.0f)
                    {
#if DEBUG_GROWNPLANT_FIXER
                        Logger.Log("DEBUG: TEST A5 progress=[" + progress + "]");
#endif
                        // Display growing tooltip
                        HandReticle.main.SetIcon(HandReticle.IconType.Progress, 1f);
                        HandReticle.main.SetProgress(progress);
                        // Fix knifeable
                        if (liveMixin != null && liveMixin.data != null && liveMixin.knifeable)
                            liveMixin.data.knifeable = false;
                    }
                }
#if DEBUG_GROWNPLANT_FIXER
                else
                    Logger.Log("DEBUG: TEST B4");
            }
            else
                Logger.Log("DEBUG: TEST C");
#else
            }
#endif
        }
    }
}
