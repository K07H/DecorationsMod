using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_FloraConfig.xaml
    /// </summary>
    public partial class UserControl_FloraConfig : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty PlantProperty = DependencyProperty.Register("Plant", typeof(FloraConfig), typeof(UserControl_FloraConfig), new FrameworkPropertyMetadata(new FloraConfig(), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(PlantChanged)));
        public static readonly DependencyProperty PlantNameProperty = DependencyProperty.Register("PlantName", typeof(string), typeof(UserControl_FloraConfig), new FrameworkPropertyMetadata("Plant", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(PlantNameChanged)));
        
        public UserControl_FloraConfig()
        {
            InitializeComponent();

            this.PlantName = this.Plant.PlantName;
            this._PlantImagePath = this.Plant.PlantImagePath;
            this._GrowthDuration = this.Plant.GrowthDuration;
            this._HealthPoints = this.Plant.HealthPoints;
            this._BioreactorCharge = this.Plant.BioreactorCharge;
            this._CanEat = this.Plant.CanEat;
            this._NutrientsAmount = this.Plant.NutrientsAmount;
            this._WaterAmount = this.Plant.WaterAmount;
            this._Decomposes = this.Plant.Decomposes;
            this._DecompositionSpeed = this.Plant.DecompositionSpeed;

            RefreshEatableVisibility();
            RefreshDecompositionVisibility();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public static void PlantNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            UserControl_FloraConfig floraConfig = (UserControl_FloraConfig)o;
            if (floraConfig != null)
            {
                if (floraConfig.PlantName != floraConfig.Plant.PlantName)
                {
                    floraConfig.PlantName = floraConfig.Plant.PlantName;
                    floraConfig.OnPropertyChanged("PlantName");
                }
            }
        }

        public static void PlantChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            UserControl_FloraConfig floraConfig = (UserControl_FloraConfig)o;
            if (floraConfig != null)
            {
                if (floraConfig.PlantName != floraConfig.Plant.PlantName)
                {
                    floraConfig.PlantName = floraConfig.Plant.PlantName;
                    floraConfig.OnPropertyChanged("PlantName");
                }
                if (floraConfig.PlantImagePath != floraConfig.Plant.PlantImagePath)
                {
                    floraConfig.PlantImagePath = floraConfig.Plant.PlantImagePath;
                    floraConfig.OnPropertyChanged("PlantImagePath");
                }
                if (floraConfig.GrowthDuration != floraConfig.Plant.GrowthDuration)
                {
                    floraConfig.GrowthDuration = floraConfig.Plant.GrowthDuration;
                    floraConfig.OnPropertyChanged("GrowthDuration");
                }
                if (floraConfig.HealthPoints != floraConfig.Plant.HealthPoints)
                {
                    floraConfig.HealthPoints = floraConfig.Plant.HealthPoints;
                    floraConfig.OnPropertyChanged("HealthPoints");
                }
                if (floraConfig.BioreactorCharge != floraConfig.Plant.BioreactorCharge)
                {
                    floraConfig.BioreactorCharge = floraConfig.Plant.BioreactorCharge;
                    floraConfig.OnPropertyChanged("BioreactorCharge");
                }
                if (floraConfig.CanEat != floraConfig.Plant.CanEat)
                {
                    floraConfig.CanEat = floraConfig.Plant.CanEat;
                    floraConfig.OnPropertyChanged("CanEat");
                }
                if (floraConfig.NutrientsAmount != floraConfig.Plant.NutrientsAmount)
                {
                    floraConfig.NutrientsAmount = floraConfig.Plant.NutrientsAmount;
                    floraConfig.OnPropertyChanged("NutrientsAmount");
                }
                if (floraConfig.WaterAmount != floraConfig.Plant.WaterAmount)
                {
                    floraConfig.WaterAmount = floraConfig.Plant.WaterAmount;
                    floraConfig.OnPropertyChanged("WaterAmount");
                }
                if (floraConfig.Decomposes != floraConfig.Plant.Decomposes)
                {
                    floraConfig.Decomposes = floraConfig.Plant.Decomposes;
                    floraConfig.OnPropertyChanged("Decomposes");
                }
                if (floraConfig.DecompositionSpeed != floraConfig.Plant.DecompositionSpeed)
                {
                    floraConfig.DecompositionSpeed = floraConfig.Plant.DecompositionSpeed;
                    floraConfig.OnPropertyChanged("DecompositionSpeed");
                }
            }
        }

        public void RefreshEatableVisibility()
        {
            bool canEat = (CB_CanEat.IsChecked != null && CB_CanEat.IsChecked.HasValue && CB_CanEat.IsChecked.Value);
            GRD_NutrientsAmount.Visibility = (canEat ? Visibility.Visible : Visibility.Collapsed);
            GRD_WaterAmount.Visibility = (canEat ? Visibility.Visible : Visibility.Collapsed);
        }

        public void RefreshDecompositionVisibility()
        {
            bool decomposes = (CB_Decomposes.IsChecked != null && CB_Decomposes.IsChecked.HasValue && CB_Decomposes.IsChecked.Value);
            GRD_DecompositionSpeed.Visibility = (decomposes ? Visibility.Visible : Visibility.Collapsed);
        }

        private void CB_CanEat_Checked(object sender, RoutedEventArgs e)
        {
            RefreshEatableVisibility();
        }

        private void CB_CanEat_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshEatableVisibility();
        }

        private void CB_Decomposes_Checked(object sender, RoutedEventArgs e)
        {
            RefreshDecompositionVisibility();
        }

        private void CB_Decomposes_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshDecompositionVisibility();
        }

        public FloraConfig Plant { get => (FloraConfig)GetValue(PlantProperty); set => SetValue(PlantProperty, value); }
        public string PlantName { get => (string)GetValue(PlantNameProperty); set => SetValue(PlantNameProperty, value); }

        private string _PlantImagePath;
        public string PlantImagePath { get { return _PlantImagePath; } set { if (_PlantImagePath != value) { _PlantImagePath = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.PlantImagePath = value; this.Plant = changedPlant; } } }
        private int _GrowthDuration;
        public int GrowthDuration { get { return _GrowthDuration; } set { if (_GrowthDuration != value) { _GrowthDuration = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.GrowthDuration = value; this.Plant = changedPlant; } } }
        private int _HealthPoints;
        public int HealthPoints { get { return _HealthPoints; } set { if (_HealthPoints != value) { _HealthPoints = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.HealthPoints = value; this.Plant = changedPlant; } } }
        private int _BioreactorCharge;
        public int BioreactorCharge { get { return _BioreactorCharge; } set { if (_BioreactorCharge != value) { _BioreactorCharge = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.BioreactorCharge = value; this.Plant = changedPlant; } } }
        private bool _CanEat;
        public bool CanEat { get { return _CanEat; } set { if (_CanEat != value) { _CanEat = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.CanEat = value; this.Plant = changedPlant; } } }
        private int _NutrientsAmount;
        public int NutrientsAmount { get { return _NutrientsAmount; } set { if (_NutrientsAmount != value) { _NutrientsAmount = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.NutrientsAmount = value; this.Plant = changedPlant; } } }
        private int _WaterAmount;
        public int WaterAmount { get { return _WaterAmount; } set { if (_WaterAmount != value) { _WaterAmount = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.WaterAmount = value; this.Plant = changedPlant; } } }
        private bool _Decomposes;
        public bool Decomposes { get { return _Decomposes; } set { if (_Decomposes != value) { _Decomposes = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.Decomposes = value; this.Plant = changedPlant; } } }
        private int _DecompositionSpeed;
        public int DecompositionSpeed { get { return _DecompositionSpeed; } set { if (_DecompositionSpeed != value) { _DecompositionSpeed = value; FloraConfig changedPlant = new FloraConfig(this.Plant); changedPlant.DecompositionSpeed = value; this.Plant = changedPlant; } } }

        public string Config_PlantGrowthDuration { get { return LanguageHelper.GetFriendlyWord("Config_PlantGrowthDuration"); } set { } }
        public string Config_PlantHealthPoints { get { return LanguageHelper.GetFriendlyWord("Config_PlantHealthPoints"); } set { } }
        public string Config_PlantBioreactorCharge { get { return LanguageHelper.GetFriendlyWord("Config_PlantBioreactorCharge"); } set { } }
        public string Config_PlantCanEat { get { return LanguageHelper.GetFriendlyWord("Config_PlantCanEat"); } set { } }
        public string Config_PlantNutrientsAmount { get { return LanguageHelper.GetFriendlyWord("Config_PlantNutrientsAmount"); } set { } }
        public string Config_PlantWaterAmount { get { return LanguageHelper.GetFriendlyWord("Config_PlantWaterAmount"); } set { } }
        public string Config_PlantDecomposes { get { return LanguageHelper.GetFriendlyWord("Config_PlantDecomposes"); } set { } }
        public string Config_PlantDecompositionSpeed { get { return LanguageHelper.GetFriendlyWord("Config_PlantDecompositionSpeed"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
