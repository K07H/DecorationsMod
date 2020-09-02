using DecorationsMod.Fixers;
using HarmonyLib;
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
    // DEBUG_SEEDS
    // DEBUG_KNIFE
    // DEBUG_LAMP
    // DEBUG_STOOL
    // DEBUG_SEAMOTH_FRAGMENTS
    // DEBUG_CYCLOPS_DOLL

    public class DecorationsMod
    {
        // List of modded items
        public static List<IDecorationItem> DecorationItems = null;

        // Decorations mod entry point
        public static void Patch()
        {
            // 1) INITIALIZE HARMONY
            if (!MyHarmony.Initialize())
                return;

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
            Logger.Log("INFO: Registering decorations fabricator...");
            Fabricator_Decorations decorationsFabricator = new Fabricator_Decorations();
            decorationsFabricator.RegisterDecorationsFabricator(DecorationsMod.DecorationItems);

            // 7) REGISTER FLORA FABRICATOR
            if (ConfigSwitcher.EnableNewFlora)
            {
                Logger.Log("INFO: Registering seeds fabricator...");
                Fabricator_Flora floraFabricator = new Fabricator_Flora();
                floraFabricator.RegisterFloraFabricator(DecorationsMod.DecorationItems);
            }

            // 8) HARMONY PATCHING
            MyHarmony.PatchAll();

            // 9) VARIOUS ENHANCEMENTS
            if (ConfigSwitcher.FixAquariumLighting)
                PrefabsHelper.FixAquariumSkyApplier();
            MyHarmony.FixFCSMods();

            // 10) SETUP IN GAME OPTIONS MENU
            Logger.Log("INFO: Setting up in-game options menu...");
            SMLHelper.V2.Handlers.OptionsPanelHandler.RegisterModOptions(new ConfigOptions("Decorations mod"));
        }

        /// <summary>Registers language strings based on user language.</summary>
        public static void RegisterLanguageStrings()
        {
            // Register tooltips
            foreach (string tooltip in LanguageHelper.Tooltips)
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
                    if ((ConfigSwitcher.EnableNutrientBlock || newItem.TechType != TechType.NutrientBlock) && (ConfigSwitcher.EnableSofas || (newItem.ClassID != "SofaStr1" && newItem.ClassID != "SofaStr2" && newItem.ClassID != "SofaStr3" && newItem.ClassID != "SofaCorner2")))
                    {
                        if (!newItem.IsHabitatBuilder ||
                            (ConfigSwitcher.EnableNewItems && ConfigSwitcher.HabitatBuilderItems.Contains(newItem.ClassID) &&
                                (ConfigSwitcher.EnableDecorativeElectronics || (newItem.ClassID != "DecorativeTechBox" && newItem.ClassID != "DecorativeControlTerminal" && newItem.ClassID != "WorkDeskScreen1" && newItem.ClassID != "WorkDeskScreen2")) &&
                                (ConfigSwitcher.EnableCustomBaseParts || newItem.ClassID != "OutdoorLadder")))
                        {
                            newItem.RegisterItem();
                            result.Add(newItem);
                        }
#if DEBUG_ITEMS_REGISTRATION
                        else
                            Logger.Log("INFO: Skipping registration for item type=[{0}]", newItemType.Name);
#endif
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
    }
}
