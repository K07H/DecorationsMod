using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod
{
    public class Fabricator_Decorations : ModPrefab
    {
        //private static CraftTree.Type _decoFabTreeTypeId = CraftTree.Type.None;
        //public static CraftTree.Type DecorationsFabricatorTreeTypeID { get => _decoFabTreeTypeId; private set => _decoFabTreeTypeId = value; }
        public CraftTree.Type TreeTypeID { get; private set; }

        public static GameObject OriginalFabricator = Resources.Load<GameObject>("Submarine/Build/Fabricator");

        public const string DecorationsFabID = "DecorationsFabricator";

        public const string HandOverText = "UseDecorationsFabricator";

        public bool IsRegistered = false;

        public Texture2D ColoredTexture = null;

#if BELOWZERO
        public RecipeData Recipe = new RecipeData()
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
#else
        public TechData Recipe = new TechData()
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
#endif

        internal Fabricator_Decorations() : base("", "")
        {
            this.ClassID = DecorationsFabID;
            this.PrefabFileName = $"Submarine/Build/{DecorationsFabID}";
            this.TechType = TechTypeHandler.AddTechType(DecorationsFabID,
                    LanguageHelper.GetFriendlyWord("DecorationsFabricatorName"),
                    LanguageHelper.GetFriendlyWord("DecorationsFabricatorDescription"),
                    true);
        }

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
                PrefabHandler.RegisterPrefab(this);

                // Register sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_purple"));

                // Register texture
                this.ColoredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_purple");

                // Associate the recipie to the new TechType
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
                
                this.IsRegistered = true;
            }
        }

        private ModCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems) //, ModCraftTreeRoot rootNode)
        {
            ModCraftTreeRoot rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(DecorationsFabID, out craftType);

            #region LAB ELEMENTS

            var labEquipmentTab = rootNode.AddTabNode("LabElements", LanguageHelper.GetFriendlyWord("LabElements"), SpriteManager.Get(TechType.LabEquipment1));

            // Lab equipments
            var analyzersTab = labEquipmentTab.AddTabNode("NonFunctionalAnalyzers", LanguageHelper.GetFriendlyWord("NonFunctionalAnalyzers"), SpriteManager.Get(TechType.LabEquipment3));
            analyzersTab.AddCraftingNode(TechType.LabEquipment1,
                                         TechType.LabEquipment2,
                                         TechType.LabEquipment3);

            // Open glass containers
            var openedGlassConteinersTab = labEquipmentTab.AddTabNode("OpenedGlassContainers", LanguageHelper.GetFriendlyWord("OpenedGlassContainers"), AssetsHelper.Assets.LoadAsset<Sprite>("labcontaineropen2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen3") != TechType.None)
                openedGlassConteinersTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen2") != TechType.None)
                openedGlassConteinersTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen1") != TechType.None)
                openedGlassConteinersTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen1"));

            // Glass containers
            var glassContainersTab = labEquipmentTab.AddTabNode("GlassContainers", LanguageHelper.GetFriendlyWord("GlassContainers"), SpriteManager.Get(TechType.LabContainer2));
            glassContainersTab.AddCraftingNode(TechType.LabContainer,
                                                TechType.LabContainer2,
                                                TechType.LabContainer3);
            if (DecorationItemsHelper.getTechType(decorationItems, "LabContainer4") != TechType.None)
                glassContainersTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabContainer4"));

            // Furnitures
            var labFurnituresTab = labEquipmentTab.AddTabNode("LabFurnitures", LanguageHelper.GetFriendlyWord("LabFurnitures"), AssetsHelper.Assets.LoadAsset<Sprite>("labshelves"));
            if (!ConfigSwitcher.SpecimenAnalyzer_asBuildable && DecorationItemsHelper.getTechType(decorationItems, "DecorationsSpecimenAnalyzer") != TechType.None)
                labFurnituresTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "DecorationsSpecimenAnalyzer"));
            if (!ConfigSwitcher.LabCart_asBuildable && DecorationItemsHelper.getTechType(decorationItems, "LabCart") != TechType.None)
                labFurnituresTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabCart"));
            if (DecorationItemsHelper.getTechType(decorationItems, "LabShelf") != TechType.None)
                labFurnituresTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabShelf"));
            if (DecorationItemsHelper.getTechType(decorationItems, "DecorationLabTube") != TechType.None)
                labFurnituresTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "DecorationLabTube"));
            if (DecorationItemsHelper.getTechType(decorationItems, "LabRobotArm") != TechType.None)
                labFurnituresTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabRobotArm"));

            #endregion

            #region ELECTRONICS

            var electronicsTab = rootNode.AddTabNode("Electronics", LanguageHelper.GetFriendlyWord("Electronics"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));

            // Wall monitors
            var wallMonitorsTab = electronicsTab.AddTabNode("WallMonitors", LanguageHelper.GetFriendlyWord("WallMonitors"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "WallMonitor1") != TechType.None)
                wallMonitorsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WallMonitor1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "WallMonitor2") != TechType.None)
                wallMonitorsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WallMonitor2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "WallMonitor3") != TechType.None)
                wallMonitorsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WallMonitor3"));

            // Circuit boxes
            var circuitBoxesTab = electronicsTab.AddTabNode("CircuitBoxes", LanguageHelper.GetFriendlyWord("CircuitBoxes"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox1") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox1b") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox1b"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2b") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2b"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2c") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2c"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2d") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2d"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3b") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3b"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3c") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3c"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3d") != TechType.None)
                circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3d"));

            //var circuitBoxTab1 = electronicsTab.AddTabNode("CircuitBoxTab1", LanguageHelper.GetFriendlyWord("CircuitBoxTab1"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox2"));
            //var circuitBoxTab2 = electronicsTab.AddTabNode("CircuitBoxTab2", LanguageHelper.GetFriendlyWord("CircuitBoxTab2"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3"));

            // Seamoth fragments
            var seamothFragmentsTab = electronicsTab.AddTabNode("SeamothFragments", LanguageHelper.GetFriendlyWord("SeamothFragments"), AssetsHelper.Assets.LoadAsset<Sprite>("seamothfragment2icon"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment1") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment2") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment3") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment4") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment4"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment5") != TechType.None)
                seamothFragmentsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeamothFragment5"));

            #endregion

            #region DRINKS & FOOD

            var barKitchenTab = rootNode.AddTabNode("DrinksAndFood", LanguageHelper.GetFriendlyWord("DrinksAndFood"), AssetsHelper.Assets.LoadAsset<Sprite>("barbottle05icon"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarCup1") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarCup1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarCup2") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarCup2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarNapkins") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarNapkins"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarBottle1") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarBottle1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarBottle2") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarBottle2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarBottle3") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarBottle3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarBottle4") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarBottle4"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarBottle5") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarBottle5"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarFood1") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarFood1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "BarFood2") != TechType.None)
                barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarFood2"));
            if (ConfigSwitcher.EnableNutrientBlock && TechType.NutrientBlock != TechType.None)
                barKitchenTab.AddCraftingNode(TechType.NutrientBlock);

            #endregion

            #region PRECURSOR

            var precursorTab = rootNode.AddTabNode("Precursor", LanguageHelper.GetFriendlyWord("Precursor"), AssetsHelper.Assets.LoadAsset<Sprite>("relic_10_b"));
            if (ConfigSwitcher.EnablePrecursorTab)
            {
                // Warpers
                var warperTab = precursorTab.AddTabNode("PrecursorWarperParts", LanguageHelper.GetFriendlyWord("PrecursorWarperParts"), AssetsHelper.Assets.LoadAsset<Sprite>("warper_icon"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart2") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart2"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart3") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart3"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart4") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart4"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart12") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart12"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart5") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart5"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart6") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart6"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart7") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart7"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart8") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart8"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart9") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart9"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart10") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart10"));
                if (DecorationItemsHelper.getTechType(decorationItems, "WarperPart11") != TechType.None)
                    warperTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WarperPart11"));

                // Weapons
                var weaponsTab = precursorTab.AddTabNode("PrecursorWeapons", LanguageHelper.GetFriendlyWord("Weapons"), AssetsHelper.Assets.LoadAsset<Sprite>("relic_02_b"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact6") != TechType.None)
                    weaponsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact6"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact7") != TechType.None)
                    weaponsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact7"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact8") != TechType.None)
                    weaponsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact8"));

                // Artefacts
                var artefactsTab = precursorTab.AddTabNode("PrecursorRelics", LanguageHelper.GetFriendlyWord("Relics"), AssetsHelper.Assets.LoadAsset<Sprite>("relic_04_b"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact1") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact1"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact2") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact2"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact3") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact3"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact4") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact4"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact5") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact5"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact9") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact9"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact10") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact10"));
                if (DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact11") != TechType.None)
                    artefactsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "AlienArtefact11"));

                // Keys
                var keysTab = precursorTab.AddTabNode("PrecursorKeys", LanguageHelper.GetFriendlyWord("PrecursorKeys"), SpriteManager.Get(TechType.PrecursorKey_Red));
                if (ConfigSwitcher.PrecursorKeysAll)
                    keysTab.AddCraftingNode(new TechType[] { TechType.PrecursorKey_Purple, TechType.PrecursorKey_Orange, TechType.PrecursorKey_Blue });
                keysTab.AddCraftingNode(new TechType[] { TechType.PrecursorKey_White, TechType.PrecursorKey_Red });
            }

            #endregion

            #region CREATURE EGGS

            ModCraftTreeTab eggsTab = rootNode.AddTabNode("EggsTab", LanguageHelper.GetFriendlyWord("EggsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("seaemperoreggicon"));

            // Damaging creatures
            var dmgCreatureEggsTab = eggsTab.AddTabNode("DmgCreatureEggsTab", LanguageHelper.GetFriendlyWord("DmgCreatureEggsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("seadragoneggicon"));
            if (DecorationItemsHelper.getTechType(decorationItems, "EggSeaDragon") != TechType.None)
                dmgCreatureEggsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "EggSeaDragon"));
            if (DecorationItemsHelper.getTechType(decorationItems, "EggsGhostLeviathan") != TechType.None)
                dmgCreatureEggsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "EggsGhostLeviathan"));
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
            var nonDmgCreatureEggsTab = eggsTab.AddTabNode("NonDmgCreatureEggsTab", LanguageHelper.GetFriendlyWord("NonDmgCreatureEggsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("seaemperoreggicon"));
            if (DecorationItemsHelper.getTechType(decorationItems, "EggSeaEmperor") != TechType.None)
                nonDmgCreatureEggsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "EggSeaEmperor"));
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

            ModCraftTreeTab leviathansTab = rootNode.AddTabNode("LeviathansTab", LanguageHelper.GetFriendlyWord("LeviathansTab"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));

            // Leviathan dolls
            ModCraftTreeTab faunaTab = leviathansTab.AddTabNode("LeviathanDolls", LanguageHelper.GetFriendlyWord("LeviathanDolls"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));
            if (DecorationItemsHelper.getTechType(decorationItems, "ReefBackDoll") != TechType.None)
                faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ReefBackDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeaTreaderDoll") != TechType.None)
                faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeaTreaderDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "ReaperLeviathanDoll") != TechType.None)
                faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ReaperLeviathanDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "GhostLeviathanDoll") != TechType.None)
                faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "GhostLeviathanDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeaDragonDoll") != TechType.None)
                faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeaDragonDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeaEmperorDoll") != TechType.None)
                faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeaEmperorDoll"));

            // Leviathan skeletons
            ModCraftTreeTab skeletonsTab = leviathansTab.AddTabNode("SkeletonsParts", LanguageHelper.GetFriendlyWord("GenericSkeletonName"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperskullicon"));
            if (DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton1") != TechType.None)
                skeletonsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton2") != TechType.None)
                skeletonsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton2"));
            if (DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton3") != TechType.None)
                skeletonsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "ReaperSkullDoll") != TechType.None)
                skeletonsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ReaperSkullDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "ReaperSkeletonDoll") != TechType.None)
                skeletonsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ReaperSkeletonDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "SeaDragonSkeleton") != TechType.None)
                skeletonsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SeaDragonSkeleton"));

            #endregion

            #region OFFICE SUPPLIES

            var officeSuppliesTab = rootNode.AddTabNode("OfficeSupplies", LanguageHelper.GetFriendlyWord("OfficeSupplies"), AssetsHelper.Assets.LoadAsset<Sprite>("clipboardicon"));
            if (!ConfigSwitcher.EmptyDesk_asBuildable && DecorationItemsHelper.getTechType(decorationItems, "DecorationsEmptyDesk") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "DecorationsEmptyDesk"));
            if (DecorationItemsHelper.getTechType(decorationItems, "Folder1") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "Folder1"));
            if (DecorationItemsHelper.getTechType(decorationItems, "Folder3") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "Folder3"));
            if (DecorationItemsHelper.getTechType(decorationItems, "Clipboard") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "Clipboard"));
            if (DecorationItemsHelper.getTechType(decorationItems, "PaperTrash") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "PaperTrash"));
            if (DecorationItemsHelper.getTechType(decorationItems, "Pen") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "Pen"));
            if (DecorationItemsHelper.getTechType(decorationItems, "PenHolder") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "PenHolder"));
            if (DecorationItemsHelper.getTechType(decorationItems, "DecorativePDA") != TechType.None)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "DecorativePDA"));

            #endregion

            #region POSTERS / ACCESSORIES / TOYS

            var patTab = rootNode.AddTabNode("ToysAndAccessories", LanguageHelper.GetFriendlyWord("ToysAndAccessories"), SpriteManager.Get(TechType.StarshipSouvenir));

            // Toys
            var toysTab = patTab.AddTabNode("Toys", LanguageHelper.GetFriendlyWord("Toys"), SpriteManager.Get(TechType.ArcadeGorgetoy));
            toysTab.AddCraftingNode(TechType.StarshipSouvenir,
                                    TechType.ArcadeGorgetoy,
                                    TechType.ToyCar);
            if (!ConfigSwitcher.Forklift_asBuidable && DecorationItemsHelper.getTechType(decorationItems, "ForkLiftDoll") != TechType.None)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ForkLiftDoll"));
            if (DecorationItemsHelper.getTechType(decorationItems, "CuddleFishDoll") != TechType.None)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CuddleFishDoll"));
            if (!ConfigSwitcher.MarkiDoll1_asBuildable && DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll1") != TechType.None)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll1"));
            if (!ConfigSwitcher.MarkiDoll2_asBuildable && DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll2") != TechType.None)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll2"));
            if (!ConfigSwitcher.JackSepticEye_asBuildable && DecorationItemsHelper.getTechType(decorationItems, "JackSepticEyeDoll") != TechType.None)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "JackSepticEyeDoll"));
            if (!ConfigSwitcher.EatMyDiction_asBuidable && DecorationItemsHelper.getTechType(decorationItems, "MarlaCat") != TechType.None)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarlaCat"));

            // Posters
            var postersTab = patTab.AddTabNode("Posters", LanguageHelper.GetFriendlyWord("Posters"), SpriteManager.Get(TechType.PosterKitty));
            postersTab.AddCraftingNode(TechType.PosterAurora,
                                       TechType.PosterExoSuit1,
                                       TechType.PosterExoSuit2,
                                       TechType.PosterKitty,
                                       TechType.Poster);
