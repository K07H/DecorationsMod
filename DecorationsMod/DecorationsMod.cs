using DecorationsMod.Fixers;
using Harmony;
using QModManager.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod
{
    // List of DEBUG flags:
    // DEBUG_ITEMS_REGISTRATION
    // DEBUG_HARMONY_PATCHING
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
    // DEBUG_SEAMOTH_FRAGMENTS

    public class DecorationsMod
    {
        // Harmony stuff
        internal static HarmonyInstance HarmonyInstance = null;

        // List of modded items
        public static List<IDecorationItem> DecorationItems = null;

        private static void FixFCSMods()
        {
            IQMod modFCS1 = QModServices.Main.FindModById("FCSAIPowerCellSocket");
            if (modFCS1 != null && modFCS1.ParsedVersion != null && modFCS1.ParsedVersion.ToString() == "1.2.3" && modFCS1.LoadedAssembly != null)
            {
                Logger.Log("INFO: Alterra Industrial Powercell Socket mod version 1.2.3 detected: Applying fix...");
                Type[] types = modFCS1.LoadedAssembly.GetTypes();
                if (types != null)
                    foreach (Type t in types)
                        if (t.Name != null && t.Name == "AIPowerCellSocketPowerManager")
                        {
                            MethodInfo isAllowedToAddMethod = t.GetMethod("IsAllowedToAdd", BindingFlags.NonPublic | BindingFlags.Instance);
                            MethodInfo isAllowedToAddPrefix = typeof(FCSModsFixer).GetMethod("IsAllowedToAdd_Powercell_Prefix", BindingFlags.Public | BindingFlags.Static);
                            HarmonyInstance.Patch(isAllowedToAddMethod, new HarmonyMethod(isAllowedToAddPrefix), null);
                            Logger.Log("INFO: Alterra Industrial Powercell Socket mod successfully fixed.");
                            break;
                        }
            }
            IQMod modFCS2 = QModServices.Main.FindModById("FCSDeepDriller");
            if (modFCS2 != null && modFCS2.ParsedVersion != null && modFCS2.ParsedVersion.ToString() == "1.2.5" && modFCS2.LoadedAssembly != null)
            {
                Logger.Log("INFO: Alterra Industrial Deep Driller mod version 1.2.5 detected: Applying fix...");
                Type[] types = modFCS2.LoadedAssembly.GetTypes();
                if (types != null)
                    foreach (Type t in types)
                        if (t.Name != null && t.Name == "FCSDeepDrillerBatteryController")
                        {
                            MethodInfo isAllowedToAddMethod = t.GetMethod("IsAllowedToAdd", BindingFlags.NonPublic | BindingFlags.Instance);
                            MethodInfo isAllowedToAddPrefix = typeof(FCSModsFixer).GetMethod("IsAllowedToAdd_Driller_Prefix", BindingFlags.Public | BindingFlags.Static);
                            HarmonyInstance.Patch(isAllowedToAddMethod, new HarmonyMethod(isAllowedToAddPrefix), null);
                            Logger.Log("INFO: Alterra Industrial Deep Driller mod successfully fixed.");
                            break;
                        }
            }
        }

        // Decorations mod entry point
        public static void Patch()
        {
            // 1) INITIALIZE HARMONY
            if ((HarmonyInstance = HarmonyInstance.Create("com.osubmarin.decorationsmod")) == null)
            {
                Logger.Log("ERROR: Unable to initialize Harmony!");
                return;
            }

            // 2) LOAD CONFIGURATION
            ConfigSwitcher.LoadConfiguration();

            // 3) REGISTER NEW ITEMS
            DecorationsMod.DecorationItems = RegisterNewItems();

            // 4) REGISTER LANGUAGE STRINGS
            RegisterLanguageStrings();

            // 5) MAKE SOME EXISTING ITEMS PICKUPABLE & PLACEABLE
            if (ConfigSwitcher.EnablePlaceItems)
                PlaceToolItems.MakeItemsPlaceable();

            // 6) REGISTER DECORATIONS FABRICATOR
            Fabricator_Decorations decorationsFabricator = new Fabricator_Decorations();
            decorationsFabricator.RegisterDecorationsFabricator(DecorationsMod.DecorationItems);

            // 7) REGISTER FLORA FABRICATOR
            if (ConfigSwitcher.EnableNewFlora)
            {
                Fabricator_Flora floraFabricator = new Fabricator_Flora();
                floraFabricator.RegisterFloraFabricator(DecorationsMod.DecorationItems);
            }

            // 8) HARMONY PATCHING
            HarmonyPatchAll();

            // 9) ENHANCEMENTS
            if (ConfigSwitcher.FixAquariumLighting)
                PrefabsHelper.FixAquariumSkyApplier();
            FixFCSMods();

            // 10) SETUP IN GAME OPTIONS MENU
            Logger.Log("INFO: Setting up in-game options menu...");
            SMLHelper.V2.Handlers.OptionsPanelHandler.RegisterModOptions(new ConfigOptions("Decorations mod"));
        }

        /// <summary>Patches Subnautica DLL with Harmony.</summary>
        private static void HarmonyPatchAll()
        {
            Logger.Log("INFO: Patching with Harmony...");
            // Fix cargo crates items-containers
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Fixing cargo crates item containers...");
#endif
            var onProtoDeserializeObjectTreeMethod = typeof(StorageContainer).GetMethod("OnProtoDeserializeObjectTree", BindingFlags.Public | BindingFlags.Instance);
            var onProtoDeserializeObjectTreePostfix = typeof(StorageContainerFixer).GetMethod("OnProtoDeserializeObjectTree_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(onProtoDeserializeObjectTreeMethod, null, new HarmonyMethod(onProtoDeserializeObjectTreePostfix));
            // Failsafe on lockers and cargo crates deconstruction
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Adding failsafe on lockers and cargo crates deconstruction...");
#endif
            var canDeconstructMethod = typeof(Constructable).GetMethod("CanDeconstruct", BindingFlags.Public | BindingFlags.Instance);
            var canDeconstructPrefix = typeof(ConstructableFixer).GetMethod("CanDeconstruct_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(canDeconstructMethod, new HarmonyMethod(canDeconstructPrefix), null);
            // Fix equipment types for batteries, power cells, and their ion versions
            if (ConfigSwitcher.EnablePlaceBatteries)
                PatchBatteries();
            // Fix aquarium sky appliers
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Fixing aquarium sky appliers...");
#endif
            var addItemMethod = typeof(Aquarium).GetMethod("AddItem", BindingFlags.NonPublic | BindingFlags.Instance);
            var addItemPostfix = typeof(AquariumFixer).GetMethod("AddItem_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(addItemMethod, null, new HarmonyMethod(addItemPostfix));
            // Fix alternative use for placeable items
            if (ConfigSwitcher.EnablePlaceItems)
            {
                var getAltUseItemActionMethod = typeof(Inventory).GetMethod("GetAltUseItemAction", BindingFlags.Public | BindingFlags.Instance);
                var getAltUseItemActionPostfix = typeof(InventoryFixer).GetMethod("GetAltUseItemAction_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(getAltUseItemActionMethod, null, new HarmonyMethod(getAltUseItemActionPostfix));
            }
            // Setup new items unlock conditions
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Setting up new items unlock conditions...");
#endif
            var isCraftRecipeUnlockedMethod = typeof(CrafterLogic).GetMethod("IsCraftRecipeUnlocked", BindingFlags.Public | BindingFlags.Static);
            var isCraftRecipeUnlockedPostfix = typeof(CrafterLogicFixer).GetMethod("IsCraftRecipeUnlocked_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(isCraftRecipeUnlockedMethod, null, new HarmonyMethod(isCraftRecipeUnlockedPostfix));
            var getTechUnlockStateMethod = typeof(KnownTech).GetMethod("GetTechUnlockState", new Type[] { typeof(TechType), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }); //, BindingFlags.Public | BindingFlags.Static);
            var getTechUnlockStatePrefix = typeof(KnownTechFixer).GetMethod("GetTechUnlockState_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(getTechUnlockStateMethod, new HarmonyMethod(getTechUnlockStatePrefix), null);
            // Load added "new item" notifications when game loads
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Setting up loading of added \"new item\" notifications...");
#endif
            var loadMostRecentSavedGameMethod = typeof(uGUI_MainMenu).GetMethod("LoadMostRecentSavedGame", BindingFlags.NonPublic | BindingFlags.Instance);
            var loadMostRecentSavedGamePrefix = typeof(uGUI_MainMenuFixer).GetMethod("LoadMostRecentSavedGame_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(loadMostRecentSavedGameMethod, new HarmonyMethod(loadMostRecentSavedGamePrefix), null);
            var onErrorConfirmedMethod = typeof(uGUI_MainMenu).GetMethod("OnErrorConfirmed", BindingFlags.NonPublic | BindingFlags.Instance);
            var onErrorConfirmedPrefix = typeof(uGUI_MainMenuFixer).GetMethod("OnErrorConfirmed_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(onErrorConfirmedMethod, new HarmonyMethod(onErrorConfirmedPrefix), null);
            var loadMethod = typeof(MainMenuLoadButton).GetMethod("Load", BindingFlags.Public | BindingFlags.Instance);
            var loadPrefix = typeof(MainMenuLoadButtonFixer).GetMethod("Load_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(loadMethod, new HarmonyMethod(loadPrefix), null);
            // Save added "new item" notifications when game saves
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Setting up saving of added \"new item\" notifications...");
#endif
            var saveGameMethod = typeof(IngameMenu).GetMethod("SaveGame", BindingFlags.Public | BindingFlags.Instance);
            var saveGamePostfix = typeof(IngameMenuFixer).GetMethod("SaveGame_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(saveGameMethod, null, new HarmonyMethod(saveGamePostfix));
            if (ConfigSwitcher.EnableNewFlora)
            {
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Setting up purple pinecone harvested material...");
#endif
                // Give salt when purple pinecone is cut
                var giveResourceOnDamageMethod = typeof(Knife).GetMethod("GiveResourceOnDamage", BindingFlags.NonPublic | BindingFlags.Instance);
                var giveResourceOnDamagePostfix = typeof(KnifeFixer).GetMethod("GiveResourceOnDamage_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(giveResourceOnDamageMethod, null, new HarmonyMethod(giveResourceOnDamagePostfix));
                // Change custom plants tooltips
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Setting up plant tooltips...");
#endif
                var onHandHoverMethod = typeof(GrownPlant).GetMethod("OnHandHover", BindingFlags.Public | BindingFlags.Instance);
                var onHandHoverPostfix = typeof(GrownPlantFixer).GetMethod("OnHandHover_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onHandHoverMethod, null, new HarmonyMethod(onHandHoverPostfix));
                // Make new flora spawn as seeds when using console commands (instead of grown plants)
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Making plants spawning as seeds...");
#endif
                var onConsoleCommand_itemMethod = typeof(InventoryConsoleCommands).GetMethod("OnConsoleCommand_item", BindingFlags.NonPublic | BindingFlags.Instance);
                var onConsoleCommand_itemPrefix = typeof(InventoryConsoleCommandsFixer).GetMethod("OnConsoleCommand_item_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onConsoleCommand_itemMethod, new HarmonyMethod(onConsoleCommand_itemPrefix), null);
                var onConsoleCommand_spawnMethod = typeof(SpawnConsoleCommand).GetMethod("OnConsoleCommand_spawn", BindingFlags.NonPublic | BindingFlags.Instance);
                var onConsoleCommand_spawnPrefix = typeof(SpawnConsoleCommandFixer).GetMethod("OnConsoleCommand_spawn_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onConsoleCommand_spawnMethod, new HarmonyMethod(onConsoleCommand_spawnPrefix), null);
                // Handles pland/seed state upon drop and pickup
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Setting up plants interactions...");
#endif
#if SUBNAUTICA
                var dropMethod = typeof(Pickupable).GetMethod("Drop", new Type[] { typeof(Vector3), typeof(Vector3), typeof(bool) });
#else
                var dropMethod = typeof(Pickupable).GetMethod("Drop", new Type[] { typeof(Vector3), typeof(Vector3) });
#endif
                var dropPostfix = typeof(PickupableFixer).GetMethod("Drop_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(dropMethod, null, new HarmonyMethod(dropPostfix));
                var onHandClickMethod = typeof(Pickupable).GetMethod("OnHandClick", BindingFlags.Public | BindingFlags.Instance);
                var onHandClickPrefix = typeof(PickupableFixer).GetMethod("OnHandClick_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onHandClickMethod, new HarmonyMethod(onHandClickPrefix), null);
            }
        }

        private static bool _patchedBatteries = false;
        /// <summary>This will make batteries and powercells placeable (uses Harmony).</summary>
        public static void PatchBatteries()
        {
            if (!_patchedBatteries)
            {
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Making batteries and powercells placeable...");
#endif
                var allowedToAddMethod = typeof(Equipment).GetMethod("AllowedToAdd", BindingFlags.Public | BindingFlags.Instance);
                var allowedToAddPrefix = typeof(EquipmentFixer).GetMethod("AllowedToAdd_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(allowedToAddMethod, new HarmonyMethod(allowedToAddPrefix), null);
                var addOrSwapMethod = typeof(Inventory).GetMethod("AddOrSwap", new Type[] { typeof(InventoryItem), typeof(Equipment), typeof(string) });
                var addOrSwapPrefix = typeof(InventoryFixer).GetMethod("AddOrSwap_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(addOrSwapMethod, new HarmonyMethod(addOrSwapPrefix), null);
                var canSwitchOrSwapMethod = typeof(uGUI_Equipment).GetMethod("CanSwitchOrSwap", BindingFlags.Public | BindingFlags.Instance);
                var canSwitchOrSwapPrefix = typeof(uGUI_EquipmentFixer).GetMethod("CanSwitchOrSwap_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(canSwitchOrSwapMethod, new HarmonyMethod(canSwitchOrSwapPrefix), null);

                _patchedBatteries = true;
            }
        }

        /// <summary>Registers language strings based on user language.</summary>
        public static void RegisterLanguageStrings()
        {
            // Register tooltips
            foreach (string tooltip in DecorationsMod.tooltips)
                SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine(tooltip, LanguageHelper.GetFriendlyWord(tooltip));
            // Register configuration strings
            foreach (string configOption in ConfigOptions.LanguageStrings)
                SMLHelper.V2.Handlers.LanguageHandler.SetLanguageLine(configOption, LanguageHelper.GetFriendlyWord(configOption));
        }

        /// <summary>Returns a list containing all new items added by this mod.</summary>
        private static List<IDecorationItem> RegisterNewItems()
        {
            List<IDecorationItem> result = new List<IDecorationItem>();

            Logger.Log("INFO: Registering items...");

            // Get the list of modified existing items
            var existingItems = from t in Assembly.GetExecutingAssembly().GetTypes() 
                                where t.IsClass && t.Namespace == "DecorationsMod.ExistingItems" 
                                select t;

            // Register modified existing items
            foreach (Type existingItemType in existingItems)
            {
#if DEBUG_ITEMS_REGISTRATION
                Logger.Log("INFO: Trying to create item type=[{0}]", existingItemType.Name);
#endif
                // Get item
                DecorationItem existingItem = (DecorationItem)(Activator.CreateInstance(existingItemType));
                if (existingItem.GameObject != null)
                {
                    // Register item
                    existingItem.RegisterItem();
                    // Unlock item at game start
                    SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(existingItem.TechType);
                    // Store item in the list
                    result.Add(existingItem);
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Unable to create existing item type=[{0}]!", existingItemType.Name);
#endif
            }

            // Get the list of new items
            var newItems = from t in Assembly.GetExecutingAssembly().GetTypes() 
                           where t.IsClass && t.Namespace == "DecorationsMod.NewItems" 
                           select t;

            // Register new items
            foreach (Type newItemType in newItems)
            {
#if DEBUG_ITEMS_REGISTRATION
                Logger.Log("INFO: Trying to create new item type=[{0}]", newItemType.Name);
#endif
                DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));
                if (newItem.GameObject != null)
                {
                    // Check if item has been enabled in Config options
                    if ((ConfigSwitcher.EnableNutrientBlock || newItem.TechType != TechType.NutrientBlock) &&
                        (ConfigSwitcher.EnableSofas || (newItem.ClassID != "SofaStr1" && newItem.ClassID != "SofaStr2" && newItem.ClassID != "SofaStr3" && newItem.ClassID != "SofaCorner2")))
                    {
                        // If decoration items from habitat builder are enabled, add everything
                        // Otherwise add only items that are not from habitat builder
                        if (ConfigSwitcher.EnableNewItems || !newItem.IsHabitatBuilder)
                        {
                            newItem.RegisterItem();
                            result.Add(newItem);
                        }
                    }
                }
#if DEBUG_ITEMS_REGISTRATION
                else
                    Logger.Log("WARNING: Unable to create new item type=[{0}]!", newItemType.Name);
#endif
            }

            // Register new flora
            if (ConfigSwitcher.EnableNewFlora)
            {
                // Get the list of new air flora
                var newFlora = from t in Assembly.GetExecutingAssembly().GetTypes()
                               where t.IsClass && t.Namespace == "DecorationsMod.Flora"
                               select t;
                // Register new air flora
                foreach (Type newItemType in newFlora)
                {
#if DEBUG_ITEMS_REGISTRATION
                    Logger.Log("INFO: Trying to create flora item type=[{0}]", newItemType.Name);
#endif
                    DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));
                    if (newItem.GameObject != null)
                    {
                        newItem.RegisterItem();
                        result.Add(newItem);
                    }
#if DEBUG_ITEMS_REGISTRATION
                    else
                        Logger.Log("WARNING: Unable create flora item type=[{0}]!", newItemType.Name);
#endif
                }

                // Get the list of new water flora
                var newWaterFlora = from t in Assembly.GetExecutingAssembly().GetTypes()
                                    where t.IsClass && t.Namespace == "DecorationsMod.FloraAquatic"
                                    select t;
                // Register new water flora
                foreach (Type newItemType in newWaterFlora)
                {
#if DEBUG_ITEMS_REGISTRATION
                    Logger.Log("INFO: Trying to create water flora item type=[{0}]", newItemType.Name);
#endif
                    DecorationItem newItem = (DecorationItem)(Activator.CreateInstance(newItemType));
                    if (newItem.GameObject != null)
                    {
                        newItem.RegisterItem();
                        result.Add(newItem);
                    }
#if DEBUG_ITEMS_REGISTRATION
                    else
                        Logger.Log("WARNING: Unable to create water flora item type=[{0}]!", newItemType.Name);
#endif
                }

                // Register existing air seeds recipes
                if (ConfigSwitcher.EnableRegularAirSeeds)
                {
                    List<TechType> processedSeeds = new List<TechType>();
                    foreach (KeyValuePair<TechType, TechType> airPlant in Fabricator_Flora.AirPlants)
                    {
                        if (!processedSeeds.Contains(airPlant.Value))
                        {
                            RegisterRecipeForTechType(airPlant.Value, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                            processedSeeds.Add(airPlant.Value);
                        }
                        if (ConfigSwitcher.AddAirSeedsWhenDiscovered)
                            SMLHelper.V2.Handlers.KnownTechHandler.SetAnalysisTechEntry(airPlant.Key, new TechType[] { airPlant.Value });
                        else
                            SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(airPlant.Value);
                    }
                }

                // Register existing water seeds recipes
                if (ConfigSwitcher.EnableRegularWaterSeeds)
                {
                    List<TechType> processedSeeds = new List<TechType>();
                    foreach (KeyValuePair<TechType, TechType> waterPlant in Fabricator_Flora.WaterPlants)
                    {
                        if (!processedSeeds.Contains(waterPlant.Value))
                        {
                            RegisterRecipeForTechType(waterPlant.Value, ConfigSwitcher.FloraRecipiesResource, ConfigSwitcher.FloraRecipiesResourceAmount);
                            processedSeeds.Add(waterPlant.Value);
                        }
                        if (ConfigSwitcher.AddWaterSeedsWhenDiscovered)
                            SMLHelper.V2.Handlers.KnownTechHandler.SetAnalysisTechEntry(waterPlant.Key, new TechType[] { waterPlant.Value });
                        else
                            SMLHelper.V2.Handlers.KnownTechHandler.UnlockOnStart(waterPlant.Value);
                    }
                }
            }

            return result;
        }

        /// <summary>Associates given recipe to a TechType.</summary>
        /// <param name="techType">The tech type that needs a recipe.</param>
        /// <param name="resource">The recipe resource.</param>
        /// <param name="resourceAmount">The recipe resource amount.</param>
        /// <param name="craftingAmount">Number of crafted items amount.</param>
        private static void RegisterRecipeForTechType(TechType techType, TechType resource, int resourceAmount = 1, int craftingAmount = 1)
        {
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

        /// <summary>List of tooltip language strings.</summary>
        private static readonly string[] tooltips = new string[11]
        {
            "LampTooltip",
            "LampTooltipCompact",
            "SwitchSeamothModel",
            "SwitchExosuitModel",
            "AdjustCargoBoxSize",
            "AdjustForkliftSize",
            "AdjustWarperSpecimenSize",
            "DisplayCoveTreeEggs",
            "CustomPictureFrameTooltip",
            "CustomPictureFrameTooltipCompact",
            "OpenCustomStorage"
        };
    }
}
