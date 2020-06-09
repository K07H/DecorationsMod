using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace DecorationsMod.Fixers
{
    public class KnownTechFixer
    {
        public static Dictionary<int, bool> AddedNotifications = new Dictionary<int, bool>();
        public static bool AddNotification(TechType techType, bool builderNotification = true, bool blueprintsNotification = true, bool craftNotification = false)
        {
            int techTypeId = (int)techType;
            if (KnownTechFixer.AddedNotifications.ContainsKey(techTypeId) && !KnownTechFixer.AddedNotifications[techTypeId])
            {
                if (builderNotification)
                {
                    try { NotificationManager.main.Add(NotificationManager.Group.Builder, Convert.ToString(techTypeId, CultureInfo.InvariantCulture), 2f, 2f); }
                    catch { Logger.Log("WARNING: Could not add builder notification for tech type [" + techType.AsString() + "]."); }
                }
                if (blueprintsNotification)
                {
                    try { NotificationManager.main.Add(NotificationManager.Group.Blueprints, Convert.ToString(techTypeId, CultureInfo.InvariantCulture), 3f, 3f); }
                    catch { Logger.Log("WARNING: Could not add blueprint notification for tech type [" + techType.AsString() + "]."); }
                }
                if (craftNotification)
                {
                    try { NotificationManager.main.Add(NotificationManager.Group.CraftTree, Convert.ToString(techTypeId, CultureInfo.InvariantCulture), 3f, 3f); }
                    catch { Logger.Log("WARNING: Could not add craft notification for tech type [" + techType.AsString() + "]."); }
                }
                KnownTechFixer.AddedNotifications[techTypeId] = true;
                return true;
            }
            return false;
        }

        public static void LoadAddedNotifications(string saveGame)
        {
            string saveDir = FilesHelper.GetSaveFolderPath();
            if (saveDir.Contains("/test/") || saveDir.Contains("\\test\\"))
            {
                if (string.IsNullOrEmpty(saveGame))
                    return; // If we reach here we don't know what is the game slot name...
                saveDir = saveDir.Replace("/test/", "/" + saveGame + "/").Replace("\\test\\", "\\" + saveGame + "\\");
            }
            if (Directory.Exists(saveDir))
            {
                string saveFile = Path.Combine(saveDir, "discovered.txt");
                if (File.Exists(saveFile))
                {
                    Logger.Log("INFO: Loading discoveries from \"" + saveFile + "\".");
                    int cnt = 0;
                    string[] lines = File.ReadAllLines(saveFile, Encoding.UTF8);
                    if (lines != null && lines.Length > 0)
                    {
                        foreach (string line in lines)
                        {
                            if (line.Length > 5 && line.Contains("="))
                            {
                                string[] splitted = line.Split(new char[] { '=' }, StringSplitOptions.None);
                                if (splitted != null && splitted.Length == 2)
                                {
                                    if (int.TryParse(splitted[0], out int techTypeId) && techTypeId >= 0)
                                    {
                                        bool discovered = !(string.Compare(splitted[1], "false", true, CultureInfo.InvariantCulture) == 0);
                                        if (AddedNotifications.ContainsKey(techTypeId))
                                            AddedNotifications[techTypeId] = discovered;
                                        else
                                            AddedNotifications.Add(techTypeId, discovered);
                                        if (discovered)
                                            ++cnt;
                                    }
                                }
                            }
                        }
                    }
                    Logger.Log("INFO: Discoveries loaded. Player made {0}/{1} discoveries ({2} remaining).", cnt, AddedNotifications.Count, AddedNotifications.Count - cnt);
                }
                else
                    Logger.Log("INFO: No discoveries saved at \"" + saveFile + "\".");
            }
            else
                Logger.Log("INFO: No discoveries directory saved at \"" + saveDir + "\".");
        }

        public static void SaveAddedNotifications()
        {
            int cnt = 0;
            string toSave = "";
            string toLog = "";
            if (AddedNotifications != null && AddedNotifications.Count > 0)
            {
                foreach (KeyValuePair<int, bool> notif in AddedNotifications)
                {
                    if (notif.Value)
                    {
                        toSave += string.Format("{0}={1}{2}", notif.Key, notif.Value, Environment.NewLine);
                        toLog += notif.Key.ToString() + ";";
                        ++cnt;
                    }
                }
            }
            if (!string.IsNullOrEmpty(toSave))
            {
                string saveDir = FilesHelper.GetSaveFolderPath();
                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);
                string saveFile = Path.Combine(saveDir, "discovered.txt");
                if (toLog.EndsWith(";"))
                    toLog = toLog.Substring(0, toLog.Length - 1);
                Logger.Log("INFO: Saving {0}/{1} discoveries to \"{2}\" (Discovered TechTypes: {3}).", cnt, AddedNotifications.Count, saveFile, toLog);
                File.WriteAllText(saveFile, toSave, Encoding.UTF8);
            }
        }

        public static bool LockReturn(ref TechUnlockState unlockState, ref int unlocked, ref int total)
        {
            unlocked = 0;
            total = 1;
            unlockState = TechUnlockState.Hidden;
            return false;
        }

        /// <summary>Sets unlock state for new items.</summary>
        /// <param name="__result">Defines if the item is locked, hidden or visible.</param>
        /// <param name="techType">The tech type of the item.</param>
        /// <param name="unlocked">The number of currently unlocked fragments.</param>
        /// <param name="total">The total number of fragments to unlock before the item gets unlocked.</param>
        /// <returns>Returns false if current item is an item from this mod (original function not called), and returns true otherwise (original function is called).</returns>
        public static bool GetTechUnlockState_Prefix(ref TechUnlockState __result, TechType techType, ref int unlocked, ref int total)
        {
            if (techType != TechType.None && ConfigSwitcher.AddItemsWhenDiscovered && GameModeUtils.RequiresBlueprints())
            {
                if (techType == CrafterLogicFixer.SeamothDoll || techType == CrafterLogicFixer.SeamothFragment1 || techType == CrafterLogicFixer.SeamothFragment2 || techType == CrafterLogicFixer.SeamothFragment3 || techType == CrafterLogicFixer.SeamothFragment4 || techType == CrafterLogicFixer.SeamothFragment5)
                {
                    if (KnownTech.GetTechUnlockState(TechType.Seamoth) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.ExosuitDoll)
                {
                    if (KnownTech.GetTechUnlockState(TechType.Exosuit) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.CuddleFishDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("CuteFish"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.GhostLeviathanDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("GhostLeviathan") && !PDAEncyclopedia.ContainsEntry("GhostLeviathanJuvenile"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.ReaperLeviathanDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("ReaperLeviathan"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.ReefBackDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("Reefback"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.SeaDragonDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("SeaDragon"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.SeaEmperorDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("SeaEmperor") && !PDAEncyclopedia.ContainsEntry("SeaEmperorLeviathan") && !PDAEncyclopedia.ContainsEntry("SeaEmperorBaby"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.SeaTreaderDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("SeaTreader"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.ReaperSkeletonDoll || techType == CrafterLogicFixer.ReaperSkullDoll)
                {
                    if (!PDAEncyclopedia.ContainsEntry("LavaZone_ReaperSkeleton"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.SeaDragonSkeleton)
                {
                    if (!PDAEncyclopedia.ContainsEntry("LostRiverBase_SeaDragonSkeleton"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.GenericSkeleton1 || techType == CrafterLogicFixer.GenericSkeleton2 || techType == CrafterLogicFixer.GenericSkeleton3)
                {
                    if (!PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_Bones") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_GiantFishSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_SeaDragonSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("LostRiver_BonesfieldHugeSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_CaveSkeletonScan") &&
                        !PDAEncyclopedia.ContainsEntry("LavaZone_ReaperSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_Cache_LostRiverBonesScan"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact1)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact1"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact2)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact2"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact3)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact3"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact4)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact4"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact5)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact5"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact6)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact6"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact7)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact7"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact8)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact8"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact9)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact10"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact10)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact11"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.AlienArtefact11)
                {
                    if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact12"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.BenchSmall || techType == CrafterLogicFixer.BenchMedium)
                {
                    if (KnownTech.GetTechUnlockState(TechType.Bench) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.SofaCorner || techType == CrafterLogicFixer.Sofa1 || techType == CrafterLogicFixer.Sofa2 || techType == CrafterLogicFixer.Sofa3)
                {
                    if (!(KnownTech.GetTechUnlockState(TechType.Bench) == TechUnlockState.Available &&
                        KnownTech.GetTechUnlockState(TechType.StarshipChair) == TechUnlockState.Available &&
                        KnownTech.GetTechUnlockState(TechType.StarshipChair2) == TechUnlockState.Available &&
                        KnownTech.GetTechUnlockState(TechType.StarshipChair3) == TechUnlockState.Available))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.Stool)
                {
                    if (!(KnownTech.GetTechUnlockState(TechType.StarshipChair) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.StarshipChair2) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.StarshipChair3) == TechUnlockState.Available))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.Stool);
                }
                else if (techType == CrafterLogicFixer.LongPlanterA)
                {
                    if (KnownTech.GetTechUnlockState(TechType.PlanterBox) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.LongPlanterA);
                }
                else if (techType == CrafterLogicFixer.LongPlanterB)
                {
                    if (KnownTech.GetTechUnlockState(TechType.FarmingTray) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.LongPlanterB);
                }
                else if (techType == CrafterLogicFixer.EmptyDesk)
                {
                    if (KnownTech.GetTechUnlockState(TechType.StarshipDesk) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.EmptyDesk);
                }
                else if (techType == CrafterLogicFixer.CustomizablePictureFrame)
                {
                    if (KnownTech.GetTechUnlockState(TechType.PictureFrame) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.CustomizablePictureFrame);
                }
                else if (techType == CrafterLogicFixer.ReactorLamp)
                {
                    if (!(KnownTech.GetTechUnlockState(TechType.Spotlight) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.LEDLight) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.Techlight) == TechUnlockState.Available))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.ReactorLamp);
                }
                else if (techType == CrafterLogicFixer.AlienPillar)
                {
                    if (!(PDAEncyclopedia.ContainsEntry("PrecursorTeleporter") ||
                        PDAEncyclopedia.ContainsEntry("PrecursorKeyTerminal") ||
                        PDAEncyclopedia.ContainsEntry("PrecursorEnergyCore") ||
                        PDAEncyclopedia.ContainsEntry("Precursor_Cache_LostRiverLabTable")))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.AlienPillar);
                }
                else if (techType == CrafterLogicFixer.WarperPart1)
                {
                    if (!PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_WarperScan"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(CrafterLogicFixer.WarperPart1);
                }
                else if (techType == CrafterLogicFixer.WarperPart2 || techType == CrafterLogicFixer.WarperPart3 || techType == CrafterLogicFixer.WarperPart4 ||
                     techType == CrafterLogicFixer.WarperPart5 || techType == CrafterLogicFixer.WarperPart6 || techType == CrafterLogicFixer.WarperPart7 ||
                     techType == CrafterLogicFixer.WarperPart8 || techType == CrafterLogicFixer.WarperPart9 || techType == CrafterLogicFixer.WarperPart10 ||
                     techType == CrafterLogicFixer.WarperPart11 || techType == CrafterLogicFixer.WarperPart12)
                {
                    if (!(PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_ProductionLine") || PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_WarperParts")))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType, false, false, true);
                }
                else if (techType == CrafterLogicFixer.LabCart || techType == CrafterLogicFixer.SpecimenAnalyzer)
                {
                    if (KnownTech.GetTechUnlockState(TechType.BaseWaterPark) != TechUnlockState.Available)
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
            }
            if (techType != TechType.None && ConfigSwitcher.AddWaterSeedsWhenDiscovered && GameModeUtils.RequiresBlueprints())
            {
                if (techType == CrafterLogicFixer.FloatingStone1)
                {
                    if (!PDAEncyclopedia.ContainsEntry("FloatingStones"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.BrineLily)
                {
                    if (!PDAEncyclopedia.ContainsEntry("BlueLostRiverLilly"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.Amoeba)
                {
                    if (!PDAEncyclopedia.ContainsEntry("BlueAmoeba"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.PyroCoral1 || techType == CrafterLogicFixer.PyroCoral2 || techType == CrafterLogicFixer.PyroCoral3)
                {
                    if (!PDAEncyclopedia.ContainsEntry("RedTipRockThings"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.CrabClawKelp1 || techType == CrafterLogicFixer.CrabClawKelp2 || techType == CrafterLogicFixer.CrabClawKelp3)
                {
                    if (!PDAEncyclopedia.ContainsEntry("BlueTipLostRiverPlant"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.BrownTubes1 || techType == CrafterLogicFixer.BrownTubes2 || techType == CrafterLogicFixer.BrownTubes3)
                {
                    if (!PDAEncyclopedia.ContainsEntry("EarthenCoralTubes"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType);
                }
                else if (techType == CrafterLogicFixer.CoveTree)
                {
                    if (!PDAEncyclopedia.ContainsEntry("TreeCoveTree"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType, false, false, true);
                }
                else if (techType == CrafterLogicFixer.MushroomTree1 || techType == CrafterLogicFixer.MushroomTree2)
                {
                    if (!PDAEncyclopedia.ContainsEntry("TreeLeech") && !PDAEncyclopedia.ContainsEntry("TreeMushroom") && !PDAEncyclopedia.ContainsEntry("TreeMushroomPiece"))
                        return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                    else
                        KnownTechFixer.AddNotification(techType, false, false, true);
                }
            }
            if (techType != TechType.None && !ConfigSwitcher.EnableEggsAtStart && GameModeUtils.RequiresBlueprints())
            {
                if (techType == CrafterLogicFixer.GhostLeviathanEggs)
                {
                    if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                    {
                        if (!PDAEncyclopedia.ContainsEntry("TreeCoveTree") && !PDAEncyclopedia.ContainsEntry("GhostLeviathan") && !PDAEncyclopedia.ContainsEntry("GhostLeviathanJuvenile"))
                            return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                        else
                            KnownTechFixer.AddNotification(CrafterLogicFixer.GhostLeviathanEggs, false, false, true);
                    }
                    else
                    {
                        if (!PDAEncyclopedia.ContainsEntry("TreeCoveTree"))
                            return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                        else
                            KnownTechFixer.AddNotification(CrafterLogicFixer.GhostLeviathanEggs, false, false, true);
                    }
                }
                else if (techType == CrafterLogicFixer.SeaDragonEgg)
                {
                    if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                    {
                        if (!PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_LeviathanEggShellScan") && !PDAEncyclopedia.ContainsEntry("SeaDragon"))
                            return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                        else
                            KnownTechFixer.AddNotification(CrafterLogicFixer.SeaDragonEgg, false, false, true);
                    }
                    else
                    {
                        if (!PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_LeviathanEggShellScan"))
                            return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                        else
                            KnownTechFixer.AddNotification(CrafterLogicFixer.SeaDragonEgg, false, false, true);
                    }
                }
                else if (techType == CrafterLogicFixer.SeaEmperorEgg)
                {
                    if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                    {
                        if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonEggChamberEmperorEgg") && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonAquariumIncubatorEggs") && !PDAEncyclopedia.ContainsEntry("SeaEmperor") && !PDAEncyclopedia.ContainsEntry("SeaEmperorLeviathan") && !PDAEncyclopedia.ContainsEntry("SeaEmperorBaby"))
                            return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                        else
                            KnownTechFixer.AddNotification(CrafterLogicFixer.SeaEmperorEgg, false, false, true);
                    }
                    else
                    {
                        if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonEggChamberEmperorEgg") && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonAquariumIncubatorEggs"))
                            return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                        else
                            KnownTechFixer.AddNotification(CrafterLogicFixer.SeaEmperorEgg, false, false, true);
                    }
                }
                else if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                {
                    switch (techType)
                    {
                        case TechType.BonesharkEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Boneshark"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.CrabsnakeEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Crabsnake"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.CrabsquidEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Crabsquid"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.CrashEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Crash") && !PDAEncyclopedia.ContainsEntry("CrashLair"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.CutefishEgg:
                            if (!PDAEncyclopedia.ContainsEntry("CuteFish"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.GasopodEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Gasopod"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.JellyrayEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Jellyray"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.JumperEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Shuttlebug"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.LavaLizardEgg:
                            if (!PDAEncyclopedia.ContainsEntry("LavaLizard"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.MesmerEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Mesmer"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.RabbitrayEgg:
                            if (!PDAEncyclopedia.ContainsEntry("RabbitRay"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.ReefbackEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Reefback"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.SandsharkEgg:
                            if (!PDAEncyclopedia.ContainsEntry("SandShark"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.ShockerEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Ampeel"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.SpadefishEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Spadefish"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        case TechType.StalkerEgg:
                            if (!PDAEncyclopedia.ContainsEntry("Stalker"))
                                return KnownTechFixer.LockReturn(ref __result, ref unlocked, ref total);
                            else
                                KnownTechFixer.AddNotification(techType, false, false, true);
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
        }
    }
}
