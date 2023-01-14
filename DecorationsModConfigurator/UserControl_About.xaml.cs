using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_About.xaml
    /// </summary>
    public partial class UserControl_About : UserControl, INotifyPropertyChanged
    {
        public UserControl_About()
        {
            InitializeComponent();

            /*
            string configuratorFolder = Path.GetDirectoryName(Uri.UnescapeDataString(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));
            string modJsonFile = Uri.UnescapeDataString(new Uri(Path.Combine(configuratorFolder, "..\\mod.json")).AbsolutePath);
            string version = null;
            if (File.Exists(modJsonFile))
            {
                string[] lines = File.ReadAllLines(modJsonFile);
                if (lines != null)
                {
                    string toSearch = "Version\": \"";
                    foreach (string line in lines)
                    {
                        int stt = line.IndexOf(toSearch);
                        if (stt > 0 && line.Length > stt + 1)
                        {
                            stt += toSearch.Length;
                            int end = line.IndexOf("\"", stt + 1);
                            if (end > stt)
                            {
                                version = line.Substring(stt, end - stt);
                                break;
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(version))
                this.DecorationsModVersion = version;
            else
                this.DecorationsModVersion = "ERROR: Cannot find \"mod.json\" at \"" + modJsonFile + "\".";
            */
            this.DecorationsModVersion = "2.0.3";

            string configuratorVersion = typeof(UserControl_About).Assembly.GetName().Version.ToString();
            int pos = configuratorVersion.LastIndexOf('.');
            if (pos > 3)
                configuratorVersion = configuratorVersion.Substring(0, pos);
            this.ConfiguratorVersion = configuratorVersion;

            IMG_SubnauticaFrance.Visibility = (LanguageHelper.UserLanguage == LanguageHelper.CountryCode.FR) ? Visibility.Visible : Visibility.Collapsed;
            IMG_SubnauticaWiki.Visibility = (LanguageHelper.UserLanguage == LanguageHelper.CountryCode.RU) ? Visibility.Visible : Visibility.Collapsed;

            (this.Content as FrameworkElement).DataContext = this;
        }

        public string DecorationsModVersion { get; set; }
        public string ConfiguratorVersion { get; set; }

        public string Config_About { get { return LanguageHelper.GetFriendlyWord("Config_TabAbout"); } set { } }
        public string Config_DecorationsModVersion { get { return LanguageHelper.GetFriendlyWord("Config_DecorationsModVersion"); } set { } }
        public string Config_DecorationsModConfiguratorVersion { get { return LanguageHelper.GetFriendlyWord("Config_DecorationsModConfiguratorVersion"); } set { } }
        public string Config_DecorationsModAuthor { get { return LanguageHelper.GetFriendlyWord("Config_DecorationsModAuthor"); } set { } }

        public string Config_ContactMeDescription { get { return LanguageHelper.GetFriendlyWord("Config_ContactMeDescription"); } set { } }
        public string Config_ModdingDiscordDescription { get { return LanguageHelper.GetFriendlyWord("Config_ModdingDiscordDescription"); } set { } }
        public string Config_ModdingDiscordSecondaryDescription { get { return LanguageHelper.GetFriendlyWord("Config_ModdingDiscordSecondaryDescription"); } set { } }
        public string Config_ModdingDiscordURL { get { return LanguageHelper.GetFriendlyWord("Config_ModdingDiscordURL"); } set { } }
        public string Config_ModdingDiscordSecondaryURL { get { return LanguageHelper.GetFriendlyWord("Config_ModdingDiscordSecondaryURL"); } set { } }

        public void RefreshGUI()
        {
            GRD_SecondaryDiscordLink.Visibility = (LanguageHelper.UserLanguage == LanguageHelper.CountryCode.FR || LanguageHelper.UserLanguage == LanguageHelper.CountryCode.RU) ? Visibility.Visible : Visibility.Collapsed;
            IMG_SubnauticaFrance.Visibility = LanguageHelper.UserLanguage == LanguageHelper.CountryCode.FR ? Visibility.Visible : Visibility.Collapsed;
            IMG_SubnauticaWiki.Visibility = LanguageHelper.UserLanguage == LanguageHelper.CountryCode.RU ? Visibility.Visible : Visibility.Collapsed;
            OnPropertyChanged("");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try { Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)); }
            catch { MessageBox.Show("Could not open URL in web browser.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); }
            e.Handled = true;
        }

        private void ContactMeMenuItem_Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("https://discordapp.com/users/329259830378364930");
        }

        private void ContactMeMenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            HL_ContactMeDiscordLink.DoClick();
        }

        private void PrimaryMenuItem_Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Config_ModdingDiscordURL);
        }

        private void PrimaryMenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            HL_ModdingDiscordPrimaryLink.DoClick();
        }

        private void SecondaryMenuItem_Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Config_ModdingDiscordSecondaryURL);
        }

        private void SecondaryMenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            HL_ModdingDiscordSecondaryLink.DoClick();
        }
    }
}
