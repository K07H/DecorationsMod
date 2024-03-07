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
    public class Fabricator_Flora : CustomPrefab
#else
    public class Fabricator_Flora : ModPrefab
#endif
    {
        public CraftTree.Type TreeTypeID { get; private set; }
        
        public const string FloraFabID = "FloraFabricator";

        public const string HandOverText = "UseFloraFabricator";

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
                new Ingredient(TechType.Magnetite, 1)
            })
        };

#if SUBNAUTICA_NAUTILUS

        [SetsRequiredMembers]
        internal Fabricator_Flora() : base(
            PrefabInfo.WithTechType(FloraFabID, LanguageHelper.GetFriendlyWord("FloraFabricatorName"), LanguageHelper.GetFriendlyWord("FloraFabricatorDescription"), unlockAtStart: true)
            .WithFileName($"Submarine/Build/{FloraFabID}")
            .WithIcon(AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_green"))
            )
        {
            SetGameObject(this.GetGameObject);
        }
#else
        internal Fabricator_Flora() : base("", "")
        {
            this.ClassID = FloraFabID;
            this.PrefabFileName = $"Submarine/Build/{FloraFabID}";
            this.TechType = TechTypeHandler.AddTechType(FloraFabID,
                    LanguageHelper.GetFriendlyWord("FloraFabricatorName"),
                    LanguageHelper.GetFriendlyWord("FloraFabricatorDescription"),
                    true);
        }
#endif

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
                CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType, TechType.Fabricator);
                
                // Register handover text
                LanguageHandler.SetLanguageLine(HandOverText, LanguageHelper.GetFriendlyWord(HandOverText));

                // Unlock at start
                KnownTechHandler.UnlockOnStart(this.TechType);

                // Set the buildable prefab

#if SUBNAUTICA_NAUTILUS

                this.Register();

