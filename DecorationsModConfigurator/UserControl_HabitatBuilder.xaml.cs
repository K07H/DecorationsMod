using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_HabitatBuilder.xaml
    /// </summary>
    public partial class UserControl_HabitatBuilder : UserControl, INotifyPropertyChanged
    {
        public UserControl_HabitatBuilder()
        {
            InitializeComponent();

            try
            {
                this.EnableNewItems = Configuration.Instance.EnableNewItems;
                this.EnableSofas = Configuration.Instance.EnableSofas;
                this.EnableDecorativeElectronics = Configuration.Instance.EnableDecorativeElectronics;
                this.EnableCustomBaseParts = Configuration.Instance.EnableCustomBaseParts;
                this.AllowIndoorLongPlanterOutside = Configuration.Instance.AllowIndoorLongPlanterOutside;
                this.AllowOutdoorLongPlanterInside = Configuration.Instance.AllowOutdoorLongPlanterInside;
            }
            catch (Exception ex) { Logger.Log("ERROR: Could not load habitat builder data from configuration. Exception=[" + ex.ToString() + "]"); }

            RefreshEnableNewItemsVisibility();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public bool EnableNewItems { get { return Configuration.Instance.EnableNewItems; } set { Configuration.Instance.EnableNewItems = value; RefreshEnableNewItemsVisibility(); } }
        public bool EnableSofas { get { return Configuration.Instance.EnableSofas; } set { Configuration.Instance.EnableSofas = value; } }
        public bool EnableDecorativeElectronics { get { return Configuration.Instance.EnableDecorativeElectronics; } set { Configuration.Instance.EnableDecorativeElectronics = value; } }
        public bool EnableCustomBaseParts { get { return Configuration.Instance.EnableCustomBaseParts; } set { Configuration.Instance.EnableCustomBaseParts = value; } }
        public bool AllowIndoorLongPlanterOutside { get { return Configuration.Instance.AllowIndoorLongPlanterOutside; } set { Configuration.Instance.AllowIndoorLongPlanterOutside = value; } }
        public bool AllowOutdoorLongPlanterInside { get { return Configuration.Instance.AllowOutdoorLongPlanterInside; } set { Configuration.Instance.AllowOutdoorLongPlanterInside = value; } }

        public bool AlienPillar1 { get { return Configuration.Instance.IsItemEnabled("AlienPillar1"); } set { if (value) Configuration.Instance.AddItem("AlienPillar1"); else Configuration.Instance.RemoveItem("AlienPillar1"); } }
        public bool AquariumSmall { get { return Configuration.Instance.IsItemEnabled("AquariumSmall"); } set { if (value) Configuration.Instance.AddItem("AquariumSmall"); else Configuration.Instance.RemoveItem("AquariumSmall"); } }
        public bool BarStool { get { return Configuration.Instance.IsItemEnabled("BarStool"); } set { if (value) Configuration.Instance.AddItem("BarStool"); else Configuration.Instance.RemoveItem("BarStool"); } }
        public bool BenchMedium { get { return Configuration.Instance.IsItemEnabled("BenchMedium"); } set { if (value) Configuration.Instance.AddItem("BenchMedium"); else Configuration.Instance.RemoveItem("BenchMedium"); } }
        public bool BenchSmall { get { return Configuration.Instance.IsItemEnabled("BenchSmall"); } set { if (value) Configuration.Instance.AddItem("BenchSmall"); else Configuration.Instance.RemoveItem("BenchSmall"); } }
        public bool CargoBox01_damaged { get { return Configuration.Instance.IsItemEnabled("CargoBox01_damaged"); } set { if (value) Configuration.Instance.AddItem("CargoBox01_damaged"); else Configuration.Instance.RemoveItem("CargoBox01_damaged"); } }
        public bool CargoBox01a { get { return Configuration.Instance.IsItemEnabled("CargoBox01a"); } set { if (value) Configuration.Instance.AddItem("CargoBox01a"); else Configuration.Instance.RemoveItem("CargoBox01a"); } }
        public bool CargoBox01b { get { return Configuration.Instance.IsItemEnabled("CargoBox01b"); } set { if (value) Configuration.Instance.AddItem("CargoBox01b"); else Configuration.Instance.RemoveItem("CargoBox01b"); } }
        public bool CustomPictureFrame { get { return Configuration.Instance.IsItemEnabled("CustomPictureFrame"); } set { if (value) Configuration.Instance.AddItem("CustomPictureFrame"); else Configuration.Instance.RemoveItem("CustomPictureFrame"); } }
        public bool CyclopsDoll { get { return Configuration.Instance.IsItemEnabled("CyclopsDoll"); } set { if (value) Configuration.Instance.AddItem("CyclopsDoll"); else Configuration.Instance.RemoveItem("CyclopsDoll"); } }
        public bool DecorationsEmptyDesk { get { return Configuration.Instance.IsItemEnabled("DecorationsEmptyDesk"); } set { if (value) Configuration.Instance.AddItem("DecorationsEmptyDesk"); else Configuration.Instance.RemoveItem("DecorationsEmptyDesk"); } }
        public bool DecorationsSpecimenAnalyzer { get { return Configuration.Instance.IsItemEnabled("DecorationsSpecimenAnalyzer"); } set { if (value) Configuration.Instance.AddItem("DecorationsSpecimenAnalyzer"); else Configuration.Instance.RemoveItem("DecorationsSpecimenAnalyzer"); } }
        public bool DecorativeControlTerminal { get { return Configuration.Instance.IsItemEnabled("DecorativeControlTerminal"); } set { if (value) Configuration.Instance.AddItem("DecorativeControlTerminal"); else Configuration.Instance.RemoveItem("DecorativeControlTerminal"); } }
        public bool DecorativeLocker { get { return Configuration.Instance.IsItemEnabled("DecorativeLocker"); } set { if (value) Configuration.Instance.AddItem("DecorativeLocker"); else Configuration.Instance.RemoveItem("DecorativeLocker"); } }
        public bool DecorativeLockerClosed { get { return Configuration.Instance.IsItemEnabled("DecorativeLockerClosed"); } set { if (value) Configuration.Instance.AddItem("DecorativeLockerClosed"); else Configuration.Instance.RemoveItem("DecorativeLockerClosed"); } }
        public bool DecorativeLockerDoor { get { return Configuration.Instance.IsItemEnabled("DecorativeLockerDoor"); } set { if (value) Configuration.Instance.AddItem("DecorativeLockerDoor"); else Configuration.Instance.RemoveItem("DecorativeLockerDoor"); } }
        public bool DecorativeTechBox { get { return Configuration.Instance.IsItemEnabled("DecorativeTechBox"); } set { if (value) Configuration.Instance.AddItem("DecorativeTechBox"); else Configuration.Instance.RemoveItem("DecorativeTechBox"); } }
        public bool MarlaCat { get { return Configuration.Instance.IsItemEnabled("MarlaCat"); } set { if (value) Configuration.Instance.AddItem("MarlaCat"); else Configuration.Instance.RemoveItem("MarlaCat"); } }
        public bool ExosuitDoll { get { return Configuration.Instance.IsItemEnabled("ExosuitDoll"); } set { if (value) Configuration.Instance.AddItem("ExosuitDoll"); else Configuration.Instance.RemoveItem("ExosuitDoll"); } }
        public bool ForkLiftDoll { get { return Configuration.Instance.IsItemEnabled("ForkLiftDoll"); } set { if (value) Configuration.Instance.AddItem("ForkLiftDoll"); else Configuration.Instance.RemoveItem("ForkLiftDoll"); } }
        public bool JackSepticEyeDoll { get { return Configuration.Instance.IsItemEnabled("JackSepticEyeDoll"); } set { if (value) Configuration.Instance.AddItem("JackSepticEyeDoll"); else Configuration.Instance.RemoveItem("JackSepticEyeDoll"); } }
        public bool LabCart { get { return Configuration.Instance.IsItemEnabled("LabCart"); } set { if (value) Configuration.Instance.AddItem("LabCart"); else Configuration.Instance.RemoveItem("LabCart"); } }
        public bool ALongPlanter { get { return Configuration.Instance.IsItemEnabled("ALongPlanter"); } set { if (value) Configuration.Instance.AddItem("ALongPlanter"); else Configuration.Instance.RemoveItem("ALongPlanter"); } }
        public bool LongPlanterB { get { return Configuration.Instance.IsItemEnabled("LongPlanterB"); } set { if (value) Configuration.Instance.AddItem("LongPlanterB"); else Configuration.Instance.RemoveItem("LongPlanterB"); } }
        public bool MarkiDoll1 { get { return Configuration.Instance.IsItemEnabled("MarkiDoll1"); } set { if (value) Configuration.Instance.AddItem("MarkiDoll1"); else Configuration.Instance.RemoveItem("MarkiDoll1"); } }
        public bool MarkiDoll2 { get { return Configuration.Instance.IsItemEnabled("MarkiDoll2"); } set { if (value) Configuration.Instance.AddItem("MarkiDoll2"); else Configuration.Instance.RemoveItem("MarkiDoll2"); } }
        public bool ReactorLamp { get { return Configuration.Instance.IsItemEnabled("ReactorLamp"); } set { if (value) Configuration.Instance.AddItem("ReactorLamp"); else Configuration.Instance.RemoveItem("ReactorLamp"); } }
        public bool SeamothDoll { get { return Configuration.Instance.IsItemEnabled("SeamothDoll"); } set { if (value) Configuration.Instance.AddItem("SeamothDoll"); else Configuration.Instance.RemoveItem("SeamothDoll"); } }
        public bool SofaCorner2 { get { return Configuration.Instance.IsItemEnabled("SofaCorner2"); } set { if (value) Configuration.Instance.AddItem("SofaCorner2"); else Configuration.Instance.RemoveItem("SofaCorner2"); } }
        public bool SofaStr1 { get { return Configuration.Instance.IsItemEnabled("SofaStr1"); } set { if (value) Configuration.Instance.AddItem("SofaStr1"); else Configuration.Instance.RemoveItem("SofaStr1"); } }
        public bool SofaStr2 { get { return Configuration.Instance.IsItemEnabled("SofaStr2"); } set { if (value) Configuration.Instance.AddItem("SofaStr2"); else Configuration.Instance.RemoveItem("SofaStr2"); } }
        public bool SofaStr3 { get { return Configuration.Instance.IsItemEnabled("SofaStr3"); } set { if (value) Configuration.Instance.AddItem("SofaStr3"); else Configuration.Instance.RemoveItem("SofaStr3"); } }
        public bool WarperPart1 { get { return Configuration.Instance.IsItemEnabled("WarperPart1"); } set { if (value) Configuration.Instance.AddItem("WarperPart1"); else Configuration.Instance.RemoveItem("WarperPart1"); } }
        public bool WorkDeskScreen1 { get { return Configuration.Instance.IsItemEnabled("WorkDeskScreen1"); } set { if (value) Configuration.Instance.AddItem("WorkDeskScreen1"); else Configuration.Instance.RemoveItem("WorkDeskScreen1"); } }
        public bool WorkDeskScreen2 { get { return Configuration.Instance.IsItemEnabled("WorkDeskScreen2"); } set { if (value) Configuration.Instance.AddItem("WorkDeskScreen2"); else Configuration.Instance.RemoveItem("WorkDeskScreen2"); } }
        public bool OutdoorLadder { get { return Configuration.Instance.IsItemEnabled("OutdoorLadder"); } set { if (value) Configuration.Instance.AddItem("OutdoorLadder"); else Configuration.Instance.RemoveItem("OutdoorLadder"); } }

        public string Config_HabitatBuilder { get { return LanguageHelper.GetFriendlyWord("Config_HabitatBuilder"); } set { } }
        public string Config_EnableNewItems { get { return LanguageHelper.GetFriendlyWord("Config_EnableNewItems"); } set { } }
        public string Config_EnableNewItemsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableNewItemsDescription"); } set { } }
        public string Config_EnableSofas { get { return LanguageHelper.GetFriendlyWord("Config_EnableSofas"); } set { } }
        public string Config_EnableSofasDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableSofasDescription"); } set { } }
        public string Config_EnableDecorativeElectronics { get { return LanguageHelper.GetFriendlyWord("Config_EnableDecorativeElectronics"); } set { } }
        public string Config_EnableDecorativeElectronicsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableDecorativeElectronicsDescription"); } set { } }
        public string Config_EnableCustomBaseParts { get { return LanguageHelper.GetFriendlyWord("Config_EnableCustomBaseParts"); } set { } }
        public string Config_EnableCustomBasePartsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableCustomBasePartsDescription"); } set { } }
        public string Config_AllowIndoorLongPlanterOutside { get { return LanguageHelper.GetFriendlyWord("Config_AllowIndoorLongPlanterOutside"); } set { } }
        public string Config_AllowIndoorLongPlanterOutsideDescription { get { return LanguageHelper.GetFriendlyWord("Config_AllowIndoorLongPlanterOutsideDescription"); } set { } }
        public string Config_AllowOutdoorLongPlanterInside { get { return LanguageHelper.GetFriendlyWord("Config_AllowOutdoorLongPlanterInside"); } set { } }
        public string Config_AllowOutdoorLongPlanterInsideDescription { get { return LanguageHelper.GetFriendlyWord("Config_AllowOutdoorLongPlanterInsideDescription"); } set { } }
        public string Config_HabitatBuilderElementsSettings { get { return LanguageHelper.GetFriendlyWord("Config_HabitatBuilderElementsSettings"); } set { } }

        public string Config_AlienPillar1 { get { return LanguageHelper.GetFriendlyWord("AlienPillar1Name"); } set { } }
        public string Config_AquariumSmall { get { return LanguageHelper.GetFriendlyWord("AquariumSmallName"); } set { } }
        public string Config_BarStool { get { return LanguageHelper.GetFriendlyWord("BarStoolName"); } set { } }
        public string Config_BenchMedium { get { return LanguageHelper.GetFriendlyWord("BenchMediumName"); } set { } }
        public string Config_BenchSmall { get { return LanguageHelper.GetFriendlyWord("BenchSmallName"); } set { } }
        public string Config_CargoBox01_damaged { get { return LanguageHelper.GetFriendlyWord("CargoBox1DmgName"); } set { } }
        public string Config_CargoBox01a { get { return LanguageHelper.GetFriendlyWord("CargoBox1aName"); } set { } }
        public string Config_CargoBox01b { get { return LanguageHelper.GetFriendlyWord("CargoBox1bName"); } set { } }
        public string Config_CustomPictureFrame { get { return LanguageHelper.GetFriendlyWord("CustomPictureFrameName"); } set { } }
        public string Config_CyclopsDoll { get { return LanguageHelper.GetFriendlyWord("CyclopsDollName"); } set { } }
        public string Config_DecorationsEmptyDesk { get { return LanguageHelper.GetFriendlyWord("DecorationsEmptyDeskName"); } set { } }
        public string Config_DecorationsSpecimenAnalyzer { get { return LanguageHelper.GetFriendlyWord("SpecimenAnalyzerName"); } set { } }
        public string Config_DecorativeControlTerminal { get { return LanguageHelper.GetFriendlyWord("DecorativeControlTerminalName"); } set { } }
        public string Config_DecorativeLocker { get { return LanguageHelper.GetFriendlyWord("DecorativeLockerName") + " (no door)"; } set { } }
        public string Config_DecorativeLockerClosed { get { return LanguageHelper.GetFriendlyWord("DecorativeLockerName") + " (door closed)"; } set { } }
        public string Config_DecorativeLockerDoor { get { return LanguageHelper.GetFriendlyWord("DecorativeLockerName") + " (door open)"; } set { } }
        public string Config_DecorativeTechBox { get { return LanguageHelper.GetFriendlyWord("DecorativeTechBoxName"); } set { } }
        public string Config_MarlaCat { get { return LanguageHelper.GetFriendlyWord("MarlaCatName"); } set { } }
        public string Config_ExosuitDoll { get { return LanguageHelper.GetFriendlyWord("ExosuitDollName"); } set { } }
        public string Config_ForkLiftDoll { get { return LanguageHelper.GetFriendlyWord("ForkLiftDollName"); } set { } }
        public string Config_JackSepticEyeDoll { get { return LanguageHelper.GetFriendlyWord("JackSepticEyeName"); } set { } }
        public string Config_LabCart { get { return LanguageHelper.GetFriendlyWord("LabCartName"); } set { } }
        public string Config_ALongPlanter { get { return LanguageHelper.GetFriendlyWord("LongPlanterName"); } set { } }
        public string Config_LongPlanterB { get { return LanguageHelper.GetFriendlyWord("ExteriorLongPlanterName"); } set { } }
        public string Config_MarkiDoll1 { get { return LanguageHelper.GetFriendlyWord("MarkiDollName") + " (Markiplier 1)"; } set { } }
        public string Config_MarkiDoll2 { get { return LanguageHelper.GetFriendlyWord("MarkiDollName") + " (Markiplier 2)"; } set { } }
        public string Config_ReactorLamp { get { return LanguageHelper.GetFriendlyWord("ReactorLampName"); } set { } }
        public string Config_SeamothDoll { get { return LanguageHelper.GetFriendlyWord("SeamothDollName"); } set { } }
        public string Config_SofaCorner2 { get { return LanguageHelper.GetFriendlyWord("SofaCorner2Name"); } set { } }
        public string Config_SofaStr1 { get { return LanguageHelper.GetFriendlyWord("SofaStr1Name"); } set { } }
        public string Config_SofaStr2 { get { return LanguageHelper.GetFriendlyWord("SofaStr2Name"); } set { } }
        public string Config_SofaStr3 { get { return LanguageHelper.GetFriendlyWord("SofaStr3Name"); } set { } }
        public string Config_WarperPart1 { get { return LanguageHelper.GetFriendlyWord("BigWarperPartName"); } set { } }
        public string Config_WorkDeskScreen1 { get { return LanguageHelper.GetFriendlyWord("WorkDeskScreen1Name"); } set { } }
        public string Config_WorkDeskScreen2 { get { return LanguageHelper.GetFriendlyWord("WorkDeskScreen2Name"); } set { } }
        public string Config_OutdoorLadder { get { return LanguageHelper.GetFriendlyWord("OutdoorLadderName"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void RefreshEnableNewItemsVisibility()
        {
            Visibility visibility = (CB_EnableNewItems.IsChecked != null && CB_EnableNewItems.IsChecked.HasValue && CB_EnableNewItems.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
            GRD_AddSofas.Visibility = visibility;
            GRD_AddDecorativeElectronics.Visibility = visibility;
            GRD_AddNewBaseParts.Visibility = visibility;
            GRD_EnableIndoorLongPlanterOutside.Visibility = visibility;
            GRD_EnableOutdoorLongPlanterInside.Visibility = visibility;
            SP_HabitatBuilderElementsSettings.Visibility = visibility;
            UC_AlienPillar1.Visibility = visibility;
            UC_AquariumSmall.Visibility = visibility;
            UC_BarStool.Visibility = visibility;
            UC_BenchMedium.Visibility = visibility;
            UC_BenchSmall.Visibility = visibility;
            UC_CargoBox01_damaged.Visibility = visibility;
            UC_CargoBox01a.Visibility = visibility;
            UC_CargoBox01b.Visibility = visibility;
            UC_CustomPictureFrame.Visibility = visibility;
            UC_CyclopsDoll.Visibility = visibility;
            UC_DecorationsEmptyDesk.Visibility = visibility;
            UC_DecorationsSpecimenAnalyzer.Visibility = visibility;
            UC_DecorativeControlTerminal.Visibility = visibility;
            UC_DecorativeLocker.Visibility = visibility;
            UC_DecorativeLockerClosed.Visibility = visibility;
            UC_DecorativeLockerDoor.Visibility = visibility;
            UC_DecorativeTechBox.Visibility = visibility;
            UC_MarlaCat.Visibility = visibility;
            UC_ExosuitDoll.Visibility = visibility;
            UC_ForkLiftDoll.Visibility = visibility;
            UC_JackSepticEyeDoll.Visibility = visibility;
            UC_LabCart.Visibility = visibility;
            UC_ALongPlanter.Visibility = visibility;
            UC_LongPlanterB.Visibility = visibility;
            UC_MarkiDoll1.Visibility = visibility;
            UC_MarkiDoll2.Visibility = visibility;
            UC_ReactorLamp.Visibility = visibility;
            UC_SeamothDoll.Visibility = visibility;
            UC_SofaCorner2.Visibility = visibility;
            UC_SofaStr1.Visibility = visibility;
            UC_SofaStr2.Visibility = visibility;
            UC_SofaStr3.Visibility = visibility;
            UC_WarperPart1.Visibility = visibility;
            UC_WorkDeskScreen1.Visibility = visibility;
            UC_WorkDeskScreen2.Visibility = visibility;
            UC_OutdoorLadder.Visibility = visibility;
        }

        private void CB_EnableNewItems_Checked(object sender, RoutedEventArgs e)
        {
            RefreshEnableNewItemsVisibility();
        }

        private void CB_EnableNewItems_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshEnableNewItemsVisibility();
        }

        public void RefreshSofasVisibility()
        {
            Visibility visibility = (CB_EnableSofas.IsChecked != null && CB_EnableSofas.IsChecked.HasValue && CB_EnableSofas.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
            UC_SofaCorner2.Visibility = visibility;
            UC_SofaStr1.Visibility = visibility;
            UC_SofaStr2.Visibility = visibility;
            UC_SofaStr3.Visibility = visibility;
        }

        private void CB_EnableSofas_Checked(object sender, RoutedEventArgs e)
        {
            RefreshSofasVisibility();
        }

        private void CB_EnableSofas_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshSofasVisibility();
        }

        public void RefreshDecorativeElectronicsVisibility()
        {
            Visibility visibility = (CB_EnableDecorativeElectronics.IsChecked != null && CB_EnableDecorativeElectronics.IsChecked.HasValue && CB_EnableDecorativeElectronics.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
            UC_DecorativeControlTerminal.Visibility = visibility;
            UC_WorkDeskScreen1.Visibility = visibility;
            UC_WorkDeskScreen2.Visibility = visibility;
            UC_DecorativeTechBox.Visibility = visibility;
        }

        private void CB_EnableDecorativeElectronics_Checked(object sender, RoutedEventArgs e)
        {
            RefreshDecorativeElectronicsVisibility();
        }

        private void CB_EnableDecorativeElectronics_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshDecorativeElectronicsVisibility();
        }

        public void RefreshCustomBasePartsVisibility()
        {
            Visibility visibility = (CB_EnableCustomBaseParts.IsChecked != null && CB_EnableCustomBaseParts.IsChecked.HasValue && CB_EnableCustomBaseParts.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
            UC_OutdoorLadder.Visibility = visibility;
        }

        private void CB_EnableCustomBaseParts_Checked(object sender, RoutedEventArgs e)
        {
            RefreshCustomBasePartsVisibility();
        }

        private void CB_EnableCustomBaseParts_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshCustomBasePartsVisibility();
        }
    }
}
