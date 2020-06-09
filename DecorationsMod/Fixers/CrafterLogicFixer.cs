namespace DecorationsMod.Fixers
{
    public class CrafterLogicFixer
    {
        public static void IsCraftRecipeUnlocked_Postfix(ref bool __result, TechType techType)
        {
            if (techType != TechType.None && __result && GameModeUtils.RequiresBlueprints())
            {
                if (techType == SeamothDoll || techType == SeamothFragment1 || techType == SeamothFragment2 || techType == SeamothFragment3 || techType == SeamothFragment4 || techType == SeamothFragment5)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.Seamoth) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == ExosuitDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.Exosuit) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == CuddleFishDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("CuteFish"))
                        __result = false;
                }
                else if (techType == GhostLeviathanDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("GhostLeviathan") && !PDAEncyclopedia.ContainsEntry("GhostLeviathanJuvenile"))
                        __result = false;
                }
                else if (techType == ReaperLeviathanDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("ReaperLeviathan"))
                        __result = false;
                }
                else if (techType == ReefBackDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("Reefback"))
                        __result = false;
                }
                else if (techType == SeaDragonDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("SeaDragon"))
                        __result = false;
                }
                else if (techType == SeaEmperorDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("SeaEmperorLeviathan") && !PDAEncyclopedia.ContainsEntry("SeaEmperorBaby"))
                        __result = false;
                }
                else if (techType == SeaTreaderDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("SeaTreader"))
                        __result = false;
                }
                else if (techType == ReaperSkeletonDoll || techType == ReaperSkullDoll)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("LavaZone_ReaperSkeleton"))
                        __result = false;
                }
                else if (techType == SeaDragonSkeleton)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("LostRiverBase_SeaDragonSkeleton"))
                        __result = false;
                }
                else if (techType == GenericSkeleton1 || techType == GenericSkeleton2 || techType == GenericSkeleton3)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_Bones") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_GiantFishSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_SeaDragonSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("LostRiver_BonesfieldHugeSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_CaveSkeletonScan") &&
                        !PDAEncyclopedia.ContainsEntry("LavaZone_ReaperSkeleton") &&
                        !PDAEncyclopedia.ContainsEntry("Precursor_Cache_LostRiverBonesScan"))
                        __result = false;
                }
                else if (techType == AlienArtefact1)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact1"))
                        __result = false;
                }
                else if (techType == AlienArtefact2)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact2"))
                        __result = false;
                }
                else if (techType == AlienArtefact3)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact3"))
                        __result = false;
                }
                else if (techType == AlienArtefact4)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact4"))
                        __result = false;
                }
                else if (techType == AlienArtefact5)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact5"))
                        __result = false;
                }
                else if (techType == AlienArtefact6)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact6"))
                        __result = false;
                }
                else if (techType == AlienArtefact7)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact7"))
                        __result = false;
                }
                else if (techType == AlienArtefact8)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact8"))
                        __result = false;
                }
                else if (techType == AlienArtefact9)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact10"))
                        __result = false;
                }
                else if (techType == AlienArtefact10)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact11"))
                        __result = false;
                }
                else if (techType == AlienArtefact11)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonArtifact12"))
                        __result = false;
                }
                else if (techType == BenchSmall || techType == BenchMedium)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.Bench) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == SofaCorner || techType == Sofa1 || techType == Sofa2 || techType == Sofa3)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered &&
                        !(KnownTech.GetTechUnlockState(TechType.Bench) == TechUnlockState.Available &&
                        KnownTech.GetTechUnlockState(TechType.StarshipChair) == TechUnlockState.Available &&
                        KnownTech.GetTechUnlockState(TechType.StarshipChair2) == TechUnlockState.Available &&
                        KnownTech.GetTechUnlockState(TechType.StarshipChair3) == TechUnlockState.Available))
                        __result = false;
                }
                else if (techType == Stool)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered &&
                        !(KnownTech.GetTechUnlockState(TechType.StarshipChair) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.StarshipChair2) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.StarshipChair3) == TechUnlockState.Available))
                        __result = false;
                }
                else if (techType == LongPlanterA)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.PlanterBox) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == LongPlanterB)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.FarmingTray) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == EmptyDesk)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.StarshipDesk) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == CustomizablePictureFrame)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.PictureFrame) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == ReactorLamp)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered &&
                        !(KnownTech.GetTechUnlockState(TechType.Spotlight) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.LEDLight) == TechUnlockState.Available ||
                        KnownTech.GetTechUnlockState(TechType.Techlight) == TechUnlockState.Available))
                        __result = false;
                }
                else if (techType == AlienPillar)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered &&
                        !(PDAEncyclopedia.ContainsEntry("PrecursorTeleporter") ||
                        PDAEncyclopedia.ContainsEntry("PrecursorKeyTerminal") ||
                        PDAEncyclopedia.ContainsEntry("PrecursorEnergyCore") ||
                        PDAEncyclopedia.ContainsEntry("Precursor_Cache_LostRiverLabTable")))
                        __result = false;
                }
                else if (techType == WarperPart1)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_WarperScan"))
                        __result = false;
                }
                else if (techType == WarperPart2 || techType == WarperPart3 || techType == WarperPart4 || techType == WarperPart5 ||
                    techType == WarperPart6 || techType == WarperPart7 || techType == WarperPart8 || techType == WarperPart9 ||
                    techType == WarperPart10 || techType == WarperPart11 || techType == WarperPart12)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && !(PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_ProductionLine") || PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_WarperParts")))
                        __result = false;
                }
                else if (techType == LabCart || techType == SpecimenAnalyzer || techType == LabRobotArm)
                {
                    if (ConfigSwitcher.AddItemsWhenDiscovered && KnownTech.GetTechUnlockState(TechType.BaseWaterPark) != TechUnlockState.Available)
                        __result = false;
                }
                else if (techType == FloatingStone1)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("FloatingStones"))
                        __result = false;
                }
                else if (techType == BrineLily)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("BlueLostRiverLilly"))
                        __result = false;
                }
                else if (techType == Amoeba)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("BlueAmoeba"))
                        __result = false;
                }
                else if (techType == PyroCoral1 || techType == PyroCoral2 || techType == PyroCoral3)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("RedTipRockThings"))
                        __result = false;
                }
                else if (techType == CrabClawKelp1 || techType == CrabClawKelp2 || techType == CrabClawKelp3)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("BlueTipLostRiverPlant"))
                        __result = false;
                }
                else if (techType == BrownTubes1 || techType == BrownTubes2 || techType == BrownTubes3)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("EarthenCoralTubes"))
                        __result = false;
                }
                else if (techType == CoveTree)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("TreeCoveTree"))
                        __result = false;
                }
                else if (techType == MushroomTree1 || techType == MushroomTree2)
                {
                    if (ConfigSwitcher.AddWaterSeedsWhenDiscovered && !PDAEncyclopedia.ContainsEntry("TreeLeech") && !PDAEncyclopedia.ContainsEntry("TreeMushroom") && !PDAEncyclopedia.ContainsEntry("TreeMushroomPiece"))
                        __result = false;
                }
                else if (!ConfigSwitcher.EnableEggsAtStart)
                {
                    if (techType == GhostLeviathanEggs)
                    {
                        if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                        {
                            if (!PDAEncyclopedia.ContainsEntry("TreeCoveTree") && !PDAEncyclopedia.ContainsEntry("GhostLeviathan") && !PDAEncyclopedia.ContainsEntry("GhostLeviathanJuvenile"))
                                __result = false;
                        }
                        else if (!PDAEncyclopedia.ContainsEntry("TreeCoveTree"))
                            __result = false;
                    }
                    else if (techType == SeaDragonEgg)
                    {
                        if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                        {
                            if (!PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_LeviathanEggShellScan") && !PDAEncyclopedia.ContainsEntry("SeaDragon"))
                                __result = false;
                        }
                        else if (!PDAEncyclopedia.ContainsEntry("Precursor_LostRiverBase_LeviathanEggShellScan"))
                            __result = false;
                    }
                    else if (techType == SeaEmperorEgg)
                    {
                        if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                        {
                            if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonEggChamberEmperorEgg") && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonAquariumIncubatorEggs") && !PDAEncyclopedia.ContainsEntry("SeaEmperor") && !PDAEncyclopedia.ContainsEntry("SeaEmperorLeviathan") && !PDAEncyclopedia.ContainsEntry("SeaEmperorBaby"))
                                __result = false;
                        }
                        else if (!PDAEncyclopedia.ContainsEntry("PrecursorPrisonEggChamberEmperorEgg") && !PDAEncyclopedia.ContainsEntry("PrecursorPrisonAquariumIncubatorEggs"))
                            __result = false;
                    }
                    else if (ConfigSwitcher.EnableEggsWhenCreatureScanned)
                    {
                        switch (techType)
                        {
                            case TechType.BonesharkEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Boneshark"))
                                    __result = false;
                                break;
                            case TechType.CrabsnakeEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Crabsnake"))
                                    __result = false;
                                break;
                            case TechType.CrabsquidEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Crabsquid"))
                                    __result = false;
                                break;
                            case TechType.CrashEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Crash") && !PDAEncyclopedia.ContainsEntry("CrashLair"))
                                    __result = false;
                                break;
                            case TechType.CutefishEgg:
                                if (!PDAEncyclopedia.ContainsEntry("CuteFish"))
                                    __result = false;
                                break;
                            case TechType.GasopodEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Gasopod"))
                                    __result = false;
                                break;
                            case TechType.JellyrayEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Jellyray"))
                                    __result = false;
                                break;
                            case TechType.JumperEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Shuttlebug"))
                                    __result = false;
                                break;
                            case TechType.LavaLizardEgg:
                                if (!PDAEncyclopedia.ContainsEntry("LavaLizard"))
                                    __result = false;
                                break;
                            case TechType.MesmerEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Mesmer"))
                                    __result = false;
                                break;
                            case TechType.RabbitrayEgg:
                                if (!PDAEncyclopedia.ContainsEntry("RabbitRay"))
                                    __result = false;
                                break;
                            case TechType.ReefbackEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Reefback"))
                                    __result = false;
                                break;
                            case TechType.SandsharkEgg:
                                if (!PDAEncyclopedia.ContainsEntry("SandShark"))
                                    __result = false;
                                break;
                            case TechType.ShockerEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Ampeel"))
                                    __result = false;
                                break;
                            case TechType.SpadefishEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Spadefish"))
                                    __result = false;
                                break;
                            case TechType.StalkerEgg:
                                if (!PDAEncyclopedia.ContainsEntry("Stalker"))
                                    __result = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        // New items tech types.

        public static TechType GhostLeviathanEggs = TechType.None;
        public static TechType SeaDragonEgg = TechType.None;
        public static TechType SeaEmperorEgg = TechType.None;

        public static TechType SeamothDoll = TechType.None;
        public static TechType ExosuitDoll = TechType.None;

        public static TechType CuddleFishDoll = TechType.None;
        public static TechType GhostLeviathanDoll = TechType.None;
        public static TechType ReaperLeviathanDoll = TechType.None;
        public static TechType ReefBackDoll = TechType.None;
        public static TechType SeaDragonDoll = TechType.None;
        public static TechType SeaEmperorDoll = TechType.None;
        public static TechType SeaTreaderDoll = TechType.None;

        public static TechType ReaperSkeletonDoll = TechType.None;
        public static TechType ReaperSkullDoll = TechType.None;
        public static TechType SeaDragonSkeleton = TechType.None;
        public static TechType GenericSkeleton1 = TechType.None;
        public static TechType GenericSkeleton2 = TechType.None;
        public static TechType GenericSkeleton3 = TechType.None;

        public static TechType AlienArtefact1 = TechType.None;
        public static TechType AlienArtefact2 = TechType.None;
        public static TechType AlienArtefact3 = TechType.None;
        public static TechType AlienArtefact4 = TechType.None;
        public static TechType AlienArtefact5 = TechType.None;
        public static TechType AlienArtefact6 = TechType.None;
        public static TechType AlienArtefact7 = TechType.None;
        public static TechType AlienArtefact8 = TechType.None;
        public static TechType AlienArtefact9 = TechType.None;
        public static TechType AlienArtefact10 = TechType.None;
        public static TechType AlienArtefact11 = TechType.None;

        public static TechType BenchSmall = TechType.None;
        public static TechType BenchMedium = TechType.None;
        public static TechType SofaCorner = TechType.None;
        public static TechType Sofa1 = TechType.None;
        public static TechType Sofa2 = TechType.None;
        public static TechType Sofa3 = TechType.None;
        public static TechType Stool = TechType.None;

        public static TechType LongPlanterA = TechType.None;
        public static TechType LongPlanterB = TechType.None;
        public static TechType EmptyDesk = TechType.None;
        public static TechType CustomizablePictureFrame = TechType.None;
        public static TechType ReactorLamp = TechType.None;
        public static TechType AlienPillar = TechType.None;
        public static TechType WarperPart1 = TechType.None; // Warper specimen

        public static TechType WarperPart2 = TechType.None;
        public static TechType WarperPart3 = TechType.None;
        public static TechType WarperPart4 = TechType.None;
        public static TechType WarperPart5 = TechType.None;
        public static TechType WarperPart6 = TechType.None;
        public static TechType WarperPart7 = TechType.None;
        public static TechType WarperPart8 = TechType.None;
        public static TechType WarperPart9 = TechType.None;
        public static TechType WarperPart10 = TechType.None;
        public static TechType WarperPart11 = TechType.None;
        public static TechType WarperPart12 = TechType.None;

        public static TechType LabCart = TechType.None;
        public static TechType SpecimenAnalyzer = TechType.None;
        public static TechType LabRobotArm = TechType.None;

        public static TechType FloatingStone1 = TechType.None;
        public static TechType BrineLily = TechType.None;
        public static TechType Amoeba = TechType.None;
        public static TechType PyroCoral1 = TechType.None;
        public static TechType PyroCoral2 = TechType.None;
        public static TechType PyroCoral3 = TechType.None;
        public static TechType CrabClawKelp1 = TechType.None;
        public static TechType CrabClawKelp2 = TechType.None;
        public static TechType CrabClawKelp3 = TechType.None;
        public static TechType BrownTubes1 = TechType.None;
        public static TechType BrownTubes2 = TechType.None;
        public static TechType BrownTubes3 = TechType.None;
        public static TechType CoveTree = TechType.None;
        public static TechType MushroomTree1 = TechType.None;
        public static TechType MushroomTree2 = TechType.None;

        public static TechType SeamothFragment1 = TechType.None;
        public static TechType SeamothFragment2 = TechType.None;
        public static TechType SeamothFragment3 = TechType.None;
        public static TechType SeamothFragment4 = TechType.None;
        public static TechType SeamothFragment5 = TechType.None;
    }
}
