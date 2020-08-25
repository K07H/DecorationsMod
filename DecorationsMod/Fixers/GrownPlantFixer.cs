using DecorationsMod.Controllers;
using SMLHelper.V2.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UWE;

namespace DecorationsMod.Fixers
{
    public class GrownPlantFixer
    {
        public static bool OnHandClick_Prefix(GrownPlant __instance, GUIHand hand)
        {
            if (__instance.seed != null && __instance.seed.gameObject != null)
            {
                PrefabIdentifier prefabID = __instance.seed.gameObject.GetComponent<PrefabIdentifier>();
                if (prefabID == null)
                    return true;
                if (prefabID.ClassId == "MarbleMelonTiny")
                {
                    PlantGenericController controllerA = __instance.gameObject.GetComponent<PlantGenericController>();
                    if (controllerA == null)
                        controllerA = __instance.seed.gameObject.GetComponent<PlantGenericController>();
                    if (controllerA != null && controllerA._progress >= 1.0f)
                    {
                        if (__instance.seed.currentPlanter != null && Inventory.Get().HasRoomFor(1, 1))
                        {
                            __instance.seed.currentPlanter.RemoveItem(__instance.seed);
                            if (global::Utils.GetSubRoot() != null)
                                __instance.seed.pickupable.destroyOnDeath = false;
                            SkyEnvironmentChanged.Send(__instance.seed.pickupable.gameObject, Player.main.GetSkyEnvironment());
                            UnityEngine.Object.Destroy(__instance.seed.pickupable.gameObject);
                            CraftData.AddToInventorySync(CrafterLogicFixer.MarbleMelonTinyFruit, 1, false, false);
                            hand.player.PlayGrab();
                        }
                    }
                    return false;
                }
                else if (prefabID.ClassId == "MarbleMelonTinyFruit")
                {
                    if (__instance.seed.currentPlanter != null && __instance.seed.pickupable != null && Inventory.Get().HasRoomFor(__instance.seed.pickupable))
                    {
                        PlantGenericController controllerA = __instance.gameObject.GetComponent<PlantGenericController>();
                        if (controllerA == null)
                            controllerA = __instance.seed.gameObject.GetComponent<PlantGenericController>();
                        if (controllerA != null && controllerA._progress >= 1.0f)
                        {
                            __instance.seed.currentPlanter.RemoveItem(__instance.seed);
                            Inventory.Get().Pickup(__instance.seed.pickupable, false);
                            hand.player.PlayGrab();
                        }
                    }
                    return false;
                }
            }
            return true;
        }

        public static void OnHandHover_Postfix(GrownPlant __instance, GUIHand hand)
        {
            // If current plant is valid
            if (__instance.seed != null && __instance.seed.pickupable != null && __instance.seed.currentPlanter != null)
            {
                bool showMelonTooltip = false;
                // Get prefab ID
                PrefabIdentifier prefabID = __instance.seed.gameObject.GetComponent<PrefabIdentifier>();
                if (prefabID == null)
                    return;

                // If current plant is one of our custom plants
                bool isSmallMelon = prefabID.ClassId == "MarbleMelonTiny" || prefabID.ClassId == "MarbleMelonTinyFruit";
                if (CustomFlora.AllPlants.Contains(prefabID.ClassId))
                {
                    LiveMixin liveMixin = null;
                    bool componentEnabled = false;
                    float progress = -1.0f;
                    PlantGenericController controllerA = __instance.gameObject.GetComponent<PlantGenericController>();
                    if (controllerA == null)
                    {
                        controllerA = __instance.seed.gameObject.GetComponent<PlantGenericController>();
                        if (controllerA != null)
                            liveMixin = __instance.seed.gameObject.GetComponent<LiveMixin>();
                    }
                    else
                        liveMixin = __instance.gameObject.GetComponent<LiveMixin>();
                    if (controllerA == null)
                    {
                        PlantMonoTransformController controllerB = __instance.gameObject.GetComponent<PlantMonoTransformController>();
                        if (controllerB == null)
                        {
                            controllerB = __instance.seed.gameObject.GetComponent<PlantMonoTransformController>();
                            if (controllerB != null)
                                liveMixin = __instance.seed.gameObject.GetComponent<LiveMixin>();
                        }
                        else
                            liveMixin = __instance.gameObject.GetComponent<LiveMixin>();
                        if (controllerB == null)
                        {
                            LandTree1Controller controllerC = __instance.gameObject.GetComponent<LandTree1Controller>();
                            if (controllerC == null)
                            {
                                controllerC = __instance.seed.gameObject.GetComponent<LandTree1Controller>();
                                if (controllerC != null)
                                    liveMixin = __instance.seed.gameObject.GetComponent<LiveMixin>();
                            }
                            else
                                liveMixin = __instance.gameObject.GetComponent<LiveMixin>();
                            if (controllerC != null)
                            {
                                progress = controllerC._progress;
                                componentEnabled = controllerC.enabled;
                            }
                        }
                        else
                        {
                            progress = controllerB._progress;
                            componentEnabled = controllerB.enabled;
                        }
                    }
                    else
                    {
                        progress = controllerA._progress;
                        componentEnabled = controllerA.enabled;
                    }

                    if (componentEnabled)
                    {
                        if (progress >= 0.0f && progress < 1.0f)
                        {
                            // Display growing tooltip
                            HandReticle.main.SetIcon(HandReticle.IconType.Progress, 1f);
                            HandReticle.main.SetProgress(progress);
                            // Fix knifeable
                            if (liveMixin != null && liveMixin.data != null && liveMixin.knifeable)
                                liveMixin.data.knifeable = false;
                        }
                        else if (isSmallMelon && progress >= 1.0f)
                            showMelonTooltip = true;
                    }
                    else if (isSmallMelon && progress >= 1.0f)
                        showMelonTooltip = true;
                }
                else if (isSmallMelon)
                    showMelonTooltip = true;
                if (showMelonTooltip)
                {
                    HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
                    if (!Player.main.HasInventoryRoom(1, 1))
                    {
#if BELOWZERO
                        HandReticle.main.SetText(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("PickupMarbleMelonTinyFruit"), false, GameInput.Button.None);
                        HandReticle.main.SetText(HandReticle.TextType.HandSubscript, "InventoryFull", true, GameInput.Button.None);
#else
                        HandReticle.main.SetInteractText(LanguageHelper.GetFriendlyWord("PickupMarbleMelonTinyFruit"), "InventoryFull", false, true, HandReticle.Hand.None);
#endif
                    }
                    else
                    {
#if BELOWZERO
                        HandReticle.main.SetText(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord("PickupMarbleMelonTinyFruit"), false, GameInput.Button.None);
#else
                        HandReticle.main.SetInteractText(LanguageHelper.GetFriendlyWord("PickupMarbleMelonTinyFruit"), string.Empty, false, false, HandReticle.Hand.None);
#endif
                    }
                }
            }
        }
    }
}
