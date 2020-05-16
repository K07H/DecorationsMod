using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
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

        public static void MakeEggsPlaceable()
        {
            GameObject egg1 = Resources.Load<GameObject>("WorldEntities/Eggs/BonesharkEgg");
            if (egg1 != null)
            {
                MakeItemPlaceable(TechType.BonesharkEgg, egg1);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.BonesharkEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.BonesharkEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/BonesharkEgg");
            GameObject egg2 = Resources.Load<GameObject>("WorldEntities/Eggs/CrabsnakeEgg");
            if (egg2 != null)
            {
                MakeItemPlaceable(TechType.CrabsnakeEgg, egg2);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.CrabsnakeEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.CrabsnakeEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/CrabsnakeEgg");
            GameObject egg3 = Resources.Load<GameObject>("WorldEntities/Eggs/CrabSquidEgg");
            if (egg3 != null)
            {
                MakeItemPlaceable(TechType.CrabsquidEgg, egg3);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.CrabsquidEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.CrabsquidEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/CrabSquidEgg");
            GameObject egg4 = Resources.Load<GameObject>("WorldEntities/Eggs/CrashEgg");
            if (egg4 != null)
            {
                MakeItemPlaceable(TechType.CrashEgg, egg4);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.CrashEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.CrashEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/CrashEgg");
            GameObject egg5 = Resources.Load<GameObject>("WorldEntities/Eggs/CuteEgg");
            if (egg5 != null)
            {
                MakeItemPlaceable(TechType.CutefishEgg, egg5);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.CutefishEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.CutefishEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/CuteEgg");
            GameObject egg6 = Resources.Load<GameObject>("WorldEntities/Eggs/GasopodEgg");
            if (egg6 != null)
            {
                MakeItemPlaceable(TechType.GasopodEgg, egg6);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.GasopodEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.GasopodEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/GasopodEgg");
            GameObject egg7 = Resources.Load<GameObject>("WorldEntities/Eggs/JellyrayEgg");
            if (egg7 != null)
            {
                MakeItemPlaceable(TechType.JellyrayEgg, egg7);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.JellyrayEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.JellyrayEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/JellyrayEgg");
            GameObject egg8 = Resources.Load<GameObject>("WorldEntities/Eggs/JumperEgg");
            if (egg8 != null)
            {
                MakeItemPlaceable(TechType.JumperEgg, egg8);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.JumperEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.JumperEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/JumperEgg");
            GameObject egg9 = Resources.Load<GameObject>("WorldEntities/Eggs/LavaLizardEgg");
            if (egg9 != null)
            {
                MakeItemPlaceable(TechType.LavaLizardEgg, egg9);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.LavaLizardEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.LavaLizardEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/LavaLizardEgg");
            GameObject egg10 = Resources.Load<GameObject>("WorldEntities/Eggs/MesmerEgg");
            if (egg10 != null)
            {
                MakeItemPlaceable(TechType.MesmerEgg, egg10);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.MesmerEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.MesmerEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/MesmerEgg");
            GameObject egg11 = Resources.Load<GameObject>("WorldEntities/Eggs/RabbitRayEgg");
            if (egg11 != null)
            {
                MakeItemPlaceable(TechType.RabbitrayEgg, egg11);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.RabbitrayEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.RabbitrayEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/RabbitRayEgg");
            GameObject egg12 = Resources.Load<GameObject>("WorldEntities/Eggs/ReefbackEgg");
            if (egg12 != null)
            {
                MakeItemPlaceable(TechType.ReefbackEgg, egg12);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.ReefbackEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.ReefbackEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/ReefbackEgg");
            GameObject egg13 = Resources.Load<GameObject>("WorldEntities/Eggs/SandsharkEgg");
            if (egg13 != null)
            {
                MakeItemPlaceable(TechType.SandsharkEgg, egg13);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.SandsharkEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.SandsharkEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/SandsharkEgg");
            GameObject egg14 = Resources.Load<GameObject>("WorldEntities/Eggs/ShockerEgg");
            if (egg14 != null)
            {
                egg14.AddComponent<Egg14_PT>();
                MakeItemPlaceable(TechType.ShockerEgg, egg14);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.ShockerEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.ShockerEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/ShockerEgg");
            GameObject egg15 = Resources.Load<GameObject>("WorldEntities/Eggs/SpadefishEgg");
            if (egg15 != null)
            {
                MakeItemPlaceable(TechType.SpadefishEgg, egg15);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.SpadefishEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.SpadefishEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/SpadefishEgg");
            GameObject egg16 = Resources.Load<GameObject>("WorldEntities/Eggs/StalkerEgg");
            if (egg16 != null)
            {
                MakeItemPlaceable(TechType.StalkerEgg, egg16);
                SMLHelper.V2.Handlers.CraftDataHandler.SetEquipmentType(TechType.StalkerEggUndiscovered, EquipmentType.Hand);
                SMLHelper.V2.Handlers.CraftDataHandler.SetQuickSlotType(TechType.StalkerEggUndiscovered, QuickSlotType.Selectable);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Eggs/StalkerEgg");
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
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack1");
            GameObject snack2 = Resources.Load<GameObject>("WorldEntities/Food/Snack2");
            if (snack2 != null)
            {
                BoxCollider snack2Collider = snack2.AddComponent<BoxCollider>();
                snack2Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack2, snack2, snack2Collider);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack2");
            GameObject snack3 = Resources.Load<GameObject>("WorldEntities/Food/Snack3");
            if (snack3 != null)
            {
                BoxCollider snack3Collider = snack3.AddComponent<BoxCollider>();
                snack3Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack3, snack3, snack3Collider);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Snack3");

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

        public static void MakeItemsPlaceable()
        {
            Logger.Log("Making some existing items positionable/pickupable...");

            // Chimicals
            GameObject bleach = Resources.Load<GameObject>("WorldEntities/Natural/bleach");
            if (bleach != null)
            {
                bleach.AddComponent<Bleach_PT>();
                MakeItemPlaceable(TechType.Bleach, bleach);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/bleach");
            GameObject lubricant = Resources.Load<GameObject>("WorldEntities/Natural/lubricant");
            if (lubricant != null)
            {
                lubricant.AddComponent<Lubricant_PT>();
                MakeItemPlaceable(TechType.Lubricant, lubricant);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/lubricant");
            GameObject polyaniline = Resources.Load<GameObject>("WorldEntities/Natural/polyaniline");
            if (polyaniline != null)
                MakeItemPlaceable(TechType.Polyaniline, polyaniline);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/polyaniline");
            GameObject benzene = Resources.Load<GameObject>("WorldEntities/Natural/benzene");
            if (benzene != null)
                MakeItemPlaceable(TechType.Benzene, benzene);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/benzene");
            GameObject hydrochloricacid = Resources.Load<GameObject>("WorldEntities/Natural/hydrochloricacid");
            if (hydrochloricacid != null)
                MakeItemPlaceable(TechType.HydrochloricAcid, hydrochloricacid);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/hydrochloricacid");
            GameObject hatchingenzymes = Resources.Load<GameObject>("WorldEntities/Natural/HatchingEnzymes");
            if (hatchingenzymes != null)
                MakeItemPlaceable(TechType.HatchingEnzymes, hatchingenzymes);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/HatchingEnzymes");

            // Food & water
            GameObject coffee = Resources.Load<GameObject>("WorldEntities/Food/Coffee");
            if (coffee != null)
                MakeItemPlaceable(TechType.Coffee, coffee);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/Coffee");
            GameObject bigfilteredwater = Resources.Load<GameObject>("WorldEntities/Food/BigFilteredWater");
            if (bigfilteredwater != null)
                MakeItemPlaceable(TechType.BigFilteredWater, bigfilteredwater);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/BigFilteredWater");
            GameObject disinfectedwater = Resources.Load<GameObject>("WorldEntities/Food/DisinfectedWater");
            if (disinfectedwater != null)
            {
                disinfectedwater.AddComponent<DisinfectedWater_PT>();
                MakeItemPlaceable(TechType.DisinfectedWater, disinfectedwater);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/DisinfectedWater");
            GameObject filteredwater = Resources.Load<GameObject>("WorldEntities/Food/FilteredWater");
            if (filteredwater != null)
            {
                filteredwater.AddComponent<FilteredWater_PT>();
                MakeItemPlaceable(TechType.FilteredWater, filteredwater);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Food/FilteredWater");

            // Snacks
            MakeSnacksPlaceable();
            
            // Electronics
            GameObject wiringkit = Resources.Load<GameObject>("WorldEntities/Natural/wiringkit");
            if (wiringkit != null)
            {
                wiringkit.AddComponent<WiringKit_PT>();
                MakeItemPlaceable(TechType.WiringKit, wiringkit);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/wiringkit");
            GameObject advancedwiringkit = Resources.Load<GameObject>("WorldEntities/Natural/advancedwiringkit");
            if (advancedwiringkit != null)
            {
                advancedwiringkit.AddComponent<AdvancedWiringKit_PT>();
                MakeItemPlaceable(TechType.AdvancedWiringKit, advancedwiringkit);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/advancedwiringkit");
            GameObject computerchip = Resources.Load<GameObject>("WorldEntities/Natural/computerchip");
            if (computerchip != null)
            {
                computerchip.AddComponent<ComputerChip_PT>();
                MakeItemPlaceable(TechType.ComputerChip, computerchip);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/computerchip");
            if (ConfigSwitcher.EnablePlaceBatteries)
            {
                GameObject powercell = Resources.Load<GameObject>("WorldEntities/Tools/PowerCell");
                if (powercell != null)
                {
                    powercell.AddComponent<PowerCell_PT>();
                    MakeItemPlaceable(TechType.PowerCell, powercell);
                }
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PowerCell");
                GameObject battery = Resources.Load<GameObject>("WorldEntities/Tools/Battery");
                if (battery != null)
                {
                    battery.AddComponent<Battery_PT>();
                    MakeItemPlaceable(TechType.Battery, battery);
                }
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PowerCell");
                GameObject ionpowercell = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonPowerCell");
                if (ionpowercell != null)
                {
                    ionpowercell.AddComponent<IonPowerCell_PT>();
                    MakeItemPlaceable(TechType.PrecursorIonPowerCell, ionpowercell);
                }
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PrecursorIonPowerCell");
                GameObject ionbattery = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonBattery");
                if (ionbattery != null)
                {
                    ionbattery.AddComponent<IonBattery_PT>();
                    MakeItemPlaceable(TechType.PrecursorIonBattery, ionbattery);
                }
                else
                    Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Tools/PrecursorIonBattery");
            }

            // Eggs
            //MakeEggsPlaceable();

            // Precursor
            GameObject ionCrystal = Resources.Load<GameObject>("WorldEntities/Natural/PrecursorIonCrystal");
            if (ionCrystal != null)
                MakeItemPlaceable(TechType.PrecursorIonCrystal, ionCrystal);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/PrecursorIonCrystal");
            /*
            GameObject purpleKey = Resources.Load<GameObject>("WorldEntities/Doodads/Precursor/PrecursorKey_Purple");
            if (purpleKey != null)
            {
                purpleKey.AddComponent<PurpleKey_PT>();
                MakeItemPlaceable(TechType.PrecursorKey_Purple, purpleKey);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Doodads/Precursor/PrecursorKey_Purple");
            GameObject orangeKey = Resources.Load<GameObject>("WorldEntities/Doodads/Precursor/PrecursorKey_Orange");
            if (orangeKey != null)
            {
                orangeKey.AddComponent<OrangeKey_PT>();
                MakeItemPlaceable(TechType.PrecursorKey_Orange, orangeKey);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Doodads/Precursor/PrecursorKey_Orange");
            GameObject blueKey = Resources.Load<GameObject>("WorldEntities/Doodads/Precursor/PrecursorKey_Blue");
            if (blueKey != null)
            {
                blueKey.AddComponent<BlueKey_PT>();
                MakeItemPlaceable(TechType.PrecursorKey_Blue, blueKey);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Doodads/Precursor/PrecursorKey_Blue");
            GameObject redKey = Resources.Load<GameObject>("WorldEntities/Doodads/Precursor/PrecursorKey_Red");
            if (redKey != null)
            {
                redKey.AddComponent<RedKey_PT>();
                MakeItemPlaceable(TechType.PrecursorKey_Red, redKey);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Doodads/Precursor/PrecursorKey_Red");
            GameObject whiteKey = Resources.Load<GameObject>("WorldEntities/Doodads/Precursor/PrecursorKey_White");
            if (whiteKey != null)
            {
                whiteKey.AddComponent<WhiteKey_PT>();
                MakeItemPlaceable(TechType.PrecursorKey_White, whiteKey);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Doodads/Precursor/PrecursorKey_White");
            */

            // Others
            GameObject stalkertooth = Resources.Load<GameObject>("WorldEntities/Natural/stalkertooth");
            if (stalkertooth != null)
            {
                stalkertooth.AddComponent<StalkerTooth_PT>();
                MakeItemPlaceable(TechType.StalkerTooth, stalkertooth);
            }
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/stalkertooth");
            GameObject firstaidkit = Resources.Load<GameObject>("WorldEntities/Natural/firstaidkit");
            if (firstaidkit != null)
                MakeItemPlaceable(TechType.FirstAidKit, firstaidkit);
            else
                Logger.Log("WARNING: Could not load type[{0}]", "WorldEntities/Natural/firstaidkit");
        }
    }
}
