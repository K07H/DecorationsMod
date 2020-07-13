using DecorationsMod.Controllers;
using Exploder;
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
        private static readonly List<string> pickedUpMelons = new List<string>();

        public static void OnHandClick_Postfix(GrownPlant __instance, GUIHand hand)
        {
            if (__instance.seed != null && __instance.seed.gameObject != null)
            {
                PrefabIdentifier prefabID = __instance.seed.gameObject.GetComponent<PrefabIdentifier>();
                if (prefabID == null)
                    return;
                if (prefabID.ClassId == "MarbleMelonTiny")
                {
                    PlantGenericController controllerA = __instance.gameObject.GetComponent<PlantGenericController>();
                    if (controllerA == null)
                        controllerA = __instance.seed.gameObject.GetComponent<PlantGenericController>();
                    if (controllerA != null)
                    {
                        if (controllerA._progress >= 1.0f)
                        {
                            if (__instance.seed.currentPlanter != null && Inventory.Get().HasRoomFor(1, 1))
                            {
                                __instance.seed.currentPlanter.RemoveItem(__instance.seed);

                                Vector3 position = __instance.seed.pickupable.gameObject.transform.position;
                                TechType techType = __instance.seed.pickupable.GetTechType();

                                __instance.seed.pickupable.GetTechName();
                                Pickupable result = __instance.seed.pickupable.Initialize();

                                ProfilingUtils.BeginSample("Trigger Goal");
                                GoalManager.main.OnCustomGoalEvent("Pickup_" + techType.AsString(false));
                                ProfilingUtils.EndSample(null);
                                __instance.seed.pickupable.PlayPickupSound();
                                __instance.seed.pickupable = result;

                                GameObject.Destroy(result.gameObject);
                                GameObject fruit = CraftData.GetPrefabForTechType(CrafterLogicFixer.MarbleMelonTinyFruit, false);
                                result = fruit.GetComponent<Pickupable>();
                                Eatable eat = fruit.GetComponent<Eatable>();
                                if (ConfigSwitcher.config_MarbleMelonTiny.Eatable)
                                {
#if SUBNAUTICA
                                    eat.stomachVolume = 10.0f;
                                    eat.allowOverfill = false;
#endif
                                    eat.decomposes = ConfigSwitcher.config_MarbleMelonTiny.Decomposes;
                                    eat.kDecayRate = ConfigSwitcher.config_MarbleMelonTiny.KDecayRate;
                                    eat.despawns = ConfigSwitcher.config_MarbleMelonTiny.Despawns;
                                    eat.despawnDelay = ConfigSwitcher.config_MarbleMelonTiny.DespawnDelay;
                                    // If it's the first time we pick this melon
                                    if (!GrownPlantFixer.pickedUpMelons.Contains(prefabID.Id))
                                    {
                                        GrownPlantFixer.pickedUpMelons.Add(prefabID.Id);
                                        // Reset food/water to initial values
                                        eat.foodValue = ConfigSwitcher.config_MarbleMelonTiny.FoodValue;
                                        eat.waterValue = ConfigSwitcher.config_MarbleMelonTiny.WaterValue;
                                        eat.timeDecayStart = DayNightCycle.main.timePassedAsFloat;
                                    }
                                }
                                PlantGenericController pgc = fruit.GetComponent<PlantGenericController>();
                                pgc.IsMarbleMelonTinyFruit = true;

                                InventoryItem item = new InventoryItem(result);
                                if (!((IItemsContainer)Inventory.Get().equipment).AddItem(item))
                                    Inventory.Get().container.UnsafeAdd(item);

                                UniqueIdentifier component = result.GetComponent<UniqueIdentifier>();
                                if (component != null)
                                    NotificationManager.main.Add(NotificationManager.Group.Inventory, component.Id, 4f);
		                        KnownTech.Analyze(CrafterLogicFixer.MarbleMelonTinyFruit, true);
		                        if (global::Utils.GetSubRoot() != null)
                                    __instance.seed.pickupable.destroyOnDeath = false;
			                    uGUI_IconNotifier.main.Play(CrafterLogicFixer.MarbleMelonTinyFruit, uGUI_IconNotifier.AnimationType.From, null);
		                        SkyEnvironmentChanged.Send(__instance.seed.pickupable.gameObject, Player.main.GetSkyEnvironment());

                                hand.player.PlayGrab();
                            }
                        }
                    }
                }
                else if (prefabID.ClassId == "MarbleMelonTinyFruit")
                {
                    if (__instance.seed.pickupable != null && Inventory.Get().HasRoomFor(__instance.seed.pickupable) && __instance.seed.currentPlanter != null)
                    {
                        __instance.seed.currentPlanter.RemoveItem(__instance.seed);
                        Inventory.Get().Pickup(__instance.seed.pickupable, false);
                        hand.player.PlayGrab();
                    }
                }
            }
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
                        else if (progress >= 1.0f && prefabID.ClassId == "MarbleMelonTiny")
                            showMelonTooltip = true;
                    }
                }
                else if (prefabID.ClassId == "MarbleMelonTinyFruit")
                    showMelonTooltip = true;
                if (showMelonTooltip)
                {
                    HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
                    if (!Player.main.HasInventoryRoom(1, 1))
                        HandReticle.main.SetInteractText(LanguageHelper.GetFriendlyWord("PickupMarbleMelonTinyFruit"), "InventoryFull", false, true, HandReticle.Hand.None);
                    else
                        HandReticle.main.SetInteractText(LanguageHelper.GetFriendlyWord("PickupMarbleMelonTinyFruit"), string.Empty, false, false, HandReticle.Hand.None);
                }
            }
        }
    }
}
