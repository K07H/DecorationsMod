using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace DecorationsMod
{
    public static class ConfigSwitcher
    {
        // THESE ARE DEFAULT SETTING.
        // They are automatically overwritten when getting configuration from Config.txt file.
        // They will be used only if they are missing from the Config.txt file, if they are incorrectly formatted in Config.txt file or if Config.txt file is missing.
        #region Settings

        // Dummy value to display info in game menu options.
        public static bool OpenDecorationsModConfigurator = false;
        public static bool OpenConfiguratorLastState = false;

        // If "true" player will be able to build most of the buildable-items provided by this mod outside bases.
        public static bool AllowBuildOutside = true;

        // If "true" player will be able to place most of the placeable-items provided by this mod outside bases.
        public static bool AllowPlaceOutside = true;

        // If "true" player will be able to build indoor long planter outside (thus allowing to plant land seeds in water).
        public static bool AllowIndoorLongPlanterOutside = true;

        // If "true" player will be able to build outdoor long planter inside (thus allowing to plant sea seeds inside base/cyclops).
        public static bool AllowOutdoorLongPlanterInside = true;

        // If "true" player will be able to place following items:
        // coffee cups, polyaniline, hydrochloric acid, benzene, hatching enzymes, eggs, snacks, lubricant, bleach, 
        // water bottles, wiring kits, computer chip, ion crystal, precursor tablets, stalker tooth, first aid kit
        public static bool EnablePlaceItems = true;

        // If "true" player will be able to place batteries, powercells, ion batteries and ion powercells.
        // Attention: If this option is enabled, you'll have to manually drag-n-drop batteries/power cells to the charger (left click won't work to equip).
        public static bool EnablePlaceBatteries = false;

        // If "true", player will be able to build/craft the following new items:      
        // lab cart, alien pillar, indoor long planter, outdoor long planter specimen analyzer, markiplier doll 1, markiplier doll 2
        // jacksepticeye doll, eatmydiction doll, empty desk, eatmydiction doll, seamoth doll, exosuit doll, forklift, cargo crates
        // sofas, benches, stool, customizable picture frame, customizable light, warper specimen and additional lockers.
        public static bool EnableNewItems = true;

        // If "true", new items will be added to fabricators/builder tool only if some similar items have been discovered in game.
        // Works for following items: sofas, benches, stool, long planters, customizable picture frame, customizable light, alien pillar, warper parts and warper specimen.
        public static bool AddItemsWhenDiscovered = false;

        // If "true", player will be able to build the seeds fabricator (and then use it to craft both existing and new seeds).
        public static bool EnableNewFlora = true;

        // If "true", player will be able to build/craft sofas.
        public static bool EnableSofas = true;

        // If "true", player will be able to craft Nutrient Blocks from the decorations fabricator.
        public static bool EnableNutrientBlock = true;

        // If "true", additionnal categories will be added to the fabricators to make all items fit in screen.
        public static bool UseFlatScreenResolution = false;

        // If "true", tooltips of the custom lamp and custom picture frame will be more compact.
        public static bool UseCompactTooltips = false;

        // If "true", player will not switch to next quickslot tool when using mouse wheel up/down if he is currently holding a placeable item.
        public static bool LockQuickslotsWhenPlacingItem = true;

        // If "true", regular aquarium will adapt to the lighting depending if it's indoor or outdoor.
        public static bool FixAquariumLighting = false;

        // If "true", glass of the aquariums (regular and the one from this mod) will be a little glowing.
        public static bool GlowingAquariumGlass = false;

        // If "true", there will be a new tab inside decorations fabricator containing following items:
        // 11 unique warper parts, 11 unique alien relics and 5 unique alien tablets
        // Hidden by default to prevent end game spoilers.
        public static bool EnablePrecursorTab = false;

        // If true: Item will be available as a buildable (in habitat builder menu).
        // If false: Item will be available as a craftable (in decorations fabricator).
        public static bool SpecimenAnalyzer_asBuildable = true;
        public static bool MarkiDoll1_asBuildable = true;
        public static bool MarkiDoll2_asBuildable = true;
        public static bool JackSepticEye_asBuildable = true;
        public static bool EatMyDiction_asBuidable = true;
        public static bool Forklift_asBuidable = true;
        public static bool SofaStr1_asBuidable = true;
        public static bool SofaStr2_asBuidable = true;
        public static bool SofaStr3_asBuidable = true;
        public static bool SofaCorner2_asBuidable = true;
        public static bool LabCart_asBuildable = true;
        public static bool EmptyDesk_asBuildable = true;

        // Setup all precursor keys in decorations fabricator (true), or only red/white (false).
        public static bool PrecursorKeysAll = true;

        // Alien keys recipies
        public static TechType PrecursorKeysResource = TechType.PrecursorIonCrystal;
        public static int PrecursorKeysResourceAmount = 1;

        // Alien relics recipies
        public static TechType RelicRecipiesResource = TechType.PrecursorIonCrystal;
        public static int RelicRecipiesResourceAmount = 1;

        // Creature eggs recipies
        public static TechType CreatureEggsResource = TechType.Salt;
        public static int CreatureEggsResourceAmount = 5;

        // New flora recipies
        public static TechType FloraRecipiesResource = TechType.Salt;
        public static int FloraRecipiesResourceAmount = 5;

        // New flora default configuration
        public static CustomFlora config_LandTree1 = new CustomFlora(2400.0f, 200.0f, true, 3.0f, 6.0f, false, 500.0f, 0.02f, false, 300.0f, true);
        public static CustomFlora config_JungleTree1 = new CustomFlora(2000.0f, 120.0f, true, 0.0f, 0.0f, false, 300.0f);
        public static CustomFlora config_JungleTree2 = new CustomFlora(2000.0f, 120.0f, true, 0.0f, 0.0f, false, 300.0f);
        public static CustomFlora config_TropicalTreeA = new CustomFlora(1400.0f, 60.0f, true, 0.0f, 0.0f, false, 200.0f); // TropicalPlant3a
        public static CustomFlora config_TropicalTreeB = new CustomFlora(1400.0f, 60.0f, true, 0.0f, 0.0f, false, 200.0f); // TropicalPlant3b
        public static CustomFlora config_TropicalTreeC = new CustomFlora(1400.0f, 60.0f, true, 0.0f, 0.0f, false, 200.0f); // TropicalPlant6a
        public static CustomFlora config_TropicalTreeD = new CustomFlora(1400.0f, 60.0f, true, 0.0f, 0.0f, false, 200.0f); // TropicalPlant6b
        public static CustomFlora config_LandPlant1 = new CustomFlora(1200.0f, 80.0f, true, 0.0f, 0.0f, false, 100.0f);
        public static CustomFlora config_LandPlant2 = new CustomFlora(1200.0f, 80.0f, true, 0.0f, 0.0f, false, 100.0f);
        public static CustomFlora config_LandPlant3 = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_LandPlant4 = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_LandPlant5 = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_Fern2 = new CustomFlora(800.0f, 60.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_Fern4 = new CustomFlora(800.0f, 60.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_TropicalPlantA = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 140.0f); // TropicalPlant1a
        public static CustomFlora config_TropicalPlantB = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 140.0f); // TropicalPlant1b
        public static CustomFlora config_TropicalPlantC = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 100.0f); // TropicalPlant2a
        public static CustomFlora config_TropicalPlantD = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 100.0f); // TropicalPlant2b
        public static CustomFlora config_TropicalPlantE = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 100.0f); // TropicalPlant7a
        public static CustomFlora config_TropicalPlantF = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 100.0f); // TropicalPlant7b
        public static CustomFlora config_TropicalPlantG = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 100.0f); // TropicalPlant10a
        public static CustomFlora config_TropicalPlantH = new CustomFlora(1200.0f, 60.0f, true, 0.0f, 0.0f, false, 100.0f); // TropicalPlant10b
        public static CustomFlora config_CrabClawKelp2 = new CustomFlora(1600.0f, 100.0f, true, 0.0f, 0.0f, false, 220.0f);
        public static CustomFlora config_CrabClawKelp1 = new CustomFlora(1600.0f, 100.0f, true, 0.0f, 0.0f, false, 220.0f);
        public static CustomFlora config_CrabClawKelp3 = new CustomFlora(1600.0f, 100.0f, true, 0.0f, 0.0f, false, 220.0f);
        public static CustomFlora config_PyroCoral1 = new CustomFlora(2000.0f, 130.0f, true, 0.0f, 0.0f, false, 300.0f);
        public static CustomFlora config_PyroCoral2 = new CustomFlora(2000.0f, 130.0f, true, 0.0f, 0.0f, false, 300.0f);
        public static CustomFlora config_PyroCoral3 = new CustomFlora(2000.0f, 130.0f, true, 0.0f, 0.0f, false, 300.0f);
        public static CustomFlora config_CoveTree1 = new CustomFlora(3000.0f, 300.0f, true, 0.0f, 0.0f, false, 400.0f);
        public static CustomFlora config_GreenReeds1 = new CustomFlora(1000.0f, 60.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_GreenReeds6 = new CustomFlora(1000.0f, 60.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_LostRiverPlant2 = new CustomFlora(1400.0f, 100.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_LostRiverPlant4 = new CustomFlora(1400.0f, 100.0f, true, 0.0f, 0.0f, false, 200.0f);
        public static CustomFlora config_PlantMiddle11 = new CustomFlora(1000.0f, 60.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_SmallDeco3 = new CustomFlora(700.0f, 10.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_FloatingStone1 = new CustomFlora(2000.0f, 130.0f, true, 0.0f, 0.0f, false, 160.0f);
        public static CustomFlora config_BrownCoralTubes1 = new CustomFlora(1400.0f, 10.0f, true, 0.0f, 0.0f, false, 50.0f);
        public static CustomFlora config_BrownCoralTubes2 = new CustomFlora(1600.0f, 10.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_BrownCoralTubes3 = new CustomFlora(1800.0f, 60.0f, true, 0.0f, 0.0f, false, 90.0f);
        public static CustomFlora config_BlueCoralTubes1 = new CustomFlora(1600.0f, 10.0f, true, 0.0f, 0.0f, false, 140.0f);
        public static CustomFlora config_SmallDeco10 = new CustomFlora(1800.0f, 10.0f, true, 0.0f, 0.0f, false, 160.0f);
        public static CustomFlora config_SmallDeco11 = new CustomFlora(1600.0f, 10.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_SmallDeco13 = new CustomFlora(1600.0f, 10.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_SmallDeco14 = new CustomFlora(1600.0f, 10.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_SmallDeco15Red = new CustomFlora(1600.0f, 10.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_SmallDeco17Purple = new CustomFlora(1600.0f, 10.0f, true, 0.0f, 0.0f, false, 120.0f);
        public static CustomFlora config_RedGrass1 = new CustomFlora(800.0f, 30.0f, true, 0.0f, 0.0f, false, 30.0f);
        public static CustomFlora config_RedGrass2 = new CustomFlora(800.0f, 40.0f, true, 0.0f, 0.0f, false, 50.0f);
        public static CustomFlora config_RedGrass3 = new CustomFlora(800.0f, 40.0f, true, 0.0f, 0.0f, false, 50.0f);
        public static CustomFlora config_RedGrass2Tall = new CustomFlora(1000.0f, 40.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_RedGrass3Tall = new CustomFlora(1000.0f, 40.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_BloodGrassRed = new CustomFlora(1000.0f, 40.0f, true, 0.0f, 0.0f, false, 70.0f);
        public static CustomFlora config_BloodGrassDense = new CustomFlora(1600.0f, 60.0f, true, 0.0f, 0.0f, false, 90.0f);
        public static CustomFlora config_MushroomTree1 = new CustomFlora(3000.0f, 300.0f, true, 0.0f, 0.0f, false, 140.0f);
        public static CustomFlora config_MushroomTree2 = new CustomFlora(1500.0f, 100.0f, true, 0.0f, 0.0f, false, 80.0f);

        // Add existing air seeds to the seeds fabricator
        public static bool EnableRegularAirSeeds = true;

        // Add existing water seeds to the seeds fabricator
        public static bool EnableRegularWaterSeeds = true;

        // Add existing air seeds to the seeds fabricator only if plant was discovered (picked up plant or seed once).
        public static bool AddAirSeedsWhenDiscovered = false;

        // Add existing water seeds to the seeds fabricator only if plant was discovered (picked up plant or seed once).
        public static bool AddWaterSeedsWhenDiscovered = false;

        // Defines water & food values for bar drinks & meals.
        public static float BarBottle1Value = 20.0f;
        public static float BarBottle2Value = 20.0f;
        public static float BarBottle3Value = 40.0f;
        public static float BarBottle4Value = 40.0f;
        public static float BarBottle5Value = 40.0f;
        public static float BarFood1FoodValue = 40.0f;
        public static float BarFood1WaterValue = 10.0f;
        public static float BarFood2FoodValue = 55.0f;
        public static float BarFood2WaterValue = 25.0f;

        // Defines the type and amount of resources dropped by Purple Pinecone flora when it gets harvested.
        public static TechType PurplePineconeDroppedResource = TechType.Salt;
        public static int PurplePineconeDroppedResourceAmount = 1;

        // Add existing game eggs to the decorations fabricator (eggs will be added to fabricator once hatched in Alien Containment unit)
        public static bool EnableRegularEggs = true;
        // Add eggs to the decorations fabricator once they gets hatched in Alien Containment unit or once their associated creature gets scanned
        public static bool EnableEggsWhenCreatureScanned = false;
        // Add eggs to the decorations fabricator from start
        public static bool EnableEggsAtStart = false;

        // NOTE: Sea dragon, sea emperor and ghost leviathan eggs will be added to the decorations fabricator once they are scanned in game.
        // If "EnableEggsWhenCreatureScanned" is set to true, sea dragon, sea emperor and ghost leviathan eggs will be added to the decorations fabricator once they are scanned in game or when their associated creature is scanned in game.
        // If "EnableEggsAtStart" is set to true, all eggs will be available from start in the decorations fabricator.

        // Ghost leviathans (spawned from Cove Trees) default config
        public static bool GhostLeviatan_enable = true;
        public static int GhostLeviatan_maxSpawns = 2;
        public static float GhostLeviatan_timeBeforeFirstSpawn = 3600f;
        public static float GhostLeviatan_spawnTimeRatio = 1f;
        public static float GhostLeviatan_health = 100f;

        // Precursor artefact animations
        public static bool AlienRelic1Animation = true;
        public static bool AlienRelic2Animation = true;
        public static bool AlienRelic3Animation = true;
        public static bool AlienRelic4Animation = true;
        public static bool AlienRelic5Animation = true;
        public static bool AlienRelic6Animation = true;
        public static bool AlienRelic7Animation = true;
        public static bool AlienRelic8Animation = true;
        public static bool AlienRelic9Animation = true;
        public static bool AlienRelic10Animation = true;
        public static bool AlienRelic11Animation = true;

        #endregion
        
        // Utility function to parse flora configuration element.
        private static bool GetFloraConfig(CustomFlora customFlora, string configStr)
        {
            bool success = true;
            string[] elems = configStr.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length >= 3) // Old Decorations mod versions (backward compatibility)
            {
                if (!float.TryParse(elems[0], NumberStyles.Float, CultureInfo.InvariantCulture, out customFlora.GrowthDuration))
                    success = false;
                if (!float.TryParse(elems[1], NumberStyles.Float, CultureInfo.InvariantCulture, out customFlora.Health))
                    success = false;
                if (!float.TryParse(elems[2], NumberStyles.Float, CultureInfo.InvariantCulture, out customFlora.Charge))
                    success = false;
                if (elems.Length >= 8)
                {
                    if (elems[3].IndexOf("true", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        customFlora.Eatable = true;
                    else if (elems[3].IndexOf("false", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        customFlora.Eatable = false;
                    else
                        success = false;
                    if (!float.TryParse(elems[4], NumberStyles.Float, CultureInfo.InvariantCulture, out customFlora.FoodValue))
                        success = false;
                    if (!float.TryParse(elems[5], NumberStyles.Float, CultureInfo.InvariantCulture, out customFlora.WaterValue))
                        success = false;
                    if (elems[6].IndexOf("true", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        customFlora.Decomposes = true;
                    else if (elems[6].IndexOf("false", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        customFlora.Decomposes = false;
                    else
                        success = false;
                    if (float.TryParse(elems[7], NumberStyles.Float, CultureInfo.InvariantCulture, out customFlora.KDecayRate))
                        customFlora.KDecayRate *= 0.001f;
                    else
                        success = false;
                }
            }
            return success;
        }

        public static string configFilePath = null;
        /// <summary>Loads configuration from Config.txt file.</summary>
        public static void LoadConfiguration()
        {
            // Get config file path
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string currentDir = Path.GetDirectoryName(path);
            configFilePath = currentDir + "/Config.txt";

            Logger.Log("INFO: Loading configuration from \"" + configFilePath + "\"...");

            // Retrieve config
            if (File.Exists(configFilePath))
            {
                string configFile = File.ReadAllText(configFilePath, Encoding.UTF8);
                string[] configLines = configFile.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string configStr in configLines)
                {
                    if (!configStr.StartsWith("#", false, CultureInfo.InvariantCulture) && configStr.Contains("="))
                    {
                        string[] configElem = configStr.Split("=".ToCharArray(), StringSplitOptions.None);
                        if (configElem != null && configElem.Length == 2)
                        {
                            string configKey = configElem[0].Trim();
                            string configValueStr = configElem[1].Trim();
                            bool configValue = (string.Compare(configValueStr, "true", true, CultureInfo.InvariantCulture) == 0);

                            TechType tmpresource = TechType.None;
                            int tmpVal = -1;
                            float fTmpVal = -1.0f;

                            switch (configKey)
                            {
                                case "allowBuildOutside":
                                    ConfigSwitcher.AllowBuildOutside = configValue; break;
                                case "allowPlaceOutside":
                                    ConfigSwitcher.AllowPlaceOutside = configValue; break;
                                case "enablePlaceItems":
                                    ConfigSwitcher.EnablePlaceItems = configValue; break;
                                case "enablePlaceBatteries":
                                    ConfigSwitcher.EnablePlaceBatteries = configValue; break;
                                case "enableNewFlora":
                                    ConfigSwitcher.EnableNewFlora = configValue; break;
                                case "enableNewItems":
                                    ConfigSwitcher.EnableNewItems = configValue; break;
                                case "addItemsWhenDiscovered":
                                    ConfigSwitcher.AddItemsWhenDiscovered = configValue; break;
                                case "enableSofas":
                                    ConfigSwitcher.EnableSofas = configValue; break;
                                case "allowIndoorLongPlanterOutside":
                                    ConfigSwitcher.AllowIndoorLongPlanterOutside = configValue; break;
                                case "allowOutdoorLongPlanterInside":
                                    ConfigSwitcher.AllowOutdoorLongPlanterInside = configValue; break;
                                case "enablePrecursorTab":
                                    ConfigSwitcher.EnablePrecursorTab = configValue; break;
                                case "enableNutrientBlock":
                                    ConfigSwitcher.EnableNutrientBlock = configValue; break;
                                case "enableAllEggs":
                                    ConfigSwitcher.EnableRegularEggs = configValue; break;
                                case "addEggsWhenCreatureScanned":
                                    ConfigSwitcher.EnableEggsWhenCreatureScanned = configValue; break;
                                case "addEggsAtStart":
                                    ConfigSwitcher.EnableEggsAtStart = configValue; break;
                                case "useAlternativeScreenResolution":
                                    ConfigSwitcher.UseFlatScreenResolution = configValue; break;
                                case "useCompactTooltips":
                                    ConfigSwitcher.UseCompactTooltips = configValue; break;
                                case "lockQuickslotsWhenPlacingItem":
                                    ConfigSwitcher.LockQuickslotsWhenPlacingItem = configValue; break;
                                case "fixAquariumLighting":
                                    ConfigSwitcher.FixAquariumLighting = configValue; break;
                                case "enableAquariumGlassGlowing":
                                    ConfigSwitcher.GlowingAquariumGlass = configValue; break;
                                case "asBuildable_SpecimenAnalyzer":
                                    ConfigSwitcher.SpecimenAnalyzer_asBuildable = configValue; break;
                                case "asBuildable_MarkiplierDoll1":
                                    ConfigSwitcher.MarkiDoll1_asBuildable = configValue; break;
                                case "asBuildable_MarkiplierDoll2":
                                    ConfigSwitcher.MarkiDoll2_asBuildable = configValue; break;
                                case "asBuildable_JackSepticEyeDoll":
                                    ConfigSwitcher.JackSepticEye_asBuildable = configValue; break;
                                case "asBuildable_EatMyDictionDoll":
                                    ConfigSwitcher.EatMyDiction_asBuidable = configValue; break;
                                case "asBuildable_ForkliftToy":
                                    ConfigSwitcher.Forklift_asBuidable = configValue; break;
                                case "asBuildable_SofaSmall":
                                    ConfigSwitcher.SofaStr1_asBuidable = configValue; break;
                                case "asBuildable_SofaMedium":
                                    ConfigSwitcher.SofaStr2_asBuidable = configValue; break;
                                case "asBuildable_SofaBig":
                                    ConfigSwitcher.SofaStr3_asBuidable = configValue; break;
                                case "asBuildable_SofaCorner":
                                    ConfigSwitcher.SofaCorner2_asBuidable = configValue; break;
                                case "asBuildable_LabCart":
                                    ConfigSwitcher.LabCart_asBuildable = configValue; break;
                                case "asBuildable_EmptyDesk":
                                    ConfigSwitcher.EmptyDesk_asBuildable = configValue; break;
                                case "precursorKeysAll":
                                    ConfigSwitcher.PrecursorKeysAll = configValue; break;
                                case "precursorKeys_RecipiesResource":
                                    tmpresource = TechType.None;
                                    if (TechTypeExtensions.FromString(configValueStr, out tmpresource, true) && tmpresource != TechType.None)
                                        ConfigSwitcher.PrecursorKeysResource = tmpresource;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource type for precursor keys recipies. Default resource will be used.");
                                    break;
                                case "precursorKeys_RecipiesResourceAmount":
                                    tmpVal = -1;
                                    if (int.TryParse(configValueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out tmpVal) && tmpVal >= 1 && tmpVal <= 10)
                                        ConfigSwitcher.PrecursorKeysResourceAmount = tmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource amount for precursor keys recipies. Default amount will be used.");
                                    break;
                                case "relics_RecipiesResource":
                                    if (TechTypeExtensions.FromString(configValueStr, out tmpresource, true) && tmpresource != TechType.None)
                                        ConfigSwitcher.RelicRecipiesResource = tmpresource;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource type for relics recipies. Default resource will be used.");
                                    break;
                                case "relics_RecipiesResourceAmount":
                                    tmpVal = -1;
                                    if (int.TryParse(configValueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out tmpVal) && tmpVal >= 1 && tmpVal <= 10)
                                        ConfigSwitcher.RelicRecipiesResourceAmount = tmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource amount for relics recipies. Default amount will be used.");
                                    break;
                                case "creatureEggs_RecipiesResource":
                                    if (TechTypeExtensions.FromString(configValueStr, out tmpresource, true) && tmpresource != TechType.None)
                                        ConfigSwitcher.CreatureEggsResource = tmpresource;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource type for creature eggs recipies. Default resource will be used.");
                                    break;
                                case "creatureEggs_RecipiesResourceAmount":
                                    tmpVal = -1;
                                    if (int.TryParse(configValueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out tmpVal) && tmpVal >= 1 && tmpVal <= 10)
                                        ConfigSwitcher.CreatureEggsResourceAmount = tmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource amount for creature eggs recipies. Default amount will be used.");
                                    break;
                                case "flora_RecipiesResource":
                                    tmpresource = TechType.None;
                                    if (TechTypeExtensions.FromString(configValueStr, out tmpresource, true) && tmpresource != TechType.None)
                                        ConfigSwitcher.FloraRecipiesResource = tmpresource;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource type for flora recipies. Default resource will be used.");
                                    break;
                                case "flora_RecipiesResourceAmount":
                                    tmpVal = -1;
                                    if (int.TryParse(configValueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out tmpVal) && tmpVal >= 1 && tmpVal <= 10)
                                        ConfigSwitcher.FloraRecipiesResourceAmount = tmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource amount for flora recipies. Default amount will be used.");
                                    break;
                                case "config_LandTree":
                                    GetFloraConfig(ConfigSwitcher.config_LandTree1, configValueStr); break;
                                case "config_JungleTreeA":
                                    GetFloraConfig(ConfigSwitcher.config_JungleTree1, configValueStr); break;
                                case "config_JungleTreeB":
                                    GetFloraConfig(ConfigSwitcher.config_JungleTree2, configValueStr); break;
                                case "config_TropicalTreeA":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalTreeA, configValueStr); break;
                                case "config_TropicalTreeB":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalTreeB, configValueStr); break;
                                case "config_TropicalTreeC":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalTreeC, configValueStr); break;
                                case "config_TropicalTreeD":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalTreeD, configValueStr); break;
                                case "config_LandPlantRedA":
                                    GetFloraConfig(ConfigSwitcher.config_LandPlant1, configValueStr); break;
                                case "config_LandPlantRedB":
                                    GetFloraConfig(ConfigSwitcher.config_LandPlant2, configValueStr); break;
                                case "config_LandPlantA":
                                    GetFloraConfig(ConfigSwitcher.config_LandPlant3, configValueStr); break;
                                case "config_LandPlantB":
                                    GetFloraConfig(ConfigSwitcher.config_LandPlant4, configValueStr); break;
                                case "config_LandPlantC":
                                    GetFloraConfig(ConfigSwitcher.config_LandPlant5, configValueStr); break;
                                case "config_FernA":
                                    GetFloraConfig(ConfigSwitcher.config_Fern2, configValueStr); break;
                                case "config_FernB":
                                    GetFloraConfig(ConfigSwitcher.config_Fern4, configValueStr); break;
                                case "config_TropicalPlantA":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantA, configValueStr); break;
                                case "config_TropicalPlantB":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantB, configValueStr); break;
                                case "config_TropicalPlantC":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantC, configValueStr); break;
                                case "config_TropicalPlantD":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantD, configValueStr); break;
                                case "config_TropicalPlantE":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantE, configValueStr); break;
                                case "config_TropicalPlantF":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantF, configValueStr); break;
                                case "config_TropicalPlantG":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantG, configValueStr); break;
                                case "config_TropicalPlantH":
                                    GetFloraConfig(ConfigSwitcher.config_TropicalPlantH, configValueStr); break;
                                case "config_CrabClawKelpA":
                                    GetFloraConfig(ConfigSwitcher.config_CrabClawKelp2, configValueStr); break;
                                case "config_CrabClawKelpB":
                                    GetFloraConfig(ConfigSwitcher.config_CrabClawKelp1, configValueStr); break;
                                case "config_CrabClawKelpC":
                                    GetFloraConfig(ConfigSwitcher.config_CrabClawKelp3, configValueStr); break;
                                case "config_PyroCoralA":
                                    GetFloraConfig(ConfigSwitcher.config_PyroCoral1, configValueStr); break;
                                case "config_PyroCoralB":
                                    GetFloraConfig(ConfigSwitcher.config_PyroCoral2, configValueStr); break;
                                case "config_PyroCoralC":
                                    GetFloraConfig(ConfigSwitcher.config_PyroCoral3, configValueStr); break;
                                case "config_CoveTree":
                                    GetFloraConfig(ConfigSwitcher.config_CoveTree1, configValueStr); break;
                                case "config_SpottedReedsA":
                                    GetFloraConfig(ConfigSwitcher.config_GreenReeds1, configValueStr); break;
                                case "config_SpottedReedsB":
                                    GetFloraConfig(ConfigSwitcher.config_GreenReeds6, configValueStr); break;
                                case "config_BrineLily":
                                    GetFloraConfig(ConfigSwitcher.config_LostRiverPlant2, configValueStr); break;
                                case "config_LostRiverPlant":
                                    GetFloraConfig(ConfigSwitcher.config_LostRiverPlant4, configValueStr); break;
                                case "config_CoralReefPlantMiddle":
                                    GetFloraConfig(ConfigSwitcher.config_PlantMiddle11, configValueStr); break;
                                case "config_SmallMushroomsDeco":
                                    GetFloraConfig(ConfigSwitcher.config_SmallDeco3, configValueStr); break;
                                case "config_FloatingStone":
                                    GetFloraConfig(ConfigSwitcher.config_FloatingStone1, configValueStr); break;
                                case "config_BrownCoralTubesA":
                                    GetFloraConfig(ConfigSwitcher.config_BrownCoralTubes1, configValueStr); break;
                                case "config_BrownCoralTubesB":
                                    GetFloraConfig(ConfigSwitcher.config_BrownCoralTubes2, configValueStr); break;
                                case "config_BrownCoralTubesC":
                                    GetFloraConfig(ConfigSwitcher.config_BrownCoralTubes3, configValueStr); break;
                                case "config_BlueCoralTubes":
                                    GetFloraConfig(ConfigSwitcher.config_BlueCoralTubes1, configValueStr); break;
                                case "config_PurplePinecone":
                                    GetFloraConfig(ConfigSwitcher.config_SmallDeco10, configValueStr); break;
                                case "config_CoralPlantYellow":
                                    GetFloraConfig(ConfigSwitcher.config_SmallDeco11, configValueStr); break;
                                case "config_CoralPlantGreen":
                                    GetFloraConfig(ConfigSwitcher.config_SmallDeco13, configValueStr); break;
                                case "config_CoralPlantBlue":
                                    GetFloraConfig(ConfigSwitcher.config_SmallDeco14, configValueStr); break;
                                case "config_CoralPlantRed":
                                    GetFloraConfig(ConfigSwitcher.config_SmallDeco15Red, configValueStr); break;
                                case "config_CoralPlantPurple":
                                    GetFloraConfig(ConfigSwitcher.config_SmallDeco17Purple, configValueStr); break;
                                case "config_BloodGrass":
                                    GetFloraConfig(ConfigSwitcher.config_BloodGrassRed, configValueStr); break;
                                case "config_RedGrass1":
                                    GetFloraConfig(ConfigSwitcher.config_RedGrass1, configValueStr); break;
                                case "config_RedGrass2":
                                    GetFloraConfig(ConfigSwitcher.config_RedGrass2, configValueStr); break;
                                case "config_RedGrass2Tall":
                                    GetFloraConfig(ConfigSwitcher.config_RedGrass2Tall, configValueStr); break;
                                case "config_RedGrass3":
                                    GetFloraConfig(ConfigSwitcher.config_RedGrass3, configValueStr); break;
                                case "config_RedGrass3Tall":
                                    GetFloraConfig(ConfigSwitcher.config_RedGrass3Tall, configValueStr); break;
                                case "config_BloodGrassDense":
                                    GetFloraConfig(ConfigSwitcher.config_BloodGrassDense, configValueStr); break;
                                case "config_MushroomTree1":
                                    GetFloraConfig(ConfigSwitcher.config_MushroomTree1, configValueStr); break;
                                case "config_MushroomTree2":
                                    GetFloraConfig(ConfigSwitcher.config_MushroomTree2, configValueStr); break;
                                case "addRegularAirSeeds":
                                    ConfigSwitcher.EnableRegularAirSeeds = configValue; break;
                                case "addAirSeedsWhenDiscovered":
                                    ConfigSwitcher.AddAirSeedsWhenDiscovered = configValue; break;
                                case "addRegularWaterSeeds":
                                    ConfigSwitcher.EnableRegularWaterSeeds = configValue; break;
                                case "addWaterSeedsWhenDiscovered":
                                    ConfigSwitcher.AddWaterSeedsWhenDiscovered = configValue; break;
                                case "GhostLeviatan_enable":
                                    GhostLeviatan_enable = configValue; break;
                                case "GhostLeviatan_maxSpawns":
                                    tmpVal = -1;
                                    if (int.TryParse(configValueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out tmpVal) && tmpVal >= 1 && tmpVal <= 10)
                                        ConfigSwitcher.GhostLeviatan_maxSpawns = tmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for ghost leviathan max spawns. Default amount will be used.");
                                    break;
                                case "GhostLeviatan_timeBeforeFirstSpawn":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 10.0f && fTmpVal <= 14400.0f)
                                        ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid value for the time before first ghost leviathan spawns. Default value will be used.");
                                    break;
                                case "GhostLeviatan_spawnTimeRatio":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1f && fTmpVal <= 1000.0f)
                                        ConfigSwitcher.GhostLeviatan_spawnTimeRatio = fTmpVal * 0.01f;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid value for ghost leviathan spawn time ratio. Default value will be used.");
                                    break;
                                case "GhostLeviatan_health":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 10.0f && fTmpVal <= 20000.0f)
                                        ConfigSwitcher.GhostLeviatan_health = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for ghost leviathan health points. Default amount will be used.");
                                    break;
                                case "alienRelic1Animation":
                                    AlienRelic1Animation = configValue; break;
                                case "alienRelic2Animation":
                                    AlienRelic2Animation = configValue; break;
                                case "alienRelic3Animation":
                                    AlienRelic3Animation = configValue; break;
                                case "alienRelic4Animation":
                                    AlienRelic4Animation = configValue; break;
                                case "alienRelic5Animation":
                                    AlienRelic5Animation = configValue; break;
                                case "alienRelic6Animation":
                                    AlienRelic6Animation = configValue; break;
                                case "alienRelic7Animation":
                                    AlienRelic7Animation = configValue; break;
                                case "alienRelic8Animation":
                                    AlienRelic8Animation = configValue; break;
                                case "alienRelic9Animation":
                                    AlienRelic9Animation = configValue; break;
                                case "alienRelic10Animation":
                                    AlienRelic10Animation = configValue; break;
                                case "alienRelic11Animation":
                                    AlienRelic11Animation = configValue; break;
                                case "barBottle1Water":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarBottle1Value = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar bottle 1 water points. Default amount will be used.");
                                    break;
                                case "barBottle2Water":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarBottle2Value = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar bottle 2 water points. Default amount will be used.");
                                    break;
                                case "barBottle3Water":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarBottle3Value = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar bottle 3 water points. Default amount will be used.");
                                    break;
                                case "barBottle4Water":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarBottle4Value = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar bottle 4 water points. Default amount will be used.");
                                    break;
                                case "barBottle5Water":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarBottle5Value = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar bottle 5 water points. Default amount will be used.");
                                    break;
                                case "barFood1Nutrient":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarFood1FoodValue = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar food 1 nutrient points. Default amount will be used.");
                                    break;
                                case "barFood1Water":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarFood1WaterValue = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar food 1 water points. Default amount will be used.");
                                    break;
                                case "barFood2Nutrient":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarFood2FoodValue = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar food 2 nutrient points. Default amount will be used.");
                                    break;
                                case "barFood2Water":
                                    fTmpVal = -1.0f;
                                    if (float.TryParse(configValueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out fTmpVal) && fTmpVal >= 1.0f && fTmpVal <= 100.0f)
                                        ConfigSwitcher.BarFood2WaterValue = fTmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid amount for bar food 2 water points. Default amount will be used.");
                                    break;
                                case "purplePineconeDroppedResource":
                                    if (TechTypeExtensions.FromString(configValueStr, out tmpresource, true) && tmpresource != TechType.None)
                                        ConfigSwitcher.PurplePineconeDroppedResource = tmpresource;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource type for purple pinecone harvested resource. Default resource will be used.");
                                    break;
                                case "purplePineconeDroppedResourceAmount":
                                    tmpVal = -1;
                                    if (int.TryParse(configValueStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out tmpVal) && tmpVal >= 1 && tmpVal <= 10)
                                        ConfigSwitcher.PurplePineconeDroppedResourceAmount = tmpVal;
                                    else
                                        Logger.Log("WARNING: \"" + configValueStr + "\" is not a valid resource amount for purple pinecone harvested resource. Default amount will be used.");
                                    break;
                                case "language":
                                    bool found = false;
                                    foreach (string lang in RegionHelper.AvailableLanguages)
                                        if (string.Compare(configValueStr, lang, true, CultureInfo.InvariantCulture) == 0)
                                        {
                                            LanguageHelper.UserLanguage = RegionHelper.GetCountryCodeFromLabel(lang);
                                            found = true;
                                        }
                                    if (!found)
                                    {
                                        if (string.Compare(configValueStr, "auto", true, CultureInfo.InvariantCulture) != 0) // Log error if input language is not "auto" or one of the supported languages
                                            Logger.Log("WARNING: Wrong language setting \"" + configValueStr + "\" in Config file, default language will be used.");
                                        LanguageHelper.UserLanguage = RegionHelper.GetDefaultCountryCode();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                // If new flora has been disabled, disable all features related to new flora.
                if (!ConfigSwitcher.EnableNewFlora)
                {
                    ConfigSwitcher.EnableRegularAirSeeds = false;
                    ConfigSwitcher.EnableRegularWaterSeeds = false;
                    ConfigSwitcher.AddAirSeedsWhenDiscovered = false;
                    ConfigSwitcher.AddWaterSeedsWhenDiscovered = false;
                }
            }
            else
                Logger.Log("WARNING: Cannot find config file. Default settings will be used.");
        }
    }
}
