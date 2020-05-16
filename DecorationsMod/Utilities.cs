using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

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
        public static string GetSaveFolderPath()
        {
            return Path.Combine(Path.Combine(@".\SNAppData\SavedGames\", SaveLoadManager.main.GetCurrentSlot()), "DecorationsMod");
        }
    }

    public static class RegionHelper
    {
        private const int GEO_FRIENDLYNAME = 8;

        private enum GeoClass : int
        {
            Nation = 16,
            Region = 14
        };

        public enum CountryCode
        {
            EN = 0,
            FR = 1,
            ES = 2,
            TR = 3,
            DE = 4,
            RU = 5
        };

        #region Win32 Declarations to get Windows locale.
        [DllImport("kernel32.dll", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int GetUserGeoID(GeoClass geoClass);

        [DllImport("kernel32.dll")]
        private static extern int GetUserDefaultLCID();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetGeoInfo(int geoid, int geoType, StringBuilder lpGeoData, int cchData, int langid);
        #endregion

        /// <summary>
        /// Returns country code.
        /// </summary>
        public static CountryCode GetCountryCode()
        {
            int geoId = GetUserGeoID(GeoClass.Nation);
            int lcid = GetUserDefaultLCID();
            StringBuilder locationBuffer = new StringBuilder(100);
            GetGeoInfo(geoId, GEO_FRIENDLYNAME, locationBuffer, locationBuffer.Capacity, lcid);

            string countryCode = locationBuffer.ToString().Trim();
            if (countryCode.Length > 1)
            {
                countryCode = countryCode.Substring(0, 2).ToLowerInvariant();
                if (countryCode.CompareTo("fr") == 0)
                    return CountryCode.FR;
                else if (countryCode.CompareTo("es") == 0)
                    return CountryCode.ES;
                else if (countryCode.CompareTo("tr") == 0)
                    return CountryCode.TR;
                else if (countryCode.CompareTo("de") == 0)
                    return CountryCode.DE;
                else if (countryCode.CompareTo("ru") == 0)
                    return CountryCode.RU;
            }
            return CountryCode.EN;
        }
    }

    public static class ImageUtils
    {
        // Scrapped from: https://github.com/RandyKnapp/SubnauticaModSystem/blob/master/SubnauticaModSystem/Common/Utility/ImageUtils.cs
        public static Texture2D LoadTextureFromFile(string imageFilePath)
        {
            if (File.Exists(imageFilePath))
            {
                byte[] imageBytes = File.ReadAllBytes(imageFilePath);
                Texture2D texture2D = new Texture2D(2, 2, TextureFormat.BC7, false);
                if (texture2D.LoadImage(imageBytes))
                    return texture2D;
                else
                    Logger.Log("ERROR: Image located at \"" + imageFilePath + "\" cannot not be loaded.");
            }
            else
                Logger.Log("ERROR: Image located at \"" + imageFilePath + "\" has not been found.");
            return null;
        }
    }
}
