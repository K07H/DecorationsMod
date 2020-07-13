using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class AquariumFixer
    {
        public static void AddItem_Postfix(Aquarium __instance, InventoryItem item)
        {
            var pid = __instance?.gameObject?.GetComponent<PrefabIdentifier>();
#if DEBUG_AQUARIUM
            Logger.Log("DEBUG: Entering AddItem_Postfix for aquarium ClassID=[" + (!string.IsNullOrEmpty(pid?.ClassId) ? pid.ClassId : "?") + "]");
#endif
            // If current item is our custom aquarium (or if it's the regular aquarium and "FixAquariumLighting" is enabled).
            if (pid != null && !string.IsNullOrEmpty(pid.ClassId) &&
                (pid.ClassId == "AquariumSmall" || (ConfigSwitcher.FixAquariumLighting && pid.ClassId == "6d71afaa-09b6-44d3-ba2d-66644ffe6a99")))
            {
                GameObject model = __instance.gameObject.FindChild("model");
                if (model != null)
                {
                    PrefabsHelper.FixAquariumFishesSkyApplier(model.FindChild("Aquarium_animation").FindChild("root"));
                    PrefabsHelper.FixAquariumFishesSkyApplier(model.FindChild("Aquarium_animation2").FindChild("root"));
                }
            }
        }
    }
}
