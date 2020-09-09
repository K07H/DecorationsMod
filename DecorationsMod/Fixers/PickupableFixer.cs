using System.Reflection;

namespace DecorationsMod.Fixers
{
    public class PickupableFixer
    {
        private static readonly MethodInfo allowedToPickUpMethod = typeof(Pickupable).GetMethod("AllowedToPickUp", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool OnHandClick_Prefix(Pickupable __instance, GUIHand hand)
        {
            if (__instance.gameObject != null)
            {
                // Get current item PID
                var pid = __instance.gameObject.GetComponent<PrefabIdentifier>();

                // If current item is one of our new flora
                if (pid != null && !string.IsNullOrEmpty(pid.ClassId) && CustomFlora.AllPlants.Contains(pid.ClassId))
                {
                    // If hand is free and item allowed to pickup
                    if (hand.IsFreeToInteract() && ((bool)allowedToPickUpMethod.Invoke(__instance, null) == true))
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
                    PrefabsHelper.HidePlantAndShowSeed(__instance.gameObject.transform, pid.ClassId); // Hide plant, show seed and enable pickupable
            }
        }
    }
}