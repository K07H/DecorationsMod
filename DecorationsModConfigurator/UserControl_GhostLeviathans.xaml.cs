using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            this.GhostLeviatan_enable = false;
            this.GhostLeviatan_health = 2000;
            this.GhostLeviatan_maxSpawns = 2;
            this.GhostLeviatan_timeBeforeFirstSpawn = 1200;
            this.GhostLeviatan_spawnTimeRatio = 100;

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
        public string Config_GhostLeviatan_health { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_health"); } set { } }
        public string Config_GhostLeviatan_maxSpawns { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_maxSpawns"); } set { } }
        public string Config_GhostLeviatan_timeBeforeFirstSpawn { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_timeBeforeFirstSpawn"); } set { } }
        public string Config_GhostLeviatan_spawnTimeRatio { get { return LanguageHelper.GetFriendlyWord("Config_GhostLeviatan_spawnTimeRatio"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