#if BELOWZERO
            postersTab.AddCraftingNode(TechType.PosterSpyPenguin);
#endif

            // Accessories
            var accessoriesTab = patTab.AddTabNode("Accessories", LanguageHelper.GetFriendlyWord("Accessories"), SpriteManager.Get(TechType.LuggageBag));
            accessoriesTab.AddCraftingNode(TechType.LuggageBag);
            if (ConfigSwitcher.EnableSofas)
            {
                if (!ConfigSwitcher.SofaStr1_asBuidable && DecorationItemsHelper.getTechType(decorationItems, "SofaStr1") != TechType.None)
                    accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaStr1"));
                if (!ConfigSwitcher.SofaStr2_asBuidable && DecorationItemsHelper.getTechType(decorationItems, "SofaStr2") != TechType.None)
                    accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaStr2"));
                if (!ConfigSwitcher.SofaStr3_asBuidable && DecorationItemsHelper.getTechType(decorationItems, "SofaStr3") != TechType.None)
                    accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaStr3"));
                if (!ConfigSwitcher.SofaCorner2_asBuidable && DecorationItemsHelper.getTechType(decorationItems, "SofaCorner2") != TechType.None)
                    accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaCorner2"));
            }
            accessoriesTab.AddCraftingNode(TechType.Cap1,
                                           TechType.Cap2);

            #endregion

            return rootNode;
        }

        public override GameObject GetGameObject()
        {
            // Instantiate CyclopsFabricator object
            var fabricatorPrefab = GameObject.Instantiate(Fabricator_Decorations.OriginalFabricator);

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

            // Set the custom texture
            SkinnedMeshRenderer skinnedMeshRenderer = fabricatorPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = this.ColoredTexture;

            return fabricatorPrefab;
        }
    }
}
