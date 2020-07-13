using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_DrinksAndFood.xaml
    /// </summary>
    public partial class UserControl_DrinksAndFood : UserControl, INotifyPropertyChanged
    {
        public UserControl_DrinksAndFood()
        {
            InitializeComponent();

            this.EnableNutrientBlock = Configuration.Instance.EnableNutrientBlock;
            this.BarBottle1Water = Configuration.Instance.BarBottle1Water;
            this.BarBottle2Water = Configuration.Instance.BarBottle2Water;
            this.BarBottle3Water = Configuration.Instance.BarBottle3Water;
            this.BarBottle4Water = Configuration.Instance.BarBottle4Water;
            this.BarBottle5Water = Configuration.Instance.BarBottle5Water;
            this.BarFood1Nutrient = Configuration.Instance.BarFood1Nutrient;
            this.BarFood1Water = Configuration.Instance.BarFood1Water;
            this.BarFood2Nutrient = Configuration.Instance.BarFood2Nutrient;
            this.BarFood2Water = Configuration.Instance.BarFood2Water;

            (this.Content as FrameworkElement).DataContext = this;
        }

        public bool EnableNutrientBlock { get { return Configuration.Instance.EnableNutrientBlock; } set { Configuration.Instance.EnableNutrientBlock = value; } }
        public int BarBottle1Water { get { return Configuration.Instance.BarBottle1Water; } set { Configuration.Instance.BarBottle1Water = (value < 1 ? 1 : value); } }
        public int BarBottle2Water { get { return Configuration.Instance.BarBottle2Water; } set { Configuration.Instance.BarBottle2Water = (value < 1 ? 1 : value); } }
        public int BarBottle3Water { get { return Configuration.Instance.BarBottle3Water; } set { Configuration.Instance.BarBottle3Water = (value < 1 ? 1 : value); } }
        public int BarBottle4Water { get { return Configuration.Instance.BarBottle4Water; } set { Configuration.Instance.BarBottle4Water = (value < 1 ? 1 : value); } }
        public int BarBottle5Water { get { return Configuration.Instance.BarBottle5Water; } set { Configuration.Instance.BarBottle5Water = (value < 1 ? 1 : value); } }
        public int BarFood1Nutrient { get { return Configuration.Instance.BarFood1Nutrient; } set { Configuration.Instance.BarFood1Nutrient = (value < 1 ? 1 : value); } }
        public int BarFood1Water { get { return Configuration.Instance.BarFood1Water; } set { Configuration.Instance.BarFood1Water = (value < 1 ? 1 : value); } }
        public int BarFood2Nutrient { get { return Configuration.Instance.BarFood2Nutrient; } set { Configuration.Instance.BarFood2Nutrient = (value < 1 ? 1 : value); } }
        public int BarFood2Water { get { return Configuration.Instance.BarFood2Water; } set { Configuration.Instance.BarFood2Water = (value < 1 ? 1 : value); } }
        
        public string Config_DrinksAndFoodSettings { get { return LanguageHelper.GetFriendlyWord("Config_DrinksAndFoodSettings"); } set { } }
        public string Config_EnableNutrientBlock { get { return LanguageHelper.GetFriendlyWord("Config_EnableNutrientBlock"); } set { } }
        public string Config_EnableNutrientBlockDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableNutrientBlockDescription"); } set { } }
        public string Config_BarBottle1_water { get { return LanguageHelper.GetFriendlyWord("Config_BarBottle1_water"); } set { } }
        public string Config_BarBottle2_water { get { return LanguageHelper.GetFriendlyWord("Config_BarBottle2_water"); } set { } }
        public string Config_BarBottle3_water { get { return LanguageHelper.GetFriendlyWord("Config_BarBottle3_water"); } set { } }
        public string Config_BarBottle4_water { get { return LanguageHelper.GetFriendlyWord("Config_BarBottle4_water"); } set { } }
        public string Config_BarBottle5_water { get { return LanguageHelper.GetFriendlyWord("Config_BarBottle5_water"); } set { } }
        public string Config_BarFood1_nutrient { get { return LanguageHelper.GetFriendlyWord("Config_BarFood1_nutrient"); } set { } }
        public string Config_BarFood1_water { get { return LanguageHelper.GetFriendlyWord("Config_BarFood1_water"); } set { } }
        public string Config_BarFood2_nutrient { get { return LanguageHelper.GetFriendlyWord("Config_BarFood2_nutrient"); } set { } }
        public string Config_BarFood2_water { get { return LanguageHelper.GetFriendlyWord("Config_BarFood2_water"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
