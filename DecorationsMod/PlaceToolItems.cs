using DecorationsMod.Controllers;
using Nautilus.Utility;
using System.Collections.Generic;
using UnityEngine;
using static CraftData;
using static VFXParticlesPool;

namespace DecorationsMod
{
    public class DecoEggInfos
    {
        public TechType DiscoveredTechType { get; set; }
        public TechType UndiscoveredTechType { get; set; }
        public float ScaleFactor { get; set; }

        public DecoEggInfos(TechType discoveredTechType, TechType undiscoveredTechType, float scaleFactor)
        {
            DiscoveredTechType = discoveredTechType;
            UndiscoveredTechType = undiscoveredTechType;
            ScaleFactor = scaleFactor;
        }
    }

    public class CraftableAndPlaceableItemInfos
    {
        public bool MakeCratable { get; set; }
        public bool MakePlaceable { get; set; }
        public TechType ItemTechType { get; set; }
        public List<Ingredient> Recipe { get; set; }
        public string VFXChild { get; set; }
        public float LocalMinY { get; set; }
        public float LocalMaxY { get; set; }
        public Vector3 PosOffset { get; set; }
        public Vector3 EulerOffset { get; set; }
        public float ScaleFactor { get; set; }

        public CraftableAndPlaceableItemInfos(bool makeCratable, bool makePlaceable, TechType itemTechType, List<Ingredient> recipe, string vfxChild, float localMinY, float localMaxY, Vector3 posOffset, Vector3 eulerOffset, float scaleFactor)
        {
            MakeCratable = makeCratable;
            MakePlaceable = makePlaceable;
            ItemTechType = itemTechType;
            Recipe = recipe;
            VFXChild = vfxChild;
            LocalMinY = localMinY;
            LocalMaxY = localMaxY;
            PosOffset = posOffset;
            EulerOffset = eulerOffset;
            ScaleFactor = scaleFactor;
        }
    }

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

