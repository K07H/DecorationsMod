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
        public CraftTree.Type TreeTypeID { get; private set; }
        
        public static GameObject OriginalFabricator = Resources.Load<GameObject>("Submarine/Build/Fabricator");

        public const string DecorationsFabID = "DecorationsFabricator";

        public const string HandOverText = "UseDecorationsFabricator";

        public bool IsRegistered = false;

        public Texture2D ColoredTexture = null;
        
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

        internal Fabricator_Decorations() : base("", "")
        {
            this.ClassID = DecorationsFabID;
            this.PrefabFileName = $"Submarine/Build/{DecorationsFabID}";
            this.TechType = TechTypeHandler.AddTechType(DecorationsFabID,
                    LanguageHelper.GetFriendlyWord("DecorationsFabricatorName"),
                    LanguageHelper.GetFriendlyWord("DecorationsFabricatorDescription"),
                    true);
        }

        public void RegisterDecorationsFabricator(List<IDecorationItem> decorationItems)
        {
            if (this.IsRegistered == false)
            {
                // Create new Craft Tree Type
                ModCraftTreeRoot rootNode = CreateCustomTree(out CraftTree.Type craftType, decorationItems);
                this.TreeTypeID = craftType;
                
                // Add the new TechType to the buildables
                CraftDataHandler.AddBuildable(this.TechType);

                // Add the new TechType to the group of Interior Module buildables
                CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType);
                
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

        private ModCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems)
        {
            ModCraftTreeRoot rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(this.ClassID, out craftType);

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
            var electronicsTab = rootNode.AddTabNode("Electronics", LanguageHelper.GetFriendlyWord("Electronics"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));
            
            // Wall monitors
            var wallMonitorsTab = electronicsTab.AddTabNode("WallMonitors", LanguageHelper.GetFriendlyWord("WallMonitors"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));
            wallMonitorsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WallMonitor1"),
                                            DecorationItemsHelper.getTechType(decorationItems, "WallMonitor2"),
                                            DecorationItemsHelper.getTechType(decorationItems, "WallMonitor3"));
            // Circuit boxes
            var circuitBoxesTab = electronicsTab.AddTabNode("CircuitBoxes", LanguageHelper.GetFriendlyWord("CircuitBoxes"), AssetsHelper.Assets.LoadAsset<Sprite>("circuitbox3"));
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

            ModCraftTreeTab faunaTab = null;
            ModCraftTreeTab skeletonsTab = null;
            if (ConfigSwitcher.UseFlatScreenResolution)
            {
                // Additionnal tab
                ModCraftTreeTab leviathansTab = rootNode.AddTabNode("LeviathansTab", LanguageHelper.GetFriendlyWord("LeviathansTab"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));

                // LEVIATHAN DOLLS
                faunaTab = leviathansTab.AddTabNode("LeviathanDolls", LanguageHelper.GetFriendlyWord("LeviathanDolls"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));
                // SKELETONS
                skeletonsTab = leviathansTab.AddTabNode("SkeletonsParts", LanguageHelper.GetFriendlyWord("GenericSkeletonName"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperskullicon"));
            }
            else
            {
                // LEVIATHAN DOLLS
                faunaTab = rootNode.AddTabNode("LeviathanDolls", LanguageHelper.GetFriendlyWord("LeviathanDolls"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));
                // SKELETONS
                skeletonsTab = rootNode.AddTabNode("SkeletonsParts", LanguageHelper.GetFriendlyWord("GenericSkeletonName"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperskullicon"));
            }
            // LEVIATHAN DOLLS
            faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ReefBackDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaTreaderDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "ReaperLeviathanDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "GhostLeviathanDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaDragonDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaEmperorDoll"));
            // SKELETONS
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
