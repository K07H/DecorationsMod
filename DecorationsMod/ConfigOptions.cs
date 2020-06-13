using SMLHelper.V2.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DecorationsMod
{
    class ConfigOptions : ModOptions
    {
        /// <summary>List of configuration option language strings.</summary>
        public static readonly string[] LanguageStrings = new string[3]
        {
            "Config_OpenDecorationsModConfigurator",
            "Config_UseCompactTooltips",
            "Config_LockQuickslotsWhenPlacingItem"
        };

        public ConfigOptions(string name) : base(name)
        {
        }

        public override void BuildModOptions()
        {
            // TODO: Use line below when button feature added to SML Helper.
            //this.AddButtonOption("OpenDecorationsModConfigurator", "Config_OpenDecorationsModConfigurator", ConfigSwitcher.OpenDecorationsModConfigurator);
            this.AddToggleOption("OpenDecorationsModConfigurator", "Config_OpenDecorationsModConfigurator", ConfigSwitcher.OpenDecorationsModConfigurator);
            this.AddToggleOption("UseCompactTooltips", "Config_UseCompactTooltips", ConfigSwitcher.UseCompactTooltips);
            this.AddToggleOption("LockQuickslotsWhenPlacingItem", "Config_LockQuickslotsWhenPlacingItem", ConfigSwitcher.LockQuickslotsWhenPlacingItem);

            // TODO: Use line below when button feature added to SML Helper.
            //this.ButtonClicked += ConfigOptions_ButtonClicked;
            this.ToggleChanged += ConfigOptions_ToggleChanged;

            // TODO: Remove line below when button feature added to SML Helper.
            this.GameObjectCreated += ConfigOptions_GameObjectCreated;
        }

        // TODO: Use method below when button feature added to SML Helper.
        /*
        private void ConfigOptions_ButtonClicked(object sender, ButtonClickedEventArgs e)
        {
            if (e.Id == "OpenDecoModConfigurator")
            {
                ConfigSwitcher.OpenDecorationsModConfigurator = !ConfigSwitcher.OpenDecorationsModConfigurator;
                // If button state changed
                if (ConfigSwitcher.OpenConfiguratorLastState != ConfigSwitcher.OpenDecorationsModConfigurator)
                {
                    // Update button state
                    ConfigSwitcher.OpenConfiguratorLastState = ConfigSwitcher.OpenDecorationsModConfigurator;
                    // Open configurator
                    string configuratorPath = ConfiguratorPath();
                    if (File.Exists(configuratorPath))
                    {
                        // Try launch configurator
                        try { Configurator = Process.Start(new ProcessStartInfo { Arguments = "/C \"" + configuratorPath + "\"", FileName = "cmd", WindowStyle = ProcessWindowStyle.Hidden }); }
                        catch (Exception ex)
                        {
                            // Cleanup any running instance on failure
                            if (Configurator != null)
                            {
                                if (!Configurator.HasExited)
                                {
                                    try { Configurator.CloseMainWindow(); }
                                    catch { }
                                }
                                try { Configurator.Close(); }
                                catch { }
                            }
                            // Log error
                            Logger.Log("ERROR: Unable to open configurator. Exception=[" + ex.ToString() + "]");
                        }
                    }
                }
            }
        }
        */

        public void UpdateConfigFile(string oldStr, string newStr)
        {
            if (!string.IsNullOrEmpty(oldStr) && !string.IsNullOrEmpty(newStr) && !string.IsNullOrEmpty(ConfigSwitcher.configFilePath) && File.Exists(ConfigSwitcher.configFilePath))
            {
                try
                {
                    string currentConfig = File.ReadAllText(ConfigSwitcher.configFilePath, Encoding.UTF8);
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
                                    Logger.Log("INFO: Replacing configuration [" + currentVal + "] by [" + newStr + "].");
                                    newConfig = currentConfig.Replace(currentVal, newStr);
                                }
                            }
                        }
                        else
                            newConfig = currentConfig.Replace(oldStr, newStr);
                        if (newConfig != null)
                            File.WriteAllText(ConfigSwitcher.configFilePath, newConfig, Encoding.UTF8);
                    }
                }
                catch
                {
                    string error = "An error happened while updating config file at \"" + ConfigSwitcher.configFilePath + "\"!";
                    Logger.Log("ERROR: " + error);
                    MenuMessageHelper.AddMessage(error, "red");
                }
            }
        }

        /// <summary>Holds the Configurator stub process.</summary>
        private static Process Configurator { get; set; }

        /// <summary>Returns the path to Decorations Mod configuration tool executable.</summary>
        private static string ConfiguratorPath() => Path.Combine(@".\QMods\DecorationsMod\Configurator\", "DecorationsModConfigurator.exe");

        /// <summary>This method gets called when a toggle value changes.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The toggle change event properties (contains ID and value of the toggle).</param>
        private void ConfigOptions_ToggleChanged(object sender, ToggleChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e?.Id))
            {
                switch (e.Id)
                {
                    case "OpenDecorationsModConfigurator":
                        break;
                    case "UseCompactTooltips":
                        if (e.Value != ConfigSwitcher.UseCompactTooltips)
                        {
                            ConfigSwitcher.UseCompactTooltips = e.Value;
                            UpdateConfigFile("\r\nuseCompactTooltips=" + (e.Value ? "false" : "true") + "\r\n", "\r\nuseCompactTooltips=" + (e.Value ? "true" : "false") + "\r\n");
                            MenuMessageHelper.AddMessage("Compact tooltips " + (e.Value ? "enabled" : "disabled") + ".", e.Value ? "green" : "orange");
                        }
                        break;
                    case "LockQuickslotsWhenPlacingItem":
                        if (e.Value != ConfigSwitcher.LockQuickslotsWhenPlacingItem)
                        {
                            ConfigSwitcher.LockQuickslotsWhenPlacingItem = e.Value;
                            UpdateConfigFile("\r\nlockQuickslotsWhenPlacingItem=" + (e.Value ? "false" : "true") + "\r\n", "\r\nlockQuickslotsWhenPlacingItem=" + (e.Value ? "true" : "false") + "\r\n");
                            MenuMessageHelper.AddMessage("Lock quickslots when placing item " + (e.Value ? "enabled" : "disabled") + ".", e.Value ? "green" : "orange");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // TODO: Remove lines below when button feature added to SML Helper.

        private static Type _tabType = typeof(uGUI_TabbedControlsPanel).GetNestedType("Tab", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _tabsField = typeof(uGUI_TabbedControlsPanel).GetField("tabs", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _tabField = _tabType.GetField("tab", BindingFlags.Public | BindingFlags.Instance);
        private static FieldInfo _paneField = _tabType.GetField("pane", BindingFlags.Public | BindingFlags.Instance);
        private static FieldInfo _containerField = _tabType.GetField("container", BindingFlags.Public | BindingFlags.Instance);

        public static RectTransform OpenConfiguratorBtnContainer = null;

        public void ConfigOptions_GameObjectCreated(object sender, GameObjectCreatedEventArgs e)
        {
            GameObject go = e.GameObject;

            if (e.Id == "OpenDecorationsModConfigurator" && go != null)
            {
                uGUI_TabbedControlsPanel tcp = null;
                try { tcp = go.transform.parent.parent.parent.parent.parent.GetComponent<uGUI_TabbedControlsPanel>(); }
                catch { tcp = null; }
                if (tcp != null)
                {
                    ICollection tabs = _tabsField.GetValue(tcp) as ICollection;
                    if (tabs != null)
                    {
                        IEnumerator enumerator = tabs.GetEnumerator();
                        if (enumerator != null)
                        {
                            int i = 0;
                            // Iterate through each panel tabs
                            while (enumerator.MoveNext())
                            {
                                object currentTab = enumerator.Current;
                                GameObject pane = (GameObject)_paneField.GetValue(currentTab);
                                GameObject tab = (GameObject)_tabField.GetValue(currentTab);
                                OpenConfiguratorBtnContainer = (RectTransform)_containerField.GetValue(currentTab);
                                if (pane != null && tab != null && OpenConfiguratorBtnContainer != null)
                                {
                                    Text paneText = pane.GetComponentInChildren<Text>();
                                    Text tabText = tab.GetComponentInChildren<Text>();
                                    // If current panel is "QModManager" and current tab is "Mods"
                                    if (paneText != null && string.Compare(paneText.text, "QModManager", false, CultureInfo.InvariantCulture) == 0 &&
                                        tabText != null && string.Compare(tabText.text, "Mods", false, CultureInfo.InvariantCulture) == 0)
                                    {
                                        // Detroy toggle
                                        GameObject.DestroyImmediate(go);
                                        // Add button
                                        tcp.AddButton(i, LanguageHelper.GetFriendlyWord("Config_OpenDecorationsModConfigurator"), new UnityEngine.Events.UnityAction(() => {
                                            ConfigSwitcher.OpenDecorationsModConfigurator = !ConfigSwitcher.OpenDecorationsModConfigurator;
                                            // If button state changed
                                            if (ConfigSwitcher.OpenConfiguratorLastState != ConfigSwitcher.OpenDecorationsModConfigurator)
                                            {
                                                // Update button state
                                                ConfigSwitcher.OpenConfiguratorLastState = ConfigSwitcher.OpenDecorationsModConfigurator;
                                                // Open configurator
                                                string configuratorPath = ConfiguratorPath();
                                                if (File.Exists(configuratorPath))
                                                {
                                                    // Try launch configurator
                                                    try { Configurator = Process.Start(new ProcessStartInfo { Arguments = "/C \"" + configuratorPath + "\"", FileName = "cmd", WindowStyle = ProcessWindowStyle.Hidden }); }
                                                    catch (Exception ex)
                                                    {
                                                        // Cleanup any running instance on failure
                                                        if (Configurator != null)
                                                        {
                                                            if (!Configurator.HasExited)
                                                            {
                                                                try { Configurator.CloseMainWindow(); }
                                                                catch { }
                                                            }
                                                            try { Configurator.Close(); }
                                                            catch { }
                                                        }
                                                        // Log error
                                                        Logger.Log("ERROR: Unable to open configurator. Exception=[" + ex.ToString() + "]");
                                                    }
                                                }
                                                // Apply "deselected" style to button
                                                foreach (Transform tr in OpenConfiguratorBtnContainer)
                                                {
                                                    // If current transform is GUI button
                                                    if (tr != null && !string.IsNullOrEmpty(tr.name) && tr.name.StartsWith("uGUI_OptionButton"))
                                                    {
                                                        Text btnText = tr.GetComponentInChildren<Text>();
                                                        if (btnText != null && !string.IsNullOrEmpty(btnText.text) &&
                                                            (string.Compare(btnText.text, "Cliquez ici pour configurer", true, CultureInfo.InvariantCulture) == 0 ||
                                                             string.Compare(btnText.text, "Haga clic aquí para configurar", true, CultureInfo.InvariantCulture) == 0 ||
                                                             string.Compare(btnText.text, "Yapılandırmak için burayı tıklayın", true, CultureInfo.InvariantCulture) == 0 ||
                                                             string.Compare(btnText.text, "Klicken Sie hier zum Konfigurieren", true, CultureInfo.InvariantCulture) == 0 ||
                                                             string.Compare(btnText.text, "Нажмите здесь, чтобы настроить", true, CultureInfo.InvariantCulture) == 0 ||
                                                             string.Compare(btnText.text, "Click here to configure", true, CultureInfo.InvariantCulture) == 0))
                                                        {
                                                            // Deselect button
                                                            Button btn = tr.GetComponentInChildren<Button>();
                                                            if (btn != null)
                                                            {
                                                                btn.OnDeselect(null);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }));
                                        break;
                                    }
                                }
                                ++i;
                            }
                        }
                    }
                }
            }
        }

        #region Old stuff

        /*"Config_UseFlatScreenResolution",
        "Config_AllowBuildOutside",
        "Config_AllowPlaceOutside",
        "Config_EnableDiscoveryMode",
        "Config_EnableSofas",
        "Config_EnablePlaceItems",
        "Config_EnablePlaceBatteries",
        "Config_EnableNewItems",
        "Config_EnableNewFlora",
        "Config_EnablePrecursorTab",
        "Config_PrecursorKeysAll",
        "Config_EnableRegularEggs",
        "Config_EnableEggsWhenCreatureScanned",
        "Config_EnableEggsAtStart",
        "Config_EnableNutrientBlock",
        "Config_EnableRegularAirSeeds",
        "Config_EnableRegularWaterSeeds",
        "Config_AddRegularAirSeedsWhenDiscovered",
        "Config_AddRegularWaterSeedsWhenDiscovered",
        "Config_AllowIndoorLongPlanterOutside",
        "Config_AllowOutdoorLongPlanterInside",
        "Config_FixAquariumLighting",
        "Config_GlowingAquariumGlass",
        "Config_PrecursorKeysResourceAmount",
        "Config_RelicRecipiesResourceAmount",
        "Config_CreatureEggsResourceAmount",
        "Config_FloraRecipiesResourceAmount",
        "Config_PurplePineconeDroppedResourceAmount",
        "Config_BarBottle1_water",
        "Config_BarBottle2_water",
        "Config_BarBottle3_water",
        "Config_BarBottle4_water",
        "Config_BarBottle5_water",
        "Config_BarFood1_nutrient",
        "Config_BarFood1_water",
        "Config_BarFood2_nutrient",
        "Config_BarFood2_water",
        "Config_GhostLeviatan_enable",
        "Config_GhostLeviatan_maxSpawns",
        "Config_GhostLeviatan_timeBeforeFirstSpawn",
        "Config_GhostLeviatan_spawnTimeRatio",
        "Config_GhostLeviatan_health"*/

        /*
        this.AddChoiceOption("DecorationsLanguage", "Language", RegionHelper.AvailableLanguages, RegionHelper.GetCountryLabelFromCode(LanguageHelper.UserLanguage));
        this.AddToggleOption("UseFlatScreenResolution", "Config_UseFlatScreenResolution", ConfigSwitcher.UseFlatScreenResolution);
        this.AddToggleOption("AllowBuildOutside", "Config_AllowBuildOutside", ConfigSwitcher.AllowBuildOutside);
        this.AddToggleOption("AllowPlaceOutside", "Config_AllowPlaceOutside", ConfigSwitcher.AllowPlaceOutside);
        this.AddToggleOption("EnableDiscoveryMode", "Config_EnableDiscoveryMode", ConfigSwitcher.AddItemsWhenDiscovered);
        this.AddToggleOption("EnableSofas", "Config_EnableSofas", ConfigSwitcher.EnableSofas);
        this.AddToggleOption("EnablePlaceItems", "Config_EnablePlaceItems", ConfigSwitcher.EnablePlaceItems);
        this.AddToggleOption("EnablePlaceBatteries", "Config_EnablePlaceBatteries", ConfigSwitcher.EnablePlaceBatteries);
        this.AddToggleOption("EnableNewItems", "Config_EnableNewItems", ConfigSwitcher.EnableNewItems);
        this.AddToggleOption("EnableNewFlora", "Config_EnableNewFlora", ConfigSwitcher.EnableNewFlora);
        this.AddToggleOption("EnablePrecursorTab", "Config_EnablePrecursorTab", ConfigSwitcher.EnablePrecursorTab);
        this.AddToggleOption("PrecursorKeysAll", "Config_PrecursorKeysAll", ConfigSwitcher.PrecursorKeysAll);
        this.AddToggleOption("EnableRegularEggs", "Config_EnableRegularEggs", ConfigSwitcher.EnableRegularEggs);
        this.AddToggleOption("EnableEggsWhenCreatureScanned", "Config_EnableEggsWhenCreatureScanned", ConfigSwitcher.EnableEggsWhenCreatureScanned);
        this.AddToggleOption("EnableEggsAtStart", "Config_EnableEggsAtStart", ConfigSwitcher.EnableEggsAtStart);
        this.AddToggleOption("EnableNutrientBlock", "Config_EnableNutrientBlock", ConfigSwitcher.EnableNutrientBlock);
        this.AddToggleOption("EnableRegularAirSeeds", "Config_EnableRegularAirSeeds", ConfigSwitcher.EnableRegularAirSeeds);
        this.AddToggleOption("EnableRegularWaterSeeds", "Config_EnableRegularWaterSeeds", ConfigSwitcher.EnableRegularWaterSeeds);
        this.AddToggleOption("AddRegularAirSeedsWhenDiscovered", "Config_AddRegularAirSeedsWhenDiscovered", ConfigSwitcher.AddAirSeedsWhenDiscovered);
        this.AddToggleOption("AddRegularWaterSeedsWhenDiscovered", "Config_AddRegularWaterSeedsWhenDiscovered", ConfigSwitcher.AddWaterSeedsWhenDiscovered);
        this.AddToggleOption("AllowIndoorLongPlanterOutside", "Config_AllowIndoorLongPlanterOutside", ConfigSwitcher.AllowIndoorLongPlanterOutside);
        this.AddToggleOption("AllowOutdoorLongPlanterInside", "Config_AllowOutdoorLongPlanterInside", ConfigSwitcher.AllowOutdoorLongPlanterInside);
        this.AddToggleOption("FixAquariumLighting", "Config_FixAquariumLighting", ConfigSwitcher.FixAquariumLighting);
        this.AddToggleOption("GlowingAquariumGlass", "Config_GlowingAquariumGlass", ConfigSwitcher.GlowingAquariumGlass);

        this.AddChoiceOption("PrecursorKeysResourceAmount", "Config_PrecursorKeysResourceAmount", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.PrecursorKeysResourceAmount > 10 ? "10" : (ConfigSwitcher.PrecursorKeysResourceAmount < 1 ? "1" : ConfigSwitcher.PrecursorKeysResourceAmount.ToString(CultureInfo.InvariantCulture)));
        this.AddChoiceOption("RelicRecipiesResourceAmount", "Config_RelicRecipiesResourceAmount", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.RelicRecipiesResourceAmount > 10 ? "10" : (ConfigSwitcher.RelicRecipiesResourceAmount < 1 ? "1" : ConfigSwitcher.RelicRecipiesResourceAmount.ToString(CultureInfo.InvariantCulture)));
        this.AddChoiceOption("CreatureEggsResourceAmount", "Config_CreatureEggsResourceAmount", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.CreatureEggsResourceAmount > 10 ? "10" : (ConfigSwitcher.CreatureEggsResourceAmount < 1 ? "1" : ConfigSwitcher.CreatureEggsResourceAmount.ToString(CultureInfo.InvariantCulture)));
        this.AddChoiceOption("FloraRecipiesResourceAmount", "Config_FloraRecipiesResourceAmount", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.FloraRecipiesResourceAmount > 10 ? "10" : (ConfigSwitcher.FloraRecipiesResourceAmount < 1 ? "1" : ConfigSwitcher.FloraRecipiesResourceAmount.ToString(CultureInfo.InvariantCulture)));

        this.AddChoiceOption("PurplePineconeDroppedResourceAmount", "Config_PurplePineconeDroppedResourceAmount", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.PurplePineconeDroppedResourceAmount > 10 ? "10" : (ConfigSwitcher.PurplePineconeDroppedResourceAmount < 1 ? "1" : ConfigSwitcher.PurplePineconeDroppedResourceAmount.ToString(CultureInfo.InvariantCulture)));

        this.AddSliderOption("BarBottle1_water", "Config_BarBottle1_water", 1.0f, 100.0f, ConfigSwitcher.BarBottle1Value > 100.0f ? 100.0f : (ConfigSwitcher.BarBottle1Value < 1.0f ? 1.0f : ConfigSwitcher.BarBottle1Value));
        this.AddSliderOption("BarBottle2_water", "Config_BarBottle2_water", 1.0f, 100.0f, ConfigSwitcher.BarBottle2Value > 100.0f ? 100.0f : (ConfigSwitcher.BarBottle2Value < 1.0f ? 1.0f : ConfigSwitcher.BarBottle2Value));
        this.AddSliderOption("BarBottle3_water", "Config_BarBottle3_water", 1.0f, 100.0f, ConfigSwitcher.BarBottle3Value > 100.0f ? 100.0f : (ConfigSwitcher.BarBottle3Value < 1.0f ? 1.0f : ConfigSwitcher.BarBottle3Value));
        this.AddSliderOption("BarBottle4_water", "Config_BarBottle4_water", 1.0f, 100.0f, ConfigSwitcher.BarBottle4Value > 100.0f ? 100.0f : (ConfigSwitcher.BarBottle4Value < 1.0f ? 1.0f : ConfigSwitcher.BarBottle4Value));
        this.AddSliderOption("BarBottle5_water", "Config_BarBottle5_water", 1.0f, 100.0f, ConfigSwitcher.BarBottle5Value > 100.0f ? 100.0f : (ConfigSwitcher.BarBottle5Value < 1.0f ? 1.0f : ConfigSwitcher.BarBottle5Value));
        this.AddSliderOption("BarFood1_nutrient", "Config_BarFood1_nutrient", 1.0f, 100.0f, ConfigSwitcher.BarFood1FoodValue > 100.0f ? 100.0f : (ConfigSwitcher.BarFood1FoodValue < 1.0f ? 1.0f : ConfigSwitcher.BarFood1FoodValue));
        this.AddSliderOption("BarFood1_water", "Config_BarFood1_water", 1.0f, 100.0f, ConfigSwitcher.BarFood1WaterValue > 100.0f ? 100.0f : (ConfigSwitcher.BarFood1WaterValue < 1.0f ? 1.0f : ConfigSwitcher.BarFood1WaterValue));
        this.AddSliderOption("BarFood2_nutrient", "Config_BarFood2_nutrient", 1.0f, 100.0f, ConfigSwitcher.BarFood2FoodValue > 100.0f ? 100.0f : (ConfigSwitcher.BarFood2FoodValue < 1.0f ? 1.0f : ConfigSwitcher.BarFood2FoodValue));
        this.AddSliderOption("BarFood2_water", "Config_BarFood2_water", 1.0f, 100.0f, ConfigSwitcher.BarFood2WaterValue > 100.0f ? 100.0f : (ConfigSwitcher.BarFood2WaterValue < 1.0f ? 1.0f : ConfigSwitcher.BarFood2WaterValue));

        this.AddToggleOption("GhostLeviatan_enable", "Config_GhostLeviatan_enable", ConfigSwitcher.GhostLeviatan_enable);
        this.AddChoiceOption("GhostLeviatan_maxSpawns", "Config_GhostLeviatan_maxSpawns", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ConfigSwitcher.GhostLeviatan_maxSpawns > 10 ? "10" : (ConfigSwitcher.GhostLeviatan_maxSpawns < 1 ? "1" : ConfigSwitcher.GhostLeviatan_maxSpawns.ToString(CultureInfo.InvariantCulture)));
        this.AddSliderOption("GhostLeviatan_timeBeforeFirstSpawn", "Config_GhostLeviatan_timeBeforeFirstSpawn", 10.0f, 14400.0f, ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn > 14400.0f ? 14400.0f : (ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn < 10.0f ? 10.0f : ConfigSwitcher.GhostLeviatan_timeBeforeFirstSpawn));
        this.AddSliderOption("GhostLeviatan_spawnTimeRatio", "Config_GhostLeviatan_spawnTimeRatio", 0.01f, 10.0f, ConfigSwitcher.GhostLeviatan_spawnTimeRatio > 10.0f ? 10.0f : (ConfigSwitcher.GhostLeviatan_spawnTimeRatio < 0.01f ? 0.01f : ConfigSwitcher.GhostLeviatan_spawnTimeRatio));
        this.AddSliderOption("GhostLeviatan_health", "Config_GhostLeviatan_health", 10.0f, 20000.0f, ConfigSwitcher.GhostLeviatan_health > 20000.0f ? 20000.0f : (ConfigSwitcher.GhostLeviatan_health < 10.0f ? 10.0f : ConfigSwitcher.GhostLeviatan_health));


        this.ChoiceChanged += ConfigOptions_ChoiceChanged;
        this.SliderChanged += ConfigOptions_SliderChanged;
        */

        /*case "UseFlatScreenResolution":
            ConfigSwitcher.UseFlatScreenResolution = e.Value;
            UpdateConfigFile("\r\nuseAlternativeScreenResolution=" + (e.Value ? "false" : "true") + "\r\n", "\r\nuseAlternativeScreenResolution=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " alternate screen resolution.");
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
        case "EnableDiscoveryMode":
            ConfigSwitcher.AddItemsWhenDiscovered = e.Value;
            UpdateConfigFile("\r\naddItemsWhenDiscovered=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddItemsWhenDiscovered=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " adding items when discovered.");
            break;
        case "EnableSofas":
            ConfigSwitcher.EnableSofas = e.Value;
            UpdateConfigFile("\r\nenableSofas=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenableSofas=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " sofas.");
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
        case "EnableNewItems":
            ConfigSwitcher.EnableNewItems = e.Value;
            UpdateConfigFile("\r\nenableNewBuilderItems=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenableNewBuilderItems=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " new builder items.");
            break;
        case "EnableNewFlora":
            ConfigSwitcher.EnableNewFlora = e.Value;
            UpdateConfigFile("\r\nenableNewFlora=" + (e.Value ? "false" : "true") + "\r\n", "\r\nenableNewFlora=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " new flora.");
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
        case "EnableEggsWhenCreatureScanned":
            ConfigSwitcher.EnableEggsWhenCreatureScanned = e.Value;
            UpdateConfigFile("\r\naddEggsWhenCreatureScanned=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddEggsWhenCreatureScanned=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " eggs unlocking when discovered.");
            break;
        case "EnableEggsAtStart":
            ConfigSwitcher.EnableEggsAtStart = e.Value;
            UpdateConfigFile("\r\naddEggsAtStart=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddEggsAtStart=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " eggs unlocking at start.");
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
        case "AddRegularAirSeedsWhenDiscovered":
            ConfigSwitcher.AddAirSeedsWhenDiscovered = e.Value;
            UpdateConfigFile("\r\naddAirSeedsWhenDiscovered=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddAirSeedsWhenDiscovered=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " unlocking air seeds when discovered.");
            break;
        case "EnableRegularWaterSeeds":
            ConfigSwitcher.EnableRegularWaterSeeds = e.Value;
            UpdateConfigFile("\r\naddRegularWaterSeeds=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddRegularWaterSeeds=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " regular water seeds.");
            break;
        case "AddRegularWaterSeedsWhenDiscovered":
            ConfigSwitcher.AddWaterSeedsWhenDiscovered = e.Value;
            UpdateConfigFile("\r\naddWaterSeedsWhenDiscovered=" + (e.Value ? "false" : "true") + "\r\n", "\r\naddWaterSeedsWhenDiscovered=" + (e.Value ? "true" : "false") + "\r\n");
            ErrorMessage.AddMessage("Restart game completely to " + (e.Value ? "enable" : "disable") + " unlocking water seeds when discovered.");
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
        *//*
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

        /*
        private void ConfigOptions_SliderChanged(object sender, SMLHelper.V2.Options.SliderChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e?.Id))
            {
                if (e.Id == "BarBottle1_water")
                {
                    ConfigSwitcher.BarBottle1Value = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarBottle1Water=\r\n", "\r\nbarBottle1Water=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar bottle 1 water value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarBottle2_water")
                {
                    ConfigSwitcher.BarBottle2Value = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarBottle2Water=\r\n", "\r\nbarBottle2Water=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar bottle 2 water value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarBottle3_water")
                {
                    ConfigSwitcher.BarBottle3Value = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarBottle3Water=\r\n", "\r\nbarBottle3Water=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar bottle 3 water value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarBottle4_water")
                {
                    ConfigSwitcher.BarBottle4Value = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarBottle4Water=\r\n", "\r\nbarBottle4Water=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar bottle 4 water value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarBottle5_water")
                {
                    ConfigSwitcher.BarBottle5Value = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarBottle5Water=\r\n", "\r\nbarBottle5Water=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar bottle 5 water value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarFood1_nutrient")
                {
                    ConfigSwitcher.BarFood1FoodValue = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarFood1Nutrient=\r\n", "\r\nbarFood1Nutrient=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar food 1 nutrient value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarFood1_water")
                {
                    ConfigSwitcher.BarFood1WaterValue = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarFood1Water=\r\n", "\r\nbarFood1Water=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar food 1 water value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarFood2_nutrient")
                {
                    ConfigSwitcher.BarFood2FoodValue = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarFood2Nutrient=\r\n", "\r\nbarFood2Nutrient=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar food 2 nutrient value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "BarFood2_water")
                {
                    ConfigSwitcher.BarFood2WaterValue = (float)e.IntegerValue;
                    UpdateConfigFile("\r\nbarFood2Water=\r\n", "\r\nbarFood2Water=" + e.IntegerValue.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Bar food 2 water value set to \"" + e.IntegerValue + "\".");
                }
                else if (e.Id == "GhostLeviatan_timeBeforeFirstSpawn")
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
        */
        /*
        private void ConfigOptions_ChoiceChanged(object sender, SMLHelper.V2.Options.ChoiceChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e?.Id) && !string.IsNullOrEmpty(e.Value))
            {
                int intVal = -1;
                if (e.Id == "DecorationsLanguage")
                {
                    LanguageHelper.UserLanguage = RegionHelper.GetCountryCodeFromLabel(e.Value);
                    UpdateConfigFile("\r\nlanguage=\r\n", "\r\nlanguage=" + e.Value + "\r\n");
                    DecorationsMod.RegisterLanguageStrings(); // Re-register language strings (because language changed).
                    ErrorMessage.AddMessage("Restart game completely to change language to \"" + e.Value + "\".");
                }
                else if (e.Id == "PrecursorKeysResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal >= 1 && intVal <= 10)
                {
                    ConfigSwitcher.PrecursorKeysResourceAmount = intVal;
                    UpdateConfigFile("\r\nprecursorKeys_RecipiesResourceAmount=\r\n", "\r\nprecursorKeys_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "RelicRecipiesResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal >= 1 && intVal <= 10)
                {
                    ConfigSwitcher.RelicRecipiesResourceAmount = intVal;
                    UpdateConfigFile("\r\nrelics_RecipiesResourceAmount=\r\n", "\r\nrelics_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "CreatureEggsResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal >= 1 && intVal <= 10)
                {
                    ConfigSwitcher.CreatureEggsResourceAmount = intVal;
                    UpdateConfigFile("\r\ncreatureEggs_RecipiesResourceAmount=\r\n", "\r\ncreatureEggs_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "FloraRecipiesResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal >= 1 && intVal <= 10)
                {
                    ConfigSwitcher.FloraRecipiesResourceAmount = intVal;
                    UpdateConfigFile("\r\nflora_RecipiesResourceAmount=\r\n", "\r\nflora_RecipiesResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "PurplePineconeDroppedResourceAmount" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal >= 1 && intVal <= 10)
                {
                    ConfigSwitcher.PurplePineconeDroppedResourceAmount = intVal;
                    UpdateConfigFile("\r\npurplePineconeDroppedResourceAmount=\r\n", "\r\npurplePineconeDroppedResourceAmount=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Restart game completely to take new amount into account.");
                }
                else if (e.Id == "GhostLeviatan_maxSpawns" && int.TryParse(e.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out intVal) && intVal >= 1 && intVal <= 10)
                {
                    ConfigSwitcher.GhostLeviatan_maxSpawns = intVal;
                    UpdateConfigFile("\r\nGhostLeviatan_maxSpawns=\r\n", "\r\nGhostLeviatan_maxSpawns=" + intVal.ToString(CultureInfo.InvariantCulture) + "\r\n");
                    ErrorMessage.AddMessage("Amount of spawning leviathans before eggs disappears set to \"" + intVal.ToString(CultureInfo.InvariantCulture) + "\".");
                }
            }
        }
        */
        /*
        private class Tooltip : MonoBehaviour, ITooltip
        {
            public string tooltip;

            public void Start() => Destroy(gameObject.GetComponent<LayoutElement>());

            public void GetTooltip(out string tooltipText, List<TooltipIcon> _) { tooltipText = tooltip; }
        }
        */

        #endregion
    }
}
