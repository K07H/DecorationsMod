using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DecorationsMod.Fixers
{
    public class uGUI_EquipmentFixer
    {
        private static FieldInfo equipmentField = typeof(uGUI_Equipment).GetField("equipment", BindingFlags.NonPublic | BindingFlags.Instance);
        
        public static bool CanSwitchOrSwap_Prefix(uGUI_Equipment __instance, ref ItemAction __result, string slotB)
        {
            if (!ItemDragManager.isDragging)
                return true;
            InventoryItem draggedItem = ItemDragManager.draggedItem;
            if (draggedItem == null)
                return true;
            Pickupable item = draggedItem.item;
            if (item == null)
                return true;
            TechType techType = item.GetTechType();
            if (techType == TechType.Battery || techType == TechType.PrecursorIonBattery)
            {
                if (Equipment.GetSlotType(slotB) == EquipmentType.BatteryCharger)
                {
                    Equipment equipmentValue = equipmentField.GetValue(__instance) as Equipment;
                    InventoryItem itemInSlot = equipmentValue.GetItemInSlot(slotB);
                    if (itemInSlot == null)
                    {
                        __result = ItemAction.Switch;
                        return false;
                    }
                    if (Inventory.CanSwap(draggedItem, itemInSlot))
                    {
                        __result = ItemAction.Swap;
                        return false;
                    }
                    __result = ItemAction.None;
                    return false;
                }
            }
            else if (techType == TechType.PowerCell || techType == TechType.PrecursorIonPowerCell)
            {
                if (Equipment.GetSlotType(slotB) == EquipmentType.PowerCellCharger)
                {
                    Equipment equipmentValue = equipmentField.GetValue(__instance) as Equipment;
                    InventoryItem itemInSlot = equipmentValue.GetItemInSlot(slotB);
                    if (itemInSlot == null)
                    {
                        __result = ItemAction.Switch;
                        return false;
                    }
                    if (Inventory.CanSwap(draggedItem, itemInSlot))
                    {
                        __result = ItemAction.Swap;
                        return false;
                    }
                    __result = ItemAction.None;
                    return false;
                }
            }
            return true;
        }
    }
}