#else
                PrefabHandler.RegisterPrefab(this);
                
                // Register sprite
                SpriteHandler.RegisterSprite(this.TechType, AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_green"));
#endif

                // Load texture
                this.ColoredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_green");

                // Associate the recipie to the new TechType
#if SUBNAUTICA_NAUTILUS
                CraftDataHandler.SetRecipeData(this.TechType, this.Recipe);
#else
                CraftDataHandler.SetTechData(this.TechType, this.Recipe);
#endif
                
                this.IsRegistered = true;
            }
        }

        private ModCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems)
        {
#if SUBNAUTICA_NAUTILUS
            craftType = EnumHandler.AddEntry<CraftTree.Type>(FloraFabID).CreateCraftTreeRoot(out ModCraftTreeRoot rootNode);

            ModCraftTreeRoot plantAirTab;
            ModCraftTreeRoot treeAirTab;
            ModCraftTreeRoot tropicalPlantTab;
#else
            ModCraftTreeRoot rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(FloraFabID, out craftType);

            ModCraftTreeTab plantAirTab;
            ModCraftTreeTab treeAirTab;
            ModCraftTreeTab tropicalPlantTab;
#endif
            if (ConfigSwitcher.UseFlatScreenResolution)
            {
                // Additional tab
                var airSeedsTab = rootNode.AddTabNode("AirSeedsTab", LanguageHelper.GetFriendlyWord("AirSeedsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_NonHarvastableAir"));

                // Plant Air
                plantAirTab = airSeedsTab.AddTabNode("PlantAirTab", LanguageHelper.GetFriendlyWord("PlantAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Plants"));
                // Tree Air
                treeAirTab = airSeedsTab.AddTabNode("TreeAirTab", LanguageHelper.GetFriendlyWord("TreeAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Trees"));
                // Tropical
                tropicalPlantTab = airSeedsTab.AddTabNode("TropicalPlantTab", LanguageHelper.GetFriendlyWord("TropicalPlantTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_TropicalPlants"));
            }
            else
            {
                // Plant Air
                plantAirTab = rootNode.AddTabNode("PlantAirTab", LanguageHelper.GetFriendlyWord("PlantAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Plants"));
                // Tree Air
                treeAirTab = rootNode.AddTabNode("TreeAirTab", LanguageHelper.GetFriendlyWord("TreeAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Trees"));
                // Tropical
                tropicalPlantTab = rootNode.AddTabNode("TropicalPlantTab", LanguageHelper.GetFriendlyWord("TropicalPlantTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_TropicalPlants"));
            }
            // Plant Air
            plantAirTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LandPlant1"),
                                        PrefabsHelper.GetTechType(decorationItems, "LandPlant2"),
                                        PrefabsHelper.GetTechType(decorationItems, "LandPlant3"),
                                        PrefabsHelper.GetTechType(decorationItems, "LandPlant4"),
                                        PrefabsHelper.GetTechType(decorationItems, "LandPlant5"));
            // Tree Air
            treeAirTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "LandTree1"),
                                       PrefabsHelper.GetTechType(decorationItems, "JungleTree1"),
                                       PrefabsHelper.GetTechType(decorationItems, "JungleTree2"),
                                       PrefabsHelper.GetTechType(decorationItems, "TropicalPlant3a"),
                                       PrefabsHelper.GetTechType(decorationItems, "TropicalPlant3b"),
                                       PrefabsHelper.GetTechType(decorationItems, "TropicalPlant6a"),
                                       PrefabsHelper.GetTechType(decorationItems, "TropicalPlant6b"));
            // Tropical
            tropicalPlantTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "TropicalPlant1a"),
                                             PrefabsHelper.GetTechType(decorationItems, "TropicalPlant1b"),
                                             PrefabsHelper.GetTechType(decorationItems, "TropicalPlant2a"),
                                             PrefabsHelper.GetTechType(decorationItems, "TropicalPlant2b"),
                                             PrefabsHelper.GetTechType(decorationItems, "TropicalPlant7a"),
                                             PrefabsHelper.GetTechType(decorationItems, "TropicalPlant7b"),
                                             PrefabsHelper.GetTechType(decorationItems, "TropicalPlant10a"),
                                             PrefabsHelper.GetTechType(decorationItems, "TropicalPlant10b"),
                                             PrefabsHelper.GetTechType(decorationItems, "Fern2"),
                                             PrefabsHelper.GetTechType(decorationItems, "Fern4"));

            // Existing air seeds from the game
            if (ConfigSwitcher.EnableRegularAirSeeds)
            {
                var regularAirSeedsTab = rootNode.AddTabNode("RegularAirSeedsTab", LanguageHelper.GetFriendlyWord("RegularAirSeedsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_HarvastableAir"));
                
                var edibleRegularAirTab = regularAirSeedsTab.AddTabNode("EdibleRegularAirTab", LanguageHelper.GetFriendlyWord("EdibleRegularAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Edibles"));
                edibleRegularAirTab.AddCraftingNode(TechType.BulboTreePiece,
                                                    TechType.PurpleVegetable,
                                                    TechType.HangingFruit,
                                                    TechType.MelonSeed,
                                                    PrefabsHelper.GetTechType(decorationItems, "MarbleMelonTiny"));

                var decorativeBigAirTab = regularAirSeedsTab.AddTabNode("DecorativeBigAirTab", LanguageHelper.GetFriendlyWord("DecorativeBigAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_BigDeco"));
                decorativeBigAirTab.AddCraftingNode(TechType.FernPalmSeed,
                                                    TechType.OrangePetalsPlantSeed,
                                                    TechType.PurpleVasePlantSeed,
                                                    TechType.OrangeMushroomSpore);

                var decorativeSmallAirTab = regularAirSeedsTab.AddTabNode("DecorativeSmallAirTab", LanguageHelper.GetFriendlyWord("DecorativeSmallAirTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_SmallDeco"));
                decorativeSmallAirTab.AddCraftingNode(TechType.PinkMushroomSpore,
                                                    TechType.PurpleRattleSpore,
                                                    TechType.PinkFlowerSeed);
            }

            // Existing water seeds from the game
            if (ConfigSwitcher.EnableRegularWaterSeeds)
            {
                var regularWaterSeedsTab = rootNode.AddTabNode("RegularWaterSeedsTab", LanguageHelper.GetFriendlyWord("RegularWaterSeedsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_HarvastableAqua"));

                var decorativeBushesWaterTab = regularWaterSeedsTab.AddTabNode("DecorativeBushesWaterTab", LanguageHelper.GetFriendlyWord("DecorativeBushesWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Bush"));
                decorativeBushesWaterTab.AddCraftingNode(TechType.PurpleBranchesSeed,
                                                         TechType.RedRollPlantSeed,
                                                         TechType.RedBushSeed,
                                                         TechType.PurpleStalkSeed,
                                                         TechType.SpottedLeavesPlantSeed);

                var regularSmallWaterTab = regularWaterSeedsTab.AddTabNode("RegularSmallWaterTab", LanguageHelper.GetFriendlyWord("RegularSmallWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_AquaSmall"));
                regularSmallWaterTab.AddCraftingNode(TechType.SmallFanSeed,
                                                     TechType.PurpleFanSeed,
                                                     TechType.PurpleTentacleSeed);

                var decorativeBigWaterTab = regularWaterSeedsTab.AddTabNode("DecorativeBigWaterTab", LanguageHelper.GetFriendlyWord("DecorativeBigWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Big"));
                decorativeBigWaterTab.AddCraftingNode(TechType.SeaCrownSeed,
                                                      TechType.GabeSFeatherSeed,
                                                      TechType.RedGreenTentacleSeed,
                                                      TechType.ShellGrassSeed,
                                                      TechType.BluePalmSeed,
                                                      TechType.EyesPlantSeed,
                                                      TechType.MembrainTreeSeed,
                                                      TechType.RedConePlantSeed,
                                                      TechType.RedBasketPlantSeed,
                                                      TechType.SnakeMushroomSpore,
                                                      TechType.SpikePlantSeed);

                var functionalBigWaterTab = regularWaterSeedsTab.AddTabNode("FunctionalBigWaterTab", LanguageHelper.GetFriendlyWord("FunctionalBigWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Functional"));
                functionalBigWaterTab.AddCraftingNode(TechType.AcidMushroomSpore,
                                                      TechType.WhiteMushroomSpore,
                                                      TechType.JellyPlantSeed,
                                                      TechType.CreepvinePiece,
                                                      TechType.CreepvineSeedCluster,
                                                      TechType.BloodOil,
                                                      TechType.PurpleBrainCoralPiece,
                                                      TechType.KooshChunk);
            }

