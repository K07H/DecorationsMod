using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DecorationsMod
{
    internal static class Logger
    {
        internal static void Log(string text, params object[] args)
        {
            if (args != null && args.Length > 0)
                text = string.Format(text, args);
            if (CyclopsDockingMod_EntryPoint._logger != null)
                CyclopsDockingMod_EntryPoint._logger.LogDebug($"[DecorationsMod] {text}");
            else
                Console.WriteLine($"[DecorationsMod] {text}");
        }

#if DEBUG
        internal static string ItemActionToString(ItemAction action)
        {
            string res = "";
            
            if ((action & ItemAction.Assign) != ItemAction.None)
                res += "ASSIGN ";
            if ((action & ItemAction.Drop) != ItemAction.None)
                res += "DROP ";
            if ((action & ItemAction.Eat) != ItemAction.None)
                res += "EAT ";
            if ((action & ItemAction.Equip) != ItemAction.None)
                res += "EQUIP ";
            if ((action & ItemAction.Swap) != ItemAction.None)
                res += "SWAP ";
            if ((action & ItemAction.Switch) != ItemAction.None)
                res += "SWITCH ";
            if ((action & ItemAction.Unequip) != ItemAction.None)
                res += "UNEQUIP ";
            if ((action & ItemAction.Use) != ItemAction.None)
                res += "USE ";

            if (res.Length > 0)
                res = res.Substring(0, res.Length - 1);
            return res;
        }

        internal static void PrintObject(System.Object o, string indent = "\t")
        {
            Type type = o.GetType();
            string init = "DEBUG: Component [" + type.Name + "] " + indent;
            Logger.Log(init + "Attributes:");
            FieldInfo[] fi = type.GetFields();
            if (fi == null || fi.Length <= 0)
                Logger.Log(init + " None");
            else
                foreach (FieldInfo f in fi)
                {
                    string val;
                    try { val = f.GetValue(o)?.ToString(); if (val == null) val = "null"; }
                    catch { val = "?"; }
                    Logger.Log(init + " * " + f.ToString() + " = " + val);
                }
            Logger.Log(init + "Properties:");
            PropertyInfo[] pi = type.GetProperties();
            if (pi == null || pi.Length <= 0)
                Logger.Log(init + " None");
            else
                foreach (PropertyInfo p in pi)
                {
                    string val;
                    try { val = p.GetValue(o, null)?.ToString(); if (val == null) val = "null"; }
                    catch { val = "?"; }
                    Logger.Log(init + " * " + p.ToString() + " = " + val);
                }
        }

        internal static void PrintTransform(Transform tr, bool details = false, string indent = "\t")
        {
            if (tr != null)
            {
                Logger.Log("DEBUG: Transform " + indent + "name=[" + tr.name + "] localPos=[" + tr.localPosition.x.ToString() + ";" + tr.localPosition.y.ToString() + ";" + tr.localPosition.z.ToString() + "] localAngles=[" + tr.localEulerAngles.x.ToString() + ";" + tr.localEulerAngles.y.ToString() + ";" + tr.localEulerAngles.z.ToString() + "] localScale=[" + tr.localScale.x.ToString() + ";" + tr.localScale.y.ToString() + ";" + tr.localScale.z.ToString() + "] pos=[" + tr.position.x.ToString() + ";" + tr.position.y.ToString() + ";" + tr.position.z.ToString() + "] angles=[" + tr.eulerAngles.x.ToString() + ";" + tr.eulerAngles.y.ToString() + ";" + tr.eulerAngles.z.ToString() + "]");
                foreach (Component c in tr.GetComponents<Component>())
                    if (c.GetType() != typeof(Transform))
                    {
                        Logger.Log("DEBUG: Transform " + indent + " => component type=[" + c.GetType().ToString() + "] name=[" + c.name + "]");
                        if (details)
                            PrintObject(c, indent + "\t");
                    }
                string newIndent = indent + "\t";
                foreach (Transform child in tr)
                    PrintTransform(child, details, newIndent);
            }
        }
#endif
    }

    internal static class FilesHelper
    {
        public static string GetSaveFolderPath() => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../SNAppData/SavedGames/", SaveLoadManager.main.GetCurrentSlot(), "DecorationsMod")).Replace('\\', '/');

        public static string GetSaveFolderPath(string saveGame)
        {
            string saveDir = FilesHelper.GetSaveFolderPath();
            if (saveDir.Contains("/test/"))
            {
                if (string.IsNullOrEmpty(saveGame))
                    return null; // If we reach here we don't know what is the game slot name...
                saveDir = saveDir.Replace("/test/", "/" + saveGame + "/");
            }
            return saveDir.Replace('\\', '/');
        }

        public static string Combine(string path1, string path2) => Path.Combine(path1, path2).Replace('\\', '/');
        public static string Combine(string path1, string path2, string path3) => Path.Combine(path1, path2, path3).Replace('\\', '/');
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
                TextMeshProUGUI entry = (TextMeshProUGUI)_getEntryMethod.Invoke(main, null);
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
            TMP_Text entry2 = (TMP_Text)_messageEntryField.GetValue(message);
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

    public static class BaseBioReactorHelper
    {
        //private static readonly Dictionary<TechType, float> charge = new Dictionary<TechType, float>(TechTypeExtensions.sTechTypeComparer)
        public static readonly FieldInfo _chargeField = typeof(BaseBioReactor).GetField("charge", BindingFlags.NonPublic | BindingFlags.Static);

        public static void SetBioReactorCharge(TechType techType, float charge)
        {
            Dictionary<TechType, float> chargeDict = (Dictionary<TechType, float>)_chargeField.GetValue(null);
            chargeDict.Add(techType, charge);
        }
    }
}
