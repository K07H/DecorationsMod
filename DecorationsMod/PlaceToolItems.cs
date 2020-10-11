using DecorationsMod.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod
{
    public static class PlaceToolItems
    {
        private static readonly Dictionary<string, TechType> _batteries = new Dictionary<string, TechType>
        {
            { "WorldEntities/Tools/PowerCell", TechType.PowerCell },
            { "WorldEntities/Tools/Battery", TechType.Battery },
            { "WorldEntities/Tools/lithiumionbattery", TechType.LithiumIonBattery },
            { "WorldEntities/Tools/PrecursorIonPowerCell", TechType.PrecursorIonPowerCell },
            { "WorldEntities/Tools/PrecursorIonBattery", TechType.PrecursorIonBattery }
        };

#if BELOWZERO
        private static readonly Dictionary<string, TechType> _items = new Dictionary<string, TechType>
        {
            { "WorldEntities/Crafting/bleach", TechType.Bleach },
            { "WorldEntities/Crafting/lubricant", TechType.Lubricant },
            { "WorldEntities/Crafting/polyaniline", TechType.Polyaniline },
            { "WorldEntities/Crafting/benzene", TechType.Benzene },
            { "WorldEntities/Crafting/hydrochloricacid", TechType.HydrochloricAcid },
            { "WorldEntities/Crafting/HatchingEnzymes", TechType.HatchingEnzymes },
            { "WorldEntities/Food/Coffee", TechType.Coffee },
            { "WorldEntities/Food/BigFilteredWater", TechType.BigFilteredWater },
            { "WorldEntities/Food/DisinfectedWater", TechType.DisinfectedWater },
            { "WorldEntities/Food/FilteredWater", TechType.FilteredWater },
            { "WorldEntities/Crafting/WiringKit", TechType.WiringKit },
            { "WorldEntities/Crafting/AdvancedWiringKit", TechType.AdvancedWiringKit },
            { "WorldEntities/EnvironmentResources/ComputerChip", TechType.ComputerChip },
            { "WorldEntities/EnvironmentResources/PrecursorIonCrystal", TechType.PrecursorIonCrystal },
            { "WorldEntities/EnvironmentResources/StalkerTooth", TechType.StalkerTooth },
            { "WorldEntities/Crafting/FirstAidKit", TechType.FirstAidKit },
        };

        internal static readonly Dictionary<string, TechType> _materials = new Dictionary<string, TechType>
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
            //{ "WorldEntities/Alterra/Supplies/metal1", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal2", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal3", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal4", TechType.ScrapMetal },
            
            { "WorldEntities/EnvironmentResources/bloodoil", TechType.BloodOil },
            { "WorldEntities/Flora/Shared/JeweledDiskPiece", TechType.JeweledDiskPiece },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceBlue", TechType.BlueJeweledDisk },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceGreen", TechType.GreenJeweledDisk },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceRed", TechType.RedJeweledDisk },
            { "WorldEntities/Flora/Shared/CoralChunk", TechType.CoralChunk },
            //{ "WorldEntities/Seeds/CreepvineSeed", TechType.CreepvineSeedCluster },
            //{ "WorldEntities/Seeds/PurpleBrainCoralPiece", TechType.PurpleBrainCoralPiece },
            //{ "WorldEntities/Creatures/GasPod", TechType.GasPod },
        };
#else
        private static readonly Dictionary<string, TechType> _items = new Dictionary<string, TechType>
        {
            { "WorldEntities/Natural/bleach", TechType.Bleach },
            { "WorldEntities/Natural/lubricant", TechType.Lubricant },
            { "WorldEntities/Natural/polyaniline", TechType.Polyaniline },
            { "WorldEntities/Natural/benzene", TechType.Benzene },
            { "WorldEntities/Natural/hydrochloricacid", TechType.HydrochloricAcid },
            { "WorldEntities/Natural/HatchingEnzymes", TechType.HatchingEnzymes },
            { "WorldEntities/Food/Coffee", TechType.Coffee },
            { "WorldEntities/Food/BigFilteredWater", TechType.BigFilteredWater },
            { "WorldEntities/Food/DisinfectedWater", TechType.DisinfectedWater },
            { "WorldEntities/Food/FilteredWater", TechType.FilteredWater },
            { "WorldEntities/Natural/wiringkit", TechType.WiringKit },
            { "WorldEntities/Natural/advancedwiringkit", TechType.AdvancedWiringKit },
            { "WorldEntities/Natural/computerchip", TechType.ComputerChip },
            { "WorldEntities/Natural/PrecursorIonCrystal", TechType.PrecursorIonCrystal },
            { "WorldEntities/Natural/stalkertooth", TechType.StalkerTooth },
            { "WorldEntities/Natural/firstaidkit", TechType.FirstAidKit },
        };

        internal static readonly Dictionary<string, TechType> _materials = new Dictionary<string, TechType>
        {
            { "WorldEntities/Natural/silicone", TechType.Silicone },
            { "WorldEntities/Natural/fibermesh", TechType.FiberMesh },
            { "WorldEntities/Natural/aramidfibers", TechType.AramidFibers },
            { "WorldEntities/Natural/aerogel", TechType.Aerogel },
            { "WorldEntities/Natural/titaniumingot", TechType.TitaniumIngot },
            { "WorldEntities/Natural/plasteelingot", TechType.PlasteelIngot },
            { "WorldEntities/Natural/glass", TechType.Glass },
            { "WorldEntities/Natural/enameledglass", TechType.EnameledGlass },
            { "WorldEntities/Natural/copperwire", TechType.CopperWire },

            { "WorldEntities/Natural/seatreaderpoop", TechType.SeaTreaderPoop },
            { "WorldEntities/Natural/titanium", TechType.Titanium },
            { "WorldEntities/Natural/crashpowder", TechType.CrashPowder },
            { "WorldEntities/Natural/copper", TechType.Copper },
            { "WorldEntities/Natural/sulphurcrystal", TechType.Sulphur },
            { "WorldEntities/Natural/diamond", TechType.Diamond },
            { "WorldEntities/Natural/gold", TechType.Gold },
            { "WorldEntities/Natural/kyanite", TechType.Kyanite },
            { "WorldEntities/Natural/lead", TechType.Lead },
            { "WorldEntities/Natural/lithium", TechType.Lithium },
            { "WorldEntities/Natural/magnetite", TechType.Magnetite },
            { "WorldEntities/Natural/nickel", TechType.Nickel },
            { "WorldEntities/Natural/quartz", TechType.Quartz },
            { "WorldEntities/Natural/aluminumoxide", TechType.AluminumOxide },
            { "WorldEntities/Natural/salt", TechType.Salt },
            { "WorldEntities/Natural/silver", TechType.Silver },
            { "WorldEntities/Natural/uraninitecrystal", TechType.UraniniteCrystal },
            //{ "WorldEntities/Natural/metal1", TechType.ScrapMetal },
            //{ "WorldEntities/Natural/metal2", TechType.ScrapMetal },
            //{ "WorldEntities/Natural/metal3", TechType.ScrapMetal },
            //{ "WorldEntities/Natural/metal4", TechType.ScrapMetal },

            { "WorldEntities/Natural/bloodoil", TechType.BloodOil },
            { "WorldEntities/doodads/coral_reef/jeweleddiskpiece", TechType.JeweledDiskPiece },
            { "WorldEntities/doodads/coral_reef/coralchunk", TechType.CoralChunk },
            //{ "worldentities/Natural/creepvineseedcluster", TechType.CreepvineSeedCluster },
            //{ "worldentities/seeds/purplebraincoralpiece", TechType.PurpleBrainCoralPiece },
            //{ "worldentities/creatures/gaspod", TechType.GasPod },
        };
