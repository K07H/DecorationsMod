using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DecorationsModConfigurator.TechTypes;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour UserControl_TechType.xaml
    /// </summary>
    public partial class UserControl_TechType : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SelectedTechTypeProperty = DependencyProperty.Register("SelectedTechType", typeof(string), typeof(UserControl_TechType), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty CustomTechTypeProperty = DependencyProperty.Register("CustomTechType", typeof(string), typeof(UserControl_TechType), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(CustomTechTypeChanged)));
        public static readonly DependencyProperty TransitionTechTypeProperty = DependencyProperty.Register("TransitionTechType", typeof(string), typeof(UserControl_TechType), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(TransitionTechTypeChanged)));

        public UserControl_TechType()
        {
            InitializeComponent();
            // Initialize default selection to Acid Mushroom
            ComboBox_TechType.SelectedIndex = 1;
            SelectedTechType = "Acid_Mushroom";
            // Initialize custom mode visibility to hidden
            SP_CustomTechType.Visibility = Visibility.Collapsed;

            (this.Content as FrameworkElement).DataContext = this;
        }

        public string SelectedTechType
        {
            get { return (string)GetValue(SelectedTechTypeProperty); }
            set
            {
                if (value != null)
                {
                    SetValue(SelectedTechTypeProperty, value);
#if DEBUG_TECHTYPE_SELECTOR
                    Logger.Log("DEBUG: TechType selection updated to [{0}]", value);
#endif
                    TransitionTechType = SelectedTechType;
                }
            }
        }

        public string CustomTechType
        {
            get { return (string)GetValue(CustomTechTypeProperty); }
            set
            {
                if (value != null)
                {
                    SetValue(CustomTechTypeProperty, value);
#if DEBUG_TECHTYPE_SELECTOR
                    Logger.Log("DEBUG: Custom TechType updated to [{0}]", value);
#endif
                    TransitionTechType = CustomTechType;
                }
            }
        }

        public string TransitionTechType
        {
            get { return (string)GetValue(TransitionTechTypeProperty); }
            set
            {
                if (value != null)
                    SetValue(TransitionTechTypeProperty, value);
            }
        }

        private static string TryGetTechTypeNameFromId(string techType)
        {
            // Check if given string is not empty
            if (string.IsNullOrEmpty(techType))
                return techType;
            // Check if given string is only composed of digits
            bool isDigits = true;
            for (int i = 0; ((i < techType.Length) && isDigits); i++)
                if (techType[i] < '0' || techType[i] > '9')
                    isDigits = false;
            // Try get TechType by ID
            Array values = Enum.GetValues(typeof(TechType));
            int length = values.Length;
            for (int i = 0; i < length; i++)
            {
                TechType currentTechType = (TechType)values.GetValue(i);
                if (string.Compare(Convert.ToString((int)currentTechType), techType, false, CultureInfo.InvariantCulture) == 0)
                    return currentTechType.AsString();
            }
            return techType;
        }

        public static void CustomTechTypeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            string newTechType = (string)e.NewValue;
            if (!string.IsNullOrEmpty(newTechType))
            {
                UserControl_TechType ctrl = (UserControl_TechType)o;
                ctrl.TransitionTechType = newTechType;
            }
        }

        public static void TransitionTechTypeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            string newTechType = (string)e.NewValue;
            if (!string.IsNullOrEmpty(newTechType))
            {
                UserControl_TechType ctrl = (UserControl_TechType)o;
                if (ctrl != null)
                {
                    if (AllTechTypes.ContainsValue(newTechType))
                    {
                        bool isCustom = true;
                        foreach (KeyValuePair<TechType, string> elem in AllTechTypes)
                        {
                            if (string.Compare(elem.Value, newTechType, false, CultureInfo.InvariantCulture) == 0)
                            {
                                foreach (object item in ctrl.ComboBox_TechType.Items)
                                {
                                    ComboBoxItem boxItem = (ComboBoxItem)item;
                                    if (boxItem != null && !string.IsNullOrEmpty(boxItem.Name) && string.Compare(boxItem.Name, newTechType, true, CultureInfo.InvariantCulture) == 0)
                                    {
#if DEBUG_TECHTYPE_SELECTOR
                                        Logger.Log("DEBUG: Found TechType [{0}] in combobox name=[{1}].", elem.Key.AsString(), boxItem.Name);
#endif
                                        ctrl.ComboBox_TechType.SelectedItem = boxItem;
                                        isCustom = false;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (isCustom)
                        {
#if DEBUG_TECHTYPE_SELECTOR
                            Logger.Log("DEBUG: TechType [{0}] not found in combobox, adding it as custom TechType.", newTechType);
#endif
                            ctrl.TB_CustomTechTypeName.Text = TryGetTechTypeNameFromId(newTechType);
                            ctrl.CustomTechType = ctrl.TB_CustomTechTypeName.Text;
                        }
                    }
                    else if (TechTypeExtensions.FromString(newTechType, out TechType techType, true) && techType != TechType.None)
                    {
                        bool isCustom = true;
                        if (AllTechTypes.ContainsKey(techType))
                        {
                            foreach (object item in ctrl.ComboBox_TechType.Items)
                            {
                                ComboBoxItem boxItem = (ComboBoxItem)item;
                                if (boxItem != null && !string.IsNullOrEmpty(boxItem.Name) && string.Compare(boxItem.Name, AllTechTypes[techType], true, CultureInfo.InvariantCulture) == 0)
                                {
#if DEBUG_TECHTYPE_SELECTOR
                                    Logger.Log("DEBUG: Found TechType [{0}] in combobox name=[{1}].", techType.AsString(), boxItem.Name);
#endif
                                    ctrl.ComboBox_TechType.SelectedItem = boxItem;
                                    isCustom = false;
                                    break;
                                }
                            }
                        }
                        if (isCustom)
                        {
#if DEBUG_TECHTYPE_SELECTOR
                            Logger.Log("DEBUG: TechType [{0}] not found in combobox, adding it as custom TechType.", newTechType);
#endif
                            ctrl.TB_CustomTechTypeName.Text = TryGetTechTypeNameFromId(newTechType);
                            ctrl.CustomTechType = ctrl.TB_CustomTechTypeName.Text;
                        }
                    }
                    else
                    {
#if DEBUG_TECHTYPE_SELECTOR
                        Logger.Log("DEBUG: TechType \"{0}\" is unknown. Using CUSTOM mode.", newTechType);
#endif
                        ctrl.TB_CustomTechTypeName.Text = TryGetTechTypeNameFromId(newTechType);
                        ctrl.CustomTechType = ctrl.TB_CustomTechTypeName.Text;
                    }
                }
            }
        }

        private void ComboBox_TechType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get currently selected TechType (return on failure)
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox == null)
                return;
            ComboBoxItem selected = (ComboBoxItem)comboBox.SelectedItem;
            if (selected == null || string.IsNullOrEmpty(selected.Name))
                return;

            // Updated selected TechType (triggers TransitionTechTypeChanged from dependency property)
            SelectedTechType = selected.Name;

            // Update custom mode visibility
            SP_CustomTechType.Visibility = (SelectedTechType == "Custom") ? Visibility.Visible : Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public static Dictionary<TechType, string> AllTechTypes = new Dictionary<TechType, string>()
        {
            { TechType.None, "Custom" },
            { TechType.AcidMushroom, "Acid_Mushroom" },
            { TechType.AcidMushroomSpore, "Acid_Mushroom_Spore" },
            { TechType.AdvancedWiringKit, "Advanced_Wiring_Kit" },
            { TechType.Aerogel, "Aerogel" },
            { TechType.AirBladder, "Air_Bladder" },
            { TechType.SeaTreaderPoop, "Alien_Feces" },
            { TechType.ArcadeGorgetoy, "Arcade_Gorge_Toy" },
            { TechType.StarshipSouvenir, "Aurora_Miniature" },
            { TechType.Battery, "Battery" },
            { TechType.Beacon, "Beacon" },
            { TechType.Benzene, "Benzene" },
            { TechType.Bladderfish, "Bladderfish" },
            { TechType.CookedBladderfish, "Cooked_Bladderfish" },
            { TechType.CuredBladderfish, "Cured_Bladderfish" },
            { TechType.Bleach, "Bleach" },
            { TechType.BloodOil, "Blood_Oil" },
            { TechType.Cap2, "Blue_Cap" },
            { TechType.BluePalmSeed, "Blue_Palm_Seed" },
            { TechType.Boomerang, "Boomerang" },
            { TechType.CookedBoomerang, "Cooked_Boomerang" },
            { TechType.CuredBoomerang, "Cured_Boomerang" },
            { TechType.PurpleBrainCoralPiece, "Brain_Coral_Sample" },
            { TechType.KooshChunk, "Bulb_Bush_Sample" },
            { TechType.BulboTreePiece, "Bulbo_Tree_Sample" },
            { TechType.MapRoomCamera, "Camera_Drone" },
            { TechType.LuggageBag, "Carry_all" },
            { TechType.PurpleBranchesSeed, "Cave_Bush_Seed" },
            { TechType.CrashPowder, "Cave_Sulfur" },
            { TechType.PurpleVegetable, "Chinese_Potato" },
            { TechType.Coffee, "Coffee" },
            { TechType.Compass, "Compass" },
            { TechType.ComputerChip, "Computer_Chip" },
            { TechType.Copper, "Copper_Ore" },
            { TechType.CopperWire, "Copper_Wire" },
            { TechType.CoralChunk, "Coral_Tube_Sample" },
            { TechType.CyclopsDecoy, "Creature_Decoy" },
            { TechType.CreepvinePiece, "Creepvine_Sample" },
            { TechType.CreepvineSeedCluster, "Creepvine_Seed_Cluster" },
            { TechType.Sulphur, "Crystalline_Sulfur" },
            { TechType.CyclopsDecoyModule, "Cyclops_Decoy_Tube_Upgrade" },
            { TechType.CyclopsHullModule1, "Cyclops_Depth_Module_MK1" },
            { TechType.CyclopsHullModule2, "Cyclops_Depth_Module_MK2" },
            { TechType.CyclopsHullModule3, "Cyclops_Depth_Module_MK3" },
            { TechType.CyclopsSeamothRepairModule, "Cyclops_Docking_Bay_Repair_Module" },
            { TechType.PowerUpgradeModule, "Cyclops_Engine_Efficiency_Module" },
            { TechType.CyclopsFireSuppressionModule, "Cyclops_Fire_Suppression_System" },
            { TechType.CyclopsShieldModule, "Cyclops_Shield_Generator" },
            { TechType.CyclopsSonarModule, "Cyclops_Sonar_Upgrade" },
            { TechType.CyclopsThermalReactorModule, "Cyclops_Thermal_Reactor_Module" },
            { TechType.LabContainer3, "Cylindrical_Sample_Flask" },
            { TechType.WhiteMushroom, "Deep_Shroom" },
            { TechType.WhiteMushroomSpore, "Deep_Shroom_Spore" },
            { TechType.Diamond, "Diamond" },
            { TechType.DisinfectedWater, "Disinfected_Water" },
            { TechType.BonesharkEgg, "Bone_Shark_Egg" },
            { TechType.CrabsnakeEgg, "Crabsnake_Egg" },
            { TechType.CrabsquidEgg, "Crabsquid_Egg" },
            { TechType.CrashEgg, "CrashEgg" },
            { TechType.CutefishEgg, "Cute_Fish_Egg" },
            { TechType.GasopodEgg, "Gasopod_Egg" },
            { TechType.JellyrayEgg, "Jellyray_Egg" },
            { TechType.LavaLizardEgg, "LavaLizard_Egg" },
            { TechType.MesmerEgg, "Mesmer_Egg" },
            { TechType.RabbitrayEgg, "Rabbit_Ray_Egg" },
            { TechType.SandsharkEgg, "Sand_Shark_Egg" },
            { TechType.ShockerEgg, "Shocker_Egg" },
            { TechType.SpadefishEgg, "Spadefish_Egg" },
            { TechType.StalkerEgg, "Stalker_Egg" },
            { TechType.EnameledGlass, "Enameled_Glass" },
            { TechType.VehiclePowerUpgradeModule, "Engine_Efficiency_Module" },
            { TechType.EyesPlantSeed, "Eye_Stalk_Seed" },
            { TechType.Eyeye, "Eyeye" },
            { TechType.CookedEyeye, "Cooked_Eyeye" },
            { TechType.CuredEyeye, "Cured_Eyeye" },
            { TechType.FernPalmSeed, "Fern_Palm_Seed" },
            { TechType.FiberMesh, "Fiber_Mesh" },
            { TechType.FilteredWater, "Filtered_Water" },
            { TechType.Fins, "Fins" },
            { TechType.FirstAidKit, "First_Aid_Kit" },
            { TechType.Flare, "Flare" },
            { TechType.Flashlight, "Flashlight" },
            { TechType.Floater, "Floater" },
            { TechType.TreeMushroomPiece, "Fungal_Sample" },
            { TechType.RedRollPlantSeed, "Furled_Papyrus_Seed" },
            { TechType.GabeSFeatherSeed, "Gabes_Feather_Seed" },
            { TechType.GarryFish, "Garryfish" },
            { TechType.CookedGarryFish, "Cooked_Garryfish" },
            { TechType.CuredGarryFish, "Cured_Garryfish" },
            { TechType.GasPod, "Gas_Pod" },
            { TechType.GasTorpedo, "Gas_Torpedo" },
            { TechType.JellyPlant, "Gel_Sack" },
            { TechType.JellyPlantSeed, "Gel_Sack_Spore" },
            { TechType.RedGreenTentacleSeed, "Ghost_Weed_Seed" },
            { TechType.Glass, "Glass" },
            { TechType.Gold, "Gold" },
            { TechType.Gravsphere, "Grav_Trap" },
            { TechType.Cap1, "Gray_Cap" },
            { TechType.OrangePetalsPlantSeed, "Grub_Basket_Seed" },
            { TechType.Builder, "Habitat_Builder" },
            { TechType.HatchingEnzymes, "Hatching_Enzymes" },
            { TechType.DoubleTank, "High_Capacity_Tank" },
            { TechType.HoleFish, "Holefish" },
            { TechType.CookedHoleFish, "Cooked_Holefish" },
            { TechType.CuredHoleFish, "Cured_Holefish" },
            { TechType.Hoopfish, "Hoopfish" },
            { TechType.CookedHoopfish, "Cooked_Hoopfish" },
            { TechType.CuredHoopfish, "Cured_Hoopfish" },
            { TechType.Hoverfish, "Hoverfish" },
            { TechType.CookedHoverfish, "Cooked_Hoverfish" },
            { TechType.CuredHoverfish, "Cured_Hoverfish" },
            { TechType.LabEquipment1, "Electron_Microscope" },
            { TechType.LabEquipment2, "Fluid_Analyzer" },
            { TechType.VehicleArmorPlating, "Hull_Reinforcement" },
            { TechType.HydrochloricAcid, "Hydrochloric_Acid" },
            { TechType.PrecursorIonBattery, "Ion_Battery" },
            { TechType.PrecursorIonPowerCell, "Ion_Power_Cell" },
            { TechType.PrecursorIonCrystal, "Ion_Precursor_Crystal" },
            { TechType.OrangeMushroomSpore, "Jaffa_Cup_Seed" },
            { TechType.SnakeMushroomSpore, "Jellyshroom_Spore" },
            { TechType.Kyanite, "Kyanite" },
            { TechType.HangingFruit, "Lantern_Fruit" },
            { TechType.BigFilteredWater, "Large_Filtered_Water" },
            { TechType.LabContainer, "Large_Sample_Flask" },
            { TechType.LaserCutter, "Laser_Cutter" },
            { TechType.Lead, "Lead" },
            { TechType.LEDLight, "Light_Stick" },
            { TechType.PlasteelTank, "Lightweight_High_Capacity_Tank" },
            { TechType.Lithium, "Lithium" },
            { TechType.Lubricant, "Lubricant" },
            { TechType.LavaBoomerang, "Magmarang" },
            { TechType.CookedLavaBoomerang, "Cooked_Magmarang" },
            { TechType.CuredLavaBoomerang, "Cured_Magmarang" },
            { TechType.Magnetite, "Magnetite" },
            { TechType.Melon, "Marblemelon" },
            { TechType.MelonSeed, "Marblemelon_Seeds" },
            { TechType.MembrainTreeSeed, "Membrain_Tree_Seed" },
            { TechType.ScrapMetal, "Metal_Salvage" },
            { TechType.PurpleVasePlantSeed, "Ming_Plant_Seed" },
            { TechType.Constructor, "Mobile_Vehicle_Bay" },
            { TechType.Nickel, "Nickel_Ore" },
            { TechType.NutrientBlock, "Nutrient_Block" },
            { TechType.Oculus, "Oculus" },
            { TechType.CookedOculus, "Cooked_Oculus" },
            { TechType.CuredOculus, "Cured_Oculus" },
            { TechType.DiveReel, "Pathfinder_Tool" },
            { TechType.Peeper, "Peeper" },
            { TechType.CookedPeeper, "Cooked_Peeper" },
            { TechType.CuredPeeper, "Cured_Peeper" },
            { TechType.PinkMushroom, "Pink_Cap" },
            { TechType.PinkMushroomSpore, "Pink_Cap_Spore" },
            { TechType.Pipe, "Pipe" },
            { TechType.BasePipeConnector, "Base_Pipe_Connector" },
            { TechType.PipeSurfaceFloater, "Pipe_Surface_Floater" },
            { TechType.PlasteelIngot, "Plasteel_Ingot" },
            { TechType.Polyaniline, "Polyaniline" },
            { TechType.PosterAurora, "Poster_Aurora" },
            { TechType.Poster, "Poster_Natural_Selection_2" },
            { TechType.PosterKitty, "Poster_Kitty" },
            { TechType.PosterExoSuit1, "Poster_Exosuit" },
            { TechType.PosterExoSuit2, "Poster_Exosuit_2" },
            { TechType.PowerCell, "Power_Cell" },
            { TechType.ExoHullModule1, "Prawn_Suit_Depth_Module_MK1" },
            { TechType.ExoHullModule2, "Prawn_Suit_Depth_Module_MK2" },
            { TechType.ExosuitDrillArmModule, "Prawn_Suit_Drill_Arm" },
            { TechType.ExosuitGrapplingArmModule, "Prawn_Suit_Grappling_Arm" },
            { TechType.ExosuitJetUpgradeModule, "Prawn_Suit_Jump_Jet_Upgrade" },
            { TechType.ExosuitPropulsionArmModule, "Prawn_Suit_Propulsion_Cannon" },
            { TechType.ExosuitThermalReactorModule, "Prawn_Suit_Thermal_Reactor" },
            { TechType.ExosuitTorpedoArmModule, "Prawn_Suit_Torpedo_Arm" },
            { TechType.PropulsionCannon, "Propulsion_Cannon" },
            { TechType.SmallFanCluster, "Pygmy_Fan_Seed" },
            { TechType.Quartz, "Quartz" },
            { TechType.RadiationSuit, "Radiation_Suit" },
            { TechType.ReactorRod, "Reactor_Rod" },
            { TechType.DepletedReactorRod, "Reactor_Rod_Depleted" },
            { TechType.Rebreather, "Rebreather" },
            { TechType.StillsuitWater, "Reclaimed_Water" },
            { TechType.LavaEyeye, "Red_Eyeye" },
            { TechType.CookedLavaEyeye, "Cooked_Red_Eyeye" },
            { TechType.CuredLavaEyeye, "Cured_Red_Eyeye" },
            { TechType.RedBushSeed, "Redwort_Seed" },
            { TechType.Reginald, "Reginald" },
            { TechType.CookedReginald, "Cooked_Reginald" },
            { TechType.CuredReginald, "Cured_Reginald" },
            { TechType.RedConePlantSeed, "Regress_Shell_Seed" },
            { TechType.ReinforcedDiveSuit, "Reinforced_Dive_Suit" },
            { TechType.Welder, "Repair_Tool" },
            { TechType.RepulsionCannon, "Repulsion_Cannon" },
            { TechType.RedBasketPlantSeed, "Rouge_Cradle_Seed" },
            { TechType.AluminumOxide, "Ruby_Crystal" },
            { TechType.Salt, "Salt_Deposit" },
            { TechType.LabEquipment3, "Sample_Analyzer" },
            { TechType.Scanner, "Scanner" },
            { TechType.MapRoomHUDChip, "Scanner_Room_HUD_Chip" },
            { TechType.MapRoomUpgradeScanRange, "Scanner_Room_Range_Upgrade" },
            { TechType.MapRoomUpgradeScanSpeed, "Scanner_Room_Speed_Upgrade" },
            { TechType.SeaCrownSeed, "Sea_Crown_Seed" },
            { TechType.Seaglide, "Seaglide" },
            { TechType.VehicleHullModule1, "Seamoth_Depth_Module_MK1" },
            { TechType.VehicleHullModule2, "Seamoth_Depth_Module_MK2" },
            { TechType.VehicleHullModule3, "Seamoth_Depth_Module_MK3" },
            { TechType.SeamothSolarCharge, "Seamoth_Solar_Charger" },
            { TechType.SeamothSonarModule, "Seamoth_Sonar" },
            { TechType.SeamothTorpedoModule, "Seamoth_Torpedo_System" },
            { TechType.Silicone, "Silicone_Rubber" },
            { TechType.Silver, "Silver_Ore" },
            { TechType.SmallMelon, "Small_Marblemelon" },
            { TechType.LabContainer2, "Small_Sample_Flask" },
            { TechType.Snack1, "Snack1" },
            { TechType.Snack2, "Snack2" },
            { TechType.Snack3, "Snack3" },
            { TechType.Spadefish, "Spadefish" },
            { TechType.CookedSpadefish, "Cooked_Spadefish" },
            { TechType.CuredSpadefish, "Cured_Spadefish" },
            { TechType.PurpleRattle, "Speckled_Rattler" },
            { TechType.PurpleRattleSpore, "Speckled_Rattler_Spore" },
            { TechType.ShellGrassSeed, "Spiked_Horn_Grass_Seed" },
            { TechType.Spinefish, "Spinefish" },
            { TechType.CookedSpinefish, "Cooked_Spinefish" },
            { TechType.CuredSpinefish, "Cured_Spinefish" },
            { TechType.SpottedLeavesPlantSeed, "Spotted_Dockleaf_Seed" },
            { TechType.StalkerTooth, "Stalker_Tooth" },
            { TechType.Tank, "Standard_O2_Tank" },
            { TechType.StasisRifle, "Stasis_Rifle" },
            { TechType.Stillsuit, "Stillsuit" },
            { TechType.VehicleStorageModule, "Storage_Module" },
            { TechType.Knife, "Survival_Knife" },
            { TechType.SwimChargeFins, "Swim_Charge_Fins" },
            { TechType.AramidFibers, "Synthetic_Fibers" },
            { TechType.JeweledDiskPiece, "Table_Coral_Sample" },
            { TechType.HeatBlade, "Thermoblade" },
            { TechType.SpikePlantSeed, "Tiger_Plant_Seed" },
            { TechType.Titanium, "Titanium" },
            { TechType.TitaniumIngot, "Titanium_Ingot" },
            { TechType.ToyCar, "Toy_Car" },
            { TechType.UltraGlideFins, "Ultra_Glide_Fins" },
            { TechType.HighCapacityTank, "Ultra_High_Capacity_Tank" },
            { TechType.UraniniteCrystal, "Uraninite_Crystal" },
            { TechType.PurpleFanSeed, "Veined_Nettle_Seed" },
            { TechType.PurpleStalkSeed, "Violet_Beau_Seed" },
            { TechType.WhirlpoolTorpedo, "Vortex_Torpedo" },
            { TechType.PinkFlowerSeed, "Voxel_Shrub_Seed" },
            { TechType.SmallStorage, "Waterproof_Locker" },
            { TechType.WiringKit, "Wiring_Kit" },
            { TechType.PurpleTentacleSeed, "Writhing_Weed_Seed" }
        };
    }
}
