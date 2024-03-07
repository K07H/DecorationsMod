#if SUBNAUTICA_NAUTILUS
using System.Diagnostics.CodeAnalysis;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using static CraftData;
#else
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
#endif
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod
{
#if SUBNAUTICA_NAUTILUS
    public class Fabricator_Decorations : CustomPrefab
#else
    public class Fabricator_Decorations : ModPrefab
#endif
    {
        //private static CraftTree.Type _decoFabTreeTypeId = CraftTree.Type.None;
        //public static CraftTree.Type DecorationsFabricatorTreeTypeID { get => _decoFabTreeTypeId; private set => _decoFabTreeTypeId = value; }
        public CraftTree.Type TreeTypeID { get; private set; }

        public static GameObject OriginalFabricator = new GameObject(DecorationsFabID);

        public const string DecorationsFabID = "DecorationsFabricator";

        public const string HandOverText = "UseDecorationsFabricator";

        public bool IsRegistered = false;

        public Texture2D ColoredTexture = null;

#if SUBNAUTICA_NAUTILUS
        public string ClassID => Info.ClassID;
        public string PrefabFileName => Info.PrefabFileName;
        public TechType TechType => Info.TechType;
#endif

#if SUBNAUTICA && !SUBNAUTICA_NAUTILUS
        public TechData Recipe = new TechData()
#else
        public RecipeData Recipe = new RecipeData()
#endif
        {
            craftAmount = 1,
            Ingredients = new List<Ingredient>(new Ingredient[4]
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.Silver, 1),
                new Ingredient(TechType.Quartz, 1)
            })
        };

