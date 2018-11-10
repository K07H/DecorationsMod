using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using SMLHelper.V2;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Crafting;

namespace DecorationsMod
{
    public class CustomFabricator
    {
        // This is the original fabricator prefab.
        private static GameObject OriginalFabricator = Resources.Load<GameObject>("Submarine/Build/Fabricator");

        #region Decorations fabricator

        // This is the TechType for the decorations fabricator.
        public static TechType DecorationsFabTechType { get; private set; }
        // This is the CraftTree.Type for the decorations fabricator.
        public static CraftTree.Type DecorationsTreeType { get; private set; }
        // This name will be used as ID for the decorations fabricator TechType and its associated CraftTree.Type.
        public const string DecorationsFabID = "DecorationsFabricator";
        
        public static void RegisterDecorationsFabricator(List<IDecorationItem> decorationItems)
        {
            Logger.Log("Creating decorations craft tree...");
            ModCraftTreeRoot customTreeRootNode = CreateCustomTree(out CraftTree.Type craftType, decorationItems);
            DecorationsTreeType = craftType;

            Logger.Log("Registering decorations fabricator...");
            // Create a new TechType for the fabricator
            DecorationsFabTechType = TechTypeHandler.AddTechType(DecorationsFabID,
                LanguageHelper.GetFriendlyWord("DecorationsFabricatorName"),
                LanguageHelper.GetFriendlyWord("DecorationsFabricatorDescription"),
                true);
            // Add new TechType to the buildables (Interior Module group)
            CraftDataHandler.AddBuildable(DecorationsFabTechType);
            CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, DecorationsFabTechType);
            // Set buildable prefab
            SMLHelper.CustomPrefabHandler.customPrefabs.Add(new SMLHelper.CustomPrefab(DecorationsFabID, $"Submarine/Build/{DecorationsFabID}", DecorationsFabTechType, GetCustomFabPrefab));
            // Set custom sprite for the Habitat Builder Tool menu
            SpriteHandler.RegisterSprite(DecorationsFabTechType, AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_purple"));
            // Create and associate recipe to the new TechType
            var customFabRecipe = new TechData(new List<Ingredient>(4)
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.Silver, 1),
                new Ingredient(TechType.Quartz, 1)
            });
            CraftDataHandler.SetTechData(DecorationsFabTechType, customFabRecipe);
        }

        private static ModCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems)
        {
            var rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(DecorationsFabID, out craftType);

            // POSTERS
            var postersTab = rootNode.AddTabNode("Posters", LanguageHelper.GetFriendlyWord("Posters"), SpriteManager.Get(TechType.PosterKitty));
            postersTab.AddCraftingNode(TechType.PosterAurora,
                                       TechType.PosterExoSuit1,
                                       TechType.PosterExoSuit2,
                                       TechType.PosterKitty,
                                       TechType.Poster);

            // LAB ELEMENTS
            var labEquipmentTab = rootNode.AddTabNode("LabElements", LanguageHelper.GetFriendlyWord("LabElements"), SpriteManager.Get(TechType.LabEquipment1));
            
            // Lab elements -> Equipments Tab
            var analyzersTab = labEquipmentTab.AddTabNode("NonFunctionalAnalyzers", LanguageHelper.GetFriendlyWord("NonFunctionalAnalyzers"), SpriteManager.Get(TechType.LabEquipment3));
            analyzersTab.AddCraftingNode(TechType.LabEquipment1,
                                         TechType.LabEquipment2,
                                         TechType.LabEquipment3);

            // Lab elements -> Open glass containers Tab
            var openedGlassConteinersTab = labEquipmentTab.AddTabNode("OpenedGlassContainers", LanguageHelper.GetFriendlyWord("OpenedGlassContainers"), new Atlas.Sprite(ImageUtils.LoadTextureFromFile("./QMods/DecorationsMod/Assets/labcontaineropen2.png")));
            openedGlassConteinersTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen3"),
                                                     DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen2"),
                                                     DecorationItemsHelper.getTechType(decorationItems, "LabContainerOpen1"));

            // Lab elements -> Glass containers Tab
            var glassContainersTab = labEquipmentTab.AddTabNode("GlassContainers", LanguageHelper.GetFriendlyWord("GlassContainers"), SpriteManager.Get(TechType.LabContainer2));
            glassContainersTab.AddCraftingNode(TechType.LabContainer,
                                               TechType.LabContainer2,
                                               TechType.LabContainer3,
                                               DecorationItemsHelper.getTechType(decorationItems, "LabContainer4"));

            // Lab elements -> Furnitures Tab
            //var labFurnituresTab = labEquipmentTab.AddTabNode("LabFurnitures", LanguageHelper.GetFriendlyWord("LabFurnitures"), AssetsHelper.Assets.LoadAsset<Sprite>("labshelves"));

            // Lab elements items
            if (!ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                labEquipmentTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "DecorationsSpecimenAnalyzer"));
            if (!ConfigSwitcher.LabCart_asBuildable)
                labEquipmentTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabCart"));
            labEquipmentTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabShelf"),
                                            DecorationItemsHelper.getTechType(decorationItems, "DecorationLabTube"),
                                            DecorationItemsHelper.getTechType(decorationItems, "LabRobotArm"));

            // ELECTRONICS
            //var electronicsTab = rootNode.AddTabNode("Electronics", LanguageHelper.GetFriendlyWord("Electronics"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));
            // Wall monitors
            var wallMonitorsTab = rootNode.AddTabNode("WallMonitors", LanguageHelper.GetFriendlyWord("WallMonitors"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));
            wallMonitorsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WallMonitor1"),
                                            DecorationItemsHelper.getTechType(decorationItems, "WallMonitor2"),
                                            DecorationItemsHelper.getTechType(decorationItems, "WallMonitor3"));
            // Circuit boxes
            var circuitBoxesTab = rootNode.AddTabNode("CircuitBoxes", LanguageHelper.GetFriendlyWord("CircuitBoxes"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3"));
            circuitBoxesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CircuitBox1"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox1b"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2b"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2c"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox2d"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3b"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3c"),
                                           DecorationItemsHelper.getTechType(decorationItems, "CircuitBox3d"));
            //var circuitBoxTab1 = electronicsTab.AddTabNode("CircuitBoxTab1", LanguageHelper.GetFriendlyWord("CircuitBoxTab1"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox2"));
            //var circuitBoxTab2 = electronicsTab.AddTabNode("CircuitBoxTab2", LanguageHelper.GetFriendlyWord("CircuitBoxTab2"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3"));

            // DRINKS & FOOD
            var barKitchenTab = rootNode.AddTabNode("DrinksAndFood", LanguageHelper.GetFriendlyWord("DrinksAndFood"), AssetsHelper.Assets.LoadAsset<Sprite>("barbottle05icon"));
            barKitchenTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BarCup1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarCup2"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarNapkins"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarBottle1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarBottle2"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarBottle3"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarBottle4"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarBottle5"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarFood1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BarFood2"));
            if (ConfigSwitcher.EnableNutrientBlock)
                barKitchenTab.AddCraftingNode(TechType.NutrientBlock);

            // OFFICE SUPPLIES
            var officeSuppliesTab = rootNode.AddTabNode("OfficeSupplies", LanguageHelper.GetFriendlyWord("OfficeSupplies"), AssetsHelper.Assets.LoadAsset<Sprite>("clipboardicon"));
            if (!ConfigSwitcher.EmptyDesk_asBuildable)
                officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "DecorationsEmptyDesk"));
            officeSuppliesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "Folder1"),
                                              DecorationItemsHelper.getTechType(decorationItems, "Folder3"),
                                              DecorationItemsHelper.getTechType(decorationItems, "Clipboard"),
                                              DecorationItemsHelper.getTechType(decorationItems, "PaperTrash"),
                                              DecorationItemsHelper.getTechType(decorationItems, "Pen"),
                                              DecorationItemsHelper.getTechType(decorationItems, "PenHolder"),
                                              DecorationItemsHelper.getTechType(decorationItems, "DecorativePDA"));

            // LEVIATHAN DOLLS
            var faunaTab = rootNode.AddTabNode("LeviathanDolls", LanguageHelper.GetFriendlyWord("LeviathanDolls"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));
            faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ReefBackDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaTreaderDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "ReaperLeviathanDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "GhostLeviathanDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaDragonDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaEmperorDoll"));

            // SKELETONS
            var skeletonsTab = rootNode.AddTabNode("SkeletonsParts", LanguageHelper.GetFriendlyWord("GenericSkeletonName"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperskullicon"));
            skeletonsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton1"),
                                         DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton2"),
                                         DecorationItemsHelper.getTechType(decorationItems, "GenericSkeleton3"),
                                         DecorationItemsHelper.getTechType(decorationItems, "ReaperSkullDoll"),
                                         DecorationItemsHelper.getTechType(decorationItems, "ReaperSkeletonDoll"),
                                         DecorationItemsHelper.getTechType(decorationItems, "SeaDragonSkeleton"));
            
            // ACCESSORIES
            var accessoriesTab = rootNode.AddTabNode("Accessories", LanguageHelper.GetFriendlyWord("Accessories"), SpriteManager.Get(TechType.LuggageBag));
            accessoriesTab.AddCraftingNode(TechType.LuggageBag);
            if (!ConfigSwitcher.SofaStr1_asBuidable)
                accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaStr1"));
            if (!ConfigSwitcher.SofaStr2_asBuidable)
                accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaStr2"));
            if (!ConfigSwitcher.SofaStr3_asBuidable)
                accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaStr3"));
            if (!ConfigSwitcher.SofaCorner2_asBuidable)
                accessoriesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SofaCorner2"));
            accessoriesTab.AddCraftingNode(TechType.Cap1,
                                           TechType.Cap2);

            // TOYS
            var toysTab = rootNode.AddTabNode("Toys", LanguageHelper.GetFriendlyWord("Toys"), SpriteManager.Get(TechType.ArcadeGorgetoy));
            toysTab.AddCraftingNode(TechType.StarshipSouvenir,
                                    TechType.ArcadeGorgetoy,
                                    TechType.ToyCar);
            if (!ConfigSwitcher.Forklift_asBuidable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ForkLiftDoll"));
            toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CuddleFishDoll"));
            if (!ConfigSwitcher.MarkiDoll1_asBuildable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll1"));
            if (!ConfigSwitcher.MarkiDoll2_asBuildable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll2"));
            if (!ConfigSwitcher.JackSepticEye_asBuildable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "JackSepticEyeDoll"));
            if (!ConfigSwitcher.EatMyDiction_asBuidable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarlaCat"));

            return rootNode;
        }

        public static GameObject GetCustomFabPrefab()
        {
            // Instanciate fabricator
            GameObject prefab = GameObject.Instantiate(OriginalFabricator);

            prefab.name = DecorationsFabID;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = DecorationsFabID;
            prefabId.name = LanguageHelper.GetFriendlyWord("DecorationsFabricatorName");

            // Update tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = DecorationsFabTechType;

            // Associate craft tree to the fabricator
            var fabricator = prefab.GetComponent<Fabricator>();
            fabricator.craftTree = DecorationsTreeType;

            var ghost = fabricator.GetComponent<GhostCrafter>();
            var powerRelay = new PowerRelay();
            // Ignore any errors you see about this fabricator not having a power relay in its parent. It does and it works.
            FieldInfo fieldInfo = typeof(GhostCrafter).GetField("powerRelay", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(ghost, powerRelay);

            // Set where it can be built
            var constructible = prefab.GetComponent<Constructable>();
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = false;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = false;
            constructible.allowedOnConstructables = false;
            constructible.controlModelState = true;
            constructible.techType = DecorationsFabTechType;

            // Set the custom texture
            Texture2D coloredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_purple");
            SkinnedMeshRenderer skinnedMeshRenderer = prefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = coloredTexture;

            return prefab;
        }

        #endregion

        #region Flora fabricator

        // This is the TechType for the decorations fabricator.
        public static TechType FloraFabTechType { get; private set; }
        // This is the CraftTree.Type for the decorations fabricator.
        public static CraftTree.Type FloraTreeType { get; private set; }
        // This name will be used as ID for the decorations fabricator TechType and its associated CraftTree.Type.
        public const string FloraFabID = "FloraFabricator";

        public static void RegisterFloraFabricator(List<IDecorationItem> decorationItems)
        {
            Logger.Log("Creating flora craft tree...");
            ModCraftTreeRoot customTreeRootNode = CreateFloraTree(out CraftTree.Type craftType, decorationItems);
            FloraTreeType = craftType;

            Logger.Log("Registering flora fabricator...");
            // Create a new TechType for the fabricator
            FloraFabTechType = TechTypeHandler.AddTechType(FloraFabID,
                LanguageHelper.GetFriendlyWord("FloraFabricatorName"),
                LanguageHelper.GetFriendlyWord("FloraFabricatorDescription"),
                true);
            // Add new TechType to the buildables (Interior Module group)
            CraftDataHandler.AddBuildable(FloraFabTechType);
            CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, FloraFabTechType);
            // Set buildable prefab
            SMLHelper.CustomPrefabHandler.customPrefabs.Add(new SMLHelper.CustomPrefab(FloraFabID, $"Submarine/Build/{FloraFabID}", FloraFabTechType, GetFloraFabPrefab));
            // Set custom sprite for the Habitat Builder Tool menu
            SpriteHandler.RegisterSprite(FloraFabTechType, AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_green"));
            // Create and associate recipe to the new TechType
            var customFabRecipe = new TechData(new List<Ingredient>(4)
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.ComputerChip, 1),
                new Ingredient(TechType.Silver, 1),
                new Ingredient(TechType.Magnetite, 1)
            });
            CraftDataHandler.SetTechData(FloraFabTechType, customFabRecipe);
        }

        private static ModCraftTreeRoot CreateFloraTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems)
        {
            var rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(FloraFabID, out craftType);
            
            // Plant Air
            var plantAirTab = rootNode.AddTabNode("PlantAirTab", LanguageHelper.GetFriendlyWord("PlantAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("landplant1icon"));
            plantAirTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LandPlant1"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant2"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant3"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant4"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant5"),
                                        DecorationItemsHelper.getTechType(decorationItems, "Fern2"),
                                        DecorationItemsHelper.getTechType(decorationItems, "Fern4"));

            // Tree Air
            var treeAirTab = rootNode.AddTabNode("TreeAirTab", LanguageHelper.GetFriendlyWord("TreeAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("landtree1seedicon"));
            treeAirTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LandTree1"),
                                       DecorationItemsHelper.getTechType(decorationItems, "JungleTree1"),
                                       DecorationItemsHelper.getTechType(decorationItems, "JungleTree2"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant3a"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant3b"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant6a"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant6b"));

            // Tropical
            var tropicalPlantTab = rootNode.AddTabNode("TropicalPlantTab", LanguageHelper.GetFriendlyWord("TropicalPlantTab"), AssetsHelper.Assets.LoadAsset<Sprite>("tropicalplant1bicon"));
            tropicalPlantTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant1a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant1b"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant2a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant2b"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant7a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant7b"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant10a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant10b"));

            // Existing air seeds from the game
            if (ConfigSwitcher.EnableRegularAirSeeds)
            {
                var regularAirSeedsTab = rootNode.AddTabNode("RegularAirSeedsTab", LanguageHelper.GetFriendlyWord("RegularAirSeedsTab"), SpriteManager.Get(TechType.BulboTreePiece));
                
                var edibleRegularAirTab = regularAirSeedsTab.AddTabNode("EdibleRegularAirTab", LanguageHelper.GetFriendlyWord("EdibleRegularAirTab"), SpriteManager.Get(TechType.MelonSeed));
                edibleRegularAirTab.AddCraftingNode(TechType.BulboTreePiece,
                                                    TechType.PurpleVegetable,
                                                    TechType.HangingFruit,
                                                    TechType.MelonSeed);

                var decorativeBigAirTab = regularAirSeedsTab.AddTabNode("DecorativeBigAirTab", LanguageHelper.GetFriendlyWord("DecorativeBigAirTab"), SpriteManager.Get(TechType.OrangePetalsPlantSeed));
                decorativeBigAirTab.AddCraftingNode(TechType.FernPalmSeed,
                                                    TechType.OrangePetalsPlantSeed,
                                                    TechType.PurpleVasePlantSeed,
                                                    TechType.OrangeMushroomSpore);

                var decorativeSmallAirTab = regularAirSeedsTab.AddTabNode("DecorativeSmallAirTab", LanguageHelper.GetFriendlyWord("DecorativeSmallAirTab"), SpriteManager.Get(TechType.PinkFlowerSeed));
                decorativeSmallAirTab.AddCraftingNode(TechType.PinkMushroomSpore,
                                                    TechType.PurpleRattleSpore,
                                                    TechType.PinkFlowerSeed);
            }

            // Existing water seeds from the game
            if (ConfigSwitcher.EnableRegularWaterSeeds)
            {
                var regularWaterSeedsTab = rootNode.AddTabNode("RegularWaterSeedsTab", LanguageHelper.GetFriendlyWord("RegularWaterSeedsTab"), SpriteManager.Get(TechType.CreepvineSeedCluster));

                var decorativeMediumWaterTab = regularWaterSeedsTab.AddTabNode("DecorativeMediumWaterTab", LanguageHelper.GetFriendlyWord("DecorativeMediumWaterTab"), SpriteManager.Get(TechType.SeaCrownSeed));
                decorativeMediumWaterTab.AddCraftingNode(TechType.GabeSFeatherSeed,
                                                         TechType.RedGreenTentacleSeed,
                                                         TechType.SeaCrownSeed,
                                                         TechType.ShellGrassSeed);

                var decorativeBushesWaterTab = regularWaterSeedsTab.AddTabNode("DecorativeBushesWaterTab", LanguageHelper.GetFriendlyWord("DecorativeBushesWaterTab"), SpriteManager.Get(TechType.PurpleStalkSeed));
                decorativeBushesWaterTab.AddCraftingNode(TechType.PurpleBranchesSeed,
                                                         TechType.RedRollPlantSeed,
                                                         TechType.RedBushSeed,
                                                         TechType.PurpleStalkSeed,
                                                         TechType.SpottedLeavesPlantSeed);

                var regularSmallWaterTab = regularWaterSeedsTab.AddTabNode("RegularSmallWaterTab", LanguageHelper.GetFriendlyWord("RegularSmallWaterTab"), SpriteManager.Get(TechType.AcidMushroomSpore));
                regularSmallWaterTab.AddCraftingNode(TechType.AcidMushroomSpore,
                                                     TechType.WhiteMushroomSpore,
                                                     TechType.JellyPlantSeed,
                                                     TechType.SmallFanSeed,
                                                     TechType.PurpleFanSeed,
                                                     TechType.PurpleTentacleSeed);

                var decorativeBigWaterTab = regularWaterSeedsTab.AddTabNode("DecorativeBigWaterTab", LanguageHelper.GetFriendlyWord("DecorativeBigWaterTab"), SpriteManager.Get(TechType.MembrainTreeSeed));
                decorativeBigWaterTab.AddCraftingNode(TechType.BluePalmSeed,
                                                      TechType.EyesPlantSeed,
                                                      TechType.MembrainTreeSeed,
                                                      TechType.RedConePlantSeed,
                                                      TechType.RedBasketPlantSeed,
                                                      TechType.SnakeMushroomSpore,
                                                      TechType.SpikePlantSeed);

                var functionalBigWaterTab = regularWaterSeedsTab.AddTabNode("FunctionalBigWaterTab", LanguageHelper.GetFriendlyWord("FunctionalBigWaterTab"), SpriteManager.Get(TechType.CreepvineSeedCluster));
                functionalBigWaterTab.AddCraftingNode(TechType.CreepvinePiece,
                                                      TechType.CreepvineSeedCluster,
                                                      TechType.BloodOil,
                                                      TechType.PurpleBrainCoralPiece,
                                                      TechType.KooshChunk);
            }

            // Plant Water
            var plantWaterTab = rootNode.AddTabNode("PlantWaterTab", LanguageHelper.GetFriendlyWord("PlantWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("spottedreedsicon"));
            plantWaterTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "GreenReeds1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "GreenReeds6"),
                                          DecorationItemsHelper.getTechType(decorationItems, "LostRiverPlant2"),
                                          DecorationItemsHelper.getTechType(decorationItems, "LostRiverPlant4"),
                                          DecorationItemsHelper.getTechType(decorationItems, "PlantMiddle11"));

            // Tree Water
            var treeWaterTab = rootNode.AddTabNode("TreeWaterTab", LanguageHelper.GetFriendlyWord("TreeWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("floatingstone1icon"));
            treeWaterTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CrabClawKelp2"),
                                         DecorationItemsHelper.getTechType(decorationItems, "CrabClawKelp1"),
                                         DecorationItemsHelper.getTechType(decorationItems, "CrabClawKelp3"),
                                         DecorationItemsHelper.getTechType(decorationItems, "PyroCoral1"),
                                         DecorationItemsHelper.getTechType(decorationItems, "PyroCoral2"),
                                         DecorationItemsHelper.getTechType(decorationItems, "PyroCoral3"),
                                         DecorationItemsHelper.getTechType(decorationItems, "FloatingStone1"));

            // Coral Water
            var coralWaterTab = rootNode.AddTabNode("CoralWaterTab", LanguageHelper.GetFriendlyWord("CoralWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("flora_smalldeco10icon"));
            coralWaterTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BrownCoralTubes1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BrownCoralTubes2"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BrownCoralTubes3"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BlueCoralTubes1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "SmallDeco10"));

            // Amphibious plants
            var amphibiousPlantsTab = rootNode.AddTabNode("AmphibiousPlantsTab", LanguageHelper.GetFriendlyWord("AmphibiousPlantsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("covetreeicon"));
            amphibiousPlantsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SmallDeco3"),
                                                DecorationItemsHelper.getTechType(decorationItems, "CoveTree1"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco11"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco13"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco14"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco15Red"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco17Purple"));

            return rootNode;
        }
        
        public static GameObject GetFloraFabPrefab()
        {
            // Instanciate fabricator
            GameObject prefab = GameObject.Instantiate(OriginalFabricator);

            prefab.name = FloraFabID;

            // Update prefab ID
            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = FloraFabID;
            prefabId.name = LanguageHelper.GetFriendlyWord("FloraFabricatorName");

            // Update tech tag
            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = FloraFabTechType;

            // Associate craft tree to the fabricator
            var fabricator = prefab.GetComponent<Fabricator>();
            fabricator.craftTree = FloraTreeType;

            var ghost = fabricator.GetComponent<GhostCrafter>();
            var powerRelay = new PowerRelay();
            // Ignore any errors you see about this fabricator not having a power relay in its parent. It does and it works.
            FieldInfo fieldInfo = typeof(GhostCrafter).GetField("powerRelay", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(ghost, powerRelay);

            // Set where it can be built
            var constructible = prefab.GetComponent<Constructable>();
            constructible.allowedInBase = true;
            constructible.allowedInSub = true;
            constructible.allowedOutside = false;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = false;
            constructible.allowedOnConstructables = false;
            constructible.controlModelState = true;
            constructible.techType = FloraFabTechType;

            // Set the custom texture
            Texture2D coloredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_green");
            SkinnedMeshRenderer skinnedMeshRenderer = prefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = coloredTexture;

            return prefab;
        }

        #endregion
    }
}
