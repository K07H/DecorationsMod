using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static DecorationsModConfigurator.TechTypes;

namespace DecorationsModConfigurator
{
    public class FloraConfig
    {
        public string PlantName { get; set; }
        public string PlantImagePath { get; set; }
        public int GrowthDuration { get; set; }
        public int HealthPoints { get; set; }
        public int BioreactorCharge { get; set; }
        public bool CanEat { get; set; }
        public int NutrientsAmount { get; set; }
        public int WaterAmount { get; set; }
        public bool Decomposes { get; set; }
        public int DecompositionSpeed { get; set; }
        public float RealDecompositionSpeed
        {
            get { return ((float)DecompositionSpeed * 0.001f); }
            private set { }
        }

        public FloraConfig(string name = "Plant",
            string imagePath = "/Images/Flora/landtree1seedicon.png",
            int growthDuration = 1200,
            int healthPoints = 100,
            int bioreactorCharge = 200,
            bool canEat = false,
            int nutrients = 3,
            int water = 6,
            bool decomposes = false,
            float decompositionSpeed = 0.02f)
        {
            this.PlantName = name;
            this.PlantImagePath = imagePath;
            this.GrowthDuration = growthDuration;
            this.HealthPoints = healthPoints;
            this.BioreactorCharge = bioreactorCharge;
            this.CanEat = canEat;
            this.NutrientsAmount = nutrients;
            this.WaterAmount = water;
            this.Decomposes = decomposes;
            this.DecompositionSpeed = (int)(decompositionSpeed * 1000.0f);
        }

        public FloraConfig(FloraConfig config)
        {
            this.PlantName = config.PlantName;
            this.PlantImagePath = config.PlantImagePath;
            this.GrowthDuration = config.GrowthDuration;
            this.HealthPoints = config.HealthPoints;
            this.BioreactorCharge = config.BioreactorCharge;
            this.CanEat = config.CanEat;
            this.NutrientsAmount = config.NutrientsAmount;
            this.WaterAmount = config.WaterAmount;
            this.Decomposes = config.Decomposes;
            this.DecompositionSpeed = config.DecompositionSpeed;
        }

