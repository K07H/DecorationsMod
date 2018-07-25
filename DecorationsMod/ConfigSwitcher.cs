using System;
using System.IO;
using System.Reflection;

namespace DecorationsMod
{
    public static class ConfigSwitcher
    {
        // THESE ARE DEFAULT SETTING.
        // They are automatically overwritten when getting configuration from Config.txt file.
        // They will be used only if Config.txt file has not been found.
        #region Settings
        
        // If "true" player will be able to place following items:
        // coffee cups, polyaniline, hydrochloric acid, benzene, hatching enzymes, eggs, snacks, lubricant, bleach, 
        // water bottles, wiring kits, computer chip, ion crystal, precursor tablets, stalker tooth, first aid kit
        public static bool EnablePlaceItems = true;

        // If "true" player will be able to place batteries, powercells, ion batteries and ion powercells.
        // Attention: If this option is enabled, you'll have to manually drag-n-drop batteries/power cells to the charger (left click won't work to equip).
        public static bool EnablePlaceBatteries = false;

        // If "true", player will be able to build/craft the following items:
        // specimen analyzer, markiplier doll 1, markiplier doll 2, jacksepticeye doll, eatmydiction doll
        // lamp, seamoth doll, exosuit doll, forklift, cargo crates, sofas
        public static bool EnableSpecialItems = true;

        // If "true", player will be able to craft Nutrient Blocks from the decorations fabricator.
        public static bool EnableNutrientBlock = true;

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

        // New flora recipies
        public static TechType FloraRecipiesResource = TechType.PrecursorIonCrystal;

        // New flora default configuration
        public static CustomFlora config_LandTree1 = new CustomFlora(2400.0f, 200.0f, true, 3.0f, 6.0f, false, 500.0f);
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

        // Add existing game seeds to the seeds fabricator, default config
        public static bool EnableRegularAirSeeds = true;
        public static bool EnableRegularWaterSeeds = true;

        // Ghost leviathans (spawned from Cove Trees) default config
        public static bool GhostLeviatan_enable = true;
        public static int GhostLeviatan_maxSpawns = 2;
        public static float GhostLeviatan_timeBeforeFirstSpawn = 3600f;
        public static float GhostLeviatan_spawnTimeRatio = 1f;
        public static float GhostLeviatan_health = 100f;

        #endregion
        
        // Utility function to parse flora configuration element.
        private static bool GetFloraConfig(CustomFlora customFlora, string configStr)
        {
            bool success = true;
            string[] elems = configStr.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (elems.Length == 3)
            {
                if (!float.TryParse(elems[0], out customFlora.GrowthDuration))
                    success = false;
                if (!float.TryParse(elems[1], out customFlora.Health))
                    success = false;
                if (!float.TryParse(elems[2], out customFlora.Charge))
                    success = false;
            }
            return success;
        }

        // Loads configuration from Config.txt file.
        public static void LoadConfiguration()
        {
            // Get config file path
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string currentDir = Path.GetDirectoryName(path);
            string configFilePath = currentDir + "/Config.txt";

            Logger.Log("Loading configuration from \"" + configFilePath + "\"...");

            // Retrieve config
            if (File.Exists(configFilePath))
            {
                string configFile = File.ReadAllText(configFilePath);
                string[] configLines = configFile.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string configStr in configLines)
                {
                    if (!configStr.StartsWith("#") && configStr.Contains("="))
                    {
                        string[] configElem = configStr.Split("=".ToCharArray(), StringSplitOptions.None);
                        if (configElem != null && configElem.Length == 2)
                        {
                            string configKey = configElem[0].Trim();
                            string configValueStr = configElem[1].Trim();
                            bool configValue = (configValueStr.CompareTo("true") == 0);

                            switch (configKey)
                            {
                                case "enablePlaceItems":
                                    ConfigSwitcher.EnablePlaceItems = configValue; break;
                                case "enablePlaceBatteries":
                                    ConfigSwitcher.EnablePlaceBatteries = configValue; break;
                                case "enableSpecialItems":
                                    ConfigSwitcher.EnableSpecialItems = configValue; break;
                                case "enableNutrientBlock":
                                    ConfigSwitcher.EnableNutrientBlock = configValue; break;
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
                                case "flora_RecipiesResource":
                                    TechType tmpresource = TechType.None;
                                    if (TechTypeExtensions.FromString(configValueStr, out tmpresource, true) && tmpresource != TechType.None)
                                        ConfigSwitcher.FloraRecipiesResource = tmpresource;
                                    else
                                        Logger.Log("Warning: \"" + configValueStr + "\" is not a valid resource type for flora recipies. Default resource will be set.");
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
                                case "addRegularAirSeeds":
                                    ConfigSwitcher.EnableRegularAirSeeds = configValue; break;
                                case "addRegularWaterSeeds":
                                    ConfigSwitcher.EnableRegularWaterSeeds = configValue; break;
                                case "GhostLeviatan_enable":
                                    GhostLeviatan_enable = configValue; break;
                                case "GhostLeviatan_maxSpawns":
                                    int.TryParse(configValueStr, out ConfigSwitcher.GhostLeviatan_maxSpawns); break;
                                case "GhostLeviatan_timeBeforeFirstSpawn":
                                    float.TryParse(configValueStr, out ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn); break;
                                case "GhostLeviatan_spawnTimeRatio":
                                    float.TryParse(configValueStr, out ConfigSwitcher.GhostLeviatan_spawnTimeRatio); break;
                                case "GhostLeviatan_health":
                                    float.TryParse(configValueStr, out ConfigSwitcher.GhostLeviatan_health); break;
                                case "language":
                                    if (configValueStr.CompareTo("fr") == 0)
                                        LanguageHelper.UserLanguage = RegionHelper.CountryCode.FR;
                                    else if (configValueStr.CompareTo("es") == 0)
                                        LanguageHelper.UserLanguage = RegionHelper.CountryCode.ES;
                                    else if (configValueStr.CompareTo("tr") == 0)
                                        LanguageHelper.UserLanguage = RegionHelper.CountryCode.TR;
                                    else if (configValueStr.CompareTo("en") == 0)
                                        LanguageHelper.UserLanguage = RegionHelper.CountryCode.EN;
                                    // else, do nothing (uses default language from Windows current Culture)
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            else
                Logger.Log("Warning: Cannot find config file. Default options will be set.");
        }
    }
}
