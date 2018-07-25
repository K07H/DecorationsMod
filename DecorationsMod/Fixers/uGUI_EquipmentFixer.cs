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

        //public ItemAction CanSwitchOrSwap(string slotB)
        public static bool CanSwitchOrSwap_Prefix(uGUI_Equipment __instance, ref ItemAction __result, string slotB)
        {
            Logger.Log("DEBUG: E1");
            if (!ItemDragManager.isDragging)
                return true;
            Logger.Log("DEBUG: E2");
            InventoryItem draggedItem = ItemDragManager.draggedItem;
            if (draggedItem == null)
                return true;
            Logger.Log("DEBUG: E3");
            Pickupable item = draggedItem.item;
            if (item == null)
                return true;
            Logger.Log("DEBUG: E4");
            TechType techType = item.GetTechType();
            if (techType == TechType.Battery || techType == TechType.PrecursorIonBattery)
            {
                Logger.Log("DEBUG: E5");
                EquipmentType slotBType = Equipment.GetSlotType(slotB);
                if (slotBType == EquipmentType.BatteryCharger)
                {
                    Logger.Log("DEBUG: E6");
                    Equipment equipmentValue = equipmentField.GetValue(__instance) as Equipment;
                    InventoryItem itemInSlot = equipmentValue.GetItemInSlot(slotB);
                    if (itemInSlot == null)
                    {
                        Logger.Log("DEBUG: E7");
                        __result = ItemAction.Switch;
                        return false;
                    }
                    if (Inventory.CanSwap(draggedItem, itemInSlot))
                    {
                        Logger.Log("DEBUG: E8");
                        __result = ItemAction.Swap;
                        return false;
                    }
                    Logger.Log("DEBUG: E9");
                    __result = ItemAction.None;
                    return false;
                }
            }
            else if (techType == TechType.PowerCell || techType == TechType.PrecursorIonPowerCell)
            {
                Logger.Log("DEBUG: E10");
                EquipmentType slotBType = Equipment.GetSlotType(slotB);
                if (slotBType == EquipmentType.PowerCellCharger)
                {
                    Logger.Log("DEBUG: E11");
                    Equipment equipmentValue = equipmentField.GetValue(__instance) as Equipment;
                    InventoryItem itemInSlot = equipmentValue.GetItemInSlot(slotB);
                    if (itemInSlot == null)
                    {
                        Logger.Log("DEBUG: E12");
                        __result = ItemAction.Switch;
                        return false;
                    }
                    if (Inventory.CanSwap(draggedItem, itemInSlot))
                    {
                        Logger.Log("DEBUG: E13");
                        __result = ItemAction.Swap;
                        return false;
                    }
                    Logger.Log("DEBUG: E14");
                    __result = ItemAction.None;
                    return false;
                }
            }
            Logger.Log("DEBUG: E15");
            return true;
        }
    }
}
