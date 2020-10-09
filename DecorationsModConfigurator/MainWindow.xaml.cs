using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Pages
        private static UserControl_HomePage HomePage = null;
        private static UserControl_GeneralSettings GeneralSettingsPage = null;
        private static UserControl_HabitatBuilder HabitatBuilderPage = null;
        private static UserControl_Discovery DiscoveryPage = null;
        private static UserControl_Precursor PrecursorPage = null;
        private static UserControl_Eggs EggsPage = null;
        private static UserControl_DrinksAndFood DrinksAndFoodPage = null;
        private static UserControl_Flora FloraPage = null;
        private static UserControl_GhostLeviathans GhostLeviathansPage = null;
        private static UserControl_ExtraSettings ExtraSettingsPage = null;
        private static UserControl_About AboutPage = null;

        // Properties
        //public string Config_ConfiguratorName { get { return LanguageHelper.GetFriendlyWord("Config_ConfiguratorName"); } set { } }
        public string Config_TabGeneral { get { return LanguageHelper.GetFriendlyWord("Config_TabGeneral"); } set { } }
        public string Config_TabHabitatBuilder { get { return LanguageHelper.GetFriendlyWord("Config_TabHabitatBuilder"); } set { } }
        public string Config_TabDiscovery { get { return LanguageHelper.GetFriendlyWord("Config_TabDiscovery"); } set { } }
        public string Config_TabPrecursor { get { return LanguageHelper.GetFriendlyWord("Config_TabPrecursor"); } set { } }
        public string Config_TabEggs { get { return LanguageHelper.GetFriendlyWord("Config_TabEggs"); } set { } }
        public string Config_TabDrinksAndFood { get { return LanguageHelper.GetFriendlyWord("Config_TabDrinksAndFood"); } set { } }
        public string Config_TabFlora { get { return LanguageHelper.GetFriendlyWord("Config_TabFlora"); } set { } }
        public string Config_TabGhostLeviathans { get { return LanguageHelper.GetFriendlyWord("Config_TabGhostLeviathans"); } set { } }
        public string Config_TabExtraSettings { get { return LanguageHelper.GetFriendlyWord("Config_TabExtraSettings"); } set { } }
        public string Config_TabAbout { get { return LanguageHelper.GetFriendlyWord("Config_TabAbout"); } set { } }
        public string Config_BtnSaveAndQuit { get { return LanguageHelper.GetFriendlyWord("Config_BtnSaveAndQuit"); } set { } }

        public MainWindow()
        {
            InitializeComponent();

            Changes = new List<string>();
            HomePage = new UserControl_HomePage();
            Stack_Main.Children.Add(HomePage);
            ListView_Menu.SelectedIndex = 0;
            Title = LanguageHelper.GetFriendlyWord("Config_ConfiguratorName");

            if (Environment.OSVersion.Version.Major < 10) // If below Windows 10
                this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#B000344C"));
            (this.Content as FrameworkElement).DataContext = this;
        }

        public enum PageId
        {
            HOME = 0,
            GENERAL_SETTINGS = 1,
            HABITAT_BUILDER = 2,
            DISCOVERY = 3,
            PRECURSOR = 4,
            EGGS = 5,
            DRINKS_AND_FOOD = 6,
            FLORA = 7,
            GHOST_LEVIATHANS = 8,
            EXTRA_SETTINGS = 9,
            ABOUT = 10
        }

        public void SwitchPage(PageId toPage)
        {
            Stack_Main.Children.Clear();
            switch (toPage)
            {
                case PageId.HABITAT_BUILDER:
                    if (HabitatBuilderPage == null)
                        HabitatBuilderPage = new UserControl_HabitatBuilder();
                    Stack_Main.Children.Add(HabitatBuilderPage);
                    break;
                case PageId.DISCOVERY:
                    if (DiscoveryPage == null)
                        DiscoveryPage = new UserControl_Discovery();
                    Stack_Main.Children.Add(DiscoveryPage);
                    break;
                case PageId.PRECURSOR:
                    if (PrecursorPage == null)
                        PrecursorPage = new UserControl_Precursor();
                    Stack_Main.Children.Add(PrecursorPage);
                    break;
                case PageId.EGGS:
                    if (EggsPage == null)
                        EggsPage = new UserControl_Eggs();
                    Stack_Main.Children.Add(EggsPage);
                    break;
                case PageId.DRINKS_AND_FOOD:
                    if (DrinksAndFoodPage == null)
                        DrinksAndFoodPage = new UserControl_DrinksAndFood();
                    Stack_Main.Children.Add(DrinksAndFoodPage);
                    break;
                case PageId.FLORA:
                    if (FloraPage == null)
                        FloraPage = new UserControl_Flora();
                    Stack_Main.Children.Add(FloraPage);
                    break;
                case PageId.GHOST_LEVIATHANS:
                    if (GhostLeviathansPage == null)
                        GhostLeviathansPage = new UserControl_GhostLeviathans();
                    Stack_Main.Children.Add(GhostLeviathansPage);
                    break;
                case PageId.EXTRA_SETTINGS:
                    if (ExtraSettingsPage == null)
                        ExtraSettingsPage = new UserControl_ExtraSettings();
                    Stack_Main.Children.Add(ExtraSettingsPage);
                    break;
                case PageId.ABOUT:
                    if (AboutPage == null)
                        AboutPage = new UserControl_About();
                    Stack_Main.Children.Add(AboutPage);
                    break;
                default: // Fallback to PageId.GENERAL_SETTINGS
                    if (GeneralSettingsPage == null)
                        GeneralSettingsPage = new UserControl_GeneralSettings();
                    Stack_Main.Children.Add(GeneralSettingsPage);
                    break;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = ((StackPanel)((ListViewItem)ListView_Menu.SelectedItem)?.Content)?.Name;
            if (!string.IsNullOrEmpty(selected) && selected.Length > 3)
            {
                selected = selected.Substring(3);
                switch (selected)
                {
                    case "HabitatBuilder":
                        SwitchPage(PageId.HABITAT_BUILDER);
                        break;
                    case "Discovery":
                        SwitchPage(PageId.DISCOVERY);
                        break;
                    case "Precursor":
                        SwitchPage(PageId.PRECURSOR);
                        break;
                    case "Eggs":
                        SwitchPage(PageId.EGGS);
                        break;
                    case "DrinksAndFood":
                        SwitchPage(PageId.DRINKS_AND_FOOD);
                        break;
                    case "Flora":
                        SwitchPage(PageId.FLORA);
                        break;
                    case "GhostLeviathans":
                        SwitchPage(PageId.GHOST_LEVIATHANS);
                        break;
                    case "ExtraSettings":
                        SwitchPage(PageId.EXTRA_SETTINGS);
                        break;
                    case "About":
                        SwitchPage(PageId.ABOUT);
                        break;
                    default:
                        SwitchPage(PageId.GENERAL_SETTINGS);
                        break;
                }
            }
        }

        public void RefreshGUI()
        {
            // Refresh window pages
            if (GeneralSettingsPage != null)
                GeneralSettingsPage.RefreshGUI();
            if (HabitatBuilderPage != null)
                HabitatBuilderPage.RefreshGUI();
            if (DiscoveryPage != null)
                DiscoveryPage.RefreshGUI();
            if (PrecursorPage != null)
                PrecursorPage.RefreshGUI();
            if (EggsPage != null)
                EggsPage.RefreshGUI();
            if (DrinksAndFoodPage != null)
                DrinksAndFoodPage.RefreshGUI();
            if (FloraPage == null)
                FloraPage = new UserControl_Flora();
            if (FloraPage != null)
                FloraPage.RefreshGUI();
            if (GhostLeviathansPage != null)
                GhostLeviathansPage.RefreshGUI();
            if (ExtraSettingsPage != null)
                ExtraSettingsPage.RefreshGUI();
            if (AboutPage != null)
                AboutPage.RefreshGUI();
            // Refresh window title
            Title = LanguageHelper.GetFriendlyWord("Config_ConfiguratorName");
            // Refresh window elements (left menu, modals, ...)
            OnPropertyChanged("");
        }

        public List<string> GetChanges()
        {
            Configuration origConfig = Configuration.InstanceOrigin;
            Configuration currentConfig = Configuration.Instance;
            List<string> changes = new List<string>();

            if (string.Compare(currentConfig.Language, origConfig.Language, true, CultureInfo.InvariantCulture) != 0)
                changes.Add("Language changed from \"" + origConfig.Language + "\" to \"" + currentConfig.Language + "\".");
            if (currentConfig.UseCompactTooltips != origConfig.UseCompactTooltips)
                changes.Add("UseCompactTooltips changed from \"" + origConfig.UseCompactTooltips.ToString() + "\" to \"" + currentConfig.UseCompactTooltips.ToString() + "\".");
            if (currentConfig.LockQuickslotsWhenPlacingItem != origConfig.LockQuickslotsWhenPlacingItem)
                changes.Add("LockQuickslotsWhenPlacingItem changed from \"" + origConfig.LockQuickslotsWhenPlacingItem.ToString() + "\" to \"" + currentConfig.LockQuickslotsWhenPlacingItem.ToString() + "\".");
            if (currentConfig.AllowBuildOutside != origConfig.AllowBuildOutside)
                changes.Add("AllowBuildOutside changed from \"" + origConfig.AllowBuildOutside.ToString() + "\" to \"" + currentConfig.AllowBuildOutside.ToString() + "\".");
            if (currentConfig.AllowPlaceOutside != origConfig.AllowPlaceOutside)
                changes.Add("AllowPlaceOutside changed from \"" + origConfig.AllowPlaceOutside.ToString() + "\" to \"" + currentConfig.AllowPlaceOutside.ToString() + "\".");
            if (currentConfig.EnablePlaceItems != origConfig.EnablePlaceItems)
                changes.Add("EnablePlaceItems changed from \"" + origConfig.EnablePlaceItems.ToString() + "\" to \"" + currentConfig.EnablePlaceItems.ToString() + "\".");
            if (currentConfig.EnablePlaceBatteries != origConfig.EnablePlaceBatteries)
                changes.Add("EnablePlaceBatteries changed from \"" + origConfig.EnablePlaceBatteries.ToString() + "\" to \"" + currentConfig.EnablePlaceBatteries.ToString() + "\".");
            if (currentConfig.EnableNewFlora != origConfig.EnableNewFlora)
                changes.Add("EnableNewFlora changed from \"" + origConfig.EnableNewFlora.ToString() + "\" to \"" + currentConfig.EnableNewFlora.ToString() + "\".");
            if (currentConfig.EnableNewItems != origConfig.EnableNewItems)
                changes.Add("EnableNewItems changed from \"" + origConfig.EnableNewItems.ToString() + "\" to \"" + currentConfig.EnableNewItems.ToString() + "\".");
            if (currentConfig.EnableSofas != origConfig.EnableSofas)
                changes.Add("EnableSofas changed from \"" + origConfig.EnableSofas.ToString() + "\" to \"" + currentConfig.EnableSofas.ToString() + "\".");
            if (currentConfig.EnableDecorativeElectronics != origConfig.EnableDecorativeElectronics)
                changes.Add("EnableDecorativeElectronics changed from \"" + origConfig.EnableDecorativeElectronics.ToString() + "\" to \"" + currentConfig.EnableDecorativeElectronics.ToString() + "\".");
            if (currentConfig.EnableCustomBaseParts != origConfig.EnableCustomBaseParts)
                changes.Add("EnableCustomBaseParts changed from \"" + origConfig.EnableCustomBaseParts.ToString() + "\" to \"" + currentConfig.EnableCustomBaseParts.ToString() + "\".");
            if (currentConfig.HabitatBuilderItems != origConfig.HabitatBuilderItems)
                changes.Add("HabitatBuilderItems changed from \"" + origConfig.HabitatBuilderItems.ToString() + "\" to \"" + currentConfig.HabitatBuilderItems.ToString() + "\".");
            if (currentConfig.AllowIndoorLongPlanterOutside != origConfig.AllowIndoorLongPlanterOutside)
                changes.Add("AllowIndoorLongPlanterOutside changed from \"" + origConfig.AllowIndoorLongPlanterOutside.ToString() + "\" to \"" + currentConfig.AllowIndoorLongPlanterOutside.ToString() + "\".");
            if (currentConfig.AllowOutdoorLongPlanterInside != origConfig.AllowOutdoorLongPlanterInside)
                changes.Add("AllowOutdoorLongPlanterInside changed from \"" + origConfig.AllowOutdoorLongPlanterInside.ToString() + "\" to \"" + currentConfig.AllowOutdoorLongPlanterInside.ToString() + "\".");
            if (currentConfig.FixAquariumLighting != origConfig.FixAquariumLighting)
                changes.Add("FixAquariumLighting changed from \"" + origConfig.FixAquariumLighting.ToString() + "\" to \"" + currentConfig.FixAquariumLighting.ToString() + "\".");
            if (currentConfig.EnableAquariumGlassGlowing != origConfig.EnableAquariumGlassGlowing)
                changes.Add("EnableAquariumGlassGlowing changed from \"" + origConfig.EnableAquariumGlassGlowing.ToString() + "\" to \"" + currentConfig.EnableAquariumGlassGlowing.ToString() + "\".");
            if (currentConfig.AddItemsWhenDiscovered != origConfig.AddItemsWhenDiscovered)
                changes.Add("AddItemsWhenDiscovered changed from \"" + origConfig.AddItemsWhenDiscovered.ToString() + "\" to \"" + currentConfig.AddItemsWhenDiscovered.ToString() + "\".");
            if (currentConfig.AddAirSeedsWhenDiscovered != origConfig.AddAirSeedsWhenDiscovered)
                changes.Add("AddAirSeedsWhenDiscovered changed from \"" + origConfig.AddAirSeedsWhenDiscovered.ToString() + "\" to \"" + currentConfig.AddAirSeedsWhenDiscovered.ToString() + "\".");
            if (currentConfig.AddWaterSeedsWhenDiscovered != origConfig.AddWaterSeedsWhenDiscovered)
                changes.Add("AddWaterSeedsWhenDiscovered changed from \"" + origConfig.AddWaterSeedsWhenDiscovered.ToString() + "\" to \"" + currentConfig.AddWaterSeedsWhenDiscovered.ToString() + "\".");
            if (currentConfig.AddEggsWhenCreatureScanned != origConfig.AddEggsWhenCreatureScanned)
                changes.Add("AddEggsWhenCreatureScanned changed from \"" + origConfig.AddEggsWhenCreatureScanned.ToString() + "\" to \"" + currentConfig.AddEggsWhenCreatureScanned.ToString() + "\".");
            if (currentConfig.AddEggsAtStart != origConfig.AddEggsAtStart)
                changes.Add("AddEggsAtStart changed from \"" + origConfig.AddEggsAtStart.ToString() + "\" to \"" + currentConfig.AddEggsAtStart.ToString() + "\".");
            if (currentConfig.EnablePrecursorTab != origConfig.EnablePrecursorTab)
                changes.Add("EnablePrecursorTab changed from \"" + origConfig.EnablePrecursorTab.ToString() + "\" to \"" + currentConfig.EnablePrecursorTab.ToString() + "\".");
            if (currentConfig.PrecursorKeysAll != origConfig.PrecursorKeysAll)
                changes.Add("PrecursorKeysAll changed from \"" + origConfig.PrecursorKeysAll.ToString() + "\" to \"" + currentConfig.PrecursorKeysAll.ToString() + "\".");
            if (string.Compare(currentConfig.PrecursorKeys_RecipiesResource, origConfig.PrecursorKeys_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                changes.Add("PrecursorKeys_RecipiesResource changed from \"" + origConfig.PrecursorKeys_RecipiesResource.ToString() + "\" to \"" + currentConfig.PrecursorKeys_RecipiesResource.ToString() + "\".");
            if (currentConfig.PrecursorKeys_RecipiesResourceAmount != origConfig.PrecursorKeys_RecipiesResourceAmount)
                changes.Add("PrecursorKeys_RecipiesResourceAmount changed from \"" + origConfig.PrecursorKeys_RecipiesResourceAmount.ToString() + "\" to \"" + currentConfig.PrecursorKeys_RecipiesResourceAmount.ToString() + "\".");
            if (string.Compare(currentConfig.Relics_RecipiesResource, origConfig.Relics_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                changes.Add("Relics_RecipiesResource changed from \"" + origConfig.Relics_RecipiesResource.ToString() + "\" to \"" + currentConfig.Relics_RecipiesResource.ToString() + "\".");
            if (currentConfig.Relics_RecipiesResourceAmount != origConfig.Relics_RecipiesResourceAmount)
                changes.Add("Relics_RecipiesResourceAmount changed from \"" + origConfig.Relics_RecipiesResourceAmount.ToString() + "\" to \"" + currentConfig.Relics_RecipiesResourceAmount.ToString() + "\".");
            if (currentConfig.AlienRelic1Animation != origConfig.AlienRelic1Animation)
                changes.Add("AlienRelic1Animation changed from \"" + origConfig.AlienRelic1Animation.ToString() + "\" to \"" + currentConfig.AlienRelic1Animation.ToString() + "\".");
            if (currentConfig.AlienRelic2Animation != origConfig.AlienRelic2Animation)
                changes.Add("AlienRelic2Animation changed from \"" + origConfig.AlienRelic2Animation.ToString() + "\" to \"" + currentConfig.AlienRelic2Animation.ToString() + "\".");
            if (currentConfig.AlienRelic3Animation != origConfig.AlienRelic3Animation)
                changes.Add("AlienRelic3Animation changed from \"" + origConfig.AlienRelic3Animation.ToString() + "\" to \"" + currentConfig.AlienRelic3Animation.ToString() + "\".");
            if (currentConfig.AlienRelic4Animation != origConfig.AlienRelic4Animation)
                changes.Add("AlienRelic4Animation changed from \"" + origConfig.AlienRelic4Animation.ToString() + "\" to \"" + currentConfig.AlienRelic4Animation.ToString() + "\".");
            if (currentConfig.AlienRelic5Animation != origConfig.AlienRelic5Animation)
                changes.Add("AlienRelic5Animation changed from \"" + origConfig.AlienRelic5Animation.ToString() + "\" to \"" + currentConfig.AlienRelic5Animation.ToString() + "\".");
            if (currentConfig.AlienRelic6Animation != origConfig.AlienRelic6Animation)
                changes.Add("AlienRelic6Animation changed from \"" + origConfig.AlienRelic6Animation.ToString() + "\" to \"" + currentConfig.AlienRelic6Animation.ToString() + "\".");
            if (currentConfig.AlienRelic7Animation != origConfig.AlienRelic7Animation)
                changes.Add("AlienRelic7Animation changed from \"" + origConfig.AlienRelic7Animation.ToString() + "\" to \"" + currentConfig.AlienRelic7Animation.ToString() + "\".");
            if (currentConfig.AlienRelic8Animation != origConfig.AlienRelic8Animation)
                changes.Add("AlienRelic8Animation changed from \"" + origConfig.AlienRelic8Animation.ToString() + "\" to \"" + currentConfig.AlienRelic8Animation.ToString() + "\".");
            if (currentConfig.AlienRelic9Animation != origConfig.AlienRelic9Animation)
                changes.Add("AlienRelic9Animation changed from \"" + origConfig.AlienRelic9Animation.ToString() + "\" to \"" + currentConfig.AlienRelic9Animation.ToString() + "\".");
            if (currentConfig.AlienRelic10Animation != origConfig.AlienRelic10Animation)
                changes.Add("AlienRelic10Animation changed from \"" + origConfig.AlienRelic10Animation.ToString() + "\" to \"" + currentConfig.AlienRelic10Animation.ToString() + "\".");
            if (currentConfig.AlienRelic11Animation != origConfig.AlienRelic11Animation)
                changes.Add("AlienRelic11Animation changed from \"" + origConfig.AlienRelic11Animation.ToString() + "\" to \"" + currentConfig.AlienRelic11Animation.ToString() + "\".");
            if (currentConfig.EnableAllEggs != origConfig.EnableAllEggs)
                changes.Add("EnableAllEggs changed from \"" + origConfig.EnableAllEggs.ToString() + "\" to \"" + currentConfig.EnableAllEggs.ToString() + "\".");
            if (string.Compare(currentConfig.CreatureEggs_RecipiesResource, origConfig.CreatureEggs_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                changes.Add("CreatureEggs_RecipiesResource changed from \"" + origConfig.CreatureEggs_RecipiesResource.ToString() + "\" to \"" + currentConfig.CreatureEggs_RecipiesResource.ToString() + "\".");
            if (currentConfig.CreatureEggs_RecipiesResourceAmount != origConfig.CreatureEggs_RecipiesResourceAmount)
                changes.Add("CreatureEggs_RecipiesResourceAmount changed from \"" + origConfig.CreatureEggs_RecipiesResourceAmount.ToString() + "\" to \"" + currentConfig.CreatureEggs_RecipiesResourceAmount.ToString() + "\".");
            if (currentConfig.EnableNutrientBlock != origConfig.EnableNutrientBlock)
                changes.Add("EnableNutrientBlock changed from \"" + origConfig.EnableNutrientBlock.ToString() + "\" to \"" + currentConfig.EnableNutrientBlock.ToString() + "\".");
            if (currentConfig.BarBottle1Water != origConfig.BarBottle1Water)
                changes.Add("BarBottle1Water changed from \"" + origConfig.BarBottle1Water.ToString() + "\" to \"" + currentConfig.BarBottle1Water.ToString() + "\".");
            if (currentConfig.BarBottle2Water != origConfig.BarBottle2Water)
                changes.Add("BarBottle2Water changed from \"" + origConfig.BarBottle2Water.ToString() + "\" to \"" + currentConfig.BarBottle2Water.ToString() + "\".");
            if (currentConfig.BarBottle3Water != origConfig.BarBottle3Water)
                changes.Add("BarBottle3Water changed from \"" + origConfig.BarBottle3Water.ToString() + "\" to \"" + currentConfig.BarBottle3Water.ToString() + "\".");
            if (currentConfig.BarBottle4Water != origConfig.BarBottle4Water)
                changes.Add("BarBottle4Water changed from \"" + origConfig.BarBottle4Water.ToString() + "\" to \"" + currentConfig.BarBottle4Water.ToString() + "\".");
            if (currentConfig.BarBottle5Water != origConfig.BarBottle5Water)
                changes.Add("BarBottle5Water changed from \"" + origConfig.BarBottle5Water.ToString() + "\" to \"" + currentConfig.BarBottle5Water.ToString() + "\".");
            if (currentConfig.BarFood1Nutrient != origConfig.BarFood1Nutrient)
                changes.Add("BarFood1Nutrient changed from \"" + origConfig.BarFood1Nutrient.ToString() + "\" to \"" + currentConfig.BarFood1Nutrient.ToString() + "\".");
            if (currentConfig.BarFood1Water != origConfig.BarFood1Water)
                changes.Add("BarFood1Water changed from \"" + origConfig.BarFood1Water.ToString() + "\" to \"" + currentConfig.BarFood1Water.ToString() + "\".");
            if (currentConfig.BarFood2Nutrient != origConfig.BarFood2Nutrient)
                changes.Add("BarFood2Nutrient changed from \"" + origConfig.BarFood2Nutrient.ToString() + "\" to \"" + currentConfig.BarFood2Nutrient.ToString() + "\".");
            if (currentConfig.BarFood2Water != origConfig.BarFood2Water)
                changes.Add("BarFood2Water changed from \"" + origConfig.BarFood2Water.ToString() + "\" to \"" + currentConfig.BarFood2Water.ToString() + "\".");
            if (currentConfig.AddRegularAirSeeds != origConfig.AddRegularAirSeeds)
                changes.Add("AddRegularAirSeeds changed from \"" + origConfig.AddRegularAirSeeds.ToString() + "\" to \"" + currentConfig.AddRegularAirSeeds.ToString() + "\".");
            if (currentConfig.AddRegularWaterSeeds != origConfig.AddRegularWaterSeeds)
                changes.Add("AddRegularWaterSeeds changed from \"" + origConfig.AddRegularWaterSeeds.ToString() + "\" to \"" + currentConfig.AddRegularWaterSeeds.ToString() + "\".");
            if (string.Compare(currentConfig.Flora_RecipiesResource, origConfig.Flora_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                changes.Add("Flora_RecipiesResource changed from \"" + origConfig.Flora_RecipiesResource.ToString() + "\" to \"" + currentConfig.Flora_RecipiesResource.ToString() + "\".");
            if (currentConfig.Flora_RecipiesResourceAmount != origConfig.Flora_RecipiesResourceAmount)
                changes.Add("Flora_RecipiesResourceAmount changed from \"" + origConfig.Flora_RecipiesResourceAmount.ToString() + "\" to \"" + currentConfig.Flora_RecipiesResourceAmount.ToString() + "\".");
            if (string.Compare(currentConfig.PurplePineconeDroppedResource, origConfig.PurplePineconeDroppedResource, true, CultureInfo.InvariantCulture) != 0)
                changes.Add("PurplePineconeDroppedResource changed from \"" + origConfig.PurplePineconeDroppedResource.ToString() + "\" to \"" + currentConfig.PurplePineconeDroppedResource.ToString() + "\".");
            if (currentConfig.PurplePineconeDroppedResourceAmount != origConfig.PurplePineconeDroppedResourceAmount)
                changes.Add("PurplePineconeDroppedResourceAmount changed from \"" + origConfig.PurplePineconeDroppedResourceAmount.ToString() + "\" to \"" + currentConfig.PurplePineconeDroppedResourceAmount.ToString() + "\".");
            if (!currentConfig.LandTree.IsEqual(origConfig.LandTree))
                changes.Add("LandTree changed from \"" + origConfig.LandTree.ToString() + "\" to \"" + currentConfig.LandTree.ToString() + "\".");
            if (!currentConfig.JungleTreeA.IsEqual(origConfig.JungleTreeA))
                changes.Add("JungleTreeA changed from \"" + origConfig.JungleTreeA.ToString() + "\" to \"" + currentConfig.JungleTreeA.ToString() + "\".");
            if (!currentConfig.JungleTreeB.IsEqual(origConfig.JungleTreeB))
                changes.Add("JungleTreeB changed from \"" + origConfig.JungleTreeB.ToString() + "\" to \"" + currentConfig.JungleTreeB.ToString() + "\".");
            if (!currentConfig.TropicalTreeA.IsEqual(origConfig.TropicalTreeA))
                changes.Add("TropicalTreeA changed from \"" + origConfig.TropicalTreeA.ToString() + "\" to \"" + currentConfig.TropicalTreeA.ToString() + "\".");
            if (!currentConfig.TropicalTreeB.IsEqual(origConfig.TropicalTreeB))
                changes.Add("TropicalTreeB changed from \"" + origConfig.TropicalTreeB.ToString() + "\" to \"" + currentConfig.TropicalTreeB.ToString() + "\".");
            if (!currentConfig.TropicalTreeC.IsEqual(origConfig.TropicalTreeC))
                changes.Add("TropicalTreeC changed from \"" + origConfig.TropicalTreeC.ToString() + "\" to \"" + currentConfig.TropicalTreeC.ToString() + "\".");
            if (!currentConfig.TropicalTreeD.IsEqual(origConfig.TropicalTreeD))
                changes.Add("TropicalTreeD changed from \"" + origConfig.TropicalTreeD.ToString() + "\" to \"" + currentConfig.TropicalTreeD.ToString() + "\".");
            if (!currentConfig.LandPlantRedA.IsEqual(origConfig.LandPlantRedA))
                changes.Add("LandPlantRedA changed from \"" + origConfig.LandPlantRedA.ToString() + "\" to \"" + currentConfig.LandPlantRedA.ToString() + "\".");
            if (!currentConfig.LandPlantRedB.IsEqual(origConfig.LandPlantRedB))
                changes.Add("LandPlantRedB changed from \"" + origConfig.LandPlantRedB.ToString() + "\" to \"" + currentConfig.LandPlantRedB.ToString() + "\".");
            if (!currentConfig.LandPlantA.IsEqual(origConfig.LandPlantA))
                changes.Add("LandPlantA changed from \"" + origConfig.LandPlantA.ToString() + "\" to \"" + currentConfig.LandPlantA.ToString() + "\".");
            if (!currentConfig.LandPlantB.IsEqual(origConfig.LandPlantB))
                changes.Add("LandPlantB changed from \"" + origConfig.LandPlantB.ToString() + "\" to \"" + currentConfig.LandPlantB.ToString() + "\".");
            if (!currentConfig.LandPlantC.IsEqual(origConfig.LandPlantC))
                changes.Add("LandPlantC changed from \"" + origConfig.LandPlantC.ToString() + "\" to \"" + currentConfig.LandPlantC.ToString() + "\".");
            if (!currentConfig.FernA.IsEqual(origConfig.FernA))
                changes.Add("FernA changed from \"" + origConfig.FernA.ToString() + "\" to \"" + currentConfig.FernA.ToString() + "\".");
            if (!currentConfig.FernB.IsEqual(origConfig.FernB))
                changes.Add("FernB changed from \"" + origConfig.FernB.ToString() + "\" to \"" + currentConfig.FernB.ToString() + "\".");
            if (!currentConfig.TropicalPlantA.IsEqual(origConfig.TropicalPlantA))
                changes.Add("TropicalPlantA changed from \"" + origConfig.TropicalPlantA.ToString() + "\" to \"" + currentConfig.TropicalPlantA.ToString() + "\".");
            if (!currentConfig.TropicalPlantB.IsEqual(origConfig.TropicalPlantB))
                changes.Add("TropicalPlantB changed from \"" + origConfig.TropicalPlantB.ToString() + "\" to \"" + currentConfig.TropicalPlantB.ToString() + "\".");
            if (!currentConfig.TropicalPlantC.IsEqual(origConfig.TropicalPlantC))
                changes.Add("TropicalPlantC changed from \"" + origConfig.TropicalPlantC.ToString() + "\" to \"" + currentConfig.TropicalPlantC.ToString() + "\".");
            if (!currentConfig.TropicalPlantD.IsEqual(origConfig.TropicalPlantD))
                changes.Add("TropicalPlantD changed from \"" + origConfig.TropicalPlantD.ToString() + "\" to \"" + currentConfig.TropicalPlantD.ToString() + "\".");
            if (!currentConfig.TropicalPlantE.IsEqual(origConfig.TropicalPlantE))
                changes.Add("TropicalPlantE changed from \"" + origConfig.TropicalPlantE.ToString() + "\" to \"" + currentConfig.TropicalPlantE.ToString() + "\".");
            if (!currentConfig.TropicalPlantF.IsEqual(origConfig.TropicalPlantF))
                changes.Add("TropicalPlantF changed from \"" + origConfig.TropicalPlantF.ToString() + "\" to \"" + currentConfig.TropicalPlantF.ToString() + "\".");
            if (!currentConfig.TropicalPlantG.IsEqual(origConfig.TropicalPlantG))
                changes.Add("TropicalPlantG changed from \"" + origConfig.TropicalPlantG.ToString() + "\" to \"" + currentConfig.TropicalPlantG.ToString() + "\".");
            if (!currentConfig.TropicalPlantH.IsEqual(origConfig.TropicalPlantH))
                changes.Add("TropicalPlantH changed from \"" + origConfig.TropicalPlantH.ToString() + "\" to \"" + currentConfig.TropicalPlantH.ToString() + "\".");
            if (!currentConfig.CrabClawKelpA.IsEqual(origConfig.CrabClawKelpA))
                changes.Add("CrabClawKelpA changed from \"" + origConfig.CrabClawKelpA.ToString() + "\" to \"" + currentConfig.CrabClawKelpA.ToString() + "\".");
            if (!currentConfig.CrabClawKelpB.IsEqual(origConfig.CrabClawKelpB))
                changes.Add("CrabClawKelpB changed from \"" + origConfig.CrabClawKelpB.ToString() + "\" to \"" + currentConfig.CrabClawKelpB.ToString() + "\".");
            if (!currentConfig.CrabClawKelpC.IsEqual(origConfig.CrabClawKelpC))
                changes.Add("CrabClawKelpC changed from \"" + origConfig.CrabClawKelpC.ToString() + "\" to \"" + currentConfig.CrabClawKelpC.ToString() + "\".");
            if (!currentConfig.PyroCoralA.IsEqual(origConfig.PyroCoralA))
                changes.Add("PyroCoralA changed from \"" + origConfig.PyroCoralA.ToString() + "\" to \"" + currentConfig.PyroCoralA.ToString() + "\".");
            if (!currentConfig.PyroCoralB.IsEqual(origConfig.PyroCoralB))
                changes.Add("PyroCoralB changed from \"" + origConfig.PyroCoralB.ToString() + "\" to \"" + currentConfig.PyroCoralB.ToString() + "\".");
            if (!currentConfig.PyroCoralC.IsEqual(origConfig.PyroCoralC))
                changes.Add("PyroCoralC changed from \"" + origConfig.PyroCoralC.ToString() + "\" to \"" + currentConfig.PyroCoralC.ToString() + "\".");
            if (!currentConfig.CoveTree.IsEqual(origConfig.CoveTree))
                changes.Add("CoveTree changed from \"" + origConfig.CoveTree.ToString() + "\" to \"" + currentConfig.CoveTree.ToString() + "\".");
            if (!currentConfig.GiantCoveTree.IsEqual(origConfig.GiantCoveTree))
                changes.Add("GiantCoveTree changed from \"" + origConfig.GiantCoveTree.ToString() + "\" to \"" + currentConfig.GiantCoveTree.ToString() + "\".");
            if (!currentConfig.SpottedReedsA.IsEqual(origConfig.SpottedReedsA))
                changes.Add("SpottedReedsA changed from \"" + origConfig.SpottedReedsA.ToString() + "\" to \"" + currentConfig.SpottedReedsA.ToString() + "\".");
            if (!currentConfig.SpottedReedsB.IsEqual(origConfig.SpottedReedsB))
                changes.Add("SpottedReedsB changed from \"" + origConfig.SpottedReedsB.ToString() + "\" to \"" + currentConfig.SpottedReedsB.ToString() + "\".");
            if (!currentConfig.BrineLily.IsEqual(origConfig.BrineLily))
                changes.Add("BrineLily changed from \"" + origConfig.BrineLily.ToString() + "\" to \"" + currentConfig.BrineLily.ToString() + "\".");
            if (!currentConfig.LostRiverPlant.IsEqual(origConfig.LostRiverPlant))
                changes.Add("LostRiverPlant changed from \"" + origConfig.LostRiverPlant.ToString() + "\" to \"" + currentConfig.LostRiverPlant.ToString() + "\".");
            if (!currentConfig.CoralReefPlantMiddle.IsEqual(origConfig.CoralReefPlantMiddle))
                changes.Add("CoralReefPlantMiddle changed from \"" + origConfig.CoralReefPlantMiddle.ToString() + "\" to \"" + currentConfig.CoralReefPlantMiddle.ToString() + "\".");
            if (!currentConfig.SmallMushroomsDeco.IsEqual(origConfig.SmallMushroomsDeco))
                changes.Add("SmallMushroomsDeco changed from \"" + origConfig.SmallMushroomsDeco.ToString() + "\" to \"" + currentConfig.SmallMushroomsDeco.ToString() + "\".");
            if (!currentConfig.FloatingStone.IsEqual(origConfig.FloatingStone))
                changes.Add("FloatingStone changed from \"" + origConfig.FloatingStone.ToString() + "\" to \"" + currentConfig.FloatingStone.ToString() + "\".");
            if (!currentConfig.BrownCoralTubesA.IsEqual(origConfig.BrownCoralTubesA))
                changes.Add("BrownCoralTubesA changed from \"" + origConfig.BrownCoralTubesA.ToString() + "\" to \"" + currentConfig.BrownCoralTubesA.ToString() + "\".");
            if (!currentConfig.BrownCoralTubesB.IsEqual(origConfig.BrownCoralTubesB))
                changes.Add("BrownCoralTubesB changed from \"" + origConfig.BrownCoralTubesB.ToString() + "\" to \"" + currentConfig.BrownCoralTubesB.ToString() + "\".");
            if (!currentConfig.BrownCoralTubesC.IsEqual(origConfig.BrownCoralTubesC))
                changes.Add("BrownCoralTubesC changed from \"" + origConfig.BrownCoralTubesC.ToString() + "\" to \"" + currentConfig.BrownCoralTubesC.ToString() + "\".");
            if (!currentConfig.BlueCoralTubes.IsEqual(origConfig.BlueCoralTubes))
                changes.Add("BlueCoralTubes changed from \"" + origConfig.BlueCoralTubes.ToString() + "\" to \"" + currentConfig.BlueCoralTubes.ToString() + "\".");
            if (!currentConfig.PurplePinecone.IsEqual(origConfig.PurplePinecone))
                changes.Add("PurplePinecone changed from \"" + origConfig.PurplePinecone.ToString() + "\" to \"" + currentConfig.PurplePinecone.ToString() + "\".");
            if (!currentConfig.CoralPlantYellow.IsEqual(origConfig.CoralPlantYellow))
                changes.Add("CoralPlantYellow changed from \"" + origConfig.CoralPlantYellow.ToString() + "\" to \"" + currentConfig.CoralPlantYellow.ToString() + "\".");
            if (!currentConfig.CoralPlantGreen.IsEqual(origConfig.CoralPlantGreen))
                changes.Add("CoralPlantGreen changed from \"" + origConfig.CoralPlantGreen.ToString() + "\" to \"" + currentConfig.CoralPlantGreen.ToString() + "\".");
            if (!currentConfig.CoralPlantBlue.IsEqual(origConfig.CoralPlantBlue))
                changes.Add("CoralPlantBlue changed from \"" + origConfig.CoralPlantBlue.ToString() + "\" to \"" + currentConfig.CoralPlantBlue.ToString() + "\".");
            if (!currentConfig.CoralPlantRed.IsEqual(origConfig.CoralPlantRed))
                changes.Add("CoralPlantRed changed from \"" + origConfig.CoralPlantRed.ToString() + "\" to \"" + currentConfig.CoralPlantRed.ToString() + "\".");
            if (!currentConfig.CoralPlantPurple.IsEqual(origConfig.CoralPlantPurple))
                changes.Add("CoralPlantPurple changed from \"" + origConfig.CoralPlantPurple.ToString() + "\" to \"" + currentConfig.CoralPlantPurple.ToString() + "\".");
            if (!currentConfig.RedGrass1.IsEqual(origConfig.RedGrass1))
                changes.Add("RedGrass1 changed from \"" + origConfig.RedGrass1.ToString() + "\" to \"" + currentConfig.RedGrass1.ToString() + "\".");
            if (!currentConfig.RedGrass2.IsEqual(origConfig.RedGrass2))
                changes.Add("RedGrass2 changed from \"" + origConfig.RedGrass2.ToString() + "\" to \"" + currentConfig.RedGrass2.ToString() + "\".");
            if (!currentConfig.RedGrass3.IsEqual(origConfig.RedGrass3))
                changes.Add("RedGrass3 changed from \"" + origConfig.RedGrass3.ToString() + "\" to \"" + currentConfig.RedGrass3.ToString() + "\".");
            if (!currentConfig.RedGrass2Tall.IsEqual(origConfig.RedGrass2Tall))
                changes.Add("RedGrass2Tall changed from \"" + origConfig.RedGrass2Tall.ToString() + "\" to \"" + currentConfig.RedGrass2Tall.ToString() + "\".");
            if (!currentConfig.RedGrass3Tall.IsEqual(origConfig.RedGrass3Tall))
                changes.Add("RedGrass3Tall changed from \"" + origConfig.RedGrass3Tall.ToString() + "\" to \"" + currentConfig.RedGrass3Tall.ToString() + "\".");
            if (!currentConfig.BloodGrass.IsEqual(origConfig.BloodGrass))
                changes.Add("BloodGrass changed from \"" + origConfig.BloodGrass.ToString() + "\" to \"" + currentConfig.BloodGrass.ToString() + "\".");
            if (!currentConfig.BloodGrassDense.IsEqual(origConfig.BloodGrassDense))
                changes.Add("BloodGrassDense changed from \"" + origConfig.BloodGrassDense.ToString() + "\" to \"" + currentConfig.BloodGrassDense.ToString() + "\".");
            if (!currentConfig.MushroomTree1.IsEqual(origConfig.MushroomTree1))
                changes.Add("MushroomTree1 changed from \"" + origConfig.MushroomTree1.ToString() + "\" to \"" + currentConfig.MushroomTree1.ToString() + "\".");
            if (!currentConfig.MushroomTree2.IsEqual(origConfig.MushroomTree2))
                changes.Add("MushroomTree2 changed from \"" + origConfig.MushroomTree2.ToString() + "\" to \"" + currentConfig.MushroomTree2.ToString() + "\".");
            if (!currentConfig.MarbleMelonTiny.IsEqual(origConfig.MarbleMelonTiny))
                changes.Add("MarbleMelonTiny changed from \"" + origConfig.MarbleMelonTiny.ToString() + "\" to \"" + currentConfig.MarbleMelonTiny.ToString() + "\".");
            if (currentConfig.GhostLeviatan_enable != origConfig.GhostLeviatan_enable)
                changes.Add("GhostLeviatan_enable changed from \"" + origConfig.GhostLeviatan_enable.ToString() + "\" to \"" + currentConfig.GhostLeviatan_enable.ToString() + "\".");
            if (currentConfig.GhostLeviatan_health != origConfig.GhostLeviatan_health)
                changes.Add("GhostLeviatan_health changed from \"" + origConfig.GhostLeviatan_health.ToString() + "\" to \"" + currentConfig.GhostLeviatan_health.ToString() + "\".");
            if (currentConfig.GhostLeviatan_maxSpawns != origConfig.GhostLeviatan_maxSpawns)
                changes.Add("GhostLeviatan_maxSpawns changed from \"" + origConfig.GhostLeviatan_maxSpawns.ToString() + "\" to \"" + currentConfig.GhostLeviatan_maxSpawns.ToString() + "\".");
            if (currentConfig.GhostLeviatan_timeBeforeFirstSpawn != origConfig.GhostLeviatan_timeBeforeFirstSpawn)
                changes.Add("GhostLeviatan_timeBeforeFirstSpawn changed from \"" + origConfig.GhostLeviatan_timeBeforeFirstSpawn.ToString() + "\" to \"" + currentConfig.GhostLeviatan_timeBeforeFirstSpawn.ToString() + "\".");
            if (currentConfig.GhostLeviatan_spawnTimeRatio != origConfig.GhostLeviatan_spawnTimeRatio)
                changes.Add("GhostLeviatan_spawnTimeRatio changed from \"" + origConfig.GhostLeviatan_spawnTimeRatio.ToString() + "\" to \"" + currentConfig.GhostLeviatan_spawnTimeRatio.ToString() + "\".");
            if (currentConfig.UseAlternativeScreenResolution != origConfig.UseAlternativeScreenResolution)
                changes.Add("UseAlternativeScreenResolution changed from \"" + origConfig.UseAlternativeScreenResolution.ToString() + "\" to \"" + currentConfig.UseAlternativeScreenResolution.ToString() + "\".");
            if (currentConfig.HideDeepGrandReefDegasiBase != origConfig.HideDeepGrandReefDegasiBase)
                changes.Add("HideDeepGrandReefDegasiBase changed from \"" + origConfig.HideDeepGrandReefDegasiBase.ToString() + "\" to \"" + currentConfig.HideDeepGrandReefDegasiBase.ToString() + "\".");
            if (currentConfig.AsBuildable_SpecimenAnalyzer != origConfig.AsBuildable_SpecimenAnalyzer)
                changes.Add("AsBuildable_SpecimenAnalyzer changed from \"" + origConfig.AsBuildable_SpecimenAnalyzer.ToString() + "\" to \"" + currentConfig.AsBuildable_SpecimenAnalyzer.ToString() + "\".");
            if (currentConfig.AsBuildable_MarkiplierDoll1 != origConfig.AsBuildable_MarkiplierDoll1)
                changes.Add("AsBuildable_MarkiplierDoll1 changed from \"" + origConfig.AsBuildable_MarkiplierDoll1.ToString() + "\" to \"" + currentConfig.AsBuildable_MarkiplierDoll1.ToString() + "\".");
            if (currentConfig.AsBuildable_MarkiplierDoll2 != origConfig.AsBuildable_MarkiplierDoll2)
                changes.Add("AsBuildable_MarkiplierDoll2 changed from \"" + origConfig.AsBuildable_MarkiplierDoll2.ToString() + "\" to \"" + currentConfig.AsBuildable_MarkiplierDoll2.ToString() + "\".");
            if (currentConfig.AsBuildable_JackSepticEyeDoll != origConfig.AsBuildable_JackSepticEyeDoll)
                changes.Add("AsBuildable_JackSepticEyeDoll changed from \"" + origConfig.AsBuildable_JackSepticEyeDoll.ToString() + "\" to \"" + currentConfig.AsBuildable_JackSepticEyeDoll.ToString() + "\".");
            if (currentConfig.AsBuildable_EatMyDictionDoll != origConfig.AsBuildable_EatMyDictionDoll)
                changes.Add("AsBuildable_EatMyDictionDoll changed from \"" + origConfig.AsBuildable_EatMyDictionDoll.ToString() + "\" to \"" + currentConfig.AsBuildable_EatMyDictionDoll.ToString() + "\".");
            if (currentConfig.AsBuildable_ForkliftToy != origConfig.AsBuildable_ForkliftToy)
                changes.Add("AsBuildable_ForkliftToy changed from \"" + origConfig.AsBuildable_ForkliftToy.ToString() + "\" to \"" + currentConfig.AsBuildable_ForkliftToy.ToString() + "\".");
            if (currentConfig.AsBuildable_SofaSmall != origConfig.AsBuildable_SofaSmall)
                changes.Add("AsBuildable_SofaSmall changed from \"" + origConfig.AsBuildable_SofaSmall.ToString() + "\" to \"" + currentConfig.AsBuildable_SofaSmall.ToString() + "\".");
            if (currentConfig.AsBuildable_SofaMedium != origConfig.AsBuildable_SofaMedium)
                changes.Add("AsBuildable_SofaMedium changed from \"" + origConfig.AsBuildable_SofaMedium.ToString() + "\" to \"" + currentConfig.AsBuildable_SofaMedium.ToString() + "\".");
            if (currentConfig.AsBuildable_SofaBig != origConfig.AsBuildable_SofaBig)
                changes.Add("AsBuildable_SofaBig changed from \"" + origConfig.AsBuildable_SofaBig.ToString() + "\" to \"" + currentConfig.AsBuildable_SofaBig.ToString() + "\".");
            if (currentConfig.AsBuildable_SofaCorner != origConfig.AsBuildable_SofaCorner)
                changes.Add("AsBuildable_SofaCorner changed from \"" + origConfig.AsBuildable_SofaCorner.ToString() + "\" to \"" + currentConfig.AsBuildable_SofaCorner.ToString() + "\".");
            if (currentConfig.AsBuildable_LabCart != origConfig.AsBuildable_LabCart)
                changes.Add("AsBuildable_LabCart changed from \"" + origConfig.AsBuildable_LabCart.ToString() + "\" to \"" + currentConfig.AsBuildable_LabCart.ToString() + "\".");
            if (currentConfig.AsBuildable_EmptyDesk != origConfig.AsBuildable_EmptyDesk)
                changes.Add("AsBuildable_EmptyDesk changed from \"" + origConfig.AsBuildable_EmptyDesk.ToString() + "\" to \"" + currentConfig.AsBuildable_EmptyDesk.ToString() + "\".");

            return changes;
        }

        public void SaveModifications()
        {
            Logger.Log("INFO: Saving changes...");
            Configuration.SaveConfiguration();
        }

        private void ConfiguratorClosing()
        {
            if (!DH_Root.IsOpen)
            {
                Changes = GetChanges();
                LV_Changes.ItemsSource = Changes;
                if (Changes != null && Changes.Count > 0)
                {
                    try { Logger.Log("INFO: Found config changes:{0}----------{0}{1}{0}----------", Environment.NewLine, string.Join(Environment.NewLine, Changes)); }
                    catch { }
                    DH_Root.IsOpen = true;
                }
                else
                    Application.Current.Shutdown(0);
            }
        }

        public List<string> Changes { get; set; }
        private void LVI_SaveAndQuit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            ConfiguratorClosing();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ConfiguratorClosing();
        }

        private void BTN_CancelAndQuit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Application.Current.Shutdown(0);
        }

        private void BTN_SaveAndQuit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            SaveModifications();
            SP_Changes.Visibility = Visibility.Collapsed;
            SP_ChangesButtons.Visibility = Visibility.Collapsed;
            SP_RestartSubnautica.Visibility = Visibility.Visible;
            SP_RestartSubnauticaButtons.Visibility = Visibility.Visible;
            GRD_DialogHost.Height = 140d;
            if (!DH_Root.IsOpen)
                DH_Root.IsOpen = true;
        }

        private void BTN_Ok_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Application.Current.Shutdown(0);
        }

        public string Config_ConfigChangedListOfChanges { get { return LanguageHelper.GetFriendlyWord("Config_ConfigChangedListOfChanges"); } set { } }
        public string Config_CancelChangesAndQuit { get { return LanguageHelper.GetFriendlyWord("Config_CancelChangesAndQuit"); } set { } }
        public string Config_SaveChangesAndQuit { get { return LanguageHelper.GetFriendlyWord("Config_SaveChangesAndQuit"); } set { } }
        public string Config_ConfigChangedPleaseRestart { get { return LanguageHelper.GetFriendlyWord("Config_ConfigChangedPleaseRestart"); } set { } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
