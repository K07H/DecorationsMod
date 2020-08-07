using QModManager.API;
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
        public static readonly string[] LanguageStrings = new string[4]
        {
            "Config_OpenDecorationsModConfigurator",
            "Config_UseCompactTooltips",
            "Config_LockQuickslotsWhenPlacingItem",
            "Config_HideDeepGrandReefDegasiBase"
        };

        public ConfigOptions(string name) : base(name)
        {
        }

        public override void BuildModOptions()
        {
            this.AddButtonOption("OpenDecorationsModConfigurator", "Config_OpenDecorationsModConfigurator");

            this.AddToggleOption("UseCompactTooltips", "Config_UseCompactTooltips", ConfigSwitcher.UseCompactTooltips);
            this.AddToggleOption("LockQuickslotsWhenPlacingItem", "Config_LockQuickslotsWhenPlacingItem", ConfigSwitcher.LockQuickslotsWhenPlacingItem);
            this.AddToggleOption("HideDeepGrandReefDegasiBase", "Config_HideDeepGrandReefDegasiBase", ConfigSwitcher.HideDeepGrandReefDegasiBase);

            this.ButtonClicked += ConfigOptions_ButtonClicked;
            this.ToggleChanged += ConfigOptions_ToggleChanged;
        }

        private void ConfigOptions_ButtonClicked(object sender, ButtonClickedEventArgs e)
        {
            if (e.Id == "OpenDecorationsModConfigurator")
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
                            MenuMessageHelper.AddMessage("Could not open configurator. Try to open it from DecorationsMod folder or edit Config text file manually.", "orange", 22);
                        }
                    }
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
                            ConfigSwitcher.UpdateConfigFile(Environment.NewLine + "useCompactTooltips=" + (e.Value ? "false" : "true") + Environment.NewLine, Environment.NewLine + "useCompactTooltips=" + (e.Value ? "true" : "false") + Environment.NewLine);
                            MenuMessageHelper.AddMessage("Compact tooltips " + (e.Value ? "enabled" : "disabled") + ".", e.Value ? "green" : "orange");
                        }
                        break;
                    case "LockQuickslotsWhenPlacingItem":
                        if (e.Value != ConfigSwitcher.LockQuickslotsWhenPlacingItem)
                        {
                            ConfigSwitcher.LockQuickslotsWhenPlacingItem = e.Value;
                            ConfigSwitcher.UpdateConfigFile(Environment.NewLine + "lockQuickslotsWhenPlacingItem=" + (e.Value ? "false" : "true") + Environment.NewLine, Environment.NewLine + "lockQuickslotsWhenPlacingItem=" + (e.Value ? "true" : "false") + Environment.NewLine);
                            MenuMessageHelper.AddMessage("Lock quickslots when placing item " + (e.Value ? "enabled" : "disabled") + ".", e.Value ? "green" : "orange");
                        }
                        break;
                    case "HideDeepGrandReefDegasiBase":
                        if (e.Value != ConfigSwitcher.HideDeepGrandReefDegasiBase)
                        {
                            ConfigSwitcher.HideDeepGrandReefDegasiBase = e.Value;
                            ConfigSwitcher.UpdateConfigFile(Environment.NewLine + "hideDeepGrandReefDegasiBase=" + (e.Value ? "false" : "true") + Environment.NewLine, Environment.NewLine + "hideDeepGrandReefDegasiBase=" + (e.Value ? "true" : "false") + Environment.NewLine);
                            PrefabsHelper.HideDegasiBase();
                            MenuMessageHelper.AddMessage("Hide Degasi base (500m) structure " + (e.Value ? "enabled" : "disabled") + ".", e.Value ? "green" : "orange");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
