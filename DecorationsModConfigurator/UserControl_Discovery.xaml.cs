using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_Discovery.xaml
    /// </summary>
    public partial class UserControl_Discovery : UserControl, INotifyPropertyChanged
    {
        public UserControl_Discovery()
        {
            InitializeComponent();

            this.AddItemsWhenDiscovered = Configuration.Instance.AddItemsWhenDiscovered;
            this.AddAirSeedsWhenDiscovered = Configuration.Instance.AddAirSeedsWhenDiscovered;
            this.AddWaterSeedsWhenDiscovered = Configuration.Instance.AddWaterSeedsWhenDiscovered;
            this.AddEggsWhenCreatureScanned = Configuration.Instance.AddEggsWhenCreatureScanned;
            this.AddEggsAtStart = Configuration.Instance.AddEggsAtStart;

            CB_AddItemsWhenDiscovered.SelectedIndex = this.AddItemsWhenDiscovered ? 0 : 1;
            CB_AddAirSeedsWhenDiscovered.SelectedIndex = this.AddAirSeedsWhenDiscovered ? 0 : 1;
            CB_AddWaterSeedsWhenDiscovered.SelectedIndex = this.AddWaterSeedsWhenDiscovered ? 0 : 1;

            if (this.AddEggsAtStart)
                CB_AddEggsWhen.SelectedIndex = 2;
            else if (this.AddEggsWhenCreatureScanned)
                CB_AddEggsWhen.SelectedIndex = 1;
            else
                CB_AddEggsWhen.SelectedIndex = 0;

            (this.Content as FrameworkElement).DataContext = this;
        }

        public bool AddItemsWhenDiscovered { get { return Configuration.Instance.AddItemsWhenDiscovered; } set { Configuration.Instance.AddItemsWhenDiscovered = value; } }
        public bool AddAirSeedsWhenDiscovered { get { return Configuration.Instance.AddAirSeedsWhenDiscovered; } set { Configuration.Instance.AddAirSeedsWhenDiscovered = value; } }
        public bool AddWaterSeedsWhenDiscovered { get { return Configuration.Instance.AddWaterSeedsWhenDiscovered; } set { Configuration.Instance.AddWaterSeedsWhenDiscovered = value; } }
        public bool AddEggsWhenCreatureScanned { get { return Configuration.Instance.AddEggsWhenCreatureScanned; } set { Configuration.Instance.AddEggsWhenCreatureScanned = value; } }
        public bool AddEggsAtStart { get { return Configuration.Instance.AddEggsAtStart; } set { Configuration.Instance.AddEggsAtStart = value; } }

        public string Config_DiscoverySettings { get { return LanguageHelper.GetFriendlyWord("Config_DiscoverySettings"); } set { } }
        public string Config_EnableDiscoveryMode { get { return LanguageHelper.GetFriendlyWord("Config_EnableDiscoveryMode"); } set { } }
        public string Config_EnableDiscoveryModeDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableDiscoveryModeDescription"); } set { } }
        public string Config_AddRegularAirSeedsWhenDiscovered { get { return LanguageHelper.GetFriendlyWord("Config_AddRegularAirSeedsWhenDiscovered"); } set { } }
        public string Config_AddRegularAirSeedsWhenDiscoveredDescription { get { return LanguageHelper.GetFriendlyWord("Config_AddRegularAirSeedsWhenDiscoveredDescription"); } set { } }
        public string Config_AddRegularWaterSeedsWhenDiscovered { get { return LanguageHelper.GetFriendlyWord("Config_AddRegularWaterSeedsWhenDiscovered"); } set { } }
        public string Config_AddRegularWaterSeedsWhenDiscoveredDescription { get { return LanguageHelper.GetFriendlyWord("Config_AddRegularWaterSeedsWhenDiscoveredDescription"); } set { } }
        public string Config_EggsDicoverySetting { get { return LanguageHelper.GetFriendlyWord("Config_EggsDicoverySetting"); } set { } }
        public string Config_EggsDicoverySettingDescription { get { return LanguageHelper.GetFriendlyWord("Config_EggsDicoverySettingDescription"); } set { } }
        public string Config_WhenDiscoveriesMade { get { return LanguageHelper.GetFriendlyWord("Config_WhenDiscoveriesMade"); } set { } }
        public string Config_WhenGameStarts { get { return LanguageHelper.GetFriendlyWord("Config_WhenGameStarts"); } set { } }
        public string Config_WhenSeedPickedUpInGame { get { return LanguageHelper.GetFriendlyWord("Config_WhenSeedPickedUpInGame"); } set { } }
        public string Config_WhenEggHatched { get { return LanguageHelper.GetFriendlyWord("Config_WhenEggHatched"); } set { } }
        public string Config_WhenCreatureScanned { get { return LanguageHelper.GetFriendlyWord("Config_WhenCreatureScanned"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void ComboBox_AddEggsWhen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)cb.SelectedItem;
                if (cbi != null && !string.IsNullOrEmpty(cbi.Name))
                {
                    if (cbi.Name == "Eggs_WhenEggHatched")
                    {
                        this.AddEggsAtStart = false;
                        this.AddEggsWhenCreatureScanned = false;
                    }
                    else if (cbi.Name == "Eggs_WhenCreatureScanned")
                    {
                        this.AddEggsAtStart = false;
                        this.AddEggsWhenCreatureScanned = true;
                    }
                    else if (cbi.Name == "Eggs_AtStart")
                    {
                        this.AddEggsAtStart = true;
                        this.AddEggsWhenCreatureScanned = false;
                    }
                }
            }
        }

        private void CB_AddWaterSeedsWhenDiscovered_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)cb.SelectedItem;
                if (cbi != null && !string.IsNullOrEmpty(cbi.Name))
                    this.AddWaterSeedsWhenDiscovered = (cbi.Name == "WaterSeeds_WhenPlantPickedUp");
            }
        }

        private void CB_AddAirSeedsWhenDiscovered_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)cb.SelectedItem;
                if (cbi != null && !string.IsNullOrEmpty(cbi.Name))
                    this.AddAirSeedsWhenDiscovered = (cbi.Name == "AirSeeds_WhenPlantPickedUp");
            }
        }

        private void CB_AddItemsWhenDiscovered_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)cb.SelectedItem;
                if (cbi != null && !string.IsNullOrEmpty(cbi.Name))
                    this.AddItemsWhenDiscovered = (cbi.Name == "Items_WhenDiscovered");
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            SP_EnableDiscoveryMod_Details.Visibility = (SP_EnableDiscoveryMod_Details.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
