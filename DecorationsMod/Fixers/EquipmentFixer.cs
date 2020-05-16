namespace DecorationsMod.Fixers
{
    public class EquipmentFixer
    {
        public static bool AllowedToAdd_Prefix(Equipment __instance, ref bool __result, string slot, Pickupable pickupable, bool verbose)
        {
            TechType objTechType = pickupable.GetTechType();
            EquipmentType slotType = Equipment.GetSlotType(slot);
            if (slotType == EquipmentType.BatteryCharger && (objTechType == TechType.Battery || objTechType == TechType.PrecursorIonBattery))
            {
#if BELOWZERO
                if (TechData.GetEquipmentType(objTechType) == EquipmentType.Hand)
#else
                if (CraftData.GetEquipmentType(objTechType) == EquipmentType.Hand)
#endif
                {
                    bool result = ((IItemsContainer)__instance).AllowedToAdd(pickupable, verbose);
                    __result = result;
                    return false;
                }
            }
            else if (slotType == EquipmentType.PowerCellCharger && (objTechType == TechType.PowerCell || objTechType == TechType.PrecursorIonPowerCell))
            {
#if BELOWZERO
                if (TechData.GetEquipmentType(objTechType) == EquipmentType.Hand)
#else
                if (CraftData.GetEquipmentType(objTechType) == EquipmentType.Hand)
#endif
                {
                    __result = ((IItemsContainer)__instance).AllowedToAdd(pickupable, verbose);
                    return false;
                }
            }
            return true;
        }
    }
}
