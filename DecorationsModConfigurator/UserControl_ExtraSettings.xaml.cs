using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_ExtraSettings.xaml
    /// </summary>
    public partial class UserControl_ExtraSettings : UserControl, INotifyPropertyChanged
    {
        public UserControl_ExtraSettings()
        {
            InitializeComponent();

            this.UseAlternativeScreenResolution = Configuration.Instance.UseAlternativeScreenResolution;
            this.HideDeepGrandReefDegasiBase = Configuration.Instance.HideDeepGrandReefDegasiBase;
            this.AsBuildable_SpecimenAnalyzer = Configuration.Instance.AsBuildable_SpecimenAnalyzer;
            this.AsBuildable_MarkiplierDoll1 = Configuration.Instance.AsBuildable_MarkiplierDoll1;
            this.AsBuildable_MarkiplierDoll2 = Configuration.Instance.AsBuildable_MarkiplierDoll2;
            this.AsBuildable_JackSepticEyeDoll = Configuration.Instance.AsBuildable_JackSepticEyeDoll;
            this.AsBuildable_EatMyDictionDoll = Configuration.Instance.AsBuildable_EatMyDictionDoll;
            this.AsBuildable_ForkliftToy = Configuration.Instance.AsBuildable_ForkliftToy;
            this.AsBuildable_SofaSmall = Configuration.Instance.AsBuildable_SofaSmall;
            this.AsBuildable_SofaMedium = Configuration.Instance.AsBuildable_SofaMedium;
            this.AsBuildable_SofaBig = Configuration.Instance.AsBuildable_SofaBig;
            this.AsBuildable_SofaCorner = Configuration.Instance.AsBuildable_SofaCorner;
            this.AsBuildable_LabCart = Configuration.Instance.AsBuildable_LabCart;
            this.AsBuildable_EmptyDesk = Configuration.Instance.AsBuildable_EmptyDesk;

            (this.Content as FrameworkElement).DataContext = this;
        }

        public bool UseAlternativeScreenResolution { get { return Configuration.Instance.UseAlternativeScreenResolution; } set { Configuration.Instance.UseAlternativeScreenResolution = value; } }
        public bool HideDeepGrandReefDegasiBase { get { return Configuration.Instance.HideDeepGrandReefDegasiBase; } set { Configuration.Instance.HideDeepGrandReefDegasiBase = value; } }
        public bool AsBuildable_SpecimenAnalyzer { get { return Configuration.Instance.AsBuildable_SpecimenAnalyzer; } set { Configuration.Instance.AsBuildable_SpecimenAnalyzer = value; } }
        public bool AsBuildable_MarkiplierDoll1 { get { return Configuration.Instance.AsBuildable_MarkiplierDoll1; } set { Configuration.Instance.AsBuildable_MarkiplierDoll1 = value; } }
        public bool AsBuildable_MarkiplierDoll2 { get { return Configuration.Instance.AsBuildable_MarkiplierDoll2; } set { Configuration.Instance.AsBuildable_MarkiplierDoll2 = value; } }
        public bool AsBuildable_JackSepticEyeDoll { get { return Configuration.Instance.AsBuildable_JackSepticEyeDoll; } set { Configuration.Instance.AsBuildable_JackSepticEyeDoll = value; } }
        public bool AsBuildable_EatMyDictionDoll { get { return Configuration.Instance.AsBuildable_EatMyDictionDoll; } set { Configuration.Instance.AsBuildable_EatMyDictionDoll = value; } }
        public bool AsBuildable_ForkliftToy { get { return Configuration.Instance.AsBuildable_ForkliftToy; } set { Configuration.Instance.AsBuildable_ForkliftToy = value; } }
        public bool AsBuildable_SofaSmall { get { return Configuration.Instance.AsBuildable_SofaSmall; } set { Configuration.Instance.AsBuildable_SofaSmall = value; } }
        public bool AsBuildable_SofaMedium { get { return Configuration.Instance.AsBuildable_SofaMedium; } set { Configuration.Instance.AsBuildable_SofaMedium = value; } }
        public bool AsBuildable_SofaBig { get { return Configuration.Instance.AsBuildable_SofaBig; } set { Configuration.Instance.AsBuildable_SofaBig = value; } }
        public bool AsBuildable_SofaCorner { get { return Configuration.Instance.AsBuildable_SofaCorner; } set { Configuration.Instance.AsBuildable_SofaCorner = value; } }
        public bool AsBuildable_LabCart { get { return Configuration.Instance.AsBuildable_LabCart; } set { Configuration.Instance.AsBuildable_LabCart = value; } }
        public bool AsBuildable_EmptyDesk { get { return Configuration.Instance.AsBuildable_EmptyDesk; } set { Configuration.Instance.AsBuildable_EmptyDesk = value; } }

        public string Config_ExtraSettings { get { return LanguageHelper.GetFriendlyWord("Config_ExtraSettings"); } set { } }
        public string Config_UseFlatScreenResolution { get { return LanguageHelper.GetFriendlyWord("Config_UseFlatScreenResolution"); } set { } }
        public string Config_UseFlatScreenResolutionDescription { get { return LanguageHelper.GetFriendlyWord("Config_UseFlatScreenResolutionDescription"); } set { } }
        public string Config_HideDeepGrandReefDegasiBase { get { return LanguageHelper.GetFriendlyWord("Config_HideDeepGrandReefDegasiBase"); } set { } }
        public string Config_HideDeepGrandReefDegasiBaseDescription { get { return LanguageHelper.GetFriendlyWord("Config_HideDeepGrandReefDegasiBaseDescription"); } set { } }
        public string Config_AsBuildable_SpecimenAnalyzer { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_SpecimenAnalyzer"); } set { } }
        public string Config_AsBuildable_MarkiplierDoll1 { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_MarkiplierDoll1"); } set { } }
        public string Config_AsBuildable_MarkiplierDoll2 { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_MarkiplierDoll2"); } set { } }
        public string Config_AsBuildable_JackSepticEyeDoll { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_JackSepticEyeDoll"); } set { } }
        public string Config_AsBuildable_EatMyDictionDoll { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_EatMyDictionDoll"); } set { } }
        public string Config_AsBuildable_ForkliftToy { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_ForkliftToy"); } set { } }
        public string Config_AsBuildable_SofaSmall { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_SofaSmall"); } set { } }
        public string Config_AsBuildable_SofaMedium { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_SofaMedium"); } set { } }
        public string Config_AsBuildable_SofaBig { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_SofaBig"); } set { } }
        public string Config_AsBuildable_SofaCorner { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_SofaCorner"); } set { } }
        public string Config_AsBuildable_LabCart { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_LabCart"); } set { } }
        public string Config_AsBuildable_EmptyDesk { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildable_EmptyDesk"); } set { } }
        public string Config_AsBuildableSettings { get { return LanguageHelper.GetFriendlyWord("Config_AsBuildableSettings"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try { Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)); }
            catch { MessageBox.Show("Could not open URL in web browser.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); }
            e.Handled = true;
        }
    }
}