        public string GetConfigStr()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                this.GrowthDuration,
                this.HealthPoints,
                this.BioreactorCharge,
                this.CanEat,
                this.NutrientsAmount,
                this.WaterAmount,
                this.Decomposes,
                this.DecompositionSpeed);
        }

        public bool IsEqual(FloraConfig to)
        {
            //string.Compare(this.PlantName, to.PlantName, true, CultureInfo.InvariantCulture) != 0 ||
            //string.Compare(this.PlantImagePath, to.PlantImagePath, true, CultureInfo.InvariantCulture) != 0 ||
            return !(this.GrowthDuration != to.GrowthDuration ||
                this.HealthPoints != to.HealthPoints ||
                this.BioreactorCharge != to.BioreactorCharge ||
                this.CanEat != to.CanEat ||
                this.NutrientsAmount != to.NutrientsAmount ||
                this.WaterAmount != to.WaterAmount ||
                this.Decomposes != to.Decomposes ||
                this.DecompositionSpeed != to.DecompositionSpeed);
        }

        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "Plant \"{0}\": GrowthDuration=[{1}] HealthPoints=[{2}] BioreactorCharge=[{3}] CanEat=[{4}] Nutrients=[{5}] Water=[{6}] Decomposes[{7}] DecompositionSpeed=[{8}] Image=[{9}]", PlantName, GrowthDuration, HealthPoints, BioreactorCharge, CanEat, NutrientsAmount, WaterAmount, Decomposes, DecompositionSpeed, PlantImagePath);
    }

    /// <summary>
    /// Logique d'interaction pour UserControl_Flora.xaml
    /// </summary>
    public partial class UserControl_Flora : UserControl, INotifyPropertyChanged
    {
#if DEBUG_FLORA_TECHTYPE_SELECTOR
        private const string LOG_SELECTOR = "DEBUG: {0} recipe updates to {1} TechType [{2}]";
#endif

        public UserControl_Flora()
        {
            InitializeComponent();

            this.AddRegularAirSeeds = Configuration.Instance.AddRegularAirSeeds;
            this.AddRegularWaterSeeds = Configuration.Instance.AddRegularWaterSeeds;
            this.Flora_RecipiesResource = Configuration.Instance.Flora_RecipiesResource;
            this.Flora_RecipiesResourceAmount = Configuration.Instance.Flora_RecipiesResourceAmount;
            this.PurplePineconeDroppedResource = Configuration.Instance.PurplePineconeDroppedResource;
            this.PurplePineconeDroppedResourceAmount = Configuration.Instance.PurplePineconeDroppedResourceAmount;

            this.LandTree = Configuration.Instance.LandTree;
            this.JungleTreeA = Configuration.Instance.JungleTreeA;
            this.JungleTreeB = Configuration.Instance.JungleTreeB;
            this.TropicalTreeA = Configuration.Instance.TropicalTreeA;
            this.TropicalTreeB = Configuration.Instance.TropicalTreeB;
            this.TropicalTreeC = Configuration.Instance.TropicalTreeC;
            this.TropicalTreeD = Configuration.Instance.TropicalTreeD;
            this.LandPlantRedA = Configuration.Instance.LandPlantRedA;
            this.LandPlantRedB = Configuration.Instance.LandPlantRedB;
            this.LandPlantA = Configuration.Instance.LandPlantA;
            this.LandPlantB = Configuration.Instance.LandPlantB;
            this.LandPlantC = Configuration.Instance.LandPlantC;
            this.FernA = Configuration.Instance.FernA;
            this.FernB = Configuration.Instance.FernB;
            this.TropicalPlantA = Configuration.Instance.TropicalPlantA;
            this.TropicalPlantB = Configuration.Instance.TropicalPlantB;
            this.TropicalPlantC = Configuration.Instance.TropicalPlantC;
            this.TropicalPlantD = Configuration.Instance.TropicalPlantD;
            this.TropicalPlantE = Configuration.Instance.TropicalPlantE;
            this.TropicalPlantF = Configuration.Instance.TropicalPlantF;
            this.TropicalPlantG = Configuration.Instance.TropicalPlantG;
            this.TropicalPlantH = Configuration.Instance.TropicalPlantH;
            this.CrabClawKelpA = Configuration.Instance.CrabClawKelpA;
            this.CrabClawKelpB = Configuration.Instance.CrabClawKelpB;
            this.CrabClawKelpC = Configuration.Instance.CrabClawKelpC;
            this.PyroCoralA = Configuration.Instance.PyroCoralA;
            this.PyroCoralB = Configuration.Instance.PyroCoralB;
            this.PyroCoralC = Configuration.Instance.PyroCoralC;
            this.CoveTree = Configuration.Instance.CoveTree;
            this.GiantCoveTree = Configuration.Instance.GiantCoveTree;
            this.SpottedReedsA = Configuration.Instance.SpottedReedsA;
            this.SpottedReedsB = Configuration.Instance.SpottedReedsB;
            this.BrineLily = Configuration.Instance.BrineLily;
            this.LostRiverPlant = Configuration.Instance.LostRiverPlant;
            this.CoralReefPlantMiddle = Configuration.Instance.CoralReefPlantMiddle;
            this.SmallMushroomsDeco = Configuration.Instance.SmallMushroomsDeco;
            this.FloatingStone = Configuration.Instance.FloatingStone;
            this.BrownCoralTubesA = Configuration.Instance.BrownCoralTubesA;
            this.BrownCoralTubesB = Configuration.Instance.BrownCoralTubesB;
            this.BrownCoralTubesC = Configuration.Instance.BrownCoralTubesC;
            this.BlueCoralTubes = Configuration.Instance.BlueCoralTubes;
            this.PurplePinecone = Configuration.Instance.PurplePinecone;
            this.CoralPlantYellow = Configuration.Instance.CoralPlantYellow;
            this.CoralPlantGreen = Configuration.Instance.CoralPlantGreen;
            this.CoralPlantBlue = Configuration.Instance.CoralPlantBlue;
            this.CoralPlantRed = Configuration.Instance.CoralPlantRed;
            this.CoralPlantPurple = Configuration.Instance.CoralPlantPurple;
            this.RedGrass1 = Configuration.Instance.RedGrass1;
            this.RedGrass2 = Configuration.Instance.RedGrass2;
            this.RedGrass3 = Configuration.Instance.RedGrass3;
            this.RedGrass2Tall = Configuration.Instance.RedGrass2Tall;
            this.RedGrass3Tall = Configuration.Instance.RedGrass3Tall;
            this.BloodGrass = Configuration.Instance.BloodGrass;
            this.BloodGrassDense = Configuration.Instance.BloodGrassDense;
            this.MushroomTree1 = Configuration.Instance.MushroomTree1;
            this.MushroomTree2 = Configuration.Instance.MushroomTree2;
            this.MarbleMelonTiny = Configuration.Instance.MarbleMelonTiny;

            InitPlantCharacteristicsArray();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public bool AddRegularAirSeeds { get { return Configuration.Instance.AddRegularAirSeeds; } set { Configuration.Instance.AddRegularAirSeeds = value; } }
        public bool AddRegularWaterSeeds { get { return Configuration.Instance.AddRegularWaterSeeds; } set { Configuration.Instance.AddRegularWaterSeeds = value; } }
        public string Flora_RecipiesResource
        {
            get { return Configuration.Instance.Flora_RecipiesResource; }
            set
            {
                string newTechType = value;
                if (UserControl_TechType.AllTechTypes.ContainsValue(newTechType))
                {
                    foreach (KeyValuePair<TechType, string> elem in UserControl_TechType.AllTechTypes)
                    {
                        if (string.Compare(newTechType, elem.Value, true, CultureInfo.InvariantCulture) == 0)
                        {
                            newTechType = elem.Key.AsString();
                            break;
                        }
                    }
#if DEBUG_FLORA_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "Flora seed", "combo", newTechType);
#endif
                }
                else if (TechTypeExtensions.FromString(newTechType, out TechType techType, true) && techType != TechType.None)
                {
                    newTechType = techType.AsString();
#if DEBUG_FLORA_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "Flora seed", "game", newTechType);
#endif
                }
#if DEBUG_FLORA_TECHTYPE_SELECTOR
                else // Else, we are in custom (advanced) mode
                    Logger.Log(LOG_SELECTOR, "Flora seed", "custom", newTechType);
#endif

                Configuration.Instance.Flora_RecipiesResource = newTechType;
            }
        }
        public string Flora_RecipiesResource_Custom { get { return Flora_RecipiesResource; } set { if (value != null) Flora_RecipiesResource = value; } }
        public int Flora_RecipiesResourceAmount { get { return Configuration.Instance.Flora_RecipiesResourceAmount; } set { Configuration.Instance.Flora_RecipiesResourceAmount = (value < 1 ? 1 : value); } }
        public string PurplePineconeDroppedResource
        {
            get { return Configuration.Instance.PurplePineconeDroppedResource; }
            set
            {
                string newTechType = value;
                if (UserControl_TechType.AllTechTypes.ContainsValue(newTechType))
                {
                    foreach (KeyValuePair<TechType, string> elem in UserControl_TechType.AllTechTypes)
                    {
                        if (string.Compare(newTechType, elem.Value, true, CultureInfo.InvariantCulture) == 0)
                        {
                            newTechType = elem.Key.AsString();
                            break;
                        }
                    }
#if DEBUG_FLORA_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "Purple pinecone", "combo", newTechType);
#endif
                }
                else if (TechTypeExtensions.FromString(newTechType, out TechType techType, true) && techType != TechType.None)
                {
                    newTechType = techType.AsString();
#if DEBUG_FLORA_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "Purple pinecone", "game", newTechType);
#endif
                }
#if DEBUG_FLORA_TECHTYPE_SELECTOR
                else // Else, we are in custom (advanced) mode
                    Logger.Log(LOG_SELECTOR, "Purple pinecone", "custom", newTechType);
#endif

                Configuration.Instance.PurplePineconeDroppedResource = newTechType;
            }
        }
        public string PurplePineconeDroppedResource_Custom { get { return PurplePineconeDroppedResource; } set { if (value != null) PurplePineconeDroppedResource = value; } }
        public int PurplePineconeDroppedResourceAmount { get { return Configuration.Instance.PurplePineconeDroppedResourceAmount; } set { Configuration.Instance.PurplePineconeDroppedResourceAmount = (value < 1 ? 1 : value); } }
        public FloraConfig LandTree { get { return Configuration.Instance.LandTree; } set { Configuration.Instance.LandTree = value; } }
        public FloraConfig JungleTreeA { get { return Configuration.Instance.JungleTreeA; } set { Configuration.Instance.JungleTreeA = value; } }
        public FloraConfig JungleTreeB { get { return Configuration.Instance.JungleTreeB; } set { Configuration.Instance.JungleTreeB = value; } }
        public FloraConfig TropicalTreeA { get { return Configuration.Instance.TropicalTreeA; } set { Configuration.Instance.TropicalTreeA = value; } }
        public FloraConfig TropicalTreeB { get { return Configuration.Instance.TropicalTreeB; } set { Configuration.Instance.TropicalTreeB = value; } }
        public FloraConfig TropicalTreeC { get { return Configuration.Instance.TropicalTreeC; } set { Configuration.Instance.TropicalTreeC = value; } }
        public FloraConfig TropicalTreeD { get { return Configuration.Instance.TropicalTreeD; } set { Configuration.Instance.TropicalTreeD = value; } }
        public FloraConfig LandPlantRedA { get { return Configuration.Instance.LandPlantRedA; } set { Configuration.Instance.LandPlantRedA = value; } }
        public FloraConfig LandPlantRedB { get { return Configuration.Instance.LandPlantRedB; } set { Configuration.Instance.LandPlantRedB = value; } }
        public FloraConfig LandPlantA { get { return Configuration.Instance.LandPlantA; } set { Configuration.Instance.LandPlantA = value; } }
        public FloraConfig LandPlantB { get { return Configuration.Instance.LandPlantB; } set { Configuration.Instance.LandPlantB = value; } }
        public FloraConfig LandPlantC { get { return Configuration.Instance.LandPlantC; } set { Configuration.Instance.LandPlantC = value; } }
        public FloraConfig FernA { get { return Configuration.Instance.FernA; } set { Configuration.Instance.FernA = value; } }
        public FloraConfig FernB { get { return Configuration.Instance.FernB; } set { Configuration.Instance.FernB = value; } }
        public FloraConfig TropicalPlantA { get { return Configuration.Instance.TropicalPlantA; } set { Configuration.Instance.TropicalPlantA = value; } }
        public FloraConfig TropicalPlantB { get { return Configuration.Instance.TropicalPlantB; } set { Configuration.Instance.TropicalPlantB = value; } }
        public FloraConfig TropicalPlantC { get { return Configuration.Instance.TropicalPlantC; } set { Configuration.Instance.TropicalPlantC = value; } }
        public FloraConfig TropicalPlantD { get { return Configuration.Instance.TropicalPlantD; } set { Configuration.Instance.TropicalPlantD = value; } }
        public FloraConfig TropicalPlantE { get { return Configuration.Instance.TropicalPlantE; } set { Configuration.Instance.TropicalPlantE = value; } }
        public FloraConfig TropicalPlantF { get { return Configuration.Instance.TropicalPlantF; } set { Configuration.Instance.TropicalPlantF = value; } }
        public FloraConfig TropicalPlantG { get { return Configuration.Instance.TropicalPlantG; } set { Configuration.Instance.TropicalPlantG = value; } }
        public FloraConfig TropicalPlantH { get { return Configuration.Instance.TropicalPlantH; } set { Configuration.Instance.TropicalPlantH = value; } }
        public FloraConfig CrabClawKelpA { get { return Configuration.Instance.CrabClawKelpA; } set { Configuration.Instance.CrabClawKelpA = value; } }
        public FloraConfig CrabClawKelpB { get { return Configuration.Instance.CrabClawKelpB; } set { Configuration.Instance.CrabClawKelpB = value; } }
        public FloraConfig CrabClawKelpC { get { return Configuration.Instance.CrabClawKelpC; } set { Configuration.Instance.CrabClawKelpC = value; } }
        public FloraConfig PyroCoralA { get { return Configuration.Instance.PyroCoralA; } set { Configuration.Instance.PyroCoralA = value; } }
        public FloraConfig PyroCoralB { get { return Configuration.Instance.PyroCoralB; } set { Configuration.Instance.PyroCoralB = value; } }
        public FloraConfig PyroCoralC { get { return Configuration.Instance.PyroCoralC; } set { Configuration.Instance.PyroCoralC = value; } }
        public FloraConfig CoveTree { get { return Configuration.Instance.CoveTree; } set { Configuration.Instance.CoveTree = value; } }
        public FloraConfig GiantCoveTree { get { return Configuration.Instance.GiantCoveTree; } set { Configuration.Instance.GiantCoveTree = value; } }
        public FloraConfig SpottedReedsA { get { return Configuration.Instance.SpottedReedsA; } set { Configuration.Instance.SpottedReedsA = value; } }
        public FloraConfig SpottedReedsB { get { return Configuration.Instance.SpottedReedsB; } set { Configuration.Instance.SpottedReedsB = value; } }
        public FloraConfig BrineLily { get { return Configuration.Instance.BrineLily; } set { Configuration.Instance.BrineLily = value; } }
        public FloraConfig LostRiverPlant { get { return Configuration.Instance.LostRiverPlant; } set { Configuration.Instance.LostRiverPlant = value; } }
        public FloraConfig CoralReefPlantMiddle { get { return Configuration.Instance.CoralReefPlantMiddle; } set { Configuration.Instance.CoralReefPlantMiddle = value; } }
        public FloraConfig SmallMushroomsDeco { get { return Configuration.Instance.SmallMushroomsDeco; } set { Configuration.Instance.SmallMushroomsDeco = value; } }
        public FloraConfig FloatingStone { get { return Configuration.Instance.FloatingStone; } set { Configuration.Instance.FloatingStone = value; } }
        public FloraConfig BrownCoralTubesA { get { return Configuration.Instance.BrownCoralTubesA; } set { Configuration.Instance.BrownCoralTubesA = value; } }
        public FloraConfig BrownCoralTubesB { get { return Configuration.Instance.BrownCoralTubesB; } set { Configuration.Instance.BrownCoralTubesB = value; } }
        public FloraConfig BrownCoralTubesC { get { return Configuration.Instance.BrownCoralTubesC; } set { Configuration.Instance.BrownCoralTubesC = value; } }
        public FloraConfig BlueCoralTubes { get { return Configuration.Instance.BlueCoralTubes; } set { Configuration.Instance.BlueCoralTubes = value; } }
        public FloraConfig PurplePinecone { get { return Configuration.Instance.PurplePinecone; } set { Configuration.Instance.PurplePinecone = value; } }
        public FloraConfig CoralPlantYellow { get { return Configuration.Instance.CoralPlantYellow; } set { Configuration.Instance.CoralPlantYellow = value; } }
        public FloraConfig CoralPlantGreen { get { return Configuration.Instance.CoralPlantGreen; } set { Configuration.Instance.CoralPlantGreen = value; } }
        public FloraConfig CoralPlantBlue { get { return Configuration.Instance.CoralPlantBlue; } set { Configuration.Instance.CoralPlantBlue = value; } }
        public FloraConfig CoralPlantRed { get { return Configuration.Instance.CoralPlantRed; } set { Configuration.Instance.CoralPlantRed = value; } }
        public FloraConfig CoralPlantPurple { get { return Configuration.Instance.CoralPlantPurple; } set { Configuration.Instance.CoralPlantPurple = value; } }
        public FloraConfig RedGrass1 { get { return Configuration.Instance.RedGrass1; } set { Configuration.Instance.RedGrass1 = value; } }
        public FloraConfig RedGrass2 { get { return Configuration.Instance.RedGrass2; } set { Configuration.Instance.RedGrass2 = value; } }
        public FloraConfig RedGrass3 { get { return Configuration.Instance.RedGrass3; } set { Configuration.Instance.RedGrass3 = value; } }
        public FloraConfig RedGrass2Tall { get { return Configuration.Instance.RedGrass2Tall; } set { Configuration.Instance.RedGrass2Tall = value; } }
        public FloraConfig RedGrass3Tall { get { return Configuration.Instance.RedGrass3Tall; } set { Configuration.Instance.RedGrass3Tall = value; } }
        public FloraConfig BloodGrass { get { return Configuration.Instance.BloodGrass; } set { Configuration.Instance.BloodGrass = value; } }
        public FloraConfig BloodGrassDense { get { return Configuration.Instance.BloodGrassDense; } set { Configuration.Instance.BloodGrassDense = value; } }
        public FloraConfig MushroomTree1 { get { return Configuration.Instance.MushroomTree1; } set { Configuration.Instance.MushroomTree1 = value; } }
        public FloraConfig MushroomTree2 { get { return Configuration.Instance.MushroomTree2; } set { Configuration.Instance.MushroomTree2 = value; } }
        public FloraConfig MarbleMelonTiny { get { return Configuration.Instance.MarbleMelonTiny; } set { Configuration.Instance.MarbleMelonTiny = value; } }

        public string Config_FloraSettings { get { return LanguageHelper.GetFriendlyWord("Config_FloraSettings"); } set { } }
        public string Config_EnableRegularAirSeeds { get { return LanguageHelper.GetFriendlyWord("Config_EnableRegularAirSeeds"); } set { } }
        public string Config_EnableRegularAirSeedsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableRegularAirSeedsDescription"); } set { } }
        public string Config_EnableRegularWaterSeeds { get { return LanguageHelper.GetFriendlyWord("Config_EnableRegularWaterSeeds"); } set { } }
        public string Config_EnableRegularWaterSeedsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableRegularWaterSeedsDescription"); } set { } }
        public string Config_FloraRecipiesResource { get { return LanguageHelper.GetFriendlyWord("Config_FloraRecipiesResource"); } set { } }
        public string Config_FloraRecipiesResourceAmount { get { return LanguageHelper.GetFriendlyWord("Config_FloraRecipiesResourceAmount"); } set { } }
        public string Config_PurplePineconeDroppedResource { get { return LanguageHelper.GetFriendlyWord("Config_PurplePineconeDroppedResource"); } set { } }
        public string Config_PurplePineconeDroppedResourceAmount { get { return LanguageHelper.GetFriendlyWord("Config_PurplePineconeDroppedResourceAmount"); } set { } }
        public string Config_PlantsCharacteristics { get { return LanguageHelper.GetFriendlyWord("Config_PlantsCharacteristics"); } set { } }
        public string Config_FilterPlantsList { get { return LanguageHelper.GetFriendlyWord("Config_FilterPlantsList"); } set { } }

        public void RefreshGUI()
        {
            Configuration.RefreshInstancePlantNames();

            if (_plantsCharacteristics != null)
                foreach (UserControl_FloraConfig flora in _plantsCharacteristics)
                    flora.RefreshGUI();

            CB_Flora_RecipiesResource.RefreshGUI();
            CB_PurplePineconeDroppedResource.RefreshGUI();

            OnPropertyChanged("");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private static UserControl_FloraConfig[] _plantsCharacteristics = null;
        private void InitPlantCharacteristicsArray(bool forceRefresh = false)
        {
            if (_plantsCharacteristics == null || forceRefresh)
            {
                _plantsCharacteristics = new UserControl_FloraConfig[57]
                {
                    UC_LandTree,
                    UC_MarbleMelonTiny,
                    UC_MushroomTree2,
                    UC_MushroomTree1,
                    UC_JungleTreeA,
                    UC_JungleTreeB,
                    UC_TropicalTreeA,
                    UC_TropicalTreeB,
                    UC_TropicalTreeC,
                    UC_TropicalTreeD,
                    UC_LandPlantRedA,
                    UC_LandPlantRedB,
                    UC_LandPlantA,
                    UC_LandPlantB,
                    UC_LandPlantC,
                    UC_FernA,
                    UC_FernB,
                    UC_TropicalPlantA,
                    UC_TropicalPlantB,
                    UC_TropicalPlantC,
                    UC_TropicalPlantD,
                    UC_TropicalPlantE,
                    UC_TropicalPlantF,
                    UC_TropicalPlantG,
                    UC_TropicalPlantH,
                    UC_CrabClawKelpA,
                    UC_CrabClawKelpB,
                    UC_CrabClawKelpC,
                    UC_PyroCoralA,
                    UC_PyroCoralB,
                    UC_PyroCoralC,
                    UC_CoveTree,
                    UC_GiantCoveTree,
                    UC_SpottedReedsA,
                    UC_SpottedReedsB,
                    UC_BrineLily,
                    UC_LostRiverPlant,
                    UC_CoralReefPlantMiddle,
                    UC_SmallMushroomsDeco,
                    UC_FloatingStone,
                    UC_BrownCoralTubesA,
                    UC_BrownCoralTubesB,
                    UC_BrownCoralTubesC,
                    UC_BlueCoralTubes,
                    UC_PurplePinecone,
                    UC_CoralPlantYellow,
                    UC_CoralPlantGreen,
                    UC_CoralPlantBlue,
                    UC_CoralPlantRed,
                    UC_CoralPlantPurple,
                    UC_RedGrass1,
                    UC_RedGrass2,
                    UC_RedGrass3,
                    UC_RedGrass2Tall,
                    UC_RedGrass3Tall,
                    UC_BloodGrass,
                    UC_BloodGrassDense
                };
            }
        }

        private void TB_FilterByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null && _plantsCharacteristics != null)
            {
                string toSearch = !string.IsNullOrWhiteSpace(tb.Text) ? tb.Text.ToLowerInvariant() : null;
                foreach (UserControl_FloraConfig plantCharacteristics in _plantsCharacteristics)
                    plantCharacteristics.Visibility = (toSearch == null || plantCharacteristics.PlantName.ToLowerInvariant().Contains(toSearch)) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
