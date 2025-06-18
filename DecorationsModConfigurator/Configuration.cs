using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using static DecorationsModConfigurator.LanguageHelper;

namespace DecorationsModConfigurator
{
    public class Configuration
    {
        #region Constants

#if DEBUG_CONFIG_CHANGED
        private const string LOG_CONFIG_CHANGE = "DEBUG: CONFIG CHANGE: {0}: [{1}]=>[{2}]";
#endif

        #endregion

        #region General settings

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (string.Compare(_language, value, true, CultureInfo.InvariantCulture) != 0)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(Language), _language, value);
#endif
                    _language = value;
                }
            }
        }

        private bool _useCompactTooltips;
        public bool UseCompactTooltips
        {
            get => _useCompactTooltips;
            set
            {
                if (_useCompactTooltips != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(UseCompactTooltips), _useCompactTooltips, value);
#endif
                    _useCompactTooltips = value;
                }
            }
        }

        private bool _lockQuickslotsWhenPlacingItem;
        public bool LockQuickslotsWhenPlacingItem
        {
            get => _lockQuickslotsWhenPlacingItem;
            set
            {
                if (_lockQuickslotsWhenPlacingItem != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(LockQuickslotsWhenPlacingItem), _lockQuickslotsWhenPlacingItem, value);
#endif
                    _lockQuickslotsWhenPlacingItem = value;
                }
            }
        }

        private bool _allowBuildOutside;
        public bool AllowBuildOutside
        {
            get => _allowBuildOutside;
            set
            {
                if (_allowBuildOutside != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AllowBuildOutside), _allowBuildOutside, value);
#endif
                    _allowBuildOutside = value;
                }
            }
        }

        private bool _allowPlaceOutside;
        public bool AllowPlaceOutside
        {
            get => _allowPlaceOutside;
            set
            {
                if (_allowPlaceOutside != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AllowPlaceOutside), _allowPlaceOutside, value);
#endif
                    _allowPlaceOutside = value;
                }
            }
        }

        private bool _enablePlaceItems;
        public bool EnablePlaceItems
        {
            get => _enablePlaceItems;
            set
            {
                if (_enablePlaceItems != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnablePlaceItems), _enablePlaceItems, value);
#endif
                    _enablePlaceItems = value;
                }
            }
        }

        private bool _enablePlaceMaterials;
        public bool EnablePlaceMaterials
        {
            get => _enablePlaceMaterials;
            set
            {
                if (_enablePlaceMaterials != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnablePlaceMaterials), _enablePlaceMaterials, value);
#endif
                    _enablePlaceMaterials = value;
                }
            }
        }

        private bool _enablePlaceBatteries;
        public bool EnablePlaceBatteries
        {
            get => _enablePlaceBatteries;
            set
            {
                if (_enablePlaceBatteries != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnablePlaceBatteries), _enablePlaceBatteries, value);
#endif
                    _enablePlaceBatteries = value;
                }
            }
        }

        private bool _enablePlaceEggs;
        public bool EnablePlaceEggs
        {
            get => _enablePlaceEggs;
            set
            {
                if (_enablePlaceEggs != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnablePlaceEggs), _enablePlaceEggs, value);
#endif
                    _enablePlaceEggs = value;
                }
            }
        }

        private bool _enableNewFlora;
        public bool EnableNewFlora
        {
            get => _enableNewFlora;
            set
            {
                if (_enableNewFlora != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableNewFlora), _enableNewFlora, value);
#endif
                    _enableNewFlora = value;
                }
            }
        }

        private bool _fixAquariumLighting;
        public bool FixAquariumLighting
        {
            get => _fixAquariumLighting;
            set
            {
                if (_fixAquariumLighting != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(FixAquariumLighting), _fixAquariumLighting, value);
#endif
                    _fixAquariumLighting = value;
                }
            }
        }

        private bool _enableAquariumGlassGlowing;
        public bool EnableAquariumGlassGlowing
        {
            get => _enableAquariumGlassGlowing;
            set
            {
                if (_enableAquariumGlassGlowing != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableAquariumGlassGlowing), _enableAquariumGlassGlowing, value);
#endif
                    _enableAquariumGlassGlowing = value;
                }
            }
        }

        #endregion

        #region Habitat builder settings

        private bool _enableNewItems;
        public bool EnableNewItems
        {
            get => _enableNewItems;
            set
            {
                if (_enableNewItems != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableNewItems), _enableNewItems, value);
#endif
                    _enableNewItems = value;
                }
            }
        }

        private bool _enableSofas;
        public bool EnableSofas
        {
            get => _enableSofas;
            set
            {
                if (_enableSofas != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableSofas), _enableSofas, value);
#endif
                    _enableSofas = value;
                }
            }
        }

        private bool _enableDecorativeElectronics;
        public bool EnableDecorativeElectronics
        {
            get => _enableDecorativeElectronics;
            set
            {
                if (_enableDecorativeElectronics != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableDecorativeElectronics), _enableDecorativeElectronics, value);
#endif
                    _enableDecorativeElectronics = value;
                }
            }
        }

        private bool _enableCustomBaseParts;
        public bool EnableCustomBaseParts
        {
            get => _enableCustomBaseParts;
            set
            {
                if (_enableCustomBaseParts != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableCustomBaseParts), _enableCustomBaseParts, value);
#endif
                    _enableCustomBaseParts = value;
                }
            }
        }

        private bool _allowIndoorLongPlanterOutside;
        public bool AllowIndoorLongPlanterOutside
        {
            get => _allowIndoorLongPlanterOutside;
            set
            {
                if (_allowIndoorLongPlanterOutside != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AllowIndoorLongPlanterOutside), _allowIndoorLongPlanterOutside, value);
#endif
                    _allowIndoorLongPlanterOutside = value;
                }
            }
        }

        private bool _allowOutdoorLongPlanterInside;
        public bool AllowOutdoorLongPlanterInside
        {
            get => _allowOutdoorLongPlanterInside;
            set
            {
                if (_allowOutdoorLongPlanterInside != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AllowOutdoorLongPlanterInside), _allowOutdoorLongPlanterInside, value);
#endif
                    _allowOutdoorLongPlanterInside = value;
                }
            }
        }

        private string _habitatBuilderItems;
        public string HabitatBuilderItems
        {
            get => _habitatBuilderItems;
            set
            {
                if (_habitatBuilderItems != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(HabitatBuilderItems), _habitatBuilderItems, value);
#endif
                    _habitatBuilderItems = value;
                }
            }
        }

        #endregion

        #region Discovery settings

        private bool _addItemsWhenDiscovered;
        public bool AddItemsWhenDiscovered
        {
            get => _addItemsWhenDiscovered;
            set
            {
                if (_addItemsWhenDiscovered != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AddItemsWhenDiscovered), _addItemsWhenDiscovered, value);
#endif
                    _addItemsWhenDiscovered = value;
                }
            }
        }

        private bool _addAirSeedsWhenDiscovered;
        public bool AddAirSeedsWhenDiscovered
        {
            get => _addAirSeedsWhenDiscovered;
            set
            {
                if (_addAirSeedsWhenDiscovered != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AddAirSeedsWhenDiscovered), _addAirSeedsWhenDiscovered, value);
#endif
                    _addAirSeedsWhenDiscovered = value;
                }
            }
        }

        private bool _addWaterSeedsWhenDiscovered;
        public bool AddWaterSeedsWhenDiscovered
        {
            get => _addWaterSeedsWhenDiscovered;
            set
            {
                if (_addWaterSeedsWhenDiscovered != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AddWaterSeedsWhenDiscovered), _addWaterSeedsWhenDiscovered, value);
#endif
                    _addWaterSeedsWhenDiscovered = value;
                }
            }
        }

        private bool _addEggsWhenCreatureScanned;
        public bool AddEggsWhenCreatureScanned
        {
            get => _addEggsWhenCreatureScanned;
            set
            {
                if (_addEggsWhenCreatureScanned != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AddEggsWhenCreatureScanned), _addEggsWhenCreatureScanned, value);
#endif
                    _addEggsWhenCreatureScanned = value;
                }
            }
        }

        private bool _addEggsAtStart;
        public bool AddEggsAtStart
        {
            get => _addEggsAtStart;
            set
            {
                if (_addEggsAtStart != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AddEggsAtStart), _addEggsAtStart, value);
#endif
                    _addEggsAtStart = value;
                }
            }
        }

        #endregion

        #region Precursor settings

        private bool _enablePrecursorTab;
        public bool EnablePrecursorTab
        {
            get => _enablePrecursorTab;
            set
            {
                if (_enablePrecursorTab != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnablePrecursorTab), _enablePrecursorTab, value);
#endif
                    _enablePrecursorTab = value;
                }
            }
        }

        private bool _precursorKeysAll;
        public bool PrecursorKeysAll
        {
            get => _precursorKeysAll;
            set
            {
                if (_precursorKeysAll != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(PrecursorKeysAll), _precursorKeysAll, value);
#endif
                    _precursorKeysAll = value;
                }
            }
        }

        private string _precursorKeys_RecipiesResource;
        public string PrecursorKeys_RecipiesResource
        {
            get => _precursorKeys_RecipiesResource;
            set
            {
                if (string.Compare(_precursorKeys_RecipiesResource, value, true, CultureInfo.InvariantCulture) != 0)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(PrecursorKeys_RecipiesResource), _precursorKeys_RecipiesResource, value);
#endif
                    _precursorKeys_RecipiesResource = value;
                }
            }
        }

        private int _precursorKeys_RecipiesResourceAmount;
        public int PrecursorKeys_RecipiesResourceAmount
        {
            get => _precursorKeys_RecipiesResourceAmount;
            set
            {
                if (_precursorKeys_RecipiesResourceAmount != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(PrecursorKeys_RecipiesResourceAmount), _precursorKeys_RecipiesResourceAmount, value);
#endif
                    _precursorKeys_RecipiesResourceAmount = value;
                }
            }
        }

        private string _relics_RecipiesResource;
        public string Relics_RecipiesResource
        {
            get => _relics_RecipiesResource;
            set
            {
                if (string.Compare(_relics_RecipiesResource, value, true, CultureInfo.InvariantCulture) != 0)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(Relics_RecipiesResource), _relics_RecipiesResource, value);
#endif
                    _relics_RecipiesResource = value;
                }
            }
        }

        private int _relics_RecipiesResourceAmount;
        public int Relics_RecipiesResourceAmount
        {
            get => _relics_RecipiesResourceAmount;
            set
            {
                if (_relics_RecipiesResourceAmount != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(Relics_RecipiesResourceAmount), _relics_RecipiesResourceAmount, value);
#endif
                    _relics_RecipiesResourceAmount = value;
                }
            }
        }

        private bool _alienRelic1Animation;
        public bool AlienRelic1Animation
        {
            get => _alienRelic1Animation;
            set
            {
                if (_alienRelic1Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic1Animation), _alienRelic1Animation, value);
#endif
                    _alienRelic1Animation = value;
                }
            }
        }

        private bool _alienRelic2Animation;
        public bool AlienRelic2Animation
        {
            get => _alienRelic2Animation;
            set
            {
                if (_alienRelic2Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic2Animation), _alienRelic2Animation, value);
#endif
                    _alienRelic2Animation = value;
                }
            }
        }

        private bool _alienRelic3Animation;
        public bool AlienRelic3Animation
        {
            get => _alienRelic3Animation;
            set
            {
                if (_alienRelic3Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic3Animation), _alienRelic3Animation, value);
#endif
                    _alienRelic3Animation = value;
                }
            }
        }

        private bool _alienRelic4Animation;
        public bool AlienRelic4Animation
        {
            get => _alienRelic4Animation;
            set
            {
                if (_alienRelic4Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic4Animation), _alienRelic4Animation, value);
#endif
                    _alienRelic4Animation = value;
                }
            }
        }

        private bool _alienRelic5Animation;
        public bool AlienRelic5Animation
        {
            get => _alienRelic5Animation;
            set
            {
                if (_alienRelic5Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic5Animation), _alienRelic5Animation, value);
#endif
                    _alienRelic5Animation = value;
                }
            }
        }

        private bool _alienRelic6Animation;
        public bool AlienRelic6Animation
        {
            get => _alienRelic6Animation;
            set
            {
                if (_alienRelic6Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic6Animation), _alienRelic6Animation, value);
#endif
                    _alienRelic6Animation = value;
                }
            }
        }

        private bool _alienRelic7Animation;
        public bool AlienRelic7Animation
        {
            get => _alienRelic7Animation;
            set
            {
                if (_alienRelic7Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic7Animation), _alienRelic7Animation, value);
#endif
                    _alienRelic7Animation = value;
                }
            }
        }

        private bool _alienRelic8Animation;
        public bool AlienRelic8Animation
        {
            get => _alienRelic8Animation;
            set
            {
                if (_alienRelic8Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic8Animation), _alienRelic8Animation, value);
#endif
                    _alienRelic8Animation = value;
                }
            }
        }

        private bool _alienRelic9Animation;
        public bool AlienRelic9Animation
        {
            get => _alienRelic9Animation;
            set
            {
                if (_alienRelic9Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic9Animation), _alienRelic9Animation, value);
#endif
                    _alienRelic9Animation = value;
                }
            }
        }

        private bool _alienRelic10Animation;
        public bool AlienRelic10Animation
        {
            get => _alienRelic10Animation;
            set
            {
                if (_alienRelic10Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic10Animation), _alienRelic10Animation, value);
#endif
                    _alienRelic10Animation = value;
                }
            }
        }

        private bool _alienRelic11Animation;
        public bool AlienRelic11Animation
        {
            get => _alienRelic11Animation;
            set
            {
                if (_alienRelic11Animation != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AlienRelic11Animation), _alienRelic11Animation, value);
#endif
                    _alienRelic11Animation = value;
                }
            }
        }

        #endregion

        #region Eggs settings

        private bool _enableAllEggs;
        public bool EnableAllEggs
        {
            get => _enableAllEggs;
            set
            {
                if (_enableAllEggs != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableAllEggs), _enableAllEggs, value);
#endif
                    _enableAllEggs = value;
                }
            }
        }

        private string _creatureEggs_RecipiesResource;
        public string CreatureEggs_RecipiesResource
        {
            get => _creatureEggs_RecipiesResource;
            set
            {
                if (string.Compare(_creatureEggs_RecipiesResource, value, true, CultureInfo.InvariantCulture) != 0)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(CreatureEggs_RecipiesResource), _creatureEggs_RecipiesResource, value);
#endif
                    _creatureEggs_RecipiesResource = value;
                }
            }
        }

        private int _creatureEggs_RecipiesResourceAmount;
        public int CreatureEggs_RecipiesResourceAmount
        {
            get => _creatureEggs_RecipiesResourceAmount;
            set
            {
                if (_creatureEggs_RecipiesResourceAmount != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(CreatureEggs_RecipiesResourceAmount), _creatureEggs_RecipiesResourceAmount, value);
#endif
                    _creatureEggs_RecipiesResourceAmount = value;
                }
            }
        }

        #endregion

        #region Drinks & food settings
        
        private bool _enableNutrientBlock;
        public bool EnableNutrientBlock
        {
            get => _enableNutrientBlock;
            set
            {
                if (_enableNutrientBlock != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(EnableNutrientBlock), _enableNutrientBlock, value);
#endif
                    _enableNutrientBlock = value;
                }
            }
        }

        private int _barBottle1Water;
        public int BarBottle1Water
        {
            get => _barBottle1Water;
            set
            {
                if (_barBottle1Water != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarBottle1Water), _barBottle1Water, value);
#endif
                    _barBottle1Water = value;
                }
            }
        }

        private int _barBottle2Water;
        public int BarBottle2Water
        {
            get => _barBottle2Water;
            set
            {
                if (_barBottle2Water != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarBottle2Water), _barBottle2Water, value);
#endif
                    _barBottle2Water = value;
                }
            }
        }

        private int _barBottle3Water;
        public int BarBottle3Water
        {
            get => _barBottle3Water;
            set
            {
                if (_barBottle3Water != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarBottle3Water), _barBottle3Water, value);
#endif
                    _barBottle3Water = value;
                }
            }
        }

        private int _barBottle4Water;
        public int BarBottle4Water
        {
            get => _barBottle4Water;
            set
            {
                if (_barBottle4Water != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarBottle4Water), _barBottle4Water, value);
#endif
                    _barBottle4Water = value;
                }
            }
        }

        private int _barBottle5Water;
        public int BarBottle5Water
        {
            get => _barBottle5Water;
            set
            {
                if (_barBottle5Water != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarBottle5Water), _barBottle5Water, value);
#endif
                    _barBottle5Water = value;
                }
            }
        }

        private int _barFood1Nutrient;
        public int BarFood1Nutrient
        {
            get => _barFood1Nutrient;
            set
            {
                if (_barFood1Nutrient != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarFood1Nutrient), _barFood1Nutrient, value);
#endif
                    _barFood1Nutrient = value;
                }
            }
        }

        private int _barFood1Water;
        public int BarFood1Water
        {
            get => _barFood1Water;
            set
            {
                if (_barFood1Water != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarFood1Water), _barFood1Water, value);
#endif
                    _barFood1Water = value;
                }
            }
        }


        private int _barFood2Nutrient;
        public int BarFood2Nutrient
        {
            get => _barFood2Nutrient;
            set
            {
                if (_barFood2Nutrient != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarFood2Nutrient), _barFood2Nutrient, value);
#endif
                    _barFood2Nutrient = value;
                }
            }
        }

        private int _barFood2Water;
        public int BarFood2Water
        {
            get => _barFood2Water;
            set
            {
                if (_barFood2Water != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(BarFood2Water), _barFood2Water, value);
#endif
                    _barFood2Water = value;
                }
            }
        }

        #endregion

        #region Flora settings

        private bool _addRegularAirSeeds;
        public bool AddRegularAirSeeds
        {
            get => _addRegularAirSeeds;
            set
            {
                if (_addRegularAirSeeds != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AddRegularAirSeeds), _addRegularAirSeeds, value);
#endif
                    _addRegularAirSeeds = value;
                }
            }
        }

        private bool _addRegularWaterSeeds;
        public bool AddRegularWaterSeeds
        {
            get => _addRegularWaterSeeds;
            set
            {
                if (_addRegularWaterSeeds != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AddRegularWaterSeeds), _addRegularWaterSeeds, value);
#endif
                    _addRegularWaterSeeds = value;
                }
            }
        }

        private string _flora_RecipiesResource;
        public string Flora_RecipiesResource
        {
            get => _flora_RecipiesResource;
            set
            {
                if (string.Compare(_flora_RecipiesResource, value, true, CultureInfo.InvariantCulture) != 0)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(Flora_RecipiesResource), _flora_RecipiesResource, value);
#endif
                    _flora_RecipiesResource = value;
                }
            }
        }

        private int _flora_RecipiesResourceAmount;
        public int Flora_RecipiesResourceAmount
        {
            get => _flora_RecipiesResourceAmount;
            set
            {
                if (_flora_RecipiesResourceAmount != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(Flora_RecipiesResourceAmount), _flora_RecipiesResourceAmount, value);
#endif
                    _flora_RecipiesResourceAmount = value;
                }
            }
        }

        private string _purplePineconeDroppedResource;
        public string PurplePineconeDroppedResource
        {
            get => _purplePineconeDroppedResource;
            set
            {
                if (string.Compare(_purplePineconeDroppedResource, value, true, CultureInfo.InvariantCulture) != 0)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(PurplePineconeDroppedResource), _purplePineconeDroppedResource, value);
#endif
                    _purplePineconeDroppedResource = value;
                }
            }
        }

        private int _purplePineconeDroppedResourceAmount;
        public int PurplePineconeDroppedResourceAmount
        {
            get => _purplePineconeDroppedResourceAmount;
            set
            {
                if (_purplePineconeDroppedResourceAmount != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(PurplePineconeDroppedResourceAmount), _purplePineconeDroppedResourceAmount, value);
#endif
                    _purplePineconeDroppedResourceAmount = value;
                }
            }
        }

        private FloraConfig _LandTree;
        public FloraConfig LandTree { get => _LandTree; set { if (_LandTree != value) {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(LandTree), _LandTree, value);
#endif
                    _LandTree = value; } } }
        private FloraConfig _JungleTreeA;
        public FloraConfig JungleTreeA { get => _JungleTreeA; set { if (_JungleTreeA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(JungleTreeA), _JungleTreeA, value);
#endif
			_JungleTreeA = value; } } }
        private FloraConfig _JungleTreeB;
        public FloraConfig JungleTreeB { get => _JungleTreeB; set { if (_JungleTreeB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(JungleTreeB), _JungleTreeB, value);
#endif
			_JungleTreeB = value; } } }
        private FloraConfig _TropicalTreeA;
        public FloraConfig TropicalTreeA { get => _TropicalTreeA; set { if (_TropicalTreeA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalTreeA), _TropicalTreeA, value);
#endif
			_TropicalTreeA = value; } } }
        private FloraConfig _TropicalTreeB;
        public FloraConfig TropicalTreeB { get => _TropicalTreeB; set { if (_TropicalTreeB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalTreeB), _TropicalTreeB, value);
#endif
			_TropicalTreeB = value; } } }
        private FloraConfig _TropicalTreeC;
        public FloraConfig TropicalTreeC { get => _TropicalTreeC; set { if (_TropicalTreeC != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalTreeC), _TropicalTreeC, value);
#endif
			_TropicalTreeC = value; } } }
        private FloraConfig _TropicalTreeD;
        public FloraConfig TropicalTreeD { get => _TropicalTreeD; set { if (_TropicalTreeD != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalTreeD), _TropicalTreeD, value);
#endif
			_TropicalTreeD = value; } } }
        private FloraConfig _LandPlantRedA;
        public FloraConfig LandPlantRedA { get => _LandPlantRedA; set { if (_LandPlantRedA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(LandPlantRedA), _LandPlantRedA, value);
#endif
			_LandPlantRedA = value; } } }
        private FloraConfig _LandPlantRedB;
        public FloraConfig LandPlantRedB { get => _LandPlantRedB; set { if (_LandPlantRedB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(LandPlantRedB), _LandPlantRedB, value);
#endif
			_LandPlantRedB = value; } } }
        private FloraConfig _LandPlantA;
        public FloraConfig LandPlantA { get => _LandPlantA; set { if (_LandPlantA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(LandPlantA), _LandPlantA, value);
#endif
			_LandPlantA = value; } } }
        private FloraConfig _LandPlantB;
        public FloraConfig LandPlantB { get => _LandPlantB; set { if (_LandPlantB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(LandPlantB), _LandPlantB, value);
#endif
			_LandPlantB = value; } } }
        private FloraConfig _LandPlantC;
        public FloraConfig LandPlantC { get => _LandPlantC; set { if (_LandPlantC != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(LandPlantC), _LandPlantC, value);
#endif
			_LandPlantC = value; } } }
        private FloraConfig _FernA;
        public FloraConfig FernA { get => _FernA; set { if (_FernA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(FernA), _FernA, value);
#endif
			_FernA = value; } } }
        private FloraConfig _FernB;
        public FloraConfig FernB { get => _FernB; set { if (_FernB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(FernB), _FernB, value);
#endif
			_FernB = value; } } }
        private FloraConfig _TropicalPlantA;
        public FloraConfig TropicalPlantA { get => _TropicalPlantA; set { if (_TropicalPlantA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantA), _TropicalPlantA, value);
#endif
			_TropicalPlantA = value; } } }
        private FloraConfig _TropicalPlantB;
        public FloraConfig TropicalPlantB { get => _TropicalPlantB; set { if (_TropicalPlantB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantB), _TropicalPlantB, value);
#endif
			_TropicalPlantB = value; } } }
        private FloraConfig _TropicalPlantC;
        public FloraConfig TropicalPlantC { get => _TropicalPlantC; set { if (_TropicalPlantC != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantC), _TropicalPlantC, value);
#endif
			_TropicalPlantC = value; } } }
        private FloraConfig _TropicalPlantD;
        public FloraConfig TropicalPlantD { get => _TropicalPlantD; set { if (_TropicalPlantD != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantD), _TropicalPlantD, value);
#endif
			_TropicalPlantD = value; } } }
        private FloraConfig _TropicalPlantE;
        public FloraConfig TropicalPlantE { get => _TropicalPlantE; set { if (_TropicalPlantE != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantE), _TropicalPlantE, value);
#endif
			_TropicalPlantE = value; } } }
        private FloraConfig _TropicalPlantF;
        public FloraConfig TropicalPlantF { get => _TropicalPlantF; set { if (_TropicalPlantF != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantF), _TropicalPlantF, value);
#endif
			_TropicalPlantF = value; } } }
        private FloraConfig _TropicalPlantG;
        public FloraConfig TropicalPlantG { get => _TropicalPlantG; set { if (_TropicalPlantG != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantG), _TropicalPlantG, value);
#endif
			_TropicalPlantG = value; } } }
        private FloraConfig _TropicalPlantH;
        public FloraConfig TropicalPlantH { get => _TropicalPlantH; set { if (_TropicalPlantH != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(TropicalPlantH), _TropicalPlantH, value);
#endif
			_TropicalPlantH = value; } } }
        private FloraConfig _CrabClawKelpA;
        public FloraConfig CrabClawKelpA { get => _CrabClawKelpA; set { if (_CrabClawKelpA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CrabClawKelpA), _CrabClawKelpA, value);
#endif
			_CrabClawKelpA = value; } } }
        private FloraConfig _CrabClawKelpB;
        public FloraConfig CrabClawKelpB { get => _CrabClawKelpB; set { if (_CrabClawKelpB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CrabClawKelpB), _CrabClawKelpB, value);
#endif
			_CrabClawKelpB = value; } } }
        private FloraConfig _CrabClawKelpC;
        public FloraConfig CrabClawKelpC { get => _CrabClawKelpC; set { if (_CrabClawKelpC != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CrabClawKelpC), _CrabClawKelpC, value);
#endif
			_CrabClawKelpC = value; } } }
        private FloraConfig _PyroCoralA;
        public FloraConfig PyroCoralA { get => _PyroCoralA; set { if (_PyroCoralA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(PyroCoralA), _PyroCoralA, value);
#endif
			_PyroCoralA = value; } } }
        private FloraConfig _PyroCoralB;
        public FloraConfig PyroCoralB { get => _PyroCoralB; set { if (_PyroCoralB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(PyroCoralB), _PyroCoralB, value);
#endif
			_PyroCoralB = value; } } }
        private FloraConfig _PyroCoralC;
        public FloraConfig PyroCoralC { get => _PyroCoralC; set { if (_PyroCoralC != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(PyroCoralC), _PyroCoralC, value);
#endif
			_PyroCoralC = value; } } }
        private FloraConfig _CoveTree;
        public FloraConfig CoveTree { get => _CoveTree; set { if (_CoveTree != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CoveTree), _CoveTree, value);
#endif
			_CoveTree = value; } } }
        private FloraConfig _GiantCoveTree;
        public FloraConfig GiantCoveTree { get => _GiantCoveTree; set { if (_GiantCoveTree != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(GiantCoveTree), _GiantCoveTree, value);
#endif
			_GiantCoveTree = value; } } }
        private FloraConfig _SpottedReedsA;
        public FloraConfig SpottedReedsA { get => _SpottedReedsA; set { if (_SpottedReedsA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(SpottedReedsA), _SpottedReedsA, value);
#endif
			_SpottedReedsA = value; } } }
        private FloraConfig _SpottedReedsB;
        public FloraConfig SpottedReedsB { get => _SpottedReedsB; set { if (_SpottedReedsB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(SpottedReedsB), _SpottedReedsB, value);
#endif
			_SpottedReedsB = value; } } }
        private FloraConfig _BrineLily;
        public FloraConfig BrineLily { get => _BrineLily; set { if (_BrineLily != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(BrineLily), _BrineLily, value);
#endif
			_BrineLily = value; } } }
        private FloraConfig _LostRiverPlant;
        public FloraConfig LostRiverPlant { get => _LostRiverPlant; set { if (_LostRiverPlant != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(LostRiverPlant), _LostRiverPlant, value);
#endif
			_LostRiverPlant = value; } } }
        private FloraConfig _CoralReefPlantMiddle;
        public FloraConfig CoralReefPlantMiddle { get => _CoralReefPlantMiddle; set { if (_CoralReefPlantMiddle != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CoralReefPlantMiddle), _CoralReefPlantMiddle, value);
#endif
			_CoralReefPlantMiddle = value; } } }
        private FloraConfig _SmallMushroomsDeco;
        public FloraConfig SmallMushroomsDeco { get => _SmallMushroomsDeco; set { if (_SmallMushroomsDeco != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(SmallMushroomsDeco), _SmallMushroomsDeco, value);
#endif
			_SmallMushroomsDeco = value; } } }
        private FloraConfig _FloatingStone;
        public FloraConfig FloatingStone { get => _FloatingStone; set { if (_FloatingStone != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(FloatingStone), _FloatingStone, value);
#endif
			_FloatingStone = value; } } }
        private FloraConfig _BrownCoralTubesA;
        public FloraConfig BrownCoralTubesA { get => _BrownCoralTubesA; set { if (_BrownCoralTubesA != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(BrownCoralTubesA), _BrownCoralTubesA, value);
#endif
			_BrownCoralTubesA = value; } } }
        private FloraConfig _BrownCoralTubesB;
        public FloraConfig BrownCoralTubesB { get => _BrownCoralTubesB; set { if (_BrownCoralTubesB != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(BrownCoralTubesB), _BrownCoralTubesB, value);
#endif
			_BrownCoralTubesB = value; } } }
        private FloraConfig _BrownCoralTubesC;
        public FloraConfig BrownCoralTubesC { get => _BrownCoralTubesC; set { if (_BrownCoralTubesC != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(BrownCoralTubesC), _BrownCoralTubesC, value);
#endif
			_BrownCoralTubesC = value; } } }
        private FloraConfig _BlueCoralTubes;
        public FloraConfig BlueCoralTubes { get => _BlueCoralTubes; set { if (_BlueCoralTubes != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(BlueCoralTubes), _BlueCoralTubes, value);
#endif
			_BlueCoralTubes = value; } } }
        private FloraConfig _PurplePinecone;
        public FloraConfig PurplePinecone { get => _PurplePinecone; set { if (_PurplePinecone != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(PurplePinecone), _PurplePinecone, value);
#endif
			_PurplePinecone = value; } } }
        private FloraConfig _CoralPlantYellow;
        public FloraConfig CoralPlantYellow { get => _CoralPlantYellow; set { if (_CoralPlantYellow != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CoralPlantYellow), _CoralPlantYellow, value);
#endif
			_CoralPlantYellow = value; } } }
        private FloraConfig _CoralPlantGreen;
        public FloraConfig CoralPlantGreen { get => _CoralPlantGreen; set { if (_CoralPlantGreen != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CoralPlantGreen), _CoralPlantGreen, value);
#endif
			_CoralPlantGreen = value; } } }
        private FloraConfig _CoralPlantBlue;
        public FloraConfig CoralPlantBlue { get => _CoralPlantBlue; set { if (_CoralPlantBlue != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CoralPlantBlue), _CoralPlantBlue, value);
#endif
			_CoralPlantBlue = value; } } }
        private FloraConfig _CoralPlantRed;
        public FloraConfig CoralPlantRed { get => _CoralPlantRed; set { if (_CoralPlantRed != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CoralPlantRed), _CoralPlantRed, value);
#endif
			_CoralPlantRed = value; } } }
        private FloraConfig _CoralPlantPurple;
        public FloraConfig CoralPlantPurple { get => _CoralPlantPurple; set { if (_CoralPlantPurple != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(CoralPlantPurple), _CoralPlantPurple, value);
#endif
			_CoralPlantPurple = value; } } }
        private FloraConfig _RedGrass1;
        public FloraConfig RedGrass1 { get => _RedGrass1; set { if (_RedGrass1 != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(RedGrass1), _RedGrass1, value);
#endif
			_RedGrass1 = value; } } }
        private FloraConfig _RedGrass2;
        public FloraConfig RedGrass2 { get => _RedGrass2; set { if (_RedGrass2 != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(RedGrass2), _RedGrass2, value);
#endif
			_RedGrass2 = value; } } }
        private FloraConfig _RedGrass3;
        public FloraConfig RedGrass3 { get => _RedGrass3; set { if (_RedGrass3 != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(RedGrass3), _RedGrass3, value);
#endif
			_RedGrass3 = value; } } }
        private FloraConfig _RedGrass2Tall;
        public FloraConfig RedGrass2Tall { get => _RedGrass2Tall; set { if (_RedGrass2Tall != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(RedGrass2Tall), _RedGrass2Tall, value);
#endif
			_RedGrass2Tall = value; } } }
        private FloraConfig _RedGrass3Tall;
        public FloraConfig RedGrass3Tall { get => _RedGrass3Tall; set { if (_RedGrass3Tall != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(RedGrass3Tall), _RedGrass3Tall, value);
#endif
			_RedGrass3Tall = value; } } }
        private FloraConfig _BloodGrass;
        public FloraConfig BloodGrass { get => _BloodGrass; set { if (_BloodGrass != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(BloodGrass), _BloodGrass, value);
#endif
			_BloodGrass = value; } } }
        private FloraConfig _BloodGrassDense;
        public FloraConfig BloodGrassDense { get => _BloodGrassDense; set { if (_BloodGrassDense != value) {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(BloodGrassDense), _BloodGrassDense, value);
#endif
			_BloodGrassDense = value; } } }

        private FloraConfig _MushroomTree1;
        public FloraConfig MushroomTree1
        {
            get => _MushroomTree1; set
            {
                if (_MushroomTree1 != value)
                {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(MushroomTree1), _MushroomTree1, value);
#endif
                    _MushroomTree1 = value;
                }
            }
        }
        private FloraConfig _MushroomTree2;
        public FloraConfig MushroomTree2
        {
            get => _MushroomTree2; set
            {
                if (_MushroomTree2 != value)
                {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(MushroomTree2), _MushroomTree2, value);
#endif
                    _MushroomTree2 = value;
                }
            }
        }

        private FloraConfig _MarbleMelonTiny;
        public FloraConfig MarbleMelonTiny
        {
            get => _MarbleMelonTiny; set
            {
                if (_MarbleMelonTiny != value)
                {
#if DEBUG_CONFIG_CHANGED
			Logger.Log(LOG_CONFIG_CHANGE, nameof(MarbleMelonTiny), _MarbleMelonTiny, value);
#endif
                    _MarbleMelonTiny = value;
                }
            }
        }

        #endregion

        #region Ghost leviathans settings

        private bool _GhostLeviatan_enable;
        public bool GhostLeviatan_enable
        {
            get => _GhostLeviatan_enable;
            set
            {
                if (_GhostLeviatan_enable != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(GhostLeviatan_enable), _GhostLeviatan_enable, value);
#endif
                    _GhostLeviatan_enable = value;
                }
            }
        }

        private int _GhostLeviatan_health;
        public int GhostLeviatan_health
        {
            get => _GhostLeviatan_health;
            set
            {
                if (_GhostLeviatan_health != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(GhostLeviatan_health), _GhostLeviatan_health, value);
#endif
                    _GhostLeviatan_health = value;
                }
            }
        }

        private int _GhostLeviatan_maxSpawns;
        public int GhostLeviatan_maxSpawns
        {
            get => _GhostLeviatan_maxSpawns;
            set
            {
                if (_GhostLeviatan_maxSpawns != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(GhostLeviatan_maxSpawns), _GhostLeviatan_maxSpawns, value);
#endif
                    _GhostLeviatan_maxSpawns = value;
                }
            }
        }

        private int _GhostLeviatan_timeBeforeFirstSpawn;
        public int GhostLeviatan_timeBeforeFirstSpawn
        {
            get => _GhostLeviatan_timeBeforeFirstSpawn;
            set
            {
                if (_GhostLeviatan_timeBeforeFirstSpawn != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(GhostLeviatan_timeBeforeFirstSpawn), _GhostLeviatan_timeBeforeFirstSpawn, value);
#endif
                    _GhostLeviatan_timeBeforeFirstSpawn = value;
                }
            }
        }

        private int _GhostLeviatan_spawnTimeRatio;
        public int GhostLeviatan_spawnTimeRatio
        {
            get => _GhostLeviatan_spawnTimeRatio;
            set
            {
                if (_GhostLeviatan_spawnTimeRatio != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(GhostLeviatan_spawnTimeRatio), _GhostLeviatan_spawnTimeRatio, value);
#endif
                    _GhostLeviatan_spawnTimeRatio = value;
                }
            }
        }

        public float GhostLeviatan_realSpawnTimeRatio
        {
            get { return ((float)GhostLeviatan_spawnTimeRatio * 0.01f); }
            private set { }
        }

        #endregion

        #region Extra settings

        private bool _useAlternativeScreenResolution;
        public bool UseAlternativeScreenResolution
        {
            get => _useAlternativeScreenResolution;
            set
            {
                if (_useAlternativeScreenResolution != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(UseAlternativeScreenResolution), _useAlternativeScreenResolution, value);
#endif
                    _useAlternativeScreenResolution = value;
                }
            }
        }

        private bool _hideDeepGrandReefDegasiBase;
        public bool HideDeepGrandReefDegasiBase
        {
            get => _hideDeepGrandReefDegasiBase;
            set
            {
                if (_hideDeepGrandReefDegasiBase != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(HideDeepGrandReefDegasiBase), _hideDeepGrandReefDegasiBase, value);
#endif
                    _hideDeepGrandReefDegasiBase = value;
                }
            }
        }

        private bool _asBuildable_SpecimenAnalyzer;
        public bool AsBuildable_SpecimenAnalyzer
        {
            get => _asBuildable_SpecimenAnalyzer;
            set
            {
                if (_asBuildable_SpecimenAnalyzer != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_SpecimenAnalyzer), _asBuildable_SpecimenAnalyzer, value);
#endif
                    _asBuildable_SpecimenAnalyzer = value;
                }
            }
        }
        
        private bool _asBuildable_MarkiplierDoll1;
        public bool AsBuildable_MarkiplierDoll1
        {
            get => _asBuildable_MarkiplierDoll1;
            set
            {
                if (_asBuildable_MarkiplierDoll1 != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_MarkiplierDoll1), _asBuildable_MarkiplierDoll1, value);
#endif
                    _asBuildable_MarkiplierDoll1 = value;
                }
            }
        }

        private bool _asBuildable_MarkiplierDoll2;
        public bool AsBuildable_MarkiplierDoll2
        {
            get => _asBuildable_MarkiplierDoll2;
            set
            {
                if (_asBuildable_MarkiplierDoll2 != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_MarkiplierDoll2), _asBuildable_MarkiplierDoll2, value);
#endif
                    _asBuildable_MarkiplierDoll2 = value;
                }
            }
        }

        private bool _asBuildable_JackSepticEyeDoll;
        public bool AsBuildable_JackSepticEyeDoll
        {
            get => _asBuildable_JackSepticEyeDoll;
            set
            {
                if (_asBuildable_JackSepticEyeDoll != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_JackSepticEyeDoll), _asBuildable_JackSepticEyeDoll, value);
#endif
                    _asBuildable_JackSepticEyeDoll = value;
                }
            }
        }

        private bool _asBuildable_EatMyDictionDoll;
        public bool AsBuildable_EatMyDictionDoll
        {
            get => _asBuildable_EatMyDictionDoll;
            set
            {
                if (_asBuildable_EatMyDictionDoll != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_EatMyDictionDoll), _asBuildable_EatMyDictionDoll, value);
#endif
                    _asBuildable_EatMyDictionDoll = value;
                }
            }
        }

        private bool _asBuildable_ForkliftToy;
        public bool AsBuildable_ForkliftToy
        {
            get => _asBuildable_ForkliftToy;
            set
            {
                if (_asBuildable_ForkliftToy != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_ForkliftToy), _asBuildable_ForkliftToy, value);
#endif
                    _asBuildable_ForkliftToy = value;
                }
            }
        }

        private bool _asBuildable_SofaSmall;
        public bool AsBuildable_SofaSmall
        {
            get => _asBuildable_SofaSmall;
            set
            {
                if (_asBuildable_SofaSmall != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_SofaSmall), _asBuildable_SofaSmall, value);
#endif
                    _asBuildable_SofaSmall = value;
                }
            }
        }

        private bool _asBuildable_SofaMedium;
        public bool AsBuildable_SofaMedium
        {
            get => _asBuildable_SofaMedium;
            set
            {
                if (_asBuildable_SofaMedium != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_SofaMedium), _asBuildable_SofaMedium, value);
#endif
                    _asBuildable_SofaMedium = value;
                }
            }
        }

        private bool _asBuildable_SofaBig;
        public bool AsBuildable_SofaBig
        {
            get => _asBuildable_SofaBig;
            set
            {
                if (_asBuildable_SofaBig != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_SofaBig), _asBuildable_SofaBig, value);
#endif
                    _asBuildable_SofaBig = value;
                }
            }
        }

        private bool _asBuildable_SofaCorner;
        public bool AsBuildable_SofaCorner
        {
            get => _asBuildable_SofaCorner;
            set
            {
                if (_asBuildable_SofaCorner != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_SofaCorner), _asBuildable_SofaCorner, value);
#endif
                    _asBuildable_SofaCorner = value;
                }
            }
        }

        private bool _asBuildable_LabCart;
        public bool AsBuildable_LabCart
        {
            get => _asBuildable_LabCart;
            set
            {
                if (_asBuildable_LabCart != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_LabCart), _asBuildable_LabCart, value);
#endif
                    _asBuildable_LabCart = value;
                }
            }
        }

        private bool _asBuildable_EmptyDesk;
        public bool AsBuildable_EmptyDesk
        {
            get => _asBuildable_EmptyDesk;
            set
            {
                if (_asBuildable_EmptyDesk != value)
                {
#if DEBUG_CONFIG_CHANGED
                    Logger.Log(LOG_CONFIG_CHANGE, nameof(AsBuildable_EmptyDesk), _asBuildable_EmptyDesk, value);
#endif
                    _asBuildable_EmptyDesk = value;
                }
            }
        }
        
        #endregion

        #region Constructor

        private Configuration()
        {
            // General settings
            this._language = GetDefaultLanguage();
            this._useCompactTooltips = false;
            this._lockQuickslotsWhenPlacingItem = false;
            this._allowBuildOutside = true;
            this._allowPlaceOutside = true;
            this._enablePlaceItems = true;
            this._enablePlaceMaterials = true;
            this._enablePlaceBatteries = true;
            this._enablePlaceEggs = true;
            this._enableNewFlora = true;
            this._fixAquariumLighting = true;
            this._enableAquariumGlassGlowing = false;

            // Habitat builder settings
            this._enableNewItems = true;
            this._enableSofas = true;
            this._enableDecorativeElectronics = true;
            this._enableCustomBaseParts = true;
            this._allowIndoorLongPlanterOutside = true;
            this._allowOutdoorLongPlanterInside = true;
            this._habitatBuilderItems = "AlienPillar1/AquariumSmall/BarStool/BenchMedium/BenchSmall/CargoBox01_damaged/CargoBox01a/CargoBox01b/CustomPictureFrame/CyclopsDoll/DecorationsEmptyDesk/DecorationsSpecimenAnalyzer/DecorativeControlTerminal/DecorativeLocker/DecorativeLockerClosed/DecorativeLockerDoor/DecorativeTechBox/MarlaCat/ExosuitDoll/ForkLiftDoll/JackSepticEyeDoll/LabCart/ALongPlanter/LongPlanterB/MarkiDoll1/MarkiDoll2/ReactorLamp/SeamothDoll/SofaCorner2/SofaStr1/SofaStr2/SofaStr3/WarperPart1/WorkDeskScreen1/WorkDeskScreen2/OutdoorLadder";

            // Discovery settings
            this._addItemsWhenDiscovered = false;
            this._addAirSeedsWhenDiscovered = false;
            this._addWaterSeedsWhenDiscovered = false;
            this._addEggsWhenCreatureScanned = false;
            this._addEggsAtStart = true;

            // Precursor settings
            this._enablePrecursorTab = true;
            this._precursorKeysAll = true;
            this._precursorKeys_RecipiesResource = "precursorioncrystal";
            this._precursorKeys_RecipiesResourceAmount = 1;
            this._relics_RecipiesResource = "precursorioncrystal";
            this._relics_RecipiesResourceAmount = 1;
            this._alienRelic1Animation = true;
            this._alienRelic2Animation = true;
            this._alienRelic3Animation = true;
            this._alienRelic4Animation = true;
            this._alienRelic5Animation = true;
            this._alienRelic6Animation = true;
            this._alienRelic7Animation = true;
            this._alienRelic8Animation = true;
            this._alienRelic9Animation = true;
            this._alienRelic10Animation = true;
            this._alienRelic11Animation = true;

            // Eggs settings
            this._enableAllEggs = true;
            this._creatureEggs_RecipiesResource = "salt";
            this._creatureEggs_RecipiesResourceAmount = 5;

            // Drinks & food settings
            this._enableNutrientBlock = true;
            this._barBottle1Water = 20;
            this._barBottle2Water = 20;
            this._barBottle3Water = 40;
            this._barBottle4Water = 40;
            this._barBottle5Water = 40;
            this._barFood1Nutrient = 40;
            this._barFood1Water = 10;
            this._barFood2Nutrient = 55;
            this._barFood2Water = 25;

            // Flora settings
            this._addRegularAirSeeds = true;
            this._addRegularWaterSeeds = true;
            this._flora_RecipiesResource = "salt";
            this._flora_RecipiesResourceAmount = 5;
            this._purplePineconeDroppedResource = "salt";
            this._purplePineconeDroppedResourceAmount = 1;
            this._LandTree = new FloraConfig(GetFriendlyWord("LandTree1Name"), "/Images/Flora/landtree1seedicon.png", 2400, 200, 500, true, 3, 6, false, 0.02f);
            this._JungleTreeA = new FloraConfig(GetFriendlyWord("JungleTree1Name"), "/Images/Flora/jungletree1icon.png", 2000, 120, 300, false, 1, 1, false, 0.02f);
            this._JungleTreeB = new FloraConfig(GetFriendlyWord("JungleTree2Name"), "/Images/Flora/jungletree2icon.png", 2000, 120, 300, false, 1, 1, false, 0.02f);
            this._TropicalTreeA = new FloraConfig(GetFriendlyWord("TropicalTreeName"), "/Images/Flora/tropicalplant3aicon.png", 1400, 100, 200, false, 1, 1, false, 0.02f);
            this._TropicalTreeB = new FloraConfig(GetFriendlyWord("TropicalTree2Name"), "/Images/Flora/tropicalplant3bicon.png", 1400, 100, 200, false, 1, 1, false, 0.02f);
            this._TropicalTreeC = new FloraConfig(GetFriendlyWord("TropicalTree3Name"), "/Images/Flora/tropicalplant6aicon.png", 1400, 100, 200, false, 1, 1, false, 0.02f);
            this._TropicalTreeD = new FloraConfig(GetFriendlyWord("TropicalTree4Name"), "/Images/Flora/tropicalplant6bicon.png", 1400, 100, 200, false, 1, 1, false, 0.02f);
            this._LandPlantRedA = new FloraConfig(GetFriendlyWord("LandPlant1Name"), "/Images/Flora/landplant1icon.png", 1200, 80, 100, false, 1, 1, false, 0.02f);
            this._LandPlantRedB = new FloraConfig(GetFriendlyWord("LandPlant2Name"), "/Images/Flora/landplant2icon.png", 1200, 80, 100, false, 1, 1, false, 0.02f);
            this._LandPlantA = new FloraConfig(GetFriendlyWord("LandPlant3Name"), "/Images/Flora/landplant3icon.png", 1200, 60, 70, false, 1, 1, false, 0.02f);
            this._LandPlantB = new FloraConfig(GetFriendlyWord("LandPlant4Name"), "/Images/Flora/landplant4icon.png", 1200, 60, 70, false, 1, 1, false, 0.02f);
            this._LandPlantC = new FloraConfig(GetFriendlyWord("LandPlant5Name"), "/Images/Flora/landplant5icon.png", 1200, 60, 70, false, 1, 1, false, 0.02f);
            this._FernA = new FloraConfig(GetFriendlyWord("FernName"), "/Images/Flora/fern2icon.png", 800, 60, 70, false, 1, 1, false, 0.02f);
            this._FernB = new FloraConfig(GetFriendlyWord("Fern2Name"), "/Images/Flora/fern4icon.png", 800, 60, 70, false, 1, 1, false, 0.02f);
            this._TropicalPlantA = new FloraConfig(GetFriendlyWord("TropicalPlantName"), "/Images/Flora/tropicalplant1aicon.png", 1200, 60, 140, false, 1, 1, false, 0.02f);
            this._TropicalPlantB = new FloraConfig(GetFriendlyWord("TropicalPlant2Name"), "/Images/Flora/tropicalplant1bicon.png", 1200, 60, 140, false, 1, 1, false, 0.02f);
            this._TropicalPlantC = new FloraConfig(GetFriendlyWord("TropicalPlant3Name"), "/Images/Flora/tropicalplant2aicon.png", 1200, 60, 100, false, 1, 1, false, 0.02f);
            this._TropicalPlantD = new FloraConfig(GetFriendlyWord("TropicalPlant4Name"), "/Images/Flora/tropicalplant2bicon.png", 1200, 60, 100, false, 1, 1, false, 0.02f);
            this._TropicalPlantE = new FloraConfig(GetFriendlyWord("TropicalPlant5Name"), "/Images/Flora/tropicalplant7aicon.png", 1200, 60, 100, false, 1, 1, false, 0.02f);
            this._TropicalPlantF = new FloraConfig(GetFriendlyWord("TropicalPlant6Name"), "/Images/Flora/tropicalplant7bicon.png", 1200, 60, 100, false, 1, 1, false, 0.02f);
            this._TropicalPlantG = new FloraConfig(GetFriendlyWord("TropicalPlant7Name"), "/Images/Flora/tropicalplant10aicon.png", 1200, 60, 100, false, 1, 1, false, 0.02f);
            this._TropicalPlantH = new FloraConfig(GetFriendlyWord("TropicalPlant8Name"), "/Images/Flora/tropicalplant10bicon.png", 1200, 60, 100, false, 1, 1, false, 0.02f);
            this._CrabClawKelpA = new FloraConfig(GetFriendlyWord("CrabClawKelpName") + " (1)", "/Images/Flora/lostriverplant2icon.png", 1600, 100, 220, false, 1, 1, false, 0.02f);
            this._CrabClawKelpB = new FloraConfig(GetFriendlyWord("CrabClawKelpName") + " (2)", "/Images/Flora/lostriverplant1icon.png", 1600, 100, 220, false, 1, 1, false, 0.02f);
            this._CrabClawKelpC = new FloraConfig(GetFriendlyWord("CrabClawKelpName") + " (3)", "/Images/Flora/lostriverplant3icon.png", 1600, 100, 220, false, 1, 1, false, 0.02f);
            this._PyroCoralA = new FloraConfig(GetFriendlyWord("PyroCoralName") + " (1)", "/Images/Flora/pyrocoral1icon.png", 2000, 130, 300, false, 1, 1, false, 0.02f);
            this._PyroCoralB = new FloraConfig(GetFriendlyWord("PyroCoralName") + " (2)", "/Images/Flora/pyrocoral2icon.png", 2000, 130, 300, false, 1, 1, false, 0.02f);
            this._PyroCoralC = new FloraConfig(GetFriendlyWord("PyroCoralName") + " (3)", "/Images/Flora/pyrocoral3icon.png", 2000, 130, 300, false, 1, 1, false, 0.02f);
            this._CoveTree = new FloraConfig(GetFriendlyWord("CoveTreeName"), "/Images/Flora/covetreeicon.png", 3000, 300, 400, false, 1, 1, false, 0.02f);
            this._GiantCoveTree = new FloraConfig(GetFriendlyWord("GiantCoveTreeName"), "/Images/Flora/covetree2icon.png", 5000, 500, 500, false, 1, 1, false, 0.02f);
            this._SpottedReedsA = new FloraConfig(GetFriendlyWord("GreenReedsName") + " (1)", "/Images/Flora/spottedreeds1icon.png", 1000, 60, 120, false, 1, 1, false, 0.02f);
            this._SpottedReedsB = new FloraConfig(GetFriendlyWord("GreenReedsName") + " (2)", "/Images/Flora/spottedreedsicon.png", 1000, 60, 120, false, 1, 1, false, 0.02f);
            this._BrineLily = new FloraConfig(GetFriendlyWord("BrineLilyName"), "/Images/Flora/lostriverplant4icon.png", 1400, 100, 120, false, 1, 1, false, 0.02f);
            this._LostRiverPlant = new FloraConfig(GetFriendlyWord("LostRiverPlantName"), "/Images/Flora/lostriverplant5icon.png", 1400, 100, 200, false, 1, 1, false, 0.02f);
            this._CoralReefPlantMiddle = new FloraConfig(GetFriendlyWord("PlantMiddle11Name"), "/Images/Flora/flora_plantmiddle11icon.png", 1000, 60, 70, false, 1, 1, false, 0.02f);
            this._SmallMushroomsDeco = new FloraConfig(GetFriendlyWord("SmallDeco3Name"), "/Images/Flora/flora_smalldeco03icon.png", 700, 10, 120, false, 1, 1, false, 0.02f);
            this._FloatingStone = new FloraConfig(GetFriendlyWord("FloatingStoneName"), "/Images/Flora/floatingstone1icon.png", 2000, 130, 160, false, 1, 1, false, 0.02f);
            this._BrownCoralTubesA = new FloraConfig(GetFriendlyWord("BrownCoralTubesName") + " (1)", "/Images/Flora/flora_browncoraltubes0203icon.png", 1400, 10, 50, false, 1, 1, false, 0.02f);
            this._BrownCoralTubesB = new FloraConfig(GetFriendlyWord("BrownCoralTubesName") + " (2)", "/Images/Flora/flora_browncoraltubes0201icon.png", 1600, 10, 70, false, 1, 1, false, 0.02f);
            this._BrownCoralTubesC = new FloraConfig(GetFriendlyWord("BrownCoralTubesName") + " (3)", "/Images/Flora/flora_browncoraltubes01icon.png", 1800, 60, 100, false, 1, 1, false, 0.02f);
            this._BlueCoralTubes = new FloraConfig(GetFriendlyWord("BlueCoralTubes1Name"), "/Images/Flora/flora_bluecoraltubesicon.png", 1600, 10, 140, false, 1, 1, false, 0.02f);
            this._PurplePinecone = new FloraConfig(GetFriendlyWord("SmallDeco10Name"), "/Images/Flora/flora_smalldeco10icon.png", 1800, 10, 160, false, 1, 1, false, 0.02f);
            this._CoralPlantYellow = new FloraConfig(GetFriendlyWord("SmallDeco11Name"), "/Images/Flora/flora_smalldeco11icon.png", 1600, 10, 120, false, 1, 1, false, 0.02f);
            this._CoralPlantGreen = new FloraConfig(GetFriendlyWord("SmallDeco13Name"), "/Images/Flora/flora_smalldeco13icon.png", 1600, 10, 120, false, 1, 1, false, 0.02f);
            this._CoralPlantBlue = new FloraConfig(GetFriendlyWord("SmallDeco14Name"), "/Images/Flora/flora_smalldeco14icon.png", 1600, 10, 120, false, 1, 1, false, 0.02f);
            this._CoralPlantRed = new FloraConfig(GetFriendlyWord("SmallDeco15RedName"), "/Images/Flora/flora_smalldeco15redicon.png", 1600, 10, 120, false, 1, 1, false, 0.02f);
            this._CoralPlantPurple = new FloraConfig(GetFriendlyWord("SmallDeco17PurpleName"), "/Images/Flora/flora_smalldeco17purpleicon.png", 1600, 10, 120, false, 1, 1, false, 0.02f);
            this._RedGrass1 = new FloraConfig(GetFriendlyWord("RedGrassName") + " (1)", "/Images/Flora/redgrasstinyicon.png", 800, 30, 30, false, 1, 1, false, 0.02f);
            this._RedGrass2 = new FloraConfig(GetFriendlyWord("RedGrassName") + " (2)", "/Images/Flora/redgrass1icon.png", 800, 40, 50, false, 1, 1, false, 0.02f);
            this._RedGrass3 = new FloraConfig(GetFriendlyWord("RedGrassName") + " (3)", "/Images/Flora/redgrass3icon.png", 800, 40, 50, false, 1, 1, false, 0.02f);
            this._RedGrass2Tall = new FloraConfig(GetFriendlyWord("RedGrassTallName") + " (1)", "/Images/Flora/redgrass2tallicon.png", 1000, 40, 70, false, 1, 1, false, 0.02f);
            this._RedGrass3Tall = new FloraConfig(GetFriendlyWord("RedGrassTallName") + " (2)", "/Images/Flora/redgrass3tallicon.png", 1000, 40, 70, false, 1, 1, false, 0.02f);
            this._BloodGrass = new FloraConfig(GetFriendlyWord("RedGrassDenseName") + " (1)", "/Images/Flora/bloodgrassicon.png", 1000, 40, 70, false, 1, 1, false, 0.02f);
            this._BloodGrassDense = new FloraConfig(GetFriendlyWord("RedGrassDenseName") + " (2)", "/Images/Flora/bloodgrassdense2icon.png", 1600, 60, 90, false, 1, 1, false, 0.02f);
            this._MushroomTree1 = new FloraConfig(GetFriendlyWord("MushroomTree1Name"), "/Images/Flora/mushroomtreeicon.png", 3000, 300, 140, false, 1, 1, false, 0.02f);
            this._MushroomTree2 = new FloraConfig(GetFriendlyWord("MushroomTree2Name"), "/Images/Flora/mushroomtree2icon.png", 1500, 100, 80, false, 1, 1, false, 0.02f);
            this._MarbleMelonTiny = new FloraConfig(GetFriendlyWord("MarbleMelonTinyFruitName"), "/Images/Flora/marblemelontinyicon.png", 800, 10, 280, true, 11, 7, true, 0.02f);

            // Ghost leviathans settings
            this._GhostLeviatan_enable = false;
            this._GhostLeviatan_health = 2000;
            this._GhostLeviatan_maxSpawns = 2;
            this._GhostLeviatan_timeBeforeFirstSpawn = 1200;
            this._GhostLeviatan_spawnTimeRatio = 100;

            // Extra settings
            this._useAlternativeScreenResolution = false;
            this._hideDeepGrandReefDegasiBase = false;
            this._asBuildable_SpecimenAnalyzer = true;
            this._asBuildable_MarkiplierDoll1 = true;
            this._asBuildable_MarkiplierDoll2 = true;
            this._asBuildable_JackSepticEyeDoll = true;
            this._asBuildable_EatMyDictionDoll = true;
            this._asBuildable_ForkliftToy = true;
            this._asBuildable_SofaSmall = true;
            this._asBuildable_SofaMedium = true;
            this._asBuildable_SofaBig = true;
            this._asBuildable_SofaCorner = true;
            this._asBuildable_LabCart = true;
            this._asBuildable_EmptyDesk = true;
        }

        #endregion

        #region Singleton instances

        // Singleton (live version)
        private static Configuration _instance = null;
        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    string configPath = GetConfigFilePath();
                    // If Config.txt exists
                    if (File.Exists(configPath))
                        _instance = LoadConfiguration(configPath);
                    else
                        Logger.Log("ERROR: Config file not found at [{0}].", configPath);
                }
                return _instance;
            }
        }

        // Singleton (original version)
        private static Configuration _instanceOrigin = null;
        public static Configuration InstanceOrigin
        {
            get
            {
                if (_instanceOrigin == null)
                {
                    string configPath = GetConfigFilePath();
                    // If Config.txt exists
                    if (File.Exists(configPath))
                        _instanceOrigin = LoadConfiguration(configPath);
                    else
                        Logger.Log("ERROR: Config file not found at [{0}].", configPath);
                }
                return _instanceOrigin;
            }
        }

        #endregion

        #region Load configuration functions

        public static void RefreshInstancePlantNames()
        {
            Configuration.Instance.LandTree.PlantName = LanguageHelper.GetFriendlyWord("LandTree1Name");
            Configuration.Instance.JungleTreeA.PlantName = LanguageHelper.GetFriendlyWord("JungleTree1Name");
            Configuration.Instance.JungleTreeB.PlantName = LanguageHelper.GetFriendlyWord("JungleTree2Name");
            Configuration.Instance.TropicalTreeA.PlantName = LanguageHelper.GetFriendlyWord("TropicalTreeName");
            Configuration.Instance.TropicalTreeB.PlantName = LanguageHelper.GetFriendlyWord("TropicalTree2Name");
            Configuration.Instance.TropicalTreeC.PlantName = LanguageHelper.GetFriendlyWord("TropicalTree3Name");
            Configuration.Instance.TropicalTreeD.PlantName = LanguageHelper.GetFriendlyWord("TropicalTree4Name");
            Configuration.Instance.LandPlantRedA.PlantName = LanguageHelper.GetFriendlyWord("LandPlant1Name");
            Configuration.Instance.LandPlantRedB.PlantName = LanguageHelper.GetFriendlyWord("LandPlant2Name");
            Configuration.Instance.LandPlantA.PlantName = LanguageHelper.GetFriendlyWord("LandPlant3Name");
            Configuration.Instance.LandPlantB.PlantName = LanguageHelper.GetFriendlyWord("LandPlant4Name");
            Configuration.Instance.LandPlantC.PlantName = LanguageHelper.GetFriendlyWord("LandPlant5Name");
            Configuration.Instance.FernA.PlantName = LanguageHelper.GetFriendlyWord("FernName");
            Configuration.Instance.FernB.PlantName = LanguageHelper.GetFriendlyWord("Fern2Name");
            Configuration.Instance.TropicalPlantA.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlantName");
            Configuration.Instance.TropicalPlantB.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlant2Name");
            Configuration.Instance.TropicalPlantC.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlant3Name");
            Configuration.Instance.TropicalPlantD.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlant4Name");
            Configuration.Instance.TropicalPlantE.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlant5Name");
            Configuration.Instance.TropicalPlantF.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlant6Name");
            Configuration.Instance.TropicalPlantG.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlant7Name");
            Configuration.Instance.TropicalPlantH.PlantName = LanguageHelper.GetFriendlyWord("TropicalPlant8Name");
            Configuration.Instance.CrabClawKelpA.PlantName = LanguageHelper.GetFriendlyWord("CrabClawKelpName") + " (1)";
            Configuration.Instance.CrabClawKelpB.PlantName = LanguageHelper.GetFriendlyWord("CrabClawKelpName") + " (2)";
            Configuration.Instance.CrabClawKelpC.PlantName = LanguageHelper.GetFriendlyWord("CrabClawKelpName") + " (3)";
            Configuration.Instance.PyroCoralA.PlantName = LanguageHelper.GetFriendlyWord("PyroCoralName") + " (1)";
            Configuration.Instance.PyroCoralB.PlantName = LanguageHelper.GetFriendlyWord("PyroCoralName") + " (2)";
            Configuration.Instance.PyroCoralC.PlantName = LanguageHelper.GetFriendlyWord("PyroCoralName") + " (3)";
            Configuration.Instance.CoveTree.PlantName = LanguageHelper.GetFriendlyWord("CoveTreeName");
            Configuration.Instance.GiantCoveTree.PlantName = LanguageHelper.GetFriendlyWord("GiantCoveTreeName");
            Configuration.Instance.SpottedReedsA.PlantName = LanguageHelper.GetFriendlyWord("GreenReedsName") + " (1)";
            Configuration.Instance.SpottedReedsB.PlantName = LanguageHelper.GetFriendlyWord("GreenReedsName") + " (2)";
            Configuration.Instance.BrineLily.PlantName = LanguageHelper.GetFriendlyWord("BrineLilyName");
            Configuration.Instance.LostRiverPlant.PlantName = LanguageHelper.GetFriendlyWord("LostRiverPlantName");
            Configuration.Instance.CoralReefPlantMiddle.PlantName = LanguageHelper.GetFriendlyWord("PlantMiddle11Name");
            Configuration.Instance.SmallMushroomsDeco.PlantName = LanguageHelper.GetFriendlyWord("SmallDeco3Name");
            Configuration.Instance.FloatingStone.PlantName = LanguageHelper.GetFriendlyWord("FloatingStoneName");
            Configuration.Instance.BrownCoralTubesA.PlantName = LanguageHelper.GetFriendlyWord("BrownCoralTubesName") + " (1)";
            Configuration.Instance.BrownCoralTubesB.PlantName = LanguageHelper.GetFriendlyWord("BrownCoralTubesName") + " (2)";
            Configuration.Instance.BrownCoralTubesC.PlantName = LanguageHelper.GetFriendlyWord("BrownCoralTubesName") + " (3)";
            Configuration.Instance.BlueCoralTubes.PlantName = LanguageHelper.GetFriendlyWord("BlueCoralTubes1Name");
            Configuration.Instance.PurplePinecone.PlantName = LanguageHelper.GetFriendlyWord("SmallDeco10Name");
            Configuration.Instance.CoralPlantYellow.PlantName = LanguageHelper.GetFriendlyWord("SmallDeco11Name");
            Configuration.Instance.CoralPlantGreen.PlantName = LanguageHelper.GetFriendlyWord("SmallDeco13Name");
            Configuration.Instance.CoralPlantBlue.PlantName = LanguageHelper.GetFriendlyWord("SmallDeco14Name");
            Configuration.Instance.CoralPlantRed.PlantName = LanguageHelper.GetFriendlyWord("SmallDeco15RedName");
            Configuration.Instance.CoralPlantPurple.PlantName = LanguageHelper.GetFriendlyWord("SmallDeco17PurpleName");
            Configuration.Instance.RedGrass1.PlantName = LanguageHelper.GetFriendlyWord("RedGrassName") + " (1)";
            Configuration.Instance.RedGrass2.PlantName = LanguageHelper.GetFriendlyWord("RedGrassName") + " (2)";
            Configuration.Instance.RedGrass3.PlantName = LanguageHelper.GetFriendlyWord("RedGrassName") + " (3)";
            Configuration.Instance.RedGrass2Tall.PlantName = LanguageHelper.GetFriendlyWord("RedGrassTallName") + " (1)";
            Configuration.Instance.RedGrass3Tall.PlantName = LanguageHelper.GetFriendlyWord("RedGrassTallName") + " (2)";
            Configuration.Instance.BloodGrass.PlantName = LanguageHelper.GetFriendlyWord("RedGrassDenseName") + " (1)";
            Configuration.Instance.BloodGrassDense.PlantName = LanguageHelper.GetFriendlyWord("RedGrassDenseName") + " (2)";
            Configuration.Instance.MushroomTree1.PlantName = LanguageHelper.GetFriendlyWord("MushroomTree1Name");
            Configuration.Instance.MushroomTree2.PlantName = LanguageHelper.GetFriendlyWord("MushroomTree2Name");
            Configuration.Instance.MarbleMelonTiny.PlantName = LanguageHelper.GetFriendlyWord("MarbleMelonTinyFruitName");
        }

        public static string GetConfigFilePath()
        {
            // Get path to Configurator.exe directory
            string configuratorFolder = Path.GetDirectoryName(Uri.UnescapeDataString(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));
            // Get path to Config.txt file (it's in Configurator.exe parent directory)
            return Uri.UnescapeDataString(new Uri(Path.Combine(configuratorFolder, "..\\Config.txt")).AbsolutePath);
        }

        public static FloraConfig LoadFloraConfig(string configStr, string alias, string image)
        {
            FloraConfig floraConfig = new FloraConfig();
            if (!string.IsNullOrWhiteSpace(configStr))
            {
                string[] elems = configStr.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (elems != null && elems.Length >= 3)
                {
                    if (int.TryParse(elems[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int growthDuration) && growthDuration >= 100 && growthDuration <= 10000)
                        floraConfig.GrowthDuration = growthDuration;
                    if (int.TryParse(elems[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int healthPoints) && healthPoints >= 10 && healthPoints <= 2000)
                        floraConfig.HealthPoints = healthPoints;
                    if (int.TryParse(elems[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int bioreactorCharge) && bioreactorCharge >= 10 && bioreactorCharge <= 2000)
                        floraConfig.BioreactorCharge = bioreactorCharge;
                    if (elems.Length >= 8)
                    {
                        if (elems[3].IndexOf("true", StringComparison.InvariantCultureIgnoreCase) >= 0)
                            floraConfig.CanEat = true;
                        else
                            floraConfig.CanEat = false;
                        if (int.TryParse(elems[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out int nutrientsAmount) && nutrientsAmount >= 1 && nutrientsAmount <= 100)
                            floraConfig.NutrientsAmount = nutrientsAmount;
                        if (int.TryParse(elems[5], NumberStyles.Integer, CultureInfo.InvariantCulture, out int waterAmount) && waterAmount >= 1 && waterAmount <= 100)
                            floraConfig.WaterAmount = waterAmount;
                        if (elems[6].IndexOf("true", StringComparison.InvariantCultureIgnoreCase) >= 0)
                            floraConfig.Decomposes = true;
                        else
                            floraConfig.Decomposes = false;
                        if (int.TryParse(elems[7], NumberStyles.Integer, CultureInfo.InvariantCulture, out int kDecayRate))
                            floraConfig.DecompositionSpeed = kDecayRate;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(alias))
                floraConfig.PlantName = alias;
            if (!string.IsNullOrWhiteSpace(image))
                floraConfig.PlantImagePath = image;
            return floraConfig;
        }

        private static Configuration LoadConfiguration(string configFilePath)
        {
            Configuration origConfig = new Configuration();
            // Iterate each lines of the Config.txt file
            string[] origConfigLines = File.ReadAllLines(configFilePath, Encoding.UTF8);
            foreach (string line in origConfigLines)
            {
                if (line.StartsWith("language="))
                    origConfig._language = line.Substring("language=".Length);
                else if (line.StartsWith("useCompactTooltips="))
                    origConfig._useCompactTooltips = string.Compare(line.Substring("useCompactTooltips=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("lockQuickslotsWhenPlacingItem="))
                    origConfig._lockQuickslotsWhenPlacingItem = string.Compare(line.Substring("lockQuickslotsWhenPlacingItem=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("allowBuildOutside="))
                    origConfig._allowBuildOutside = !(string.Compare(line.Substring("allowBuildOutside=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("allowPlaceOutside="))
                    origConfig._allowPlaceOutside = !(string.Compare(line.Substring("allowPlaceOutside=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enablePlaceItems="))
                    origConfig._enablePlaceItems = !(string.Compare(line.Substring("enablePlaceItems=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enablePlaceMaterials="))
                    origConfig._enablePlaceMaterials = !(string.Compare(line.Substring("enablePlaceMaterials=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enablePlaceBatteries="))
                    origConfig._enablePlaceBatteries = string.Compare(line.Substring("enablePlaceBatteries=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("enablePlaceEggs="))
                    origConfig._enablePlaceEggs = string.Compare(line.Substring("enablePlaceEggs=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("enableNewFlora="))
                    origConfig._enableNewFlora = !(string.Compare(line.Substring("enableNewFlora=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enableNewItems="))
                    origConfig._enableNewItems = !(string.Compare(line.Substring("enableNewItems=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enableSofas="))
                    origConfig._enableSofas = !(string.Compare(line.Substring("enableSofas=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enableDecorativeElectronics="))
                    origConfig._enableDecorativeElectronics = !(string.Compare(line.Substring("enableDecorativeElectronics=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enableCustomBaseParts="))
                    origConfig._enableCustomBaseParts = !(string.Compare(line.Substring("enableCustomBaseParts=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("habitatBuilderItems="))
                    origConfig._habitatBuilderItems = line.Substring("habitatBuilderItems=".Length);
                else if (line.StartsWith("enableNutrientBlock="))
                    origConfig._enableNutrientBlock = !(string.Compare(line.Substring("enableNutrientBlock=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("allowIndoorLongPlanterOutside="))
                    origConfig._allowIndoorLongPlanterOutside = !(string.Compare(line.Substring("allowIndoorLongPlanterOutside=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("allowOutdoorLongPlanterInside="))
                    origConfig._allowOutdoorLongPlanterInside = !(string.Compare(line.Substring("allowOutdoorLongPlanterInside=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("fixAquariumLighting="))
                    origConfig._fixAquariumLighting = !(string.Compare(line.Substring("fixAquariumLighting=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enableAquariumGlassGlowing="))
                    origConfig._enableAquariumGlassGlowing = string.Compare(line.Substring("enableAquariumGlassGlowing=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("addItemsWhenDiscovered="))
                    origConfig._addItemsWhenDiscovered = string.Compare(line.Substring("addItemsWhenDiscovered=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("addAirSeedsWhenDiscovered="))
                    origConfig._addAirSeedsWhenDiscovered = string.Compare(line.Substring("addAirSeedsWhenDiscovered=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("addWaterSeedsWhenDiscovered="))
                    origConfig._addWaterSeedsWhenDiscovered = string.Compare(line.Substring("addWaterSeedsWhenDiscovered=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("addEggsWhenCreatureScanned="))
                    origConfig._addEggsWhenCreatureScanned = string.Compare(line.Substring("addEggsWhenCreatureScanned=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("addEggsAtStart="))
                    origConfig._addEggsAtStart = string.Compare(line.Substring("addEggsAtStart=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("enablePrecursorTab="))
                    origConfig._enablePrecursorTab = !(string.Compare(line.Substring("enablePrecursorTab=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("precursorKeysAll="))
                    origConfig._precursorKeysAll = !(string.Compare(line.Substring("precursorKeysAll=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("precursorKeys_RecipiesResource="))
                    origConfig._precursorKeys_RecipiesResource = line.Substring("precursorKeys_RecipiesResource=".Length);
                else if (line.StartsWith("precursorKeys_RecipiesResourceAmount="))
                    origConfig._precursorKeys_RecipiesResourceAmount = (int.TryParse(line.Substring("precursorKeys_RecipiesResourceAmount=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 1);
                else if (line.StartsWith("relics_RecipiesResource="))
                    origConfig._relics_RecipiesResource = line.Substring("relics_RecipiesResource=".Length);
                else if (line.StartsWith("relics_RecipiesResourceAmount="))
                    origConfig._relics_RecipiesResourceAmount = (int.TryParse(line.Substring("relics_RecipiesResourceAmount=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 1);
                else if (line.StartsWith("alienRelic1Animation="))
                    origConfig._alienRelic1Animation = !(string.Compare(line.Substring("alienRelic1Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic2Animation="))
                    origConfig._alienRelic2Animation = !(string.Compare(line.Substring("alienRelic2Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic3Animation="))
                    origConfig._alienRelic3Animation = !(string.Compare(line.Substring("alienRelic3Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic4Animation="))
                    origConfig._alienRelic4Animation = !(string.Compare(line.Substring("alienRelic4Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic5Animation="))
                    origConfig._alienRelic5Animation = !(string.Compare(line.Substring("alienRelic5Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic6Animation="))
                    origConfig._alienRelic6Animation = !(string.Compare(line.Substring("alienRelic6Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic7Animation="))
                    origConfig._alienRelic7Animation = !(string.Compare(line.Substring("alienRelic7Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic8Animation="))
                    origConfig._alienRelic8Animation = !(string.Compare(line.Substring("alienRelic8Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic9Animation="))
                    origConfig._alienRelic9Animation = !(string.Compare(line.Substring("alienRelic9Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic10Animation="))
                    origConfig._alienRelic10Animation = !(string.Compare(line.Substring("alienRelic10Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("alienRelic11Animation="))
                    origConfig._alienRelic11Animation = !(string.Compare(line.Substring("alienRelic11Animation=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("enableAllEggs="))
                    origConfig._enableAllEggs = !(string.Compare(line.Substring("enableAllEggs=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("creatureEggs_RecipiesResource="))
                    origConfig._creatureEggs_RecipiesResource = line.Substring("creatureEggs_RecipiesResource=".Length);
                else if (line.StartsWith("creatureEggs_RecipiesResourceAmount="))
                    origConfig._creatureEggs_RecipiesResourceAmount = (int.TryParse(line.Substring("creatureEggs_RecipiesResourceAmount=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 5);
                else if (line.StartsWith("barBottle1Water="))
                    origConfig._barBottle1Water = (int.TryParse(line.Substring("barBottle1Water=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 20);
                else if (line.StartsWith("barBottle2Water="))
                    origConfig._barBottle2Water = (int.TryParse(line.Substring("barBottle2Water=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 20);
                else if (line.StartsWith("barBottle3Water="))
                    origConfig._barBottle3Water = (int.TryParse(line.Substring("barBottle3Water=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 40);
                else if (line.StartsWith("barBottle4Water="))
                    origConfig._barBottle4Water = (int.TryParse(line.Substring("barBottle4Water=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 40);
                else if (line.StartsWith("barBottle5Water="))
                    origConfig._barBottle5Water = (int.TryParse(line.Substring("barBottle5Water=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 40);
                else if (line.StartsWith("barFood1Nutrient="))
                    origConfig._barFood1Nutrient = (int.TryParse(line.Substring("barFood1Nutrient=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 40);
                else if (line.StartsWith("barFood1Water="))
                    origConfig._barFood1Water = (int.TryParse(line.Substring("barFood1Water=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 10);
                else if (line.StartsWith("barFood2Nutrient="))
                    origConfig._barFood2Nutrient = (int.TryParse(line.Substring("barFood2Nutrient=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 55);
                else if (line.StartsWith("barFood2Water="))
                    origConfig._barFood2Water = (int.TryParse(line.Substring("barFood2Water=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 25);
                else if (line.StartsWith("addRegularAirSeeds="))
                    origConfig._addRegularAirSeeds = !(string.Compare(line.Substring("addRegularAirSeeds=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("addRegularWaterSeeds="))
                    origConfig._addRegularWaterSeeds = !(string.Compare(line.Substring("addRegularWaterSeeds=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("flora_RecipiesResource="))
                    origConfig._flora_RecipiesResource = line.Substring("flora_RecipiesResource=".Length);
                else if (line.StartsWith("flora_RecipiesResourceAmount="))
                    origConfig._flora_RecipiesResourceAmount = (int.TryParse(line.Substring("flora_RecipiesResourceAmount=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 5);
                else if (line.StartsWith("purplePineconeDroppedResource="))
                    origConfig._purplePineconeDroppedResource = line.Substring("purplePineconeDroppedResource=".Length);
                else if (line.StartsWith("purplePineconeDroppedResourceAmount="))
                    origConfig._purplePineconeDroppedResourceAmount = (int.TryParse(line.Substring("purplePineconeDroppedResourceAmount=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 1);
                else if (line.StartsWith("config_LandTree="))
                    origConfig._LandTree = LoadFloraConfig(line.Substring("config_LandTree=".Length), LanguageHelper.GetFriendlyWord("LandTree1Name"), "/Images/Flora/landtree1seedicon.png");
                else if (line.StartsWith("config_JungleTreeA="))
                    origConfig._JungleTreeA = LoadFloraConfig(line.Substring("config_JungleTreeA=".Length), LanguageHelper.GetFriendlyWord("JungleTree1Name"), "/Images/Flora/jungletree1icon.png");
                else if (line.StartsWith("config_JungleTreeB="))
                    origConfig._JungleTreeB = LoadFloraConfig(line.Substring("config_JungleTreeB=".Length), LanguageHelper.GetFriendlyWord("JungleTree2Name"), "/Images/Flora/jungletree2icon.png");
                else if (line.StartsWith("config_TropicalTreeA="))
                    origConfig._TropicalTreeA = LoadFloraConfig(line.Substring("config_TropicalTreeA=".Length), LanguageHelper.GetFriendlyWord("TropicalTreeName"), "/Images/Flora/tropicalplant3aicon.png");
                else if (line.StartsWith("config_TropicalTreeB="))
                    origConfig._TropicalTreeB = LoadFloraConfig(line.Substring("config_TropicalTreeB=".Length), LanguageHelper.GetFriendlyWord("TropicalTree2Name"), "/Images/Flora/tropicalplant3bicon.png");
                else if (line.StartsWith("config_TropicalTreeC="))
                    origConfig._TropicalTreeC = LoadFloraConfig(line.Substring("config_TropicalTreeC=".Length), LanguageHelper.GetFriendlyWord("TropicalTree3Name"), "/Images/Flora/tropicalplant6aicon.png");
                else if (line.StartsWith("config_TropicalTreeD="))
                    origConfig._TropicalTreeD = LoadFloraConfig(line.Substring("config_TropicalTreeD=".Length), LanguageHelper.GetFriendlyWord("TropicalTree4Name"), "/Images/Flora/tropicalplant6bicon.png");
                else if (line.StartsWith("config_LandPlantRedA="))
                    origConfig._LandPlantRedA = LoadFloraConfig(line.Substring("config_LandPlantRedA=".Length), LanguageHelper.GetFriendlyWord("LandPlant1Name"), "/Images/Flora/landplant1icon.png");
                else if (line.StartsWith("config_LandPlantRedB="))
                    origConfig._LandPlantRedB = LoadFloraConfig(line.Substring("config_LandPlantRedB=".Length), LanguageHelper.GetFriendlyWord("LandPlant2Name"), "/Images/Flora/landplant2icon.png");
                else if (line.StartsWith("config_LandPlantA="))
                    origConfig._LandPlantA = LoadFloraConfig(line.Substring("config_LandPlantA=".Length), LanguageHelper.GetFriendlyWord("LandPlant3Name"), "/Images/Flora/landplant3icon.png");
                else if (line.StartsWith("config_LandPlantB="))
                    origConfig._LandPlantB = LoadFloraConfig(line.Substring("config_LandPlantB=".Length), LanguageHelper.GetFriendlyWord("LandPlant4Name"), "/Images/Flora/landplant4icon.png");
                else if (line.StartsWith("config_LandPlantC="))
                    origConfig._LandPlantC = LoadFloraConfig(line.Substring("config_LandPlantC=".Length), LanguageHelper.GetFriendlyWord("LandPlant5Name"), "/Images/Flora/landplant5icon.png");
                else if (line.StartsWith("config_FernA="))
                    origConfig._FernA = LoadFloraConfig(line.Substring("config_FernA=".Length), LanguageHelper.GetFriendlyWord("FernName"), "/Images/Flora/fern2icon.png");
                else if (line.StartsWith("config_FernB="))
                    origConfig._FernB = LoadFloraConfig(line.Substring("config_FernB=".Length), LanguageHelper.GetFriendlyWord("Fern2Name"), "/Images/Flora/fern4icon.png");
                else if (line.StartsWith("config_TropicalPlantA="))
                    origConfig._TropicalPlantA = LoadFloraConfig(line.Substring("config_TropicalPlantA=".Length), LanguageHelper.GetFriendlyWord("TropicalPlantName"), "/Images/Flora/tropicalplant1aicon.png");
                else if (line.StartsWith("config_TropicalPlantB="))
                    origConfig._TropicalPlantB = LoadFloraConfig(line.Substring("config_TropicalPlantB=".Length), LanguageHelper.GetFriendlyWord("TropicalPlant2Name"), "/Images/Flora/tropicalplant1bicon.png");
                else if (line.StartsWith("config_TropicalPlantC="))
                    origConfig._TropicalPlantC = LoadFloraConfig(line.Substring("config_TropicalPlantC=".Length), LanguageHelper.GetFriendlyWord("TropicalPlant3Name"), "/Images/Flora/tropicalplant2aicon.png");
                else if (line.StartsWith("config_TropicalPlantD="))
                    origConfig._TropicalPlantD = LoadFloraConfig(line.Substring("config_TropicalPlantD=".Length), LanguageHelper.GetFriendlyWord("TropicalPlant4Name"), "/Images/Flora/tropicalplant2bicon.png");
                else if (line.StartsWith("config_TropicalPlantE="))
                    origConfig._TropicalPlantE = LoadFloraConfig(line.Substring("config_TropicalPlantE=".Length), LanguageHelper.GetFriendlyWord("TropicalPlant5Name"), "/Images/Flora/tropicalplant7aicon.png");
                else if (line.StartsWith("config_TropicalPlantF="))
                    origConfig._TropicalPlantF = LoadFloraConfig(line.Substring("config_TropicalPlantF=".Length), LanguageHelper.GetFriendlyWord("TropicalPlant6Name"), "/Images/Flora/tropicalplant7bicon.png");
                else if (line.StartsWith("config_TropicalPlantG="))
                    origConfig._TropicalPlantG = LoadFloraConfig(line.Substring("config_TropicalPlantG=".Length), LanguageHelper.GetFriendlyWord("TropicalPlant7Name"), "/Images/Flora/tropicalplant10aicon.png");
                else if (line.StartsWith("config_TropicalPlantH="))
                    origConfig._TropicalPlantH = LoadFloraConfig(line.Substring("config_TropicalPlantH=".Length), LanguageHelper.GetFriendlyWord("TropicalPlant8Name"), "/Images/Flora/tropicalplant10bicon.png");
                else if (line.StartsWith("config_CrabClawKelpA="))
                    origConfig._CrabClawKelpA = LoadFloraConfig(line.Substring("config_CrabClawKelpA=".Length), LanguageHelper.GetFriendlyWord("CrabClawKelpName") + " (1)", "/Images/Flora/lostriverplant2icon.png");
                else if (line.StartsWith("config_CrabClawKelpB="))
                    origConfig._CrabClawKelpB = LoadFloraConfig(line.Substring("config_CrabClawKelpB=".Length), LanguageHelper.GetFriendlyWord("CrabClawKelpName") + " (2)", "/Images/Flora/lostriverplant1icon.png");
                else if (line.StartsWith("config_CrabClawKelpC="))
                    origConfig._CrabClawKelpC = LoadFloraConfig(line.Substring("config_CrabClawKelpC=".Length), LanguageHelper.GetFriendlyWord("CrabClawKelpName") + " (3)", "/Images/Flora/lostriverplant3icon.png");
                else if (line.StartsWith("config_PyroCoralA="))
                    origConfig._PyroCoralA = LoadFloraConfig(line.Substring("config_PyroCoralA=".Length), LanguageHelper.GetFriendlyWord("PyroCoralName") + " (1)", "/Images/Flora/pyrocoral1icon.png");
                else if (line.StartsWith("config_PyroCoralB="))
                    origConfig._PyroCoralB = LoadFloraConfig(line.Substring("config_PyroCoralB=".Length), LanguageHelper.GetFriendlyWord("PyroCoralName") + " (2)", "/Images/Flora/pyrocoral2icon.png");
                else if (line.StartsWith("config_PyroCoralC="))
                    origConfig._PyroCoralC = LoadFloraConfig(line.Substring("config_PyroCoralC=".Length), LanguageHelper.GetFriendlyWord("PyroCoralName") + " (3)", "/Images/Flora/pyrocoral3icon.png");
                else if (line.StartsWith("config_CoveTree="))
                    origConfig._CoveTree = LoadFloraConfig(line.Substring("config_CoveTree=".Length), LanguageHelper.GetFriendlyWord("CoveTreeName"), "/Images/Flora/covetreeicon.png");
                else if (line.StartsWith("config_GiantCoveTree="))
                    origConfig._GiantCoveTree = LoadFloraConfig(line.Substring("config_GiantCoveTree=".Length), LanguageHelper.GetFriendlyWord("GiantCoveTreeName"), "/Images/Flora/covetree2icon.png");
                else if (line.StartsWith("config_SpottedReedsA="))
                    origConfig._SpottedReedsA = LoadFloraConfig(line.Substring("config_SpottedReedsA=".Length), LanguageHelper.GetFriendlyWord("GreenReedsName") + " (1)", "/Images/Flora/spottedreeds1icon.png");
                else if (line.StartsWith("config_SpottedReedsB="))
                    origConfig._SpottedReedsB = LoadFloraConfig(line.Substring("config_SpottedReedsB=".Length), LanguageHelper.GetFriendlyWord("GreenReedsName") + " (2)", "/Images/Flora/spottedreedsicon.png");
                else if (line.StartsWith("config_BrineLily="))
                    origConfig._BrineLily = LoadFloraConfig(line.Substring("config_BrineLily=".Length), LanguageHelper.GetFriendlyWord("BrineLilyName"), "/Images/Flora/lostriverplant4icon.png");
                else if (line.StartsWith("config_LostRiverPlant="))
                    origConfig._LostRiverPlant = LoadFloraConfig(line.Substring("config_LostRiverPlant=".Length), LanguageHelper.GetFriendlyWord("LostRiverPlantName"), "/Images/Flora/lostriverplant5icon.png");
                else if (line.StartsWith("config_CoralReefPlantMiddle="))
                    origConfig._CoralReefPlantMiddle = LoadFloraConfig(line.Substring("config_CoralReefPlantMiddle=".Length), LanguageHelper.GetFriendlyWord("PlantMiddle11Name"), "/Images/Flora/flora_plantmiddle11icon.png");
                else if (line.StartsWith("config_SmallMushroomsDeco="))
                    origConfig._SmallMushroomsDeco = LoadFloraConfig(line.Substring("config_SmallMushroomsDeco=".Length), LanguageHelper.GetFriendlyWord("SmallDeco3Name"), "/Images/Flora/flora_smalldeco03icon.png");
                else if (line.StartsWith("config_FloatingStone="))
                    origConfig._FloatingStone = LoadFloraConfig(line.Substring("config_FloatingStone=".Length), LanguageHelper.GetFriendlyWord("FloatingStoneName"), "/Images/Flora/floatingstone1icon.png");
                else if (line.StartsWith("config_BrownCoralTubesA="))
                    origConfig._BrownCoralTubesA = LoadFloraConfig(line.Substring("config_BrownCoralTubesA=".Length), LanguageHelper.GetFriendlyWord("BrownCoralTubesName") + " (1)", "/Images/Flora/flora_browncoraltubes0203icon.png");
                else if (line.StartsWith("config_BrownCoralTubesB="))
                    origConfig._BrownCoralTubesB = LoadFloraConfig(line.Substring("config_BrownCoralTubesB=".Length), LanguageHelper.GetFriendlyWord("BrownCoralTubesName") + " (2)", "/Images/Flora/flora_browncoraltubes0201icon.png");
                else if (line.StartsWith("config_BrownCoralTubesC="))
                    origConfig._BrownCoralTubesC = LoadFloraConfig(line.Substring("config_BrownCoralTubesC=".Length), LanguageHelper.GetFriendlyWord("BrownCoralTubesName") + " (3)", "/Images/Flora/flora_browncoraltubes01icon.png");
                else if (line.StartsWith("config_BlueCoralTubes="))
                    origConfig._BlueCoralTubes = LoadFloraConfig(line.Substring("config_BlueCoralTubes=".Length), LanguageHelper.GetFriendlyWord("BlueCoralTubes1Name"), "/Images/Flora/flora_bluecoraltubesicon.png");
                else if (line.StartsWith("config_PurplePinecone="))
                    origConfig._PurplePinecone = LoadFloraConfig(line.Substring("config_PurplePinecone=".Length), LanguageHelper.GetFriendlyWord("SmallDeco10Name"), "/Images/Flora/flora_smalldeco10icon.png");
                else if (line.StartsWith("config_CoralPlantYellow="))
                    origConfig._CoralPlantYellow = LoadFloraConfig(line.Substring("config_CoralPlantYellow=".Length), LanguageHelper.GetFriendlyWord("SmallDeco11Name"), "/Images/Flora/flora_smalldeco11icon.png");
                else if (line.StartsWith("config_CoralPlantGreen="))
                    origConfig._CoralPlantGreen = LoadFloraConfig(line.Substring("config_CoralPlantGreen=".Length), LanguageHelper.GetFriendlyWord("SmallDeco13Name"), "/Images/Flora/flora_smalldeco13icon.png");
                else if (line.StartsWith("config_CoralPlantBlue="))
                    origConfig._CoralPlantBlue = LoadFloraConfig(line.Substring("config_CoralPlantBlue=".Length), LanguageHelper.GetFriendlyWord("SmallDeco14Name"), "/Images/Flora/flora_smalldeco14icon.png");
                else if (line.StartsWith("config_CoralPlantRed="))
                    origConfig._CoralPlantRed = LoadFloraConfig(line.Substring("config_CoralPlantRed=".Length), LanguageHelper.GetFriendlyWord("SmallDeco15RedName"), "/Images/Flora/flora_smalldeco15redicon.png");
                else if (line.StartsWith("config_CoralPlantPurple="))
                    origConfig._CoralPlantPurple = LoadFloraConfig(line.Substring("config_CoralPlantPurple=".Length), LanguageHelper.GetFriendlyWord("SmallDeco17PurpleName"), "/Images/Flora/flora_smalldeco17purpleicon.png");
                else if (line.StartsWith("config_RedGrass1="))
                    origConfig._RedGrass1 = LoadFloraConfig(line.Substring("config_RedGrass1=".Length), LanguageHelper.GetFriendlyWord("RedGrassName") + " (1)", "/Images/Flora/redgrasstinyicon.png");
                else if (line.StartsWith("config_RedGrass2="))
                    origConfig._RedGrass2 = LoadFloraConfig(line.Substring("config_RedGrass2=".Length), LanguageHelper.GetFriendlyWord("RedGrassName") + " (2)", "/Images/Flora/redgrass1icon.png");
                else if (line.StartsWith("config_RedGrass3="))
                    origConfig._RedGrass3 = LoadFloraConfig(line.Substring("config_RedGrass3=".Length), LanguageHelper.GetFriendlyWord("RedGrassName") + " (3)", "/Images/Flora/redgrass3icon.png");
                else if (line.StartsWith("config_RedGrass2Tall="))
                    origConfig._RedGrass2Tall = LoadFloraConfig(line.Substring("config_RedGrass2Tall=".Length), LanguageHelper.GetFriendlyWord("RedGrassTallName") + " (1)", "/Images/Flora/redgrass2tallicon.png");
                else if (line.StartsWith("config_RedGrass3Tall="))
                    origConfig._RedGrass3Tall = LoadFloraConfig(line.Substring("config_RedGrass3Tall=".Length), LanguageHelper.GetFriendlyWord("RedGrassTallName") + " (2)", "/Images/Flora/redgrass3tallicon.png");
                else if (line.StartsWith("config_BloodGrass="))
                    origConfig._BloodGrass = LoadFloraConfig(line.Substring("config_BloodGrass=".Length), LanguageHelper.GetFriendlyWord("RedGrassDenseName") + " (1)", "/Images/Flora/bloodgrassicon.png");
                else if (line.StartsWith("config_BloodGrassDense="))
                    origConfig._BloodGrassDense = LoadFloraConfig(line.Substring("config_BloodGrassDense=".Length), LanguageHelper.GetFriendlyWord("RedGrassDenseName") + " (2)", "/Images/Flora/bloodgrassdense2icon.png");
                else if (line.StartsWith("config_MushroomTree1="))
                    origConfig._MushroomTree1 = LoadFloraConfig(line.Substring("config_MushroomTree1=".Length), LanguageHelper.GetFriendlyWord("MushroomTree1Name"), "/Images/Flora/mushroomtreeicon.png");
                else if (line.StartsWith("config_MushroomTree2="))
                    origConfig._MushroomTree2 = LoadFloraConfig(line.Substring("config_MushroomTree2=".Length), LanguageHelper.GetFriendlyWord("MushroomTree2Name"), "/Images/Flora/mushroomtree2icon.png");
                else if (line.StartsWith("config_MarbleMelonTiny="))
                    origConfig._MarbleMelonTiny = LoadFloraConfig(line.Substring("config_MarbleMelonTiny=".Length), LanguageHelper.GetFriendlyWord("MarbleMelonTinyFruitName"), "/Images/Flora/marblemelontinyicon.png");
                else if (line.StartsWith("GhostLeviatan_enable="))
                    origConfig._GhostLeviatan_enable = string.Compare(line.Substring("GhostLeviatan_enable=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("GhostLeviatan_health="))
                    origConfig._GhostLeviatan_health = (int.TryParse(line.Substring("GhostLeviatan_health=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 2000);
                else if (line.StartsWith("GhostLeviatan_maxSpawns="))
                    origConfig._GhostLeviatan_maxSpawns = (int.TryParse(line.Substring("GhostLeviatan_maxSpawns=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 2);
                else if (line.StartsWith("GhostLeviatan_timeBeforeFirstSpawn="))
                    origConfig._GhostLeviatan_timeBeforeFirstSpawn = (int.TryParse(line.Substring("GhostLeviatan_timeBeforeFirstSpawn=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 1200);
                else if (line.StartsWith("GhostLeviatan_spawnTimeRatio="))
                    origConfig._GhostLeviatan_spawnTimeRatio = (int.TryParse(line.Substring("GhostLeviatan_spawnTimeRatio=".Length), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tmpInt) ? tmpInt : 100);
                else if (line.StartsWith("useAlternativeScreenResolution="))
                    origConfig._useAlternativeScreenResolution = string.Compare(line.Substring("useAlternativeScreenResolution=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("hideDeepGrandReefDegasiBase="))
                    origConfig._hideDeepGrandReefDegasiBase = string.Compare(line.Substring("hideDeepGrandReefDegasiBase=".Length), "true", true, CultureInfo.InvariantCulture) == 0;
                else if (line.StartsWith("asBuildable_SpecimenAnalyzer="))
                    origConfig._asBuildable_SpecimenAnalyzer = !(string.Compare(line.Substring("asBuildable_SpecimenAnalyzer=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_MarkiplierDoll1="))
                    origConfig._asBuildable_MarkiplierDoll1 = !(string.Compare(line.Substring("asBuildable_MarkiplierDoll1=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_MarkiplierDoll2="))
                    origConfig._asBuildable_MarkiplierDoll2 = !(string.Compare(line.Substring("asBuildable_MarkiplierDoll2=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_JackSepticEyeDoll="))
                    origConfig._asBuildable_JackSepticEyeDoll = !(string.Compare(line.Substring("asBuildable_JackSepticEyeDoll=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_EatMyDictionDoll="))
                    origConfig._asBuildable_EatMyDictionDoll = !(string.Compare(line.Substring("asBuildable_EatMyDictionDoll=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_ForkliftToy="))
                    origConfig._asBuildable_ForkliftToy = !(string.Compare(line.Substring("asBuildable_ForkliftToy=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_SofaSmall="))
                    origConfig._asBuildable_SofaSmall = !(string.Compare(line.Substring("asBuildable_SofaSmall=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_SofaMedium="))
                    origConfig._asBuildable_SofaMedium = !(string.Compare(line.Substring("asBuildable_SofaMedium=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_SofaBig="))
                    origConfig._asBuildable_SofaBig = !(string.Compare(line.Substring("asBuildable_SofaBig=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_SofaCorner="))
                    origConfig._asBuildable_SofaCorner = !(string.Compare(line.Substring("asBuildable_SofaCorner=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_LabCart="))
                    origConfig._asBuildable_LabCart = !(string.Compare(line.Substring("asBuildable_LabCart=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
                else if (line.StartsWith("asBuildable_EmptyDesk="))
                    origConfig._asBuildable_EmptyDesk = !(string.Compare(line.Substring("asBuildable_EmptyDesk=".Length), "false", true, CultureInfo.InvariantCulture) == 0);
            }
            return origConfig;
        }

        #endregion

        #region Save configuration functions

        public static bool ReplaceStr(ref string str, string newValue, string searchStt)
        {
            int stt = str.IndexOf(searchStt, 0, StringComparison.OrdinalIgnoreCase);
            if (stt >= 0)
            {
                int end = str.IndexOf(Environment.NewLine, stt + searchStt.Length, StringComparison.OrdinalIgnoreCase);
                // Mac failsafe
                if (end <= stt)
                    end = str.IndexOf("\n", stt + searchStt.Length, StringComparison.OrdinalIgnoreCase);
                // Windows failsafe
                if (end <= stt)
                    end = str.IndexOf("\r\n", stt + searchStt.Length, StringComparison.OrdinalIgnoreCase);
                if (end > stt)
                {
                    // We found setting string. Replace and return success.
                    string toReplace = str.Substring(stt, end - stt);
                    string replaceWith = searchStt + newValue;
                    str = str.Replace(toReplace, replaceWith);
                    return true;
                }
            }
            // If we reach here we didn't find the setting string. Return failure.
            return false;
        }

        public static bool SaveConfiguration()
        {
            string configPath = GetConfigFilePath();
            // If Config.txt exists
            if (File.Exists(configPath))
            {
                // Iterate each lines of the Config.txt file
                string configContent = File.ReadAllText(configPath, Encoding.UTF8);
                if (!string.IsNullOrEmpty(configContent))
                {
                    Configuration origConfig = Configuration.InstanceOrigin;
                    Configuration currentConfig = Configuration.Instance;

                    if (string.Compare(currentConfig.Language, origConfig.Language, true, CultureInfo.InvariantCulture) != 0)
                        ReplaceStr(ref configContent, currentConfig.Language, "\nlanguage=");
                    if (currentConfig.UseCompactTooltips != origConfig.UseCompactTooltips)
                        ReplaceStr(ref configContent, currentConfig.UseCompactTooltips.ToString(CultureInfo.InvariantCulture), "\nuseCompactTooltips=");
                    if (currentConfig.LockQuickslotsWhenPlacingItem != origConfig.LockQuickslotsWhenPlacingItem)
                        ReplaceStr(ref configContent, currentConfig.LockQuickslotsWhenPlacingItem.ToString(CultureInfo.InvariantCulture), "\nlockQuickslotsWhenPlacingItem=");
                    if (currentConfig.AllowBuildOutside != origConfig.AllowBuildOutside)
                        ReplaceStr(ref configContent, currentConfig.AllowBuildOutside.ToString(CultureInfo.InvariantCulture), "\nallowBuildOutside=");
                    if (currentConfig.AllowPlaceOutside != origConfig.AllowPlaceOutside)
                        ReplaceStr(ref configContent, currentConfig.AllowPlaceOutside.ToString(CultureInfo.InvariantCulture), "\nallowPlaceOutside=");
                    if (currentConfig.EnablePlaceItems != origConfig.EnablePlaceItems)
                        ReplaceStr(ref configContent, currentConfig.EnablePlaceItems.ToString(CultureInfo.InvariantCulture), "\nenablePlaceItems=");
                    if (currentConfig.EnablePlaceMaterials != origConfig.EnablePlaceMaterials)
                        ReplaceStr(ref configContent, currentConfig.EnablePlaceMaterials.ToString(CultureInfo.InvariantCulture), "\nenablePlaceMaterials=");
                    if (currentConfig.EnablePlaceBatteries != origConfig.EnablePlaceBatteries)
                        ReplaceStr(ref configContent, currentConfig.EnablePlaceBatteries.ToString(CultureInfo.InvariantCulture), "\nenablePlaceBatteries=");
                    if (currentConfig.EnablePlaceEggs != origConfig.EnablePlaceEggs)
                        ReplaceStr(ref configContent, currentConfig.EnablePlaceEggs.ToString(CultureInfo.InvariantCulture), "\nenablePlaceEggs=");
                    if (currentConfig.EnableNewFlora != origConfig.EnableNewFlora)
                        ReplaceStr(ref configContent, currentConfig.EnableNewFlora.ToString(CultureInfo.InvariantCulture), "\nenableNewFlora=");
                    if (currentConfig.EnableNewItems != origConfig.EnableNewItems)
                        ReplaceStr(ref configContent, currentConfig.EnableNewItems.ToString(CultureInfo.InvariantCulture), "\nenableNewItems=");
                    if (currentConfig.EnableSofas != origConfig.EnableSofas)
                        ReplaceStr(ref configContent, currentConfig.EnableSofas.ToString(CultureInfo.InvariantCulture), "\nenableSofas=");
                    if (currentConfig.EnableDecorativeElectronics != origConfig.EnableDecorativeElectronics)
                        ReplaceStr(ref configContent, currentConfig.EnableDecorativeElectronics.ToString(CultureInfo.InvariantCulture), "\nenableDecorativeElectronics=");
                    if (currentConfig.EnableCustomBaseParts != origConfig.EnableCustomBaseParts)
                        ReplaceStr(ref configContent, currentConfig.EnableCustomBaseParts.ToString(CultureInfo.InvariantCulture), "\nenableCustomBaseParts=");
                    if (currentConfig.HabitatBuilderItems != origConfig.HabitatBuilderItems)
                        ReplaceStr(ref configContent, currentConfig.HabitatBuilderItems, "\nhabitatBuilderItems=");
                    if (currentConfig.AllowIndoorLongPlanterOutside != origConfig.AllowIndoorLongPlanterOutside)
                        ReplaceStr(ref configContent, currentConfig.AllowIndoorLongPlanterOutside.ToString(CultureInfo.InvariantCulture), "\nallowIndoorLongPlanterOutside=");
                    if (currentConfig.AllowOutdoorLongPlanterInside != origConfig.AllowOutdoorLongPlanterInside)
                        ReplaceStr(ref configContent, currentConfig.AllowOutdoorLongPlanterInside.ToString(CultureInfo.InvariantCulture), "\nallowOutdoorLongPlanterInside=");
                    if (currentConfig.FixAquariumLighting != origConfig.FixAquariumLighting)
                        ReplaceStr(ref configContent, currentConfig.FixAquariumLighting.ToString(CultureInfo.InvariantCulture), "\nfixAquariumLighting=");
                    if (currentConfig.EnableAquariumGlassGlowing != origConfig.EnableAquariumGlassGlowing)
                        ReplaceStr(ref configContent, currentConfig.EnableAquariumGlassGlowing.ToString(CultureInfo.InvariantCulture), "\nenableAquariumGlassGlowing=");
                    if (currentConfig.AddItemsWhenDiscovered != origConfig.AddItemsWhenDiscovered)
                        ReplaceStr(ref configContent, currentConfig.AddItemsWhenDiscovered.ToString(CultureInfo.InvariantCulture), "\naddItemsWhenDiscovered=");
                    if (currentConfig.AddAirSeedsWhenDiscovered != origConfig.AddAirSeedsWhenDiscovered)
                        ReplaceStr(ref configContent, currentConfig.AddAirSeedsWhenDiscovered.ToString(CultureInfo.InvariantCulture), "\naddAirSeedsWhenDiscovered=");
                    if (currentConfig.AddWaterSeedsWhenDiscovered != origConfig.AddWaterSeedsWhenDiscovered)
                        ReplaceStr(ref configContent, currentConfig.AddWaterSeedsWhenDiscovered.ToString(CultureInfo.InvariantCulture), "\naddWaterSeedsWhenDiscovered=");
                    if (currentConfig.AddEggsWhenCreatureScanned != origConfig.AddEggsWhenCreatureScanned)
                        ReplaceStr(ref configContent, currentConfig.AddEggsWhenCreatureScanned.ToString(CultureInfo.InvariantCulture), "\naddEggsWhenCreatureScanned=");
                    if (currentConfig.AddEggsAtStart != origConfig.AddEggsAtStart)
                        ReplaceStr(ref configContent, currentConfig.AddEggsAtStart.ToString(CultureInfo.InvariantCulture), "\naddEggsAtStart=");
                    if (currentConfig.EnablePrecursorTab != origConfig.EnablePrecursorTab)
                        ReplaceStr(ref configContent, currentConfig.EnablePrecursorTab.ToString(CultureInfo.InvariantCulture), "\nenablePrecursorTab=");
                    if (currentConfig.PrecursorKeysAll != origConfig.PrecursorKeysAll)
                        ReplaceStr(ref configContent, currentConfig.PrecursorKeysAll.ToString(CultureInfo.InvariantCulture), "\nprecursorKeysAll=");
                    if (string.Compare(currentConfig.PrecursorKeys_RecipiesResource, origConfig.PrecursorKeys_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                        ReplaceStr(ref configContent, currentConfig.PrecursorKeys_RecipiesResource, "\nprecursorKeys_RecipiesResource=");
                    if (currentConfig.PrecursorKeys_RecipiesResourceAmount != origConfig.PrecursorKeys_RecipiesResourceAmount)
                        ReplaceStr(ref configContent, currentConfig.PrecursorKeys_RecipiesResourceAmount.ToString(CultureInfo.InvariantCulture), "\nprecursorKeys_RecipiesResourceAmount=");
                    if (string.Compare(currentConfig.Relics_RecipiesResource, origConfig.Relics_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                        ReplaceStr(ref configContent, currentConfig.Relics_RecipiesResource, "\nrelics_RecipiesResource=");
                    if (currentConfig.Relics_RecipiesResourceAmount != origConfig.Relics_RecipiesResourceAmount)
                        ReplaceStr(ref configContent, currentConfig.Relics_RecipiesResourceAmount.ToString(CultureInfo.InvariantCulture), "\nrelics_RecipiesResourceAmount=");
                    if (currentConfig.AlienRelic1Animation != origConfig.AlienRelic1Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic1Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic1Animation=");
                    if (currentConfig.AlienRelic2Animation != origConfig.AlienRelic2Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic2Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic2Animation=");
                    if (currentConfig.AlienRelic3Animation != origConfig.AlienRelic3Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic3Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic3Animation=");
                    if (currentConfig.AlienRelic4Animation != origConfig.AlienRelic4Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic4Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic4Animation=");
                    if (currentConfig.AlienRelic5Animation != origConfig.AlienRelic5Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic5Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic5Animation=");
                    if (currentConfig.AlienRelic6Animation != origConfig.AlienRelic6Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic6Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic6Animation=");
                    if (currentConfig.AlienRelic7Animation != origConfig.AlienRelic7Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic7Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic7Animation=");
                    if (currentConfig.AlienRelic8Animation != origConfig.AlienRelic8Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic8Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic8Animation=");
                    if (currentConfig.AlienRelic9Animation != origConfig.AlienRelic9Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic9Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic9Animation=");
                    if (currentConfig.AlienRelic10Animation != origConfig.AlienRelic10Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic10Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic10Animation=");
                    if (currentConfig.AlienRelic11Animation != origConfig.AlienRelic11Animation)
                        ReplaceStr(ref configContent, currentConfig.AlienRelic11Animation.ToString(CultureInfo.InvariantCulture), "\nalienRelic11Animation=");
                    if (currentConfig.EnableAllEggs != origConfig.EnableAllEggs)
                        ReplaceStr(ref configContent, currentConfig.EnableAllEggs.ToString(CultureInfo.InvariantCulture), "\nenableAllEggs=");
                    if (string.Compare(currentConfig.CreatureEggs_RecipiesResource, origConfig.CreatureEggs_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                        ReplaceStr(ref configContent, currentConfig.CreatureEggs_RecipiesResource, "\ncreatureEggs_RecipiesResource=");
                    if (currentConfig.CreatureEggs_RecipiesResourceAmount != origConfig.CreatureEggs_RecipiesResourceAmount)
                        ReplaceStr(ref configContent, currentConfig.CreatureEggs_RecipiesResourceAmount.ToString(CultureInfo.InvariantCulture), "\ncreatureEggs_RecipiesResourceAmount=");
                    if (currentConfig.EnableNutrientBlock != origConfig.EnableNutrientBlock)
                        ReplaceStr(ref configContent, currentConfig.EnableNutrientBlock.ToString(CultureInfo.InvariantCulture), "\nenableNutrientBlock=");
                    if (currentConfig.BarBottle1Water != origConfig.BarBottle1Water)
                        ReplaceStr(ref configContent, currentConfig.BarBottle1Water.ToString(CultureInfo.InvariantCulture), "\nbarBottle1Water=");
                    if (currentConfig.BarBottle2Water != origConfig.BarBottle2Water)
                        ReplaceStr(ref configContent, currentConfig.BarBottle2Water.ToString(CultureInfo.InvariantCulture), "\nbarBottle2Water=");
                    if (currentConfig.BarBottle3Water != origConfig.BarBottle3Water)
                        ReplaceStr(ref configContent, currentConfig.BarBottle3Water.ToString(CultureInfo.InvariantCulture), "\nbarBottle3Water=");
                    if (currentConfig.BarBottle4Water != origConfig.BarBottle4Water)
                        ReplaceStr(ref configContent, currentConfig.BarBottle4Water.ToString(CultureInfo.InvariantCulture), "\nbarBottle4Water=");
                    if (currentConfig.BarBottle5Water != origConfig.BarBottle5Water)
                        ReplaceStr(ref configContent, currentConfig.BarBottle5Water.ToString(CultureInfo.InvariantCulture), "\nbarBottle5Water=");
                    if (currentConfig.BarFood1Nutrient != origConfig.BarFood1Nutrient)
                        ReplaceStr(ref configContent, currentConfig.BarFood1Nutrient.ToString(CultureInfo.InvariantCulture), "\nbarFood1Nutrient=");
                    if (currentConfig.BarFood1Water != origConfig.BarFood1Water)
                        ReplaceStr(ref configContent, currentConfig.BarFood1Water.ToString(CultureInfo.InvariantCulture), "\nbarFood1Water=");
                    if (currentConfig.BarFood2Nutrient != origConfig.BarFood2Nutrient)
                        ReplaceStr(ref configContent, currentConfig.BarFood2Nutrient.ToString(CultureInfo.InvariantCulture), "\nbarFood2Nutrient=");
                    if (currentConfig.BarFood2Water != origConfig.BarFood2Water)
                        ReplaceStr(ref configContent, currentConfig.BarFood2Water.ToString(CultureInfo.InvariantCulture), "\nbarFood2Water=");
                    if (currentConfig.AddRegularAirSeeds != origConfig.AddRegularAirSeeds)
                        ReplaceStr(ref configContent, currentConfig.AddRegularAirSeeds.ToString(CultureInfo.InvariantCulture), "\naddRegularAirSeeds=");
                    if (currentConfig.AddRegularWaterSeeds != origConfig.AddRegularWaterSeeds)
                        ReplaceStr(ref configContent, currentConfig.AddRegularWaterSeeds.ToString(CultureInfo.InvariantCulture), "\naddRegularWaterSeeds=");
                    if (string.Compare(currentConfig.Flora_RecipiesResource, origConfig.Flora_RecipiesResource, true, CultureInfo.InvariantCulture) != 0)
                        ReplaceStr(ref configContent, currentConfig.Flora_RecipiesResource, "\nflora_RecipiesResource=");
                    if (currentConfig.Flora_RecipiesResourceAmount != origConfig.Flora_RecipiesResourceAmount)
                        ReplaceStr(ref configContent, currentConfig.Flora_RecipiesResourceAmount.ToString(CultureInfo.InvariantCulture), "\nflora_RecipiesResourceAmount=");
                    if (string.Compare(currentConfig.PurplePineconeDroppedResource, origConfig.PurplePineconeDroppedResource, true, CultureInfo.InvariantCulture) != 0)
                        ReplaceStr(ref configContent, currentConfig.PurplePineconeDroppedResource, "\npurplePineconeDroppedResource=");
                    if (currentConfig.PurplePineconeDroppedResourceAmount != origConfig.PurplePineconeDroppedResourceAmount)
                        ReplaceStr(ref configContent, currentConfig.PurplePineconeDroppedResourceAmount.ToString(CultureInfo.InvariantCulture), "\npurplePineconeDroppedResourceAmount=");
                    if (!currentConfig.LandTree.IsEqual(origConfig.LandTree))
                        ReplaceStr(ref configContent, currentConfig.LandTree.GetConfigStr(), "\nconfig_LandTree=");
                    if (!currentConfig.JungleTreeA.IsEqual(origConfig.JungleTreeA))
                        ReplaceStr(ref configContent, currentConfig.JungleTreeA.GetConfigStr(), "\nconfig_JungleTreeA=");
                    if (!currentConfig.JungleTreeB.IsEqual(origConfig.JungleTreeB))
                        ReplaceStr(ref configContent, currentConfig.JungleTreeB.GetConfigStr(), "\nconfig_JungleTreeB=");
                    if (!currentConfig.TropicalTreeA.IsEqual(origConfig.TropicalTreeA))
                        ReplaceStr(ref configContent, currentConfig.TropicalTreeA.GetConfigStr(), "\nconfig_TropicalTreeA=");
                    if (!currentConfig.TropicalTreeB.IsEqual(origConfig.TropicalTreeB))
                        ReplaceStr(ref configContent, currentConfig.TropicalTreeB.GetConfigStr(), "\nconfig_TropicalTreeB=");
                    if (!currentConfig.TropicalTreeC.IsEqual(origConfig.TropicalTreeC))
                        ReplaceStr(ref configContent, currentConfig.TropicalTreeC.GetConfigStr(), "\nconfig_TropicalTreeC=");
                    if (!currentConfig.TropicalTreeD.IsEqual(origConfig.TropicalTreeD))
                        ReplaceStr(ref configContent, currentConfig.TropicalTreeD.GetConfigStr(), "\nconfig_TropicalTreeD=");
                    if (!currentConfig.LandPlantRedA.IsEqual(origConfig.LandPlantRedA))
                        ReplaceStr(ref configContent, currentConfig.LandPlantRedA.GetConfigStr(), "\nconfig_LandPlantRedA=");
                    if (!currentConfig.LandPlantRedB.IsEqual(origConfig.LandPlantRedB))
                        ReplaceStr(ref configContent, currentConfig.LandPlantRedB.GetConfigStr(), "\nconfig_LandPlantRedB=");
                    if (!currentConfig.LandPlantA.IsEqual(origConfig.LandPlantA))
                        ReplaceStr(ref configContent, currentConfig.LandPlantA.GetConfigStr(), "\nconfig_LandPlantA=");
                    if (!currentConfig.LandPlantB.IsEqual(origConfig.LandPlantB))
                        ReplaceStr(ref configContent, currentConfig.LandPlantB.GetConfigStr(), "\nconfig_LandPlantB=");
                    if (!currentConfig.LandPlantC.IsEqual(origConfig.LandPlantC))
                        ReplaceStr(ref configContent, currentConfig.LandPlantC.GetConfigStr(), "\nconfig_LandPlantC=");
                    if (!currentConfig.FernA.IsEqual(origConfig.FernA))
                        ReplaceStr(ref configContent, currentConfig.FernA.GetConfigStr(), "\nconfig_FernA=");
                    if (!currentConfig.FernB.IsEqual(origConfig.FernB))
                        ReplaceStr(ref configContent, currentConfig.FernB.GetConfigStr(), "\nconfig_FernB=");
                    if (!currentConfig.TropicalPlantA.IsEqual(origConfig.TropicalPlantA))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantA.GetConfigStr(), "\nconfig_TropicalPlantA=");
                    if (!currentConfig.TropicalPlantB.IsEqual(origConfig.TropicalPlantB))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantB.GetConfigStr(), "\nconfig_TropicalPlantB=");
                    if (!currentConfig.TropicalPlantC.IsEqual(origConfig.TropicalPlantC))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantC.GetConfigStr(), "\nconfig_TropicalPlantC=");
                    if (!currentConfig.TropicalPlantD.IsEqual(origConfig.TropicalPlantD))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantD.GetConfigStr(), "\nconfig_TropicalPlantD=");
                    if (!currentConfig.TropicalPlantE.IsEqual(origConfig.TropicalPlantE))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantE.GetConfigStr(), "\nconfig_TropicalPlantE=");
                    if (!currentConfig.TropicalPlantF.IsEqual(origConfig.TropicalPlantF))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantF.GetConfigStr(), "\nconfig_TropicalPlantF=");
                    if (!currentConfig.TropicalPlantG.IsEqual(origConfig.TropicalPlantG))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantG.GetConfigStr(), "\nconfig_TropicalPlantG=");
                    if (!currentConfig.TropicalPlantH.IsEqual(origConfig.TropicalPlantH))
                        ReplaceStr(ref configContent, currentConfig.TropicalPlantH.GetConfigStr(), "\nconfig_TropicalPlantH=");
                    if (!currentConfig.CrabClawKelpA.IsEqual(origConfig.CrabClawKelpA))
                        ReplaceStr(ref configContent, currentConfig.CrabClawKelpA.GetConfigStr(), "\nconfig_CrabClawKelpA=");
                    if (!currentConfig.CrabClawKelpB.IsEqual(origConfig.CrabClawKelpB))
                        ReplaceStr(ref configContent, currentConfig.CrabClawKelpB.GetConfigStr(), "\nconfig_CrabClawKelpB=");
                    if (!currentConfig.CrabClawKelpC.IsEqual(origConfig.CrabClawKelpC))
                        ReplaceStr(ref configContent, currentConfig.CrabClawKelpC.GetConfigStr(), "\nconfig_CrabClawKelpC=");
                    if (!currentConfig.PyroCoralA.IsEqual(origConfig.PyroCoralA))
                        ReplaceStr(ref configContent, currentConfig.PyroCoralA.GetConfigStr(), "\nconfig_PyroCoralA=");
                    if (!currentConfig.PyroCoralB.IsEqual(origConfig.PyroCoralB))
                        ReplaceStr(ref configContent, currentConfig.PyroCoralB.GetConfigStr(), "\nconfig_PyroCoralB=");
                    if (!currentConfig.PyroCoralC.IsEqual(origConfig.PyroCoralC))
                        ReplaceStr(ref configContent, currentConfig.PyroCoralC.GetConfigStr(), "\nconfig_PyroCoralC=");
                    if (!currentConfig.CoveTree.IsEqual(origConfig.CoveTree))
                        ReplaceStr(ref configContent, currentConfig.CoveTree.GetConfigStr(), "\nconfig_CoveTree=");
                    if (!currentConfig.GiantCoveTree.IsEqual(origConfig.GiantCoveTree))
                        ReplaceStr(ref configContent, currentConfig.GiantCoveTree.GetConfigStr(), "\nconfig_GiantCoveTree=");
                    if (!currentConfig.SpottedReedsA.IsEqual(origConfig.SpottedReedsA))
                        ReplaceStr(ref configContent, currentConfig.SpottedReedsA.GetConfigStr(), "\nconfig_SpottedReedsA=");
                    if (!currentConfig.SpottedReedsB.IsEqual(origConfig.SpottedReedsB))
                        ReplaceStr(ref configContent, currentConfig.SpottedReedsB.GetConfigStr(), "\nconfig_SpottedReedsB=");
                    if (!currentConfig.BrineLily.IsEqual(origConfig.BrineLily))
                        ReplaceStr(ref configContent, currentConfig.BrineLily.GetConfigStr(), "\nconfig_BrineLily=");
                    if (!currentConfig.LostRiverPlant.IsEqual(origConfig.LostRiverPlant))
                        ReplaceStr(ref configContent, currentConfig.LostRiverPlant.GetConfigStr(), "\nconfig_LostRiverPlant=");
                    if (!currentConfig.CoralReefPlantMiddle.IsEqual(origConfig.CoralReefPlantMiddle))
                        ReplaceStr(ref configContent, currentConfig.CoralReefPlantMiddle.GetConfigStr(), "\nconfig_CoralReefPlantMiddle=");
                    if (!currentConfig.SmallMushroomsDeco.IsEqual(origConfig.SmallMushroomsDeco))
                        ReplaceStr(ref configContent, currentConfig.SmallMushroomsDeco.GetConfigStr(), "\nconfig_SmallMushroomsDeco=");
                    if (!currentConfig.FloatingStone.IsEqual(origConfig.FloatingStone))
                        ReplaceStr(ref configContent, currentConfig.FloatingStone.GetConfigStr(), "\nconfig_FloatingStone=");
                    if (!currentConfig.BrownCoralTubesA.IsEqual(origConfig.BrownCoralTubesA))
                        ReplaceStr(ref configContent, currentConfig.BrownCoralTubesA.GetConfigStr(), "\nconfig_BrownCoralTubesA=");
                    if (!currentConfig.BrownCoralTubesB.IsEqual(origConfig.BrownCoralTubesB))
                        ReplaceStr(ref configContent, currentConfig.BrownCoralTubesB.GetConfigStr(), "\nconfig_BrownCoralTubesB=");
                    if (!currentConfig.BrownCoralTubesC.IsEqual(origConfig.BrownCoralTubesC))
                        ReplaceStr(ref configContent, currentConfig.BrownCoralTubesC.GetConfigStr(), "\nconfig_BrownCoralTubesC=");
                    if (!currentConfig.BlueCoralTubes.IsEqual(origConfig.BlueCoralTubes))
                        ReplaceStr(ref configContent, currentConfig.BlueCoralTubes.GetConfigStr(), "\nconfig_BlueCoralTubes=");
                    if (!currentConfig.PurplePinecone.IsEqual(origConfig.PurplePinecone))
                        ReplaceStr(ref configContent, currentConfig.PurplePinecone.GetConfigStr(), "\nconfig_PurplePinecone=");
                    if (!currentConfig.CoralPlantYellow.IsEqual(origConfig.CoralPlantYellow))
                        ReplaceStr(ref configContent, currentConfig.CoralPlantYellow.GetConfigStr(), "\nconfig_CoralPlantYellow=");
                    if (!currentConfig.CoralPlantGreen.IsEqual(origConfig.CoralPlantGreen))
                        ReplaceStr(ref configContent, currentConfig.CoralPlantGreen.GetConfigStr(), "\nconfig_CoralPlantGreen=");
                    if (!currentConfig.CoralPlantBlue.IsEqual(origConfig.CoralPlantBlue))
                        ReplaceStr(ref configContent, currentConfig.CoralPlantBlue.GetConfigStr(), "\nconfig_CoralPlantBlue=");
                    if (!currentConfig.CoralPlantRed.IsEqual(origConfig.CoralPlantRed))
                        ReplaceStr(ref configContent, currentConfig.CoralPlantRed.GetConfigStr(), "\nconfig_CoralPlantRed=");
                    if (!currentConfig.CoralPlantPurple.IsEqual(origConfig.CoralPlantPurple))
                        ReplaceStr(ref configContent, currentConfig.CoralPlantPurple.GetConfigStr(), "\nconfig_CoralPlantPurple=");
                    if (!currentConfig.RedGrass1.IsEqual(origConfig.RedGrass1))
                        ReplaceStr(ref configContent, currentConfig.RedGrass1.GetConfigStr(), "\nconfig_RedGrass1=");
                    if (!currentConfig.RedGrass2.IsEqual(origConfig.RedGrass2))
                        ReplaceStr(ref configContent, currentConfig.RedGrass2.GetConfigStr(), "\nconfig_RedGrass2=");
                    if (!currentConfig.RedGrass3.IsEqual(origConfig.RedGrass3))
                        ReplaceStr(ref configContent, currentConfig.RedGrass3.GetConfigStr(), "\nconfig_RedGrass3=");
                    if (!currentConfig.RedGrass2Tall.IsEqual(origConfig.RedGrass2Tall))
                        ReplaceStr(ref configContent, currentConfig.RedGrass2Tall.GetConfigStr(), "\nconfig_RedGrass2Tall=");
                    if (!currentConfig.RedGrass3Tall.IsEqual(origConfig.RedGrass3Tall))
                        ReplaceStr(ref configContent, currentConfig.RedGrass3Tall.GetConfigStr(), "\nconfig_RedGrass3Tall=");
                    if (!currentConfig.BloodGrass.IsEqual(origConfig.BloodGrass))
                        ReplaceStr(ref configContent, currentConfig.BloodGrass.GetConfigStr(), "\nconfig_BloodGrass=");
                    if (!currentConfig.BloodGrassDense.IsEqual(origConfig.BloodGrassDense))
                        ReplaceStr(ref configContent, currentConfig.BloodGrassDense.GetConfigStr(), "\nconfig_BloodGrassDense=");
                    if (!currentConfig.MushroomTree1.IsEqual(origConfig.MushroomTree1))
                        ReplaceStr(ref configContent, currentConfig.MushroomTree1.GetConfigStr(), "\nconfig_MushroomTree1=");
                    if (!currentConfig.MushroomTree2.IsEqual(origConfig.MushroomTree2))
                        ReplaceStr(ref configContent, currentConfig.MushroomTree2.GetConfigStr(), "\nconfig_MushroomTree2=");
                    if (!currentConfig.MarbleMelonTiny.IsEqual(origConfig.MarbleMelonTiny))
                        ReplaceStr(ref configContent, currentConfig.MarbleMelonTiny.GetConfigStr(), "\nconfig_MarbleMelonTiny=");
                    if (currentConfig.GhostLeviatan_enable != origConfig.GhostLeviatan_enable)
                        ReplaceStr(ref configContent, currentConfig.GhostLeviatan_enable.ToString(CultureInfo.InvariantCulture), "\nGhostLeviatan_enable=");
                    if (currentConfig.GhostLeviatan_health != origConfig.GhostLeviatan_health)
                        ReplaceStr(ref configContent, currentConfig.GhostLeviatan_health.ToString(CultureInfo.InvariantCulture), "\nGhostLeviatan_health=");
                    if (currentConfig.GhostLeviatan_maxSpawns != origConfig.GhostLeviatan_maxSpawns)
                        ReplaceStr(ref configContent, currentConfig.GhostLeviatan_maxSpawns.ToString(CultureInfo.InvariantCulture), "\nGhostLeviatan_maxSpawns=");
                    if (currentConfig.GhostLeviatan_timeBeforeFirstSpawn != origConfig.GhostLeviatan_timeBeforeFirstSpawn)
                        ReplaceStr(ref configContent, currentConfig.GhostLeviatan_timeBeforeFirstSpawn.ToString(CultureInfo.InvariantCulture), "\nGhostLeviatan_timeBeforeFirstSpawn=");
                    if (currentConfig.GhostLeviatan_spawnTimeRatio != origConfig.GhostLeviatan_spawnTimeRatio)
                        ReplaceStr(ref configContent, currentConfig.GhostLeviatan_spawnTimeRatio.ToString(CultureInfo.InvariantCulture), "\nGhostLeviatan_spawnTimeRatio=");
                    if (currentConfig.UseAlternativeScreenResolution != origConfig.UseAlternativeScreenResolution)
                        ReplaceStr(ref configContent, currentConfig.UseAlternativeScreenResolution.ToString(CultureInfo.InvariantCulture), "\nuseAlternativeScreenResolution=");
                    if (currentConfig.HideDeepGrandReefDegasiBase != origConfig.HideDeepGrandReefDegasiBase)
                        ReplaceStr(ref configContent, currentConfig.HideDeepGrandReefDegasiBase.ToString(CultureInfo.InvariantCulture), "\nhideDeepGrandReefDegasiBase=");
                    if (currentConfig.AsBuildable_SpecimenAnalyzer != origConfig.AsBuildable_SpecimenAnalyzer)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_SpecimenAnalyzer.ToString(CultureInfo.InvariantCulture), "\nasBuildable_SpecimenAnalyzer=");
                    if (currentConfig.AsBuildable_MarkiplierDoll1 != origConfig.AsBuildable_MarkiplierDoll1)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_MarkiplierDoll1.ToString(CultureInfo.InvariantCulture), "\nasBuildable_MarkiplierDoll1=");
                    if (currentConfig.AsBuildable_MarkiplierDoll2 != origConfig.AsBuildable_MarkiplierDoll2)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_MarkiplierDoll2.ToString(CultureInfo.InvariantCulture), "\nasBuildable_MarkiplierDoll2=");
                    if (currentConfig.AsBuildable_JackSepticEyeDoll != origConfig.AsBuildable_JackSepticEyeDoll)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_JackSepticEyeDoll.ToString(CultureInfo.InvariantCulture), "\nasBuildable_JackSepticEyeDoll=");
                    if (currentConfig.AsBuildable_EatMyDictionDoll != origConfig.AsBuildable_EatMyDictionDoll)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_EatMyDictionDoll.ToString(CultureInfo.InvariantCulture), "\nasBuildable_EatMyDictionDoll=");
                    if (currentConfig.AsBuildable_ForkliftToy != origConfig.AsBuildable_ForkliftToy)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_ForkliftToy.ToString(CultureInfo.InvariantCulture), "\nasBuildable_ForkliftToy=");
                    if (currentConfig.AsBuildable_SofaSmall != origConfig.AsBuildable_SofaSmall)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_SofaSmall.ToString(CultureInfo.InvariantCulture), "\nasBuildable_SofaSmall=");
                    if (currentConfig.AsBuildable_SofaMedium != origConfig.AsBuildable_SofaMedium)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_SofaMedium.ToString(CultureInfo.InvariantCulture), "\nasBuildable_SofaMedium=");
                    if (currentConfig.AsBuildable_SofaBig != origConfig.AsBuildable_SofaBig)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_SofaBig.ToString(CultureInfo.InvariantCulture), "\nasBuildable_SofaBig=");
                    if (currentConfig.AsBuildable_SofaCorner != origConfig.AsBuildable_SofaCorner)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_SofaCorner.ToString(CultureInfo.InvariantCulture), "\nasBuildable_SofaCorner=");
                    if (currentConfig.AsBuildable_LabCart != origConfig.AsBuildable_LabCart)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_LabCart.ToString(CultureInfo.InvariantCulture), "\nasBuildable_LabCart=");
                    if (currentConfig.AsBuildable_EmptyDesk != origConfig.AsBuildable_EmptyDesk)
                        ReplaceStr(ref configContent, currentConfig.AsBuildable_EmptyDesk.ToString(CultureInfo.InvariantCulture), "\nasBuildable_EmptyDesk=");

                    try
                    {
                        File.WriteAllText(configPath, configContent, Encoding.UTF8);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("ERROR: Unable to write new configuration. Exception=[{0}]", ex.ToString());
                        return false;
                    }
                }
            }
            else
                Logger.Log("ERROR: Config file not found at [{0}].", configPath);
            return false;
        }

        #endregion

        #region Language functions

        /// <summary>Returns 2-letter country code or "auto" base on operating system.</summary>
        public static string GetDefaultLanguage()
        {
            string label = CultureInfo.InstalledUICulture?.TwoLetterISOLanguageName;
            if (!string.IsNullOrEmpty(label) && label.Length == 2)
            {
                label = label.ToLowerInvariant();
                foreach (KeyValuePair<CountryCode, string> c in CountryCodes)
                    if (label == c.Value)
                        return label;
            }
            return "auto";
        }

        #endregion

        public bool IsItemEnabled(string itemName)
        {
            if (!string.IsNullOrEmpty(_habitatBuilderItems))
            {
                string[] items = _habitatBuilderItems.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (items != null)
                    foreach (string item in items)
                        if (item == itemName)
                            return true;
            }
            return false;
        }

        public void AddItem(string itemName)
        {
            string[] items = _habitatBuilderItems.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (items != null)
            {
                foreach (string item in items)
                    if (item == itemName)
                        return;
                _habitatBuilderItems += ("/" + itemName);
            }
            else
                _habitatBuilderItems = itemName;
        }

        public void RemoveItem(string itemName)
        {
            if (!string.IsNullOrEmpty(_habitatBuilderItems))
            {
                string[] items = _habitatBuilderItems.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (items != null)
                {
                    List<string> itemsList = new List<string>();
                    foreach (string item in items)
                        if (item != itemName)
                            itemsList.Add(item);
                    bool first = true;
                    string newItemsList = "";
                    foreach (string s in itemsList)
                    {
                        if (first)
                        {
                            newItemsList += s;
                            first = false;
                        }
                        else
                            newItemsList += ("/" + s);
                    }
                    _habitatBuilderItems = newItemsList;
                }
            }
        }
    }
}
