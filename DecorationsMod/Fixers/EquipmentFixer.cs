namespace DecorationsMod.Fixers
{
    public class EquipmentFixer
    {
        public static bool AllowedToAdd_Prefix(Equipment __instance, ref bool __result, string slot, Pickupable pickupable, bool verbose)
        {
            TechType objTechType = pickupable.GetTechType();
            EquipmentType slotType = Equipment.GetSlotType(slot);
            if (slotType == EquipmentType.BatteryCharger && ModdedBatteriesFixer.BatteriesTechTypes().Contains(objTechType))
            {
#if SUBNAUTICA
                EquipmentType eType = CraftData.GetEquipmentType(objTechType);
#else
                EquipmentType eType = TechData.GetEquipmentType(objTechType);
#endif
                if (eType == EquipmentType.Hand || eType == EquipmentType.BatteryCharger)
                {
#if DEBUG_PLACE_TOOL
                    Logger.Log("DEBUG: AllowedToAdd battery charger for " + objTechType.AsString(false));
#endif
                    bool result = ((IItemsContainer)__instance).AllowedToAdd(pickupable, verbose);
                    __result = result;
                    return false;
                }
            }
            else if (slotType == EquipmentType.PowerCellCharger && ModdedBatteriesFixer.PowercellsTechTypes().Contains(objTechType))
            {
#if SUBNAUTICA
                EquipmentType eType = CraftData.GetEquipmentType(objTechType);
#else
                EquipmentType eType = TechData.GetEquipmentType(objTechType);
#endif
                if (eType == EquipmentType.Hand || eType == EquipmentType.PowerCellCharger)
                {
#if DEBUG_PLACE_TOOL
                    Logger.Log("DEBUG: AllowedToAdd powercell charger for " + objTechType.AsString(false));
#endif
                    __result = ((IItemsContainer)__instance).AllowedToAdd(pickupable, verbose);
                    return false;
                }
            }
            return true;
        }
    }
}
