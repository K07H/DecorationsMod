using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

            string configuratorVersion = typeof(UserControl_About).Assembly.GetName().Version.ToString();
            int pos = configuratorVersion.LastIndexOf('.');
            if (pos > 3)
                configuratorVersion = configuratorVersion.Substring(0, pos);
            this.ConfiguratorVersion = configuratorVersion;

            (this.Content as FrameworkElement).DataContext = this;
        }

        public string DecorationsModVersion { get; set; }
        public string ConfiguratorVersion { get; set; }

        public string Config_About { get { return LanguageHelper.GetFriendlyWord("Config_TabAbout"); } set { } }
        public string Config_DecorationsModVersion { get { return LanguageHelper.GetFriendlyWord("Config_DecorationsModVersion"); } set { } }
        public string Config_DecorationsModConfiguratorVersion { get { return LanguageHelper.GetFriendlyWord("Config_DecorationsModConfiguratorVersion"); } set { } }
        public string Config_DecorationsModAuthor { get { return LanguageHelper.GetFriendlyWord("Config_DecorationsModAuthor"); } set { } }

        public void RefreshGUI() => OnPropertyChanged("");

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
