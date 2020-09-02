using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_GhostLeviathans.xaml
    /// </summary>
    public partial class UserControl_GhostLeviathans : UserControl, INotifyPropertyChanged
    {
        public UserControl_GhostLeviathans()
        {
            InitializeComponent();

            this.GhostLeviatan_enable = Configuration.Instance.GhostLeviatan_enable;
            this.GhostLeviatan_health = Configuration.Instance.GhostLeviatan_health;
            this.GhostLeviatan_maxSpawns = Configuration.Instance.GhostLeviatan_maxSpawns;
            this.GhostLeviatan_timeBeforeFirstSpawn = Configuration.Instance.GhostLeviatan_timeBeforeFirstSpawn;
            this.GhostLeviatan_spawnTimeRatio = Configuration.Instance.GhostLeviatan_spawnTimeRatio;

            RefreshFieldsVisibility();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public void RefreshFieldsVisibility()
        {
            Visibility visibility = (CB_GhostLeviatan_enable.IsChecked != null && CB_GhostLeviatan_enable.IsChecked.HasValue && CB_GhostLeviatan_enable.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
            GRD_GhostLeviatan_health.Visibility = visibility;
            GRD_GhostLeviatan_maxSpawns.Visibility = visibility;
            GRD_GhostLeviatan_timeBeforeFirstSpawn.Visibility = visibility;
            GRD_GhostLeviatan_spawnTimeRatio.Visibility = visibility;
        }

        private void CB_GhostLeviatan_enable_Checked(object sender, RoutedEventArgs e)
        {
            RefreshFieldsVisibility();
        }

        private void CB_GhostLeviatan_enable_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshFieldsVisibility();
        }

        public bool GhostLeviatan_enable { get { return Configuration.Instance.GhostLeviatan_enable; } set { Configuration.Instance.GhostLeviatan_enable = value; } }
        public int GhostLeviatan_health { get { return Configuration.Instance.GhostLeviatan_health; } set { Configuration.Instance.GhostLeviatan_health = value; } }
        public int GhostLeviatan_maxSpawns { get { return Configuration.Instance.GhostLeviatan_maxSpawns; } set { Configuration.Instance.GhostLeviatan_maxSpawns = value; } }
        public int GhostLeviatan_timeBeforeFirstSpawn { get { return Configuration.Instance.GhostLeviatan_timeBeforeFirstSpawn; } set { Configuration.Instance.GhostLeviatan_timeBeforeFirstSpawn = value; } }
        public int GhostLeviatan_spawnTimeRatio { get { return Configuration.Instance.GhostLeviatan_spawnTimeRatio; } set { Configuration.Instance.GhostLeviatan_spawnTimeRatio = value; } }

        public string Config_GhostLeviathansSettings { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviathansSettings"); } set { } }
        public string Config_GhostLeviatan_enable { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_enable"); } set { } }
        public string Config_GhostLeviatan_enableDescription { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_enableDescription"); } set { } }
        public string Config_GhostLeviatan_health { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_health"); } set { } }
        public string Config_GhostLeviatan_healthDescription { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_healthDescription"); } set { } }
        public string Config_GhostLeviatan_maxSpawns { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_maxSpawns"); } set { } }
        public string Config_GhostLeviatan_maxSpawnsDescription { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_maxSpawnsDescription"); } set { } }
        public string Config_GhostLeviatan_timeBeforeFirstSpawn { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_timeBeforeFirstSpawn"); } set { } }
        public string Config_GhostLeviatan_timeBeforeFirstSpawnDescription { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_timeBeforeFirstSpawnDescription"); } set { } }
        public string Config_GhostLeviatan_spawnTimeRatio { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_spawnTimeRatio"); } set { } }
        public string Config_GhostLeviatan_spawnTimeRatioDescription { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_spawnTimeRatioDescription"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
