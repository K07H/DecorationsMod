﻿using DecorationsMod.Fixers;
using HarmonyLib;
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
                Logger.Error("Unable to initialize Harmony!");
                return false;
            }
            return true;
        }

        private static bool _allPatched = false;
        /// <summary>Patches Subnautica DLL with Harmony.</summary>
        public static void PatchAll()
        {
            if (_allPatched)
                return;
            Logger.Info("Patching with Harmony...");
            // Fix cargo crates items-containers
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Fixing cargo crates item containers...");
#endif
            var onProtoDeserializeObjectTreeMethod = typeof(StorageContainer).GetMethod("OnProtoDeserializeObjectTree", BindingFlags.Public | BindingFlags.Instance);
            var onProtoDeserializeObjectTreePostfix = typeof(StorageContainerFixer).GetMethod("OnProtoDeserializeObjectTree_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(onProtoDeserializeObjectTreeMethod, null, new HarmonyMethod(onProtoDeserializeObjectTreePostfix));
            // Failsafe on lockers and cargo crates deconstruction + custom base parts
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Adding failsafe on lockers and cargo crates deconstruction...");
#endif
            var canDeconstructMethod = typeof(Constructable).GetMethod("CanDeconstruct", BindingFlags.Public | BindingFlags.Instance);
            var canDeconstructPrefix = typeof(ConstructableFixer).GetMethod("CanDeconstruct_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(canDeconstructMethod, new HarmonyMethod(canDeconstructPrefix), null);
            var constructMethod = typeof(Constructable).GetMethod("Construct", BindingFlags.Public | BindingFlags.Instance);
            var constructPostfix = typeof(ConstructableFixer).GetMethod("Construct_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(constructMethod, null, new HarmonyMethod(constructPostfix));
            // Fix equipment types for batteries, power cells, and their ion versions
            if (ConfigSwitcher.EnablePlaceBatteries)
                PatchBatteries();
            // Fix aquarium sky appliers
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Fixing aquarium sky appliers...");
#endif
            var addItemMethod = typeof(Aquarium).GetMethod("AddItem", BindingFlags.NonPublic | BindingFlags.Instance);
            var addItemPostfix = typeof(AquariumFixer).GetMethod("AddItem_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(addItemMethod, null, new HarmonyMethod(addItemPostfix));
            var removeItemMethod = typeof(Aquarium).GetMethod("RemoveItem", BindingFlags.NonPublic | BindingFlags.Instance);
            var removeItemPrefix = typeof(AquariumFixer).GetMethod("RemoveItem_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(removeItemMethod, new HarmonyMethod(removeItemPrefix), null);
            // Fix alternative use for placeable items
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Fixing alternative use for placeable items...");
#endif
            if (ConfigSwitcher.EnablePlaceItems)
            {
                /* LEGACY
                var getAltUseItemActionMethod = typeof(Inventory).GetMethod("GetAltUseItemAction", BindingFlags.Public | BindingFlags.Instance);
                var getAltUseItemActionPrefix = typeof(InventoryFixer).GetMethod("GetAltUseItemAction_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(getAltUseItemActionMethod, new HarmonyMethod(getAltUseItemActionPrefix), null);
                */
                var getAllItemActionsMethod = typeof(Inventory).GetMethod("GetAllItemActions", BindingFlags.Public | BindingFlags.Instance);
                var getAllItemActionsPrefix = typeof(InventoryFixer).GetMethod("GetAllItemActions_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(getAllItemActionsMethod, new HarmonyMethod(getAllItemActionsPrefix), null);
                /* DEBUG
                var getItemActionMethod = typeof(Inventory).GetMethod("GetItemAction", BindingFlags.Public | BindingFlags.Instance);
                var getItemActionPrefix = typeof(InventoryFixer).GetMethod("GetItemAction_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(getItemActionMethod, new HarmonyMethod(getItemActionPrefix), null);
                */
            }
            // Fix colors for custom fabricators icons
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Fixing colors for custom fabricators icons...");
#endif
            var createIconMethod = typeof(uGUI_CraftingMenu).GetMethod("CreateIcon", BindingFlags.NonPublic | BindingFlags.Instance);
            //var createIconMethod = typeof(uGUI_CraftNode).GetMethod("CreateIcon", BindingFlags.NonPublic | BindingFlags.Instance);
            var createIconPostfix = typeof(uGUI_CraftNodeFixer).GetMethod("CreateIcon_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(createIconMethod, null, new HarmonyMethod(createIconPostfix));
            // Setup new items unlock conditions
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Setting up new items unlock conditions...");
#endif
            var isCraftRecipeUnlockedMethod = typeof(CrafterLogic).GetMethod("IsCraftRecipeUnlocked", BindingFlags.Public | BindingFlags.Static);
            var isCraftRecipeUnlockedPostfix = typeof(CrafterLogicFixer).GetMethod("IsCraftRecipeUnlocked_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(isCraftRecipeUnlockedMethod, null, new HarmonyMethod(isCraftRecipeUnlockedPostfix));
            var getTechUnlockStateMethod = typeof(KnownTech).GetMethod("GetTechUnlockState", new Type[] { typeof(TechType), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }); //, BindingFlags.Public | BindingFlags.Static);
            var getTechUnlockStatePrefix = typeof(KnownTechFixer).GetMethod("GetTechUnlockState_Prefix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(getTechUnlockStateMethod, new HarmonyMethod(getTechUnlockStatePrefix), null);
            // Load added "new item" notifications and ladders positions when game loads
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Setting up data-loading for notifications and ladders...");
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
            // Save added "new item" notifications and ladders positions when game saves
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Setting up data-saving for notifications and ladders...");
#endif
            var saveGameMethod = typeof(IngameMenu).GetMethod("SaveGame", BindingFlags.Public | BindingFlags.Instance);
            var saveGamePostfix = typeof(IngameMenuFixer).GetMethod("SaveGame_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(saveGameMethod, null, new HarmonyMethod(saveGamePostfix));
            var quitGameMethod = typeof(IngameMenu).GetMethod("QuitGame", BindingFlags.Public | BindingFlags.Instance);
            var quitGamePostfix = typeof(IngameMenuFixer).GetMethod("QuitGame_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(quitGameMethod, null, new HarmonyMethod(quitGamePostfix));
            //var onYesMethod = typeof(IngameMenuQuitConfirmation).GetMethod("OnYes", BindingFlags.Public | BindingFlags.Instance);
            //var onYesPrefix = typeof(IngameMenuQuitConfirmationFixer).GetMethod("OnYes_Prefix", BindingFlags.Public | BindingFlags.Static);
            //HarmonyInstance.Patch(onYesMethod, new HarmonyMethod(onYesPrefix), null);
            // Handles "Hide Degasi base (500m)" feature
#if DEBUG_HARMONY_PATCHING
                Logger.Debug("Adding biome checks...");
#endif
            var calculateBiomeMethod = typeof(Player).GetMethod("CalculateBiome", BindingFlags.NonPublic | BindingFlags.Instance);
            var calculateBiomePostfix = typeof(PlayerFixer).GetMethod("CalculateBiome_Postfix", BindingFlags.Public | BindingFlags.Static);
            HarmonyInstance.Patch(calculateBiomeMethod, null, new HarmonyMethod(calculateBiomePostfix));
            if (ConfigSwitcher.EnableNewFlora)
            {
                // Give salt when purple pinecone is cut
#if DEBUG_HARMONY_PATCHING
                Logger.Debug("Setting up purple pinecone harvested material...");
#endif
                var giveResourceOnDamageMethod = typeof(Knife).GetMethod("GiveResourceOnDamage", BindingFlags.NonPublic | BindingFlags.Instance);
                var giveResourceOnDamagePostfix = typeof(KnifeFixer).GetMethod("GiveResourceOnDamage_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(giveResourceOnDamageMethod, null, new HarmonyMethod(giveResourceOnDamagePostfix));
                // Change custom plants tooltips
#if DEBUG_HARMONY_PATCHING
                Logger.Debug("Setting up plant tooltips...");
#endif
                var onHandHoverMethod = typeof(GrownPlant).GetMethod("OnHandHover", BindingFlags.Public | BindingFlags.Instance);
                var onHandHoverPostfix = typeof(GrownPlantFixer).GetMethod("OnHandHover_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onHandHoverMethod, null, new HarmonyMethod(onHandHoverPostfix));
                var plant_onHandClickMethod = typeof(GrownPlant).GetMethod("OnHandClick", BindingFlags.Public | BindingFlags.Instance);
                var plant_onHandClickPrefix = typeof(GrownPlantFixer).GetMethod("OnHandClick_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(plant_onHandClickMethod, new HarmonyMethod(plant_onHandClickPrefix), null);
                // Make new flora spawn as seeds when using console commands (instead of grown plants)
#if DEBUG_HARMONY_PATCHING
                Logger.Debug("Making plants spawning as seeds...");
#endif
                /*
                var onConsoleCommand_itemMethod = typeof(InventoryConsoleCommands).GetMethod("OnConsoleCommand_item", BindingFlags.NonPublic | BindingFlags.Instance);
                var onConsoleCommand_itemPrefix = typeof(InventoryConsoleCommandsFixer).GetMethod("OnConsoleCommand_item_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onConsoleCommand_itemMethod, new HarmonyMethod(onConsoleCommand_itemPrefix), null);
                */
                var ItemCmdSpawnAsyncMethod = typeof(InventoryConsoleCommands).GetMethod("ItemCmdSpawnAsync", BindingFlags.NonPublic | BindingFlags.Static);
                var ItemCmdSpawnAsyncPostfix = typeof(InventoryConsoleCommandsFixer).GetMethod("ItemCmdSpawnAsync_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(ItemCmdSpawnAsyncMethod, null, new HarmonyMethod(ItemCmdSpawnAsyncPostfix));
                /*
                var onConsoleCommand_spawnMethod = typeof(SpawnConsoleCommand).GetMethod("OnConsoleCommand_spawn", BindingFlags.NonPublic | BindingFlags.Instance);
                var onConsoleCommand_spawnPrefix = typeof(SpawnConsoleCommandFixer).GetMethod("OnConsoleCommand_spawn_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(onConsoleCommand_spawnMethod, new HarmonyMethod(onConsoleCommand_spawnPrefix), null);
                */
                var SpawnAsyncMethod = typeof(SpawnConsoleCommand).GetMethod("SpawnAsync", BindingFlags.NonPublic | BindingFlags.Instance);
                var SpawnAsyncPostfix = typeof(SpawnConsoleCommandFixer).GetMethod("SpawnAsync_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(SpawnAsyncMethod, null, new HarmonyMethod(SpawnAsyncPostfix));
                // Handles pland/seed state upon drop and pickup
#if DEBUG_HARMONY_PATCHING
                Logger.Debug("Setting up plants interactions...");
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
                Logger.Debug("Setting up custom base parts...");
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
            _allPatched = true;
        }

        private static bool _patchedBatteries = false;
        /// <summary>This will make batteries and powercells placeable.</summary>
        public static void PatchBatteries()
        {
            if (_patchedBatteries)
                return;
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Making batteries and powercells placeable...");
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

        private static bool _patchedPictureFrame = false;
        /// <summary>This will enable advanced features for our customizable picture frame.</summary>
        public static void PatchPictureFrames()
        {
            if (_patchedPictureFrame)
                return;

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

        /*
        private static bool _fixedAutoLoadMod = false;
        /// <summary>This wil enforce loading process when AutoLoad mod is present.</summary>
        public static void FixAutoLoadMod()
        {
            if (_fixedAutoLoadMod)
                return;

            IQMod autoLoadMod = QModServices.Main.FindModById("Straitjacket.Subnautica.Mods.AutoLoad");
            if (autoLoadMod != null && autoLoadMod.Enable)
            {
#if DEBUG_HARMONY_PATCHING
                Logger.Debug("Fixing for AutoLoad mod...");
#endif
                var beginAsyncSceneLoadMethod = typeof(uGUI_SceneLoading).GetMethod("BeginAsyncSceneLoad", BindingFlags.Public | BindingFlags.Instance);
                var beginAsyncSceneLoadPrefix = typeof(AutoLoadModFixer).GetMethod("BeginAsyncSceneLoad_Prefix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(beginAsyncSceneLoadMethod, new HarmonyMethod(beginAsyncSceneLoadPrefix), null);
            }
            _fixedAutoLoadMod = true;
        }
        */

        private static bool _fixedSignInput = false;
        /// <summary>This will fix sign input loading vanilla bug.</summary>
        public static void FixSignInput()
        {
            if (_fixedSignInput)
                return;
#if DEBUG_HARMONY_PATCHING
            Logger.Debug("Fixing uGUI_SignInput loading...");
#endif
            var updateScaleMethod = typeof(uGUI_SignInput).GetMethod("UpdateScale", BindingFlags.NonPublic | BindingFlags.Instance);
            if (updateScaleMethod != null)
            {
                var updateScalePostfix = typeof(DECOuGUI_SignInputFixer).GetMethod("DECOUpdateScale_Postfix", BindingFlags.Public | BindingFlags.Static);
                HarmonyInstance.Patch(updateScaleMethod, null, new HarmonyMethod(updateScalePostfix));
            }
            _fixedSignInput = true;
        }
    }
}
