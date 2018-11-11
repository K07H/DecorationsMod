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
        
        internal Fabricator_Flora() : base(FloraFabID, $"{FloraFabID}PreFab")
        {
        }

        public void RegisterFloraFabricator(List<IDecorationItem> decorationItems)
        {
            // Create new Craft Tree Type
            ModCraftTreeRoot rootNode = CreateCustomTree(out CraftTree.Type craftType, decorationItems);
            this.TreeTypeID = craftType;

            // Create a new TechType for new fabricator
            this.TechType = TechTypeHandler.AddTechType(FloraFabID,
                LanguageHelper.GetFriendlyWord("FloraFabricatorName"),
                LanguageHelper.GetFriendlyWord("FloraFabricatorDescription"),
                AssetsHelper.Assets.LoadAsset<Sprite>("fabricator_icon_green"),
                true);

            // Create a Recipie for the new TechType
            var customFabRecipe = new TechData()
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

            // Add the new TechType to the buildables
            CraftDataHandler.AddBuildable(this.TechType);

            // Add the new TechType to the group of Interior Module buildables
            CraftDataHandler.AddToGroup(TechGroup.InteriorModules, TechCategory.InteriorModule, this.TechType);

            LanguageHandler.SetLanguageLine(HandOverText, LanguageHelper.GetFriendlyWord(HandOverText));

            // Set the buildable prefab
            PrefabHandler.RegisterPrefab(this);

            // Associate the recipie to the new TechType
            CraftDataHandler.SetTechData(this.TechType, customFabRecipe);

            // Unlock at start
            KnownTechHandler.UnlockOnStart(this.TechType);
        }

        private ModCraftTreeRoot CreateCustomTree(out CraftTree.Type craftType, List<IDecorationItem> decorationItems)
        {
            ModCraftTreeRoot rootNode = CraftTreeHandler.CreateCustomCraftTreeAndType(FloraFabID, out craftType);

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

        public override GameObject GetGameObject()
        {
            // Instantiate CyclopsFabricator object
            var fabricatorPrefab = GameObject.Instantiate(Resources.Load<GameObject>("Submarine/Build/Fabricator"));

            // Update prefab name
            fabricatorPrefab.name = FloraFabID;

            // Add prefab ID
            PrefabIdentifier prefabId = fabricatorPrefab.AddComponent<PrefabIdentifier>();
            prefabId.ClassId = FloraFabID;
            prefabId.name = LanguageHelper.GetFriendlyWord("FloraFabricatorName");

            // Add tech tag
            TechTag techTag = fabricatorPrefab.AddComponent<TechTag>();
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
            Texture2D coloredTexture = AssetsHelper.Assets.LoadAsset<Texture2D>("submarine_fabricator_green");
            SkinnedMeshRenderer skinnedMeshRenderer = fabricatorPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.mainTexture = coloredTexture;

            return fabricatorPrefab;
        }
    }
}
