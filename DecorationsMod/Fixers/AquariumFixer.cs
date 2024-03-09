using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class AquariumFixer
    {
        private const string AquariumClassId = "6d71afaa-09b6-44d3-ba2d-66644ffe6a99";

        public static void RefreshAquariumSkyAppliers(Aquarium aquarium, bool remove = false)
        {
            var pid = aquarium?.gameObject?.GetComponent<PrefabIdentifier>();
#if DEBUG_AQUARIUM
            Logger.Debug("Entering RefreshAquariumSkyAppliers for aquarium ClassID=[" + (!string.IsNullOrEmpty(pid?.ClassId) ? pid.ClassId : "?") + "]");
#endif
            // If current item is our custom aquarium (or if it's the regular aquarium and "FixAquariumLighting" is enabled).
            if (pid != null && !string.IsNullOrEmpty(pid.ClassId) &&
                (pid.ClassId == "AquariumSmall" || (ConfigSwitcher.FixAquariumLighting && pid.ClassId == AquariumClassId)))
            {
                GameObject model = aquarium.gameObject.FindChild("model");
                if (model != null)
                {
                    PrefabsHelper.FixAquariumFishesSkyApplier(model.FindChild("Aquarium_animation").FindChild("root"), remove);
                    PrefabsHelper.FixAquariumFishesSkyApplier(model.FindChild("Aquarium_animation2").FindChild("root"), remove);
                }
            }
        }

        public static void AddItem_Postfix(Aquarium __instance, InventoryItem item)
        {
            RefreshAquariumSkyAppliers(__instance, false);
        }

        public static bool RemoveItem_Prefix(Aquarium __instance, InventoryItem item)
        {
            RefreshAquariumSkyAppliers(__instance, true);
            return true;
        }
    }
}
