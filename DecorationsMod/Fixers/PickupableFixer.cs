using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class PickupableFixer
    {
        public static MethodInfo allowedToPickUpMethod = typeof(Pickupable).GetMethod("AllowedToPickUp", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool OnHandClick_Prefix(Pickupable __instance, GUIHand hand)
        {
            if (__instance.gameObject != null)
            {
                // Get current item PID
                var pid = __instance.gameObject.GetComponent<PrefabIdentifier>();

                // If current item is one of our new flora
                if (pid != null && !string.IsNullOrEmpty(pid.ClassId) && CustomFlora.AllPlants.Contains(pid.ClassId) && allowedToPickUpMethod != null)
                {
                    // If hand is free and item allowed to pickup
                    if (hand.IsFreeToInteract() && ((bool)allowedToPickUpMethod.Invoke(__instance, new object[] { }) == true))
                    {
                        // Try pickup flora
                        if (Inventory.Get().Pickup(__instance, false))
                        {
                            // Show plant, hide seed and disable pickupable now that plant has been picked up
                            PrefabsHelper.ShowPlantAndHideSeed(__instance.gameObject.transform, pid.ClassId);
                            // Play grab animation
                            Player.main.PlayGrab();
                            // Refresh waterpark status
                            WaterParkItem component = __instance.GetComponent<WaterParkItem>();
                            if (component == null)
                                component = __instance.gameObject.GetComponent<WaterParkItem>();
                            if (component != null)
                                component.SetWaterPark(null);
                        }
                        else
                            ErrorMessage.AddWarning(Language.main.Get("InventoryFull")); 
                    }
                    // Don't call original function if current item is one of our new flora
                    return false;
                }
            }
            // Give back execution to original function
            return true;
        }

        public static void Drop_Postfix(Pickupable __instance)
        {
            if (__instance.gameObject != null)
            {
                // Get current item PID
                var pid = __instance.gameObject.GetComponent<PrefabIdentifier>();

                // If current item is one of our new flora
                if (pid != null && !string.IsNullOrEmpty(pid.ClassId) && CustomFlora.AllPlants.Contains(pid.ClassId))
                {
                    // On drop: hide plant, show seed and enable pickupable
                    PrefabsHelper.HidePlantAndShowSeed(__instance.gameObject.transform, pid.ClassId);
                }
            }
        }
    }
}