        internal static readonly Dictionary<string, DecoEggInfos> _eggs = new Dictionary<string, DecoEggInfos>
        {
            { "WorldEntities/Eggs/BonesharkEgg.prefab", new DecoEggInfos(TechType.BonesharkEgg, TechType.BonesharkEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/CrabsnakeEgg.prefab", new DecoEggInfos(TechType.CrabsnakeEgg, TechType.CrabsnakeEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/CrabSquidEgg.prefab", new DecoEggInfos(TechType.CrabsquidEgg, TechType.CrabsquidEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/CrashEgg.prefab", new DecoEggInfos(TechType.CrashEgg, TechType.CrashEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/CuteEgg.prefab", new DecoEggInfos(TechType.CutefishEgg, TechType.CutefishEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/GasopodEgg.prefab", new DecoEggInfos(TechType.GasopodEgg, TechType.GasopodEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/JellyrayEgg.prefab", new DecoEggInfos(TechType.JellyrayEgg, TechType.JellyrayEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/JumperEgg.prefab", new DecoEggInfos(TechType.JumperEgg, TechType.JumperEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/LavaLizardEgg.prefab", new DecoEggInfos(TechType.LavaLizardEgg, TechType.LavaLizardEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/MesmerEgg.prefab", new DecoEggInfos(TechType.MesmerEgg, TechType.MesmerEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/RabbitRayEgg.prefab", new DecoEggInfos(TechType.RabbitrayEgg, TechType.RabbitrayEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/ReefbackEgg.prefab", new DecoEggInfos(TechType.ReefbackEgg, TechType.ReefbackEggUndiscovered, 0.5f) },
            { "WorldEntities/Eggs/SandsharkEgg.prefab", new DecoEggInfos(TechType.SandsharkEgg, TechType.SandsharkEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/ShockerEgg.prefab", new DecoEggInfos(TechType.ShockerEgg, TechType.ShockerEggUndiscovered, 1.0f) },
            { "WorldEntities/Eggs/SpadefishEgg.prefab", new DecoEggInfos(TechType.SpadefishEgg, TechType.SpadefishEggUndiscovered, 0.65f) },
            { "WorldEntities/Eggs/StalkerEgg.prefab", new DecoEggInfos(TechType.StalkerEgg, TechType.StalkerEggUndiscovered, 1.0f) }
        };

        internal static readonly Dictionary<string, CraftableAndPlaceableItemInfos> _craftableAndPlaceableItems = new Dictionary<string, CraftableAndPlaceableItemInfos>
        {
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_arcade_gorgetoy_01.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.ArcadeGorgetoy, new List<Ingredient>() { new Ingredient(TechType.FiberMesh, 2) }, "descent_arcade_gorgetoy_01", -0.1f, 0.6f, new Vector3(0f, 0f, 0.04f), new Vector3(-90f, 0f, 0f), 1.0f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_02.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.Cap1, new List<Ingredient>() { new Ingredient(TechType.FiberMesh, 1) }, "descent_plaza_shelf_cap_02", -0.2f, 0.2f, new Vector3(0f, 0.06f, 0.04f), new Vector3(-90f, -90f, 0f), 1.0f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_03.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.Cap2, new List<Ingredient>() { new Ingredient(TechType.FiberMesh, 1) }, "descent_plaza_shelf_cap_03", -0.2f, 0.2f, new Vector3(0f, 0.06f, 0.04f), new Vector3(-90f, -90f, 0f), 1.0f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_01.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.LabContainer, new List<Ingredient>() { new Ingredient(TechType.Glass, 2) }, "biodome_lab_containers_close_01", -0.1f, 0.8f, new Vector3(0f, 0f, 0.04f), new Vector3(0f, 0f, 0f), 1.0f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_02.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.LabContainer2, new List<Ingredient>() { new Ingredient(TechType.Glass, 1) }, "biodome_lab_containers_close_02", -0.1f, 0.5f, new Vector3(0f, 0f, 0.04f), new Vector3(0f, 0f, 0f), 1.0f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_tube_01.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.LabContainer3, new List<Ingredient>() { new Ingredient(TechType.Glass, 1) }, "biodome_lab_containers_tube_01", -0.1f, 0.36f, new Vector3(0f, 0f, 0.04f), new Vector3(0f, 0f, 0f), 1.0f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_01.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.LabEquipment1, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.Glass, 1) }, "discovery_lab_props_01", -0.2f, 0.6f, new Vector3(0f, 0f, 0.04f), new Vector3(-90f, 0f, 0f), 0.75f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_02.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.LabEquipment2, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.Glass, 1) }, "discovery_lab_props_02", -0.2f, 0.8f, new Vector3(0f, 0f, 0.04f), new Vector3(0f, 0f, 0f), 0.75f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_03.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.LabEquipment3, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.Glass, 1) }, "discovery_lab_props_03", -0.2f, 0.8f, new Vector3(0f, 0f, 0.04f), new Vector3(0f, 0f, 0f), 0.75f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.LuggageBag, new List<Ingredient>() { new Ingredient(TechType.FiberMesh, 2), new Ingredient(TechType.Silicone, 1) }, "model", -0.2f, 0.7f, new Vector3(0f, 0f, 0.04f), new Vector3(0f, 0f, 0f), 0.8f) },
            { "WorldEntities/Environment/Wrecks/poster_aurora.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.PosterAurora, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.FiberMesh, 1) }, "model", -0.5f, 0.2f, new Vector3(0f, 0.4f, 0.04f), new Vector3(0f, 0f, 0f), 0.25f) },
            { "WorldEntities/Environment/Wrecks/poster_exosuit_01.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.PosterExoSuit1, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.FiberMesh, 1) }, "model", -0.6f, 0.2f, new Vector3(0f, 0.5f, 0.04f), new Vector3(0f, 0f, 0f), 0.25f) },
            { "WorldEntities/Environment/Wrecks/poster_exosuit_02.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.PosterExoSuit2, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.FiberMesh, 1) }, "model", -0.6f, 0.2f, new Vector3(0f, 0.5f, 0.04f), new Vector3(0f, 0f, 0f), 0.25f) },
            { "WorldEntities/Environment/Wrecks/poster_kitty.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.PosterKitty, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.FiberMesh, 1) }, "model", -0.6f, 0.2f, new Vector3(0f, 0.5f, 0.04f), new Vector3(0f, 0f, 0f), 0.25f) },
            { "WorldEntities/Environment/Wrecks/Poster.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.Poster, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.FiberMesh, 1) }, "model", -0.6f, 0.2f, new Vector3(0f, 0.5f, 0.04f), new Vector3(0f, 0f, 0f), 0.25f) },
            { "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir.prefab", new CraftableAndPlaceableItemInfos(true, false, TechType.StarshipSouvenir, new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(TechType.Glass, 1) }, "starship_souvenir", -0.2f, 0.5f, new Vector3(0f, 0.1f, 0.04f), new Vector3(-90f, -90f, 0f), 1.0f) },
            { "WorldEntities/Doodads/Precursor/PrecursorKey_Blue.prefab", new CraftableAndPlaceableItemInfos(false, true, TechType.PrecursorKey_Blue, null, null, -0.2f, 0.3f, new Vector3(0f, 0.05f, 0.04f), new Vector3(0f, 0f, 0f), 0.8f) },
            { "WorldEntities/Doodads/Precursor/PrecursorKey_Orange.prefab", new CraftableAndPlaceableItemInfos(false, true, TechType.PrecursorKey_Orange, null, null, -0.2f, 0.3f, new Vector3(0f, 0.05f, 0.04f), new Vector3(0f, 0f, 0f), 0.8f) },
            { "WorldEntities/Doodads/Precursor/PrecursorKey_Purple.prefab", new CraftableAndPlaceableItemInfos(false, true, TechType.PrecursorKey_Purple, null, null, -0.2f, 0.3f, new Vector3(0f, 0.05f, 0.04f), new Vector3(0f, 0f, 0f), 0.8f) },
            { "WorldEntities/Doodads/Precursor/PrecursorKey_Red.prefab", new CraftableAndPlaceableItemInfos(true, true, TechType.PrecursorKey_Red, new List<Ingredient>() { new Ingredient(ConfigSwitcher.PrecursorKeysResource, ConfigSwitcher.PrecursorKeysResourceAmount) },  null, -0.2f, 0.3f, new Vector3(0f, 0.05f, 0.04f), new Vector3(0f, 0f, 0f), 0.8f) },
            { "WorldEntities/Doodads/Precursor/PrecursorKey_White.prefab", new CraftableAndPlaceableItemInfos(true, true, TechType.PrecursorKey_White, new List<Ingredient>() { new Ingredient(ConfigSwitcher.PrecursorKeysResource, ConfigSwitcher.PrecursorKeysResourceAmount) },  null, -0.2f, 0.3f, new Vector3(0f, 0.05f, 0.04f), new Vector3(0f, 0f, 0f), 0.8f) }
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
            Nautilus.Handlers.CraftDataHandler.SetEquipmentType(techType, EquipmentType.Hand);

            // Set as selectable item.
            Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(techType, QuickSlotType.Selectable);
        }

        private static void MakeSnacksPlaceable()
        {
            GameObject snack1 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Food/Snack1.prefab");
            if (snack1 != null)
            {
                BoxCollider snack1Collider = snack1.AddComponent<BoxCollider>();
                snack1Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                snack1.AddComponent<CustomPlaceToolController>();
                snack1.AddComponent<Snack1_PT>();
                MakeItemPlaceable(TechType.Snack1, snack1, snack1Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Warning("WARNING: Could not load type [{0}]", "WorldEntities/Food/Snack1");
#endif
            GameObject snack2 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Food/Snack2.prefab");
            if (snack2 != null)
            {
                BoxCollider snack2Collider = snack2.AddComponent<BoxCollider>();
                snack2Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                snack2.AddComponent<CustomPlaceToolController>();
                snack2.AddComponent<Snack2_PT>();
                MakeItemPlaceable(TechType.Snack2, snack2, snack2Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Warning("WARNING: Could not load type [{0}]", "WorldEntities/Food/Snack2");
#endif
            GameObject snack3 = PrefabsHelper.LoadGameObjectFromFilename("WorldEntities/Food/Snack3.prefab");
            if (snack3 != null)
            {
                BoxCollider snack3Collider = snack3.AddComponent<BoxCollider>();
                snack3Collider.size = new Vector3(0.17f, 0.18f, 0.8f);
                snack3.AddComponent<CustomPlaceToolController>();
                snack3.AddComponent<Snack3_PT>();
                MakeItemPlaceable(TechType.Snack3, snack3, snack3Collider);
            }
#if DEBUG_ITEMS_REGISTRATION
            else
                Logger.Warning("WARNING: Could not load type [{0}]", "WorldEntities/Food/Snack3");
#endif

            // Swap Snack2 and Snack3 (as tooltips do not match models)
#if DEBUG_ITEMS_REGISTRATION
            Logger.Debug("DEBUG: Swapping Snack2 and Snack3 (as tooltips do not match models).");
#endif
            string lang = Language.main.GetCurrentLanguage();
            string snack2Name = Language.main.Get("Snack2");
            string snack2Tooltip = Language.main.Get("Tooltip_Snack2");
            var snack2Icon = SpriteManager.Get(TechType.Snack2);
            string snack3Name = Language.main.Get("Snack3");
            string snack3Tooltip = Language.main.Get("Tooltip_Snack3");
            var snack3Icon = SpriteManager.Get(TechType.Snack3);

            Nautilus.Handlers.LanguageHandler.SetLanguageLine("Snack2", snack3Name, lang);
            Nautilus.Handlers.LanguageHandler.SetLanguageLine("Tooltip_Snack2", snack3Tooltip, lang);
            Nautilus.Handlers.LanguageHandler.SetLanguageLine("Snack3", snack2Name, lang);
            Nautilus.Handlers.LanguageHandler.SetLanguageLine("Tooltip_Snack3", snack2Tooltip, lang);
            Nautilus.Handlers.SpriteHandler.RegisterSprite(TechType.Snack2, snack3Icon);
            Nautilus.Handlers.SpriteHandler.RegisterSprite(TechType.Snack3, snack2Icon);
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
                Logger.Warning("WARNING: Could not load type [{0}]", batteryPath);
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
                Logger.Warning("WARNING: Could not load type [{0}]", materialPath);
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
                Logger.Warning("WARNING: Could not load type [{0}]", itemPath);
#endif
        }

        private static void MakeEggCraftableAndPlaceable(DecoEggInfos eggInfo, string eggPath)
        {
            if (eggInfo == null || string.IsNullOrEmpty(eggPath))
                return;
            GameObject egg = PrefabsHelper.LoadGameObjectFromFilename(eggPath);
            if (egg != null)
            {
                if (ConfigSwitcher.EnableRegularEggs)
                {
                    Logger.Info($"INFO: Making {eggInfo.DiscoveredTechType.AsString()} craftable...");
                    // Associate recipe to the new TechType
                    Nautilus.Crafting.RecipeData recipe = new Nautilus.Crafting.RecipeData(new List<Ingredient>()
                    { new Ingredient(ConfigSwitcher.CreatureEggsResource, ConfigSwitcher.CreatureEggsResourceAmount) })
                    { craftAmount = 1 };
                    Nautilus.Handlers.CraftDataHandler.SetRecipeData(eggInfo.DiscoveredTechType, recipe);
                    // Set unlock conditions
                    if (ConfigSwitcher.EnableEggsAtStart || ConfigSwitcher.EnableEggsWhenCreatureScanned)
                        Nautilus.Handlers.KnownTechHandler.UnlockOnStart(eggInfo.DiscoveredTechType);
                    // Add fabricating animation
                    var fabricating = egg.GetComponent<VFXFabricating>();
                    if (fabricating == null)
                        fabricating = egg.AddComponent<VFXFabricating>();
                    fabricating.localMinY = -0.2f;
                    fabricating.localMaxY = 0.8f;
                    fabricating.posOffset = new Vector3(0f, 0.05f, 0.04f);
                    fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
                    fabricating.scaleFactor = eggInfo.ScaleFactor;
                }
                if (ConfigSwitcher.EnablePlaceItems && ConfigSwitcher.EnablePlaceEggs)
                {
                    Logger.Info($"INFO: Making {eggInfo.DiscoveredTechType.AsString()} placeable...");
                    // We can place this egg
                    PrefabsHelper.SetDefaultPlaceTool(egg);
                    // Add undiscovered egg to the hand-equipments and set as selectable
                    Nautilus.Handlers.CraftDataHandler.SetEquipmentType(eggInfo.UndiscoveredTechType, EquipmentType.Hand);
                    Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(eggInfo.UndiscoveredTechType, QuickSlotType.Selectable);
                    // Add egg to the hand-equipments and set as selectable
                    Nautilus.Handlers.CraftDataHandler.SetEquipmentType(eggInfo.DiscoveredTechType, EquipmentType.Hand);
                    Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(eggInfo.DiscoveredTechType, QuickSlotType.Selectable);
                }
            }
            else
                Logger.Warning($"WARNING: Could not load game object for {eggInfo.DiscoveredTechType.ToString()}.");
        }

        private static bool _madeItemsPlaceable = false;
        public static void MakeItemsPlaceable()
        {
            if (!_madeItemsPlaceable)
            {
                Logger.Info("INFO: Making items placeable/pickupable...");

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

        private static void MakeItemsCraftableAndPlaceable(CraftableAndPlaceableItemInfos itemInfos, string itemPath)
        {
            if (itemInfos == null || string.IsNullOrEmpty(itemPath))
                return;
            GameObject item = PrefabsHelper.LoadGameObjectFromFilename(itemPath);
            if (item != null)
            {
                if (itemInfos.MakePlaceable)
                {
                    // Add the new TechType to the hand-equipments
                    Nautilus.Handlers.CraftDataHandler.SetEquipmentType(itemInfos.ItemTechType, EquipmentType.Hand);
                    // Set quick slot type.
                    Nautilus.Handlers.CraftDataHandler.SetQuickSlotType(itemInfos.ItemTechType, QuickSlotType.Selectable);
                }

                if (itemInfos.MakeCratable)
                {
                    if (itemInfos.Recipe != null)
                    {
                        // Update item recipe
                        Nautilus.Crafting.RecipeData recipe = new Nautilus.Crafting.RecipeData(itemInfos.Recipe) { craftAmount = 1 };
                        Nautilus.Handlers.CraftDataHandler.SetRecipeData(itemInfos.ItemTechType, recipe);
                    }

                    // Update fabricating animation
                    VFXFabricating fabricating = null;
                    if (itemInfos.VFXChild != null)
                    {
                        GameObject child = item.FindChild(itemInfos.VFXChild);
                        if (child != null)
                        {
                            fabricating = child.GetComponent<VFXFabricating>();
                            if (fabricating == null)
                                fabricating = child.AddComponent<VFXFabricating>();
                        }
                    }
                    if (fabricating == null)
                    {
                        fabricating = item.GetComponent<VFXFabricating>();
                        if (fabricating == null)
                            fabricating = item.AddComponent<VFXFabricating>();
                    }
                    if (fabricating != null)
                    {
                        fabricating.localMinY = itemInfos.LocalMinY;
                        fabricating.localMaxY = itemInfos.LocalMaxY;
                        fabricating.posOffset = itemInfos.PosOffset;
                        fabricating.eulerOffset = itemInfos.EulerOffset;
                        fabricating.scaleFactor = itemInfos.ScaleFactor;
                        fabricating.enabled = true;
                    }
                }

                if (itemInfos.ItemTechType == TechType.PrecursorKey_Red || itemInfos.ItemTechType == TechType.PrecursorKey_White)
                {
                    // We can pick this item
                    var pickupable = item.GetComponent<Pickupable>();
                    if (pickupable == null)
                        pickupable = item.GetComponentInChildren<Pickupable>();
                    if (pickupable != null)
                    {
                        pickupable.isPickupable = true;
                        pickupable.randomizeRotationWhenDropped = true;
                    }

                    // Set unlock conditions
                    if (itemInfos.MakeCratable)
                    {
                        if (ConfigSwitcher.AddItemsWhenDiscovered)
                            Nautilus.Handlers.KnownTechHandler.SetAnalysisTechEntry(TechType.PrecursorKey_Purple, new TechType[] { itemInfos.ItemTechType });
                        else
                            Nautilus.Handlers.KnownTechHandler.UnlockOnStart(itemInfos.ItemTechType);
                    }

                    // Make placeable
                    if (itemInfos.MakePlaceable)
                    {
                        // Retrieve collider
                        Collider collider = item.GetComponent<BoxCollider>();
                        if (collider != null)
                            collider.isTrigger = true;
                        // Update PlaceTool
                        var cpt = item.GetComponent<CustomPlaceToolController>();
                        if (cpt == null)
                            cpt = item.AddComponent<CustomPlaceToolController>();
                        if (itemInfos.ItemTechType == TechType.PrecursorKey_Red)
                        {
                            RedKey_PT placeTool = item.GetComponent<RedKey_PT>();
                            if (placeTool == null)
                                placeTool = item.AddComponent<RedKey_PT>();
                            placeTool.allowedInBase = true;
                            placeTool.allowedOnBase = true;
                            placeTool.allowedOnCeiling = false;
                            placeTool.allowedOnConstructable = true;
                            placeTool.allowedOnGround = true;
                            placeTool.allowedOnRigidBody = true;
                            placeTool.allowedOnWalls = false;
                            placeTool.allowedOutside = true;
                            placeTool.rotationEnabled = true;
                            placeTool.enabled = true;
                            placeTool.hasAnimations = false;
                            placeTool.hasBashAnimation = false;
                            placeTool.hasFirstUseAnimation = false;
                            placeTool.mainCollider = collider;
                            placeTool.pickupable = pickupable;
                        }
                        else if (itemInfos.ItemTechType == TechType.PrecursorKey_White)
                        {
                            WhiteKey_PT placeTool = item.GetComponent<WhiteKey_PT>();
                            if (placeTool == null)
                                placeTool = item.AddComponent<WhiteKey_PT>();
                            placeTool.allowedInBase = true;
                            placeTool.allowedOnBase = true;
                            placeTool.allowedOnCeiling = false;
                            placeTool.allowedOnConstructable = true;
                            placeTool.allowedOnGround = true;
                            placeTool.allowedOnRigidBody = true;
                            placeTool.allowedOnWalls = false;
                            placeTool.allowedOutside = true;
                            placeTool.rotationEnabled = true;
                            placeTool.enabled = true;
                            placeTool.hasAnimations = false;
                            placeTool.hasBashAnimation = false;
                            placeTool.hasFirstUseAnimation = false;
                            placeTool.mainCollider = collider;
                            placeTool.pickupable = pickupable;
                        }
                    }

                    // Update sky applier
                    PrefabsHelper.UpdateOrAddSkyApplier(item);
                }
                else if (itemInfos.ItemTechType == TechType.PrecursorKey_Blue || itemInfos.ItemTechType == TechType.PrecursorKey_Orange || itemInfos.ItemTechType == TechType.PrecursorKey_Purple)
                {
                    // Make placeable
                    if (itemInfos.MakePlaceable)
                    {
                        // We can pick this item
                        var pickupable = item.GetComponent<Pickupable>();
                        if (pickupable == null)
                            pickupable = item.GetComponentInChildren<Pickupable>();
                        if (pickupable != null)
                        {
                            pickupable.isPickupable = true;
                            pickupable.randomizeRotationWhenDropped = true;
                        }
                        // Retrieve collider
                        Collider collider = item.GetComponent<BoxCollider>();
                        if (collider != null)
                            collider.isTrigger = true;
                        // Update PlaceTool
                        var cpt = item.GetComponent<CustomPlaceToolController>();
                        if (cpt == null)
                            cpt = item.AddComponent<CustomPlaceToolController>();
                        if (itemInfos.ItemTechType == TechType.PrecursorKey_Blue)
                        {
                            BlueKey_PT placeTool = item.GetComponent<BlueKey_PT>();
                            if (placeTool == null)
                                placeTool = item.AddComponent<BlueKey_PT>();
                            placeTool.allowedInBase = true;
                            placeTool.allowedOnBase = true;
                            placeTool.allowedOnCeiling = false;
                            placeTool.allowedOnConstructable = true;
                            placeTool.allowedOnGround = true;
                            placeTool.allowedOnRigidBody = true;
                            placeTool.allowedOnWalls = false;
                            placeTool.allowedOutside = true;
                            placeTool.rotationEnabled = true;
                            placeTool.enabled = true;
                            placeTool.hasAnimations = false;
                            placeTool.hasBashAnimation = false;
                            placeTool.hasFirstUseAnimation = false;
                            placeTool.mainCollider = collider;
                            placeTool.pickupable = pickupable;
                        }
                        else if (itemInfos.ItemTechType == TechType.PrecursorKey_Orange)
                        {
                            OrangeKey_PT placeTool = item.GetComponent<OrangeKey_PT>();
                            if (placeTool == null)
                                placeTool = item.AddComponent<OrangeKey_PT>();
                            placeTool.allowedInBase = true;
                            placeTool.allowedOnBase = true;
                            placeTool.allowedOnCeiling = false;
                            placeTool.allowedOnConstructable = true;
                            placeTool.allowedOnGround = true;
                            placeTool.allowedOnRigidBody = true;
                            placeTool.allowedOnWalls = false;
                            placeTool.allowedOutside = true;
                            placeTool.rotationEnabled = true;
                            placeTool.enabled = true;
                            placeTool.hasAnimations = false;
                            placeTool.hasBashAnimation = false;
                            placeTool.hasFirstUseAnimation = false;
                            placeTool.mainCollider = collider;
                            placeTool.pickupable = pickupable;
                        }
                        else if (itemInfos.ItemTechType == TechType.PrecursorKey_Purple)
                        {
                            PurpleKey_PT placeTool = item.GetComponent<PurpleKey_PT>();
                            if (placeTool == null)
                                placeTool = item.AddComponent<PurpleKey_PT>();
                            placeTool.allowedInBase = true;
                            placeTool.allowedOnBase = true;
                            placeTool.allowedOnCeiling = false;
                            placeTool.allowedOnConstructable = true;
                            placeTool.allowedOnGround = true;
                            placeTool.allowedOnRigidBody = true;
                            placeTool.allowedOnWalls = false;
                            placeTool.allowedOutside = true;
                            placeTool.rotationEnabled = true;
                            placeTool.enabled = true;
                            placeTool.hasAnimations = false;
                            placeTool.hasBashAnimation = false;
                            placeTool.hasFirstUseAnimation = false;
                            placeTool.mainCollider = collider;
                            placeTool.pickupable = pickupable;
                        }
                    }

                    // Update sky applier
                    PrefabsHelper.UpdateOrAddSkyApplier(item);
                }
                else
                {
                    // Unlock at start if it's not a precursor tablet
                    if (itemInfos.MakeCratable && itemInfos.ItemTechType != TechType.NutrientBlock)
                        Nautilus.Handlers.KnownTechHandler.UnlockOnStart(itemInfos.ItemTechType);

                    // Update PlaceTool
                    PrefabsHelper.SetDefaultPlaceTool(item, null, null, false, false, true);
                    PlaceTool placeTool = item.GetComponent<GenericPlaceTool>();
                    if (placeTool == null)
                        placeTool = item.GetComponent<PlaceTool>();
                    if (placeTool == null)
                        placeTool = item.GetComponentInChildren<GenericPlaceTool>();
                    if (placeTool == null)
                        placeTool = item.GetComponentInChildren<PlaceTool>();
                    if (placeTool != null)
                    {
                        if (itemInfos.ItemTechType == TechType.Poster
                            || itemInfos.ItemTechType == TechType.PosterAurora
                            || itemInfos.ItemTechType == TechType.PosterExoSuit1
                            || itemInfos.ItemTechType == TechType.PosterExoSuit2
                            || itemInfos.ItemTechType == TechType.PosterKitty
#if BELOWZERO
                            || itemInfos.ItemTechType == TechType.PosterSpyPenguin
#endif
                        )
                        {
                            placeTool.allowedOnGround = false;
                            placeTool.allowedOnWalls = true;
                            placeTool.hasAnimations = false;
                            placeTool.hasBashAnimation = false;
                            placeTool.hasFirstUseAnimation = false;
                        }
                        else
                        {
                            placeTool.allowedOnGround = true;
                            placeTool.allowedOnWalls = false;
                        }
                    }
                }
            }
        }

        private static bool _madeItemsCraftable = false;
        public static void MakeItemsCraftableAndPlaceable()
        {
            if (!_madeItemsCraftable)
            {
                // Eggs
                foreach (KeyValuePair<string, DecoEggInfos> egg in _eggs)
                    MakeEggCraftableAndPlaceable(egg.Value, egg.Key);

                // Other items
                Logger.Info($"INFO: Making other items cratable and/or placeable...");
                foreach (KeyValuePair<string, CraftableAndPlaceableItemInfos> item in _craftableAndPlaceableItems)
                    MakeItemsCraftableAndPlaceable(item.Value, item.Key);

                _madeItemsCraftable = true;
            }
        }
    }
}