#if SUBNAUTICA_NAUTILUS
        [SetsRequiredMembers]
        internal Fabricator_Decorations() : base(
            PrefabInfo.WithTechType(DecorationsFabID, LanguageHelper.GetFriendlyWord("DecorationsFabricatorName"), LanguageHelper.GetFriendlyWord("DecorationsFabricatorDescription"), unlockAtStart: true)
            .WithFileName($"Submarine/Build/{DecorationsFabID}")
            .WithIcon(AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_purple"))
            )
        {
            SetGameObject(this.GetGameObject);
        }
#else
        internal Fabricator_Decorations() : base("", "")
        {
            this.ClassID = DecorationsFabID;
            this.PrefabFileName = $"Submarine/Build/{DecorationsFabID}";
            this.TechType = TechTypeHandler.AddTechType(DecorationsFabID,
                    LanguageHelper.GetFriendlyWord("DecorationsFabricatorName"),
                    LanguageHelper.GetFriendlyWord("DecorationsFabricatorDescription"),
                    true);
        }
#endif

        public void RegisterDecorationsFabricator(List<IDecorationItem> decorationItems) //, ModCraftTreeRoot rootNode, CraftTree.Type craftType)
        {
            if (this.IsRegistered == false)
            {
                // Create new Craft Tree Type
                CreateCustomTree(out CraftTree.Type craftType, decorationItems);
                this.TreeTypeID = craftType;

                //CreateCustomTree(decorationItems, rootNode);
                //Fabricator_Decorations.DecorationsFabricatorTreeTypeID = craftType;
                
                // Add the new TechType to the buildables
                CraftDataHandler.AddBuildable(this.TechType);

                // Add the new TechType to the group of Interior Module buildables
                CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType, TechType.Fabricator);
                
                LanguageHandler.SetLanguageLine(HandOverText, LanguageHelper.GetFriendlyWord(HandOverText));

                // Unlock at start
                KnownTechHandler.UnlockOnStart(this.TechType);

                // Set the buildable prefab
#if SUBNAUTICA_NAUTILUS
                this.Register();
#else
                PrefabHandler.RegisterPrefab(this);

                // Register sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_purple"));
#endif

                // Register texture
                this.ColoredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_purple");

                // Associate the recipie to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif
                
                this.IsRegistered = true;
            }
        }

        private ModCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems) //, ModCraftTreeRoot rootNode)
        {
#if SUBNAUTICA_NAUTILUS
            craftType = EnumHandler.AddEntry<CraftTree.Type>(DecorationsFabID).CreateCraftTreeRoot(out ModCraftTreeRoot rootNode);
#else
            ModCraftTreeRoot rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(DecorationsFabID, out craftType);
#endif

            #region LAB ELEMENTS

            ModCraftTreeTab labEquipmentTab = AddTabNode(rootNode, "LabElements", LanguageHelper.GetFriendlyWord("LabElements"), AssetsHelper.Assets.LoadAsset<Sprite>("D_LabEquipment2"));

            // Lab equipments
            ModCraftTreeTab analyzersTab = labEquipmentTab.AddTabNode("NonFunctionalAnalyzers", LanguageHelper.GetFriendlyWord("NonFunctionalAnalyzers"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Analyzers"));
            analyzersTab.AddCraftingNode(TechType.LabEquipment1,
                                         TechType.LabEquipment2,
                                         TechType.LabEquipment3);

            // Open glass containers
            ModCraftTreeTab openedGlassConteinersTab = labEquipmentTab.AddTabNode("OpenedGlassContainers", LanguageHelper.GetFriendlyWord("OpenedGlassContainers"), AssetsHelper.Assets.LoadAsset<Sprite>("D_OpenGlass"));
            if (PrefabsHelper.GetTechType(decorationItems, "LabContainerOpen3") != TechType.None)
                openedGlassConteinersTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LabContainerOpen3"));
            if (PrefabsHelper.GetTechType(decorationItems, "LabContainerOpen2") != TechType.None)
                openedGlassConteinersTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LabContainerOpen2"));
            if (PrefabsHelper.GetTechType(decorationItems, "LabContainerOpen1") != TechType.None)
                openedGlassConteinersTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LabContainerOpen1"));

            // Glass containers
            ModCraftTreeTab glassContainersTab = labEquipmentTab.AddTabNode("GlassContainers", LanguageHelper.GetFriendlyWord("GlassContainers"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Glass"));
            glassContainersTab.AddCraftingNode(TechType.LabContainer,
                                                TechType.LabContainer2,
                                                TechType.LabContainer3);
            if (PrefabsHelper.GetTechType(decorationItems, "LabContainer4") != TechType.None)
                glassContainersTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LabContainer4"));

            // Furnitures
            ModCraftTreeTab labFurnituresTab = labEquipmentTab.AddTabNode("LabFurnitures", LanguageHelper.GetFriendlyWord("LabFurnitures"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Furnitures"));
            if (!ConfigSwitcher.SpecimenAnalyzer_asBuildable && PrefabsHelper.GetTechType(decorationItems, "DecorationsSpecimenAnalyzer") != TechType.None)
                labFurnituresTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "DecorationsSpecimenAnalyzer"));
            if (!ConfigSwitcher.LabCart_asBuildable && PrefabsHelper.GetTechType(decorationItems, "LabCart") != TechType.None)
                labFurnituresTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LabCart"));
            if (PrefabsHelper.GetTechType(decorationItems, "LabShelf") != TechType.None)
                labFurnituresTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LabShelf"));
            if (PrefabsHelper.GetTechType(decorationItems, "DecorationLabTube") != TechType.None)
                labFurnituresTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "DecorationLabTube"));
            if (PrefabsHelper.GetTechType(decorationItems, "LabRobotArm") != TechType.None)
                labFurnituresTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LabRobotArm"));

            #endregion

            #region ELECTRONICS

            ModCraftTreeTab electronicsTab = AddTabNode(rootNode, "Electronics", LanguageHelper.GetFriendlyWord("Electronics"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Electronics"));

            // Wall monitors
            ModCraftTreeTab wallMonitorsTab = electronicsTab.AddTabNode("WallMonitors", LanguageHelper.GetFriendlyWord("WallMonitors"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Monitor"));
            if (PrefabsHelper.GetTechType(decorationItems, "WallMonitor1") != TechType.None)
                wallMonitorsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WallMonitor1"));
            if (PrefabsHelper.GetTechType(decorationItems, "WallMonitor2") != TechType.None)
                wallMonitorsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WallMonitor2"));
            if (PrefabsHelper.GetTechType(decorationItems, "WallMonitor3") != TechType.None)
                wallMonitorsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WallMonitor3"));

            // Circuit boxes
            ModCraftTreeTab circuitBoxesTab = electronicsTab.AddTabNode("CircuitBoxes", LanguageHelper.GetFriendlyWord("CircuitBoxes"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Box"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox1") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox1"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox1b") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox1b"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox2") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox2"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox2b") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox2b"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox2c") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox2c"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox2d") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox2d"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox3") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox3"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox3b") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox3b"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox3c") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox3c"));
            if (PrefabsHelper.GetTechType(decorationItems, "CircuitBox3d") != TechType.None)
                circuitBoxesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CircuitBox3d"));

            //ModCraftTreeTab circuitBoxTab1 = electronicsTab.AddTabNode("CircuitBoxTab1", LanguageHelper.GetFriendlyWord("CircuitBoxTab1"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox2"));
            //ModCraftTreeTab circuitBoxTab2 = electronicsTab.AddTabNode("CircuitBoxTab2", LanguageHelper.GetFriendlyWord("CircuitBoxTab2"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3"));

            // Seamoth fragments
            ModCraftTreeTab seamothFragmentsTab = electronicsTab.AddTabNode("SeamothFragments", LanguageHelper.GetFriendlyWord("SeamothFragments"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Seamoth"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeamothFragment1") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeamothFragment1"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeamothFragment2") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeamothFragment2"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeamothFragment3") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeamothFragment3"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeamothFragment4") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeamothFragment4"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeamothFragment5") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeamothFragment5"));

            #endregion

            #region DRINKS & FOOD

            ModCraftTreeTab barKitchenTab = AddTabNode(rootNode, "DrinksAndFood", LanguageHelper.GetFriendlyWord("DrinksAndFood"), AssetsHelper.Assets.LoadAsset<Sprite>("D_FoodandDrink"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarCup1") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarCup1"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarCup2") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarCup2"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarNapkins") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarNapkins"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarBottle1") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarBottle1"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarBottle2") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarBottle2"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarBottle3") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarBottle3"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarBottle4") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarBottle4"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarBottle5") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarBottle5"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarFood1") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarFood1"));
            if (PrefabsHelper.GetTechType(decorationItems, "BarFood2") != TechType.None)
                barKitchenTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BarFood2"));
            if (ConfigSwitcher.EnableNutrientBlock)
                barKitchenTab.AddCraftingNode(TechType.NutrientBlock);

            #endregion

            #region PRECURSOR

            ModCraftTreeTab precursorTab = AddTabNode(rootNode, "Precursor", LanguageHelper.GetFriendlyWord("Precursor"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Precursors"));
            if (ConfigSwitcher.EnablePrecursorTab)
            {
                // Warpers
                ModCraftTreeTab warperTab = precursorTab.AddTabNode("PrecursorWarperParts", LanguageHelper.GetFriendlyWord("PrecursorWarperParts"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Warper"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart2") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart2"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart3") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart3"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart4") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart4"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart12") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart12"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart5") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart5"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart6") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart6"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart7") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart7"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart8") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart8"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart9") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart9"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart10") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart10"));
                if (PrefabsHelper.GetTechType(decorationItems, "WarperPart11") != TechType.None)
                    warperTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "WarperPart11"));

                // Weapons
                ModCraftTreeTab weaponsTab = precursorTab.AddTabNode("PrecursorWeapons", LanguageHelper.GetFriendlyWord("Weapons"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Weapons"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact6") != TechType.None)
                    weaponsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact6"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact7") != TechType.None)
                    weaponsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact7"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact8") != TechType.None)
                    weaponsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact8"));

                // Artefacts
                ModCraftTreeTab artefactsTab = precursorTab.AddTabNode("PrecursorRelics", LanguageHelper.GetFriendlyWord("Relics"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Relics"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact1") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact1"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact2") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact2"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact3") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact3"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact4") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact4"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact5") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact5"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact9") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact9"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact10") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact10"));
                if (PrefabsHelper.GetTechType(decorationItems, "AlienArtefact11") != TechType.None)
                    artefactsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "AlienArtefact11"));

                // Keys
                ModCraftTreeTab keysTab = precursorTab.AddTabNode("PrecursorKeys", LanguageHelper.GetFriendlyWord("PrecursorKeys"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Tablets"));
                if (ConfigSwitcher.PrecursorKeysAll)
                    keysTab.AddCraftingNode(new TechType[] { TechType.PrecursorKey_Purple, TechType.PrecursorKey_Orange, TechType.PrecursorKey_Blue });
                keysTab.AddCraftingNode(new TechType[] { TechType.PrecursorKey_White, TechType.PrecursorKey_Red });
            }

            #endregion

            #region CREATURE EGGS

            ModCraftTreeTab eggsTab = AddTabNode(rootNode, "EggsTab", LanguageHelper.GetFriendlyWord("EggsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_CreatureEggs"));

            // Damaging creatures
            ModCraftTreeTab dmgCreatureEggsTab = eggsTab.AddTabNode("DmgCreatureEggsTab", LanguageHelper.GetFriendlyWord("DmgCreatureEggsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Aggressive"));
            if (PrefabsHelper.GetTechType(decorationItems, "EggSeaDragon") != TechType.None)
                dmgCreatureEggsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "EggSeaDragon"));
            if (PrefabsHelper.GetTechType(decorationItems, "EggsGhostLeviathan") != TechType.None)
                dmgCreatureEggsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "EggsGhostLeviathan"));
            if (ConfigSwitcher.EnableRegularEggs)
            {
                dmgCreatureEggsTab.AddCraftingNode(TechType.BonesharkEgg,
                    TechType.CrabsnakeEgg,
                    TechType.CrabsquidEgg,
                    TechType.CrashEgg,
                    TechType.GasopodEgg,
                    TechType.LavaLizardEgg,
                    TechType.SandsharkEgg,
                    TechType.ShockerEgg,
                    TechType.StalkerEgg,
                    TechType.MesmerEgg
                );
            }

            // Non-damaging creatures
            ModCraftTreeTab nonDmgCreatureEggsTab = eggsTab.AddTabNode("NonDmgCreatureEggsTab", LanguageHelper.GetFriendlyWord("NonDmgCreatureEggsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Peacefull"));
            if (PrefabsHelper.GetTechType(decorationItems, "EggSeaEmperor") != TechType.None)
                nonDmgCreatureEggsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "EggSeaEmperor"));
            if (ConfigSwitcher.EnableRegularEggs)
            {
                nonDmgCreatureEggsTab.AddCraftingNode(TechType.JellyrayEgg,
                    TechType.RabbitrayEgg,
                    TechType.CutefishEgg,
                    TechType.SpadefishEgg,
                    TechType.JumperEgg,
                    TechType.ReefbackEgg
                );
            }

            #endregion

            #region LEVIATHAN DOLLS & SKELETONS

            ModCraftTreeTab leviathansTab = AddTabNode(rootNode, "LeviathansTab", LanguageHelper.GetFriendlyWord("LeviathansTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Leviathans"));

            // Leviathan dolls
            ModCraftTreeTab faunaTab = leviathansTab.AddTabNode("LeviathanDolls", LanguageHelper.GetFriendlyWord("LeviathanDolls"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Dolls"));
            if (PrefabsHelper.GetTechType(decorationItems, "ReefBackDoll") != TechType.None)
                faunaTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "ReefBackDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeaTreaderDoll") != TechType.None)
                faunaTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeaTreaderDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "ReaperLeviathanDoll") != TechType.None)
                faunaTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "ReaperLeviathanDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "GhostLeviathanDoll") != TechType.None)
                faunaTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "GhostLeviathanDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeaDragonDoll") != TechType.None)
                faunaTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeaDragonDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeaEmperorDoll") != TechType.None)
                faunaTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeaEmperorDoll"));

            // Leviathan skeletons
            ModCraftTreeTab skeletonsTab = leviathansTab.AddTabNode("SkeletonsParts", LanguageHelper.GetFriendlyWord("GenericSkeletons"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Skeleton"));
            if (PrefabsHelper.GetTechType(decorationItems, "GenericSkeleton1") != TechType.None)
                skeletonsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "GenericSkeleton1"));
            if (PrefabsHelper.GetTechType(decorationItems, "GenericSkeleton2") != TechType.None)
                skeletonsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "GenericSkeleton2"));
            if (PrefabsHelper.GetTechType(decorationItems, "GenericSkeleton3") != TechType.None)
                skeletonsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "GenericSkeleton3"));
            if (PrefabsHelper.GetTechType(decorationItems, "ReaperSkullDoll") != TechType.None)
                skeletonsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "ReaperSkullDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "ReaperSkeletonDoll") != TechType.None)
                skeletonsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "ReaperSkeletonDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "SeaDragonSkeleton") != TechType.None)
                skeletonsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SeaDragonSkeleton"));

            #endregion

            #region OFFICE SUPPLIES

            ModCraftTreeTab officeSuppliesTab = AddTabNode(rootNode, "OfficeSupplies", LanguageHelper.GetFriendlyWord("OfficeSupplies"), AssetsHelper.Assets.LoadAsset<Sprite>("D_OfficeMaterials"));
            if (!ConfigSwitcher.EmptyDesk_asBuildable && PrefabsHelper.GetTechType(decorationItems, "DecorationsEmptyDesk") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "DecorationsEmptyDesk"));
            if (PrefabsHelper.GetTechType(decorationItems, "Folder1") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "Folder1"));
            if (PrefabsHelper.GetTechType(decorationItems, "Folder3") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "Folder3"));
            if (PrefabsHelper.GetTechType(decorationItems, "Clipboard") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "Clipboard"));
            if (PrefabsHelper.GetTechType(decorationItems, "PaperTrash") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "PaperTrash"));
            if (PrefabsHelper.GetTechType(decorationItems, "Pen") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "Pen"));
            if (PrefabsHelper.GetTechType(decorationItems, "PenHolder") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "PenHolder"));
            if (PrefabsHelper.GetTechType(decorationItems, "DecorativePDA") != TechType.None)
                officeSuppliesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "DecorativePDA"));

            #endregion

            #region POSTERS / ACCESSORIES / TOYS

            ModCraftTreeTab patTab = AddTabNode(rootNode, "ToysAndAccessories", LanguageHelper.GetFriendlyWord("ToysAndAccessories"), AssetsHelper.Assets.LoadAsset<Sprite>("D_ToysAccessories"));

            // Toys
            ModCraftTreeTab toysTab = patTab.AddTabNode("Toys", LanguageHelper.GetFriendlyWord("Toys"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Toys"));
            toysTab.AddCraftingNode(TechType.StarshipSouvenir,
                                    TechType.ArcadeGorgetoy,
                                    TechType.ToyCar);
            if (!ConfigSwitcher.Forklift_asBuidable && PrefabsHelper.GetTechType(decorationItems, "ForkLiftDoll") != TechType.None)
                toysTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "ForkLiftDoll"));
            if (PrefabsHelper.GetTechType(decorationItems, "CuddleFishDoll") != TechType.None)
                toysTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "CuddleFishDoll"));
            if (!ConfigSwitcher.MarkiDoll1_asBuildable && PrefabsHelper.GetTechType(decorationItems, "MarkiDoll1") != TechType.None)
                toysTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "MarkiDoll1"));
            if (!ConfigSwitcher.MarkiDoll2_asBuildable && PrefabsHelper.GetTechType(decorationItems, "MarkiDoll2") != TechType.None)
                toysTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "MarkiDoll2"));
            if (!ConfigSwitcher.JackSepticEye_asBuildable && PrefabsHelper.GetTechType(decorationItems, "JackSepticEyeDoll") != TechType.None)
                toysTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "JackSepticEyeDoll"));
            if (!ConfigSwitcher.EatMyDiction_asBuidable && PrefabsHelper.GetTechType(decorationItems, "MarlaCat") != TechType.None)
                toysTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "MarlaCat"));

            // Posters
            ModCraftTreeTab postersTab = patTab.AddTabNode("Posters", LanguageHelper.GetFriendlyWord("Posters"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Posters"));
            postersTab.AddCraftingNode(TechType.PosterAurora,
                                       TechType.PosterExoSuit1,
                                       TechType.PosterExoSuit2,
                                       TechType.PosterKitty,
                                       TechType.Poster);
#if BELOWZERO
            postersTab.AddCraftingNode(TechType.PosterSpyPenguin);
#endif

            // Accessories
            ModCraftTreeTab accessoriesTab = patTab.AddTabNode("Accessories", LanguageHelper.GetFriendlyWord("Accessories"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Accessories"));
            accessoriesTab.AddCraftingNode(TechType.LuggageBag);
            if (ConfigSwitcher.EnableSofas)
            {
                if (!ConfigSwitcher.SofaStr1_asBuidable && PrefabsHelper.GetTechType(decorationItems, "SofaStr1") != TechType.None)
                    accessoriesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SofaStr1"));
                if (!ConfigSwitcher.SofaStr2_asBuidable && PrefabsHelper.GetTechType(decorationItems, "SofaStr2") != TechType.None)
                    accessoriesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SofaStr2"));
                if (!ConfigSwitcher.SofaStr3_asBuidable && PrefabsHelper.GetTechType(decorationItems, "SofaStr3") != TechType.None)
                    accessoriesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SofaStr3"));
                if (!ConfigSwitcher.SofaCorner2_asBuidable && PrefabsHelper.GetTechType(decorationItems, "SofaCorner2") != TechType.None)
                    accessoriesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SofaCorner2"));
            }
            accessoriesTab.AddCraftingNode(TechType.Cap1,
                                           TechType.Cap2);

            #endregion

            return rootNode;
        }

        public static GameObject _decorationsFabricator = null;

#if SUBNAUTICA_NAUTILUS
        public GameObject GetGameObject()
#else
        public override GameObject GetGameObject()
#endif
        {
            if (_decorationsFabricator == null)
                _decorationsFabricator = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Fabricator.prefab");

            // Instantiate CyclopsFabricator object
            //var fabricatorPrefab = GameObject.Instantiate(Fabricator_Decorations.OriginalFabricator);
            var fabricatorPrefab = GameObject.Instantiate(_decorationsFabricator);

            // Update prefab name
            fabricatorPrefab.name = this.ClassID;

            // Add prefab ID
            PrefabIdentifier prefabId = fabricatorPrefab.GetComponent<PrefabIdentifier>();
            if (prefabId == null)
                prefabId = fabricatorPrefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = this.ClassID;
            prefabId.name = LanguageHelper.GetFriendlyWord("DecorationsFabricatorName");

            // Add tech tag
            TechTag techTag = fabricatorPrefab.GetComponent<TechTag>();
            if (techTag == null)
                techTag = fabricatorPrefab.AddComponent<TechTag>();
            techTag.type = this.TechType;

            // Adjust position
            GameObject model = fabricatorPrefab.FindChild("submarine_fabricator_01");
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y, model.transform.localPosition.z + 0.035f);

            // Associate custom craft tree to the fabricator
            Fabricator fabricator = fabricatorPrefab.GetComponent<Fabricator>();
            fabricator.craftTree = this.TreeTypeID;
            fabricator.handOverText = HandOverText;

            // Associate power relay
            GhostCrafter ghost = fabricator.GetComponent<GhostCrafter>();
            var powerRelay = new PowerRelay();
            FieldInfo fieldInfo = typeof(GhostCrafter).GetField("powerRelay", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(ghost, powerRelay); // This isn't correct, but nothing else seems to work.

            // Set where this can be built.
            Constructable constructible = fabricatorPrefab.GetComponent<Constructable>();
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = ConfigSwitcher.AllowBuildOutside;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = false;
            constructible.allowedOnConstructables = false;
            constructible.controlModelState = true;
            constructible.techType = this.TechType;
            constructible.placeMinDistance = 0.5f;

            // Set the custom texture
            SkinnedMeshRenderer skinnedMeshRenderer = fabricatorPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = this.ColoredTexture;

            return fabricatorPrefab;
        }

        internal static ModCraftTreeTab AddTabNode(ModCraftTreeLinkingNode parent, string id, string name, Sprite icon)
        {
#if SUBNAUTICA_NAUTILUS
            parent.AddTabNode(id, name, icon);
            return parent.GetTabNode(id);
#else
            return parent.AddTabNode(id, name, icon);
#endif
        }
    }
}
