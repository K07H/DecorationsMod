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

        private static void MakeSnacksPlaceable()
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
        private static void MakeBatteriesPlaceable()
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

#if BELOWZERO
        private static readonly Dictionary<string, TechType> _materials = new Dictionary<string, TechType>
        {
            { "WorldEntities/Crafting/Silicone", TechType.Silicone },
            { "WorldEntities/Crafting/FiberMesh", TechType.FiberMesh },
            { "WorldEntities/Crafting/aramidfibers", TechType.AramidFibers },
            { "WorldEntities/Crafting/aerogel", TechType.Aerogel },
            { "WorldEntities/Crafting/TitaniumIngot", TechType.TitaniumIngot },
            { "WorldEntities/Crafting/PlasteelIngot", TechType.PlasteelIngot },
            { "WorldEntities/Crafting/Glass", TechType.Glass },
            { "WorldEntities/Crafting/EnameledGlass", TechType.EnameledGlass },
            { "WorldEntities/Crafting/CopperWire", TechType.CopperWire },

            { "WorldEntities/EnvironmentResources/SeaTreaderPoop", TechType.SeaTreaderPoop },
            //{ "WorldEntities/Alterra/Supplies/metal1", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal2", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal3", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal4", TechType.ScrapMetal },
            { "WorldEntities/EnvironmentResources/titanium", TechType.Titanium },
            { "WorldEntities/EnvironmentResources/crashpowder", TechType.CrashPowder },
            { "WorldEntities/EnvironmentResources/copper", TechType.Copper },
            { "WorldEntities/EnvironmentResources/sulphurcrystal", TechType.Sulphur },
            { "WorldEntities/EnvironmentResources/diamond", TechType.Diamond },
            { "WorldEntities/EnvironmentResources/gold", TechType.Gold },
            { "WorldEntities/EnvironmentResources/kyanite", TechType.Kyanite },
            { "WorldEntities/EnvironmentResources/lead", TechType.Lead },
            { "WorldEntities/EnvironmentResources/lithium", TechType.Lithium },
            { "WorldEntities/EnvironmentResources/magnetite", TechType.Magnetite },
            { "WorldEntities/EnvironmentResources/nickel", TechType.Nickel },
            { "WorldEntities/EnvironmentResources/quartz", TechType.Quartz },
            { "WorldEntities/EnvironmentResources/aluminumoxide", TechType.AluminumOxide },
            { "WorldEntities/EnvironmentResources/salt", TechType.Salt },
            { "WorldEntities/EnvironmentResources/silver", TechType.Silver },
            { "WorldEntities/EnvironmentResources/uraninitecrystal", TechType.UraniniteCrystal },
            
            { "WorldEntities/EnvironmentResources/bloodoil", TechType.BloodOil },
            //{ "WorldEntities/Seeds/CreepvineSeed", TechType.CreepvineSeedCluster },

            //{ "WorldEntities/Seeds/PurpleBrainCoralPiece", TechType.PurpleBrainCoralPiece },
            { "WorldEntities/Flora/Shared/JeweledDiskPiece", TechType.JeweledDiskPiece },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceBlue", TechType.BlueJeweledDisk },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceGreen", TechType.GreenJeweledDisk },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceRed", TechType.RedJeweledDisk },
            { "WorldEntities/Flora/Shared/CoralChunk", TechType.CoralChunk },
            //{ "WorldEntities/Creatures/GasPod", TechType.GasPod },
        };
