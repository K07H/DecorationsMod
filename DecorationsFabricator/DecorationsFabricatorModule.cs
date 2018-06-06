using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using SMLHelper;
using SMLHelper.Patchers;
using UnityEngine;

namespace DecorationsMod
{
    public static class ConfigSwitcher
    {
        // Following settings are automatically initialized when retrieving config.

        // If true you'll be able to place following items:
        // coffee cups, polyaniline, hydrochloric acid, benzene, hatching enzymes, reactor rod and deplated reactor rod
        public static bool EnablePlaceItems = true;

        // If "true", you'll be able to build/craft the following items:
        // specimen analyzer, markiplier doll 1, markiplier doll 2, jacksepticeye doll, eatmydiction doll
        public static bool EnableSpecialItems = true;

        // If true: Item will be available as a buildable (in habitat builder menu).
        // If false: Item will be available as a craftable (in decorations fabricator).
        public static bool SpecimenAnalyzer_asBuildable = true;
        public static bool MarkiDoll1_asBuildable = true;
        public static bool MarkiDoll2_asBuildable = true;
        public static bool JackSepticEye_asBuildable = true;
        public static bool EatMyDiction_asBuidable = true;
    }

    public class DecorationsFabricatorModule
    {
        public static CraftTree.Type DecorationsTreeType { get; private set; }
        public static TechType DecorationsFabTechType { get; private set; }

        // This name will be used as both the new TechType of the buildable fabricator and the CraftTree Type for the custom crafting tree.
        public const string DecorationsFabID = "DecorationsFabricator";
        
        public static void Patch()
        {
            // Get config file path
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string currentDir = Path.GetDirectoryName(path);
            string configFilePath = currentDir + "/Config.txt";

            Logger.Log("Loading configuration from \"" + configFilePath + "\"...", null);
            
            // Retrieve config
            if (File.Exists(configFilePath))
            {
                string configFile = File.ReadAllText(configFilePath);
                string[] configLines = configFile.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string configStr in configLines)
                {
                    if (!configStr.StartsWith("#") && configStr.Contains("="))
                    {
                        string[] configElem = configStr.Split("=".ToCharArray(), StringSplitOptions.None);
                        if (configElem != null && configElem.Length == 2)
                        {
                            string configKey = configElem[0].Trim();
                            bool configValue = (configElem[1].Trim().CompareTo("true") == 0);

                            if (configKey.CompareTo("enablePlaceItems") == 0)
                                ConfigSwitcher.EnablePlaceItems = configValue;
                            else if (configKey.CompareTo("enableSpecialItems") == 0)
                                ConfigSwitcher.EnableSpecialItems = configValue;
                            else if (configKey.CompareTo("asBuildable_SpecimenAnalyzer") == 0)
                                ConfigSwitcher.SpecimenAnalyzer_asBuildable = configValue;
                            else if (configKey.CompareTo("asBuildable_MarkiplierDoll1") == 0)
                                ConfigSwitcher.MarkiDoll1_asBuildable = configValue;
                            else if (configKey.CompareTo("asBuildable_MarkiplierDoll2") == 0)
                                ConfigSwitcher.MarkiDoll2_asBuildable = configValue;
                            else if (configKey.CompareTo("asBuildable_JackSepticEyeDoll") == 0)
                                ConfigSwitcher.JackSepticEye_asBuildable = configValue;
                            else if (configKey.CompareTo("asBuildable_EatMyDictionDoll") == 0)
                                ConfigSwitcher.EatMyDiction_asBuidable = configValue;
                        }
                    }
                }
            }
            else
                Logger.Log("Warning: Cannot find config file, default options will be set.");

            Logger.Log("Registering items...", null);

            // Register all items
            List<DecorationItem> decorationItems = RegisterDecorationItems();

            Logger.Log("Making some existing items placeable...", null);

            // Make some existing items placeable/pickupable
            if (ConfigSwitcher.EnablePlaceItems)
                PlaceToolItems.MakeItemsPlaceable();

            Logger.Log("Creating craft tree...", null);

            // Create new CraftTree Type
            CustomCraftTreeRoot customTreeRootNode = CreateCustomTree(out CraftTree.Type craftType, decorationItems);
            DecorationsTreeType = craftType;

            Logger.Log("Registering the new fabricator...", null);

            // Create a new TechType for the new fabricator
            DecorationsFabTechType = TechTypePatcher.AddTechType(DecorationsFabID,
                                                                 LanguageHelper.GetFriendlyWord("DecorationsFabricatorName"),
                                                                 LanguageHelper.GetFriendlyWord("DecorationsFabricatorDescription"),
                                                                 true);
            
