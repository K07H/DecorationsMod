using FMOD;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DecorationsMod
{
    class ConfigOptions : SMLHelper.V2.Options.ModOptions
    {
        public ConfigOptions(string name) : base(name)
        {
        }

        public override void BuildModOptions()
        {
            this.AddChoiceOption("DecorationsLanguage", "Language", RegionHelper.AvailableLanguages, RegionHelper.GetCountryLabelFromCode(LanguageHelper.UserLanguage));
            
            this.AddToggleOption("UseFlatScreenResolution", "Use alternate screen resolution", ConfigSwitcher.UseFlatScreenResolution);
            this.AddToggleOption("UseCompactTooltips", "Use compact tooltips", ConfigSwitcher.UseCompactTooltips);
            this.AddToggleOption("LockQuickslotsWhenPlacingItem", "Lock quickslots when placing item", ConfigSwitcher.LockQuickslotsWhenPlacingItem);
            this.AddToggleOption("AllowBuildOutside", "Allow build outside", ConfigSwitcher.AllowBuildOutside);
            this.AddToggleOption("AllowPlaceOutside", "Allow place outside", ConfigSwitcher.AllowPlaceOutside);
            this.AddToggleOption("EnablePlaceItems", "Enable items placing", ConfigSwitcher.EnablePlaceItems);
            this.AddToggleOption("EnablePlaceBatteries", "Enable batteries placing", ConfigSwitcher.EnablePlaceBatteries);
            this.AddToggleOption("EnableSpecialItems", "Enable special items", ConfigSwitcher.EnableSpecialItems);
            this.AddToggleOption("EnablePrecursorTab", "Enable precursor tab", ConfigSwitcher.EnablePrecursorTab);
            this.AddToggleOption("PrecursorKeysAll", "Add regular alien tablets in deco fabricator", ConfigSwitcher.PrecursorKeysAll);
            this.AddToggleOption("EnableRegularEggs", "Add regular eggs in deco fabricator", ConfigSwitcher.EnableRegularEggs);
            this.AddToggleOption("EnableNutrientBlock", "Add nutrient block in deco fabricator", ConfigSwitcher.EnableNutrientBlock);
            this.AddToggleOption("EnableRegularAirSeeds", "Add regular air seeds in flora fabricator", ConfigSwitcher.EnableRegularAirSeeds);
            this.AddToggleOption("EnableRegularWaterSeeds", "Add regular water seeds in flora fabricator", ConfigSwitcher.EnableRegularWaterSeeds);
            this.AddToggleOption("AllowIndoorLongPlanterOutside", "Allow indoor long planter outside", ConfigSwitcher.AllowIndoorLongPlanterOutside);
            this.AddToggleOption("AllowOutdoorLongPlanterInside", "Allow outdoor long planter inside", ConfigSwitcher.AllowOutdoorLongPlanterInside);
            this.AddToggleOption("FixAquariumLighting", "Fix aquarium lighting", ConfigSwitcher.FixAquariumLighting);
            this.AddToggleOption("GlowingAquariumGlass", "Enable aquarium glowing effect", ConfigSwitcher.GlowingAquariumGlass);

            this.AddChoiceOption("PrecursorKeysResourceAmount", "Amount of resources to craft alien tablet", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.PrecursorKeysResourceAmount > 10 ? "10" : (ConfigSwitcher.PrecursorKeysResourceAmount < 1 ? "1" : ConfigSwitcher.PrecursorKeysResourceAmount.ToString(CultureInfo.InvariantCulture)));
            this.AddChoiceOption("RelicRecipiesResourceAmount", "Amount of resources to craft alien relic", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.RelicRecipiesResourceAmount > 10 ? "10" : (ConfigSwitcher.RelicRecipiesResourceAmount < 1 ? "1" : ConfigSwitcher.RelicRecipiesResourceAmount.ToString(CultureInfo.InvariantCulture)));
            this.AddChoiceOption("CreatureEggsResourceAmount", "Amount of resources to craft creature egg", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.CreatureEggsResourceAmount > 10 ? "10" : (ConfigSwitcher.CreatureEggsResourceAmount < 1 ? "1" : ConfigSwitcher.CreatureEggsResourceAmount.ToString(CultureInfo.InvariantCulture)));
            this.AddChoiceOption("FloraRecipiesResourceAmount", "Amount of resources to craft flora seed", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.FloraRecipiesResourceAmount > 10 ? "10" : (ConfigSwitcher.FloraRecipiesResourceAmount < 1 ? "1" : ConfigSwitcher.FloraRecipiesResourceAmount.ToString(CultureInfo.InvariantCulture)));

            /*
            this.AddToggleOption("AlienRelic1Animation", "Enable alien relic 1 animation", ConfigSwitcher.AlienRelic1Animation);
            this.AddToggleOption("AlienRelic2Animation", "Enable alien relic 2 animation", ConfigSwitcher.AlienRelic2Animation);
            this.AddToggleOption("AlienRelic3Animation", "Enable alien relic 3 animation", ConfigSwitcher.AlienRelic3Animation);
            this.AddToggleOption("AlienRelic4Animation", "Enable alien relic 4 animation", ConfigSwitcher.AlienRelic4Animation);
            this.AddToggleOption("AlienRelic5Animation", "Enable alien relic 5 animation", ConfigSwitcher.AlienRelic5Animation);
            this.AddToggleOption("AlienRelic6Animation", "Enable alien relic 6 animation", ConfigSwitcher.AlienRelic6Animation);
            this.AddToggleOption("AlienRelic7Animation", "Enable alien relic 7 animation", ConfigSwitcher.AlienRelic7Animation);
            this.AddToggleOption("AlienRelic8Animation", "Enable alien relic 8 animation", ConfigSwitcher.AlienRelic8Animation);
            this.AddToggleOption("AlienRelic9Animation", "Enable alien relic 9 animation", ConfigSwitcher.AlienRelic9Animation);
            this.AddToggleOption("AlienRelic10Animation", "Enable alien relic 10 animation", ConfigSwitcher.AlienRelic10Animation);
            this.AddToggleOption("AlienRelic11Animation", "Enable alien relic 11 animation", ConfigSwitcher.AlienRelic11Animation);
            */

            this.AddToggleOption("GhostLeviatan_enable", "Enable leviatan spawning from cove tree eggs", ConfigSwitcher.GhostLeviatan_enable);
            this.AddChoiceOption("GhostLeviatan_maxSpawns", "Leviatans to spawn before eggs disappear", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.GhostLeviatan_maxSpawns > 10 ? "10" : (ConfigSwitcher.GhostLeviatan_maxSpawns < 1 ? "1" : ConfigSwitcher.GhostLeviatan_maxSpawns.ToString(CultureInfo.InvariantCulture)));
            this.AddSliderOption("GhostLeviatan_timeBeforeFirstSpawn", "First leviatan spawn in", 10.0f, 14400.0f, ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn > 14400.0f ? 14400.0f : (ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn < 10.0f ? 10.0f : ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn));
            this.AddSliderOption("GhostLeviatan_spawnTimeRatio", "Time between 2 leviatan spawns", 0.01f, 10.0f, ConfigSwitcher.GhostLeviatan_spawnTimeRatio > 10.0f ? 10.0f : (ConfigSwitcher.GhostLeviatan_spawnTimeRatio < 0.01f ? 0.01f : ConfigSwitcher.GhostLeviatan_spawnTimeRatio));
            this.AddSliderOption("GhostLeviatan_health", "Spawned leviatan health points", 20.0f, 20000.0f, ConfigSwitcher.GhostLeviatan_health > 20000.0f ? 20000.0f : (ConfigSwitcher.GhostLeviatan_health < 20.0f ? 20.0f : ConfigSwitcher.GhostLeviatan_health));

            this.ChoiceChanged += ConfigOptions_ChoiceChanged;
            this.SliderChanged += ConfigOptions_SliderChanged;
            this.ToggleChanged += ConfigOptions_ToggleChanged;
        }

        public void UpdateConfigFile(string oldStr, string newStr)
        {
            if (!string.IsNullOrEmpty(oldStr) && !string.IsNullOrEmpty(newStr) && !string.IsNullOrEmpty(ConfigSwitcher.configFilePath) && File.Exists(ConfigSwitcher.configFilePath))
            {
                try
                {
                    string currentConfig = File.ReadAllText(ConfigSwitcher.configFilePath);
                    if (!string.IsNullOrEmpty(currentConfig))
                    {
                        string newConfig = null;
                        if (oldStr.EndsWith("=\r\n"))
                        {
                            string toSearch = oldStr.Substring(0, oldStr.Length - 2);
                            int stt = currentConfig.IndexOf(toSearch);
                            if (stt > 0)
                            {
                                int end = currentConfig.IndexOf("\r\n", stt + toSearch.Length);
                                if (end > stt && end < currentConfig.Length)
                                {
                                    string currentVal = currentConfig.Substring(stt, (end - stt)) + "\r\n";
                                    Logger.Log("DEBUG: Replacing config [" + currentVal + "] with [" + newStr + "].");
                                    newConfig = currentConfig.Replace(currentVal, newStr);
                                }
                            }
                        }
                        else
                            newConfig = currentConfig.Replace(oldStr, newStr);
                        if (newConfig != null)
                            File.WriteAllText(ConfigSwitcher.configFilePath, newConfig);
                    }
                }
                catch { ErrorMessage.AddMessage("An error happened while updating config file at \"" + ConfigSwitcher.configFilePath + "\"!"); }
            }
        }

        private void ConfigOptions_ToggleChanged(object sender, SMLHelper.V2.Options.ToggleChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e?.Id))
            {
                switch (e.Id)
                {
                    case "UseFlatScreenResolution":
                        ConfigSwitcher.UseFlatScreenResolution = e.Value;
                        UpdateConfigFile("\r\nuseAlternativeScreenResolution=" + (e.Value ? "false" : "true") + "\r\n", "\r\nuseAlternativeScreenResolution=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " alternate screen resolution.");
                        break;
                    case "UseCompactTooltips":
                        ConfigSwitcher.UseCompactTooltips = e.Value;
                        UpdateConfigFile("\r\nuseCompactTooltips=" + (e.Value ? "false" : "true") + "\r\n", "\r\nuseCompactTooltips=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Compact tooltips " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "LockQuickslotsWhenPlacingItem":
                        ConfigSwitcher.LockQuickslotsWhenPlacingItem = e.Value;
                        UpdateConfigFile("\r\nlockQuickslotsWhenPlacingItem=" + (e.Value ? "false" : "true") + "\r\n", "\r\nlockQuickslotsWhenPlacingItem=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Lock quickslots when placing item " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AllowBuildOutside":
                        ConfigSwitcher.AllowBuildOutside = e.Value;
                        UpdateConfigFile("\r\nallowBuildOutside=" + (e.Value ? "false" : "true") + "\r\n", "\r\nallowBuildOutside=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Allow build outside " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AllowPlaceOutside":
                        ConfigSwitcher.AllowPlaceOutside = e.Value;
                        UpdateConfigFile("\r\nallowPlaceOutside=" + (e.Value ? "false" : "true") + "\r\n", "\r\nallowPlaceOutside=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Allow place outside " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "EnablePlaceItems":
                        ConfigSwitcher.EnablePlaceItems = e.Value;
                        if (e.Value)
                            PlaceToolItems.MakeItemsPlaceable();
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " placeable items.");
                        UpdateConfigFile("\r\nenablePlaceItems=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenablePlaceItems=" + (e.Value ? "true" : "false") + "\r\n");
                        break;
                    case "EnablePlaceBatteries":
                        ConfigSwitcher.EnablePlaceBatteries = e.Value;
                        if (e.Value)
                        {
                            PlaceToolItems.MakeBatteriesPlaceable();
                            DecorationsMod.PatchBatteries();
                        }
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " placeable batteries.");
                        UpdateConfigFile("\r\nenablePlaceBatteries=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenablePlaceBatteries=" + (e.Value ? "true" : "false") + "\r\n");
                        break;
                    case "EnableSpecialItems":
                        ConfigSwitcher.EnableSpecialItems = e.Value;
                        UpdateConfigFile("\r\nenableNewBuilderItems=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenableNewBuilderItems=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " new builder items.");
                        break;
                    case "EnablePrecursorTab":
                        ConfigSwitcher.EnablePrecursorTab = e.Value;
                        UpdateConfigFile("\r\nenablePrecursorTab=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenablePrecursorTab=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " precursor tab.");
                        break;
                    case "EnableNutrientBlock":
                        ConfigSwitcher.EnableNutrientBlock = e.Value;
                        UpdateConfigFile("\r\nenableNutrientBlock=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenableNutrientBlock=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " nutrient block.");
                        break;
                    case "EnableRegularEggs":
                        ConfigSwitcher.EnableRegularEggs = e.Value;
                        UpdateConfigFile("\r\nenableAllEggs=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenableAllEggs=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " regular eggs.");
                        break;
                    case "PrecursorKeysAll":
                        ConfigSwitcher.PrecursorKeysAll = e.Value;
                        UpdateConfigFile("\r\nprecursorKeysAll=" + (e.Value ? "false" : "true") + "\r\n", "\r\nprecursorKeysAll=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " regular alien tablets.");
                        break;
                    case "EnableRegularAirSeeds":
                        ConfigSwitcher.EnableRegularAirSeeds = e.Value;
                        UpdateConfigFile("\r\naddRegularAirSeeds=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddRegularAirSeeds=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " regular air seeds.");
                        break;
                    case "EnableRegularWaterSeeds":
                        ConfigSwitcher.EnableRegularWaterSeeds = e.Value;
                        UpdateConfigFile("\r\naddRegularWaterSeeds=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddRegularWaterSeeds=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " regular water seeds.");
                        break;
                    case "AllowIndoorLongPlanterOutside":
                        ConfigSwitcher.AllowIndoorLongPlanterOutside = e.Value;
                        UpdateConfigFile("\r\nallowIndoorLongPlanterOutside=" + (e.Value ? "false" : "true") + "\r\n", "\r\nallowIndoorLongPlanterOutside=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Indoor long planter outside " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AllowOutdoorLongPlanterInside":
                        ConfigSwitcher.AllowOutdoorLongPlanterInside = e.Value;
                        UpdateConfigFile("\r\nallowOutdoorLongPlanterInside=" + (e.Value ? "false" : "true") + "\r\n", "\r\nallowOutdoorLongPlanterInside=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Outdoor long planter inside " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "FixAquariumLighting":
                        ConfigSwitcher.FixAquariumLighting = e.Value;
                        if (e.Value)
                        {
                            PrefabsHelper.FixAquariumSkyApplier();
                            ErrorMessage.AddMessage("Aquarium lighting fix enabled.");
                        }
                        else
                            ErrorMessage.AddMessage("Restart game completely to disable aquarium lighting fix.");
                        UpdateConfigFile("\r\nfixAquariumLighting=" + (e.Value ? "false" : "true") + "\r\n", "\r\nfixAquariumLighting=" + (e.Value ? "true" : "false") + "\r\n");
                        break;
                    case "GlowingAquariumGlass":
                        ConfigSwitcher.GlowingAquariumGlass = e.Value;
                        UpdateConfigFile("\r\nenableAquariumGlassGlowing=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenableAquariumGlassGlowing=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " glowing effect.");
                        break;
                    case "GhostLeviatan_enable":
                        ConfigSwitcher.GhostLeviatan_enable = e.Value;
                        UpdateConfigFile("\r\nGhostLeviatan_enable=" + (e.Value ? "false" : "true") + "\r\n", "\r\nGhostLeviatan_enable=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Ghost Leviatan spawning " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    /*
                    case "AlienRelic1Animation":
                        ConfigSwitcher.AlienRelic1Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic1Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic1Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 1 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic2Animation":
                        ConfigSwitcher.AlienRelic2Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic2Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic2Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 2 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic3Animation":
                        ConfigSwitcher.AlienRelic3Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic3Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic3Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 3 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic4Animation":
                        ConfigSwitcher.AlienRelic4Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic4Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic4Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 4 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic5Animation":
                        ConfigSwitcher.AlienRelic5Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic5Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic5Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 5 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic6Animation":
                        ConfigSwitcher.AlienRelic6Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic6Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic6Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 6 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic7Animation":
                        ConfigSwitcher.AlienRelic7Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic7Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic7Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 7 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic8Animation":
                        ConfigSwitcher.AlienRelic8Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic8Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic8Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 8 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic9Animation":
                        ConfigSwitcher.AlienRelic9Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic9Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic9Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 9 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic10Animation":
                        ConfigSwitcher.AlienRelic10Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic10Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic10Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 10 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    case "AlienRelic11Animation":
                        ConfigSwitcher.AlienRelic11Animation = e.Value;
                        UpdateConfigFile("\r\nalienRelic11Animation=" + (e.Value ? "false" : "true") + "\r\n", "\r\nalienRelic11Animation=" + (e.Value ? "true" : "false") + "\r\n");
                        ErrorMessage.AddMessage("Alien relic 11 animation " + (e.Value ? "enabled" : "disabled") + ".");
                        break;
                    */
                    default:
                        break;
                }
            }
        }

        private void ConfigOptions_SliderChanged(object sender, SMLHelper.V2.Options.SliderChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e?.Id))
            {
                if (e.Id == "GhostLeviatan_timeBeforeFirstSpawn")
                {
                    ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nGhostLeviatan_timeBeforeFirstSpawn=\r\n", "\r\nGhostLeviatan_timeBeforeFirstSpawn=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Seconds until first leviathan spawn set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "GhostLeviatan_spawnTimeRatio")
                {
                    ConfigSwitcher.GhostLeviatan_spawnTimeRatio = e.Value;
                    UpdateConfigFile("\r\nGhostLeviatan_spawnTimeRatio=\r\n", "\r\nGhostLeviatan_spawnTimeRatio=" + e.Value.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Time ratio between two leviathan spawns set to \"" + e.Value + "\".");
                }
                else if (e.Id == "GhostLeviatan_health")
                {
                    ConfigSwitcher.GhostLeviatan_health = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nGhostLeviatan_health=\r\n", "\r\nGhostLeviatan_health=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Leviathan health set to \"" + e.IntegerValue + "\".");
                }
            }
        }

        private void ConfigOptions_ChoiceChanged(object sender, SMLHelper.V2.Options.ChoiceChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e?.Id) && !string.IsNullOrEmpty(e.Value))
            {
                int intVal = -1;
                if (e.Id == "DecorationsLanguage")
                {
                    LanguageHelper.UserLanguage = RegionHelper.GetCountryCodeFromLabel(e.Value);
                    UpdateConfigFile("\r\nlanguage=\r\n", "\r\nlanguage=" + e.Value + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to switch language to \"" + e.Value + "\".");
                }
                else if (e.Id == "PrecursorKeysResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal > 0 && intVal < 6)
                {
                    ConfigSwitcher.PrecursorKeysResourceAmount = intVal;
                    UpdateConfigFile("\r\nprecursorKeys_RecipiesResourceAmount=\r\n", "\r\nprecursorKeys_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "RelicRecipiesResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal > 0 && intVal < 6)
                {
                    ConfigSwitcher.RelicRecipiesResourceAmount = intVal;
                    UpdateConfigFile("\r\nrelics_RecipiesResourceAmount=\r\n", "\r\nrelics_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "CreatureEggsResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal > 0 && intVal < 6)
                {
                    ConfigSwitcher.CreatureEggsResourceAmount = intVal;
                    UpdateConfigFile("\r\ncreatureEggs_RecipiesResourceAmount=\r\n", "\r\ncreatureEggs_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "FloraRecipiesResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal > 0 && intVal < 6)
                {
                    ConfigSwitcher.FloraRecipiesResourceAmount = intVal;
                    UpdateConfigFile("\r\nflora_RecipiesResourceAmount=\r\n", "\r\nflora_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "GhostLeviatan_maxSpawns" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal > 0 && intVal < 11)
                {
                    ConfigSwitcher.GhostLeviatan_maxSpawns = intVal;
                    UpdateConfigFile("\r\nGhostLeviatan_maxSpawns=\r\n", "\r\nGhostLeviatan_maxSpawns=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Amount of spawning leviathans before eggs disappears set to \"" + intVal.ToString(CultureInfo.InvariantCulture) + "\".");
                }
            }
        }
    }
}