#endif

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
                Logger.Log("WARNING: Could not load type [{0}]", "WorldEntities/Food/Snack2");
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
                Logger.Log("WARNING: Could not load type [{0}]", "WorldEntities/Food/Snack3");
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

        private static void MakeBatteryPlaceable(TechType batteryTechType, string batteryPath)
        {
            GameObject obj = Resources.Load<GameObject>(batteryPath);
            if (obj != null)
            {
                if (batteryTechType == TechType.Battery || batteryTechType == TechType.PrecursorIonBattery)
                {
                    obj.AddComponent<CustomPlaceToolController>();
                    obj.AddComponent<Battery_PT>();
                }
                else if (batteryTechType == TechType.LithiumIonBattery)
                {
                    obj.AddComponent<CustomPlaceToolController>();
                    obj.AddComponent<LithiumIonBattery_PT>();
                }
                else if (batteryTechType == TechType.PowerCell)
                {
                    obj.AddComponent<CustomPlaceToolController>();
                    obj.AddComponent<PowerCell_PT>();
                }
                else if (batteryTechType == TechType.PrecursorIonPowerCell)
                {
                    obj.AddComponent<CustomPlaceToolController>();
                    obj.AddComponent<IonPowerCell_PT>();
                }
                MakeItemPlaceable(batteryTechType, obj);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type [{0}]", batteryPath);
#endif
        }

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
                Logger.Log("WARNING: Could not load type [{0}]", materialPath);
#endif
        }

        private static void MakeItemPlaceable(TechType itemTechType, string itemPath)
        {
            GameObject item = Resources.Load<GameObject>(itemPath);
            if (item != null)
            {
                if (itemTechType == TechType.Bleach)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<Bleach_PT>();
                }
                else if (itemTechType == TechType.Lubricant)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<Lubricant_PT>();
                }
                else if (itemTechType == TechType.DisinfectedWater)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<DisinfectedWater_PT>();
                }
                else if (itemTechType == TechType.FilteredWater)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<FilteredWater_PT>();
                }
                else if (itemTechType == TechType.WiringKit)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<WiringKit_PT>();
                }
                else if (itemTechType == TechType.AdvancedWiringKit)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<AdvancedWiringKit_PT>();
                }
                else if (itemTechType == TechType.ComputerChip)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<ComputerChip_PT>();
                }
                else if (itemTechType == TechType.StalkerTooth)
                {
                    item.AddComponent<CustomPlaceToolController>();
                    item.AddComponent<StalkerTooth_PT>();
                }
                MakeItemPlaceable(itemTechType, item);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Log("WARNING: Could not load type [{0}]", itemPath);
#endif
        }

        private static bool _madeItemsPlaceable = false;
        public static void MakeItemsPlaceable()
        {
            if (!_madeItemsPlaceable)
            {
                Logger.Log("INFO: Making items placeable/pickupable...");

                // Decorative items
                foreach (KeyValuePair<string, TechType> it in _items)
                    MakeItemPlaceable(it.Value, it.Key);

                // Snacks
                MakeSnacksPlaceable();

                // Batteries/powercells
                if (ConfigSwitcher.EnablePlaceBatteries)
                    foreach (KeyValuePair<string, TechType> k in _batteries)
                        MakeBatteryPlaceable(k.Value, k.Key);

                // Other materials
                if (ConfigSwitcher.EnablePlaceOtherMaterials)
                    foreach (KeyValuePair<string, TechType> mat in _materials)
                        MakeMaterialPlaceable(mat.Value, mat.Key);

                _madeItemsPlaceable = true;
            }
        }
    }
}
