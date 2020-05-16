using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod
{
    public class Fabricator_Flora : ModPrefab
    {
        public CraftTree.Type TreeTypeID { get; private set; }
        
        public const string FloraFabID = "FloraFabricator";

        public const string HandOverText = "UseFloraFabricator";

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
                new Ingredient(TechType.Magnetite, 1)
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
                new Ingredient(TechType.Magnetite, 1)
            })
        };
#endif

        internal Fabricator_Flora() : base("", "")
        {
            this.ClassID = FloraFabID;
            this.PrefabFileName = $"Submarine/Build/{FloraFabID}";
            this.TechType = TechTypeHandler.AddTechType(FloraFabID,
                    LanguageHelper.GetFriendlyWord("FloraFabricatorName"),
                    LanguageHelper.GetFriendlyWord("FloraFabricatorDescription"),
                    true);
        }

        public void RegisterFloraFabricator(List<IDecorationItem> decorationItems)
        {
            if (this.IsRegistered == false)
            {
                // Create new Craft Tree Type
                CreateCustomTree(out CraftTree.Type craftType, decorationItems);
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
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_green"));

                // Load texture
                this.ColoredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_green");

                // Associate the recipie to the new TechType
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
                
                this.IsRegistered = true;
            }
        }

        private ModCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems)
        {
            ModCraftTreeRoot rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(FloraFabID, out craftType);

            ModCraftTreeTab plantAirTab;
            ModCraftTreeTab treeAirTab;
            ModCraftTreeTab tropicalPlantTab;
            if (ConfigSwitcher.UseFlatScreenResolution)
            {
                // Additional tab
                ModCraftTreeTab airSeedsTab = rootNode.AddTabNode("AirSeedsTab", LanguageHelper.GetFriendlyWord("AirSeedsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("landtree1seedicon"));

                // Plant Air
                plantAirTab = airSeedsTab.AddTabNode("PlantAirTab", LanguageHelper.GetFriendlyWord("PlantAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("landplant1icon"));
                // Tree Air
                treeAirTab = airSeedsTab.AddTabNode("TreeAirTab", LanguageHelper.GetFriendlyWord("TreeAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("landtree1seedicon"));
                // Tropical
                tropicalPlantTab = airSeedsTab.AddTabNode("TropicalPlantTab", LanguageHelper.GetFriendlyWord("TropicalPlantTab"), AssetsHelper.Assets.LoadAsset<Sprite>("tropicalplant1bicon"));
            }
            else
            {
                // Plant Air
                plantAirTab = rootNode.AddTabNode("PlantAirTab", LanguageHelper.GetFriendlyWord("PlantAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("landplant1icon"));
                // Tree Air
                treeAirTab = rootNode.AddTabNode("TreeAirTab", LanguageHelper.GetFriendlyWord("TreeAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("landtree1seedicon"));
                // Tropical
                tropicalPlantTab = rootNode.AddTabNode("TropicalPlantTab", LanguageHelper.GetFriendlyWord("TropicalPlantTab"), AssetsHelper.Assets.LoadAsset<Sprite>("tropicalplant1bicon"));
            }
            // Plant Air
            plantAirTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LandPlant1"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant2"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant3"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant4"),
                                        DecorationItemsHelper.getTechType(decorationItems, "LandPlant5"));
            // Tree Air
            treeAirTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "LandTree1"),
                                       DecorationItemsHelper.getTechType(decorationItems, "JungleTree1"),
                                       DecorationItemsHelper.getTechType(decorationItems, "JungleTree2"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant3a"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant3b"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant6a"),
                                       DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant6b"));
            // Tropical
            tropicalPlantTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant1a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant1b"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant2a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant2b"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant7a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant7b"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant10a"),
                                             DecorationItemsHelper.getTechType(decorationItems, "TropicalPlant10b"),
                                             DecorationItemsHelper.getTechType(decorationItems, "Fern2"),
                                             DecorationItemsHelper.getTechType(decorationItems, "Fern4"));

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

            ModCraftTreeTab plantWaterTab;
            ModCraftTreeTab treeWaterTab;
            if (ConfigSwitcher.UseFlatScreenResolution)
            {
                // Additional tab
                ModCraftTreeTab waterSeedsTab = rootNode.AddTabNode("WaterSeedsTab", LanguageHelper.GetFriendlyWord("WaterSeedsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("floatingstone1icon"));

                // Plant Water
                plantWaterTab = waterSeedsTab.AddTabNode("PlantWaterTab", LanguageHelper.GetFriendlyWord("PlantWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("spottedreedsicon"));
                // Tree Water
                treeWaterTab = waterSeedsTab.AddTabNode("TreeWaterTab", LanguageHelper.GetFriendlyWord("TreeWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("floatingstone1icon"));
            }
            else
            {
                // Plant Water
                plantWaterTab = rootNode.AddTabNode("PlantWaterTab", LanguageHelper.GetFriendlyWord("PlantWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("spottedreedsicon"));
                // Tree Water
                treeWaterTab = rootNode.AddTabNode("TreeWaterTab", LanguageHelper.GetFriendlyWord("TreeWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("floatingstone1icon"));
            }
            // Plant Water
            plantWaterTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "GreenReeds1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "GreenReeds6"),
                                          DecorationItemsHelper.getTechType(decorationItems, "LostRiverPlant2"),
                                          DecorationItemsHelper.getTechType(decorationItems, "LostRiverPlant4"),
                                          DecorationItemsHelper.getTechType(decorationItems, "PlantMiddle11"));
            var redGrassesTab = plantWaterTab.AddTabNode("RedGrassesTab", LanguageHelper.GetFriendlyWord("RedGrassesTab"), AssetsHelper.Assets.LoadAsset<Sprite>("redgrass"));
            redGrassesTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BloodGrassRed"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BloodGrassDense"),
                                          DecorationItemsHelper.getTechType(decorationItems, "RedGrass1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "RedGrass2"),
                                          DecorationItemsHelper.getTechType(decorationItems, "RedGrass2Tall"),
                                          DecorationItemsHelper.getTechType(decorationItems, "RedGrass3"),
                                          DecorationItemsHelper.getTechType(decorationItems, "RedGrass3Tall")); 

            // Tree Water
            treeWaterTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "CrabClawKelp2"),
                                         DecorationItemsHelper.getTechType(decorationItems, "CrabClawKelp1"),
                                         DecorationItemsHelper.getTechType(decorationItems, "CrabClawKelp3"),
                                         DecorationItemsHelper.getTechType(decorationItems, "PyroCoral1"),
                                         DecorationItemsHelper.getTechType(decorationItems, "PyroCoral2"),
                                         DecorationItemsHelper.getTechType(decorationItems, "PyroCoral3"),
                                         DecorationItemsHelper.getTechType(decorationItems, "FloatingStone1"));

            // Amphibious plants
            var amphibiousPlantsTab = rootNode.AddTabNode("AmphibiousPlantsTab", LanguageHelper.GetFriendlyWord("AmphibiousPlantsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("covetreeicon"));
            amphibiousPlantsTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "SmallDeco3"),
                                                DecorationItemsHelper.getTechType(decorationItems, "CoveTree1"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco11"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco13"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco14"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco15Red"),
                                                DecorationItemsHelper.getTechType(decorationItems, "SmallDeco17Purple"));

            // Coral Water
            var coralWaterTab = rootNode.AddTabNode("CoralWaterTab", LanguageHelper.GetFriendlyWord("CoralWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("flora_smalldeco10icon"));
            coralWaterTab.AddCraftingNode(DecorationItemsHelper.getTechType(decorationItems, "BrownCoralTubes1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BrownCoralTubes2"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BrownCoralTubes3"),
                                          DecorationItemsHelper.getTechType(decorationItems, "BlueCoralTubes1"),
                                          DecorationItemsHelper.getTechType(decorationItems, "SmallDeco10"));

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
            prefabId.name = LanguageHelper.GetFriendlyWord("FloraFabricatorName");

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
