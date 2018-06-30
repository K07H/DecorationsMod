using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SMLHelper;
using SMLHelper.Patchers;
using Harmony;
using DecorationsMod.Fixers;

namespace DecorationsMod
{
    public class DecorationsMod
    {
        // Harmony stuff
        internal static HarmonyInstance HarmonyInstance = null;
        internal static Dictionary<TechType, CraftData.BackgroundType> CustomBackgroundTypes = new Dictionary<TechType, CraftData.BackgroundType>(TechTypeExtensions.sTechTypeComparer);
        internal static Dictionary<TechType, float> CustomCharges = new Dictionary<TechType, float>(TechTypeExtensions.sTechTypeComparer);
        internal static Dictionary<TechType, int> CustomFinalCutBonusList = new Dictionary<TechType, int>(TechTypeExtensions.sTechTypeComparer);

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
            CustomFabricator.RegisterDecorationsFabricator(decorationItems);

            // 6) REGISTER FLORA FABRICATOR
            CustomFabricator.RegisterFloraFabricator(decorationItems);
            
            // 7) HARMONY PATCHING
            Logger.Log("Patching with Harmony...");
            // Patch dictionaries
            Utility.PatchDictionary(typeof(CraftData), "backgroundTypes", CustomBackgroundTypes);
            Utility.PatchDictionary(typeof(CraftData), "harvestFinalCutBonusList", CustomFinalCutBonusList);
            Utility.PatchDictionary(typeof(BaseBioReactor), "charge", CustomCharges);
            // Give salt when purple pinecone is cut
            var giveResourceOnDamageMethod = typeof(Knife).GetMethod("GiveResourceOnDamage", BindingFlags.NonPublic | BindingFlags.Instance);
            var giveResourceOnDamagePostfix = typeof(KnifeFixer).GetMethod("GiveResourceOnDamage_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(giveResourceOnDamageMethod, null, new HarmonyMethod(giveResourceOnDamagePostfix));
            // Make plants undropable
            //public static bool CanDropItemHere(Pickupable item, bool notify = false)
            var canDropItemHereMethod = typeof(Inventory).GetMethod("CanDropItemHere", BindingFlags.Public | BindingFlags.Static);
            var canDropItemHerePrefix = typeof(InventoryFixer).GetMethod("CanDropItemHere_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(canDropItemHereMethod, new HarmonyMethod(canDropItemHerePrefix), null);
            // Change custom plants tooltips
            var onHandHoverMethod = typeof(GrownPlant).GetMethod("OnHandHover", BindingFlags.Public | BindingFlags.Instance);
            var onHandHoverPostfix = typeof(GrownPlantFixer).GetMethod("OnHandHover_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(onHandHoverMethod, null, new HarmonyMethod(onHandHoverPostfix));
            // Fix DisableColliders method
            /* var disableCollidersMethod = typeof(Pickupable).GetMethod("DisableColliders", BindingFlags.NonPublic | BindingFlags.Instance);
            var prefix = typeof(LayerFixer).GetMethod("DisableColliders_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(disableCollidersMethod, new HarmonyMethod(prefix), null); */
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
                KnownTechPatcher.unlockedAtStart.Add(existingItem.TechType);
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

            // Register lamp tooltip
            LanguagePatcher.customLines.Add("ToggleLamp", LanguageHelper.GetFriendlyWord("LampTooltip"));
            // Register seamoth doll tooltip
            LanguagePatcher.customLines.Add("SwitchSeamothModel", LanguageHelper.GetFriendlyWord("SwitchSeamothModel"));
            // Register exosuit doll tooltip
            LanguagePatcher.customLines.Add("SwitchExosuitModel", LanguageHelper.GetFriendlyWord("SwitchExosuitModel"));
            // Register cargo boxes tooltip
            LanguagePatcher.customLines.Add("AdjustCargoBoxSize", LanguageHelper.GetFriendlyWord("AdjustItemSize"));
            // Register forklift tooltip
            LanguagePatcher.customLines.Add("AdjustForkliftSize", LanguageHelper.GetFriendlyWord("AdjustItemSize"));
            // Register cove tree tooltip
            LanguagePatcher.customLines.Add("DisplayCoveTreeEggs", LanguageHelper.GetFriendlyWord("DisplayCoveTreeEggs"));

            return result;
        }
    }
}
