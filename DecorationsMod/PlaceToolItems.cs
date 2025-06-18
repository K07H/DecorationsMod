#if SUBNAUTICA_NAUTILUS
using Nautilus.Handlers;
#else
using SMLHelper.V2.Handlers;
#endif
using DecorationsMod.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod
{
    public static class PlaceToolItems
    {
        private static readonly Dictionary<string, TechType> _batteries = new Dictionary<string, TechType>
        {
            { "WorldEntities/Tools/PowerCell.prefab", TechType.PowerCell },
            { "WorldEntities/Tools/Battery.prefab", TechType.Battery },
            { "WorldEntities/Tools/LithiumIonBattery.prefab", TechType.LithiumIonBattery },
            { "WorldEntities/Tools/PrecursorIonPowerCell.prefab", TechType.PrecursorIonPowerCell },
            { "WorldEntities/Tools/PrecursorIonBattery.prefab", TechType.PrecursorIonBattery }
        };

#if SUBNAUTICA
        private static readonly Dictionary<string, TechType> _items = new Dictionary<string, TechType>
        {
            { "WorldEntities/Natural/Bleach.prefab", TechType.Bleach },
            { "WorldEntities/Natural/Lubricant.prefab", TechType.Lubricant },
            { "WorldEntities/Natural/polyaniline.prefab", TechType.Polyaniline },
            { "WorldEntities/Natural/benzene.prefab", TechType.Benzene },
            { "WorldEntities/Natural/hydrochloricacid.prefab", TechType.HydrochloricAcid },
            { "WorldEntities/Natural/HatchingEnzymes.prefab", TechType.HatchingEnzymes },
            { "WorldEntities/Food/Coffee.prefab", TechType.Coffee },
            { "WorldEntities/Food/BigFilteredWater.prefab", TechType.BigFilteredWater },
            { "WorldEntities/Food/DisinfectedWater.prefab", TechType.DisinfectedWater },
            { "WorldEntities/Food/FilteredWater.prefab", TechType.FilteredWater },
            { "WorldEntities/Natural/WiringKit.prefab", TechType.WiringKit },
            { "WorldEntities/Natural/AdvancedWiringKit.prefab", TechType.AdvancedWiringKit },
            { "WorldEntities/Natural/ComputerChip.prefab", TechType.ComputerChip },
            { "WorldEntities/Natural/PrecursorIonCrystal.prefab", TechType.PrecursorIonCrystal },
            { "WorldEntities/Natural/StalkerTooth.prefab", TechType.StalkerTooth },
            { "WorldEntities/Natural/FirstAidKit.prefab", TechType.FirstAidKit },
        };

        internal static readonly Dictionary<string, TechType> _materials = new Dictionary<string, TechType>
        {
            { "WorldEntities/Natural/Silicone.prefab", TechType.Silicone },
            { "WorldEntities/Natural/FiberMesh.prefab", TechType.FiberMesh },
            { "WorldEntities/Natural/aramidfibers.prefab", TechType.AramidFibers },
            { "WorldEntities/Natural/aerogel.prefab", TechType.Aerogel },
            { "WorldEntities/Natural/TitaniumIngot.prefab", TechType.TitaniumIngot },
            { "WorldEntities/Natural/PlasteelIngot.prefab", TechType.PlasteelIngot },
            { "WorldEntities/Natural/Glass.prefab", TechType.Glass },
            { "WorldEntities/Natural/EnameledGlass.prefab", TechType.EnameledGlass },
            { "WorldEntities/Natural/CopperWire.prefab", TechType.CopperWire },

            { "WorldEntities/Natural/SeaTreaderPoop.prefab", TechType.SeaTreaderPoop },
            { "WorldEntities/Natural/Titanium.prefab", TechType.Titanium },
            { "WorldEntities/Natural/CrashPowder.prefab", TechType.CrashPowder },
            { "WorldEntities/Natural/copper.prefab", TechType.Copper },
            { "WorldEntities/Natural/sulphurcrystal.prefab", TechType.Sulphur },
            { "WorldEntities/Natural/diamond.prefab", TechType.Diamond },
            { "WorldEntities/Natural/gold.prefab", TechType.Gold },
            { "WorldEntities/Natural/kyanite.prefab", TechType.Kyanite },
            { "WorldEntities/Natural/Lead.prefab", TechType.Lead },
            { "WorldEntities/Natural/lithium.prefab", TechType.Lithium },
            { "WorldEntities/Natural/magnetite.prefab", TechType.Magnetite },
            { "WorldEntities/Natural/nickel.prefab", TechType.Nickel },
            { "WorldEntities/Natural/quartz.prefab", TechType.Quartz },
            { "WorldEntities/Natural/aluminumoxide.prefab", TechType.AluminumOxide },
            { "WorldEntities/Natural/salt.prefab", TechType.Salt },
            { "WorldEntities/Natural/silver.prefab", TechType.Silver },
            { "WorldEntities/Natural/uraninitecrystal.prefab", TechType.UraniniteCrystal },
            //{ "WorldEntities/Natural/metal1.prefab", TechType.ScrapMetal },
            //{ "WorldEntities/Natural/metal2.prefab", TechType.ScrapMetal },
            //{ "WorldEntities/Natural/metal3.prefab", TechType.ScrapMetal },
            //{ "WorldEntities/Natural/metal4.prefab", TechType.ScrapMetal },

            { "WorldEntities/Natural/bloodoil.prefab", TechType.BloodOil },
            { "WorldEntities/Doodads/Coral_reef/JeweledDiskPiece.prefab", TechType.JeweledDiskPiece },
            { "WorldEntities/Doodads/Coral_reef/JeweledDiskPieceBlue.prefab", TechType.BlueJeweledDisk },
            { "WorldEntities/Doodads/Coral_reef/JeweledDiskPieceGreen.prefab", TechType.GreenJeweledDisk },
            { "WorldEntities/Doodads/Coral_reef/JeweledDiskPieceRed.prefab", TechType.RedJeweledDisk },
            { "WorldEntities/Doodads/Coral_reef/CoralChunk.prefab", TechType.CoralChunk },
            //{ "WorldEntities/Natural/creepvineseedcluster.prefab", TechType.CreepvineSeedCluster },
            //{ "WorldEntities/seeds/purplebraincoralpiece.prefab", TechType.PurpleBrainCoralPiece },
            //{ "WorldEntities/creatures/gaspod.prefab", TechType.GasPod },
        };
#else
        private static readonly Dictionary<string, TechType> _items = new Dictionary<string, TechType>
        {
            { "WorldEntities/Crafting/bleach.prefab", TechType.Bleach },
            { "WorldEntities/Crafting/lubricant.prefab", TechType.Lubricant },
            { "WorldEntities/Crafting/polyaniline.prefab", TechType.Polyaniline },
            { "WorldEntities/Crafting/benzene.prefab", TechType.Benzene },
            { "WorldEntities/Crafting/hydrochloricacid.prefab", TechType.HydrochloricAcid },
            { "WorldEntities/Crafting/HatchingEnzymes.prefab", TechType.HatchingEnzymes },
            { "WorldEntities/Food/Coffee.prefab", TechType.Coffee },
            { "WorldEntities/Food/BigFilteredWater.prefab", TechType.BigFilteredWater },
            { "WorldEntities/Food/DisinfectedWater.prefab", TechType.DisinfectedWater },
            { "WorldEntities/Food/FilteredWater.prefab", TechType.FilteredWater },
            { "WorldEntities/Crafting/WiringKit.prefab", TechType.WiringKit },
            { "WorldEntities/Crafting/AdvancedWiringKit.prefab", TechType.AdvancedWiringKit },
            { "WorldEntities/EnvironmentResources/ComputerChip.prefab", TechType.ComputerChip },
            { "WorldEntities/EnvironmentResources/PrecursorIonCrystal.prefab", TechType.PrecursorIonCrystal },
            { "WorldEntities/EnvironmentResources/StalkerTooth.prefab", TechType.StalkerTooth },
            { "WorldEntities/Crafting/FirstAidKit.prefab", TechType.FirstAidKit },
        };

        internal static readonly Dictionary<string, TechType> _materials = new Dictionary<string, TechType>
        {
            { "WorldEntities/Crafting/Silicone.prefab", TechType.Silicone },
            { "WorldEntities/Crafting/FiberMesh.prefab", TechType.FiberMesh },
            { "WorldEntities/Crafting/aramidfibers.prefab", TechType.AramidFibers },
            { "WorldEntities/Crafting/aerogel.prefab", TechType.Aerogel },
            { "WorldEntities/Crafting/TitaniumIngot.prefab", TechType.TitaniumIngot },
            { "WorldEntities/Crafting/PlasteelIngot.prefab", TechType.PlasteelIngot },
            { "WorldEntities/Crafting/Glass.prefab", TechType.Glass },
            { "WorldEntities/Crafting/EnameledGlass.prefab", TechType.EnameledGlass },
            { "WorldEntities/Crafting/CopperWire.prefab", TechType.CopperWire },

            { "WorldEntities/EnvironmentResources/SeaTreaderPoop.prefab", TechType.SeaTreaderPoop },
            { "WorldEntities/EnvironmentResources/titanium.prefab", TechType.Titanium },
            { "WorldEntities/EnvironmentResources/crashpowder.prefab", TechType.CrashPowder },
            { "WorldEntities/EnvironmentResources/copper.prefab", TechType.Copper },
            { "WorldEntities/EnvironmentResources/sulphurcrystal.prefab", TechType.Sulphur },
            { "WorldEntities/EnvironmentResources/diamond.prefab", TechType.Diamond },
            { "WorldEntities/EnvironmentResources/gold.prefab", TechType.Gold },
            { "WorldEntities/EnvironmentResources/kyanite.prefab", TechType.Kyanite },
            { "WorldEntities/EnvironmentResources/lead.prefab", TechType.Lead },
            { "WorldEntities/EnvironmentResources/lithium.prefab", TechType.Lithium },
            { "WorldEntities/EnvironmentResources/magnetite.prefab", TechType.Magnetite },
            { "WorldEntities/EnvironmentResources/nickel.prefab", TechType.Nickel },
            { "WorldEntities/EnvironmentResources/quartz.prefab", TechType.Quartz },
            { "WorldEntities/EnvironmentResources/aluminumoxide.prefab", TechType.AluminumOxide },
            { "WorldEntities/EnvironmentResources/salt.prefab", TechType.Salt },
            { "WorldEntities/EnvironmentResources/silver.prefab", TechType.Silver },
            { "WorldEntities/EnvironmentResources/uraninitecrystal.prefab", TechType.UraniniteCrystal },
            //{ "WorldEntities/Alterra/Supplies/metal1.prefab", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal2.prefab", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal3.prefab", TechType.ScrapMetal },
            //{ "WorldEntities/Alterra/Supplies/metal4.prefab", TechType.ScrapMetal },
            
            { "WorldEntities/EnvironmentResources/bloodoil.prefab", TechType.BloodOil },
            { "WorldEntities/Flora/Shared/JeweledDiskPiece.prefab", TechType.JeweledDiskPiece },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceBlue.prefab", TechType.BlueJeweledDisk },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceGreen.prefab", TechType.GreenJeweledDisk },
            { "WorldEntities/Flora/Shared/JeweledDiskPieceRed.prefab", TechType.RedJeweledDisk },
            { "WorldEntities/Flora/Shared/CoralChunk.prefab", TechType.CoralChunk },
            //{ "WorldEntities/Seeds/CreepvineSeed.prefab", TechType.CreepvineSeedCluster },
            //{ "WorldEntities/Seeds/PurpleBrainCoralPiece.prefab", TechType.PurpleBrainCoralPiece },
            //{ "WorldEntities/Creatures/GasPod.prefab", TechType.GasPod },
        };
#endif

        private static void MakeItemPlaceable(TechType techType, GameObject item, Collider collider = null)
        {
            // We can pick this item
            PrefabsHelper.SetDefaultPickupable(item);

            // We can place this item
            PrefabsHelper.SetDefaultPlaceTool(item, collider);

            // Add TechType to the hand-equipments
            CraftDataHandler.SetEquipmentType(techType, EquipmentType.Hand);

            // Set as selectable item.
            CraftDataHandler.SetQuickSlotType(techType, QuickSlotType.Selectable);
        }

        private static void MakeSnacksPlaceable()
        {
            GameObject snack1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Food/Snack1.prefab");
            if (snack1 != null)
            {
                BoxCollider snack1Collider = snack1.AddComponent<BoxCollider>();
                snack1Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack1, snack1, snack1Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Warning("Could not load type [{0}]", "WorldEntities/Food/Snack1");
#endif
            GameObject snack2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Food/Snack2.prefab");
            if (snack2 != null)
            {
                BoxCollider snack2Collider = snack2.AddComponent<BoxCollider>();
                snack2Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack2, snack2, snack2Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Warning("Could not load type [{0}]", "WorldEntities/Food/Snack2");
#endif
            GameObject snack3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Food/Snack3.prefab");
            if (snack3 != null)
            {
                BoxCollider snack3Collider = snack3.AddComponent<BoxCollider>();
                snack3Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                MakeItemPlaceable(TechType.Snack3, snack3, snack3Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Warning("Could not load type [{0}]", "WorldEntities/Food/Snack3");
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
            GameObject obj = PrefabsHelper.LoadGameObjectFromFilename(batteryPath);
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
                Logger.Warning("Could not load type [{0}]", batteryPath);
#endif
        }

        private static void MakeMaterialPlaceable(TechType materialTechType, string materialPath)
        {
            GameObject mat = PrefabsHelper.LoadGameObjectFromFilename(materialPath);
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
                Logger.Warning("Could not load type [{0}]", materialPath);
#endif
        }

        private static void MakeItemPlaceable(TechType itemTechType, string itemPath)
        {
            GameObject item = PrefabsHelper.LoadGameObjectFromFilename(itemPath);
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
                Logger.Warning("Could not load type [{0}]", itemPath);
#endif
        }

        private static bool _madeItemsPlaceable = false;
        public static void MakeItemsPlaceable()
        {
            if (!_madeItemsPlaceable)
            {
                Logger.Info("Making items placeable/pickupable...");

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
