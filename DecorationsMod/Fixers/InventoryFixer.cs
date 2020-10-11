using System.Collections.Generic;

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
            bool isBattery = ModdedBatteriesFixer.BatteriesTechTypes().Contains(techType);
            bool isPowercell = ModdedBatteriesFixer.PowercellsTechTypes().Contains(techType);
            if (isBattery || isPowercell)
            {
                IItemsContainer container = itemA.container;
                if (container == null)
                    return true;
                Equipment equipment = container as Equipment;
                bool flag = equipment != null;
                string empty = string.Empty;
                if (flag && !equipment.GetItemSlot(item, ref empty))
                    return true;
                EquipmentType equipmentType = isPowercell ? EquipmentType.PowerCellCharger : EquipmentType.BatteryCharger;
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

        public static bool GetAltUseItemAction_Prefix(Inventory __instance, ref ItemAction __result, InventoryItem item)
        {
            if (item != null)
            {
                IItemsContainer container = item.container;
                IItemsContainer oppositeContainer = __instance.GetOppositeContainer(item);
                bool flag = container != null && container is Equipment;
                bool flag2 = oppositeContainer != null && oppositeContainer is Equipment;
                if (oppositeContainer != null && !flag2 && !flag)
                {
                    if (item.CanDrag(false))
                    {
                        __result = ItemAction.Switch;
#if DEBUG_PLACE_TOOL
                        Logger.Log("DEBUG: AltUse returns SWITCH for " + (item.item != null ? item.item.GetTechType().AsString(false) : "?"));
#endif
                        return false;
                    }
                }
                else if (container == __instance.container && Inventory.CanDropItemHere(item.item, false))
                {
                    __result = ItemAction.Drop;
#if DEBUG_PLACE_TOOL
                    Logger.Log("DEBUG: AltUse returns DROP for " + (item.item != null ? item.item.GetTechType().AsString(false) : "?"));
#endif
                    return false;
                }
                if (GameInput.GetPrimaryDevice() == GameInput.Device.Controller)
                {
                    if (item.item != null && ConfigSwitcher.EnablePlaceItems && Inventory.main.GetCanBindItem(item))
                    {
                        TechType techType = item.item.GetTechType();
                        if (techType == TechType.Bleach ||
                            techType == TechType.Lubricant ||
                            techType == TechType.Polyaniline ||
                            techType == TechType.Benzene ||
                            techType == TechType.HydrochloricAcid ||
                            techType == TechType.HatchingEnzymes ||
                            techType == TechType.Coffee ||
                            techType == TechType.BigFilteredWater ||
                            techType == TechType.DisinfectedWater ||
                            techType == TechType.FilteredWater ||
                            techType == TechType.WiringKit ||
                            techType == TechType.AdvancedWiringKit ||
                            techType == TechType.ComputerChip ||
                            techType == TechType.PrecursorIonCrystal ||
                            techType == TechType.StalkerTooth ||
                            techType == TechType.FirstAidKit ||
                            techType == TechType.Snack1 ||
                            techType == TechType.Snack2 ||
                            techType == TechType.Snack3)
                        {
#if DEBUG_PLACE_TOOL
                            Logger.Log("DEBUG: AltUse returns ASSIGN 1 for " + techType.AsString(false));
#endif
                            __result = ItemAction.Assign;
                            return false;
                        }
                        else if (ConfigSwitcher.EnablePlaceBatteries && ModdedBatteriesFixer.Chargeable(techType))
                        {
#if DEBUG_PLACE_TOOL
                            Logger.Log("DEBUG: AltUse returns ASSIGN 2 for " + techType.AsString(false));
#endif
                            __result = ItemAction.Assign;
                            return false;
                        }
                        else if (ConfigSwitcher.EnablePlaceOtherMaterials)
                            foreach (KeyValuePair<string, TechType> it in PlaceToolItems._materials)
                                if (techType == it.Value)
                                {
#if DEBUG_PLACE_TOOL
                                    Logger.Log("DEBUG: AltUse returns ASSIGN 3 for " + techType.AsString(false));
#endif
                                    __result = ItemAction.Assign;
                                    return false;
                                }
                    }
                }
                else
                {
                    if (item.item != null && ConfigSwitcher.EnablePlaceItems && ConfigSwitcher.EnablePlaceBatteries)
                    {
                        TechType techType = item.item.GetTechType();
                        bool isBattery = ModdedBatteriesFixer.BatteriesTechTypes().Contains(techType);
                        bool isPowercell = ModdedBatteriesFixer.PowercellsTechTypes().Contains(techType);
                        if (isBattery || isPowercell)
                        {
                            if (flag2)
                            {
                                Equipment equipment = oppositeContainer as Equipment;
                                EquipmentType equipmentType = isPowercell ? EquipmentType.PowerCellCharger : EquipmentType.BatteryCharger;
                                if (equipment.GetFreeSlot(equipmentType, out string text))
                                {
#if DEBUG_PLACE_TOOL
                                    Logger.Log("DEBUG: AltUse returns EQUIP for " + techType.AsString(false));
#endif
                                    __result = ItemAction.Equip;
                                    return false;
                                }
                                if (equipment.GetCompatibleSlot(equipmentType, out text))
                                {
#if DEBUG_PLACE_TOOL
                                    Logger.Log("DEBUG: AltUse returns SWAP for " + techType.AsString(false));
#endif
                                    __result = ItemAction.Swap;
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
