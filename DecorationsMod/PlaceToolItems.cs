using DecorationsMod.Controllers;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod
{
    public static class PlaceToolItems
    {
        private static void MakeItemPlaceable(TechType techType, GameObject item, Collider collider = null)
        {
            // We can pick this item
            PrefabsHelper.SetDefaultPickupable(item);

            // We can place this item
            PrefabsHelper.SetDefaultPlaceTool(item, collider);

            // Add TechType to the hand-equipments
            SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(techType, EquipmentType.Hand);

            // Set as selectable item.
            SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(techType, QuickSlotType.Selectable);
        }

        public static void MakeSnacksPlaceable()
        {
            GameObject snack1 = Resources.Load<GameObject>("WorldEntities/Food/Snack1");
            if (snack1 != null)
            {
                BoxCollider snack1Collider = snack1.AddComponent<BoxCollider>();
                snack1Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack1, snack1, snack1Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack1");
#endif
            GameObject snack2 = Resources.Load<GameObject>("WorldEntities/Food/Snack2");
            if (snack2 != null)
            {
                BoxCollider snack2Collider = snack2.AddComponent<BoxCollider>();
                snack2Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack2, snack2, snack2Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack2");
#endif
            GameObject snack3 = Resources.Load<GameObject>("WorldEntities/Food/Snack3");
            if (snack3 != null)
            {
                BoxCollider snack3Collider = snack3.AddComponent<BoxCollider>();
                snack3Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack3, snack3, snack3Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack3");
#endif

            // Swap Snack2 and Snack3 techtypes (as tooltips do not match models)
            if (snack2 != null && snack3 != null)
            {
                var snack2PrefabId = snack2.GetComponent<PrefabIdentifier>();
                var snack3PrefabId = snack3.GetComponent<PrefabIdentifier>();
                var snack2TechTag = snack2.GetComponent<TechTag>();
                var snack3TechTag = snack3.GetComponent<TechTag>();
                string tmpclassid = snack2PrefabId.ClassId;
                snack2PrefabId.ClassId = snack3PrefabId.ClassId;
                snack3PrefabId.ClassId = tmpclassid;
                string tmpname = snack2PrefabId.name;
                snack2PrefabId.name = snack3PrefabId.name;
                snack3PrefabId.name = tmpname;
                TechType tmpTechType = snack2TechTag.type;
                snack2TechTag.type = snack3TechTag.type;
                snack3TechTag.type = tmpTechType;
            }
        }

        private static bool _batteriesMadePlaceable = false;
        public static void MakeBatteriesPlaceable()
        {
            if (!_batteriesMadePlaceable)
            {
                GameObject powercell = Resources.Load<GameObject>("WorldEntities/Tools/PowerCell");
                if (powercell != null)
                {
                    powercell.AddComponent<CustomPlaceToolController>();
                    powercell.AddComponent<PowerCell_PT>();
                    MakeItemPlaceable(TechType.PowerCell, powercell);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PowerCell");
#endif
                GameObject battery = Resources.Load<GameObject>("WorldEntities/Tools/Battery");
                if (battery != null)
                {
                    battery.AddComponent<CustomPlaceToolController>();
                    battery.AddComponent<Battery_PT>();
                    MakeItemPlaceable(TechType.Battery, battery);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PowerCell");
#endif
                GameObject ionpowercell = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonPowerCell");
                if (ionpowercell != null)
                {
                    ionpowercell.AddComponent<CustomPlaceToolController>();
                    ionpowercell.AddComponent<IonPowerCell_PT>();
                    MakeItemPlaceable(TechType.PrecursorIonPowerCell, ionpowercell);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PrecursorIonPowerCell");
#endif
                GameObject ionbattery = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonBattery");
                if (ionbattery != null)
                {
                    ionbattery.AddComponent<CustomPlaceToolController>();
                    ionbattery.AddComponent<IonBattery_PT>();
                    MakeItemPlaceable(TechType.PrecursorIonBattery, ionbattery);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PrecursorIonBattery");
#endif

                _batteriesMadePlaceable = true;
            }
        }

        private static bool _madeItemsPlaceable = false;
        public static void MakeItemsPlaceable()
        {
            if (!_madeItemsPlaceable)
            {
                Logger.Log("INFO: Making items placeable/pickupable...");

                // Chimicals
#if SUBNAUTICA
                GameObject bleach = Resources.Load<GameObject>("WorldEntities/Natural/bleach");
#else
                GameObject bleach = Resources.Load<GameObject>("WorldEntities/Crafting/Bleach");
#endif
                if (bleach != null)
                {
                    bleach.AddComponent<CustomPlaceToolController>();
                    bleach.AddComponent<Bleach_PT>();
                    MakeItemPlaceable(TechType.Bleach, bleach);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "bleach");
#endif

#if SUBNAUTICA
                GameObject lubricant = Resources.Load<GameObject>("WorldEntities/Natural/lubricant");
#else
                GameObject lubricant = Resources.Load<GameObject>("WorldEntities/Crafting/Lubricant");
#endif
                if (lubricant != null)
                {
                    lubricant.AddComponent<CustomPlaceToolController>();
                    lubricant.AddComponent<Lubricant_PT>();
                    MakeItemPlaceable(TechType.Lubricant, lubricant);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "lubricant");
#endif

#if SUBNAUTICA
                GameObject polyaniline = Resources.Load<GameObject>("WorldEntities/Natural/polyaniline");
#else
                GameObject polyaniline = Resources.Load<GameObject>("WorldEntities/Crafting/polyaniline");
#endif
                if (polyaniline != null)
                    MakeItemPlaceable(TechType.Polyaniline, polyaniline);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "polyaniline");
#endif

#if SUBNAUTICA
                GameObject benzene = Resources.Load<GameObject>("WorldEntities/Natural/benzene");
#else
                GameObject benzene = Resources.Load<GameObject>("WorldEntities/Crafting/benzene");
#endif
                if (benzene != null)
                    MakeItemPlaceable(TechType.Benzene, benzene);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "benzene");
#endif

#if SUBNAUTICA
                GameObject hydrochloricacid = Resources.Load<GameObject>("WorldEntities/Natural/hydrochloricacid");
#else
                GameObject hydrochloricacid = Resources.Load<GameObject>("WorldEntities/Crafting/hydrochloricacid");
#endif
                if (hydrochloricacid != null)
                    MakeItemPlaceable(TechType.HydrochloricAcid, hydrochloricacid);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "hydrochloricacid");
#endif

#if SUBNAUTICA
                GameObject hatchingenzymes = Resources.Load<GameObject>("WorldEntities/Natural/HatchingEnzymes");
#else
                GameObject hatchingenzymes = Resources.Load<GameObject>("WorldEntities/Crafting/HatchingEnzymes");
#endif
                if (hatchingenzymes != null)
                    MakeItemPlaceable(TechType.HatchingEnzymes, hatchingenzymes);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "HatchingEnzymes");
#endif

                // Food & water
                GameObject coffee = Resources.Load<GameObject>("WorldEntities/Food/Coffee");
                if (coffee != null)
                    MakeItemPlaceable(TechType.Coffee, coffee);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "Coffee");
#endif
                GameObject bigfilteredwater = Resources.Load<GameObject>("WorldEntities/Food/BigFilteredWater");
                if (bigfilteredwater != null)
                    MakeItemPlaceable(TechType.BigFilteredWater, bigfilteredwater);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "BigFilteredWater");
#endif
                GameObject disinfectedwater = Resources.Load<GameObject>("WorldEntities/Food/DisinfectedWater");
                if (disinfectedwater != null)
                {
                    disinfectedwater.AddComponent<CustomPlaceToolController>();
                    disinfectedwater.AddComponent<DisinfectedWater_PT>();
                    MakeItemPlaceable(TechType.DisinfectedWater, disinfectedwater);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "DisinfectedWater");
#endif
                GameObject filteredwater = Resources.Load<GameObject>("WorldEntities/Food/FilteredWater");
                if (filteredwater != null)
                {
                    filteredwater.AddComponent<CustomPlaceToolController>();
                    filteredwater.AddComponent<FilteredWater_PT>();
                    MakeItemPlaceable(TechType.FilteredWater, filteredwater);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "FilteredWater");
#endif

                // Snacks
                MakeSnacksPlaceable();

                // Electronics
#if SUBNAUTICA
                GameObject wiringkit = Resources.Load<GameObject>("WorldEntities/Natural/wiringkit");
#else
                GameObject wiringkit = Resources.Load<GameObject>("WorldEntities/Crafting/WiringKit");
#endif
                if (wiringkit != null)
                {
                    wiringkit.AddComponent<CustomPlaceToolController>();
                    wiringkit.AddComponent<WiringKit_PT>();
                    MakeItemPlaceable(TechType.WiringKit, wiringkit);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "wiringkit");
#endif

#if SUBNAUTICA
                GameObject advancedwiringkit = Resources.Load<GameObject>("WorldEntities/Natural/advancedwiringkit");
#else
                GameObject advancedwiringkit = Resources.Load<GameObject>("WorldEntities/Crafting/AdvancedWiringKit");
#endif
                if (advancedwiringkit != null)
                {
                    advancedwiringkit.AddComponent<CustomPlaceToolController>();
                    advancedwiringkit.AddComponent<AdvancedWiringKit_PT>();
                    MakeItemPlaceable(TechType.AdvancedWiringKit, advancedwiringkit);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "advancedwiringkit");
#endif

#if SUBNAUTICA
                GameObject computerchip = Resources.Load<GameObject>("WorldEntities/Natural/computerchip");
#else
                GameObject computerchip = Resources.Load<GameObject>("WorldEntities/EnvironmentResources/ComputerChip");
#endif
                if (computerchip != null)
                {
                    computerchip.AddComponent<CustomPlaceToolController>();
                    computerchip.AddComponent<ComputerChip_PT>();
                    MakeItemPlaceable(TechType.ComputerChip, computerchip);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "computerchip");
#endif

                if (ConfigSwitcher.EnablePlaceBatteries)
                    MakeBatteriesPlaceable();

                // Precursor
#if SUBNAUTICA
                GameObject ionCrystal = Resources.Load<GameObject>("WorldEntities/Natural/PrecursorIonCrystal");
#else
                GameObject ionCrystal = Resources.Load<GameObject>("WorldEntities/EnvironmentResources/PrecursorIonCrystal");
#endif
                if (ionCrystal != null)
                    MakeItemPlaceable(TechType.PrecursorIonCrystal, ionCrystal);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "PrecursorIonCrystal");
#endif

                // Others
#if SUBNAUTICA
                GameObject stalkertooth = Resources.Load<GameObject>("WorldEntities/Natural/stalkertooth");
#else
                GameObject stalkertooth = Resources.Load<GameObject>("WorldEntities/EnvironmentResources/StalkerTooth");
#endif
                if (stalkertooth != null)
                {
                    stalkertooth.AddComponent<CustomPlaceToolController>();
                    stalkertooth.AddComponent<StalkerTooth_PT>();
                    MakeItemPlaceable(TechType.StalkerTooth, stalkertooth);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "stalkertooth");
#endif

#if SUBNAUTICA
                GameObject firstaidkit = Resources.Load<GameObject>("WorldEntities/Natural/firstaidkit");
#else
                GameObject firstaidkit = Resources.Load<GameObject>("WorldEntities/Crafting/FirstAidKit");
#endif
                if (firstaidkit != null)
                    MakeItemPlaceable(TechType.FirstAidKit, firstaidkit);
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "firstaidkit");
#endif

                _madeItemsPlaceable = true;
            }
        }
    }
}
