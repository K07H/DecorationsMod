using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public static class ConstructableFixer
    {
        /// <summary>Holds ladders directions.</summary>
        public static readonly Dictionary<string, KeyValuePair<int, bool>> LadderDirections = new Dictionary<string, KeyValuePair<int, bool>>();

        public static bool CanDeconstruct_Prefix(Constructable __instance, ref bool __result, out string reason)
        {
            string techTypeStr = __instance.techType.AsString();
            if (techTypeStr.StartsWith("DecorativeLocker", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("DecorativeLockerClosed", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("DecorativeLockerDoor", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("CargoBox01_damaged", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("CargoBox01a", true, CultureInfo.InvariantCulture) ||
                techTypeStr.StartsWith("CargoBox01b", true, CultureInfo.InvariantCulture))
            {
                foreach (Transform tr in __instance.gameObject.transform)
                {
                    if (tr.name.StartsWith("Locker(Clone)", true, CultureInfo.InvariantCulture))
                    {
                        StorageContainer sc = tr.GetComponent<StorageContainer>();
                        if (sc != null && sc.container != null && sc.container.count > 0)
                        {
                            if (Language.main != null)
                            {
                                string notEmptyError = Language.main.Get("DeconstructNonEmptyStorageContainerError");
                                reason = (!string.IsNullOrEmpty(notEmptyError) ? notEmptyError : "Not empty!");
                            }
                            else
                                reason = "Not empty!";
                            __result = false;
                            return false;
                        }
                    }
                }
            }
            reason = null;
            return true;
        }

        public static void Construct_Postfix(Constructable __instance)
        {
            if (__instance.techType == CrafterLogicFixer.OutdoorLadder)
            {
                GameObject ladder = __instance.gameObject;
                if (ladder != null)
                {
                    PrefabIdentifier pid = ladder.GetComponent<PrefabIdentifier>();
                    if (pid != null)
                    {
                        Transform modelTr = ladder.FindChild("OutdoorLadderModel")?.transform;
                        if (modelTr != null)
                        {
                            Vector3 modelPos = modelTr.position;
                            if (BuilderFixer.TempLadderDirections.ContainsKey(modelPos))
                            {
                                int direction = BuilderFixer.TempLadderDirections[modelPos].Key;
                                bool inverted = BuilderFixer.TempLadderDirections[modelPos].Value;
                                LadderDirections[pid.Id] = new KeyValuePair<int, bool>(direction, inverted);
                            }
                        }
                    }
                }
                BuilderFixer.TempLadderDirections.Clear();
            }
        }

        public static void LoadLadderDirections(string saveGame)
        {
            string saveDir = FilesHelper.GetSaveFolderPath(saveGame);
            if (Directory.Exists(saveDir))
            {
                string saveFile = Path.Combine(saveDir, "outdoorladders.txt").Replace('\\', '/');
                if (File.Exists(saveFile))
                {
                    Logger.Log("INFO: Loading outdoor ladder directions from \"" + saveFile + "\".");
                    int cnt = 0;
                    string[] lines = File.ReadAllLines(saveFile, Encoding.UTF8);
                    if (lines != null && lines.Length > 0)
                    {
                        foreach (string line in lines)
                        {
                            if (line.Length > 3 && line.Contains("="))
                            {
                                string[] splitted = line.Split(new char[] { '=' }, StringSplitOptions.None);
                                if (splitted != null && splitted.Length == 3)
                                {
                                    string pid = splitted[0];
                                    bool inverted = splitted[2] == "1";
                                    if (int.TryParse(splitted[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int direction) && direction >= 0 && direction <= 3)
                                    {
                                        LadderDirections[pid] = new KeyValuePair<int, bool>(direction, inverted);
                                        ++cnt;
                                    }
                                }
                            }
                        }
                    }
                    Logger.Log("INFO: {0} outdoor ladder directions were loaded.", cnt);
                }
                else
                    Logger.Log("INFO: No outdoor ladder directions saved at \"" + saveFile + "\".");
            }
            else
                Logger.Log("INFO: No save directory found for outdoor ladders at \"" + saveDir + "\".");
        }

        public static void SaveLadderDirections()
        {
            int cnt = 0;
            string toSave = "";
            if (LadderDirections != null && LadderDirections.Count > 0)
                foreach (KeyValuePair<string, KeyValuePair<int, bool>> direction in LadderDirections)
                {
                    toSave += string.Format(CultureInfo.InvariantCulture, "{0}={1}={2}{3}", direction.Key, direction.Value.Key.ToString(), direction.Value.Value ? "1" : "0", Environment.NewLine);
                    cnt++;
                }
            if (!string.IsNullOrEmpty(toSave))
            {
                string saveDir = FilesHelper.GetSaveFolderPath();
                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);
                string saveFile = Path.Combine(saveDir, "outdoorladders.txt");
                Logger.Log("INFO: Saving {0} outdoor ladder directions to \"{1}\".", cnt, saveFile);
                File.WriteAllText(saveFile, toSave, Encoding.UTF8);
            }
        }
    }
}
