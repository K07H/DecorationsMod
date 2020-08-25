using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
#if BELOWZERO
using TMPro;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
#if SUBNAUTICA
using UnityEngine.UI;
#endif

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

        public static string GetSaveFolderPath(string saveGame)
        {
            string saveDir = FilesHelper.GetSaveFolderPath();
            if (saveDir.Contains("/test/") || saveDir.Contains("\\test\\"))
            {
                if (string.IsNullOrEmpty(saveGame))
                    return null; // If we reach here we don't know what is the game slot name...
                saveDir = saveDir.Replace("/test/", "/" + saveGame + "/").Replace("\\test\\", "\\" + saveGame + "\\").Replace('\\', '/');
            }
            return saveDir;
        }
    }

    public static class RegionHelper
    {
        /// <summary>Supported languages.</summary>
        public static string[] AvailableLanguages = new string[7] { "en", "fr", "es", "de", "ru", "tr", "nl" };

        /// <summary>Supported country codes.</summary>
        public enum CountryCode
        {
            EN = 0,
            FR = 1,
            ES = 2,
            DE = 3,
            RU = 4,
            TR = 5,
            NL = 6
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
                else if (string.Compare(label, "nl", true, CultureInfo.InvariantCulture) == 0)
                    return CountryCode.NL;
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
                case CountryCode.NL:
                    return "nl";
                default:
                    return "en";
            }
        }
    }

    /// <summary>Class used to display messages from menu.</summary>
    public static class MenuMessageHelper
    {
        // Reflected elements
        private static readonly FieldInfo _mainErrorMessage = typeof(ErrorMessage).GetField("main", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo _timeDelayField = typeof(ErrorMessage).GetField("timeDelay", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo _timeFadeOutField = typeof(ErrorMessage).GetField("timeFadeOut", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo _timeInvisibleField = typeof(ErrorMessage).GetField("timeInvisible", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo _messagesField = typeof(ErrorMessage).GetField("messages", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo _onUpdateMethod = typeof(ErrorMessage).GetMethod("OnUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo _releaseEntryMethod = typeof(ErrorMessage).GetMethod("ReleaseEntry", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly Type _messageType = typeof(ErrorMessage).GetNestedType("_Message", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo _getExistingMessageMethod = typeof(ErrorMessage).GetMethod("GetExistingMessage", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo _getEntryMethod = typeof(ErrorMessage).GetMethod("GetEntry", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo _messageEntryField = _messageType.GetField("entry", BindingFlags.Public | BindingFlags.Instance);
        private static readonly FieldInfo _messageMessageTextField = _messageType.GetField("messageText", BindingFlags.Public | BindingFlags.Instance);
        private static readonly FieldInfo _messageNumField = _messageType.GetField("num", BindingFlags.Public | BindingFlags.Instance);
        private static readonly FieldInfo _messageTimeEndField = _messageType.GetField("timeEnd", BindingFlags.Public | BindingFlags.Instance);

        /// <summary>Manually updates ErrorMessage GUI layout.</summary>
        private static void UpdateErrorMessages()
        {
            if (_mainErrorMessage != null)
            {
                ErrorMessage main = (ErrorMessage)_mainErrorMessage.GetValue(null);
                if (main != null && _onUpdateMethod != null)
                    _onUpdateMethod.Invoke(main, null);
            }
        }

        /// <summary>Clears out currently displayed messages in top left corner of the screen.</summary>
        private static void CleanMessages()
        {
            if (_mainErrorMessage == null || _messagesField == null || _messageEntryField == null || _releaseEntryMethod == null)
                return;
            ErrorMessage main = (ErrorMessage)_mainErrorMessage.GetValue(null);
            if (main != null)
            {
                IList messages = _messagesField.GetValue(main) as IList;
                if (messages != null)
                {
                    for (int i = 0; i < messages.Count; i++)
                        _releaseEntryMethod.Invoke(main, new object[] { _messageEntryField.GetValue(messages[i]) });
                    messages.Clear();
                }
            }
        }

        /// <summary>Displays given message in top left corner of the screen.</summary>
        /// <param name="messageText">The message to display.</param>
        private static void AddMessageInternal(string messageText)
        {
            if (string.IsNullOrEmpty(messageText) || _mainErrorMessage == null)
                return;
            ErrorMessage main = (ErrorMessage)_mainErrorMessage.GetValue(null);
            if (main == null || _getExistingMessageMethod == null || _getEntryMethod == null || _messageType == null ||
                _messageEntryField == null || _messageMessageTextField == null || _messageNumField == null || _messageTimeEndField == null ||
                _timeDelayField == null || _timeFadeOutField == null || _timeInvisibleField == null || _messagesField == null)
                return;
            MenuMessageHelper.CleanMessages();
            float timeDelay = (float)_timeDelayField.GetValue(main);
            float timeFadeOut = (float)_timeFadeOutField.GetValue(main);
            float timeInvisible = (float)_timeInvisibleField.GetValue(main);
            object message = _getExistingMessageMethod.Invoke(main, new object[] { messageText });
            if (message == null)
            {
#if SUBNAUTICA
                Text entry = (Text)_getEntryMethod.Invoke(main, null);
#else
                TextMeshProUGUI entry = (TextMeshProUGUI)_getEntryMethod.Invoke(main, null);
#endif
                entry.gameObject.SetActive(true);
                entry.text = messageText;
                message = Activator.CreateInstance(_messageType, true);
                _messageEntryField.SetValue(message, entry);
                _messageMessageTextField.SetValue(message, messageText);
                _messageNumField.SetValue(message, 1);
                _messageTimeEndField.SetValue(message, Time.time + timeDelay + timeFadeOut + timeInvisible);
                if (_messagesField.GetValue(main) is IList messages)
                    messages.Add(message);
                MenuMessageHelper.UpdateErrorMessages();
                return;
            }
#if SUBNAUTICA
            Text entry2 = (Text)_messageEntryField.GetValue(message);
#else
            TMP_Text entry2 = (TMP_Text)_messageEntryField.GetValue(message);
#endif
            _messageTimeEndField.SetValue(message, Time.time + timeDelay + timeFadeOut + timeInvisible);
            int messageNum = ((int)_messageNumField.GetValue(message)) + 1;
            _messageNumField.SetValue(message, messageNum);
            entry2.text = string.Format("{0} (x{1})", messageText, messageNum.ToString());
            MenuMessageHelper.UpdateErrorMessages();
        }

        /// <summary>Displays given message in top left corner of the screen (works from main menu and in-game menu).</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="size">The size of the text.</param>
        public static void AddMessage(string text, string color = "white", int size = 25)
        {
            string toPrint = string.Format("<size={0}><color={1}>{2}</color></size>", size, color, text);
            if (SceneManager.GetSceneByName("Main").isLoaded)
                AddMessageInternal(toPrint);
            else
                ErrorMessage.AddMessage(toPrint);
        }
    }
}
