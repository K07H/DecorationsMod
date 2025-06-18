using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_GeneralSettings.xaml
    /// </summary>
    public partial class UserControl_GeneralSettings : UserControl, INotifyPropertyChanged
    {
        public UserControl_GeneralSettings()
        {
            InitializeComponent();

            try
            {
                this.Lang = Configuration.Instance.Language;
                this.UseCompactTooltips = Configuration.Instance.UseCompactTooltips;
                this.LockQuickslotsWhenPlacingItem = Configuration.Instance.LockQuickslotsWhenPlacingItem;
                this.AllowBuildOutside = Configuration.Instance.AllowBuildOutside;
                this.AllowPlaceOutside = Configuration.Instance.AllowPlaceOutside;
                this.EnablePlaceItems = Configuration.Instance.EnablePlaceItems;
                this.EnablePlaceMaterials = Configuration.Instance.EnablePlaceMaterials;
                this.EnablePlaceBatteries = Configuration.Instance.EnablePlaceBatteries;
                this.EnablePlaceEggs = Configuration.Instance.EnablePlaceEggs;
                this.EnableNewFlora = Configuration.Instance.EnableNewFlora;
                this.FixAquariumLighting = Configuration.Instance.FixAquariumLighting;
                this.EnableAquariumGlassGlowing = Configuration.Instance.EnableAquariumGlassGlowing;
            }
            catch (Exception ex) { Logger.Log("ERROR: Could not load data from configuration. Exception=[" + ex.ToString() + "]"); }

            RefreshMenuVisibilities();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public string Lang
        { 
            get { return Configuration.Instance.Language; } 
            set 
            { 
                Configuration.Instance.Language = value;
                switch (Configuration.Instance.Language)
                {
                    case "de":
                        ComboBox_Language.SelectedIndex = 1; // DE
                        break;
                    case "en":
                        ComboBox_Language.SelectedIndex = 2; // EN
                        break;
                    case "es":
                        ComboBox_Language.SelectedIndex = 3; // ES
                        break;
                    case "fr":
                        ComboBox_Language.SelectedIndex = 4; // FR
                        break;
                    case "nl":
                        ComboBox_Language.SelectedIndex = 5; // NL
                        break;
                    case "ru":
                        ComboBox_Language.SelectedIndex = 6; // RU
                        break;
                    case "tr":
                        ComboBox_Language.SelectedIndex = 7; // TR
                        break;
                    default:
                        ComboBox_Language.SelectedIndex = 0; // AUTO
                        break;
                }
            } 
        }
        public bool UseCompactTooltips { get { return Configuration.Instance.UseCompactTooltips; } set { Configuration.Instance.UseCompactTooltips = value; } }
        public bool LockQuickslotsWhenPlacingItem { get { return Configuration.Instance.LockQuickslotsWhenPlacingItem; } set { Configuration.Instance.LockQuickslotsWhenPlacingItem = value; } }
        public bool AllowBuildOutside { get { return Configuration.Instance.AllowBuildOutside; } set { Configuration.Instance.AllowBuildOutside = value; } }
        public bool AllowPlaceOutside { get { return Configuration.Instance.AllowPlaceOutside; } set { Configuration.Instance.AllowPlaceOutside = value; } }
        public bool EnablePlaceItems { get { return Configuration.Instance.EnablePlaceItems; } set { Configuration.Instance.EnablePlaceItems = value; } }
        public bool EnablePlaceMaterials { get { return Configuration.Instance.EnablePlaceMaterials; } set { Configuration.Instance.EnablePlaceMaterials = value; } }
        public bool EnablePlaceBatteries { get { return Configuration.Instance.EnablePlaceBatteries; } set { Configuration.Instance.EnablePlaceBatteries = value; } }
        public bool EnablePlaceEggs { get { return Configuration.Instance.EnablePlaceEggs; } set { Configuration.Instance.EnablePlaceEggs = value; } }
        public bool EnableNewFlora { get { return Configuration.Instance.EnableNewFlora; } set { Configuration.Instance.EnableNewFlora = value; RefreshMenuVisibilities(); } }
        public bool FixAquariumLighting { get { return Configuration.Instance.FixAquariumLighting; } set { Configuration.Instance.FixAquariumLighting = value; } }
        public bool EnableAquariumGlassGlowing { get { return Configuration.Instance.EnableAquariumGlassGlowing; } set { Configuration.Instance.EnableAquariumGlassGlowing = value; } }

        public string Config_GeneralSettings { get { return LanguageHelper.GetFriendlyWord("Config_GeneralSettings"); } set { } }
        public string Config_Language { get { return LanguageHelper.GetFriendlyWord("Config_Language"); } set { } }
        public string Config_UseCompactTooltips { get { return LanguageHelper.GetFriendlyWord("Config_UseCompactTooltips"); } set { } }
        public string Config_UseCompactTooltipsDescription { get { return LanguageHelper.GetFriendlyWord("Config_UseCompactTooltipsDescription"); } set { } }
        public string Config_LockQuickslotsWhenPlacingItem { get { return LanguageHelper.GetFriendlyWord("Config_LockQuickslotsWhenPlacingItem"); } set { } }
        public string Config_LockQuickslotsWhenPlacingItemDescription { get { return LanguageHelper.GetFriendlyWord("Config_LockQuickslotsWhenPlacingItemDescription"); } set { } }
        public string Config_AllowBuildOutside { get { return LanguageHelper.GetFriendlyWord("Config_AllowBuildOutside"); } set { } }
        public string Config_AllowBuildOutsideDescription { get { return LanguageHelper.GetFriendlyWord("Config_AllowBuildOutsideDescription"); } set { } }
        public string Config_AllowPlaceOutside { get { return LanguageHelper.GetFriendlyWord("Config_AllowPlaceOutside"); } set { } }
        public string Config_AllowPlaceOutsideDescription { get { return LanguageHelper.GetFriendlyWord("Config_AllowPlaceOutsideDescription"); } set { } }
        public string Config_EnablePlaceItems { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceItems"); } set { } }
        public string Config_EnablePlaceItemsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceItemsDescription"); } set { } }
        public string Config_EnablePlaceMaterials { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceMaterials"); } set { } }
        public string Config_EnablePlaceMaterialsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceMaterialsDescription"); } set { } }
        public string Config_EnablePlaceBatteries { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceBatteries"); } set { } }
        public string Config_EnablePlaceBatteriesDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceBatteriesDescription"); } set { } }
        public string Config_EnablePlaceEggs { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceEggs"); } set { } }
        public string Config_EnablePlaceEggsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnablePlaceEggsDescription"); } set { } }
        public string Config_EnableNewFlora { get { return LanguageHelper.GetFriendlyWord("Config_EnableNewFlora"); } set { } }
        public string Config_EnableNewFloraDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableNewFloraDescription"); } set { } }
        public string Config_FixAquariumLighting { get { return LanguageHelper.GetFriendlyWord("Config_FixAquariumLighting"); } set { } }
        public string Config_FixAquariumLightingDescription { get { return LanguageHelper.GetFriendlyWord("Config_FixAquariumLightingDescription"); } set { } }
        public string Config_EnableAquariumGlassGlowing { get { return LanguageHelper.GetFriendlyWord("Config_GlowingAquariumGlass"); } set { } }
        public string Config_EnableAquariumGlassGlowingDescription { get { return LanguageHelper.GetFriendlyWord("Config_GlowingAquariumGlassDescription"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void ComboBox_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedLanguage = ((ComboBoxItem)ComboBox_Language.SelectedItem)?.Name;
            if (!string.IsNullOrEmpty(selectedLanguage) && (selectedLanguage.Length == 7 || selectedLanguage.Length == 9))
                Configuration.Instance.Language = selectedLanguage.Substring(5);
            LanguageHelper.UserLanguage = LanguageHelper.Lang();
            OnPropertyChanged("");
            var mainWindow = GetParentOfType<MainWindow>(ComboBox_Language);
            if (mainWindow != null)
            {
                MainWindow mw = (MainWindow)mainWindow;
                mw.RefreshGUI();
            }
        }

        private UIElement GetParentOfType<T>(DependencyObject obj)
        {
            var parent = VisualTreeHelper.GetParent(obj) as UIElement;
            if (parent == null)
                return null;
            if (parent is T)
                return parent;
            else
                return GetParentOfType<T>(parent);
        }

        public void RefreshMenuVisibilities()
        {
            if (CB_EnableNewFlora != null)
            {
                var parent = GetParentOfType<MainWindow>(CB_EnableNewFlora);
                if (parent != null)
                {
                    DialogHost dh = (DialogHost)((MainWindow)parent).Content;
                    if (dh != null)
                    {
                        Grid grdA = (Grid)(dh.Content);
                        if (grdA != null)
                        {
                            Grid grdB = (Grid)grdA.Children[0];
                            if (grdB != null)
                            {
                                ListView lv = (ListView)grdB.Children[0];
                                if (lv != null && lv.Items != null)
                                {
                                    Visibility visibility = (CB_EnableNewFlora.IsChecked != null && CB_EnableNewFlora.IsChecked.HasValue && CB_EnableNewFlora.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
                                    foreach (ListViewItem lvi in lv.Items)
                                    {
                                        if (string.Compare("MenuBtn_Flora", lvi.Name, false, CultureInfo.InvariantCulture) == 0)
                                            lvi.Visibility = visibility;
                                        else if (string.Compare("MenuBtn_GhostLeviathans", lvi.Name, false, CultureInfo.InvariantCulture) == 0)
                                            lvi.Visibility = visibility;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CB_EnableNewFlora_Checked(object sender, RoutedEventArgs e)
        {
            RefreshMenuVisibilities();
        }

        private void CB_EnableNewFlora_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshMenuVisibilities();
        }

        private void CB_EnablePlaceItems_Checked(object sender, RoutedEventArgs e)
        {
            GRD_EnablePlaceBatteries.Visibility = Visibility.Visible;
            GRD_EnablePlaceMaterials.Visibility = Visibility.Visible;
            GRD_EnablePlaceEggs.Visibility = Visibility.Visible;
        }

        private void CB_EnablePlaceItems_Unchecked(object sender, RoutedEventArgs e)
        {
            GRD_EnablePlaceBatteries.Visibility = Visibility.Collapsed;
            GRD_EnablePlaceMaterials.Visibility = Visibility.Collapsed;
            GRD_EnablePlaceEggs.Visibility = Visibility.Collapsed;
        }
    }
}
