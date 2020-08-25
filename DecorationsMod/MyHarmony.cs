using DecorationsMod.Fixers;
using HarmonyLib;
using QModManager.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace DecorationsMod
{
    public static class MyHarmony
    {
        /// <summary>Holds Harmony instance.</summary>
        private static Harmony HarmonyInstance = null;

        /// <summary>Used to initialize Harmony instance. Returns false on failure.</summary>
        public static bool Initialize()
        {
            if ((HarmonyInstance = new Harmony("com.osubmarin.decorationsmod")) == null)
            {
                Logger.Log("ERROR: Unable to initialize Harmony!");
                return false;
            }
            return true;
        }

        /// <summary>Patches Subnautica DLL with Harmony.</summary>
        public static void PatchAll()
        {
            Logger.Log("INFO: Patching with Harmony...");
            // Fix cargo crates items-containers
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Fixing cargo crates item containers...");
#endif
            var onProtoDeserializeObjectTreeMethod = typeof(StorageContainer).GetMethod("OnProtoDeserializeObjectTree", BindingFlags.Public | BindingFlags.Instance);
            var onProtoDeserializeObjectTreePostfix = typeof(StorageContainerFixer).GetMethod("OnProtoDeserializeObjectTree_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(onProtoDeserializeObjectTreeMethod, null, new HarmonyMethod(onProtoDeserializeObjectTreePostfix));
            // Failsafe on lockers and cargo crates deconstruction + custom base parts
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Adding failsafe on lockers and cargo crates deconstruction...");
#endif
            var canDeconstructMethod = typeof(Constructable).GetMethod("CanDeconstruct", BindingFlags.Public | BindingFlags.Instance);
            var canDeconstructPrefix = typeof(ConstructableFixer).GetMethod("CanDeconstruct_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(canDeconstructMethod, new HarmonyMethod(canDeconstructPrefix), null);
            var constructMethod = typeof(Constructable).GetMethod("Construct", BindingFlags.Public | BindingFlags.Instance);
            var constructPostfix = typeof(ConstructableFixer).GetMethod("Construct_Postfix", BindingFlags.Public | BindingFlags.Static);
            var constructPrefix = typeof(ConstructableFixer).GetMethod("Construct_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(constructMethod, new HarmonyMethod(constructPrefix), new HarmonyMethod(constructPostfix));
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
            var removeItemMethod = typeof(Aquarium).GetMethod("RemoveItem", BindingFlags.NonPublic | BindingFlags.Instance);
            var removeItemPrefix = typeof(AquariumFixer).GetMethod("RemoveItem_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(removeItemMethod, new HarmonyMethod(removeItemPrefix), null);
            // Fix alternative use for placeable items
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Fixing alternative use for placeable items...");
#endif
            if (ConfigSwitcher.EnablePlaceItems)
            {
                var getAltUseItemActionMethod = typeof(Inventory).GetMethod("GetAltUseItemAction", BindingFlags.Public | BindingFlags.Instance);
                var getAltUseItemActionPrefix = typeof(InventoryFixer).GetMethod("GetAltUseItemAction_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(getAltUseItemActionMethod, new HarmonyMethod(getAltUseItemActionPrefix), null);
            }
            // Fix colors for custom fabricators icons
#if DEBUG_HARMONY_PATCHING
            Logger.Log("DEBUG: Fixing colors for custom fabricators icons...");
#endif
#if BELOWZERO
            var createIconMethod = typeof(uGUI_CraftingMenu).GetMethod("CreateIcon", BindingFlags.NonPublic | BindingFlags.Instance);
#else
            var createIconMethod = typeof(uGUI_CraftNode).GetMethod("CreateIcon", BindingFlags.NonPublic | BindingFlags.Instance);
#endif
            var createIconPostfix = typeof(uGUI_CraftNodeFixer).GetMethod("CreateIcon_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(createIconMethod, null, new HarmonyMethod(createIconPostfix));
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
            // Handles "Hide Degasi base (500m)" feature
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Adding biome checks...");
#endif
            var calculateBiomeMethod = typeof(Player).GetMethod("CalculateBiome", BindingFlags.NonPublic | BindingFlags.Instance);
            var calculateBiomePostfix = typeof(PlayerFixer).GetMethod("CalculateBiome_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(calculateBiomeMethod, null, new HarmonyMethod(calculateBiomePostfix));
            if (ConfigSwitcher.EnableNewFlora)
            {
                // Give salt when purple pinecone is cut
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Setting up purple pinecone harvested material...");
#endif
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
                var plant_onHandClickMethod = typeof(GrownPlant).GetMethod("OnHandClick", BindingFlags.Public | BindingFlags.Instance);
                var plant_onHandClickPrefix = typeof(GrownPlantFixer).GetMethod("OnHandClick_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(plant_onHandClickMethod, new HarmonyMethod(plant_onHandClickPrefix), null);
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
            if (ConfigSwitcher.EnableCustomBaseParts)
            {
#if DEBUG_HARMONY_PATCHING
                Logger.Log("DEBUG: Setting up custom base parts...");
#endif
                // Handles outdoor ladder positioning
                var setPlaceOnSurfaceMethod = typeof(Builder).GetMethod("SetPlaceOnSurface", BindingFlags.NonPublic | BindingFlags.Static);
                var setPlaceOnSurfacePrefix = typeof(BuilderFixer).GetMethod("SetPlaceOnSurface_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(setPlaceOnSurfaceMethod, new HarmonyMethod(setPlaceOnSurfacePrefix), null);
                // Prevent building outdoor ladder on ground
                var checkSurfaceTypeMethod = typeof(Builder).GetMethod("CheckSurfaceType", BindingFlags.NonPublic | BindingFlags.Static);
                var checkSurfaceTypePostfix = typeof(BuilderFixer).GetMethod("CheckSurfaceType_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(checkSurfaceTypeMethod, null, new HarmonyMethod(checkSurfaceTypePostfix));
            }
        }

        private static bool _patchedBatteries = false;
        /// <summary>This will make batteries and powercells placeable.</summary>
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

        private static bool _patchedPictureFrame = false;
        /// <summary>This will enable advanced features for our customizable picture frame.</summary>
        public static void PatchPictureFrames()
        {
            if (!_patchedPictureFrame)
            {
                // Override OnHandHover
                var pictureFrameType = typeof(PictureFrame);
                var onHandHoverMethod = pictureFrameType.GetMethod("OnHandHover", BindingFlags.Public | BindingFlags.Instance);
                var postfix = typeof(PictureFrameFixer).GetMethod("OnHandHover_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onHandHoverMethod, null, new HarmonyMethod(postfix));

                // Override OnHandClick
                var onHandClickMethod = pictureFrameType.GetMethod("OnHandClick", BindingFlags.Public | BindingFlags.Instance);
                var prefix = typeof(PictureFrameFixer).GetMethod("OnHandClick_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onHandClickMethod, new HarmonyMethod(prefix), null);

                _patchedPictureFrame = true;
            }
        }

        /// <summary>This will make FCS mods compatible with the "Place batteries/powercells" feature.</summary>
        public static void FixFCSMods()
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
    }
}
