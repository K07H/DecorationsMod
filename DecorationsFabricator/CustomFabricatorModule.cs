namespace DecorationsFabricator
{
    using System.Collections.Generic;
    using SMLHelper;
    using SMLHelper.Patchers;
    using UnityEngine;
    using System.Reflection;

    public class DecorationsFabricatorModule
    {
        public static CraftTree.Type DecorationsTreeType { get; private set; }
        public static TechType DecorationsFabTechType { get; private set; }
        public static TechType LabContainer4TechType { get; private set; }
        
        public static GameObject OriginalPosterObject { get; set; }
        public static GameObject OriginalPosterAuroraObject { get; set; }
        public static GameObject OriginalPosterExosuit1Object { get; set; }
        public static GameObject OriginalPosterExosuit2Object { get; set; }
        public static GameObject OriginalPosterKittyObject { get; set; }
        public static GameObject OriginalLabContainerObject { get; set; }
        public static GameObject OriginalLabContainer2Object { get; set; }
        public static GameObject OriginalLabContainer3Object { get; set; }
        public static GameObject OriginalLabContainer4Object { get; set; }
        public static GameObject OriginalLabEquipment1Object { get; set; }
        public static GameObject OriginalLabEquipment2Object { get; set; }
        public static GameObject OriginalLabEquipment3Object { get; set; }
        public static GameObject OriginalCap1Object { get; set; }
        public static GameObject OriginalCap2Object { get; set; }
        public static GameObject OriginalStarshipSouvenirObject { get; set; }
        public static GameObject OriginalArcadeGorgetoyObject { get; set; }
        public static GameObject OriginalToyCarObject { get; set; }
        public static GameObject OriginalLuggageBagObject { get; set; }

        // Get language.
        private static RegionAndLanguageHelper.CountryCode UserLanguage = RegionAndLanguageHelper.GetCountryCode();

        // This name will be used as both the new TechType of the buildable fabricator and the CraftTree Type for the custom crafting tree.
        public const string DecorationsFabID = "DecorationsFabricator";

        // This name will be used as the new TechType of the Lab Container 4
        public const string LabContainer4ID = "LabContainer4";

        #region Friendly names function

        private static string GetFriendlyWord(string word)
        {
            switch (word)
            {
                case "DecorationsFabricatorName":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Fabricateur de Décorations";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Fabricador de Decoraciones";
                    else
                        return "Decorations Fabricator";
                case "DecorationsFabricatorDescription":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Un fabricateur permettant de produire des objets décoratifs.";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Un Fabricador para producir artículos de decoración.";
                    else
                        return "A fabricator to produce decoration items.";
                case "Posters":
                    return "Posters";
                case "LabElements":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Éléments de Laboratoire";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Elementos de Laboratorio";
                    else
                        return "Laboratory Elements";
                case "GlassContainers":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Récipients en Verre Inutiles";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Envases de Vidrio Inútiles";
                    else
                        return "Useless Glass Containers";
                case "Caps":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Casquettes";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Gorras";
                    else
                        return "Caps";
                case "Toys":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Jouets";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Juguetes";
                    else
                        return "Toys";
                case "Accessories":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Accessoires";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Accesorios";
                    else
                        return "Accessories";
                case "LabContainer4Name":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Conteneur d'échantillons cylindrique long";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Contenedor de muestra cilíndrico largo";
                    else
                        return "Long cylindrical sample container";
                case "LabContainer4Description":
                    if (UserLanguage == RegionAndLanguageHelper.CountryCode.FR)
                        return "Un conteneur d'échantillons cylindrique long, probablement inutile.";
                    else if (UserLanguage == RegionAndLanguageHelper.CountryCode.ES)
                        return "Un contenedor de muestra cilíndrico largo, probablemente inútil.";
                    else
                        return "A long cylindrical sample container, probably useless.";
                default:
                    return "?";
            }
        }

        #endregion

        // Load AssetBundles (they must only be loaded once).
        private static AssetBundle Assets = AssetBundle.LoadFromFile(@"./QMods/DecorationsFabricator/Assets/decorations.assets");

        public static void Patch()
        {
            Logger.Log("Initializing Decorations Fabricator mod.", null);
            
            #region Lab Container 4

            // Create a new TechType for the Lab Container 4
            LabContainer4TechType = TechTypePatcher.AddTechType(LabContainer4ID, GetFriendlyWord("LabContainer4Name"), GetFriendlyWord("LabContainer4Description"), true);
            // Add the new TechType to the buildables
            CraftDataPatcher.customEquipmentTypes.Add(LabContainer4TechType, EquipmentType.Hand);
            // Set the buildable prefab
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(LabContainer4ID, $"WorldEntities/Doodads/Debris/Wrecks/Decoration/{LabContainer4ID}", LabContainer4TechType, GetLabContainer4Prefab));
            // Set the custom sprite for the Habitat Builder Tool menu
            CustomSpriteHandler.customSprites.Add(new CustomSprite(LabContainer4TechType, Assets.LoadAsset<Sprite>("labcontainer4")));
            // Create a recipe for the new TechType
            var LabContainer4Recipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[1]
                    {
                        new IngredientHelper(TechType.Glass, 1),
                    }),
                _techType = DecorationsFabTechType
            };
            // Associate recipe to the new TechType
            CraftDataPatcher.customTechData[LabContainer4TechType] = LabContainer4Recipe;

            #endregion

            // Create a new TechType for the new fabricator
            DecorationsFabTechType = TechTypePatcher.AddTechType(DecorationsFabID, GetFriendlyWord("DecorationsFabricatorName"), GetFriendlyWord("DecorationsFabricatorDescription"), true);

            // Create new CraftTree Type
            CustomCraftTreeRoot customTreeRootNode = CreateCustomTree(out CraftTree.Type craftType);
            DecorationsTreeType = craftType;
            
            #region Modify existing TechTypes
            
            // Modify prefab of Poster TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("72da21f9-f3e2-4183-ac57-d3679fb09122", "WorldEntities/Environment/Wrecks/Poster", TechType.Poster, GetPosterPrefab));
            // Modify prefab of Poster Aurora TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("876cbea4-b4bf-4311-8264-5118bfef291c", "WorldEntities/Environment/Wrecks/poster_aurora", TechType.PosterAurora, GetPosterAuroraPrefab));
            // Modify prefab of Poster Exosuit 1 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("336f276f-9546-40d0-98cb-974994dee3bf", "WorldEntities/Environment/Wrecks/poster_exosuit_01", TechType.PosterExoSuit1, GetPosterExosuit1Prefab));
            // Modify prefab of Poster Exosuit 2 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("d76dd251-492d-4bf9-8adb-25e59d709df2", "WorldEntities/Environment/Wrecks/poster_exosuit_02", TechType.PosterExoSuit2, GetPosterExosuit2Prefab));
            // Modify prefab of Poster Kitty TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("d809cb15-6784-4f7c-bf5d-f7d0c5bf8546", "WorldEntities/Environment/Wrecks/poster_kitty", TechType.PosterKitty, GetPosterKittyPrefab));

            // Modify prefab of LabContainer TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("e7f9c5e7-3906-4efd-b239-28783bce17a5", "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_01", TechType.LabContainer, GetLabContainerPrefab));
            // Modify prefab of LabContainer 2 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("e3e00261-92fc-4f52-bad2-4f0e5802a43d", "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_02", TechType.LabContainer2, GetLabContainer2Prefab));
            // Modify prefab of LabContainer 3 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("7f601dd4-0645-414d-bb62-5b0b62985836", "WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_tube_01", TechType.LabContainer3, GetLabContainer3Prefab));
            // Modify prefab of LabEquipment 1 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("2cee55bc-6136-47c5-a1ed-14c8f3203856", "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_01", TechType.LabEquipment1, GetLabEquipment1Prefab));
            // Modify prefab of LabEquipment 2 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("9c5f22de-5049-48bb-ad1e-0d78c894210e", "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_02", TechType.LabEquipment2, GetLabEquipment2Prefab));
            // Modify prefab of LabEquipment 3 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("3fd9050b-4baf-4a78-a883-e774c648887c", "WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_03", TechType.LabEquipment3, GetLabEquipment3Prefab));

            // Modify prefab of Cap 1 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("5884d27a-8798-4f09-82ec-c7671a604504", "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_02", TechType.Cap1, GetCap1Prefab));
            // Modify prefab of Cap 2 TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("3dc40631-2945-4109-acdc-823a9a0a8646", "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_03", TechType.Cap2, GetCap2Prefab));

            // Modify prefab of Starship Souvenir TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("c0d320d2-537e-4128-90ec-ab1466cfbbc3", "WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir", TechType.StarshipSouvenir, GetStarshipSouvenirPrefab));
            // Modify prefab of Arcade Gorgetoy TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("7ea4a91e-80fc-43aa-8ce3-5d52bd19e278", "WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_arcade_gorgetoy_01", TechType.ArcadeGorgetoy, GetArcadeGorgetoyPrefab));
            // Modify prefab of ToyCar TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("dfabc84e-c4c5-45d9-8b01-ca0eaeeb8e65", "WorldEntities/Doodads/Debris/Wrecks/Decoration/Goldglove_car_02", TechType.ToyCar, GetToyCarPrefab));

            // Modify prefab of LuggageBag TechType
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab("3616e7f3-5079-443d-85b4-9ad68fcbd924", "WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4", TechType.LuggageBag, GetLuggageBagPrefab));
            
            #endregion

            // Add the new Craft Tree and link it to the new CraftTree Type
            CraftTreePatcher.CustomTrees[DecorationsTreeType] = customTreeRootNode;
            
            // Create a recipe for the new TechType
            var customFabRecipe = new TechDataHelper()
            {
                _craftAmount = 1,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[4]
                    {
                        new IngredientHelper(TechType.Titanium, 2),
                        new IngredientHelper(TechType.ComputerChip, 1),
                        new IngredientHelper(TechType.Diamond, 1),
                        new IngredientHelper(TechType.Lead, 1),
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
            CustomSpriteHandler.customSprites.Add(new CustomSprite(DecorationsFabTechType, Assets.LoadAsset<Sprite>("fabricator_icon_purple")));

            // Associate recipe to the new TechType
            CraftDataPatcher.customTechData[DecorationsFabTechType] = customFabRecipe;

            // Define recipes for existing TechTypes
            SetCustomeRecipes();

            Logger.Log("Decorations Fabricator mod initialized successfully.", null);
        }

        #region CraftTree
        
        private static CustomCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType)
        {
            var rootNode = CraftTreeTypePatcher.AddCraftTreeType(DecorationsFabID, out craftType);

            var postersTab = rootNode.AddTabNode("Posters", GetFriendlyWord("Posters"), SpriteManager.Get(TechType.PosterKitty));
            postersTab.AddCraftingNode(TechType.PosterAurora,
                                       TechType.PosterExoSuit1,
                                       TechType.PosterExoSuit2,
                                       TechType.PosterKitty,
                                       TechType.Poster);

            var labEquipmentTab = rootNode.AddTabNode("LabElements", GetFriendlyWord("LabElements"), SpriteManager.Get(TechType.LabEquipment1));
            var glassContainersTab = labEquipmentTab.AddTabNode("GlassContainers", GetFriendlyWord("GlassContainers"), SpriteManager.Get(TechType.LabContainer2));
            glassContainersTab.AddCraftingNode(TechType.LabContainer,
                                               TechType.LabContainer2,
                                               TechType.LabContainer3,
                                               LabContainer4TechType);
            labEquipmentTab.AddCraftingNode(TechType.LabEquipment1,
                                            TechType.LabEquipment2,
                                            TechType.LabEquipment3);

            var capsTab = rootNode.AddTabNode("Caps", GetFriendlyWord("Caps"), SpriteManager.Get(TechType.Cap2));
            capsTab.AddCraftingNode(TechType.Cap1,
                                    TechType.Cap2);

            var toysTab = rootNode.AddTabNode("Toys", GetFriendlyWord("Toys"), SpriteManager.Get(TechType.ArcadeGorgetoy));
            toysTab.AddCraftingNode(TechType.StarshipSouvenir,
                                    TechType.ArcadeGorgetoy,
                                    TechType.ToyCar);

            var accessoriesTab = rootNode.AddTabNode("Accessories", GetFriendlyWord("Accessories"), SpriteManager.Get(TechType.LuggageBag));
            accessoriesTab.AddCraftingNode(TechType.LuggageBag);

            return rootNode;
        }

        #endregion

        #region Recipes

        private static void SetCustomRecipe(TechType techType, int craftAmount, List<IngredientHelper> ingredients)
        {
            var techData = new TechDataHelper
            {
                _craftAmount = craftAmount,
                _ingredients = ingredients,
                _techType = techType
            };
            CraftDataPatcher.customTechData.Add(techType, techData);
        }

        private static void SetCustomeRecipes()
        {
            List<IngredientHelper> posterRecipe = new List<IngredientHelper>();
            posterRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            posterRecipe.Add(new IngredientHelper(TechType.FiberMesh, 1));
            SetCustomRecipe(TechType.Poster, 1, posterRecipe);

            List<IngredientHelper> posterAuroraRecipe = new List<IngredientHelper>();
            posterAuroraRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            posterAuroraRecipe.Add(new IngredientHelper(TechType.FiberMesh, 1));
            SetCustomRecipe(TechType.PosterAurora, 1, posterAuroraRecipe);

            List<IngredientHelper> posterExoSuitARecipe = new List<IngredientHelper>();
            posterExoSuitARecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            posterExoSuitARecipe.Add(new IngredientHelper(TechType.FiberMesh, 1));
            SetCustomRecipe(TechType.PosterExoSuit1, 1, posterExoSuitARecipe);

            List<IngredientHelper> posterExoSuitBRecipe = new List<IngredientHelper>();
            posterExoSuitBRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            posterExoSuitBRecipe.Add(new IngredientHelper(TechType.FiberMesh, 1));
            SetCustomRecipe(TechType.PosterExoSuit2, 1, posterExoSuitBRecipe);

            List<IngredientHelper> posterKittyRecipe = new List<IngredientHelper>();
            posterKittyRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            posterKittyRecipe.Add(new IngredientHelper(TechType.FiberMesh, 1));
            SetCustomRecipe(TechType.PosterKitty, 1, posterKittyRecipe);

            List<IngredientHelper> labContainerARecipe = new List<IngredientHelper>();
            labContainerARecipe.Add(new IngredientHelper(TechType.Glass, 2));
            SetCustomRecipe(TechType.LabContainer, 1, labContainerARecipe);

            List<IngredientHelper> labContainerBRecipe = new List<IngredientHelper>();
            labContainerBRecipe.Add(new IngredientHelper(TechType.Glass, 1));
            SetCustomRecipe(TechType.LabContainer2, 1, labContainerBRecipe);

            List<IngredientHelper> labContainerCRecipe = new List<IngredientHelper>();
            labContainerCRecipe.Add(new IngredientHelper(TechType.Glass, 1));
            SetCustomRecipe(TechType.LabContainer3, 1, labContainerCRecipe);

            List<IngredientHelper> labEquipmentARecipe = new List<IngredientHelper>();
            labEquipmentARecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            labEquipmentARecipe.Add(new IngredientHelper(TechType.Glass, 1));
            SetCustomRecipe(TechType.LabEquipment1, 1, labEquipmentARecipe);

            List<IngredientHelper> labEquipmentBRecipe = new List<IngredientHelper>();
            labEquipmentBRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            labEquipmentBRecipe.Add(new IngredientHelper(TechType.Glass, 1));
            SetCustomRecipe(TechType.LabEquipment2, 1, labEquipmentBRecipe);

            List<IngredientHelper> labEquipmentCRecipe = new List<IngredientHelper>();
            labEquipmentCRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            labEquipmentCRecipe.Add(new IngredientHelper(TechType.Glass, 1));
            SetCustomRecipe(TechType.LabEquipment3, 1, labEquipmentCRecipe);

            List<IngredientHelper> capARecipe = new List<IngredientHelper>();
            capARecipe.Add(new IngredientHelper(TechType.FiberMesh, 1));
            SetCustomRecipe(TechType.Cap1, 1, capARecipe);

            List<IngredientHelper> capBRecipe = new List<IngredientHelper>();
            capBRecipe.Add(new IngredientHelper(TechType.FiberMesh, 1));
            SetCustomRecipe(TechType.Cap2, 1, capBRecipe);

            List<IngredientHelper> starshipSouvenirRecipe = new List<IngredientHelper>();
            starshipSouvenirRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            starshipSouvenirRecipe.Add(new IngredientHelper(TechType.Glass, 1));
            SetCustomRecipe(TechType.StarshipSouvenir, 1, starshipSouvenirRecipe);

            List<IngredientHelper> arcadeGorgetoyRecipe = new List<IngredientHelper>();
            arcadeGorgetoyRecipe.Add(new IngredientHelper(TechType.FiberMesh, 2));
            SetCustomRecipe(TechType.ArcadeGorgetoy, 1, arcadeGorgetoyRecipe);

            List<IngredientHelper> toyCarRecipe = new List<IngredientHelper>();
            toyCarRecipe.Add(new IngredientHelper(TechType.Titanium, 1));
            toyCarRecipe.Add(new IngredientHelper(TechType.Glass, 1));
            toyCarRecipe.Add(new IngredientHelper(TechType.Silicone, 1));
            SetCustomRecipe(TechType.ToyCar, 1, toyCarRecipe);

            List<IngredientHelper> carryAllRecipe = new List<IngredientHelper>();
            carryAllRecipe.Add(new IngredientHelper(TechType.FiberMesh, 2));
            carryAllRecipe.Add(new IngredientHelper(TechType.Silicone, 1));
            SetCustomRecipe(TechType.LuggageBag, 1, carryAllRecipe);
        }

        #endregion

        #region GetPrefab Delegates (Modifies existing prefabs)

        public static GameObject GetPosterPrefab()
        {
            GameObject originalPrefab = OriginalPosterObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.6f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.5f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }

        public static GameObject GetPosterAuroraPrefab()
        {
            GameObject originalPrefab = OriginalPosterAuroraObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.5f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.4f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }

        public static GameObject GetPosterExosuit1Prefab()
        {
            GameObject originalPrefab = OriginalPosterExosuit1Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.6f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.5f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }

        public static GameObject GetPosterExosuit2Prefab()
        {
            GameObject originalPrefab = OriginalPosterExosuit2Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.6f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.5f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }

        public static GameObject GetPosterKittyPrefab()
        {
            GameObject originalPrefab = OriginalPosterKittyObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.6f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.5f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.25f;

            return prefab;
        }

        public static GameObject GetLabContainerPrefab()
        {
            GameObject originalPrefab = OriginalLabContainerObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("biodome_lab_containers_close_01").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.80f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }

        public static GameObject GetLabContainer2Prefab()
        {
            GameObject originalPrefab = OriginalLabContainer2Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("biodome_lab_containers_close_02").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.50f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }

        public static GameObject GetLabContainer3Prefab()
        {
            GameObject originalPrefab = OriginalLabContainer3Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("biodome_lab_containers_tube_01").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.36f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }
        
        public static GameObject GetLabContainer4Prefab()
        {
            GameObject originalPrefab = OriginalLabContainer4Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);
            GameObject model = prefab.FindChild("biodome_lab_containers_tube_02");

            // Set TechTag
            prefab.AddComponent<TechTag>().type = LabContainer4TechType;
            
            // Add sky applier
            var skyApplier = prefab.AddComponent<SkyApplier>();
            skyApplier.anchorSky = Skies.Custom;
            skyApplier.dynamic = false;
            skyApplier.emissiveFromPower = false;
            
            // Add box collider
            var collider = prefab.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.43f, 0.25f, 0.07f);

            // Detroy immediate rigid body
            var rb = prefab.GetComponent<Rigidbody>();
            MonoBehaviour.DestroyImmediate(rb);

            // We can pick this item
            var pickupable = prefab.AddComponent<Pickupable>();
            pickupable.isPickupable = true;
            pickupable.randomizeRotationWhenDropped = true;

            var pickPrefab = prefab.AddComponent<PickPrefab>();
            pickPrefab.destroyOnPicked = false;
            pickPrefab.pickTech = LabContainer4TechType;

            // We can place this item
            var placeTool = prefab.AddComponent<PlaceTool>();
            placeTool.allowedInBase = true;
            placeTool.allowedOnBase = true;
            placeTool.allowedOnCeiling = false;
            placeTool.allowedOnConstructable = true;
            placeTool.allowedOnGround = true;
            placeTool.allowedOnRigidBody = false;
            placeTool.allowedOnWalls = false;
            placeTool.allowedOutside = false;
            placeTool.rotationEnabled = true;
            placeTool.dropTime = 0.5f;
            placeTool.drawTime = 0.5f;
            placeTool.enabled = true;
            placeTool.hasAnimations = false;
            placeTool.hasBashAnimation = false;
            placeTool.hasFirstUseAnimation = false;
            placeTool.holsterTime = 0.5f;
            placeTool.ikAimRightArm = true;
            placeTool.mainCollider = collider;
            placeTool.pickupable = pickupable;
            placeTool.reloadMode = PlayerTool.ReloadMode.None;
            placeTool.socket = PlayerTool.Socket.RightHand;

            // Add large world entity and set cell level
            var largeWorldEntity = prefab.AddComponent<LargeWorldEntity>();
            largeWorldEntity.cellLevel = LargeWorldEntity.CellLevel.Near;
            
            // Add fabricating animation
            var fabricating = model.AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.36f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            // Dont destroy GameObject on load
            var gameObj = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(gameObj);

            return prefab;
        }

        public static GameObject GetLabEquipment1Prefab()
        {
            GameObject originalPrefab = OriginalLabEquipment1Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("discovery_lab_props_01").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
            fabricating.scaleFactor = 0.75f;

            return prefab;
        }

        public static GameObject GetLabEquipment2Prefab()
        {
            GameObject originalPrefab = OriginalLabEquipment2Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("discovery_lab_props_02").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.75f;

            return prefab;
        }

        public static GameObject GetLabEquipment3Prefab()
        {
            GameObject originalPrefab = OriginalLabEquipment3Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("discovery_lab_props_03").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.8f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.75f;

            return prefab;
        }

        public static GameObject GetCap1Prefab()
        {
            GameObject originalPrefab = OriginalCap1Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_plaza_shelf_cap_02").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.06f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }

        public static GameObject GetCap2Prefab()
        {
            GameObject originalPrefab = OriginalCap2Object;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_plaza_shelf_cap_03").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.2f;
            fabricating.posOffset = new Vector3(0f, 0.06f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }

        public static GameObject GetStarshipSouvenirPrefab()
        {
            GameObject originalPrefab = OriginalStarshipSouvenirObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("starship_souvenir").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.5f;
            fabricating.posOffset = new Vector3(0f, 0.1f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, -90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }

        public static GameObject GetArcadeGorgetoyPrefab()
        {
            GameObject originalPrefab = OriginalArcadeGorgetoyObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("descent_arcade_gorgetoy_01").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.1f;
            fabricating.localMaxY = 0.6f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(-90f, 0f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }

        public static GameObject GetToyCarPrefab()
        {
            GameObject originalPrefab = OriginalToyCarObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.04f;
            fabricating.localMaxY = 0.25f;
            fabricating.posOffset = new Vector3(-0.05f, 0f, -0.06f);
            fabricating.eulerOffset = new Vector3(0f, 90f, 0f);
            fabricating.scaleFactor = 1f;

            return prefab;
        }

        public static GameObject GetLuggageBagPrefab()
        {
            GameObject originalPrefab = OriginalLuggageBagObject;
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            // Add fabricating animation
            var fabricating = prefab.FindChild("model").AddComponent<VFXFabricating>();

            // Tune fabricating animation
            fabricating.localMinY = -0.2f;
            fabricating.localMaxY = 0.7f;
            fabricating.posOffset = new Vector3(0f, 0f, 0.04f);
            fabricating.eulerOffset = new Vector3(0f, 0f, 0f);
            fabricating.scaleFactor = 0.8f;

            return prefab;
        }

        public static GameObject GetPrefab()
        {
            // The standard Fabricator is the base to this new item
            GameObject originalPrefab = Resources.Load<GameObject>("Submarine/Build/Fabricator");
            GameObject prefab = GameObject.Instantiate(originalPrefab);

            prefab.name = DecorationsFabID;

            var prefabId = prefab.GetComponent<PrefabIdentifier>();
            prefabId.ClassId = DecorationsFabID;
            prefabId.name = GetFriendlyWord("DecorationsFabricatorName");

            var techTag = prefab.GetComponent<TechTag>();
            techTag.type = DecorationsFabTechType;

            var fabricator = prefab.GetComponent<Fabricator>();
            fabricator.craftTree = DecorationsTreeType; // This is how the custom craft tree is associated to the fabricator

            // All this was necessary because the PowerRelay wasn't being instantiated
            var ghost = fabricator.GetComponent<GhostCrafter>();
            var crafter = ghost.GetComponent<Crafter>();
            var powerRelay = new PowerRelay();

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
            var coloredTexture = Assets.LoadAsset<Texture2D>("submarine_fabricator_purple");

            var skinnedMeshRenderer = prefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = coloredTexture;

            // Add a slight purple tint to the material for added effect
            skinnedMeshRenderer.material.color = new Color(0.87f, 0.8f, 0.95f);

            return prefab;
        }

        #endregion
    }
}
