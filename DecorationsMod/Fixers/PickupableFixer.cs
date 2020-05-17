/*using Harmony;

namespace DecorationsMod.Fixers
{
    
    [HarmonyPatch(typeof(Pickupable))]
    [HarmonyPatch("OnHandClick")]
    public class PickupableFixer
    {
        //public void OnHandClick(GUIHand hand)
        public static void OnHandClick_Postfix(Pickupable __instance, GUIHand hand)
        {
            var pid = __instance?.gameObject?.GetComponent<PrefabIdentifier>();

            // If current item is our Doomsday device
            if (pid != null && !string.IsNullOrEmpty(pid.ClassId) && pid.ClassId == "AlienArtefact6")
            {
                var pta = __instance.gameObject.GetComponent<PlayerTriggerAnimation>();
                if (pta != null)
                    pta.enabled = false;
            }
        }
    }
}
*/