#if SUBNAUTICA_NAUTILUS
            ModCraftTreeRoot plantWaterTab;
            ModCraftTreeRoot treeWaterTab;
#else
            ModCraftTreeTab plantWaterTab;
            ModCraftTreeTab treeWaterTab;
#endif
            if (ConfigSwitcher.UseFlatScreenResolution)
            {
                // Additional tab
                var waterSeedsTab = rootNode.AddTabNode("WaterSeedsTab", LanguageHelper.GetFriendlyWord("WaterSeedsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_NonHarvastableAqua"));

                // Plant Water
                plantWaterTab = waterSeedsTab.AddTabNode("PlantWaterTab", LanguageHelper.GetFriendlyWord("PlantWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_AquaticPlants"));
                // Tree Water
                treeWaterTab = waterSeedsTab.AddTabNode("TreeWaterTab", LanguageHelper.GetFriendlyWord("TreeWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_AquaticTrees"));
            }
            else
            {
                // Plant Water
                plantWaterTab = rootNode.AddTabNode("PlantWaterTab", LanguageHelper.GetFriendlyWord("PlantWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_AquaticPlants"));
                // Tree Water
                treeWaterTab = rootNode.AddTabNode("TreeWaterTab", LanguageHelper.GetFriendlyWord("TreeWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_AquaticTrees"));
            }
            // Plant Water
            plantWaterTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "GreenReeds1"),
                                          PrefabsHelper.GetTechType(decorationItems, "GreenReeds6"),
                                          PrefabsHelper.GetTechType(decorationItems, "LostRiverPlant2"),
                                          PrefabsHelper.GetTechType(decorationItems, "LostRiverPlant4"),
                                          PrefabsHelper.GetTechType(decorationItems, "PlantMiddle11"));
            var redGrassesTab = plantWaterTab.AddTabNode("RedGrassesTab", LanguageHelper.GetFriendlyWord("RedGrassesTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Grass"));
            redGrassesTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BloodGrassRed"),
                                          PrefabsHelper.GetTechType(decorationItems, "BloodGrassDense"),
                                          PrefabsHelper.GetTechType(decorationItems, "RedGrass1"),
                                          PrefabsHelper.GetTechType(decorationItems, "RedGrass2"),
                                          PrefabsHelper.GetTechType(decorationItems, "RedGrass2Tall"),
                                          PrefabsHelper.GetTechType(decorationItems, "RedGrass3"),
                                          PrefabsHelper.GetTechType(decorationItems, "RedGrass3Tall")); 

            // Tree Water
            treeWaterTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "MushroomTree2"),
                                         PrefabsHelper.GetTechType(decorationItems, "MushroomTree1"),
                                         PrefabsHelper.GetTechType(decorationItems, "CrabClawKelp2"),
                                         PrefabsHelper.GetTechType(decorationItems, "CrabClawKelp1"),
                                         PrefabsHelper.GetTechType(decorationItems, "CrabClawKelp3"),
                                         PrefabsHelper.GetTechType(decorationItems, "PyroCoral1"),
                                         PrefabsHelper.GetTechType(decorationItems, "PyroCoral2"),
                                         PrefabsHelper.GetTechType(decorationItems, "PyroCoral3"),
                                         PrefabsHelper.GetTechType(decorationItems, "FloatingStone1"));

            // Amphibious plants
            var amphibiousPlantsTab = rootNode.AddTabNode("AmphibiousPlantsTab", LanguageHelper.GetFriendlyWord("AmphibiousPlantsTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_AmphibiousPlants"));
            amphibiousPlantsTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "SmallDeco3"),
                                                PrefabsHelper.GetTechType(decorationItems, "CoveTree1"),
                                                PrefabsHelper.GetTechType(decorationItems, "CoveTree2"),
                                                PrefabsHelper.GetTechType(decorationItems, "SmallDeco11"),
                                                PrefabsHelper.GetTechType(decorationItems, "SmallDeco13"),
                                                PrefabsHelper.GetTechType(decorationItems, "SmallDeco14"),
                                                PrefabsHelper.GetTechType(decorationItems, "SmallDeco15Red"),
                                                PrefabsHelper.GetTechType(decorationItems, "SmallDeco17Purple"));

            // Coral Water
            var coralWaterTab = rootNode.AddTabNode("CoralWaterTab", LanguageHelper.GetFriendlyWord("CoralWaterTab"), AssetsHelper.Assets.LoadAsset<Sprite>("D_Corals"));
            coralWaterTab.AddCraftingNode(PrefabsHelper.GetTechType(decorationItems, "BrownCoralTubes1"),
                                          PrefabsHelper.GetTechType(decorationItems, "BrownCoralTubes2"),
                                          PrefabsHelper.GetTechType(decorationItems, "BrownCoralTubes3"),
                                          PrefabsHelper.GetTechType(decorationItems, "BlueCoralTubes1"),
                                          PrefabsHelper.GetTechType(decorationItems, "SmallDeco10"));

            return rootNode;
        }

