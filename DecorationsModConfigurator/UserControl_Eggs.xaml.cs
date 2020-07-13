using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using static DecorationsModConfigurator.TechTypes;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_Eggs.xaml
    /// </summary>
    public partial class UserControl_Eggs : UserControl, INotifyPropertyChanged
    {
#if DEBUG_EGGS_TECHTYPE_SELECTOR
        private const string LOG_EGGS_SELECTOR = "DEBUG: Eggs recipe updates to {0} TechType [{1}]";
#endif

        public UserControl_Eggs()
        {
            InitializeComponent();

            this.EnableAllEggs = Configuration.Instance.EnableAllEggs;
            this.CreatureEggs_RecipiesResource = Configuration.Instance.CreatureEggs_RecipiesResource;
            this.CreatureEggs_RecipiesResourceAmount = Configuration.Instance.CreatureEggs_RecipiesResourceAmount;

            (this.Content as FrameworkElement).DataContext = this;
        }

        public bool EnableAllEggs { get { return Configuration.Instance.EnableAllEggs; } set { Configuration.Instance.EnableAllEggs = value; } }
        public string CreatureEggs_RecipiesResource
        {
            get { return Configuration.Instance.CreatureEggs_RecipiesResource; }
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
#if DEBUG_EGGS_TECHTYPE_SELECTOR
                    Logger.Log(LOG_EGGS_SELECTOR, "combo", newTechType);
#endif
                }
                else if (TechTypeExtensions.FromString(newTechType, out TechType techType, true) && techType != TechType.None)
                {
                    newTechType = techType.AsString();
#if DEBUG_EGGS_TECHTYPE_SELECTOR
                    Logger.Log(LOG_EGGS_SELECTOR, "game", newTechType);
#endif
                }
#if DEBUG_EGGS_TECHTYPE_SELECTOR
                else // Else, we are in custom (advanced) mode
                    Logger.Log(LOG_EGGS_SELECTOR, "custom", newTechType);
#endif

                Configuration.Instance.CreatureEggs_RecipiesResource = newTechType;
            }
        }
        public string CreatureEggs_RecipiesResource_Custom { get { return CreatureEggs_RecipiesResource; } set { if (value != null) CreatureEggs_RecipiesResource = value; } }
        public int CreatureEggs_RecipiesResourceAmount { get { return Configuration.Instance.CreatureEggs_RecipiesResourceAmount; } set { Configuration.Instance.CreatureEggs_RecipiesResourceAmount = (value < 1 ? 1 : value); } }
        
        public string Config_EggsSettings { get { return LanguageHelper.GetFriendlyWord("Config_EggsSettings"); } set { } }
        public string Config_EnableRegularEggs { get { return LanguageHelper.GetFriendlyWord("Config_EnableRegularEggs"); } set { } }
        public string Config_EnableRegularEggsDescription { get { return LanguageHelper.GetFriendlyWord("Config_EnableRegularEggsDescription"); } set { } }
        public string Config_CreatureEggsResource { get { return LanguageHelper.GetFriendlyWord("Config_CreatureEggsResource"); } set { } }
        public string Config_CreatureEggsResourceAmount { get { return LanguageHelper.GetFriendlyWord("Config_CreatureEggsResourceAmount"); } set { } }

        public void RefreshGUI()
        {
            CB_CreatureEggs_RecipiesResource.RefreshGUI();
            OnPropertyChanged("");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
