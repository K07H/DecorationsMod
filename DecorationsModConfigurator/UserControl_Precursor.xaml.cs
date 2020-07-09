using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using static DecorationsModConfigurator.TechTypes;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_Precursor.xaml
    /// </summary>
    public partial class UserControl_Precursor : UserControl, INotifyPropertyChanged
    {
#if DEBUG_PRECURSOR_TECHTYPE_SELECTOR
        private const string LOG_SELECTOR = "DEBUG: {0} recipe updates to {1} TechType [{2}]";
#endif

        public UserControl_Precursor()
        {
            InitializeComponent();

            this.EnablePrecursorTab = Configuration.Instance.EnablePrecursorTab;
            this.PrecursorKeysAll = Configuration.Instance.PrecursorKeysAll;
            this.PrecursorKeys_RecipiesResource = Configuration.Instance.PrecursorKeys_RecipiesResource;
            this.PrecursorKeys_RecipiesResourceAmount = Configuration.Instance.PrecursorKeys_RecipiesResourceAmount;
            this.Relics_RecipiesResource = Configuration.Instance.Relics_RecipiesResource;
            this.Relics_RecipiesResourceAmount = Configuration.Instance.Relics_RecipiesResourceAmount;
            this.AlienRelic1Animation = Configuration.Instance.AlienRelic1Animation;
            this.AlienRelic2Animation = Configuration.Instance.AlienRelic2Animation;
            this.AlienRelic3Animation = Configuration.Instance.AlienRelic3Animation;
            this.AlienRelic4Animation = Configuration.Instance.AlienRelic4Animation;
            this.AlienRelic5Animation = Configuration.Instance.AlienRelic5Animation;
            this.AlienRelic6Animation = Configuration.Instance.AlienRelic6Animation;
            this.AlienRelic7Animation = Configuration.Instance.AlienRelic7Animation;
            this.AlienRelic8Animation = Configuration.Instance.AlienRelic8Animation;
            this.AlienRelic9Animation = Configuration.Instance.AlienRelic9Animation;
            this.AlienRelic10Animation = Configuration.Instance.AlienRelic10Animation;
            this.AlienRelic11Animation = Configuration.Instance.AlienRelic11Animation;

            RefreshVisibilities();

            (this.Content as FrameworkElement).DataContext = this;
        }

        public bool EnablePrecursorTab { get { return Configuration.Instance.EnablePrecursorTab; } set { Configuration.Instance.EnablePrecursorTab = value; RefreshVisibilities(); } }
        public bool PrecursorKeysAll { get { return Configuration.Instance.PrecursorKeysAll; } set { Configuration.Instance.PrecursorKeysAll = value; } }
        public string PrecursorKeys_RecipiesResource
        {
            get { return Configuration.Instance.PrecursorKeys_RecipiesResource; }
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
#if DEBUG_PRECURSOR_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "PrecursorKeys", "combo", newTechType);
#endif
                }
                else if (TechTypeExtensions.FromString(newTechType, out TechType techType, true) && techType != TechType.None)
                {
                    newTechType = techType.AsString();
#if DEBUG_PRECURSOR_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "PrecursorKeys", "game", newTechType);
#endif
                }
#if DEBUG_PRECURSOR_TECHTYPE_SELECTOR
                else // Else, we are in custom (advanced) mode
                    Logger.Log(LOG_SELECTOR, "PrecursorKeys", "custom", newTechType);
#endif

                Configuration.Instance.PrecursorKeys_RecipiesResource = newTechType;
            }
        }
        public string PrecursorKeys_RecipiesResource_Custom { get { return PrecursorKeys_RecipiesResource; } set { if (value != null) PrecursorKeys_RecipiesResource = value; } }
        public int PrecursorKeys_RecipiesResourceAmount { get { return Configuration.Instance.PrecursorKeys_RecipiesResourceAmount; } set { Configuration.Instance.PrecursorKeys_RecipiesResourceAmount = (value < 1 ? 1 : value); } }
        public string Relics_RecipiesResource
        {
            get { return Configuration.Instance.Relics_RecipiesResource; }
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
#if DEBUG_PRECURSOR_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "PrecursorRelics", "combo", newTechType);
#endif
                }
                else if (TechTypeExtensions.FromString(newTechType, out TechType techType, true) && techType != TechType.None)
                {
                    newTechType = techType.AsString();
#if DEBUG_PRECURSOR_TECHTYPE_SELECTOR
                    Logger.Log(LOG_SELECTOR, "PrecursorRelics", "game", newTechType);
#endif
                }
#if DEBUG_PRECURSOR_TECHTYPE_SELECTOR
                else // Else, we are in custom (advanced) mode
                    Logger.Log(LOG_SELECTOR, "PrecursorRelics", "custom", newTechType);
