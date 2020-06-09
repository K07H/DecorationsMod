using System;
using System.Globalization;
using System.IO;

namespace DecorationsMod
{
    internal static class Logger
    {
        internal static void Log(string text, params object[] args)
        {
            if (args != null && args.Length > 0)
                text = string.Format(text, args);
            Console.WriteLine($"[DecorationsMod] {text}");
        }
    }

    internal static class FilesHelper
    {
        public static string GetSaveFolderPath() => Path.Combine(Path.Combine(@".\SNAppData\SavedGames\", SaveLoadManager.main.GetCurrentSlot()), "DecorationsMod");
    }

    public static class RegionHelper
    {
        /// <summary>Supported languages.</summary>
        public static string[] AvailableLanguages = new string[6] { "en", "fr", "es", "de", "ru", "tr" };

        /// <summary>Supported country codes.</summary>
        public enum CountryCode
        {
            EN = 0,
            FR = 1,
            ES = 2,
            DE = 3,
            RU = 4,
            TR = 5
        };

        /// <summary>Returns default country code.</summary>
        public static CountryCode GetDefaultCountryCode() => GetCountryCodeFromLabel(CultureInfo.InstalledUICulture?.TwoLetterISOLanguageName);

        /// <summary>Returns country code from language label.</summary>
        /// <param name="label">Language label.</param>
        /// <returns>Returns the associated country code.</returns>
        public static CountryCode GetCountryCodeFromLabel(string label)
        {
            if (!string.IsNullOrEmpty(label) && label.Length == 2)
            {
                if (string.Compare(label, "fr", true, CultureInfo.InvariantCulture) == 0)
                    return CountryCode.FR;
                else if (string.Compare(label, "ru", true, CultureInfo.InvariantCulture) == 0)
                    return CountryCode.RU;
                else if (string.Compare(label, "tr", true, CultureInfo.InvariantCulture) == 0)
                    return CountryCode.TR;
                else if (string.Compare(label, "de", true, CultureInfo.InvariantCulture) == 0)
                    return CountryCode.DE;
                else if (string.Compare(label, "es", true, CultureInfo.InvariantCulture) == 0)
                    return CountryCode.ES;
            }
            return CountryCode.EN;
        }

        /// <summary>Returns language label from country code.</summary>
        /// <param name="code">Country code.</param>
        /// <returns>Returns the associated language label.</returns>
        public static string GetCountryLabelFromCode(CountryCode code)
        {
            switch (code)
            {
                case CountryCode.FR:
                    return "fr";
                case CountryCode.DE:
                    return "de";
                case CountryCode.ES:
                    return "es";
                case CountryCode.RU:
                    return "ru";
                case CountryCode.TR:
                    return "tr";
                default:
                    return "en";
            }
        }
    }
}