#else
        private static readonly Dictionary<string, TechType> _materials = new Dictionary<string, TechType>
        {
            { "worldentities/natural/silicone", TechType.Silicone },
            { "worldentities/natural/fibermesh", TechType.FiberMesh },
            { "worldentities/natural/aramidfibers", TechType.AramidFibers },
            { "worldentities/natural/aerogel", TechType.Aerogel },
            { "worldentities/natural/titaniumingot", TechType.TitaniumIngot },
            { "worldentities/natural/plasteelingot", TechType.PlasteelIngot },
            { "worldentities/natural/glass", TechType.Glass },
            { "worldentities/natural/enameledglass", TechType.EnameledGlass },
            { "worldentities/natural/copperwire", TechType.CopperWire },

            { "worldentities/natural/seatreaderpoop", TechType.SeaTreaderPoop },
            //{ "worldentities/natural/metal1", TechType.ScrapMetal },
            //{ "worldentities/natural/metal2", TechType.ScrapMetal },
            //{ "worldentities/natural/metal3", TechType.ScrapMetal },
            //{ "worldentities/natural/metal4", TechType.ScrapMetal },
            { "worldentities/natural/titanium", TechType.Titanium },
            { "worldentities/natural/crashpowder", TechType.CrashPowder },
            { "worldentities/natural/copper", TechType.Copper },
            { "worldentities/natural/sulphurcrystal", TechType.Sulphur },
            { "worldentities/natural/diamond", TechType.Diamond },
            { "worldentities/natural/gold", TechType.Gold },
            { "worldentities/natural/kyanite", TechType.Kyanite },
            { "worldentities/natural/lead", TechType.Lead },
            { "worldentities/natural/lithium", TechType.Lithium },
            { "worldentities/natural/magnetite", TechType.Magnetite },
            { "worldentities/natural/nickel", TechType.Nickel },
            { "worldentities/natural/quartz", TechType.Quartz },
            { "worldentities/natural/aluminumoxide", TechType.AluminumOxide },
            { "worldentities/natural/salt", TechType.Salt },
            { "worldentities/natural/silver", TechType.Silver },
            { "worldentities/natural/uraninitecrystal", TechType.UraniniteCrystal },

            { "worldentities/natural/bloodoil", TechType.BloodOil },
            //{ "worldentities/natural/creepvineseedcluster", TechType.CreepvineSeedCluster },

            //{ "worldentities/seeds/purplebraincoralpiece", TechType.PurpleBrainCoralPiece },
            { "worldentities/doodads/coral_reef/jeweleddiskpiece", TechType.JeweledDiskPiece },
            { "worldentities/doodads/coral_reef/coralchunk", TechType.CoralChunk },
            //{ "worldentities/creatures/gaspod", TechType.GasPod },
        };
#endif

        private static void MakeMaterialPlaceable(TechType materialTechType, string materialPath)
        {
            GameObject mat = Resources.Load<GameObject>(materialPath);
            if (mat != null)
            {
                if (materialTechType == TechType.FiberMesh)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<FiberMesh_PT>();
                }
                else if (materialTechType == TechType.AramidFibers)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<AramidFibers_PT>();
                }
                else if (materialTechType == TechType.SeaTreaderPoop)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<SeaTreaderPoop_PT>();
                }
                else if (materialTechType == TechType.Diamond)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Diamond_PT>();
                }
                else if (materialTechType == TechType.Lithium)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Lithium_PT>();
                }
                else if (materialTechType == TechType.Magnetite)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Magnetite_PT>();
                }
                else if (materialTechType == TechType.Nickel)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Nickel_PT>();
                }
                else if (materialTechType == TechType.Quartz)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Quartz_PT>();
                }
                else if (materialTechType == TechType.Sulphur)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Sulphur_PT>();
                }
                else if (materialTechType == TechType.Glass)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Glass_PT>();
                }
                else if (materialTechType == TechType.EnameledGlass)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<EnameledGlass_PT>();
                }
                else if (materialTechType == TechType.PlasteelIngot)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<PlasteelIngot_PT>();
                }
                else if (materialTechType == TechType.Silicone)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Silicone_PT>();
                }
                else if (materialTechType == TechType.CopperWire)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<CopperWire_PT>();
                }
                else if (materialTechType == TechType.Silver)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Silver_PT>();
                }
                else if (materialTechType == TechType.Gold)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Gold_PT>();
                }
                else if (materialTechType == TechType.Salt)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Salt_PT>();
                }
                else if (materialTechType == TechType.BloodOil)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<BloodOil_PT>();
                }
                else if (materialTechType == TechType.Titanium)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Titanium_PT>();
                }
                else if (materialTechType == TechType.Lead)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Lead_PT>();
                }
                else if (materialTechType == TechType.UraniniteCrystal)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<UraniniteCrystal_PT>();
                }
                else if (materialTechType == TechType.Kyanite)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Kyanite_PT>();
                }
                else if (materialTechType == TechType.JeweledDiskPiece)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<JeweledDiskPiece_PT>();
                }
                else if (materialTechType == TechType.AluminumOxide)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<AluminumOxide_PT>();
                }
                else if (materialTechType == TechType.CoralChunk)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<CoralChunk_PT>();
                }
                else if (materialTechType == TechType.Copper)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<Copper_PT>();
                }
                else if (materialTechType == TechType.TitaniumIngot)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<TitaniumIngot_PT>();
                }
                else if (materialTechType == TechType.CrashPowder)
                {
                    mat.AddComponent<CustomPlaceToolController>();
                    mat.AddComponent<CrashPowder_PT>();
                }
                MakeItemPlaceable(materialTechType, mat);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type[{0}]", materialPath);
#endif
        }

        private static void MakeRemainingMaterialsPlaceable()
        {
            foreach (KeyValuePair<string, TechType> mat in _materials)
                MakeMaterialPlaceable(mat.Value, mat.Key);
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

                // Other materials
                if (ConfigSwitcher.EnablePlaceOtherMaterials)
                    MakeRemainingMaterialsPlaceable();

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
