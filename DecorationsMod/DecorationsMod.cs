using DecorationsMod.Fixers;
using Harmony;
using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DecorationsMod
{
    // Available DEBUG flags:
    // DEBUG_AQUARIUM
    // DEBUG_CARGO_CRATES
    // DEBUG_COVE_TREE
    // DEBUG_CUSTOM_PICTURE_FRAME
    // DEBUG_DROP_ITEM
    // DEBUG_FLORA
    // DEBUG_FLORA_ANIMATION
    // DEBUG_FLORA_ENTRY
    // DEBUG_GROWNPLANT_FIXER
    // DEBUG_KNIFE
    // DEBUG_LAMP
    // DEBUG_STOOL

    public class DecorationsMod
    {
        // Harmony stuff
        internal static HarmonyInstance HarmonyInstance = null;

        public static List<IDecorationItem> DecorationItems = null;

        public static void Patch()
        {
            // 1) INITIALIZE HARMONY
            HarmonyInstance = HarmonyInstance.Create("com.osubmarin.decorationsmod");

            // 2) LOAD CONFIGURATION
            ConfigSwitcher.LoadConfiguration();

            // 3) REGISTER DECORATION ITEMS
            DecorationsMod.DecorationItems = RegisterDecorationItems();
            
            // 4) MAKE SOME EXISTING ITEMS PICKUPABLE & POSITIONABLE
            if (ConfigSwitcher.EnablePlaceItems)
                PlaceToolItems.MakeItemsPlaceable();
            
            // 5) REGISTER DECORATIONS FABRICATOR
            Fabricator_Decorations decorationsFabricator = new Fabricator_Decorations();
            decorationsFabricator.RegisterDecorationsFabricator(DecorationsMod.DecorationItems); //, rootNode, craftType);

            // 6) REGISTER FLORA FABRICATOR
            Fabricator_Flora floraFabricator = new Fabricator_Flora();
            floraFabricator.RegisterFloraFabricator(DecorationsMod.DecorationItems);

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
            // Fix aquarium sky appliers
            var addItemMethod = typeof(Aquarium).GetMethod("AddItem", BindingFlags.NonPublic | BindingFlags.Instance);
            var addItemPostfix = typeof(AquariumFixer).GetMethod("AddItem_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(addItemMethod, null, new HarmonyMethod(addItemPostfix));

            // 8) ENHANCEMENTS
            if (ConfigSwitcher.FixAquariumLighting)
                PrefabsHelper.FixAquariumSkyApplier();
        }

        private static void RegisterRecipeForTechType(TechType techType, TechType resource, int resourceAmount = 1, int craftingAmount = 1)
        {
            // Associate recipe to the new TechType
#if BELOWZERO
            SMLHelper.V2.Crafting.RecipeData techTypeRecipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = craftingAmount,
                Ingredients = new List<Ingredient>(new Ingredient[1]
                    {
                        new Ingredient(resource, resourceAmount)
                    }),
            };
            SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(techType, techTypeRecipe);
#else
            var techTypeRecipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = craftingAmount,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[1] {
                    new SMLHelper.V2.Crafting.Ingredient(resource, resourceAmount)
                })
            };
            SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(techType, techTypeRecipe);
#endif
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
                Logger.Log("DEBUG: Trying to create item type=[{0}]", existingItemType.Name);
                // Get item
                DecorationItem existingItem = (DecorationItem)(Activator.CreateInstance(existingItemType));
                if (existingItem.GameObject == null)
                    Logger.Log("WARNING: Unable to create item type=[{0}]!", existingItemType.Name);
                else
                {
                    // Register item
                    existingItem.RegisterItem();
                    // Unlock item at game start
                    SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(existingItem.TechType);
                    // Store item in the list
                    result.Add(existingItem);
                }
            }

            // Get the list of new items
            var newItems = from t in Assembly.GetExecutingAssembly().GetTypes() 
                           where t.IsClass && t.Namespace == "DecorationsMod.NewItems" 
                           select t;

            // Register new items
            foreach (Type newItemType in newItems)
            {
                Logger.Log("DEBUG: Trying to create new item type=[{0}] baseType=[{1}] declaringType=[{2}] qualifiedName=[{3}]", newItemType?.Name, newItemType?.BaseType?.Name, newItemType?.DeclaringType?.Name, newItemType?.AssemblyQualifiedName);
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));
                if (newItem.GameObject == null)
                    Logger.Log("WARNING: Unable create new item type=[{0}]!", newItemType.Name);
                else
                {
                    // If current item is not a nutrient block continue, otherwise if that is a nutrient block
                    // we continue only if nutrient blocks are enabled in Config.txt file.
                    if (newItem.TechType != TechType.NutrientBlock || ConfigSwitcher.EnableNutrientBlock) // && ((newItem.TechType != TechType.PrecursorKey_Purple && newItem.TechType != TechType.PrecursorKey_Orange && newItem.TechType != TechType.PrecursorKey_Blue) || ConfigSwitcher.PrecursorKeysAll))
                    {
                        // If decoration items from habitat builder are enabled, add everything.
                        // Otherwise add only items that are not from habitat builder.
                        if (ConfigSwitcher.EnableSpecialItems || !newItem.IsHabitatBuilder)
                        {
                            newItem.RegisterItem();
                            result.Add(newItem);
                        }
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
                Logger.Log("DEBUG: Trying to create flora item type=[{0}]", newItemType.Name);
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));

                if (newItem.GameObject == null)
                    Logger.Log("WARNING: Unable create flora item type=[{0}]!", newItemType.Name);
                else
                {
                    newItem.RegisterItem();
                    result.Add(newItem);
                }
            }

            // Get the list of new water flora
            var newWaterFlora = from t in Assembly.GetExecutingAssembly().GetTypes()
                           where t.IsClass && t.Namespace == "DecorationsMod.FloraAquatic"
                           select t;

            // Register new water flora
            foreach (Type newItemType in newWaterFlora)
            {
                Logger.Log("DEBUG: Trying to create water flora item type=[{0}]", newItemType.Name);
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));

                if (newItem.GameObject == null)
                    Logger.Log("WARNING: Unable create water flora item type=[{0}]!", newItemType.Name);
                else
                {
                    newItem.RegisterItem();
                    result.Add(newItem);
                }
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

                RegisterRecipeForTechType(TechType.GabeSFeatherSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.RedGreenTentacleSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.SeaCrownSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.ShellGrassSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.PurpleBranchesSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.RedRollPlantSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.RedBushSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.PurpleStalkSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.SpottedLeavesPlantSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.AcidMushroomSpore, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.WhiteMushroomSpore, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.JellyPlantSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.SmallFanSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.PurpleFanSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.PurpleTentacleSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.BluePalmSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.EyesPlantSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.MembrainTreeSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.RedConePlantSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.RedBasketPlantSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.SnakeMushroomSpore, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.SpikePlantSeed, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.CreepvinePiece, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.CreepvineSeedCluster, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.BloodOil, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.PurpleBrainCoralPiece, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                RegisterRecipeForTechType(TechType.KooshChunk, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
            }

            // Register lamp tooltips
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("LampTooltip", LanguageHelper.GetFriendlyWord("LampTooltip"));
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("LampTooltipCompact", LanguageHelper.GetFriendlyWord("LampTooltipCompact"));

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
            
            // Register custom picture frame tooltips
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("CustomPictureFrameTooltip", LanguageHelper.GetFriendlyWord("CustomPictureFrameTooltip"));
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("CustomPictureFrameTooltipCompact", LanguageHelper.GetFriendlyWord("CustomPictureFrameTooltipCompact"));
            
            // Register custom lockers tooltip
            SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine("OpenCustomStorage", LanguageHelper.GetFriendlyWord("OpenCustomStorage"));

            return result;
        }
    }
}
