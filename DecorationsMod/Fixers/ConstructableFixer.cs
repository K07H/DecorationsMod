using System.Globalization;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class ConstructableFixer
    {
        public static bool CanDeconstruct_Prefix(Constructable __instance, ref bool __result, out string reason)
        {
            string techTypeStr = __instance.techType.AsString();
            if (techTypeStr.StartsWith("DecorativeLocker", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("DecorativeLockerClosed", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("DecorativeLockerDoor", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("CargoBox01_damaged", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("CargoBox01a", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("CargoBox01b", true, CultureInfo.InvariantCulture))
            {
                foreach (Transform tr in __instance.gameObject.transform)
                {
                    if (tr.name.StartsWith("Locker(Clone)", true, CultureInfo.InvariantCulture))
                    {
                        StorageContainer sc = tr.GetComponent<StorageContainer>();
                        if (sc != null && sc.container != null && sc.container.count > 0)
                        {
                            if (Language.main != null)
                            {
                                string notEmptyError = Language.main.Get("DeconstructNonEmptyStorageContainerError");
                                reason = (!string.IsNullOrEmpty(notEmptyError) ? notEmptyError : "Not empty!");
                            }
                            else
                                reason = "Not empty!";
                            __result = false;
                            return false;
                        }
                    }
                }
            }
            reason = null;
            return true;
        }
    }
}
