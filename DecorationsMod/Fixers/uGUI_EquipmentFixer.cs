using System.Reflection;

namespace DecorationsMod.Fixers
{
    public class uGUI_EquipmentFixer
    {
        private static readonly FieldInfo equipmentField = typeof(uGUI_Equipment).GetField("equipment", BindingFlags.NonPublic | BindingFlags.Instance);
        
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
            if (ModdedBatteriesFixer.BatteriesTechTypes().Contains(techType))
            {
                if (Equipment.GetSlotType(slotB) == EquipmentType.BatteryCharger)
                {
                    Equipment equipmentValue = equipmentField.GetValue(__instance) as Equipment;
                    InventoryItem itemInSlot = equipmentValue.GetItemInSlot(slotB);
                    if (itemInSlot == null)
                    {
#if DEBUG_PLACE_TOOL
                        Logger.Log("DEBUG: CanSwitchOrSwap returns SWITCH battery for " + techType.AsString(false));
#endif
                        __result = ItemAction.Switch;
                        return false;
                    }
                    if (Inventory.CanSwap(draggedItem, itemInSlot))
                    {
#if DEBUG_PLACE_TOOL
                        Logger.Log("DEBUG: CanSwitchOrSwap returns SWAP battery for " + techType.AsString(false));
#endif
                        __result = ItemAction.Swap;
                        return false;
                    }
#if DEBUG_PLACE_TOOL
                    Logger.Log("DEBUG: CanSwitchOrSwap returns NONE battery for " + techType.AsString(false));
#endif
                    __result = ItemAction.None;
                    return false;
                }
            }
            else if (ModdedBatteriesFixer.PowercellsTechTypes().Contains(techType))
            {
                if (Equipment.GetSlotType(slotB) == EquipmentType.PowerCellCharger)
                {
                    Equipment equipmentValue = equipmentField.GetValue(__instance) as Equipment;
                    InventoryItem itemInSlot = equipmentValue.GetItemInSlot(slotB);
                    if (itemInSlot == null)
                    {
#if DEBUG_PLACE_TOOL
                        Logger.Log("DEBUG: CanSwitchOrSwap returns SWITCH powercell for " + techType.AsString(false));
#endif
                        __result = ItemAction.Switch;
                        return false;
                    }
                    if (Inventory.CanSwap(draggedItem, itemInSlot))
                    {
#if DEBUG_PLACE_TOOL
                        Logger.Log("DEBUG: CanSwitchOrSwap returns SWAP powercell for " + techType.AsString(false));
#endif
                        __result = ItemAction.Swap;
                        return false;
                    }
#if DEBUG_PLACE_TOOL
                    Logger.Log("DEBUG: CanSwitchOrSwap returns NONE powercell for " + techType.AsString(false));
#endif
                    __result = ItemAction.None;
                    return false;
                }
            }
#if DEBUG_PLACE_TOOL
            Logger.Log("DEBUG: CanSwitchOrSwap returns NONE for " + techType.AsString(false) + " and origin function will get called.");
#endif
            __result = ItemAction.None;
            return true;
        }
    }
}
