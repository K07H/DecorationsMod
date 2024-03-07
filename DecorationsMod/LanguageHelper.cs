using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace DecorationsMod
{
    public static class LanguageHelper
    {
        private static string _language = CultureInfo.InstalledUICulture.TwoLetterISOLanguageName;

        public static string Language { 
            get { return _language; }
            set {
                if (_language != value) //prevent reloading of lang files if the language didn't change
                {
                    _language = value;
                    Mappings = LoadMappings();
                }
            }
        }

        private static Dictionary<string, string> Mappings = LoadMappings();

        private static Dictionary<string, string> LoadMappings()
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            LoadStrings(Language, mappings);
            if (Language != "en") LoadStrings("en", mappings); //use english mappings as a fallback
            return mappings;
        }

        private static string getLangFilePath(string region)
        {
            return FilesHelper.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets/lang/" + region + ".json");
        }

        private static void LoadStrings(string region, Dictionary<string, string> existingStrings)
        {
            string langFile = getLangFilePath(region);
            if (!File.Exists(langFile))
                Logger.Error("Failed to load missing language file " + langFile);
            else
            {
                JObject jObject = JObject.Parse(File.ReadAllText(langFile));
                try
                {
                    Dictionary<string, string> strings = jObject.ToObject<Dictionary<string, string>>();
                    foreach (KeyValuePair<string, string> mapping in strings)
                        if (!existingStrings.ContainsKey(mapping.Key))
                            existingStrings.Add(mapping.Key, mapping.Value.Replace("\n", Environment.NewLine)); //replace original newline with environment's newline
                }
                catch (Exception ex)
                {
                    Logger.Error("Failed to load invalid language file " + langFile);
                }
            }
        }

        /// <summary>List of tooltip language strings.</summary>
        public static readonly string[] Tooltips = new string[13]
        {
            "LampTooltip",
            "LampTooltipCompact",
            "SwitchSeamothModel",
            "SwitchExosuitModel",
            "CyclopsDollTooltip",
            "CyclopsDollTooltipCompact",
            "AdjustCargoBoxSize",
            "AdjustForkliftSize",
            "AdjustWarperSpecimenSize",
            "DisplayCoveTreeEggs",
            "CustomPictureFrameTooltip",
            "CustomPictureFrameTooltipCompact",
            "OpenCustomStorage"
        };

        /// <summary>Returns translated text based on selected language.</summary>
        /// <param name="word">The ID of the text to translate.</param>
        public static string GetFriendlyWord(string word)
        {
            if (Mappings.ContainsKey(word))
                return Mappings[word];
            else
                return "<Untranslated key> " + word;
        }

        /// <summary>Returns formatted translated text based on selected language.</summary>
        /// <param name="word">The ID of the text to translate.</param>
        /// <param name="args">objects to format the string with</param>
        public static string GetFriendlyWord(string word, params object[] args)
        {
            return String.Format(GetFriendlyWord(word), args);
        }
    }
}