#endif

                Configuration.Instance.Relics_RecipiesResource = newTechType;
            }
        }
        public string Relics_RecipiesResource_Custom { get { return Relics_RecipiesResource; } set { if (value != null) Relics_RecipiesResource = value; } }
        public int Relics_RecipiesResourceAmount { get { return Configuration.Instance.Relics_RecipiesResourceAmount; } set { Configuration.Instance.Relics_RecipiesResourceAmount = (value < 1 ? 1 : value); } }
        public bool AlienRelic1Animation { get { return Configuration.Instance.AlienRelic1Animation; } set { Configuration.Instance.AlienRelic1Animation = value; } }
        public bool AlienRelic2Animation { get { return Configuration.Instance.AlienRelic2Animation; } set { Configuration.Instance.AlienRelic2Animation = value; } }
        public bool AlienRelic3Animation { get { return Configuration.Instance.AlienRelic3Animation; } set { Configuration.Instance.AlienRelic3Animation = value; } }
        public bool AlienRelic4Animation { get { return Configuration.Instance.AlienRelic4Animation; } set { Configuration.Instance.AlienRelic4Animation = value; } }
        public bool AlienRelic5Animation { get { return Configuration.Instance.AlienRelic5Animation; } set { Configuration.Instance.AlienRelic5Animation = value; } }
        public bool AlienRelic6Animation { get { return Configuration.Instance.AlienRelic6Animation; } set { Configuration.Instance.AlienRelic6Animation = value; } }
        public bool AlienRelic7Animation { get { return Configuration.Instance.AlienRelic7Animation; } set { Configuration.Instance.AlienRelic7Animation = value; } }
        public bool AlienRelic8Animation { get { return Configuration.Instance.AlienRelic8Animation; } set { Configuration.Instance.AlienRelic8Animation = value; } }
        public bool AlienRelic9Animation { get { return Configuration.Instance.AlienRelic9Animation; } set { Configuration.Instance.AlienRelic9Animation = value; } }
        public bool AlienRelic10Animation { get { return Configuration.Instance.AlienRelic10Animation; } set { Configuration.Instance.AlienRelic10Animation = value; } }
        public bool AlienRelic11Animation { get { return Configuration.Instance.AlienRelic11Animation; } set { Configuration.Instance.AlienRelic11Animation = value; } }

        public string Config_PrecursorSettings { get { return LanguageHelper.GetFriendlyWord("Config_PrecursorSettings"); } set { } }
        public string Config_EnablePrecursorTab { get { return LanguageHelper.GetFriendlyWord("Config_EnablePrecursorTab"); } set { } }
        public string Config_EnablePrecursorTabDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnablePrecursorTabDescription"); } set { } }
        public string Config_PrecursorKeysAll { get { return LanguageHelper.GetFriendlyWord("Config_PrecursorKeysAll"); } set { } }
        public string Config_PrecursorKeysAllDescription { get { return LanguageHelper.GetFriendlyWord("Config_PrecursorKeysAllDescription"); } set { } }
        public string Config_PrecursorKeysResource { get { return LanguageHelper.GetFriendlyWord("Config_PrecursorKeysResource"); } set { } }
        public string Config_PrecursorKeysResourceAmount { get { return LanguageHelper.GetFriendlyWord("Config_PrecursorKeysResourceAmount"); } set { } }
        public string Config_RelicRecipiesResource { get { return LanguageHelper.GetFriendlyWord("Config_RelicRecipiesResource"); } set { } }
        public string Config_RelicRecipiesResourceAmount { get { return LanguageHelper.GetFriendlyWord("Config_RelicRecipiesResourceAmount"); } set { } }
        public string Config_EnablePrecursorRelicAnims { get { return LanguageHelper.GetFriendlyWord("Config_EnablePrecursorRelicAnims"); } set { } }
        public string Config_AlienRelic1Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic1Name"); } set { } }
        public string Config_AlienRelic2Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic2Name"); } set { } }
        public string Config_AlienRelic3Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic3Name"); } set { } }
        public string Config_AlienRelic4Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic4Name"); } set { } }
        public string Config_AlienRelic5Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic5Name"); } set { } }
        public string Config_AlienRelic6Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic6Name"); } set { } }
        public string Config_AlienRelic7Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic7Name"); } set { } }
        public string Config_AlienRelic8Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic8Name"); } set { } }
        public string Config_AlienRelic9Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic9Name"); } set { } }
        public string Config_AlienRelic10Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic10Name"); } set { } }
        public string Config_AlienRelic11Name { get { return LanguageHelper.GetFriendlyWord("AlienRelic11Name"); } set { } }

        public void RefreshGUI()
        {
            CB_PrecursorKeys_RecipiesResource.RefreshGUI();
            CB_Relics_RecipiesResource.RefreshGUI();
            OnPropertyChanged("");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void RefreshVisibilities()
        {
            Visibility visibility = (CB_EnablePrecursorTab.IsChecked != null && CB_EnablePrecursorTab.IsChecked.HasValue && CB_EnablePrecursorTab.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
            GRD_PrecursorKeysAll.Visibility = visibility;
            GRD_PrecursorKeys_RecipiesResource.Visibility = visibility;
            GRD_PrecursorKeys_RecipiesResourceAmount.Visibility = visibility;
            GRD_Relics_RecipiesResource.Visibility = visibility;
            GRD_Relics_RecipiesResourceAmount.Visibility = visibility;
            TB_EnablePrecursorRelicAnims.Visibility = visibility;
            GRD_AlienRelic1Animation.Visibility = visibility;
            GRD_AlienRelic2Animation.Visibility = visibility;
            GRD_AlienRelic3Animation.Visibility = visibility;
            GRD_AlienRelic4Animation.Visibility = visibility;
            GRD_AlienRelic5Animation.Visibility = visibility;
            GRD_AlienRelic6Animation.Visibility = visibility;
            GRD_AlienRelic7Animation.Visibility = visibility;
            GRD_AlienRelic8Animation.Visibility = visibility;
            GRD_AlienRelic9Animation.Visibility = visibility;
            GRD_AlienRelic10Animation.Visibility = visibility;
            GRD_AlienRelic11Animation.Visibility = visibility;
        }

        private void CB_EnablePrecursorTab_Checked(object sender, RoutedEventArgs e)
        {
            RefreshVisibilities();
        }

        private void CB_EnablePrecursorTab_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshVisibilities();
        }
    }
}
