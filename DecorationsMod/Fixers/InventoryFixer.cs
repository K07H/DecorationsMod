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

        public static bool CanDropItemHere_Prefix(bool __result, Pickupable item, bool notify = false)
        {
            bool isPlant = false;

#if DEBUG_DROP_ITEM
            Logger.Log("DEBUG: Entering CanDropItemHere_Prefix() for item name=[" + item.gameObject.name + "]");
#endif

            // Check if current item is a plant
            Plantable plant = item.gameObject.GetComponent<Plantable>();
            if (plant != null)
                isPlant = true;
            else
            {
#if DEBUG_DROP_ITEM
                Logger.Log("DEBUG: A) Cannot find plant");
#endif
                GrownPlant grownPlant = item.gameObject.GetComponent<GrownPlant>();
                if (grownPlant != null)
                    isPlant = true;
#if DEBUG_DROP_ITEM
                else
                    Logger.Log("DEBUG: B) Cannot find grownplant");
#endif
            }

            if (isPlant)
            {
                // Check if current plant is one of our custom plants
                PrefabIdentifier id = item.gameObject.GetComponent<PrefabIdentifier>();
                if (id != null)
                {
                    if (!string.IsNullOrEmpty(id.ClassId) && CustomFlora.AllPlants.Contains(id.ClassId))
                    {
                        // Set result value to false (cannot drop item) and return false to prevent original function from being called
                        __result = false;
                        return false;
                    }
#if DEBUG_DROP_ITEM
                    else
                        Logger.Log("DEBUG: E) Cannot find class ID");
                }
                else
                    Logger.Log("DEBUG: D) Cannot find prefab ID");
            }
            else
                Logger.Log("DEBUG: C) Item is not a plant");
#else
                }
            }
#endif

            // Return true to call original function
            return true;
        }
    }
}