#if SUBNAUTICA_NAUTILUS
        public GameObject GetGameObject()
#else
        public override GameObject GetGameObject()
#endif
        {
            if (Fabricator_Decorations._decorationsFabricator == null)
                Fabricator_Decorations._decorationsFabricator = PrefabsHelper.LoadGameObjectFromFilename("Submarine/Build/Fabricator.prefab");

            // Instantiate CyclopsFabricator object
            var fabricatorPrefab = GameObject.Instantiate(Fabricator_Decorations._decorationsFabricator);

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
            constructible.placeMinDistance = 0.5f;

            // Set the custom texture
            SkinnedMeshRenderer skinnedMeshRenderer = fabricatorPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = this.ColoredTexture;

            return fabricatorPrefab;
        }

        public static readonly Dictionary<TechType, TechType> AirPlants = new Dictionary<TechType, TechType>()
        {
            { TechType.BulboTree, TechType.BulboTreePiece },
            { TechType.BulboTreePiece, TechType.BulboTreePiece },
            { TechType.PurpleVegetablePlant, TechType.PurpleVegetable },
            { TechType.PurpleVegetable, TechType.PurpleVegetable },
            { TechType.HangingFruitTree, TechType.HangingFruit },
            { TechType.HangingFruit, TechType.HangingFruit },
            { TechType.MelonPlant, TechType.MelonSeed },
            { TechType.MelonSeed, TechType.MelonSeed },
            { TechType.FernPalm, TechType.FernPalmSeed },
            { TechType.FernPalmSeed, TechType.FernPalmSeed },
            { TechType.OrangePetalsPlant, TechType.OrangePetalsPlantSeed },
            { TechType.OrangePetalsPlantSeed, TechType.OrangePetalsPlantSeed },
            { TechType.PurpleVasePlant, TechType.PurpleVasePlantSeed },
            { TechType.PurpleVasePlantSeed, TechType.PurpleVasePlantSeed },
            { TechType.OrangeMushroom, TechType.OrangeMushroomSpore },
            { TechType.OrangeMushroomSpore, TechType.OrangeMushroomSpore },
            { TechType.PinkMushroom, TechType.PinkMushroomSpore },
            { TechType.PinkMushroomSpore, TechType.PinkMushroomSpore },
            { TechType.PurpleRattle, TechType.PurpleRattleSpore },
            { TechType.PurpleRattleSpore, TechType.PurpleRattleSpore },
            { TechType.PinkFlower, TechType.PinkFlowerSeed },
            { TechType.PinkFlowerSeed, TechType.PinkFlowerSeed }
        };

        public static readonly Dictionary<TechType, TechType> WaterPlants = new Dictionary<TechType, TechType>()
        {
            { TechType.GabeSFeather, TechType.GabeSFeatherSeed },
            { TechType.GabeSFeatherSeed, TechType.GabeSFeatherSeed },
            { TechType.RedGreenTentacle, TechType.RedGreenTentacleSeed },
            { TechType.RedGreenTentacleSeed, TechType.RedGreenTentacleSeed },
            { TechType.SeaCrown, TechType.SeaCrownSeed },
            { TechType.SeaCrownSeed, TechType.SeaCrownSeed },
            { TechType.ShellGrass, TechType.ShellGrassSeed },
            { TechType.ShellGrassSeed, TechType.ShellGrassSeed },
            { TechType.PurpleBranches, TechType.PurpleBranchesSeed },
            { TechType.PurpleBranchesSeed, TechType.PurpleBranchesSeed },
            { TechType.RedRollPlant, TechType.RedRollPlantSeed },
            { TechType.RedRollPlantSeed, TechType.RedRollPlantSeed },
            { TechType.RedBush, TechType.RedBushSeed },
            { TechType.RedBushSeed, TechType.RedBushSeed },
            { TechType.PurpleStalk, TechType.PurpleStalkSeed },
            { TechType.PurpleStalkSeed, TechType.PurpleStalkSeed },
            { TechType.SpottedLeavesPlant, TechType.SpottedLeavesPlantSeed },
            { TechType.SpottedLeavesPlantSeed, TechType.SpottedLeavesPlantSeed },
            { TechType.AcidMushroom, TechType.AcidMushroomSpore },
            { TechType.AcidMushroomSpore, TechType.AcidMushroomSpore },
            { TechType.WhiteMushroom, TechType.WhiteMushroomSpore },
            { TechType.WhiteMushroomSpore, TechType.WhiteMushroomSpore },
            { TechType.JellyPlant, TechType.JellyPlantSeed },
            { TechType.JellyPlantSeed, TechType.JellyPlantSeed },
            { TechType.SmallFan, TechType.SmallFanSeed },
            { TechType.SmallFanSeed, TechType.SmallFanSeed },
            { TechType.PurpleFan, TechType.PurpleFanSeed },
            { TechType.PurpleFanSeed, TechType.PurpleFanSeed },
            { TechType.PurpleTentacle, TechType.PurpleTentacleSeed },
            { TechType.PurpleTentacleSeed, TechType.PurpleTentacleSeed },
            { TechType.BluePalm, TechType.BluePalmSeed },
            { TechType.BluePalmSeed, TechType.BluePalmSeed },
            { TechType.EyesPlant, TechType.EyesPlantSeed },
            { TechType.EyesPlantSeed, TechType.EyesPlantSeed },
            { TechType.MembrainTree, TechType.MembrainTreeSeed },
            { TechType.MembrainTreeSeed, TechType.MembrainTreeSeed },
            { TechType.RedConePlant, TechType.RedConePlantSeed },
            { TechType.RedConePlantSeed, TechType.RedConePlantSeed },
            { TechType.RedBasketPlant, TechType.RedBasketPlantSeed },
            { TechType.RedBasketPlantSeed, TechType.RedBasketPlantSeed },
            { TechType.SnakeMushroom, TechType.SnakeMushroomSpore },
            { TechType.SnakeMushroomSpore, TechType.SnakeMushroomSpore },
            { TechType.SpikePlant, TechType.SpikePlantSeed },
            { TechType.SpikePlantSeed, TechType.SpikePlantSeed },
            { TechType.Creepvine, TechType.CreepvinePiece },
            { TechType.CreepvinePiece, TechType.CreepvinePiece },
            { TechType.CreepvineSeedCluster, TechType.CreepvineSeedCluster },
            { TechType.BloodVine, TechType.BloodOil },
            { TechType.BloodRoot, TechType.BloodOil },
            { TechType.BloodOil, TechType.BloodOil },
            { TechType.PurpleBrainCoral, TechType.PurpleBrainCoralPiece },
            { TechType.PurpleBrainCoralPiece, TechType.PurpleBrainCoralPiece },
            { TechType.MediumKoosh, TechType.KooshChunk },
            { TechType.HugeKoosh, TechType.KooshChunk },
            { TechType.KooshChunk, TechType.KooshChunk }
        };
    }
}
