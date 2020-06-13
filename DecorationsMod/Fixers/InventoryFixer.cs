namespace DecorationsMod.Fixers
{
    public class InventoryFixer
    {
        public static bool AddOrSwap_Prefix(ref bool __result, InventoryItem itemA, Equipment equipmentB, string slotB)
        {
            if (itemA == null || !itemA.CanDrag(true) || equipmentB == null)
                return true;
            Pickupable item = itemA.item;
            if (item == null)
                return true;
            TechType techType = item.GetTechType();
            if (techType == TechType.Battery || techType == TechType.PrecursorIonBattery ||
                techType == TechType.PowerCell || techType == TechType.PrecursorIonPowerCell)
            {
                IItemsContainer container = itemA.container;
                if (container == null)
                    return true;
                Equipment equipment = container as Equipment;
                bool flag = equipment != null;
                string empty = string.Empty;
                if (flag && !equipment.GetItemSlot(item, ref empty))
                    return true;
#if BELOWZERO
                EquipmentType equipmentType = TechData.GetEquipmentType(techType);
#else
                EquipmentType equipmentType = CraftData.GetEquipmentType(techType);
#endif
                if (string.IsNullOrEmpty(slotB))
                    equipmentB.GetCompatibleSlot(equipmentType, out slotB);
                if (string.IsNullOrEmpty(slotB))
                    return true;
                if (container == equipmentB && empty == slotB)
                    return true;
                EquipmentType slotBType = Equipment.GetSlotType(slotB);
                if (slotBType != EquipmentType.BatteryCharger && slotBType != EquipmentType.PowerCellCharger)
                    return true;
                else // Else, we're trying to plug a battery or powercell to its charger
                {
                    InventoryItem inventoryItem = equipmentB.RemoveItem(slotB, false, true);
                    if (inventoryItem == null)
                    {
                        if (equipmentB.AddItem(slotB, itemA, false))
                        {
                            __result = true;
                            return false;
                        }
                    }
                    else if (equipmentB.AddItem(slotB, itemA, false))
                    {
                        if ((flag && equipment.AddItem(empty, inventoryItem, false)) || (!flag && container.AddItem(inventoryItem)))
                        {
                            __result = true;
                            return false;
                        }
                        if (flag)
                            equipment.AddItem(empty, itemA, true);
                        else
                            container.AddItem(itemA);
                        equipmentB.AddItem(slotB, inventoryItem, true);
                    }
                    else
                        equipmentB.AddItem(slotB, inventoryItem, true);
                    __result = false;
                    return false;
                }
            }
            return true;
        }
    }
}