            // Create a recipe for the new TechType
            var customFabRecipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[4]
                    {
                        new IngredientHelper(TechType.Titanium, 1),
                        new IngredientHelper(TechType.ComputerChip, 1),
                        new IngredientHelper(TechType.Diamond, 1),
                        new IngredientHelper(TechType.Magnetite, 1)
                    }),
                _techType = DecorationsFabTechType
            };

            // Add the new TechType to the buildables
            CraftDataPatcher.customBuildables.Add(DecorationsFabTechType);

            // Add the new TechType to the group of Interior Module buildables
            CraftDataPatcher.AddToCustomGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, DecorationsFabTechType);

            // Set the buildable prefab
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(DecorationsFabID, $"Submarine/Build/{DecorationsFabID}", DecorationsFabTechType, GetPrefab));

            // Set the custom sprite for the Habitat Builder Tool menu
            CustomSpriteHandler.customSprites.Add(new CustomSprite(DecorationsFabTechType, AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_purple")));

            // Associate recipe to the new TechType
            CraftDataPatcher.customTechData[DecorationsFabTechType] = customFabRecipe;
        }

        private static List<DecorationItem> RegisterDecorationItems()
        {
            List<DecorationItem> result = new List<DecorationItem>();

            // Get the list of modified existing items
            var existingItems = from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes() 
                                where t.IsClass && t.Namespace == "DecorationsMod.ExistingItems" 
                                select t;
            
            // Register modified existing items
            foreach (Type existingItemType in existingItems)
            {
                DecorationItem existingItem = (DecorationItem)(Activator.CreateInstance(existingItemType));
                existingItem.RegisterItem();
                KnownTechPatcher.unlockedAtStart.Add(existingItem.TechType);
                result.Add(existingItem);
            }

            // Get the list of new items
            var newItems = from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes() 
                           where t.IsClass && t.Namespace == "DecorationsMod.NewItems" 
                           select t;

            // Register new items
            foreach (Type newItemType in newItems)
            {
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));

                if (ConfigSwitcher.EnableSpecialItems)
                {
                    newItem.RegisterItem();
                    result.Add(newItem);
                }
                else
                {
                    // If we reach here decoration items from habitat builder are disabled
                    if (!newItem.IsHabitatBuilder)
                    {
                        newItem.RegisterItem();
                        result.Add(newItem);
                    }
                }
            }

            // Register lamp tooltip
            LanguagePatcher.customLines.Add("ToggleLamp", "Adjust light range" + Environment.NewLine + 
                                                          "(Hold 'I' to change intensity)" + Environment.NewLine +
                                                          "(Hold 'R' to change red levels)" + Environment.NewLine +
                                                          "(Hold 'G' to change green levels)" + Environment.NewLine +
                                                          "(Hold 'B' to change blue levels)" + Environment.NewLine +
                                                          "(Hold 'E' to change rod color)");

            // Register seamoth doll tooltip
            LanguagePatcher.customLines.Add("SwitchSeamothModel", "Switch model");

            return result;
        }

        private static CustomCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<DecorationItem> decorationItems)
        {
            var rootNode = CraftTreeTypePatcher.CreateCustomCraftTreeAndType(DecorationsFabID, out craftType);

            var postersTab = rootNode.AddTabNode("Posters", LanguageHelper.GetFriendlyWord("Posters"), SpriteManager.Get(TechType.PosterKitty));
            postersTab.AddCraftingNode(TechType.PosterAurora,
                                       TechType.PosterExoSuit1,
                                       TechType.PosterExoSuit2,
                                       TechType.PosterKitty,
                                       TechType.Poster);

            var labEquipmentTab = rootNode.AddTabNode("LabElements", LanguageHelper.GetFriendlyWord("LabElements"), SpriteManager.Get(TechType.LabEquipment1));

            var glassContainersTab = labEquipmentTab.AddTabNode("GlassContainers", LanguageHelper.GetFriendlyWord("GlassContainers"), SpriteManager.Get(TechType.LabContainer2));
            glassContainersTab.AddCraftingNode(TechType.LabContainer,
                                               TechType.LabContainer2,
                                               TechType.LabContainer3,
                                               DecorationItemsHelper.getTechType(decorationItems, "LabContainer4"));

            var analyzersTab = labEquipmentTab.AddTabNode("NonFunctionalAnalyzers", LanguageHelper.GetFriendlyWord("NonFunctionalAnalyzers"), SpriteManager.Get(TechType.LabEquipment3));
            analyzersTab.AddCraftingNode(TechType.LabEquipment1,
                                         TechType.LabEquipment2,
                                         TechType.LabEquipment3);

            var furnituresTab = labEquipmentTab.AddTabNode("LabFurnitures", LanguageHelper.GetFriendlyWord("LabFurnitures"), AssetsHelper.Assets.LoadAsset<Sprite>("labcarticon"));
            furnituresTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LabShelf"),
                                          DecorationItemsHelper.getTechType(decorationItems, "LabCart"),
                                          DecorationItemsHelper.getTechType(decorationItems, "DecorationLabTube"));
            if (!ConfigSwitcher.SpecimenAnalyzer_asBuildable)
                labEquipmentTab.AddCraftingNode(TechType.SpecimenAnalyzer);

            var electronicsTab = rootNode.AddTabNode("Electronics", LanguageHelper.GetFriendlyWord("Electronics"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));

            var wallMonitorsTab = electronicsTab.AddTabNode("WallMonitors", LanguageHelper.GetFriendlyWord("WallMonitors"), AssetsHelper.Assets.LoadAsset<Sprite>("computer3"));
            wallMonitorsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "WallMonitor1"),
                                            DecorationItemsHelper.getTechType(decorationItems, "WallMonitor2"),
                                            DecorationItemsHelper.getTechType(decorationItems, "WallMonitor3"));

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
            
            var caps = rootNode.AddTabNode("Caps", LanguageHelper.GetFriendlyWord("Caps"), SpriteManager.Get(TechType.Cap1));
            caps.AddCraftingNode(TechType.Cap1,
                                 TechType.Cap2);

            var toysTab = rootNode.AddTabNode("Toys", LanguageHelper.GetFriendlyWord("Toys"), SpriteManager.Get(TechType.ArcadeGorgetoy));
            toysTab.AddCraftingNode(TechType.StarshipSouvenir,
                                    TechType.ArcadeGorgetoy,
                                    TechType.ToyCar,
                                    DecorationItemsHelper.getTechType(decorationItems, "CuddleFishDoll"));
            if (!ConfigSwitcher.MarkiDoll1_asBuildable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll1"));
            if (!ConfigSwitcher.MarkiDoll2_asBuildable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarkiDoll2"));
            if (!ConfigSwitcher.JackSepticEye_asBuildable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "JackSepticEyeDoll"));
            if (!ConfigSwitcher.EatMyDiction_asBuidable)
                toysTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "MarlaCat"));

            var faunaTab = rootNode.AddTabNode("LeviathanDolls", LanguageHelper.GetFriendlyWord("LeviathanDolls"), AssetsHelper.Assets.LoadAsset<Sprite>("reaperleviathanicon"));
            faunaTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "ReefBackDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaTreaderDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "ReaperLeviathanDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "GhostLeviathanDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaDragonDoll"),
                                     DecorationItemsHelper.getTechType(decorationItems, "SeaEmperorDoll"));
            
            var accessoriesTab = rootNode.AddTabNode("Accessories", LanguageHelper.GetFriendlyWord("Accessories"), SpriteManager.Get(TechType.LuggageBag));
            accessoriesTab.AddCraftingNode(TechType.LuggageBag,
                                           TechType.NutrientBlock);
            
            return rootNode;
        }
        
        public static GameObject GetPrefab()
        {
            // The standard Fabricator is the base to this new item
            GameObject originalPrefab = Resources.Load<GameObject>("Submarine/Build/Fabricator");
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            prefab.name = DecorationsFabID;

            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = DecorationsFabID;
            prefabId.name = LanguageHelper.GetFriendlyWord("DecorationsFabricatorName");

            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = DecorationsFabTechType;

            var fabricator = prefab.GetComponent<Fabricator>();
            fabricator.craftTree = DecorationsTreeType; // This is how the custom craft tree is associated to the fabricator

            // All this was necessary because the PowerRelay wasn't being instantiated
            var ghost = fabricator.GetComponent<GhostCrafter>();
            var powerRelay = new PowerRelay();
            // Ignore any errors you see about this fabricator not having a power relay in its parent. It does and it works.
            
            FieldInfo fieldInfo = typeof(GhostCrafter).GetField("powerRelay", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(ghost, powerRelay);

            // Set where this can be built
            var constructible = prefab.GetComponent<Constructable>();
            constructible.allowedInBase = true;
            constructible.allowedInSub = true; // This is the important one
            constructible.allowedOutside = false;
            constructible.allowedOnCeiling = false;
            constructible.allowedOnGround = false;
            constructible.allowedOnConstructables = false;
            constructible.controlModelState = true;
            constructible.techType = DecorationsFabTechType; // This was necessary to correctly associate the recipe at building time

            // Set the custom texture
            Texture2D coloredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_purple");
            SkinnedMeshRenderer skinnedMeshRenderer = prefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = coloredTexture;
            
            return prefab;
        }
    }
}
