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
            // TODO: Use line below when button feature added to SML Helper.
            //this.AddButtonOption("OpenDecorationsModConfigurator", "Config_OpenDecorationsModConfigurator", ConfigSwitcher.OpenDecorationsModConfigurator);
            this.AddToggleOption("OpenDecorationsModConfigurator", "Config_OpenDecorationsModConfigurator", ConfigSwitcher.OpenDecorationsModConfigurator);
            this.AddToggleOption("UseCompactTooltips", "Config_UseCompactTooltips", ConfigSwitcher.UseCompactTooltips);
            this.AddToggleOption("LockQuickslotsWhenPlacingItem", "Config_LockQuickslotsWhenPlacingItem", ConfigSwitcher.LockQuickslotsWhenPlacingItem);
            this.AddToggleOption("HideDeepGrandReefDegasiBase", "Config_HideDeepGrandReefDegasiBase", ConfigSwitcher.HideDeepGrandReefDegasiBase);

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

        private static readonly List<KeyValuePair<string, Vector3>> DegasiBaseParts = new List<KeyValuePair<string, Vector3>>(new KeyValuePair<string, Vector3>[23]
        {
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-641.4045f, -505.624f, -939.858f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-641.4045f, -505.624f, -939.858f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-640.7137f, -506.242f, -939.9625f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-640.7137f, -506.242f, -939.9625f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-640.0577f, -505.959f, -947.2491f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-640.0577f, -505.959f, -947.2491f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-658.4609f, -513.24f, -956.6729f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-635.91f, -512.54f, -951.6974f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-647.5078f, -507.28f, -940.6493f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -509.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -509.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-635.91f, -502.04f, -951.6974f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-647.97f, -502.04f, -935.7426f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-633.9626f, -512.54f, -937.6899f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -512.54f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -502.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Placeholder)", new Vector3(-641.4045f, -505.624f, -939.858f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Placeholder)", new Vector3(-640.7137f, -506.242f, -939.9625f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Placeholder)", new Vector3(-640.0577f, -505.959f, -947.2491f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -505.54f, -943.72f)),
            new KeyValuePair<string, Vector3>("BaseCell", new Vector3(-641.94f, -509.04f, -943.72f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02", new Vector3(-642.637f, -506.784f, -940.607f)),
            new KeyValuePair<string, Vector3>("biodome_lab_containers_open_02(Clone)", new Vector3(-642.637f, -506.784f, -940.607f))
        });

        public static void ApplyDegasiVisibility(GameObject obj)
        {
            Vector3 pos;
            string name;
            try { pos = obj.transform.position; name = obj.name; }
            catch { pos = Vector3.zero; name = null; }
            if (name != null && pos != Vector3.zero)
            {
                foreach (KeyValuePair<string, Vector3> part in DegasiBaseParts)
                    if (part.Key == name && 
                        pos.x > part.Value.x - 1.0f && pos.x < part.Value.x + 1.0f && 
                        pos.y > part.Value.y - 1.0f && pos.y < part.Value.y + 1.0f && 
                        pos.z > part.Value.z - 1.0f && pos.z < part.Value.z + 1.0f)
                    {
                        if ((ConfigSwitcher.HideDeepGrandReefDegasiBase && obj.activeSelf) ||
                            (!ConfigSwitcher.HideDeepGrandReefDegasiBase && !obj.activeSelf))
                            obj.SetActive(!ConfigSwitcher.HideDeepGrandReefDegasiBase);
                    }
            }
        }

        public static void HideDegasiBase()
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
            if (objs != null)
                foreach (GameObject obj in objs)
                    ApplyDegasiVisibility(obj);
        }

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
                    case "HideDeepGrandReefDegasiBase":
                        if (e.Value != ConfigSwitcher.HideDeepGrandReefDegasiBase)
                        {
                            ConfigSwitcher.HideDeepGrandReefDegasiBase = e.Value;
                            UpdateConfigFile("\r\nhideDeepGrandReefDegasiBase=" + (e.Value ? "false" : "true") + "\r\n", "\r\nhideDeepGrandReefDegasiBase=" + (e.Value ? "true" : "false") + "\r\n");
                            ConfigOptions.HideDegasiBase();
                            MenuMessageHelper.AddMessage("Hide Degasi base (500m) structure " + (e.Value ? "enabled" : "disabled") + ".", e.Value ? "green" : "orange");
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
#if SUBNAUTICA
                                    Text paneText = pane.GetComponentInChildren<Text>();
                                    Text tabText = tab.GetComponentInChildren<Text>();
#else
                                    TMPro.TextMeshProUGUI paneText = pane.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                                    TMPro.TextMeshProUGUI tabText = tab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
#endif
                                    // If current panel is "QModManager" and current tab is "Mods"
                                    if (paneText != null && string.Compare(paneText.text, "QModManager", false, CultureInfo.InvariantCulture) == 0 &&
                                        tabText != null && string.Compare(tabText.text, "Mods", false, CultureInfo.InvariantCulture) == 0)
                                    {
                                        // Detroy toggle
                                        GameObject.DestroyImmediate(go);
                                        // Add button
                                        tcp.AddButton(i, LanguageHelper.GetFriendlyWord("Config_OpenDecorationsModConfigurator"), new UnityEngine.Events.UnityAction(() =>
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
                                                // Apply "deselected" style to button
                                                foreach (Transform tr in OpenConfiguratorBtnContainer)
                                                {
                                                    // If current transform is GUI button
                                                    if (tr != null && !string.IsNullOrEmpty(tr.name) && tr.name.StartsWith("uGUI_OptionButton"))
                                                    {
#if SUBNAUTICA
                                                        Text btnText = tr.GetComponentInChildren<Text>();
#else
                                                        TMPro.TextMeshProUGUI btnText = tr.GetComponentInChildren<TMPro.TextMeshProUGUI>();
#endif
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
    }
}
