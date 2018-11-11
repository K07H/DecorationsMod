using DecorationsMod.Fixers;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DecorationsMod
{
    public class DecorationsMod
    {
        // Harmony stuff
        internal static HarmonyInstance HarmonyInstance = null;

        public static void Patch()
        {
            // 1) INITIALIZE HARMONY
            HarmonyInstance = HarmonyInstance.Create("com.osubmarin.decorationsmod");

            // 2) LOAD CONFIGURATION
            ConfigSwitcher.LoadConfiguration();

            // 3) REGISTER DECORATION ITEMS
            List<IDecorationItem> decorationItems = RegisterDecorationItems();

            // 4) MAKE SOME EXISTING ITEMS PICKUPABLE & POSITIONABLE
            if (ConfigSwitcher.EnablePlaceItems)
                PlaceToolItems.MakeItemsPlaceable();
            
            // 5) REGISTER DECORATIONS FABRICATOR
            Fabricator_Decorations decorationsFabricator = new Fabricator_Decorations();
            decorationsFabricator.RegisterDecorationsFabricator(decorationItems);

            // 6) REGISTER FLORA FABRICATOR
            Fabricator_Flora floraFabricator = new Fabricator_Flora();
            floraFabricator.RegisterFloraFabricator(decorationItems);

            // 7) HARMONY PATCHING
            Logger.Log("Patching with Harmony...");
            // Give salt when purple pinecone is cut
            var giveResourceOnDamageMethod = typeof(Knife).GetMethod("GiveResourceOnDamage", BindingFlags.NonPublic | BindingFlags.Instance);
            var giveResourceOnDamagePostfix = typeof(KnifeFixer).GetMethod("GiveResourceOnDamage_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(giveResourceOnDamageMethod, null, new HarmonyMethod(giveResourceOnDamagePostfix));
            // Make plants undropable
            var canDropItemHereMethod = typeof(Inventory).GetMethod("CanDropItemHere", BindingFlags.Public | BindingFlags.Static);
            var canDropItemHerePrefix = typeof(InventoryFixer).GetMethod("CanDropItemHere_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(canDropItemHereMethod, new HarmonyMethod(canDropItemHerePrefix), null);
            // Change custom plants tooltips
            var onHandHoverMethod = typeof(GrownPlant).GetMethod("OnHandHover", BindingFlags.Public | BindingFlags.Instance);
            var onHandHoverPostfix = typeof(GrownPlantFixer).GetMethod("OnHandHover_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(onHandHoverMethod, null, new HarmonyMethod(onHandHoverPostfix));
            // Fix cargo crates items-containers
            var onProtoDeserializeObjectTreeMethod = typeof(StorageContainer).GetMethod("OnProtoDeserializeObjectTree", BindingFlags.Public | BindingFlags.Instance);
            var onProtoDeserializeObjectTreePostfix = typeof(StorageContainerFixer).GetMethod("OnProtoDeserializeObjectTree_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(onProtoDeserializeObjectTreeMethod, null, new HarmonyMethod(onProtoDeserializeObjectTreePostfix));
            // Failsafe on lockers and cargo crates deconstruction
            var canDeconstructMethod = typeof(Constructable).GetMethod("CanDeconstruct", BindingFlags.Public | BindingFlags.Instance);
            var canDeconstructPrefix = typeof(ConstructableFixer).GetMethod("CanDeconstruct_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(canDeconstructMethod, new HarmonyMethod(canDeconstructPrefix), null);
            // Fix equipment types for batteries, power cells, and their ion versions
            if (ConfigSwitcher.EnablePlaceBatteries)
            {
                var allowedToAddMethod = typeof(Equipment).GetMethod("AllowedToAdd", BindingFlags.Public | BindingFlags.Instance);
                var allowedToAddPrefix = typeof(EquipmentFixer).GetMethod("AllowedToAdd_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(allowedToAddMethod, new HarmonyMethod(allowedToAddPrefix), null);
                var addOrSwapMethod = typeof(Inventory).GetMethod("AddOrSwap", new Type[] { typeof(InventoryItem), typeof(Equipment), typeof(string) }); //, BindingFlags.Public | BindingFlags.Static);
                var addOrSwapPrefix = typeof(InventoryFixer).GetMethod("AddOrSwap_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(addOrSwapMethod, new HarmonyMethod(addOrSwapPrefix), null);
                var canSwitchOrSwapMethod = typeof(uGUI_Equipment).GetMethod("CanSwitchOrSwap", BindingFlags.Public | BindingFlags.Instance);
                var canSwitchOrSwapPrefix = typeof(uGUI_EquipmentFixer).GetMethod("CanSwitchOrSwap_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(canSwitchOrSwapMethod, new HarmonyMethod(canSwitchOrSwapPrefix), null);
            }
        }

        private static void RegisterRecipeForTechType(TechType techType, TechType resource, int resourceAmount = 1, int craftingAmount = 1)
        {
            // Associate recipe to the new TechType
            var techTypeRecipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = craftingAmount,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1] {
                    new SMLHelper.V2.Crafting.Ingredient(resource, resourceAmount)
                })
            };
            SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(techType, techTypeRecipe);
        }

        private static List<IDecorationItem> RegisterDecorationItems()
        {
            List<IDecorationItem> result = new List<IDecorationItem>();

            Logger.Log("Registering items...");

            // Get the list of modified existing items
            var existingItems = from t in Assembly.GetExecutingAssembly().GetTypes() 
                                where t.IsClass && t.Namespace == "DecorationsMod.ExistingItems" 
                                select t;
            // Register modified existing items
            foreach (Type existingItemType in existingItems)
            {
                // Get item
                DecorationItem existingItem = (DecorationItem)(Activator.CreateInstance(existingItemType));
                // Register item
                existingItem.RegisterItem();
                // Unlock item at game start
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(existingItem.TechType);
                // Store item in the list
                result.Add(existingItem);
            }

            // Get the list of new items
            var newItems = from t in Assembly.GetExecutingAssembly().GetTypes() 
                           where t.IsClass && t.Namespace == "DecorationsMod.NewItems" 
                           select t;
            // Register new items
            foreach (Type newItemType in newItems)
            {
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));

                // If current item is not a nutrient block continue, otherwise if that is a nutrient block
                // we continue only if nutrient blocks are enabled in Config.txt file.
                if (newItem.TechType != TechType.NutrientBlock || (newItem.TechType == TechType.NutrientBlock && ConfigSwitcher.EnableNutrientBlock))
                {
                    // If decoration items from habitat builder are enabled, add everything.
                    // Otherwise add only items that are not from habitat builder.
                    if (ConfigSwitcher.EnableSpecialItems || (!ConfigSwitcher.EnableSpecialItems && !newItem.IsHabitatBuilder))
                    {
                        newItem.RegisterItem();
                        result.Add(newItem);
                    }
                }
            }

            // Get the list of new land flora
            var newFlora = from t in Assembly.GetExecutingAssembly().GetTypes()
                           where t.IsClass && t.Namespace == "DecorationsMod.Flora"
                           select t;
            // Register new land flora
            foreach (Type newItemType in newFlora)
            {
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));
                
                newItem.RegisterItem();
                result.Add(newItem);
            }

            // Get the list of new water flora
            var newWaterFlora = from t in Assembly.GetExecutingAssembly().GetTypes()
                           where t.IsClass && t.Namespace == "DecorationsMod.FloraAquatic"
                           select t;
            // Register new water flora
            foreach (Type newItemType in newWaterFlora)
            {
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));

                newItem.RegisterItem();
                result.Add(newItem);
            }
            
            // Register existing air seeds recipes
            if (ConfigSwitcher.EnableRegularAirSeeds)
            {
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.BulboTreePiece);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleVegetable);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.HangingFruit);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.MelonSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.FernPalmSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.OrangePetalsPlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleVasePlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.OrangeMushroomSpore);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PinkMushroomSpore);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleRattleSpore);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PinkFlowerSeed);

                RegisterRecipeForTechType(TechType.BulboTreePiece, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleVegetable, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.HangingFruit, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.MelonSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.FernPalmSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.OrangePetalsPlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleVasePlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.OrangeMushroomSpore, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PinkMushroomSpore, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleRattleSpore, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PinkFlowerSeed, ConfigSwitcher.FloraRecipiesResource);
            }
            
            // Register existing water seeds recipes
            if (ConfigSwitcher.EnableRegularWaterSeeds)
            {
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.GabeSFeatherSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.RedGreenTentacleSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.SeaCrownSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.ShellGrassSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleBranchesSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.RedRollPlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.RedBushSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleStalkSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.SpottedLeavesPlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.AcidMushroomSpore);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.WhiteMushroomSpore);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.JellyPlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.SmallFanSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleFanSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleTentacleSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.BluePalmSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.EyesPlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.MembrainTreeSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.RedConePlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.RedBasketPlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.SnakeMushroomSpore);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.SpikePlantSeed);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.CreepvinePiece);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.CreepvineSeedCluster);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.BloodOil);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.PurpleBrainCoralPiece);
                SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(TechType.KooshChunk);

                RegisterRecipeForTechType(TechType.GabeSFeatherSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.RedGreenTentacleSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.SeaCrownSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.ShellGrassSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleBranchesSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.RedRollPlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.RedBushSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleStalkSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.SpottedLeavesPlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.AcidMushroomSpore, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.WhiteMushroomSpore, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.JellyPlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.SmallFanSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleFanSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleTentacleSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.BluePalmSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.EyesPlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.MembrainTreeSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.RedConePlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.RedBasketPlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.SnakeMushroomSpore, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.SpikePlantSeed, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.CreepvinePiece, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.CreepvineSeedCluster, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.BloodOil, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.PurpleBrainCoralPiece, ConfigSwitcher.FloraRecipiesResource);
                RegisterRecipeForTechType(TechType.KooshChunk, ConfigSwitcher.FloraRecipiesResource);
            }

            // Register lamp tooltip
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("ToggleLamp", LanguageHelper.GetFriendlyWord("LampTooltip"));
            // Register seamoth doll tooltip
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("SwitchSeamothModel", LanguageHelper.GetFriendlyWord("SwitchSeamothModel"));
            // Register exosuit doll tooltip
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("SwitchExosuitModel", LanguageHelper.GetFriendlyWord("SwitchExosuitModel"));
            // Register cargo boxes tooltip
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("AdjustCargoBoxSize", LanguageHelper.GetFriendlyWord("AdjustCargoBoxSize"));
            // Register forklift tooltip
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("AdjustForkliftSize", LanguageHelper.GetFriendlyWord("AdjustForkliftSize"));
            // Register cove tree tooltip
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("DisplayCoveTreeEggs", LanguageHelper.GetFriendlyWord("DisplayCoveTreeEggs"));

            return result;
        }
    }
}
