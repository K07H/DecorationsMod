using System;
using static DecorationsMod.RegionHelper;

namespace DecorationsMod
{
    public static class LanguageHelper
    {
        /// <summary>Current language.</summary>
        public static CountryCode UserLanguage = RegionHelper.GetDefaultCountryCode();

        /// <summary>Returns growing tooltip text based on selected language.</summary>
        /// <param name="progress">The growth progression (in percents).</param>
        public static string GetFriendlyGrowingTooltip(int progress)
        {
            if (UserLanguage == CountryCode.FR)
                return "Croissance : " + progress + "%";
            else if (UserLanguage == CountryCode.ES)
                return "Crecimiento: " + progress + "%";
            else if (UserLanguage == CountryCode.TR)
                return "Büyüme: " + progress + "%";
            else if (UserLanguage == CountryCode.DE)
                return "Wachstum: " + progress + "%";
            else if (UserLanguage == CountryCode.RU)
                return "Рост: " + progress + "%";
            else if (UserLanguage == CountryCode.NL)
                return "Groei: " + progress + "%";
            else
                return "Growth: " + progress + "%";
        }

        /// <summary>Returns translated text based on selected language.</summary>
        /// <param name="word">The ID of the text to translate.</param>
        public static string GetFriendlyWord(string word)
        {
            switch (word)
            {
                case "DecorationsFabricatorName":
                    if (UserLanguage == CountryCode.FR)
                        return "Fabricateur de décorations";
                    else if (UserLanguage == CountryCode.ES)
                        return "Fabricador de decoraciones";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekorasyon Üreticisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dekorationsfabrikator";
                    else if (UserLanguage == CountryCode.RU)
                        return "Изготовитель декораций";
                    else if (UserLanguage == CountryCode.NL)
						return "Decoraties fabriceerder"; // thanks Thom for the translation of fabricator :D
                    else
                        return "Decorations fabricator";
                case "DecorationsFabricatorDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un fabricateur permettant de produire des objets décoratifs.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un Fabricador para producir artículos de decoración.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekorasyon eşyaları üretmek için bir üretici.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Fabrikator, der zum Erstellen von Dekorationen dient.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Изготавливает декорации для украшения вашего будущего дома.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een fabriceerder om decoratieve objecten te fabriceeren.";
                    else
                        return "A fabricator to produce decoration items.";
                case "UseDecorationsFabricator":
                    if (UserLanguage == CountryCode.FR)
                        return "Utiliser le fabricateur de décorations";
                    else if (UserLanguage == CountryCode.ES)
                        return "Utilizar el fabricador de decoraciones";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekorasyon Üreticisi kullanın";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dekorationsfabrikator verwenden";
                    else if (UserLanguage == CountryCode.RU)
                        return "Используйте Изготовитель декораций";
                    else if (UserLanguage == CountryCode.NL)
						return "Gebruik decoratie fabriceerder";
                    else
                        return "Use decorations fabricator";
                case "FloraFabricatorName":
                    if (UserLanguage == CountryCode.FR)
                        return "Fabricateur de graînes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Fabricador de semillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tohum Üreticisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Samenfabrikator";
                    else if (UserLanguage == CountryCode.RU)
                        return "Изготовитель семян";
                    else if (UserLanguage == CountryCode.NL)
						return "Zaden fabriceerder";
                    else
                        return "Seeds fabricator";
                case "FloraFabricatorDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un fabricateur permettant de produire des graînes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un Fabricador para producir semillas.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tohum üretmek için bir üretici.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Fabrikator zum Erstellen von Saatgut.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Изготавливает семена для будущего их использования.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een fabriceerder om zaden te produceren.";
                    else
                        return "A fabricator to produce seeds.";
                case "UseFloraFabricator":
                    if (UserLanguage == CountryCode.FR)
                        return "Utiliser le fabricateur de graînes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Utilizar el fabricador de semillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tohum Üreticisi kullanın";
                    else if (UserLanguage == CountryCode.DE)
                        return "Samenfabrikator verwenden";
                    else if (UserLanguage == CountryCode.RU)
                        return "Используйте Изготовитель семян";
                    else if (UserLanguage == CountryCode.NL)
						return "Gebruik zaden fabriceerder";
                    else
                        return "Use seeds fabricator";
                case "Posters":
                    if (UserLanguage == CountryCode.TR)
                        return "Posterler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Poster";
                    else if (UserLanguage == CountryCode.RU)
                        return "Постеры";
                    else
                        return "Posters";
                case "LabElements":
                    if (UserLanguage == CountryCode.FR)
                        return "Éléments de laboratoire";
                    else if (UserLanguage == CountryCode.ES)
                        return "Elementos de laboratorio";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar eşyaları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Laborelemente";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторные элементы";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Laboratorium-elementen";
                    else
                        return "Laboratory elements";
                case "GlassContainers":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneurs d'échantillons inutiles";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedores de muestra inútiles";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız cam kavanozlar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Glasbehälter (ohne Funktion)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стеклянные контейнеры";
                    else if (UserLanguage == CountryCode.NL)
						return "Nutteloze glazen containers";
                    else
                        return "Useless glass containers";
                case "OpenedGlassContainers":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneurs d'échantillons inutiles ouverts";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedor de muestra inútiles abiertos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız açık örnek kaplar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Offener Glasbehälter (ohne Funktion)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Открытые стеклянные контейнеры";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Open glazen containers (nutteloos)";
                    else
                        return "Useless open glass containers";
                case "NonFunctionalAnalyzers":
                    if (UserLanguage == CountryCode.FR)
                        return "Analyseurs non-fonctionnels";
                    else if (UserLanguage == CountryCode.ES)
                        return "Analizadores no funcionales";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız inceleyici";
                    else if (UserLanguage == CountryCode.DE)
                        return "Analysegeräte (ohne Funktion)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Нерабочие анализаторы";
                    else if (UserLanguage == CountryCode.NL)
						return "Non-functionele analyseerders";
                    else
                        return "Non-functional analyzers";
                case "LabFurnitures":
                    if (UserLanguage == CountryCode.FR)
                        return "Mobilier de laboratoire";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muebles de laboratorio";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar Eşyaları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Laborausstattung";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторное оборудование";
                    else if (UserLanguage == CountryCode.NL)
						return "Laboratorium meubels";
                    else
                        return "Lab furnitures";
                case "WallMonitors":
                    if (UserLanguage == CountryCode.FR)
                        return "Ordinateurs muraux";
                    else if (UserLanguage == CountryCode.ES)
                        return "Computadoras de pared";
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvar bilgisayarları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wandmonitore";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенные компьютеры";
                    else if (UserLanguage == CountryCode.NL)
						return "Muur computers";
                    else
                        return "Wall computers";
                case "CircuitBoxes":
                    if (UserLanguage == CountryCode.FR)
                        return "Boîtes de circuits";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cajas de circuitos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sigorta";
                    else if (UserLanguage == CountryCode.DE)
                        return "Schaltkästen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Узлы питания";
                    else if (UserLanguage == CountryCode.NL)
						return "Schakelings kasten";
                    else
                        return "Circuits boxes";
                case "Electronics":
                    if (UserLanguage == CountryCode.FR)
                        return "Électronique";
                    else if (UserLanguage == CountryCode.ES)
                        return "Electrónica";
                    else if (UserLanguage == CountryCode.TR)
                        return "Elektronik";
                    else if (UserLanguage == CountryCode.DE)
                        return "Elektrogeräte";
                    else if (UserLanguage == CountryCode.RU)
                        return "Электроника";
                    else if (UserLanguage == CountryCode.NL)
						return "Electronica";
                    else
                        return "Electronics";
                case "ElectronicsAndFragments":
                    if (UserLanguage == CountryCode.FR)
                        return "Électronique et fragments";
                    else if (UserLanguage == CountryCode.ES)
                        return "Electrónica y fragmentos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Elektronik ve parçalar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Elektronik und Fragmente";
                    else if (UserLanguage == CountryCode.RU)
                        return "Электроника и фрагменты";
                    else if (UserLanguage == CountryCode.NL)
						return "Electronica en fragmenten";
                    else
                        return "Electronics and fragments";
                case "CircuitBoxTab1":
                    if (UserLanguage == CountryCode.FR)
                        return "Relais de connectivité";
                    else if (UserLanguage == CountryCode.ES)
                        return "Relés de conectividad";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bağlantı Röleleri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Verbindungsrelais";
                    else if (UserLanguage == CountryCode.RU)
                        return "Реле подключения";
                    else if (UserLanguage == CountryCode.NL)
						return "Verbindings relais";
                    else
                        return "Connectivity relays";
                case "CircuitBoxTab2":
                    if (UserLanguage == CountryCode.FR)
                        return "Relais électrique haute tension";
                    else if (UserLanguage == CountryCode.ES)
                        return "Relés eléctricos de alta tensión";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yüksek voltajlı elektrik röleleri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Hochspannungsrelais";
                    else if (UserLanguage == CountryCode.RU)
                        return "Высоковольтные реле";
                    else if (UserLanguage == CountryCode.NL)
						return "Hoge voltage elektrische relais";
                    else
                        return "High voltage electrical relays";
                case "Toys":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouets";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguetes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Oyuncaklar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Spielzeuge";
                    else if (UserLanguage == CountryCode.RU)
                        return "Игрушки";
                    else if (UserLanguage == CountryCode.NL)
						return "Speelgoed";
                    else
                        return "Toys";
                case "OfficeSupplies":
                    if (UserLanguage == CountryCode.FR)
                        return "Fournitures de bureau";
                    else if (UserLanguage == CountryCode.ES)
                        return "Material de oficina";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ofis malzemeleri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Bürobedarf";
                    else if (UserLanguage == CountryCode.RU)
                        return "Канцелярные предметы";
                    else if (UserLanguage == CountryCode.NL)
						return "Kantoor spullen";
                    else
                        return "Office supplies";
                case "Accessories":
                    if (UserLanguage == CountryCode.FR)
                        return "Accessoires";
                    else if (UserLanguage == CountryCode.ES)
                        return "Accesorios";
                    else if (UserLanguage == CountryCode.TR)
                        return "Aksesuarlar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Accessories";
                    else if (UserLanguage == CountryCode.RU)
                        return "Аксессуары";
                    else if (UserLanguage == CountryCode.NL)
						return "Accessoires";
                    else
                        return "Accessories";
                case "ToysAndAccessories":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouets & accessoires";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguetes y accesorios";
                    else if (UserLanguage == CountryCode.TR)
                        return "Oyuncaklar ve aksesuarlar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Spielzeug & zubehör";
                    else if (UserLanguage == CountryCode.RU)
                        return "Игрушки и аксессуары";
                    else if (UserLanguage == CountryCode.NL)
						return "Speelgoed en accessoires";
                    else
                        return "Toys & accessories";
                case "Precursor":
                    if (UserLanguage == CountryCode.FR)
                        return "Précurseurs";
                    else if (UserLanguage == CountryCode.ES)
                        return "Precursores";
                    else if (UserLanguage == CountryCode.TR)
                        return "Öncüleri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Erbauer";
                    else if (UserLanguage == CountryCode.RU)
                        return "Архитекторы";
                    else if (UserLanguage == CountryCode.NL)
						return "Precursor"; // Thank you again, Thom. we both deciced it's more of a name so we kept it 
                    else
                        return "Precursor";
                case "PrecursorWarperParts": // Ency_Precursor_LostRiverBase_WarperParts
                    if (UserLanguage == CountryCode.FR)
                        return "Morceaux de warper";
                    else if (UserLanguage == CountryCode.ES)
                        return "Partes de curvador";
                    else if (UserLanguage == CountryCode.TR)
                        return "S\u0131\u00e7ray\u0131c\u0131 par\u00e7alar\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Warper-Teile";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0427\u0430\u0441\u0442\u0438 \u0442\u0435\u043b\u0430 \u0441\u0442\u0440\u0430\u0436\u0430";
                    else if (UserLanguage == CountryCode.NL)
						return "Warper onderdelen";
                    else
                        return "Warper parts";
                case "PrecursorKeys":
                    if (UserLanguage == CountryCode.FR)
                        return "Tablettes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tablillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tabletler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tafel";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скрижали";
                    else if (UserLanguage == CountryCode.NL)
						return "Tabletten";
                    else
                        return "Tablets";
                case "Weapons":
                    if (UserLanguage == CountryCode.FR)
                        return "Armes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Armas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Öncü";
                    else if (UserLanguage == CountryCode.DE)
                        return "Waffen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Оружие";
                    else if (UserLanguage == CountryCode.NL)
						return "Wapens";
                    else
                        return "Weapons";
                case "Relics":
                    if (UserLanguage == CountryCode.FR)
                        return "Reliques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Reliquias";
                    else if (UserLanguage == CountryCode.TR)
                        return "Emanetler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Relikte";
                    else if (UserLanguage == CountryCode.RU)
                        return "Реликвии";
                    else if (UserLanguage == CountryCode.NL)
						return "Relikwieën";
                    else
                        return "Relics";
                case "LeviathansTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Léviathans";
                    else if (UserLanguage == CountryCode.ES)
                        return "Leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Canavar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Leviathan";
                    else if (UserLanguage == CountryCode.RU)
                        return "Левиафан";
                    else if (UserLanguage == CountryCode.NL)
						return "Leviatan";
                    else
                        return "Leviathan";
                case "FloraTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Flore alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Flora alienígena";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı florası";
                    else if (UserLanguage == CountryCode.DE)
                        return "Außerirdische Flora";
                    else if (UserLanguage == CountryCode.RU)
                        return "Флора пришельцев";
                    else if (UserLanguage == CountryCode.NL)
						return "Alien flora";
                    else
                        return "Alien flora";
                case "PlantAirTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Pflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растения";
                    else if (UserLanguage == CountryCode.NL)
						return "Planten";
                    else
                        return "Plants";
                case "TreeAirTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbres";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árboles";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ağaçlar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Bäume";
                    else if (UserLanguage == CountryCode.RU)
                        return "Деревья";
                    else if (UserLanguage == CountryCode.NL)
						return "Bomen";
                    else
                        return "Trees";
                case "TropicalPlantTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes tropicales";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas tropicales";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Pflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропические растения";
                    else if (UserLanguage == CountryCode.NL)
						return "Tropische planten";
                    else
                        return "Tropical plants";
                case "PlantWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas acuáticas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Su bitkileri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Подводные растения";
                    else if (UserLanguage == CountryCode.NL)
						return "Aquatische planten";
                    else
                        return "Aquatic plants";
                case "TreeWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbres aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árboles acuáticos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Su ağaçları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserbäume";
                    else if (UserLanguage == CountryCode.RU)
                        return "Подводные деревья";
                    else if (UserLanguage == CountryCode.NL)
						return "Aquatische bomen";
                    else
                        return "Aquatic trees";
                case "CoralWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Coraux";
                    else if (UserLanguage == CountryCode.ES)
                        return "Corales";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mercanlar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Korallen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кораллы";
                    else if (UserLanguage == CountryCode.NL)
						return "Koralen";
                    else
                        return "Corals";
                case "AmphibiousPlantsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes amphibies";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas anfibias";
                    else if (UserLanguage == CountryCode.TR)
                        return "Amfibi bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Amphibische Pflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растения-амфибии";
                    else if (UserLanguage == CountryCode.NL)
						return "Amfibische planten";
                    else
                        return "Amphibious plants";
                case "RedGrassesTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Herbes sanguines";
                    else if (UserLanguage == CountryCode.ES)
                        return "Hierbas de sangre";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kan otları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Blutgräser";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кровавая трава";
                    else if (UserLanguage == CountryCode.NL)
						return "Bloed grassen";
                    else
                        return "Blood grasses";
                case "EggsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "\u0152ufs de cr\u00e9atures";
                    else if (UserLanguage == CountryCode.ES)
                        return "Huevos de criaturas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yaratıklar yumurta";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kreaturen Eier";
                    else if (UserLanguage == CountryCode.RU)
                        return "Яйца существ";
                    else if (UserLanguage == CountryCode.NL)
						return "Wezen eieren";
                    else
                        return "Creatures eggs";
                case "DmgCreatureEggsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Cr\u00e9atures aggressives";
                    else if (UserLanguage == CountryCode.ES)
                        return "Criaturas agresivas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Saldırgan yaratıklar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Aggressive Kreaturen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Агрессивные существа";
                    else if (UserLanguage == CountryCode.NL)
						return "Agressieve wezens";
                    else
                        return "Aggressive creatures";
                case "NonDmgCreatureEggsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Cr\u00e9atures pacifiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Criaturas pacíficas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Huzurlu yaratıklar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Friedliche Kreaturen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Мирные существа";
                    else if (UserLanguage == CountryCode.NL)
						return "Vredige wezens";
                    else
                        return "Peaceful creatures";
                case "LabContainer4Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneur d'échantillons cylindrique inversé";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedor de muestra cilíndrico invertido";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ters örnek kavanozu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Umgekehrter zylindrischer Probenbehälter";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стеклянный контейнер для образцов";
                    else if (UserLanguage == CountryCode.NL)
						return "Omgekeerde cylindrische monster container";
                    else
                        return "Inverted cylindrical sample container";
                case "LabContainer4Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un conteneur d'échantillons cylindrique inversé, probablement inutile.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un contenedor de muestra cilíndrico invertido, probablemente inútil.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ters çevrilmiş bir örnek kavanozu, muhtemelen yararsız.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein umgekehrter zylindrischer Probenbehälter, wahrscheinlich nutzlos.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стеклянный контейнер для образцов, вероятно, бесполезен.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een omgekeerde cylindrische monster container, waarschijnlijk nutteloos.";
                    else
                        return "An inverted cylindrical sample container, probably useless.";
                case "SmallLabContainerOpenName":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit conteneur d'échantillons (ouvert)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedor de muestra pequeño (abierto)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük örnek kabı (açık)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleiner Probenbehälter (offen)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Маленький контейнер для образцов (открытый)";
                    else if (UserLanguage == CountryCode.NL)
						return "Klein monster container (open)";
                    else
                        return "Small sample container (open)";
                case "MediumLabContainerOpenName":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneur d'échantillons (ouvert)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedor de muestra (abierto)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Numune kabı (açık)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probenbehälter (offen)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Контейнер для образцов (открытый)";
                    else if (UserLanguage == CountryCode.NL)
						return "Monster container (open)";
                    else
                        return "Sample container (open)";
                case "LargeLabContainerOpenName":
                    if (UserLanguage == CountryCode.FR)
                        return "Grand conteneur d'échantillons (ouvert)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedor de muestra grande (abierto)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük örnek kabı (açık)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Großer Probenbehälter (offen)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Большой контейнер для образцов (открытый)";
                    else if (UserLanguage == CountryCode.NL)
						return "Grote monster container (open)";
                    else
                        return "Large sample container (open)";
                case "LabContainerOpenDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un conteneur d'échantillons ouvert, probablement inutile.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un contenedor de muestra abierto, probablemente inútil.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Açık bir örnek kap, muhtemelen işe yaramaz.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein offener Probenbehälter, wahrscheinlich nutzlos.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Открытый контейнер для образцов, вероятно, бесполезен.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een open monster container, waarschijnlijk nutteloos.";
                    else
                        return "An open sample container, probably useless.";
                case "LabTubeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Étagères tubulaires de laboratoire (non-fonctionnelles)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes tubulares de laboratorio (no funcionales)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Boru şeklinde raflar (yararsız)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Röhrenartiger Laborschrank (nicht funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторные полки";
                    else if (UserLanguage == CountryCode.NL)
						return "Buisvormig laboratorium kast (niet functioneel)";
                    else
                        return "Tubular laboratory shelves (non-functional)";
                case "LabTubeDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des étagères tubulaires pour stocker des échantillons.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes tubulares para almacenar muestras.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnekleri depolamak için boru şeklinde raflar.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Schrank in Form einer Röhre zur Aufbewahrung von Proben.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративная стойка лабораторных полок для образцов.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een buisvormige kast om monsters te bewaren.";
                    else
                        return "Tubular shelves for storing samples.";
                case "LabCartName":
                    if (UserLanguage == CountryCode.FR)
                        return "Chariot de laboratoire";
                    else if (UserLanguage == CountryCode.ES)
                        return "Carro de laboratorio";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labatuvar arabası";
                    else if (UserLanguage == CountryCode.DE)
                        return "Laborwagen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторная тележка";
                    else if (UserLanguage == CountryCode.NL)
						return "Laboratorium wagen";
                    else
                        return "Lab cart";
                case "LabCartDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un chariot à échantillons de laboratoire.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un carro de muestra de laboratorio.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir örnek arabası.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Laborprobenwagen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративная лабораторная тележка для образцов и инструментов.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een laboratorium monster wagen."; 
                    else
                        return "A laboratory sample cart.";
                case "LabShelfName":
                    if (UserLanguage == CountryCode.FR)
                        return "Étagères de laboratoire (non-fonctionnelles)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes de laboratorio (no funcionales)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar rafı (yararsız)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Laborregale (nicht funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторный шкаф";
                    else if (UserLanguage == CountryCode.NL)
						return "Laboratorium planken (niet functioneel)";
                    else
                        return "Laboratory shelves (non-functional)";
                case "LabShelfDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des étagères pour stocker des échantillons.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes para almacenar muestras.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnekleri depolamak için raf.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Regale für die Aufbewahrung von Proben.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный лабораторный шкаф для образцов.";
                    else if (UserLanguage == CountryCode.NL)
						return "Planken om monsters te bewaren.";
                    else
                        return "Shelves for storing samples.";
                case "WallMonitor1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Moniteur mural";
                    else if (UserLanguage == CountryCode.ES)
                        return "Monitor de pared";
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvar monitörü";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wandmonitor";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный монитор";
                    else if (UserLanguage == CountryCode.NL)
						return "Wand monitor";
                    else
                        return "Wall monitor";
                case "WallMonitor1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un moniteur mural (doit être relié à un serveur pour fonctionner).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un monitor de pared (debe estar conectado a un servidor para funcionar).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir duvar monitörü (kullanılması için bir sunucuya bağlanılması gerekiyor).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Wandmonitor (muss mit einem Server verbunden sein, um zu funktionieren).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный информационный монитор (можно подключить к серверу для работы).";
                    else if (UserLanguage == CountryCode.NL)
						return "Een wand monitor (moet aan een server vebonden zijn om te werken).";
                    else
                        return "A wall monitor (must be connected to a server to work).";
                case "WallMonitor2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Ordinateur mural simple";
                    else if (UserLanguage == CountryCode.ES)
                        return "Computadora de pared simple";
                    else if (UserLanguage == CountryCode.TR)
                        return "Basit duvar monitörü";
                    else if (UserLanguage == CountryCode.DE)
                        return "Simpler Wandcomputer";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный портативный компьютер";
                    else if (UserLanguage == CountryCode.NL)
						return "Simpele wand computer";
                    else
                        return "Simple wall computer";
                case "WallMonitor2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit ordinateur mural simple (doit être relié à un serveur pour fonctionner).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una computadora simple montada en la pared (debe estar conectada a un servidor para funcionar).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük, basit duvara monte edilen bilgisayar (kullanılması için bir sunucuya bağlanılması gerekiyor).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein kleiner, einfacher an der Wand befestigter Computer (muss mit einem Server verbunden sein, um zu funktionieren).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный портативный компьютер (можно подключить к серверу для работы).";
                    else if (UserLanguage == CountryCode.NL)
						return "Een kleine, simpele wand bevestigde computer (moet aan een server verbonden zijn om te werken).";
                    else
                        return "A small, simple wall-mounted computer (must be connected to a server to function).";
                case "WallMonitor3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Ordinateur mural";
                    else if (UserLanguage == CountryCode.ES)
                        return "Computadora de pared";
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvar bilgisayarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wandcomputer";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный компьютер";
                    else if (UserLanguage == CountryCode.NL)
						return "Wand computer";
                    else
                        return "Wall computer";
                case "WallMonitor3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un ordinateur mural performant (doit être relié à un serveur pour fonctionner).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una poderosa computadora montada en la pared (debe estar conectada a un servidor para funcionar).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Güçlü duvara monte edilen bilgisayar (kullanılması için bir sunucuya bağlanılması gerekiyor).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein leistungsfähiger an der Wand befestigter Computer (muss mit einem Server verbunden sein, um zu funktionieren).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный компьютер (можно подключить к серверу для работы).";
                    else if (UserLanguage == CountryCode.NL)
						return "Een krachtige wand bevestigde computer (moet aan een server verbonden zijn om te werken).";
                    else
                        return "A powerful wall-mounted computer (must be connected to a server to function).";
                case "CircuitBox1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Boîte de circuits";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de circuitos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sigorta";
                    else if (UserLanguage == CountryCode.DE)
                        return "Schaltkasten";
                    else if (UserLanguage == CountryCode.RU)
                        return "Распределительная коробка";
                    else if (UserLanguage == CountryCode.NL)
						return "Schakelings kast";
                    else
                        return "Circuits box";
                case "CircuitBox1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Une boîte de circuits simple (permet la mise sous tension des appareils électriques).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de circuitos simple (permite la alimentación de dispositivos eléctricos).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Basit sigorta (elektrikli eşyalara güç iletimi sağlar).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein einfacher Schaltkasten (ermöglicht das Einschalten von elektrischen Geräten).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Простая распределительная коробка (позволяет питать электрические устройства).";
                    else if (UserLanguage == CountryCode.NL)
						return "Een simpele schakelings kast (staat aanzetten van elektrische apparatuur toe).";
                    else
                        return "A simple circuit box (allows powering of electrical devices).";
                case "CircuitBox2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Relai de connectivité";
                    else if (UserLanguage == CountryCode.ES)
                        return "Relé de conectividad";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bağlantı Rölesi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Verbindungsrelais";
                    else if (UserLanguage == CountryCode.RU)
                        return "Реле подключения";
                    else if (UserLanguage == CountryCode.NL)
						return "Verbindings relais";
                    else
                        return "Connectivity relay";
                case "CircuitBox2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un relai permettant l'interconnexion des équipements.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un relevo para la interconexión de equipos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Eşyaların ara iletişimi için röle.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Relais für die Verbindung von Geräten.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Реле для подключения оборудования от сети.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een relais voor de interconnectie van apparatuur.";
                    else
                        return "A relay for the interconnection of equipment.";
                case "CircuitBox3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Relai électrique haute tension";
                    else if (UserLanguage == CountryCode.ES)
                        return "Relé eléctrico de alta tensión";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yüksek voltajlı elektrik rölesi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Hochspannungsrelais";
                    else if (UserLanguage == CountryCode.RU)
                        return "Высоковольтное реле";
                    else if (UserLanguage == CountryCode.NL)
						return "Hoge voltage elektrische relais";
                    else
                        return "High voltage electrical relay";
                case "CircuitBox3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un composant permettant l'acheminement de grandes quantités d'énergie.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un componente que permite el transporte de grandes cantidades de energía.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük enerjileri transfer etmek için kullanılan kutu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein elektrisches Verbindungselement, das den Transport großer Energiemengen ermöglicht.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Компонент, позволяющий передавать большое количество энергии.";
                    else if (UserLanguage == CountryCode.NL)
						return "Een onderdeel dat het vervoeren van grote hoeveelheden energie toe staat.";
                    else
                        return "A component allowing the transport of large amounts of energy.";
                case "SpecimenAnalyzerName":
                    if (UserLanguage == CountryCode.FR)
                        return "Analyseur de spécimen";
                    else if (UserLanguage == CountryCode.ES)
                        return "Analizador de especímenes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnek İnceleyici";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probenanalysator";
                    else if (UserLanguage == CountryCode.RU)
                        return "Анализатор образцов";
                    else if (UserLanguage == CountryCode.NL)
						return "Exemplaar analyseerder";
                    else
                        return "Specimen analyzer";
                case "SpecimenAnalyzerDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Étudie des spécimens pour en déduire des schémas de fabrication.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estudie especímenes para deducir patrones de comportamiento.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Üretim kalıplarını anlamak için örnekler üzerinde çalışır.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Gerät zur Untersuchung von Proben, um Herstellungsmuster abzuleiten.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Позволяет изучать образцы для их последующего производства.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Bestudeer exemplaren om patronen van aanmaak te herkennen.";
                    else
                        return "Study specimens to deduce patterns of manufacture.";
                case "SmallEmperorName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée d'empereur léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeca de leviatán emperador";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz İmparatoru oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seeimperator-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла левиафана «Морской Император»";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Emperor leviathan pop";
                    else
                        return "Emperor leviathan doll";
                case "SmallEmperorDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée d'empereur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca de emperador leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu Deniz İmparatoru oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Seeimperator-Puppe wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эта кукла была создана по результатам научных наблюдений на 4546B.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze emperor leviathan pop was gecreëerd van observaties gemaakt op 4546B.";
                    else
                        return "This emperor leviathan doll was created from observations made on 4546B.";
                case "MarlaCatName":
                    if (UserLanguage == CountryCode.RU)
                        return "Марла";
                    else
                        return "Marla";
                case "MarlaCatDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Le chat de EatMyDiction.";
                    else if (UserLanguage == CountryCode.ES)
                        return "El gato de EatMyDiction.";
                    else if (UserLanguage == CountryCode.TR)
                        return "EatMyDiction'un kedisi.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Friss-meine-Aussprache-Katze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла кошки «EatMyDiction».";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Eat My Diction's kat.";
                    else
                        return "Eat My Diction's cat.";
                case "MarkiDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Une poupée peu commune";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un muñeco inusual";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız oyuncak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine ungewöhnliche Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Необычная кукла";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een ongewone pop";
                    else
                        return "An unusual doll";
                case "MarkiDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une poupée peu commune.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un muñeco inusual.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız bir oyuncak.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine ungewöhnliche Puppe.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Какая-то странная и необычная кукла.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een ongewone pop.";
                    else
                        return "An unusual doll.";
                case "JackSepticEyeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneur de JackSepticEye";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tanque de JackSeptic";
                    else if (UserLanguage == CountryCode.TR)
                        return "Jack's Septic Tüpü";
                    else if (UserLanguage == CountryCode.DE)
                        return "Jacks Septischer Tank";
                    else if (UserLanguage == CountryCode.RU)
                        return "Отстойник Джека";
                    else if (UserLanguage == CountryCode.NL)
                    	return "JackSepticEye's Tank";
                    else
                        return "Jack's Septic Tank";
                case "JackSepticEyeDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un objet peu commun.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un objeto inusual.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız bir eşya.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein ungewöhnlicher Gegenstand.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Модель талисмана Septiceye Sam в банке.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een ongewoon object.";
                    else
                        return "An unusual item.";
                case "LeviathanDolls":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupées de léviathans";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñecos de Leviatanes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Canavar oyuncakları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Leviathan-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Куклы Левиафанов";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Leviatan poppen";
                    else
                        return "Leviathan dolls";
                case "GhostLeviathanDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de léviathan fantôme";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeco de Leviatán Fantasma";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hayalet canavar oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Puppe eines Phantom-Leviathans";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Призрачного Левиафана";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Ghost leviatan pop";
                    else
                        return "Ghost leviathan doll";
                case "GhostLeviathanDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de léviathan fantôme a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este muñeco de leviatán fantasma fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu hayalet canavar oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Phantom-Leviathans wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эта кукла была создана по результатам научных наблюдений на 4546B.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze ghost leviathan pop was gecreëerd van observaties gemaakt op 4546B.";
                    else
                        return "This ghost leviathan doll was created from observations made on 4546B.";
                case "ReaperLeviathanDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de faucheur léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeco de Leviatán Segador";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı canavar oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Cheliceratops-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла левиафана «Жнец»";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Reaper leviathan pop"; // no real translation for reaper 
                    else
                        return "Reaper leviathan doll";
                case "ReaperLeviathanDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de faucheur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este muñeco de leviatán segador fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu tırpanlı canavar oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Cheliceratops wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эта кукла была создана по результатам научных наблюдений на 4546B.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze reaper leviathan pop was gecreëerd van observaties gemaakt op 4546B.";
                    else
                        return "This reaper leviathan doll was created from observations made on 4546B.";
                case "SeaDragonDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de dragon des mers léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeco de dragón marino leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz ejderhası oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seedrachen-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Морского Дракона";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sea dragon leviathan pop";
                    else
                        return "Sea dragon leviathan doll";
                case "SeaDragonDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de dragon des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este muñeco de dragón marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu deniz ejderhası oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Seedrachen wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эта кукла была создана по результатам научных наблюдений на 4546B.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze sea dragon leviathan pop was gecreëerd van observaties gemaakt op 4546B.";
                    else
                        return "This sea dragon leviathan doll was created from observations made on 4546B.";
                case "SeaTreaderDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de pèlerin des mers léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeco de Caminante Marino leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz gezgini oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seewanderer-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Морского Топтуна";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sea threader leviathan pop";
                    else
                        return "Sea treader leviathan doll";
                case "SeaTreaderDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de pèlerin des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este muñeco de caminante marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu deniz gezgini oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Seewanderers wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эта кукла была создана по результатам научных наблюдений на 4546B.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze sea treader leviathan pop was gecreëerd van observaties gemaakt op 4546B.";
                    else
                        return "This sea treader leviathan doll was created from observations made on 4546B.";
                case "ReefBackDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de reefback léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeco de Portarrecifes Leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Resif devi oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Riffrücken-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Рифоспина";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Reefback leviathan pop";
                    else
                        return "Reefback leviathan doll";
                case "ReefBackDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de reefback léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este muñeco de portarrecifes leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu resif devi oyuncağı 4546B'deki gözlemlerle yapıldı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Riffrückens wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эта кукла была создана по результатам научных наблюдений на 4546B.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze reefback leviathan pop was gecreëerd van observaties gemaakt op 4546B.";
                    else
                        return "This reefback leviathan doll was created from observations made on 4546B.";
                case "CuddleFishDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de câlineur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeco de pez monada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sevimli balık oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Knuddelfisch-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Ласки";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Cuddlefish pop";
                    else
                        return "Cuddlefish doll";
                case "CuddleFishDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de câlineur a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este muñeco de pez monada fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu sevimli balık oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Knuddelfischs wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эта кукла была создана по результатам научных наблюдений на 4546B.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze cuddlefish pop was gecreëerd van observaties gemaakt op 4546B.";
                    else
                        return "This cuddlefish doll was created from observations made on 4546B.";
                case "ReactorLampName":
                    if (UserLanguage == CountryCode.FR)
                        return "Lampe (lumière customizable)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Lámpara (luz personalizable)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Lamba (özelleştirilebilir ışık)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lampe (anpassbares Licht)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лампа (настраиваемый свет)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Lamp (aanpasbaar licht)";
                    else
                        return "Lamp (customizable light)";
                case "ReactorLampDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une lampe customisable. Peut être construit à l'extérieur et à l'intérieur." + Environment.NewLine + Environment.NewLine + "Utilisation : " + GetFriendlyWord("LampTooltipCompact") + Environment.NewLine + Environment.NewLine + "PS: Si vous téléchargez le mod « Base Light Switch » vous pourrez éteindre la lumière par défaut de votre base ce qui vous permettra de profiter pleinement de ces lampes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una lámpara personalizable. Se puede construir en interiores y exteriores." + Environment.NewLine + Environment.NewLine + "Uso: " + GetFriendlyWord("LampTooltipCompact");
                    else if (UserLanguage == CountryCode.TR)
                        return "Özelleştirilebilir lamba. İç ve dış mekanlarda inşa edilebilir." + Environment.NewLine + Environment.NewLine + "Kullanımı: " + GetFriendlyWord("LampTooltipCompact");
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine anpassbare Lampe. Kann drinnen und draußen gebaut werden." + Environment.NewLine + Environment.NewLine + "Verwendungszweck: " + GetFriendlyWord("LampTooltipCompact");
                    else if (UserLanguage == CountryCode.RU)
                        return "Настраиваемая лампа. Может быть построена внутри и снаружи." + Environment.NewLine + Environment.NewLine + "Применение: " + GetFriendlyWord("LampTooltipCompact");
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een aanpasbare lamp. Kan binnen en buiten gebouwd worden." + Environment.NewLine + Environment.NewLine + "Gebruik: " + GetFriendlyWord("LampTooltipCompact");
                    else
                        return "A customizable lamp. Can be built indoor and outdoor." + Environment.NewLine + Environment.NewLine + "Usage: " + GetFriendlyWord("LampTooltipCompact") + Environment.NewLine + Environment.NewLine + "PS: If you download the « Base Light Switch » mod you'll be able to turn off base default light and thus enjoy this lamp even more.";
                case "SeamothDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouet Seamoth";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguete del Seamoth";
                    else if (UserLanguage == CountryCode.TR)
                        return "Seamoth oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seemotten-Spielzeug";
                    else if (UserLanguage == CountryCode.RU)
                        return "Игрушечный Мотылёк";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Seamoth speelgoed";
                    else
                        return "Seamoth toy";
                case "SeamothDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une miniature décorative du seamoth. Cliquez dessus pour changer de modèle.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura decorativa de seamoth. Haga clic en él para cambiar de modelo.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Minyatür seamoth oyuncağı. Modeli değiştirmek için üzerine tıklayın.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine dekorative Miniatur der Seemotte. Klicken Sie darauf, um das Modell zu ändern.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративная кукла для интерьера. Нажмите на нее, чтобы изменить модель.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een decoratief miniatuur van de seamoth. Click er op om de model te veranderen.";
                    else
                        return "A decorative miniature of the seamoth. Click on it to change its model.";
                case "ExosuitDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouet combinaison PRAWN";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguete del traje PRAWN";
                    else if (UserLanguage == CountryCode.TR)
                        return "Suban giysi oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Krebsanzug-Spielzeug";
                    else if (UserLanguage == CountryCode.RU)
                        return "Игрушечный костюм КРАБ";
                    else if (UserLanguage == CountryCode.NL)
                    	return "PRAWN suit speelgoed";
                    else
                        return "PRAWN suit toy";
                case "ExosuitDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une miniature décorative de la combinaison PRAWN. " + GetFriendlyWord("SwitchExosuitModel");
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura decorativa de traje PRAWN. " + GetFriendlyWord("SwitchExosuitModel");
                    else if (UserLanguage == CountryCode.TR)
                        return "Minyatür suban giysi oyuncağı. " + GetFriendlyWord("SwitchExosuitModel");
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine dekorative Miniatur des Krebs-Anzuges. " + GetFriendlyWord("SwitchExosuitModel");
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративная миниатюра костюма КРАБ. " + GetFriendlyWord("SwitchExosuitModel");
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een decoratief miniatuur van de PRAWN suit. " + GetFriendlyWord("SwitchExosuitModel");
                    else
                        return "A decorative miniature of the PRAWN suit. " + GetFriendlyWord("SwitchExosuitModel");
                case "ForkLiftDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Monte-charge (non fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Montacargas (no funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Forklift (yararsız)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Gabelstapler (nicht funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Грузоподъемник";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Heftruck (niet functioneel)";
                    else
                        return "Forklift (non-functional)";
                case "ForkLiftDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un monte-charge décoratif. " + GetFriendlyWord("AdjustForkliftSize");
                    else if (UserLanguage == CountryCode.ES)
                        return "Un montacargas decorativo. " + GetFriendlyWord("AdjustForkliftSize");
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekoratif forklift. " + GetFriendlyWord("AdjustForkliftSize");
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein dekorativer Gabelstapler. " + GetFriendlyWord("AdjustForkliftSize");
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный грузоподъемник. " + GetFriendlyWord("AdjustForkliftSize");
                    else if (UserLanguage == CountryCode.NL)
                        return "Een decoratieve heftruck. " + GetFriendlyWord("AjustForkliftSize");
                    else
                        return "A decorative forklift. " + GetFriendlyWord("AdjustForkliftSize");
                case "DrinksAndFood":
                    if (UserLanguage == CountryCode.FR)
                        return "Boissons & nourriture";
                    else if (UserLanguage == CountryCode.ES)
                        return "Bebidas & comida";
                    else if (UserLanguage == CountryCode.TR)
                        return "İçecek & Yiyecekler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Essen & Trinken";
                    else if (UserLanguage == CountryCode.RU)
                        return "Напитки и Еда";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Eten & drinken";
                    else
                        return "Drinks & food";
                case "BarBottleName":
                    if (UserLanguage == CountryCode.FR)
                        return "Bouteille de bar";
                    else if (UserLanguage == CountryCode.ES)
                        return "Botella de bar";
                    else if (UserLanguage == CountryCode.TR)
                        return "Şişe";
                    else if (UserLanguage == CountryCode.DE)
                        return "Barflasche";
                    else if (UserLanguage == CountryCode.RU)
                        return "Бутылка из бара";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Bar fles";
                    else
                        return "Bar bottle";
                case "BarBottleDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une bouteille contenant un délicieux breuvage.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una botella que contiene una deliciosa bebida.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Lezzetli bir içecek barındıran şişe.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Flasche mit einem leckeren Getränk.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Бутылка содержащая вкусный напиток.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een fles met heerlijk drinken er in.";
                    else
                        return "A bottle containing a delicious beverage.";
                case "BarCup2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Gobelet";
                    else if (UserLanguage == CountryCode.ES)
                        return "Taza";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bardak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tasse";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стакан";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Beker";
                    else
                        return "Cup";
                case "BarCup2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un gobelet fait de titane.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una taza hecha de titanio.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Titanyum bardak.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Tasse aus titanium.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стакан, сделанный из титана.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een beker gemaakt van titanium.";
                    else
                        return "A cup made of titanium.";
                case "BarCup1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit gobelet";
                    else if (UserLanguage == CountryCode.ES)
                        return "Taza pequeña";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bardak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Tasse";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стопка";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine beker";
                    else
                        return "Small cup";
                case "BarCup1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit gobelet fait de titane.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una pequeña taza hecha de titanio.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük titanyum bardak.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine kleine Tasse aus Titanium.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Небольша стакан, сделанный из титана.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een kleine beker gemaakt van titanium.";
                    else
                        return "A small cup made of titanium.";
                case "BarFood1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit plat";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plato pequeño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük yemek";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Mahlzeit";
                    else if (UserLanguage == CountryCode.RU)
                        return "Рагу";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine maaltijd";
                    else
                        return "Small meal";
                case "BarFood1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un plat à base de poisson.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una comida hecha de pescado.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Balıktan yapılmış bir yemek.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Mahlzeit aus Fisch.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Блюдо из небольших тушёных кусочков рыбы.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een maaltijd gemaakt van vis";
                    else
                        return "A meal made of fish.";
                case "BarFood2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plateau repas";
                    else if (UserLanguage == CountryCode.ES)
                        return "Bandeja de comida";
                    else if (UserLanguage == CountryCode.TR)
                        return "1 tepsi yemek";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tablett mit Essen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Поднос с едой";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Maaltijd blad";
                    else
                        return "Meal tray";
                case "BarFood2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un repas complet et équilibré.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una comida completa y equilibrada.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dengeli bir öğün.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine komplette und ausgewogene Mahlzeit.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Полноценный обед, приготовленный из доступных растений и рыбы.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een kompleet en gebalanceerde maaltijd";
                    else
                        return "A complete and balanced meal.";
                case "BarNapkinsName":
                    if (UserLanguage == CountryCode.FR)
                        return "Serviettes de table";
                    else if (UserLanguage == CountryCode.ES)
                        return "Servilletas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Peçete";
                    else if (UserLanguage == CountryCode.DE)
                        return "Servietten";
                    else if (UserLanguage == CountryCode.RU)
                        return "Салфетки";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Servetten";
                    else
                        return "Napkins";
                case "BarNapkinsDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des serviettes de table en maille de fibre.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Servilletas de malla de fibra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Fiber örgüden yapılmış peçete.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Servietten aus Fasergewebe.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Салфетки, сделанные из сетчатого волокна.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Servetten gemaakt van vezelgaas";
                    else
                        return "Napkins made of fiber mesh.";
                case "LabRobotArmName":
                    if (UserLanguage == CountryCode.FR)
                        return "Bras robot (non fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Brazo robótico (no funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız Robot Kolu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Roboterarm (nicht funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторный манипулятор(нерабочий)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Robot arm (niet functioneel)";
                    else
                        return "Robot arm (non-functional)";
                case "LabRobotArmDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un bras robot de laboratoire (non fonctionnel).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un brazo robótico de laboratorio (no funcional).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar robotu kolu (yararsız).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Laborroboterarm (nicht funktionsfähig).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоратичная лабораторная рука-манипулятор (нерабочая).";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een labratorium robot arm (niet functioneel).";
                    else
                        return "A laboratory robot arm (non-functional).";
                case "ReaperSkullDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Crâne de faucheur léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cráneo de segador leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı canavar kafatası";
                    else if (UserLanguage == CountryCode.DE)
                        return "Cheliceratops-Schädel";
                    else if (UserLanguage == CountryCode.RU)
                        return "Череп левиафана «Жнец»";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Reaper leviathan schedel";
                    else
                        return "Reaper leviathan skull";
                case "ReaperSkullDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une réplique de crâne d'un faucheur léviathan.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una réplica del cráneo de un segador leviatán.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı canavar kafatasının bir replikası.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Replik von einem Cheliceratops-Schädel.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Репродукция черепа левиафана «Жнец».";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een replica van een reaper leviatan schedel";
                    else
                        return "A replica of a reaper leviathan skull.";
                case "LampTooltip":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour allumer/éteindre, ou :" + Environment.NewLine +
                               "Maintenez 'R' et cliquez pour changer le niveau de rouge." + Environment.NewLine +
                               "Maintenez 'G' et cliquez pour changer le niveau de vert." + Environment.NewLine +
                               "Maintenez 'B' et cliquez pour changer le niveau de bleu." + Environment.NewLine +
                               "Maintenez 'F' et cliquez pour changer la portée." + Environment.NewLine +
                               "Maintenez 'I' et cliquez pour changer l'intensité." + Environment.NewLine +
                               "Maintenez 'T' et cliquez pour changer l'intensité du néon." + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour changer la couleur du néon." + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para activar/desactivar, o:" + Environment.NewLine +
                               "Mantenga 'R' y haga clic para cambiar los niveles rojos." + Environment.NewLine +
                               "Mantenga 'G' y haga clic para cambiar los niveles verdes." + Environment.NewLine +
                               "Mantenga 'B' y haga clic para cambiar los niveles azules." + Environment.NewLine +
                               "Mantenga 'F' y haga clic para cambiar el alcance." + Environment.NewLine +
                               "Mantenga 'I' y haga clic para cambiar la intensidad." + Environment.NewLine +
                               "Mantenga 'T' y haga clic para cambiar la intensidad del neón." + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar el color del neón." + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Açmak/kapatmak için tıklayın, ya da:" + Environment.NewLine +
                               "Kırmızı rengi değiştirmek için 'R' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Yeşil rengi değiştirmek için 'G' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Mavi rengi değiştirmek için 'B' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Aralığı değiştirmek için 'F' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Yoğunluğu değiştirmek için 'I' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Neon tüp yoğunluğu değiştirmek için 'T' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Neon tüp rengini değiştirmek için 'E' tuşuna basarken sol tıklayın." + Environment.NewLine;
                    else if (UserLanguage == CountryCode.DE)
                        return "Zum ein- oder auszuschalten klicken, oder:" + Environment.NewLine +
                               "'R' drücken und klicken, um die Rotstufe zu ändern." + Environment.NewLine +
                               "'G' drücken und klicken, um die Grünstufe zu ändern." + Environment.NewLine +
                               "'B' drücken und klicken, um die Blaustufe zu ändern." + Environment.NewLine +
                               "'F' drücken und klickenn Sie, um die Lichtstärke anzupassen." + Environment.NewLine +
                               "'I' drücken und klicken, um die Intensität zu ändern." + Environment.NewLine +
                               "'T' drücken und klicken, um die Intensität der Neonröhre zu ändern." + Environment.NewLine +
                               "'E' drücken und klickenn Sie, um die Neonröhrenfarbe zu ändern." + Environment.NewLine;
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите, чтобы настроить:" + Environment.NewLine +
                               "Удерживайте 'R' и выберете уровень красного." + Environment.NewLine +
                               "Удерживайте 'G' и выберете уровень зеленого." + Environment.NewLine +
                               "Удерживайте 'B' выберете уровень синего." + Environment.NewLine +
                               "Удерживайте 'F' выберете уровень яркости." + Environment.NewLine +
                               "Удерживайте 'I' выберете уровень интенсивность света." + Environment.NewLine +
                               "Удерживайте 'T' выберете уровень интенсивности неона лампы." + Environment.NewLine +
                               "Удерживайте 'E' выберете цвет неона лампы." + Environment.NewLine;
					else if (UserLanguage == CountryCode.NL)
						return "Klikken om aan/uit te zetten, of:" + Environment.NewLine +
							   "Houd 'R' vast en klik om rood niveau te veranderen." + Environment.NewLine +
							   "Houd 'G' vast en klik om groen niveau te veranderen." + Environment.NewLine +
							   "Houd 'B' vast en klik om blauw niveau te veranderen." + Environment.NewLine +
							   "Houd 'F' vast en klik om licht bereik te veranderen." + Environment.NewLine +
							   "Houd 'I' vast en klik om licht intensiteit te veranderen." + Environment.NewLine +
							   "Houd 'T' vast en klik om neon buis intensiteit te veranderen." + Environment.NewLine + 
							   "Houd 'E' vast en klik om neon buis kleur te veranderen." + Environment.NewLine;
                    else
                        return "Click to turn on/off, or:" + Environment.NewLine +
                               "Hold 'R' and click to change red levels." + Environment.NewLine +
                               "Hold 'G' and click to change green levels." + Environment.NewLine +
                               "Hold 'B' and click to change blue levels." + Environment.NewLine +
                               "Hold 'F' and click to change light range." + Environment.NewLine +
                               "Hold 'I' and click to change light intensity." + Environment.NewLine +
                               "Hold 'T' and click to change neon tube intensity." + Environment.NewLine +
                               "Hold 'E' and click to change neon tube color." + Environment.NewLine;
                case "LampTooltipCompact":
                    if (UserLanguage == CountryCode.FR)
                        return "Clic: ON/OFF, R+Clic: rouge, G+Clic: vert, B+Clic: bleu, F+Clic: portée, I+Clic: intensité, T+Clic: intensité néon, E+Clic: couleur néon";
                    else if (UserLanguage == CountryCode.ES)
                        return "Clic: ON/OFF, R+Clic: rojos, G+Clic: verdes, B+Clic: azules, F+Clic: alcance, I+Clic: intensidad, T+Clic: neón intensidad, E+Clic: neón color";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tıklayın: Açmak/kapatmak, R+tıklayın: Kırmızı, G+tıklayın: Yeşil, B+tıklayın: Mavi, F+tıklayın: Aralığı değiştirmek, I+tıklayın: Yoğunluğu, T+tıklayın: Neon tüp yoğunluğu, E+tıklayın: Neon tüp rengini";
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken: ON/OFF, R+klicken: Rotstufe, G+klicken: Grünstufe, B+klicken: Blaustufe, F+klicken: Lichtstärke, I+klicken: Intensität, T+klicken: Intensität der Neonröhre, E+klicken: Neonröhrenfarbe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите: ON/OFF, R+ЛКМ: красный, G+ЛКМ: зеленый, B+ЛКМ: синий, F+ЛКМ: яркость, I+ЛКМ: интенсивность, T+ЛКМ: итенсивность неона, E+ЛКМ: цвет неона";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Klik: AAN/UIT, R+klikken: rood, G+klikken: groen, B+klikken: blauw, F+klikken: bereik, I+klikken: intensiteit, T+klikken: neon intensiteit, E+klikken: neon kleur";
                    else
                        return "Click: ON/OFF, R+Click: red, G+Click: green, B+Click: blue, F+Click: range, I+Click: intensity, T+Click: Neon intensity, E+Click: Neon color";
                case "SwitchSeamothModel":
                    if (UserLanguage == CountryCode.FR)
                        return "Changer le modèle";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cambiar el modelo";
                    else if (UserLanguage == CountryCode.TR)
                        return "Model değiştir";
                    else if (UserLanguage == CountryCode.DE)
                        return "Modell ändern";
                    else if (UserLanguage == CountryCode.RU)
                        return "Переключить модель";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Verander model";
                    else
                        return "Switch model";
                case "SwitchExosuitModel":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour changer le modèle de bras gauche, ou:" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour changer le modèle de bras droit" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para cambiar el modelo de brazo izquierdo, o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar al modelo de brazo derecho" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Sol kol modelini değiştirmek için sol tıklayın," + Environment.NewLine +
                               "Sağ kol modelini değiştirmek için 'E' tuşuna basarken sol tıklayın" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken, um das linke Armmodell zu ändern, oder:" + Environment.NewLine +
                               "'E' drücken und klicken, um das rechte Armmodell zu wechseln" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите, чтобы изменить модель левой руки:" + Environment.NewLine +
                               "Удерживайте 'E' и нажмите ЛКМ, чтобы изменить модель правой руки" + Environment.NewLine;
					else if (UserLanguage == CountryCode.NL)
						return "Klik om linker arm model te veranderen, of:" +Environment.NewLine +
							   "Houd 'E' vast en klik om rechter arm model te veranderen" + Environment.NewLine;
                    else
                        return "Click to change left arm model, or:" + Environment.NewLine +
                               "Hold 'E' and click to change right arm model" + Environment.NewLine;
                case "AdjustForkliftSize":
                    if (UserLanguage == CountryCode.FR)
                        return "Maintenez 'E' et cliquez pour modifier la taille";
                    else if (UserLanguage == CountryCode.ES)
                        return "Mantenga 'E' y haga clic para cambiar el tamaño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyüklüğü ayarlamak için 'E' tuşuna basarken sol tıklayın";
                    else if (UserLanguage == CountryCode.DE)
                        return "'E' drücken und klicken, um die Größe zu ändern";
                    else if (UserLanguage == CountryCode.RU)
                        return "Удерживайте 'E' и нажмите ЛКМ, чтобы изменить размер";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Houd 'E' vast en klik om grootte aan te passen";
                    else
                        return "Hold 'E' and click to adjust size";
                case "AdjustWarperSpecimenSize":
                    if (UserLanguage == CountryCode.FR)
                        return "Maintenez 'E' et cliquez pour modifier la taille";
                    else if (UserLanguage == CountryCode.ES)
                        return "Mantenga 'E' y haga clic para cambiar el tamaño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyüklüğü ayarlamak için 'E' tuşuna basarken sol tıklayın";
                    else if (UserLanguage == CountryCode.DE)
                        return "'E' drücken und klicken, um die Größe zu ändern";
                    else if (UserLanguage == CountryCode.RU)
                        return "Удерживайте 'E' и нажмите ЛКМ, чтобы изменить размер";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Houd 'E' vast en klik om grootte aan te passen";
                    else
                        return "Hold 'E' and click to adjust size";
                case "AdjustCargoBoxSize":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour accéder au stockage, ou:" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour modifier la taille" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para acceder al almacenamiento, o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar el tamaño" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Depolama alanına erişmek için tıklayın," + Environment.NewLine +
                               "Büyüklüğü ayarlamak için 'E' tuşuna basarken sol tıklayın" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken Sie hier, um den Container zu öffnen, oder:" + Environment.NewLine +
                               "'E' drücken und klicken, um die Größe zu ändern" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите здесь, чтобы открыть контейнер, или:" + Environment.NewLine +
                               "Удерживайте 'E' и нажмите ЛКМ, чтобы изменить размер" + Environment.NewLine;
					else if (UserLanguage == CountryCode.NL) 
						return "Klikken om in opslag te komen, of:" + Environment.NewLine +
							   "Houd 'E' vast en klik om grootte aan te passen" + Environment.NewLine; 
                    else
                        return "Click to access storage, or:" + Environment.NewLine +
                               "Hold 'E' and click to adjust size" + Environment.NewLine;
                case "CargoBox1aName":
                    if (UserLanguage == CountryCode.FR)
                        return "Caisse de chargement renforcée";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de carga reforzada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Güçlendirilimiş Kargo Kutusu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Verstärkte Frachtkiste";
                    else if (UserLanguage == CountryCode.RU)
                        return "Защищенный грузовой ящик";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Versterkt vrachtkist";
                    else
                        return "Reinforced cargo crate";
                case "CargoBox1aDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement renforcée permettant le transport des marchandises. Peut être construit à l'intérieur et à l'extérieur." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga reforzada que permite el transporte de mercancías. Se puede construir en interiores y exteriores." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.TR)
                        return "İyi şeyleri taşımak için güçlendirilmiş kargo kutusu. İç ve dış mekanlarda inşa edilebilir." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine durch Stahlplatten verstärkte Frachtkiste für den Transport von Gütern. Kann drinnen und draußen gebaut werden." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.RU)
                        return "Используется для защиты груза от внешних воздействий. Может быть построен внутри и снаружи." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een versterkt vrachtkist gemaakt om goederen te transporteren. Kan binnen en buiten gebouwd worden." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else
                        return "A reinforced cargo crate made for the transport of goods. Can be built indoor and outdoor." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                case "CargoBox1bName":
                    if (UserLanguage == CountryCode.FR)
                        return "Caisse de chargement";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de carga";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kargo Kutusu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Frachtkiste";
                    else if (UserLanguage == CountryCode.RU)
                        return "Грузовой ящик";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Vrachtkist";
                    else
                        return "Cargo crate";
                case "CargoBox1bDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement permettant le transport des marchandises. Peut être construit à l'intérieur et à l'extérieur." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga que permite el transporte de mercancías. Se puede construir en interiores y exteriores." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.TR)
                        return "İyi şeyleri taşımak için kargo kutusu. İç ve dış mekanlarda inşa edilebilir." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Frachtkiste für den Transport von Gütern. Kann drinnen und draußen gebaut werden." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.RU)
                        return "Используется для транспортировки грузов. Может быть построен внутри и снаружи." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een vrachtkist gemaakt om goederen te transporteren. Kan binnen en buiten gebouwd worden." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AjustCargoboxSize");
                    else
                        return "A cargo crate made for the transport of goods. Can be built indoor and outdoor." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                case "CargoBox1DmgName":
                    if (UserLanguage == CountryCode.FR)
                        return "Caisse de chargement endommagée";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de carga dañada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hasarlı kargo kutusu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beschädigte Frachtkiste";
                    else if (UserLanguage == CountryCode.RU)
                        return "Сломанный грузовой ящик";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Beschadigd vrachtkist";
                    else
                        return "Damaged cargo crate";
                case "CargoBox1DmgDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement en piteux état. Peut être construit à l'intérieur et à l'extérieur." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga en mal estado. Se puede construir en interiores y exteriores." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.TR)
                        return "Kullanılamayan hasarlı kargo kutusu. İç ve dış mekanlarda inşa edilebilir." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine unbrauchbar beschädigte Frachtkiste. Kann drinnen und draußen gebaut werden." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.RU)
                        return "Поврежденный грузовой ящик. Может быть построен внутри и снаружи." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een beschadigd en onbruikbare vrachtkist. Kan binnen en buiten gebouwd worden." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AjustCargoboxSize");
                    else
                        return "An unusable damaged cargo crate. Can be built indoor and outdoor." + Environment.NewLine + Environment.NewLine + GetFriendlyWord("AdjustCargoBoxSize");
                case "Folder1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Documents";
                    else if (UserLanguage == CountryCode.ES)
                        return "Documentos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dosya";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dokumente";
                    else if (UserLanguage == CountryCode.RU)
                        return "Документы";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Documenten";
                    else
                        return "Documents";
                case "Folder1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un dossier contenant divers documents.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una carpeta que contiene varios documentos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Önemli dökümanlar içeren bir dosya.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Ordner mit verschiedenen Dokumenten.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Папка с какой-то очередной бюрократией.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een folder met verschillende documenten er in";
                    else
                        return "A folder containing various documents.";
                case "ClipboardName":
                    if (UserLanguage == CountryCode.FR)
                        return "Presse-papiers";
                    else if (UserLanguage == CountryCode.ES)
                        return "Portapapeles";
                    else if (UserLanguage == CountryCode.TR)
                        return "Klipli dosya";
                    else if (UserLanguage == CountryCode.DE)
                        return "Clipboard";
                    else if (UserLanguage == CountryCode.RU)
                        return "Планшет";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Klembord";
                    else
                        return "Clipboard";
                case "ClipboardDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un presse-papiers contenant divers documents.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un portapapeles que contiene varios documentos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Önemli dökümanlar içeren klipli dosya";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Clipboard mit eingeheftetem Dokument.";
                    else if (UserLanguage == CountryCode.RU)
                        return "И снова какая-то бумага с бюрократией.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een klembord met verschillende documenten er op";
                    else
                        return "A clipboard containing various documents.";
                case "PenName":
                    if (UserLanguage == CountryCode.FR)
                        return "Stylo";
                    else if (UserLanguage == CountryCode.ES)
                        return "Lápiz";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kalem";
                    else if (UserLanguage == CountryCode.DE)
                        return "Stift";
                    else if (UserLanguage == CountryCode.RU)
                        return "Ручка";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Pen";
                    else
                        return "Pen";
                case "PenDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un stylo Alterra.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un lápiz Alterra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Alterra kalemi.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Alterra-Stift.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Первый раз в первый класс.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een Alterra pen";
                    else
                        return "An Alterra pen.";
                case "PenHolderName":
                    if (UserLanguage == CountryCode.FR)
                        return "Porte-stylo";
                    else if (UserLanguage == CountryCode.ES)
                        return "Portalápices";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kalem tutucu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Stifthalter";
                    else if (UserLanguage == CountryCode.RU)
                        return "Подставка для ручек";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Pennen bak";
                    else
                        return "Pen holder";
                case "PenHolderDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un porte-stylo Alterra.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un portalápices de Alterra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Alterra kalem tutucu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Alterra-Stifthalter.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Можно напихать всяких ручек и карандашей и засыпать все это скрепками.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een Alterra pennen bak";
                    else
                        return "An Alterra pen holder.";
                case "PaperTrashName":
                    if (UserLanguage == CountryCode.FR)
                        return "Papiers froissés";
                    else if (UserLanguage == CountryCode.ES)
                        return "Papeles arrugados";
                    else if (UserLanguage == CountryCode.TR)
                        return "Buruşuk kağıt";
                    else if (UserLanguage == CountryCode.DE)
                        return "Zerknülltes Papier";
                    else if (UserLanguage == CountryCode.RU)
                        return "Мятая бумага";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Verfrommeld papier";
                    else
                        return "Crumpled papers";
                case "PaperTrashDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des documents inutiles.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Documentos innecesarios";
                    else if (UserLanguage == CountryCode.TR)
                        return "Önemsiz dökümanlar.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Unwichtiger Papiermüll.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Видимо кому-то срочно приспичило по нужде.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Onnodige documenten";
                    else
                        return "Unnecessary documents.";
                case "SofaStr1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit banc (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Asiento pequeño (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Bank (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Малая софа";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine bank (decoratief en functioneel)";
                    else
                        return "Small bench (decorative and functional)";
                case "SofaStr1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit banc : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un asiento pequeño: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine kleine Bank: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een kleine bank: Estetisch en praktisch om te rusten.";
                    else
                        return "A small bench: Aesthetic and practical to rest.";
                case "SofaStr2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Banc moyen (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Asiento mediano (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Orta boyutlu oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Mittelgroße Bank (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Средняя софа";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Middelgrote bank (decoratief en functioneel)";
                    else
                        return "Medium bench (decorative and functional)";
                case "SofaStr2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un banc moyen : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un asiento mediano: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Orta boyutlu bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Bank mittlerer Größe: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een middelgrote bank: Estetisch en praktisch om te rusten.";
                    else
                        return "A medium bench: Aesthetic and practical to rest.";
                case "SofaStr3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Banc large (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Asiento grande (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lange Bank (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Большая софа";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Lange bank (decoratief en functioneel)"; 
                    else
                        return "Long bench (decorative and functional)";
                case "SofaStr3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un banc large : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un asiento grande: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine große Bank: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een lange bank: Estetisch en praktisch om te rusten.";
                    else
                        return "A long bench: Aesthetic and practical to rest.";
                case "SofaCorner2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Angle de banc (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Asiento angulado (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Açılı oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Bank-Eckelement (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Угловая софа";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Bank hoek (decoratief en functioneel)";
                    else
                        return "Bench angle (decorative and functional)";
                case "SofaCorner2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un angle de banc : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un asiento angulado: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Açılı bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Bankecke mit Winkel: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een bank hoek: Estetisch en praktisch om te rusten";
                    else
                        return "A bench angle: Aesthetic and practical to rest.";
                case "CustomPictureFrameName":
                    if (UserLanguage == CountryCode.FR)
                        return "Cadre photo personnalisable";
                    else if (UserLanguage == CountryCode.ES)
                        return "Marco de fotos personalizables";
                    else if (UserLanguage == CountryCode.TR)
                        return "Özelleştirilebilir fotoğraf çerçevesi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Anpassbarer Bilderrahmen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Рамка для изображений";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een aanpasbaar foto frame";
                    else
                        return "Customizable picture frame";
                case "CustomPictureFrameDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un cadre mural personnalisable." + Environment.NewLine + Environment.NewLine + "Utilisation : " + GetFriendlyWord("CustomPictureFrameTooltipCompact");
                    else if (UserLanguage == CountryCode.ES)
                        return "Marco de pared personalizable." + Environment.NewLine + Environment.NewLine + "Uso: " + GetFriendlyWord("CustomPictureFrameTooltipCompact");
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvara asılan çerçevedir." + Environment.NewLine + Environment.NewLine + "Kullanımı: " + GetFriendlyWord("CustomPictureFrameTooltipCompact");
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein an der Wand befestigter, kundengerechter Rahmen." + Environment.NewLine + Environment.NewLine + "Verwendungszweck: " + GetFriendlyWord("CustomPictureFrameTooltipCompact");
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенная рамка для различных изображений и надписей." + Environment.NewLine + Environment.NewLine + "Применение: " + GetFriendlyWord("CustomPictureFrameTooltipCompact");
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een wand bevestigd aanpasbaar frame." + Environment.NewLine + Environment.NewLine + "Gebruik: " + GetFriendlyWord("CustomPictureFrameTooltipCompact");
                    else
                        return "Wall-mounted customizable frame." + Environment.NewLine + Environment.NewLine + "Usage: " + GetFriendlyWord("CustomPictureFrameTooltipCompact");
                case "CustomPictureFrameTooltip":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour placer une image, ou:" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour ajuster la taille" + Environment.NewLine +
                               "Maintenez 'R' et cliquez pour tourner le cadre photo" + Environment.NewLine +
                               "Maintenez 'F' et cliquez pour modifier le cadre (mode poster)" + Environment.NewLine +
                               "Maintenez 'T' et cliquez pour activer le mode diaporama" + Environment.NewLine +
                               "Maintenez 'G' et cliquez pour activer la selection aléatoire" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para establecer la imagen o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para ajustar el tamaño" + Environment.NewLine +
                               "Mantenga 'R' y haga clic para girar el marco de la imagen" + Environment.NewLine +
                               "Mantenga 'F' y haga clic para cambiar el marco" + Environment.NewLine +
                               "Mantenga 'T' y haga clic para habilitar la presentación de diapositivas." + Environment.NewLine +
                               "Mantenga 'G' y haga clic para habilitar la selección aleatoria" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Görüntüyü ayarlamak için tıklayın veya:" + Environment.NewLine +
                               "Boyutu ayarlamak için 'E' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine +
                               "Fotoğraf çerçevesini çevirmek için 'R' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine +
                               "Çerçeveyi değiştirmek için 'F' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine +
                               "Slayt gösterisi modunu etkinleştirmek için 'T' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine +
                               "Rastgele seçimi etkinleştirmek için 'G' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken, um das Bild festzulegen, oder:" + Environment.NewLine +
                               "'E' drücken und klicken, um die Größe anzupassen." + Environment.NewLine +
                               "'R' drücken und klicken, um den Bilderrahmen zu drehen." + Environment.NewLine +
                               "'F' drücken und klicken, um den Rahmen zu ändern." + Environment.NewLine +
                               "'T' drücken und klicken, um den Diashow-Modus zu aktivieren." + Environment.NewLine +
                               "'G' drücken und klicken, um die zufällige Auswahl zu aktivieren." + Environment.NewLine;
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите, чтобы изменить:" + Environment.NewLine +
                               "Удерживайте 'E' чтобы отрегулировать размер." + Environment.NewLine +
                               "Удерживайте 'R' чтобы повернуть рамку." + Environment.NewLine +
                               "Удерживайте 'F' чтобы изменить тип рамки." + Environment.NewLine +
                               "Удерживайте 'T' чтобы включить режим слайд-шоу." + Environment.NewLine +
                               "Удерживайте 'G' чтобы включить случайный выбор изображения." + Environment.NewLine;
					else if (UserLanguage == CountryCode.NL)
						return "Klikken om foto in te stellen, of:" + Environment.NewLine +
							   "Houd 'E' vast en klik om grootte aan te passen" + Environment.NewLine +
							   "Houd 'R' vast en klik om het frame te draaien" + Environment.NewLine + 
							   "Houd 'F' vast en klik om de frame stijl te veranderen (poster modus)" + Environment.NewLine + 
							   "Houd 'T' vast en klik om slideshow modus te activeren" + Environment.NewLine + 
							   "Houd 'G' vast en klik om random foto selectie aan te zetten" + Environment.NewLine;
                    else
                        return "Click to set picture, or:" + Environment.NewLine +
                               "Hold 'E' and click to adjust size" + Environment.NewLine +
                               "Hold 'R' and click to rotate picture frame" + Environment.NewLine +
                               "Hold 'F' and click to change frame style (poster mode)" + Environment.NewLine +
                               "Hold 'T' and click to enable slideshow mode" + Environment.NewLine +
                               "Hold 'G' and click to enable random image selection" + Environment.NewLine;
                case "CustomPictureFrameTooltipCompact":
                    if (UserLanguage == CountryCode.FR)
                        return "Click: image, E+Click: taille, R+Click: orientation, F+Click: cadre, T+Click: diaporama, G+Click: aléatoire";
                    else if (UserLanguage == CountryCode.ES)
                        return "Clic: imagen, E+Clic: tamaño, R+Clic: girar, F+Clic: marco, T+Clic: diapositivas, G+clic: aleatorizar";
                    else if (UserLanguage == CountryCode.TR)
                        return "tıklayın: Görüntüyü ayarlamak, E+tıklayın: Boyutu, R+tıklayın: çevirmek, F+tıklayın: çerçevesini, T+tıklayın: slayt gösterisi, G+tıklayın: rastgele";
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken: Bild, E+Klicken: Größe, R+Klicken: Drehen, F+Klicken: Rahmen, T+Klicken: Diashow, G+Klicken: Randomisieren";
                    else if (UserLanguage == CountryCode.RU)
                        return "ЛКМ: сменить изображение, E+ЛКМ: размер, R+ЛКМ: повернуть, F+ЛКМ: тип рамки, T+ЛКМ: слайд-шоу, G+ЛКМ: случайный";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Klikken: foto, E+klikken: grootte, R+klikken: orientatie, F+klikken: frame stijl, T+klikken: slideshow, G+klikken: randomize foto's";
                    else
                        return "Click: picture, E+Click: size, R+Click: orientation, F+Click: frame style, T+Click: slideshow, G+Click: randomize";
                case "LandTree1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre alien vivace";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena perenne";
                    else if (UserLanguage == CountryCode.TR)
                        return "Çok yıllık uzaylı ağacı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beständiger Alien-Baum";
                    else if (UserLanguage == CountryCode.RU)
                        return "Многолетнее дерево";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Overblijvende alien boom";
                    else
                        return "Perennial alien tree";
                case "LandTree1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Une variété d'arbre terrestre alien aux propriétés intéréssantes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una variedad de árbol terrestre alienígena con propiedades interesantes.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İlginç özelliklere sahip uzaylı kara ağacı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein außerirdischer Landbaum mit interessanten Eigenschaften.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Многолетнее инопланетное дерево, с подстветкой.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een alien land boom soort met interessante eigenschappen.";
                    else
                        return "An alien land tree variety with interesting properties.";
                case "JungleTree1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre à lianes (alpha)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena (1)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı ağacı (1)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Außerirdischer Baum (1)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Дерево альфа";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Touw boom (1)";
                    else
                        return "Rope tree (alpha)";
                case "JungleTree1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen alpha d'une variété alien d'arbre à lianes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muestra alfa de una variedad de árboles terrestres alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara ağacının alfa örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Alpha-Probe einer fremden Landbaumsorte.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Альфа-образец разновидности инопланетного дерева.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een alfa exemplaar van een alien land boom soort";
                    else
                        return "Alpha specimen of an alien rope tree variety.";
                case "JungleTree2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre à lianes (beta)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena (2)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı ağacı (2)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Außerirdischer Baum (2)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Дерево бета";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Touw boom (2)";
                    else
                        return "Rope tree (beta)";
                case "JungleTree2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen beta d'une variété alien d'arbre à lianes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Beta espécimen de una variedad de árboles terrestres alienígenas.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara ağacının beta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beta-Probe einer fremden Landbaumsorte.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Бета-образец разновидности инопланетного дерева.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Beta exemplaar van een alien land boom soort";
                    else
                        return "Beta specimen of an alien rope tree variety.";
                case "LandPlant1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante griffe-sang";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena bioluminiscente (1)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Biyolüminesan uzaylı bitkisi (1)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Biolumineszente Alienpflanze (1)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение альфа";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Rood getipte plant (1)";
                    else
                        return "Red tipped plant";
                case "LandPlant1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen alpha d'une variété de plante terrestre bioluminescente.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen alfa de una variedad de plantas terrestres alienígenas con propiedades interesantes.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İlginç özelliklere sahip uzaylı kara ağacının alfa örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Alpha-Probe einer fremden Landpflanzensorte mit interessanten Eigenschaften.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Альфа-образец разновидности инопланетного растения.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Alfa exemplaar van een rood getipte plant. Het heeft interessante eigenschappen zoals bioluminescentie.";
                    else
                        return "Alpha specimen of a red tipped plant. It has interesting properties like bioluminescence.";
                case "LandPlant2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Grande plante griffe-sang";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena bioluminiscente (2)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Biyolüminesan uzaylı bitkisi (2)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Biolumineszente Alienpflanze (2)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение бета";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoog, rood getipte plant (2)";
                    else
                        return "Tall red tipped plant";
                case "LandPlant2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen beta d'une variété de plante terrestre bioluminescente. Ce spécimen de plante griffe-sang est plus grand mais partage les mêmes characteristiques que le spécimen alpha.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen beta de una variedad de plantas terrestres alienígenas con propiedades interesantes.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İlginç özelliklere sahip uzaylı kara ağacının beta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beta-Probe einer fremden Landpflanzensorte mit interessanten Eigenschaften.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Бета-образец разновидности инопланетного растения.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Beta exemplaar van een rood getipte plant. Het is hoger dan het alfa exemplaar maar heeft dezelfde eigenschappen";
                    else
                        return "Beta specimen of a red tipped plant. It is taller than the alpha specimen but share the same properties.";
                case "LandPlant3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Buisson à branches de trèfle";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de planta terrestre alienígena (1)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisi örneği (1)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer außerirdischen Landpflanze (1)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Gamma";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Klaver takken (1)";
                    else
                        return "Clover branches";
                case "LandPlant3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire alpha de plante alien. La forme de ses feuilles rappellent celle du trèfle observée sur Terre.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen vulgar alfa de planta terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisinin kaba bir alfa örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Alpha-Probe einer gewöhnlichen außerirdischen Landpflanze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Гамма-образец разновидности инопланетного растения.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Alfa exemplaar van een groffe alien land plant";
                    else
                        return "Alpha specimen of a vulgar alien land plant. Its leaves shape is similar to the shape of clover leaves.";
                case "LandPlant4Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Buisson à plumes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de planta terrestre alienígena (2)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisi örneği (2)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer außerirdischen Landpflanze (2)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Дельта";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Veren struik (2)";
                    else
                        return "Feather bush";
                case "LandPlant4Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire beta de plante terrestre alien. La forme de ses feuilles peuvent faire penser à des plumes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Especie Vulgar beta de planta terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisinin kaba bir beta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beta-Probe einer gewöhnlichen außerirdischen Landpflanze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Дельта-образец разновидности инопланетного растения.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Beta exemplaar van een groffe alien land plant";
                    else
                        return "Beta specimen of a vulgar alien land plant.";
                case "LandPlant5Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Buisson à pois";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de planta terrestre alienígena (3)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisi örneği (3)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer außerirdischen Landpflanze (3)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Эпсилон";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Polka dot struik";
                    else
                        return "Polka dot bush";
                case "LandPlant5Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire thêta de plante terrestre alien charactérisé par la présence de pois clairs sur l'ensemble de ses feuilles.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen theta de vulgar planta terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisinin teta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Gamma-Probe einer gewöhnlichen außerirdischen Landpflanze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эпсилон-образец разновидности инопланетного растения.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Theta exemplaar van een groffe alien plant.";
                    else
                        return "Theta specimen of a vulgar alien land plant. It can be easily recognized thanks to the presence of bright polka dots all over its leaves.";
                case "TropicalPlantName":
                    if (UserLanguage == CountryCode.FR)
                        return "Grand buisson tropical";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (1)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (1)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (1)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoge palm struik";
                    else
                        return "Tall palm bush";
                case "TropicalPlant2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit buisson tropical";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (2)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (2)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (2)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст бета";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine palm struik";
                    else
                        return "Small palm bush";
                case "TropicalPlant3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Grande fougère tropicale";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (3)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (3)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (3)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст гамма";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoge tropische vaarn";
                    else
                        return "Tall tropical fern";
                case "TropicalPlant4Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petite fougère tropicale";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (4)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (4)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (4)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст тета";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine tropische vaarn";
                    else
                        return "Small tropical fern";
                case "TropicalPlant5Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Grand buisson-aloe";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (5)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (5)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (5)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст эпсилон";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoog struik gras";
                    else
                        return "Tall bush grass";
                case "TropicalPlant6Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit buisson-aloe";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (6)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (6)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (6)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст омега";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine struik gras";
                    else
                        return "Small bush grass";
                case "TropicalPlant7Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Grappe de vigne alpha";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (5)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (5)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (5)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст омикрон";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Lianen kluster alfa";
                    else
                        return "Vine cluster alpha";
                case "TropicalPlant8Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Grappe de vigne beta";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena tropical (6)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki (6)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze (6)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст сигма";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Lianen kluster beta";
                    else
                        return "Vine cluster beta";
                case "TropicalPlantDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire de plante alien tropicale.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de vulgar planta alienígena tropical.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropikal uzaylı bitkisinin örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer gewöhnlichen außerirdischen Tropenpflanze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Образец инопланетного тропического куста.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Exemplaar van een groffe tropische alien plant";
                    else
                        return "Specimen of a vulgar tropical alien plant.";
                case "TropicalTreeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Grand arbre épineux";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena tropical (1)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı ağacı (1)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropischer Alienbaum (1)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропическое дерево";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoge, stekelige vaarn boom";
                    else
                        return "Tall spiky-fern tree";
                case "TropicalTree2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit arbre épineux";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena tropical (2)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı ağacı (2)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropischer Alienbaum (2)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропическое Бета";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine stekelige vaarn boom";
                    else
                        return "Small spiky-fern tree";
                case "TropicalTree3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Grand arbre cactus";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena tropical (3)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı ağacı (3)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropischer Alienbaum (3)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропическое гамма";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoge cactus vaarn boom";
                    else
                        return "Tall cactus-fern tree";
                case "TropicalTree4Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit arbre cactus";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena tropical (4)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı ağacı (4)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropischer Alienbaum (4)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропическое тета";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine cactus vaarn boom";
                    else
                        return "Small cactus-fern tree";
                case "TropicalTreeDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire d'arbre alien tropical. Partage des caractéristiques avec les variétés de fougères observées sur la planète 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de vulgar árbol alienígena tropical.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropikal uzaylı bitkisinin örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe eines gewöhnlichen außerirdischen Tropenbaums.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Образец инопланетного тропического дерева.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Exemplaar van een groffe tropische alien boom. Deelt de zelfde kenmerken als andere vaarn soorten op 4546B.";
                    else
                        return "Specimen of a vulgar tropical alien tree. Shares charactistics with fern species observed on 4546B.";
                case "FernName":
                    if (UserLanguage == CountryCode.FR)
                        return "Grande fougère";
                    else if (UserLanguage == CountryCode.ES)
                        return "Helecho grande";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük eğreltiotu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Großer Farn";
                    else if (UserLanguage == CountryCode.RU)
                        return "Большой папоротник";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoge vaarn";
                    else
                        return "Tall fern";
                case "Fern2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petite fougère";
                    else if (UserLanguage == CountryCode.ES)
                        return "Helecho pequeño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük eğreltiotu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleiner Farn";
                    else if (UserLanguage == CountryCode.RU)
                        return "Маленький папоротник";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine vaarn";
                    else
                        return "Small fern";
                case "FernDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une simple fougère.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un simple helecho.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Basit bir eğrelti.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein einfacher Farn.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Обычный папоротник и даже не курится.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een standaard vaarn.";
                    else
                        return "A simple fern.";
                case "CrabClawKelpName":
                    if (UserLanguage == CountryCode.FR)
                        return "Algue pince de crabe";
                    else if (UserLanguage == CountryCode.ES)
                        return "Alga Garra Cangrejo";
                    else if (UserLanguage == CountryCode.TR)
                        return "Pençe Yosunu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Klauentang";
                    else if (UserLanguage == CountryCode.RU)
                        return "Крабья клешня";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Krabbenschaarkelp";
                    else
                        return "Crab Claw Kelp";
                case "CrabClawKelpDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une espèce d'algues à pointe bleue qui tend à pousser à l'intérieur ou à proximité de bassins de saumure acide sur le fond de l'océan.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una especie de alga con punta azul que tiende a crecer en o cerca de piscinas de salmuera ácida en el suelo oceánico.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Okyanus tabanında, asidik tuz havuzlarında ya da yakınlarında büyüyen mavi uçlu bir yosun türüdür.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Blauspitzen-Seetangart, die dazu neigt, in oder in der Nähe von sauren Solebecken auf dem Meeresboden zu wachsen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Вид водорослей с голубыми наконечниками, которые имеют тенденцию расти вблизи кислых соляных бассейнов на дне океана.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een blauw getipte zeewier soort dat in of rondom de zure brine baden op de zeebodem groeit.";
                    else
                        return "A blue-tipped kelp species which tends to grow in or near to acidic brine pools on the ocean floor.";
                case "PyroCoralName": // Ency_RedTipRockThings
                    if (UserLanguage == CountryCode.FR)
                        return "Corail de feu";
                    else if (UserLanguage == CountryCode.ES)
                        return "Coral Flamígero";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ateş Mercanı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Feuerkoralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "Огненный коралл";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Vuurkoraal";
                    else
                        return "Pyrocoral";
                case "PyroCoralDescription": // EncyDesc_RedTipRockThings
                    if (UserLanguage == CountryCode.FR)
                        return "Cette espèce de corail est différente des autres rencontrées sur 4546B dans la mesure où elle s'appuie sur le flux de magma plutôt que sur le courant d'eau pour la fournir en nutriments.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Alga Garra Cangrejo";
                    else if (UserLanguage == CountryCode.TR)
                        return "4546B gezegeninde karşılaşılan türlerden farklı olarak, bu mercan, besin maddelerini su yerine magma akışı üzerinden saılamaktadır.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Korallenart ist anders als alle anderen auf 4546B. Sie filtert ihre Nährstoffe nicht aus dem Wasser, sondern benötigt dafür einen steten Magma-Fluss.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Этот вид кораллов отличается от других, встречающихся на 4546B. Он находится на потоках магмы, а не на потоках воды для доставки ему питательных веществ.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze koraalsoort is niet als alle andere op 4546B. Het is afhankelijk van magma vloei inplaats van water voor voedingsstoffen.";
                    else
                        return "This coral species is unlike any other encountered on 4546B. It relies on magma flow rather than water current to deliver nutrients.";
                case "CoveTreeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre de crique géant";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol Gigante de la Ensenada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dev Kovuk Ağacı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lebensbaum";
                    else if (UserLanguage == CountryCode.RU)
                        return "Гигантское дерево-укрытие";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Gigantische grot boom";
                    else
                        return "Giant Cove Tree";
                case "CoveTreeDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Clone réalisé par prélèvement sur un grand arbre rencontré dans une caverne profonde (le seul de son genre observé sur la planète). Peut être planté sur terre et sous l'eau.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Clon hecho de un vasto árbol encontrado en una profunda ensenada (el único de su tipo encontrado en el planeta). Se puede plantar en tierra y bajo el agua.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnekleme ile yapılan klon: Derin bir kovukta yaşayan büyük bir ağaçtır (ve gezegende türünün tek örneğidir). Karada ve su altında ekilebilir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Klon aus einem riesigen Baum, der in einer tiefen Höhle angetroffen(das einzige bekannte Exemplar seiner Art auf dem Planeten). Kann zu Land und unter Wasser gepflanzt werden.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Клон, сделанный из огромного дерева, встреченного в древесной бухте в биоме Затернная река(единственное в своем роде, встречающееся на планете). Может быть посажен на суше и под водой.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een kloon gemaakt van een grote boom in een diepe grot (De enige van de soort op deze planeet). Kan onderwater geplant worden.";
                    else
                        return "Clone made from a vast tree encountered in a deep cove (the only one of its kind encountered on the planet). Can be planted on land and under water.";
                case "DisplayCoveTreeEggs":
                    if (UserLanguage == CountryCode.FR)
                        return "Afficher/masquer les œufs";
                    else if (UserLanguage == CountryCode.ES)
                        return "Mostrar/ocultar huevos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yumurtaları göster/sakla";
                    else if (UserLanguage == CountryCode.DE)
                        return "Zeige/Verstecke Eier";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скрыть/Показать яйца";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Toon/verberg eieren";
                    else
                        return "Show/hide eggs";
                case "MushroomTree1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre à champignons géant";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol Gigante con Hongos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dev mantar ağacı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Riesiger Baum mit Pilzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Гигантское грибное дерево";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Gigantische paddestoel boom";
                    else
                        return "Giant Mushroom Tree";
                case "MushroomTree2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre à champignons";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol con Hongos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mantar ağacı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Baum mit Pilzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Грибное дерево";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Paddestoel boom";
                    else
                        return "Mushroom Tree";
                case "MushroomTreeDescription": // GiantMushroomTree
                    if (UserLanguage == CountryCode.FR)
                        return "Grande masse organique";
                    else if (UserLanguage == CountryCode.ES)
                        return "Masa org\u00e1nica grande";
                    else if (UserLanguage == CountryCode.TR)
                        return "B\u00fcy\u00fck organik k\u00fctle";
                    else if (UserLanguage == CountryCode.DE)
                        return "gro\u00dfe organische Masse";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0411\u043e\u043b\u044c\u0448\u0430\u044f \u043e\u0440\u0433\u0430\u043d\u0438\u0447\u0435\u0441\u043a\u0430\u044f \u043c\u0430\u0441\u0441\u0430";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Grote organische massa";
                    else
                        return "Large organic mass";
                case "FloatingStoneName":
                    if (UserLanguage == CountryCode.FR)
                        return "Gousse d'ancrage";
                    else if (UserLanguage == CountryCode.ES)
                        return "Vainas ancladas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz Mayını";
                    else if (UserLanguage == CountryCode.DE)
                        return "Leuchtglobus";
                    else if (UserLanguage == CountryCode.RU)
                        return "Якорный кокон";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Anker pod";
                    else
                        return "Anchor Pod";
                case "FloatingStoneDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Ces étranges végétaux sont de grandes sphères membraneuses, gonflées de gaz, ancrées au fond marin par leur système racinaire.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta inusual especie de flora consisten en extensas y esféricas membranas llenas de gas, ancladas al lecho marino con su sistema de raíces.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu eşsiz bitki türleri, küresel, gaz dolu zar ve ona bağlı, deniz tabanına uzanan kökten oluşmaktadır.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese ungewöhnliche Pflanze besteht aus einer großen, kugelförmigen, gasgefüllten Membran, die mit ihrem Wurzelsystem am Meeresboden verankert ist.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Этот необычный образец флоры состоит из большой сферической газонаполненной мембраны, прикрепленной к морскому дну своей корневой системой.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze ongewone flora soort bestaat uit een grote, bolvormige, gas gevulde membraan verankerd aan de zee vloer door het wortelsysteem.";
                    else
                        return "This unusual flora specimen consist of a large, spherical, gas-filled membrane, anchored to the sea floor by its root system.";
                case "GreenReedsName":
                    if (UserLanguage == CountryCode.FR)
                        return "Roseaux tachetés";
                    else if (UserLanguage == CountryCode.ES)
                        return "Carrizo moteado";
                    else if (UserLanguage == CountryCode.TR)
                        return "Benekli sazlık";
                    else if (UserLanguage == CountryCode.DE)
                        return "Gefleckte Schilfe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Пятнистый тростник";
                    else if (UserLanguage == CountryCode.NL)
                    	return "gevlekt riet";
                    else
                        return "Spotted reeds";
                case "GreenReedsDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un spécimen de roseaux alien.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un espécimen de juncos alienígenas.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yabancı sazlıkların bir örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer außerirdischen Schilfart.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Образец пятнистого тростника.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een exemplaar van alien riet.";
                    else
                        return "A specimen of alien reeds.";
                case "BrineLilyName": // Ency_BlueLostRiverLilly
                    if (UserLanguage == CountryCode.FR)
                        return "Nénuphar de mer";
                    else if (UserLanguage == CountryCode.ES)
                        return "Salnúfar";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tuz Zambağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Salzseerose";
                    else if (UserLanguage == CountryCode.RU)
                        return "Раповая лилия";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Brine lelie";
                    else
                        return "Brine Lily";
                case "BrineLilyDescription": // EncyDesc_BlueLostRiverLilly
                    if (UserLanguage == CountryCode.FR)
                        return "Ces plantes, qui peuvent rappeler le nénuphar, ont évolué de manière à tirer parti de la densité relative des bassins de saumure acide que l’on rencontre près du plancher océanique, et flotter à leur surface.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estas plantas parecidas a nenúfares han evolucionado para tomar ventaja de la relativa densidad de las piscinas de salmuera ácida encontradas cerca del lecho marino para flotar a salvo en su superficie.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu zambak benzeri bitkiler, okyanus tabanında, sudan daha yoğun asidik tuz havuzlarında yüzer halde bulunmaktadırlar.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese lilienartigen Pflanzen haben sich dahingehend entwickelt, dass sie die relative Dichte der in der Nähe des Meeresbodens anzutreffenden sauren Solebecken nutzen, um sicher auf der Oberfläche zu schwimmen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эти лилии эволюционировали, чтобы воспользоваться относительной плотностью кислых бассейнов, встречающихся вблизи дна океана, чтобы безопасно плавать на поверхности.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze lelie-achtige planten zijn geëvolueerd om te profiteren van de relatieve dichtheid van de zure brine baden gevonden op de zee bodem om veilig op de oppervlakte te drijven.";
                    else
                        return "These lily-like plants have evolved to take advantage of the relative density of the acidic brine pools encountered near the ocean floor to float safely on the surface.";
                case "LostRiverPlantName": // Ency_BlueAmoeba
                    if (UserLanguage == CountryCode.FR)
                        return "Amibo\u00efde";
                    else if (UserLanguage == CountryCode.ES)
                        return "Ameba";
                    else if (UserLanguage == CountryCode.TR)
                        return "Amipsi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Am\u00f6boid";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0410\u043c\u0451\u0431\u043e\u0438\u0434";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Amoebiform";
                    else
                        return "Amoeboid";
                case "LostRiverPlantDescription": // EncyDesc_BlueAmoeba
                    if (UserLanguage == CountryCode.FR)
                        return "Un organisme simple et insensible, trouv\u00e9 attach\u00e9 \u00e0 la terre avec des niveaux \u00e9lev\u00e9s de mati\u00e8res organiques fossilis\u00e9es.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un simple organismo insensible, encontrado unido al suelo con altos niveles de materia org\u00e1nica fosilizada.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Y\u00fcksek d\u00fczeyde fosille\u015fmi\u015f madde ve karaya ba\u011flanm\u0131\u015f basit, duyars\u0131z bir organizmad\u0131r.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein einfacher, nicht empfindungsf\u00e4higer Organismus, der auf Oberfl\u00e4chen mit hohem Gehalt an versteinertem organischem Material vorkommt.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u041f\u0440\u043e\u0441\u0442\u0435\u0439\u0448\u0438\u0439 \u043d\u0435\u0440\u0430\u0437\u0443\u043c\u043d\u044b\u0439 \u043e\u0440\u0433\u0430\u043d\u0438\u0437\u043c, \u043e\u0431\u043d\u0430\u0440\u0443\u0436\u0435\u043d\u043d\u044b\u0439 \u043f\u0440\u0438\u043a\u0440\u0435\u043f\u043b\u0451\u043d\u043d\u044b\u043c \u043a \u043c\u0435\u0441\u0442\u0443 \u0441 \u0432\u044b\u0441\u043e\u043a\u0438\u043c \u0443\u0440\u043e\u0432\u043d\u0435\u043c \u043e\u043a\u0430\u043c\u0435\u043d\u0435\u0432\u0448\u0435\u0433\u043e \u043e\u0440\u0433\u0430\u043d\u0438\u0447\u0435\u0441\u043a\u043e\u0433\u043e \u0432\u0435\u0449\u0435\u0441\u0442\u0432\u0430.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een simpele, niet bewust organisme, dat gevonden word vast aan land met veel gefossiliseerd organisch materie.";
                    else
                        return "A simple, non-sentient organism, found attached to land with high levels of fossilized organic matter.";
                case "PlantMiddle11Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Algues translucides alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Algas translúcidas alienígenas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yarı saydam yabancı algler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Transluzente Alienalge";
                    else if (UserLanguage == CountryCode.RU)
                        return "Люмиэрии веселые";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Doorschijnende alien algen";
                    else
                        return "Translucent alien algae";
                case "PlantMiddle11Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Une variété de grandes algues alien translucides.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una variedad de grandes algas translúcidas alienígenas.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük saydam yabancı alg türleri.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Vielzahl von großen transluzenten außerirdischen Algen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Разнообразие крупных полупрозрачных чужеродных водорослей.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een variatie van grote doorschijnende alien algen.";
                    else
                        return "A variety of large translucent alien algae.";
                case "SmallDeco3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Champignons décoratifs alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Setas decorativas alienígenas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yabancı dekoratif mantar";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dekorative Alien-Pilze";
                    else if (UserLanguage == CountryCode.RU)
                        return "Фуксевидные грибы";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Decoratieve alien paddestoelen";
                    else
                        return "Alien decorative mushrooms";
                case "SmallDeco3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Une variété décorative de petits champignons alien. Peut être plantée sur terre et sous l'eau.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una variedad decorativa de pequeños hongos alienígenas. Se puede plantar en tierra y bajo el agua.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük uzaylı mantarların dekoratif çeşitliliği. Karada ve su altında ekilebilir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Vielzahl von dekorativen außerirdischen Pilzen. Kann zu Land und unter Wasser gepflanzt werden.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративное разнообразие маленьких чужеродных грибов. Могут быть посажены на суше и под водой.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een decoratieve variatie van kleine alien paddestoelen. Can op land en onderwater geplant worden.";
                    else
                        return "A decorative variety of small alien mushrooms. Can be planted on land and under water.";
                case "BrownCoralTubesName":
                    if (UserLanguage == CountryCode.FR)
                        return "Tubes de corail enterr\u00e9s";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tubos de Coral Terrestres";
                    else if (UserLanguage == CountryCode.TR)
                        return "Toprak Mercan\u0131 Borular\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Braune R\u00f6hrenkoralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0417\u0435\u043c\u043b\u044f\u043d\u044b\u0435 \u043a\u043e\u0440\u0430\u043b\u043b\u043e\u0432\u044b\u0435 \u0442\u0440\u0443\u0431\u044b";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Aarden koraal tubes";
                    else
                        return "Earthen coral tubes";
                case "BrownCoralTubesDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Les ressemblances g\u00e9n\u00e9tiques aux tubes de corail g\u00e9ants rencontr\u00e9s ailleurs sugg\u00e8rent l'existence d'une divergence \u00e9volutive il y a environ 100 000 ans. Cette sous-esp\u00e8ce, moins riche en calcium et de plus petite taille, se rencontre en plus vastes colonies \u00e0 de plus grandes profondeurs.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Las similitudes gen\u00e9ticas de los tubos de coral gigante encontrados por todas partes sugieren una divergencia evolutiva hace aproximadamente 100,000 a\u00f1os, con esta subespecie siendo sustancialmente baja en el contenido de calcio, y especializ\u00e1ndose en crecer en peque\u00f1os y densos montones a m\u00e1s profundidad.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kar\u015f\u0131la\u015f\u0131lan kocaman mercan borular\u0131yla kal\u0131t\u0131msal olarak benzerlikleri, yakla\u015f\u0131k olarak 100.000 y\u0131l \u00f6nce evrimsel bir ayr\u0131\u015fma ya\u015fad\u0131klar\u0131n\u0131 g\u00f6steriyor. Bu alt t\u00fcr\u00fcn i\u00e7erdi\u011fi kalsiyum daha azd\u0131r; daha derinlerde, daha k\u00fc\u00e7\u00fck ve yo\u011fun \u00f6bekler halinde ya\u015fama konusunda uzmanla\u015fm\u0131\u015flard\u0131r.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Die genetische \u00c4hnlichkeit mit den Riesenr\u00f6hrenkorallen, die anderswo vorkommen, weist auf eine Auseinanderentwicklung vor ungef\u00e4hr 100.000 Jahren hin, wobei diese Unterart wesentlich kalzium\u00e4rmer wurde und sich darauf spezialisiert hat, in kleineren, dichteren Kolonien in gr\u00f6\u00dferen Tiefen zu wachsen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0413\u0435\u043d\u0435\u0442\u0438\u0447\u0435\u0441\u043a\u043e\u0435 \u0441\u0445\u043e\u0434\u0441\u0442\u0432\u043e \u0441 \u0433\u0438\u0433\u0430\u043d\u0442\u0441\u043a\u0438\u043c\u0438 \u043a\u043e\u0440\u0430\u043b\u043b\u043e\u0432\u044b\u043c\u0438 \u0442\u0440\u0443\u0431\u0430\u043c\u0438, \u0432\u0441\u0442\u0440\u0435\u0447\u0430\u0435\u043c\u044b\u043c\u0438 \u0432 \u0434\u0440\u0443\u0433\u0438\u0445 \u043c\u0435\u0441\u0442\u0430\u0445, \u043f\u0440\u0435\u0434\u043f\u043e\u043b\u0430\u0433\u0430\u0435\u0442 \u044d\u0432\u043e\u043b\u044e\u0446\u0438\u043e\u043d\u043d\u043e\u0435 \u0440\u0430\u0441\u0445\u043e\u0436\u0434\u0435\u043d\u0438\u0435 \u043f\u0440\u0438\u043c\u0435\u0440\u043d\u043e 100 000 \u043b\u0435\u0442 \u043d\u0430\u0437\u0430\u0434, \u043f\u0440\u0438 \u044d\u0442\u043e\u043c \u0434\u0430\u043d\u043d\u044b\u0439 \u043f\u043e\u0434\u0432\u0438\u0434 \u0441\u043e\u0434\u0435\u0440\u0436\u0438\u0442 \u0441\u0443\u0449\u0435\u0441\u0442\u0432\u0435\u043d\u043d\u043e \u043c\u0435\u043d\u044c\u0448\u0435 \u043a\u0430\u043b\u044c\u0446\u0438\u044f \u0438 \u0441\u043f\u0435\u0446\u0438\u0430\u043b\u0438\u0437\u0438\u0440\u0443\u0435\u0442\u0441\u044f \u043d\u0430 \u0440\u0430\u0437\u0440\u0430\u0441\u0442\u0430\u043d\u0438\u0438 \u043c\u0435\u043d\u044c\u0448\u0438\u043c\u0438 \u0438 \u0431\u043e\u043b\u0435\u0435 \u043f\u043b\u043e\u0442\u043d\u044b\u043c\u0438 \u0441\u043a\u043e\u043f\u043b\u0435\u043d\u0438\u044f\u043c\u0438 \u043d\u0430 \u0431\u043e\u043b\u0435\u0435 \u0433\u043b\u0443\u0431\u043e\u043a\u0438\u0445 \u0443\u0440\u043e\u0432\u043d\u044f\u0445.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Genetische gelijkenissen aan de gigantische koraal tubes ergens anders gevonden suggereert evolutionaire afwijking ongeveer 100,000 jaar geleden, deze sub-soort is veel lager in calcium gehalte, en specialiseerd in groeien in kleinere, dichtere pakken op diepere niveaus.";
                    else
                        return "Genetic resemblances to the giant coral tubes encountered elsewhere suggests evolutionary divergence approximately 100,000 years ago, with this subspecies being substantially lower in calcium content, and specializing in growing in smaller, denser packs at deeper levels.";
                case "AlienFloraSampleDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Échantillon de flore extraterrestre.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muestra de flora alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yabancı bitki örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Außerirdische Flore-Probe.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Образец инопланетной флоры.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Alien flora monster";
                    else
                        return "Alien flora sample.";
                case "BlueCoralTubes1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Tubes de corail d'argile bleu";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tubos de coral azul embarrados";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mavi toprak mercanı boruları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Blaue irdene Korallenröhren";
                    else if (UserLanguage == CountryCode.RU)
                        return "Трубчатый голубой коралл";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Jordi's Tung (blauw aarden koraal tubes)";
                    else
                        return "Jordi's Tung (Blue earthen coral tubes)";
                case "SmallDeco10Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Pomme de pin violette";
                    else if (UserLanguage == CountryCode.ES)
                        return "Piña morada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mor çam kozalağını";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lila Tannenzapfen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Пурпурная шишка";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Paarse dennenappel";
                    else
                        return "Purple pinecone";
                case "SmallDeco10Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un spécimen alien de pomme de pin violette.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un espécimen alienígena de piña morada.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mor çam kozalak bir yabancı örnek.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Außerirdische Probe eines lilanen, tannenzapfenähnlichen Gewächs.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Образец Фуксевидной Шишки.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een alien soort van de paarse dennenappel.";
                    else
                        return "An alien specimen of purple pine cone.";
                case "SmallDeco11Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante corail jaune";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta de coral amarillo";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sarı mercan bitkisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Gelbe Koralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "Желтый коралл";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Gele koraal plant";
                    else
                        return "Yellow coral plant";
                case "SmallDeco13Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante corail verte";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta de coral verde";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yeşil mercan bitkisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Grüne Koralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "Зеленый коралл";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Groene koraal plant";
                    else
                        return "Green coral plant";
                case "SmallDeco14Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante corail bleu";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta de coral azul";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mavi mercan bitkisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Blaue Koralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "Голубой коралл";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Blauwe koraal plant";
                    else
                        return "Blue coral plant";
                case "SmallDeco15RedName":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante corail rouge";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta de coral rojo";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kırmızı mercan bitkisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Rote Koralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "Красный коралл";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Rode koraal plant";
                    else
                        return "Red coral plant";
                case "SmallDeco17PurpleName":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante corail violette";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta de coral púrpura";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mor mercan bitkisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lila Koralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "Мозговой коралл";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Paarse koraal plant";
                    else
                        return "Purple coral plant";
                case "DecorationsEmptyDeskName":
                    if (UserLanguage == CountryCode.FR)
                        return "Bureau vide";
                    else if (UserLanguage == CountryCode.ES)
                        return "Escritorio vacío";
                    else if (UserLanguage == CountryCode.TR)
                        return "Boş masa";
                    else if (UserLanguage == CountryCode.DE)
                        return "Leerer Tisch";
                    else if (UserLanguage == CountryCode.RU)
                        return "Пустой стол";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Leeg bureau";
                    else
                        return "Empty desk";
                case "DecorationsEmptyDeskDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Plan de travail commun de vaisseau spatial.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una mesa de trabajo común para naves.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sıradan uzay gemisi masası.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Gewöhnlicher Raumschiff-Schreibtisch.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Обычное рабочее место.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Gewone ruimteschip werk bureau.";
                    else
                        return "Common starship work desk.";
                case "BenchSmallName":
                    if (UserLanguage == CountryCode.FR)
                        return "Tout petit banc";
                    else if (UserLanguage == CountryCode.ES)
                        return "Banco muy pequeño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Çok küçük tezgah";
                    else if (UserLanguage == CountryCode.DE)
                        return "Sehr kleine Bank";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень малая лавка";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Heel kleine bank";
                    else
                        return "Very small bench";
                case "BenchMediumName":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit banc";
                    else if (UserLanguage == CountryCode.ES)
                        return "Banco pequeño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük oturak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Bank";
                    else if (UserLanguage == CountryCode.RU)
                        return "Малая лавка";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine bank";
                    else
                        return "Small bench";
                case "BenchDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Simple appareil de relaxation en métal. Conserve l'énergie au repos.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una simple aplicación de metal para relajarse. Conserva la energía mientras descansas.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Basit metal eşya. Dinlenmeyi ve böylece enerji toplamayı sağlar.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Einfaches Metall-Entspannungsmobiliar. Spart Energie beim Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Обычное место, где можно посидеть и восстановить силы.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Simpele metalen relax toestel. Behoud energie tijdens rusten.";
                    else
                        return "Simple metal relaxation appliance. Conserves energy when resting.";
                case "DecorativePDAName":
                    if (UserLanguage == CountryCode.FR)
                        return "PDA décoratif";
                    else if (UserLanguage == CountryCode.ES)
                        return "PDA decorativo";
                    else if (UserLanguage == CountryCode.TR)
                        return "PDA dekoratif";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dekoratives PDA";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный КПК";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Decoratieve PDA";
                    else
                        return "Decorative PDA";
                case "DecorativePDADescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un PDA purement décoratif.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un PDA puramente decorativo.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tamamen dekoratif bir PDA.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein PDA zu reinen Dekorationszwecken.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Просто для красивого вида.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een alleen decoratieve PDA.";
                    else
                        return "A purely decorative PDA.";
                case "GenericSkeletonName":
                    if (UserLanguage == CountryCode.FR)
                        return "Restes Squelettiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Restos Esqueléticos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Iskelet Kalıntısı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Skelettüberreste";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скелетные остатки";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Skelet overblijfselen";
                    else
                        return "Skeletal Remains";
                case "GenericSkeletonDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Les restes squelettiques d'un prédateur.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Los restos esqueléticos de un depredador.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir yırtıcının iskelet kalıntıları.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Die skelettierten Überreste eines Raubtiers.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скелетные останки хищника.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "De skelet overblijfselen van een roofdier";
                    else
                        return "The skeletal remains of a predator.";
                case "SeaDragonSkeletonName":
                    if (UserLanguage == CountryCode.FR)
                        return "Squelette de dragon des mers";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esqueleto de dragón marino";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz ejderhası iskeleti";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seedrachen-Skelett";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скелет морского дракона";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sea dragon skelet";
                    else
                        return "Sea dragon skeleton";
                case "LeviathanSkeletonDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une reproduction miniature du squelette d'un prédateur de classe Léviathan.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura de un esqueleto de depredador de clase leviatán.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir Leviathan sınıfı yırtıcı iskeletinin minyatürü.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Miniatur eines Raubtierskeletts der Leviathan-Klasse.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Миниатюры скелета Левиафана.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een miniatuur van een leviathan klasse roofdier skelet";
                    else
                        return "A miniature of a Leviathan class predator skeleton.";
                case "ReaperSkeletonDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Squelette de Reaper Léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esqueleto de Segador Leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı Canavar i̇skeleti";
                    else if (UserLanguage == CountryCode.DE)
                        return "Cheliceratops-Skelett";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скелет Жнеца";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Leviathan skelet overblijfselen";
                    else
                        return "Leviathan Skeletal Remains";
                case "OpenCustomStorage":
                    if (UserLanguage == CountryCode.FR)
                        return "Ouvrir le casier";
                    else if (UserLanguage == CountryCode.ES)
                        return "Abre el armario";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dolabı aç";
                    else if (UserLanguage == CountryCode.DE)
                        return "Öffne das Schließfach";
                    else if (UserLanguage == CountryCode.RU)
                        return "Откройте шкафчик";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Open het kluisje";
                    else
                        return "Open the locker";
                case "DecorativeLockerName":
                    if (UserLanguage == CountryCode.FR)
                        return "Casier";
                    else if (UserLanguage == CountryCode.ES)
                        return "Casillero";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dolap";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wandspind";
                    else if (UserLanguage == CountryCode.RU)
                        return "Шкафчик";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kluisje";
                    else
                        return "Locker";
                case "DecorativeLockerDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un casier de stockage.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un casillero de almacenamiento.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir depolama dolabı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein dekorativer Wandspind.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный шкафчик.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een opslag kluisje.";
                    else
                        return "A storage locker.";
                case "AirSeedsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes terrestres";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas terrestres";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mevcut karasal bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Terrestrische Pflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Наземные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Lucht planten";
                    else
                        return "Air plants";
                case "RegularAirSeedsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes terrestres existantes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas terrestres existentes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Karasal bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Bestehende terrestrische Pflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Существующие наземные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Bestaande lucht planten";
                    else
                        return "Existing air plants";
                case "EdibleRegularAirTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes terrestres comestibles";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas terrestres comestibles";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yenilebilir karasal bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Essbare Landpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Съедобные наземные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Eetbare lucht planten";
                    else
                        return "Edible air plants";
                case "DecorativeBigAirTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Grandes plantes terrestres";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas terrestres grandes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük karasal bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Große Landpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Крупные наземные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Grote lucht planten";
                    else
                        return "Big air plants";
                case "DecorativeSmallAirTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Petites plantes terrestres";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas terrestres pequeñas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük karasal bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Landpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Малые наземные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine lucht planten";
                    else
                        return "Small air plants";
                case "WaterSeedsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas acuáticas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sucul bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Водные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Aquatische planten";
                    else
                        return "Aquatic plants";
                case "RegularWaterSeedsTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes aquatiques existantes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas acuáticas existentes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mevcut sucul bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Bestehende Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Существующие водные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Bestaande aquatische planten";
                    else
                        return "Existing aquatic plants";
                case "DecorativeMediumWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes aquatiques décoratives";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas acuáticas decorativas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekoratif sucul bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dekorative Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративные водные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Decoratieve aquatische planten";
                    else
                        return "Decorative aquatic plants";
                case "DecorativeBushesWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Buissons aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Arbustos acuáticos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Su çalıları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Aquatische Büsche";
                    else if (UserLanguage == CountryCode.RU)
                        return "Водные кусты";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Aquatische struiken";
                    else
                        return "Aquatic bushes";
                case "RegularSmallWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Petites plantes aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas acuáticas pequeñas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük sucul bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Малые водные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine aquatische planten";
                    else
                        return "Small aquatic plants";
                case "DecorativeBigWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Grandes plantes aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas acuáticas grandes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük su bitkileri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Große Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Крупные водные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Grote aquatische planten";
                    else
                        return "Big aquatic plants";
                case "FunctionalBigWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes aquatiques fonctionnelles";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plantas acuaticas funcionales";
                    else if (UserLanguage == CountryCode.TR)
                        return "Fonksiyonel su bitkileri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Funktionelle Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Функциональные водные растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Functionele aquatische planten";
                    else
                        return "Functional aquatic plants";
                case "LongPlanterName":
                    if (UserLanguage == CountryCode.FR)
                        return "Longue jardinière";
                    else if (UserLanguage == CountryCode.ES)
                        return "Jardinera larga";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun ekici";
                    else if (UserLanguage == CountryCode.DE)
                        return "Langer Pflanzentopf";
                    else if (UserLanguage == CountryCode.RU)
                        return "Длинная грядка";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Lange planter";
                    else
                        return "Long planter";
                case "LongPlanterDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une longue jardinière.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una maceta larga.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun bir ekici.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein langer Pflanzentopf.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Длинная грядка для разных растений.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een lange planter.";
                    else
                        return "A long planter.";
                case "ExteriorLongPlanterName":
                    if (UserLanguage == CountryCode.FR)
                        return "Longue jardinière d'exterieur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Jardinera larga de exterior";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun açık ekici";
                    else if (UserLanguage == CountryCode.DE)
                        return "Langer Pflanzentopf im Freien";
                    else if (UserLanguage == CountryCode.RU)
                        return "Длинная уличная грядка";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een lange buiten planter.";
                    else
                        return "Long outdoor planter";
                case "ExteriorLongPlanterDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une longue jardinière d'exterieur. On peut y planter tout type de plante aquatique.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una jardinera larga para el exterior. Puedes plantar cualquier tipo de planta acuática allí.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun bir açık ekici. Orada herhangi bir tür su bitkisi ekebilirsiniz.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein langer Pflanzentopf im Freien. Sie können dort jede Art von Wasserpflanze pflanzen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Длинная уличная грядка. Вы можете посадить там любой тип водных растений.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een lange buiten planter. Het kan iedere type van aquatische plant groeien.";
                    else
                        return "A long outdoor planter. It can grow any type of aquatic plant.";
                case "BarStoolName":
                    if (UserLanguage == CountryCode.FR)
                        return "Tabouret";
                    else if (UserLanguage == CountryCode.ES)
                        return "Taburete";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tabure";
                    else if (UserLanguage == CountryCode.DE)
                        return "Hocker";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стул";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Stoel";
                    else
                        return "Stool";
                case "BarStoolDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un tabouret.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un taburete.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir tabure.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Hocker.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Просто стул.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een stoel.";
                    else
                        return "A stool.";
                case "AlienRelic1Name": // Ency_PrecursorPrisonArtifact1
                    if (UserLanguage == CountryCode.FR)
                        return "Particulateur de mati\u00e8re organique";
                    else if (UserLanguage == CountryCode.ES)
                        return "Particularizador de Materia Org\u00e1nica";
                    else if (UserLanguage == CountryCode.TR)
                        return "Organik Madde Par\u00e7alay\u0131c\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Biomaterie-Partikulator";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0420\u0430\u0441\u0449\u0435\u043f\u0438\u0442\u0435\u043b\u044c \u043e\u0440\u0433\u0430\u043d\u0438\u0447\u0435\u0441\u043a\u043e\u0439 \u043c\u0430\u0442\u0435\u0440\u0438\u0438";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Organische materie verfijner";
                    else
                        return "Organic Matter Particulator";
                case "AlienRelic1Description": // EncyDesc_PrecursorPrisonArtifact1
                    if (UserLanguage == CountryCode.FR)
                        return "Cet appareil contient un isotope radioactif tr\u00e8s instable, qui d\u00e9truirait probablement tous les organismes qui y seraient expos\u00e9s, tout en laissant les structures physiques intactes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este dispositivo contiene un is\u00f3topo radiactivo altamente inestable, seguramente para destruir todos los organismos expuestos a \u00e9l, dejando las estructuras f\u00edsicas intactas.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu cihaz, y\u00fcksek d\u00fczeyde karars\u0131z radyoaktif izotop i\u00e7ermektedir. Fiziksel yap\u0131lar\u0131n\u0131 bozmadan, t\u00fcm organizmalar\u0131 yok etmektedir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dieses Ger\u00e4t enth\u00e4lt ein h\u00f6chst instabiles radioaktives Isotop, das wahrscheinlich alle Organismen, die ihm ausgesetzt werden, zerst\u00f6rt, physikalische Strukturen aber intakt l\u00e4sst.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u043e\u0435 \u0443\u0441\u0442\u0440\u043e\u0439\u0441\u0442\u0432\u043e \u0441\u043e\u0434\u0435\u0440\u0436\u0438\u0442 \u043a\u0440\u0430\u0439\u043d\u0435 \u043d\u0435\u0441\u0442\u0430\u0431\u0438\u043b\u044c\u043d\u044b\u0439 \u0440\u0430\u0434\u0438\u043e\u0430\u043a\u0442\u0438\u0432\u043d\u044b\u0439 \u0438\u0437\u043e\u0442\u043e\u043f, \u043f\u043e-\u0432\u0438\u0434\u0438\u043c\u043e\u043c\u0443, \u0441\u043f\u043e\u0441\u043e\u0431\u043d\u044b\u0439 \u0443\u043d\u0438\u0447\u0442\u043e\u0436\u0438\u0442\u044c \u043b\u044e\u0431\u044b\u0435 \u043e\u0440\u0433\u0430\u043d\u0438\u0437\u043c\u044b, \u043d\u0430 \u043a\u043e\u0442\u043e\u0440\u044b\u0435 \u043e\u043d \u0432\u043e\u0437\u0434\u0435\u0439\u0441\u0442\u0432\u0443\u0435\u0442, \u043e\u0441\u0442\u0430\u0432\u043b\u044f\u044f \u0444\u0438\u0437\u0438\u0447\u0435\u0441\u043a\u0438\u0435 \u0441\u0442\u0440\u0443\u043a\u0442\u0443\u0440\u044b \u043d\u0435\u0442\u0440\u043e\u043d\u0443\u0442\u044b\u043c\u0438.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dit apparaat heeft een zeer onstabiel radioactieve isotoop, wat waarschijnlijk alle organismes die eraan blootgesteld worden vernietigt, terwijl het fysieke gebouwen intact laat.";
                    else
                        return "This device contains a highly unstable radioactive isotope, likely to destroy all organisms exposed to it, while leaving physical structures intact.";
                case "AlienRelic2Name": // Ency_PrecursorPrisonArtifact2
                    if (UserLanguage == CountryCode.FR)
                        return "Projecteur holographique";
                    else if (UserLanguage == CountryCode.ES)
                        return "Proyector Hologr\u00e1fico";
                    else if (UserLanguage == CountryCode.TR)
                        return "Holografik Yans\u0131t\u0131c\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "holografischer Projektor";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0413\u043e\u043b\u043e\u0433\u0440\u0430\u0444\u0438\u0447\u0435\u0441\u043a\u0438\u0439 \u043f\u0440\u043e\u0435\u043a\u0442\u043e\u0440";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Holographische projector";
                    else
                        return "Holographic Projector";
                case "AlienRelic2Description": // EncyDesc_PrecursorPrisonArtifact2
                    if (UserLanguage == CountryCode.FR)
                        return "Cet appareil contient des composants de mise en r\u00e9seau et une unit\u00e9 de projection holographique.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este dispositivo contiene una red de aparatos y una unidad de proyecci\u00f3n hologr\u00e1fica.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu cihaz, a\u011f ba\u011fda\u015ft\u0131r\u0131c\u0131s\u0131 ve holografik yans\u0131t\u0131m birimlerinden olu\u015fmaktad\u0131r.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dieses Ger\u00e4t enth\u00e4lt Netzwerktechnik und eine holografische Projektionseinheit.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u043e\u0435 \u0443\u0441\u0442\u0440\u043e\u0439\u0441\u0442\u0432\u043e \u0441\u043e\u0434\u0435\u0440\u0436\u0438\u0442 \u0441\u0435\u0442\u0435\u0432\u043e\u0435 \u043e\u0431\u043e\u0440\u0443\u0434\u043e\u0432\u0430\u043d\u0438\u0435 \u0438 \u0443\u0437\u0435\u043b \u0433\u043e\u043b\u043e\u0433\u0440\u0430\u0444\u0438\u0447\u0435\u0441\u043a\u043e\u0433\u043e \u043f\u0440\u043e\u0435\u0446\u0438\u0440\u043e\u0432\u0430\u043d\u0438\u044f.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dit apparaat heeft een netwerk inrichting en een holografische projectie afdeling.";
                    else
                        return "This device contains network apparatus and a holographic projection unit.";
                case "AlienRelic3Name": // Ency_PrecursorPrisonArtifact3
                    if (UserLanguage == CountryCode.FR)
                        return "Tablette rudimentaire";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tablilla Rudimentaria";
                    else if (UserLanguage == CountryCode.TR)
                        return "\u0130lkel Tablet";
                    else if (UserLanguage == CountryCode.DE)
                        return "rudiment\u00e4re Tafel";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u041f\u0440\u0438\u043c\u0438\u0442\u0438\u0432\u043d\u0430\u044f \u0441\u043a\u0440\u0438\u0436\u0430\u043b\u044c";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Rudimentaire tablet";
                    else
                        return "Rudimentary Tablet";
                case "AlienRelic3Description": // EncyDesc_PrecursorPrisonArtifact3
                    if (UserLanguage == CountryCode.FR)
                        return "Cet appareil partage beaucoup de similitudes avec les tablettes utilis\u00e9es pour acc\u00e9der aux complexes aliens, bien que sa structure soit moins complexe.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este dispositivo comparte muchas similitudes con las tablillas usadas para acceder a las instalaciones alien\u00edgenas, aunque est\u00e1 estructura es menos compleja.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu cihaz, uzayl\u0131 \u00fcslerine eri\u015fmek i\u00e7in kullan\u0131lan tabletlerle bir\u00e7ok benzer \u00f6zellik ta\u015f\u0131maktad\u0131r.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dieses Ger\u00e4t hat viele Gemeinsamkeiten mit den Tafeln, die f\u00fcr den Zugang zu den fremden Einrichtungen verwendet werden, obwohl sein Aufbau weniger komplex ist.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u043e\u0435 \u0443\u0441\u0442\u0440\u043e\u0439\u0441\u0442\u0432\u043e \u0438\u043c\u0435\u0435\u0442 \u0431\u043e\u043b\u044c\u0448\u043e\u0435 \u0441\u0445\u043e\u0434\u0441\u0442\u0432\u043e \u0441\u043e \u0441\u043a\u0440\u0438\u0436\u0430\u043b\u044f\u043c\u0438, \u0438\u0441\u043f\u043e\u043b\u044c\u0437\u0443\u0435\u043c\u044b\u043c\u0438 \u0434\u043b\u044f \u0434\u043e\u0441\u0442\u0443\u043f\u0430 \u0432 \u044d\u0442\u0438 \u043a\u043e\u043c\u043f\u043b\u0435\u043a\u0441\u044b, \u0445\u043e\u0442\u044f \u0435\u0433\u043e \u0441\u0442\u0440\u043e\u0435\u043d\u0438\u0435 \u0433\u043e\u0440\u0430\u0437\u0434\u043e \u043c\u0435\u043d\u0435\u0435 \u0441\u043b\u043e\u0436\u043d\u043e.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dit apparaat deelt veel gelijkenissen met de tablets gebruikt om de alien gebouwen in te komen, maar de structuur is minder complex.";
                    else
                        return "This device shares many similarities with the tablets used to access the alien facilities, although its structure is rather less complex.";
                case "AlienRelic4Name": // Ency_PrecursorPrisonArtifact4
                    if (UserLanguage == CountryCode.FR)
                        return "Implant traceur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Implante de rastreo";
                    else if (UserLanguage == CountryCode.TR)
                        return "\u0130zleme Cihaz\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Sensor-Implantat";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0418\u043c\u043f\u043b\u0430\u043d\u0442\u0430\u0442 \u0441\u043b\u0435\u0436\u0435\u043d\u0438\u044f";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Tracking implantaat";
                    else
                        return "Tracking Implant";
                case "AlienRelic4Description": // EncyDesc_PrecursorPrisonArtifact4
                    if (UserLanguage == CountryCode.FR)
                        return "Cette structure \u00e9met un signal haute fr\u00e9quence coh\u00e9rent avec les transmissions d\u00e9j\u00e0 intercept\u00e9es.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este constructo est\u00e1 emitiendo una se\u00f1al de alta frecuencia que coincide con las transmisiones alien\u00edgenas hasta ahora interceptadas.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu yap\u0131, kesintiye u\u011frayan di\u011fer uzayl\u0131 yay\u0131nlar\u0131 ile e\u015fle\u015fen, y\u00fcksek bant geni\u015fli\u011finde yay\u0131n yapmaktad\u0131r.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Konstruktion gibt ein Signal mit hoher Bandbreite ab, das mit den fremden \u00dcbertragungen \u00fcbereinstimmt, die anderswo abgefangen wurden.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u044b\u0439 \u043a\u043e\u043d\u0441\u0442\u0440\u0443\u043a\u0442 \u0438\u0437\u043b\u0443\u0447\u0430\u0435\u0442 \u0448\u0438\u0440\u043e\u043a\u043e\u043f\u043e\u043b\u043e\u0441\u043d\u044b\u0439 \u0441\u0438\u0433\u043d\u0430\u043b, \u0441\u043e\u0432\u043f\u0430\u0434\u0430\u044e\u0449\u0438\u0439 \u0441 \u0438\u043d\u043e\u043f\u043b\u0430\u043d\u0435\u0442\u043d\u044b\u043c\u0438 \u043f\u0435\u0440\u0435\u0434\u0430\u0447\u0430\u043c\u0438, \u043f\u0435\u0440\u0435\u0445\u0432\u0430\u0447\u0435\u043d\u043d\u044b\u043c\u0438 \u0432 \u0434\u0440\u0443\u0433\u0438\u0445 \u043c\u0435\u0441\u0442\u0430\u0445.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze constructie zend hoge bandbreedte signalen uit die overeen komen met de alien uitzendingen ergens anders onderschepen.";
                    else
                        return "This construct is emitting a high-bandwidth signal consistent with alien transmissions intercepted elsewhere.";
                case "AlienRelic5Name": // Ency_PrecursorPrisonArtifact5
                    if (UserLanguage == CountryCode.FR)
                        return "Gravure alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Talla Alien\u00edgena";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzayl\u0131 Oymas\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "fremdartige Schnitzerei";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0418\u043d\u043e\u043f\u043b\u0430\u043d\u0435\u0442\u043d\u043e\u0435 \u0440\u0435\u0437\u043d\u043e\u0435 \u0443\u043a\u0440\u0430\u0448\u0435\u043d\u0438\u0435";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Alien uitsnijding";
                    else
                        return "Alien Carving";
                case "AlienRelic5Description": // EncyDesc_PrecursorPrisonArtifact5
                    if (UserLanguage == CountryCode.FR)
                        return "Cette sculpture a des centaines de milliers d'ann\u00e9es et est fabriqu\u00e9e \u00e0 partir d'une fibre naturelle non reconnue, cultiv\u00e9e sur une plan\u00e8te inconnue.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta talla tiene cientos de miles de a\u00f1os, y est\u00e1 hecha de una fibra natural irreconocible que posiblemente crece en un planeta desconocido.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu oyma, y\u00fcz binlerce y\u0131ll\u0131kt\u0131r ve bilinmeyen, olas\u0131l\u0131kla hen\u00fcz ke\u015ffedilmemi\u015f bir gezegende yeti\u015fen do\u011fal bir yap\u0131dan \u00fcretilmi\u015ftir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Schnitzerei ist mehrere hunderttausend Jahre alt und wurde aus einer unbekannten nat\u00fcrlichen Faser hergestellt, die auf einem unbekannten Planeten gewachsen ist.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u043e\u0435 \u0440\u0435\u0437\u043d\u043e\u0435 \u0438\u0437\u0434\u0435\u043b\u0438\u0435 \u0431\u044b\u043b\u043e \u0441\u0434\u0435\u043b\u0430\u043d\u043e \u0442\u044b\u0441\u044f\u0447\u0438 \u043b\u0435\u0442 \u043d\u0430\u0437\u0430\u0434 \u0438\u0437 \u043d\u0435\u0438\u0437\u0432\u0435\u0441\u0442\u043d\u043e\u0433\u043e \u043f\u0440\u0438\u0440\u043e\u0434\u043d\u043e\u0433\u043e \u0432\u043e\u043b\u043e\u043a\u043d\u0430, \u0432\u044b\u0440\u0430\u0449\u0435\u043d\u043d\u043e\u0433\u043e \u043d\u0430 \u0435\u0449\u0451 \u043d\u0435 \u043e\u0442\u043a\u0440\u044b\u0442\u043e\u0439 \u043f\u043b\u0430\u043d\u0435\u0442\u0435.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze uitsnijding is honderd duizenden jaren oud, en gemaakt van een onherkend natuurlijke stof, gegroeid op een onbekende planeet.";
                    else
                        return "This carving is hundreds of thousands of years old, and made from an unrecognized natural fiber, grown on an unknown planet.";
                case "AlienRelic6Name": // Ency_PrecursorPrisonArtifact6
                    if (UserLanguage == CountryCode.FR)
                        return "Dispositif de fin du monde";
                    else if (UserLanguage == CountryCode.ES)
                        return "Dispositivo del Juicio Final";
                    else if (UserLanguage == CountryCode.TR)
                        return "K\u0131yamet Cihaz\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Weltenvernichter";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u041c\u0430\u0448\u0438\u043d\u0430 \u0421\u0443\u0434\u043d\u043e\u0433\u043e \u0434\u043d\u044f";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Doomsday apparaat";
                    else
                        return "Doomsday Device";
                case "AlienRelic6Description": // EncyDesc_PrecursorPrisonArtifact6
                    if (UserLanguage == CountryCode.FR)
                        return "Les scans indiquent que ce dispositif contient suffisamment de potentiel \u00e9nerg\u00e9tique pour d\u00e9truire toute la plan\u00e8te, ainsi que la majeure partie du syst\u00e8me solaire. Par chance, il est endommag\u00e9.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Los an\u00e1lisis indican que este dispositivo contiene el potencial energ\u00e9tico para destruir todo el planeta, junto con la mayor\u00eda del sistema solar. Afortunadamente, se ha averiado.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Taramalar, bu cihaz\u0131n t\u00fcm gezegeni ve g\u00fcne\u015f sisteminin b\u00fcy\u00fck k\u0131sm\u0131n\u0131 yok edebilecek bir enerji bar\u0131nd\u0131rd\u0131\u011f\u0131n\u0131 g\u00f6stermektedir. Neyse ki bozulmu\u015f.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Scans zeigen, dass dieses Ger\u00e4t genug potentielle Energie enth\u00e4lt, um den gesamten Planeten und den Gro\u00dfteil des Planetensystems zu vernichten. Gl\u00fccklicherweise hatte es eine Fehlfunktion.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0421\u043a\u0430\u043d\u0438\u0440\u043e\u0432\u0430\u043d\u0438\u0435 \u0443\u043a\u0430\u0437\u044b\u0432\u0430\u0435\u0442 \u043d\u0430 \u0442\u043e, \u0447\u0442\u043e \u0434\u0430\u043d\u043d\u043e\u0435 \u0443\u0441\u0442\u0440\u043e\u0439\u0441\u0442\u0432\u043e \u0441\u043e\u0434\u0435\u0440\u0436\u0438\u0442 \u0434\u043e\u0441\u0442\u0430\u0442\u043e\u0447\u043d\u0443\u044e \u043f\u043e\u0442\u0435\u043d\u0446\u0438\u0430\u043b\u044c\u043d\u0443\u044e \u044d\u043d\u0435\u0440\u0433\u0438\u044e, \u0447\u0442\u043e\u0431\u044b \u0443\u043d\u0438\u0447\u0442\u043e\u0436\u0438\u0442\u044c \u0432\u0441\u044e \u043f\u043b\u0430\u043d\u0435\u0442\u0443 \u0432\u043c\u0435\u0441\u0442\u0435 \u0441 \u0431\u043e\u043b\u044c\u0448\u0435\u0439 \u0447\u0430\u0441\u0442\u044c\u044e \u0441\u043e\u043b\u043d\u0435\u0447\u043d\u043e\u0439 \u0441\u0438\u0441\u0442\u0435\u043c\u044b. \u041a \u0441\u0447\u0430\u0441\u0442\u044c\u044e, \u043e\u043d\u043e \u043d\u0435\u0438\u0441\u043f\u0440\u0430\u0432\u043d\u043e.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Scans geven aan dat dit apparaat genoeg potentiele energy bezit om de hele planeet te vernietigen samen met het grotendeel van het zonnestelsel. Gelukkig was er een storing.";
                    else
                        return "Scans indicate this device contains enough potential energy to destroy the entire planet, along with most of the solar system. Fortunately, it has malfunctioned.";
                case "AlienRelic7Name": // Ency_PrecursorPrisonArtifact7
                    if (UserLanguage == CountryCode.FR)
                        return "Fusil alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Rifle Antiguo";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzayl\u0131 T\u00fcfe\u011fi";
                    else if (UserLanguage == CountryCode.DE)
                        return "fremdartiges Gewehr";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0418\u043d\u043e\u043f\u043b\u0430\u043d\u0435\u0442\u043d\u043e\u0435 \u0440\u0443\u0436\u044c\u0451";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Alien geweer";
                    else
                        return "Alien Rifle";
                case "AlienRelic7Description": // EncyDesc_PrecursorPrisonArtifact7
                    if (UserLanguage == CountryCode.FR)
                        return "Ressemblant fortement avec l'armement humain dans la forme, cet appareil doit avoir \u00e9t\u00e9 con\u00e7u en pensant \u00e0 un utilisateur humano\u00efde.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Con una gran similitud a la forma de las armas humanas, este dispositivo debi\u00f3 ser dise\u00f1ado para un usuario humanoide.";
                    else if (UserLanguage == CountryCode.TR)
                        return "\u0130nsan yap\u0131m\u0131 silahlarla g\u00fc\u00e7l\u00fc benzerlikler ta\u015f\u0131yan bu cihaz, insan t\u00fcr\u00fc bir kullan\u0131c\u0131 ile tasarlanm\u0131\u015f olmal\u0131d\u0131r.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Aufgrund seiner starken \u00c4hnlichkeit mit menschlicher Waffentechnik muss dieses Ger\u00e4t in Anlehnung an einen humanoiden Benutzer entworfen worden sein.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u043e\u0435 \u0443\u0441\u0442\u0440\u043e\u0439\u0441\u0442\u0432\u043e, \u0441\u0445\u043e\u0436\u0435\u0435 \u043f\u043e \u0444\u043e\u0440\u043c\u0435 \u0441 \u0447\u0435\u043b\u043e\u0432\u0435\u0447\u0435\u0441\u043a\u0438\u043c \u043e\u0440\u0443\u0436\u0438\u0435\u043c, \u0441\u043a\u043e\u0440\u0435\u0435 \u0432\u0441\u0435\u0433\u043e, \u0441\u043f\u0440\u043e\u0435\u043a\u0442\u0438\u0440\u043e\u0432\u0430\u043d\u043e \u0434\u043b\u044f \u0433\u0443\u043c\u0430\u043d\u043e\u0438\u0434\u043d\u043e\u0433\u043e \u043f\u043e\u043b\u044c\u0437\u043e\u0432\u0430\u0442\u0435\u043b\u044f.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Veel overeenkomsten met een menselijk geweer in vorm, dit apparaat moet ontworpen zijn met een mensachtige gebruiker in gedachten.";
                    else
                        return "Strong resemblance to human weaponry in form, this device must have been designed with a humanoid user in mind.";
                case "AlienRelic8Name": // Ency_PrecursorPrisonArtifact8
                    if (UserLanguage == CountryCode.FR)
                        return "Ancienne \u00e9p\u00e9e de la Terre";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espada Antigua de la Tierra";
                    else if (UserLanguage == CountryCode.TR)
                        return "\u00c7ok Eski D\u00fcnya K\u0131l\u0131c\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "antikes Schwert von der Erde";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0440\u0435\u0432\u043d\u0438\u0439 \u0437\u0435\u043c\u043d\u043e\u0439 \u043a\u043b\u0438\u043d\u043e\u043a";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Oud aarde mes";
                    else
                        return "Ancient Earth Blade";
                case "AlienRelic8Description": // EncyDesc_PrecursorPrisonArtifact8
                    if (UserLanguage == CountryCode.FR)
                        return "Une ancienne \u00e9p\u00e9e terrienne, qui remonte au XIIIe si\u00e8cle.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una antigua espada de la Tierra que data del siglo 13.";
                    else if (UserLanguage == CountryCode.TR)
                        return "13.y\u00fczy\u0131l \u00f6ncesinden kalma eski bir d\u00fcnya k\u0131l\u0131c\u0131.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein antikes Schwert von der Erde, auf das 13. Jahrhundert zur\u00fcckdatiert.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0440\u0435\u0432\u043d\u0438\u0439 \u0437\u0435\u043c\u043d\u043e\u0439 \u043a\u043b\u0438\u043d\u043e\u043a, \u0434\u0430\u0442\u0438\u0440\u0443\u0435\u043c\u044b\u0439 XIII \u0432\u0435\u043a\u043e\u043c.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een oud aarde mes, daterend uit de 13e eeuw.";
                    else
                        return "An ancient Earth blade, dating back to the 13th century.";
                case "AlienRelic9Name": // Ency_PrecursorPrisonArtifact10
                    if (UserLanguage == CountryCode.FR)
                        return "Statue Alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estatua Alien\u00edgena";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzayl\u0131 Heykeli";
                    else if (UserLanguage == CountryCode.DE)
                        return "fremde Skulptur";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0418\u043d\u043e\u043f\u043b\u0430\u043d\u0435\u0442\u043d\u0430\u044f \u0441\u0442\u0430\u0442\u0443\u044d\u0442\u043a\u0430";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Alien standbeeld";
                    else
                        return "Alien Statue";
                case "AlienRelic9Description": // EncyDesc_PrecursorPrisonArtifact10
                    if (UserLanguage == CountryCode.FR)
                        return "Cet artefact n'est pas aliment\u00e9, ce qui sugg\u00e8re qu'il a servi \u00e0 un but plus c\u00e9r\u00e9moniel que pratique.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este artefacto no tiene energ\u00eda, lo que sugiere que serv\u00eda para un prop\u00f3sito m\u00e1s ceremonial que pr\u00e1ctico.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu yap\u0131t\u0131n g\u00fcc\u00fcn\u00fcn olmamas\u0131, uygulamaya d\u00f6n\u00fck de\u011fil de simgesel ama\u00e7l\u0131 oldu\u011funu g\u00f6steriyor.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dieses Artefakt enth\u00e4lt keine Energiequelle, was darauf hinweist, dass es eher einem zeremoniellen als einem praktischen Zweck diente.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u044b\u0439 \u0430\u0440\u0442\u0435\u0444\u0430\u043a\u0442 \u043d\u0435 \u0441\u043e\u0434\u0435\u0440\u0436\u0438\u0442 \u044d\u043d\u0435\u0440\u0433\u0438\u0438, \u0447\u0442\u043e \u043f\u0440\u0435\u0434\u043f\u043e\u043b\u0430\u0433\u0430\u0435\u0442 \u0435\u0433\u043e \u0431\u043e\u043b\u0435\u0435 \u0446\u0435\u0440\u0435\u043c\u043e\u043d\u0438\u0430\u043b\u044c\u043d\u043e\u0435, \u043d\u0435\u0436\u0435\u043b\u0438 \u043f\u0440\u0430\u043a\u0442\u0438\u0447\u0435\u0441\u043a\u043e\u0435 \u043d\u0430\u0437\u043d\u0430\u0447\u0435\u043d\u0438\u0435.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dit artefact is onkrachtig, dat suggereert dat it een ceremoniele dienst had, in plaats van een praktisch doel."; /// unpowered was confusing as fuck
                    else
                        return "This artifact is unpowered, suggesting it served a ceremonial, rather than practical purpose.";
                case "AlienRelic10Name": // Ency_PrecursorPrisonArtifact11
                    if (UserLanguage == CountryCode.FR)
                        return "Dispositif de traduction";
                    else if (UserLanguage == CountryCode.ES)
                        return "Dispositivo de Traducci\u00f3n";
                    else if (UserLanguage == CountryCode.TR)
                        return "\u00c7eviri Cihaz\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "\u00dcbersetzungsger\u00e4t";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0423\u0441\u0442\u0440\u043e\u0439\u0441\u0442\u0432\u043e \u043f\u0435\u0440\u0435\u0432\u043e\u0434\u0430";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Vertalings apparaat";
                    else
                        return "Translation Device";
                case "AlienRelic10Description": // EncyDesc_PrecursorPrisonArtifact11
                    if (UserLanguage == CountryCode.FR)
                        return "Cet appareil stocke des donn\u00e9es linguistiques de plus de 1000 langages diff\u00e9rents.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este dispositivo almacena datos ling\u00fcisticos de m\u00e1s de 1,000 idiomas diferentes.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu cihaz, 1000'in \u00fczerinde farkl\u0131 dilin verilerini tutmaktad\u0131r.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dieses Ger\u00e4t hat linguistische Daten von \u00fcber 1.000 verschiedenen Sprachen gespeichert.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0414\u0430\u043d\u043d\u043e\u0435 \u0443\u0441\u0442\u0440\u043e\u0439\u0441\u0442\u0432\u043e \u0445\u0440\u0430\u043d\u0438\u0442 \u043b\u0438\u043d\u0433\u0432\u0438\u0441\u0442\u0438\u0447\u0435\u0441\u043a\u0438\u0435 \u0434\u0430\u043d\u043d\u044b\u0435 \u043e \u0431\u043e\u043b\u0435\u0435 \u0447\u0435\u043c 1000 \u0440\u0430\u0437\u043b\u0438\u0447\u043d\u044b\u0445 \u044f\u0437\u044b\u043a\u0430\u0445.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dit apparaat heeft taalkundige data van over 1000 verschillende talen opgeslagen.";
                    else
                        return "This device stores linguistic data from over 1,000 different languages.";
                case "AlienRelic11Name": // Ency_PrecursorPrisonArtifact12
                    if (UserLanguage == CountryCode.FR)
                        return "Bloc d\u2019une structure extraterrestre";
                    else if (UserLanguage == CountryCode.ES)
                        return "Bloque de Construcci\u00f3n Alien\u00edgena";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzayl\u0131 Yap\u0131 Ta\u015f\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "fremder Baustein";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0418\u043d\u043e\u043f\u043b\u0430\u043d\u0435\u0442\u043d\u044b\u0439 \u0441\u0442\u0440\u043e\u0438\u0442\u0435\u043b\u044c\u043d\u044b\u0439 \u0431\u043b\u043e\u043a";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Alien bouw blok";
                    else
                        return "Alien Building Block";
                case "AlienRelic11Description": // EncyDesc_PrecursorPrisonArtifact12
                    if (UserLanguage == CountryCode.FR)
                        return "Cet objet ressemblant \u00e0 une roche comporte des parties organiques aussi bien que m\u00e9caniques, et il existe une certaine similitude g\u00e9n\u00e9tique et technologique avec la conception des cr\u00e9atures t\u00e9l\u00e9portantes rencontr\u00e9es ailleurs.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este objeto rocoso presenta partes tanto org\u00e1nicas como mec\u00e1nicas, y hay algunas coincidencias gen\u00e9ticas y tecnol\u00f3gicas el dise\u00f1o de los constructos que se teletransportan encontrados en otros lugares.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu kaya benzeri nesne, organik par\u00e7alar\u0131n yan\u0131 s\u0131ra mekanik par\u00e7alar da i\u00e7ermektedir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dieses steinartige Material enth\u00e4lt sowohl organische als auch mechanische Teile. Im Aufbau gibt es einige genetische und technische \u00dcberschneidungen mit den selbstteleportierenden Konstruktionen, die woanders gefunden wurden.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u042d\u0442\u043e\u0442 \u043f\u043e\u0445\u043e\u0436\u0438\u0439 \u043d\u0430 \u043a\u0430\u043c\u0435\u043d\u044c \u043c\u0430\u0442\u0435\u0440\u0438\u0430\u043b \u0441\u043e\u0434\u0435\u0440\u0436\u0438\u0442 \u043a\u0430\u043a \u043e\u0440\u0433\u0430\u043d\u0438\u0447\u0435\u0441\u043a\u0438\u0435, \u0442\u0430\u043a \u0438 \u043c\u0435\u0445\u0430\u043d\u0438\u0447\u0435\u0441\u043a\u0438\u0435 \u0447\u0430\u0441\u0442\u0438 \u0438 \u0438\u043c\u0435\u0435\u0442 \u0433\u0435\u043d\u0435\u0442\u0438\u0447\u0435\u0441\u043a\u043e\u0435 \u0438 \u0442\u0435\u0445\u043d\u043e\u043b\u043e\u0433\u0438\u0447\u0435\u0441\u043a\u043e\u0435 \u0441\u0445\u043e\u0434\u0441\u0442\u0432\u043e \u0441\u043e \u0441\u0442\u0440\u0443\u043a\u0442\u0443\u0440\u043e\u0439 \u0432\u0441\u0442\u0440\u0435\u0447\u0435\u043d\u043d\u044b\u0445 \u0432 \u0434\u0440\u0443\u0433\u0438\u0445 \u043c\u0435\u0441\u0442\u0430\u0445 \u0442\u0435\u043b\u0435\u043f\u043e\u0440\u0442\u0438\u0440\u0443\u044e\u0449\u0438\u0445\u0441\u044f \u043a\u043e\u043d\u0441\u0442\u0440\u0443\u043a\u0442\u043e\u0432.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dit steen achtige object heeft zowel organische als mechanische onderdelen, en er is genetische en technologische kruising met het ontwerp van de zelf teleporterende constructies ergens anders gevonden.";
                    else
                        return "This rock-like object features organic as well as mechanical parts, and there is some genetic and technological crossover with the design of the self-warping constructs encountered elsewhere.";
                case "AlienPillar1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Piédestal";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pedestal";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kaideye";
                    else if (UserLanguage == CountryCode.DE)
                        return "Sockel";
                    else if (UserLanguage == CountryCode.RU)
                        return "Пьедестал";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voetstuk";
                    else
                        return "Pedestal";
                case "AlienPillar1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit piédestal (pratique pour y exposer les reliques ou autre objet).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un pequeño pedestal (útil para colocar reliquias).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bir kaide (kalıntıları yerleştirmek için kullanışlı).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein kleiner Sockel (praktisch zum Platzieren von Reliquien).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Небольшой постамент (удобен для размещения реликвий).";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een klein voetstuk (handig voor het plaatsen van relikwieën en andere objecten";
                    else
                        return "A small pedestal (handy for placing relics or any other item).";
                case "EggSeaEmperorName": // Ency_PrecursorPrisonEggChamberEmperorEgg
                    if (UserLanguage == CountryCode.FR)
                        return "\u0152uf d\u2019empereur intact";
                    else if (UserLanguage == CountryCode.ES)
                        return "Huevo de Emperador Intacto";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bozulmam\u0131\u015f \u0130mparator Yumurtas\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "intaktes Imperator-Ei";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u041d\u0435\u043f\u043e\u0432\u0440\u0435\u0436\u0434\u0451\u043d\u043d\u043e\u0435 \u044f\u0439\u0446\u043e \u0438\u043c\u043f\u0435\u0440\u0430\u0442\u043e\u0440\u0430";
                    else if (UserLanguage == CountryCode.NL)
                    	return "In tact Emperor ei";
                    else
                        return "In-tact Emperor Egg";
                case "EggSeaEmperorDescription": // EncyDesc_PrecursorPrisonEggChamberEmperorEgg
                    if (UserLanguage == CountryCode.FR)
                        return "Les analyses indiquent que l\u2019\u0153uf est dans un \u00e9tat sain de stase auto-r\u00e9gul\u00e9e.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Los an\u00e1lisis indican que el huevo est\u00e1 en un estado saludable de estasis  autorregulada.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Taramalar, yumurtan\u0131n kendi i\u00e7 dengesini sa\u011flam\u0131\u015f, dura\u011fan bir halde ve sa\u011fl\u0131kl\u0131 oldu\u011funu g\u00f6steriyor.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Scans zeigen, dass sich das Ei in einem gesunden Zustand einer selbst regulierten Stase befindet.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0421\u043a\u0430\u043d\u0438\u0440\u043e\u0432\u0430\u043d\u0438\u0435 \u043f\u043e\u043a\u0430\u0437\u044b\u0432\u0430\u0435\u0442, \u0447\u0442\u043e \u044f\u0439\u0446\u043e \u043d\u0430\u0445\u043e\u0434\u0438\u0442\u0441\u044f \u0432 \u0437\u0434\u043e\u0440\u043e\u0432\u043e\u043c \u0441\u043e\u0441\u0442\u043e\u044f\u043d\u0438\u0438 \u0441\u0430\u043c\u043e\u0440\u0435\u0433\u0443\u043b\u0438\u0440\u0443\u0435\u043c\u043e\u0433\u043e \u0441\u0442\u0430\u0437\u0438\u0441\u0430.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Scans geven aan dat het ei in een gezonde staat van zelf-regulerende stasis is.";
                    else
                        return "Scans indicate the egg is in a healthy condition of self-regulated stasis.";
                case "EggSeaDragonName": // Ency_Precursor_LostRiverBase_LeviathanEggShellScan
                    if (UserLanguage == CountryCode.FR)
                        return "\u0152uf de Dragon des mers";
                    else if (UserLanguage == CountryCode.ES)
                        return "Huevo de Drag\u00f3n Marino";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz Ejderhas\u0131 Yumurtas\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seedrachen-Ei";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u042f\u0439\u0446\u043e \u043c\u043e\u0440\u0441\u043a\u043e\u0433\u043e \u0434\u0440\u0430\u043a\u043e\u043d\u0430";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sea dragon ei";
                    else
                        return "Sea Dragon Egg";
                case "EggSeaDragonDescription": // EncyDesc_Precursor_LostRiverBase_LeviathanEggShellScan
                    if (UserLanguage == CountryCode.FR)
                        return "Ce grand \u0153uf est maintenu dans un environnement herm\u00e9tiquement clos, et a \u00e9t\u00e9 st\u00e9rilis\u00e9 chimiquement.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Este gran huevo se mantiene en un ambiente herm\u00e9ticamente sellado y ha sido esterilizado qu\u00edmicamente.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu b\u00fcy\u00fck yumurta ortamdan tamamen yal\u0131t\u0131lm\u0131\u015ft\u0131r ve kimyasal olarak temizlenmi\u015ftir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dieses gro\u00dfe Ei wird in einem hermetisch versiegelten Beh\u00e4lter aufbewahrt und wurde chemisch sterilisiert.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u042d\u0442\u043e \u0431\u043e\u043b\u044c\u0448\u043e\u0435 \u044f\u0439\u0446\u043e \u0441\u043e\u0434\u0435\u0440\u0436\u0438\u0442\u0441\u044f \u0432 \u0433\u0435\u0440\u043c\u0435\u0442\u0438\u0447\u043d\u043e \u0437\u0430\u043a\u0440\u044b\u0442\u043e\u0439 \u0441\u0440\u0435\u0434\u0435 \u0438 \u0431\u044b\u043b\u043e \u0445\u0438\u043c\u0438\u0447\u0435\u0441\u043a\u0438 \u0441\u0442\u0435\u0440\u0438\u043b\u0438\u0437\u043e\u0432\u0430\u043d\u043e.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dit grote ei word gehouden in een luchtdichte omgeving, en is chemisch gesteriliseerd.";
                    else
                        return "This large egg is held in a hermetically sealed environment, and has been chemically sterilized.";
                case "EggsGhostLeviathanName": // Ency_Precursor_LostRiverBase_LeviathanEggShellScan
                    if (UserLanguage == CountryCode.FR)
                        return "\u0152ufs de L\u00e9viathan Fant\u00f4me";
                    else if (UserLanguage == CountryCode.ES)
                        return "Huevos de Leviat\u00e1n Fantasma";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hayalet Canavar Yumurtalar\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Phantom-Eier";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u042f\u0439\u0446\u0430 \u043f\u0440\u0438\u0437\u0440\u0430\u0447\u043d\u043e\u0433\u043e \u043b\u0435\u0432\u0438\u0430\u0444\u0430\u043d\u0430";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Ghost leviathan ei";
                    else
                        return "Ghost Leviathan Eggs";
                case "EggsGhostLeviathanDescription": // EncyDesc_Precursor_LostRiverBase_LeviathanEggShellScan
                    if (UserLanguage == CountryCode.FR)
                        return "\u0152ufs non \u00e9clos appartenant \u00e0 l'esp\u00e8ce baptis\u00e9e \"l\u00e9viathan fant\u00f4me\".";
                    else if (UserLanguage == CountryCode.ES)
                        return "Huevos maduros, pertenecientes a la especie denominada \"leviat\u00e1n fantasma\".";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yumurtalar, 'hayalet canavar' adl\u0131 canl\u0131ya aittir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Unausgebr\u00fctete Eier, die zu einer Tierart mit der Bezeichnung \u201ePhantom\u201c geh\u00f6ren.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Созревающие яйца, принадлежащие виду под названием «призрачный левиафан».";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Rijpende eieren van de soort genaamd 'ghost leviathan'.";
                    else
                        return "Maturing eggs belonging to the species designated 'ghost leviathan'.";
                case "WarperPartName":
                    if (UserLanguage == CountryCode.FR)
                        return "El\u00e9ment de warper";
                    else if (UserLanguage == CountryCode.ES)
                        return "Parte de curvador";
                    else if (UserLanguage == CountryCode.TR)
                        return "S\u0131\u00e7ray\u0131c\u0131 par\u00e7alar\u0131";
                    else if (UserLanguage == CountryCode.DE)
                        return "Warper-Teile";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0427\u0430\u0441\u0442\u0438 \u0442\u0435\u043b\u0430 \u0441\u0442\u0440\u0430\u0436\u0430";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Warper onderdeel";
                    else
                        return "Warper part";
                case "WarperPartDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une partie du corps d'un warper.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una parte del cuerpo de curvador.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir vücut kısmı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein wärmeres Körperteil.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Часть тела Стража.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een warper lichaams deel.";
                    else
                        return "A warper body part.";
                case "BigWarperPartName":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen de Warper";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen Curvador";
                    else if (UserLanguage == CountryCode.TR)
                        return "Çözgü örneği";
                    else if (UserLanguage == CountryCode.DE)
                        return "Warper Probe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Образец Стража";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Warper exemplaar";
                    else
                        return "Warper Specimen";
                case "BigWarperPartDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un spécimen de warper presque complet. " + GetFriendlyWord("AdjustWarperSpecimenSize");
                    else if (UserLanguage == CountryCode.ES)
                        return "Un espécimen casi completo de urdidor. " + GetFriendlyWord("AdjustWarperSpecimenSize");
                    else if (UserLanguage == CountryCode.TR)
                        return "Neredeyse tam bir çözgü örneği. " + GetFriendlyWord("AdjustWarperSpecimenSize");
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein fast vollständiges Exemplar von Warper. " + GetFriendlyWord("AdjustWarperSpecimenSize");
                    else if (UserLanguage == CountryCode.RU)
                        return "Почти полный образец Стража. " + GetFriendlyWord("AdjustWarperSpecimenSize");
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een bijna kompleet exemplaar van de warper. " + GetFriendlyWord("AjustWarperSpecimenSize");
                    else
                        return "An almost complete specimen of warper. " + GetFriendlyWord("AdjustWarperSpecimenSize");
                case "HangingWarperPartName":
                    if (UserLanguage == CountryCode.FR)
                        return "El\u00e9ment de warper suspendu";
                    else if (UserLanguage == CountryCode.ES)
                        return "Parte de curvador colgante";
                    else if (UserLanguage == CountryCode.TR)
                        return "Asma Sıçrayıcı Parçaları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Hängendes warperteil";
                    else if (UserLanguage == CountryCode.RU)
                        return "Подвешенная часть Стража";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hangend warper onderdeel";
                    else
                        return "Hanging warper part";
                case "HangingWarperPartDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une partie du corps d'un warper suspendue.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una parte del cuerpo suspendida de una curvador.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir çözgünün askıya alınmış bir vücut kısmı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein wärmeres Körperteil.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Подвешенная часть Стража. Удобен для изучения.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een hangend warper onderdeel.";
                    else
                        return "An hanging warper body part.";
                case "AquariumSmallName":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit aquarium";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeño acuario";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük akvaryum";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleines Aquarium";
                    else if (UserLanguage == CountryCode.RU)
                        return "Небольшой аквариум";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Klein aquarium";
                    else
                        return "Small aquarium";
                case "AquariumSmallDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit aquarium (ne peut contenir qu'un seul spécimen).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un acuario pequeño (solo puede contener una muestra).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bir akvaryum (sadece bir örnek içerebilir).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein kleines Aquarium (kann nur ein Exemplar enthalten).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Небольшой аквариум (может содержать только один образец).";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een klein aquarium (kan maar één exemplaar bevatten)";
                    else
                        return "A small aquarium (can only contain one specimen).";
                case "RedGrassName": // Ency_BloodGrass
                    if (UserLanguage == CountryCode.FR)
                        return "Herbe sanguine";
                    else if (UserLanguage == CountryCode.ES)
                        return "Hierba de sangre";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kan otu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Blutgras";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кровавые водоросли";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Bloed gras";
                    else
                        return "Blood grass";
                case "RedGrassDescription": // EncyDesc_BloodGrass
                    if (UserLanguage == CountryCode.FR)
                        return "Une vari\u00e9t\u00e9 d'algue marine assez commune, adapt\u00e9e aux environnements sablonneux peu profonds.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una hierba marina com\u00fan adaptada a las zonas arenosas de los baj\u00edos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "S\u0131\u011fl\u0131klar ve kumlu ortamlara uyum sa\u011flam\u0131\u015f yayg\u0131n bir deniz otudur.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein einfaches Seegras, das sich an seichte, sandige Umgebungen angepasst hat.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Обычная морская трава, приспособленная к мелководной песчаной среде.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een gewone zeegras soort aangepast op ondiepe, zanderige omgevingen";
                    else
                        return "A common sea grass adapted to shallow, sandy environments.";
                case "RedGrassTallName":
                    if (UserLanguage == CountryCode.FR)
                        return "Haute herbe sanguine";
                    else if (UserLanguage == CountryCode.ES)
                        return "Hierba alta de sangre";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun kan otu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Hohes Blutgras";
                    else if (UserLanguage == CountryCode.RU)
                        return "Высокие кровавые водоросли";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoog bloed gras";
                    else
                        return "Tall blood grass";
                case "RedGrassDenseName":
                    if (UserLanguage == CountryCode.FR)
                        return "Herbe sanguine dense";
                    else if (UserLanguage == CountryCode.ES)
                        return "Densa hierba de sangre";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yoğun kan otu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Dichtes Blutgras";
                    else if (UserLanguage == CountryCode.RU)
                        return "Густые кровавые водоросли";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Dicht bloed gras";
                    else
                        return "Dense blood grass";
                case "SeamothFragments":
                    if (UserLanguage == CountryCode.FR)
                        return "Fragments de Seamoth";
                    else if (UserLanguage == CountryCode.ES)
                        return "Fragmentos de Seamoth";
                    else if (UserLanguage == CountryCode.TR)
                        return "Seamoth Parçaları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seemotten Fragmente";
                    else if (UserLanguage == CountryCode.RU)
                        return "Фрагменты Мотылька";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Seamoth fragmenten";
                    else
                        return "Seamoth fragments";
                case "SeamothFragmentName":
                    if (UserLanguage == CountryCode.FR)
                        return "Fragment de Seamoth";
                    else if (UserLanguage == CountryCode.ES)
                        return "Fragmento de Seamoth";
                    else if (UserLanguage == CountryCode.TR)
                        return "Seamoth parçası";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seemotten Fragment";
                    else if (UserLanguage == CountryCode.RU)
                        return "Фрагмент Мотылька";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Seamoth fragment";
                    else
                        return "Seamoth fragment";
                case "SeamothFragmentDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un fragment de technologie du Seamoth.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un fragmento de tecnolog\u00eda del Seamoth.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Seamoth teknolojisinin bir par\u00e7as\u0131.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Fragment der Seemotten-Technologie.";
                    else if (UserLanguage == CountryCode.RU)
                        return "\u0424\u0440\u0430\u0433\u043c\u0435\u043d\u0442 \u0442\u0435\u0445\u043d\u043e\u043b\u043e\u0433\u0438\u0438 \u00ab\u041c\u043e\u0442\u044b\u043b\u044c\u043a\u0430\u00bb.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een fragment van de seamoth technologie";
                    else
                        return "A fragment of seamoth technology.";
                // New elements (used by Decorations Mod and Configurator)
                case "DecorativeControlTerminalName":
                    if (UserLanguage == CountryCode.FR)
                        return "Terminal de contrôle";
                    else if (UserLanguage == CountryCode.ES)
                        return "Terminal de control";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kontrol terminali";
                    else if (UserLanguage == CountryCode.DE)
                        return "Steuerterminal";
                    else if (UserLanguage == CountryCode.RU)
                        return "Терминал управления";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Controle terminal";
                    else
                        return "Control terminal";
                case "DecorativeControlTerminalDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un terminal de contrôle décoratif.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un terminal de control decorativo.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekoratif bir kontrol terminali.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein dekoratives Kontrollterminal.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный терминал управления.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een decoratieve controle terminal.";
                    else
                        return "A decorative control terminal.";
                case "WorkDeskScreen1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Écran mural";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pantalla de pared";
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvar ekranı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wandbildschirm";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный экран";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Wand scherm";
                    else
                        return "Wall screen";
                case "WorkDeskScreen1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un écran mural décoratif.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una pantalla de pared decorativa.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekoratif bir duvar ekranı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein dekorativer Wandschirm.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный настенный экран.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een decoratief wand scherm";
                    else
                        return "A decorative wall screen.";
                case "WorkDeskScreen2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Écran mural endommagé";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pantalla de pared dañada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hasarlı duvar ekranı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beschädigter Wandbildschirm";
                    else if (UserLanguage == CountryCode.RU)
                        return "Поврежденный экран";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Beschadigd wand scherm";
                    else
                        return "Damaged wall screen";
                case "WorkDeskScreen2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un écran mural décoratif endommagé.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una pantalla de pared decorativa dañada.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hasarlı bir dekoratif duvar ekranı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein beschädigter dekorativer Wandschirm.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Поврежденный декоративный настенный экран.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een beschadigd decoratief wand scherm";
                    else
                        return "A damaged decorative wall screen.";
                case "MarbleMelonTinyName":
                    if (UserLanguage == CountryCode.FR)
                        return "Graîne de petit melon marbr\u00e9";
                    else if (UserLanguage == CountryCode.ES)
                        return "Peque\u00f1o melom\u00e1rmol fresco";
                    else if (UserLanguage == CountryCode.TR)
                        return "Taze k\u00fc\u00e7\u00fck mermer kavunu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Frische kleine Marmormelone";
                    else if (UserLanguage == CountryCode.RU)
                        return "Семя Маленькой мраморной дыни";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Klein marblemeloen zaad";
                    else
                        return "Small marblemelon seed";
                case "MarbleMelonTinyFruitName": // SmallMelon
                    if (UserLanguage == CountryCode.FR)
                        return "Petit melon marbr\u00e9";
                    else if (UserLanguage == CountryCode.ES)
                        return "Peque\u00f1o melom\u00e1rmol";
                    else if (UserLanguage == CountryCode.TR)
                        return "K\u00fc\u00e7\u00fck mermer kavunu";
                    else if (UserLanguage == CountryCode.DE)
                        return "kleine Marmormelone";
                    else if (UserLanguage == CountryCode.RU)
                        return "Маленькая мраморная дыня";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Kleine marblemeloen";
                    else
                        return "Small marblemelon";
                case "MarbleMelonTinyFruitDescription": // EncyDesc_MelonPlant
                    if (UserLanguage == CountryCode.FR)
                        return "Cette plante collecte l'eau dans l'air plut\u00f4t que dans ses racines et produit de gros fruits charnus comestibles ayant une importante teneur en eau.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta planta absorbe el agua del aire en lugar de depender de su sistema de ra\u00edces, y produce grandes y jugosas frutas que son tanto comestibles como incre\u00edblemente ricas en l\u00edquidos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu bitki, k\u00f6k sistemleri yerine havadan su toplar ve yenilebilir, y\u00fcksek su i\u00e7eren, etli, b\u00fcy\u00fck meyveler \u00fcretir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Pflanze sammelt Wasser aus der Luft, anstatt sich auf ihr Wurzelsystem zu verlassen, und erzeugt gro\u00dfe, fleischige Fr\u00fcchte, die sowohl essbar sind, als auch einen untypisch hohen Wassergehalt haben.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Это растение собирает воду из воздуха, не полагаясь на свою корневую систему, и производит большие, мясистые плоды, которые не только съедобны, но и обладают необычайно высоким содержанием воды.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Deze plant verzameld water van de lucht in plaats van een wortel systeem, en produceerd grote, vlezig fruit die beiden eetbaar zijn, en een hoog water gehalte hebben.";
                    else
                        return "This plant collects water from the air rather than relying on its root system, and produces large, fleshy fruits which are both edible, and have atypically high water content.";
                case "PickupMarbleMelonTinyFruit":
                    if (UserLanguage == CountryCode.FR)
                        return "Ramasser petit melon marbr\u00e9";
                    else if (UserLanguage == CountryCode.ES)
                        return "Recogida peque\u00f1o melom\u00e1rmol";
                    else if (UserLanguage == CountryCode.TR)
                        return "Pikap k\u00fc\u00e7\u00fck mermer kavunu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Sammle kleine Marmormelone";
                    else if (UserLanguage == CountryCode.RU)
                        return "Маленькая мраморная дыня.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Pak klein marblemeloen op";
                    else
                        return "Pickup small marblemelon";
                case "DecorativeTechBoxName":
                    if (UserLanguage == CountryCode.FR)
                        return "Boîtier électronique";
                    else if (UserLanguage == CountryCode.ES)
                        return "Carcasa electrónica";
                    else if (UserLanguage == CountryCode.TR)
                        return "Elektronik gövde";
                    else if (UserLanguage == CountryCode.DE)
                        return "Elektronikbox";
                    else if (UserLanguage == CountryCode.RU)
                        return "Корпус электроники";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Elektronica doos";
                    else
                        return "Tech box";
                case "DecorativeTechBoxDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit boîtier électronique décoratif contenant divers composants nécessaires au traitement des flux d'énergie et de données.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una pequeña caja electrónica decorativa que contiene varios componentes necesarios para procesar flujos de energía y datos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Enerji ve veri akışlarını işlemek için gerekli çeşitli bileşenleri içeren küçük, dekoratif bir elektronik kutu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine kleine dekorative Elektronikbox, die verschiedene Komponenten enthält, die zur Verarbeitung von Energie- und Datenflüssen erforderlich sind.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Небольшая декоративная электронная коробка, которая содержит различные компоненты, необходимые для обработки энергии и потоков данных.";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Een klein decoratieve electronika doos met verschillende elektrische onderdelen nodig voor het verwerken van energie en data stromen.";
                    else
                        return "A small decorative tech box which contains various electronic components required to process energy and data streams.";
                case "CyclopsDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouet Cyclops";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguete del Cyclops";
                    else if (UserLanguage == CountryCode.TR)
                        return "Cyclops oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Zyklop-Spielzeug";
                    else if (UserLanguage == CountryCode.RU)
                        return "Игрушечный Циклоп";
                    else if (UserLanguage == CountryCode.NL)
                    	return "";
                    else
                        return "Cyclops toy";
                case "CyclopsDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une miniature décorative du Cyclops. Utilisation :" + Environment.NewLine + GetFriendlyWord("CyclopsDollTooltipCompact");
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura decorativa de Cyclops. Uso:" + Environment.NewLine + GetFriendlyWord("CyclopsDollTooltipCompact");
                    else if (UserLanguage == CountryCode.TR)
                        return "Minyatür Cyclops oyuncağı. Kullanımı:" + Environment.NewLine + GetFriendlyWord("CyclopsDollTooltipCompact");
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine dekorative Miniatur der Zyklop. Verwendungszweck:" + Environment.NewLine + GetFriendlyWord("CyclopsDollTooltipCompact");
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративная миниатюра Циклоп. Применение:" + Environment.NewLine + GetFriendlyWord("CyclopsDollTooltipCompact");
                    else if (UserLanguage == CountryCode.NL)
                    	return "";
                    else
                        return "A decorative miniature of the Cyclops. Usage:" + Environment.NewLine + GetFriendlyWord("CyclopsDollTooltipCompact");
                case "CyclopsDollTooltip":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour interagir, ou :" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour modifier la taille" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para interactuar o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar el tamaño" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Etkileşim için tıklayın veya:" + Environment.NewLine +
                               "Büyüklüğü ayarlamak için 'E' tuşuna basarken sol tıklayın" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken Sie hier, um zu interagieren, oder:" + Environment.NewLine +
                               "'E' drücken und klicken, um die Größe zu ändern" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите, чтобы взаимодействовать, или:" + Environment.NewLine +
                               "Удерживайте 'E' и нажмите ЛКМ, чтобы изменить размер" + Environment.NewLine;
                    else
                        return "Click to interact, or:" + Environment.NewLine +
                               "Hold 'E' and click to adjust size" + Environment.NewLine;
                case "CyclopsDollTooltipCompact":
                    if (UserLanguage == CountryCode.FR)
                        return "Click: interagir, E+Click: taille";
                    else if (UserLanguage == CountryCode.ES)
                        return "Click: interactuar, E+Click: tamaño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tıklayın: etkileşim, E+Tıklayın: boyut";
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken: interagieren, E+Klicken: Größe";
                    else if (UserLanguage == CountryCode.RU)
                        return "ЛКМ: взаимодействовать, E+ЛКМ: размер";
                    else if (UserLanguage == CountryCode.NL)
                    	return "";
                    else
                        return "Click: interact, E+Click: size";
                case "CyclopsSize":
                    if (UserLanguage == CountryCode.FR)
                        return "Taille du Cyclops: ";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tamaño de Cyclops: ";
                    else if (UserLanguage == CountryCode.TR)
                        return "Cyclops boyutu: ";
                    else if (UserLanguage == CountryCode.DE)
                        return "Zyklopengröße: ";
                    else if (UserLanguage == CountryCode.RU)
                        return "Размер Циклоп: ";
                    else if (UserLanguage == CountryCode.NL)
                    	return "";
                    else
                        return "Cyclops size: ";
                // Configuration (used by Decorations Mod and Configurator)
                case "Config_UseFlatScreenResolution":
                    if (UserLanguage == CountryCode.FR)
                        return "Icônes compactes dans le fabricateur de graines";
                    else if (UserLanguage == CountryCode.ES)
                        return "Íconos compactos en el fabricador de semillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tohum yapımcısı içindeki kompakt simgeler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kompakte Symbole im Saatguthersteller";
                    else if (UserLanguage == CountryCode.RU)
                        return "Компактные иконки в изготовителе семян";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Compacte iconen in zaden fabriceerder";
                    else
                        return "Compact icons in seeds fabricator";
                case "Config_UseCompactTooltips":
                    if (UserLanguage == CountryCode.FR)
                        return "Infobulles compactes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Información sobre herramientas compacta";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kompakt araç ipuçları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kompakte Tooltips";
                    else if (UserLanguage == CountryCode.RU)
                        return "Компактные подсказки";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Gebruik compacte tooltips";
                    else
                        return "Use compact tooltips";
                case "Config_LockQuickslotsWhenPlacingItem":
                    if (UserLanguage == CountryCode.FR)
                        return "Bloquer quickslots lors placement objet";
                    else if (UserLanguage == CountryCode.ES)
                        return "Bloquee las ranuras al colocar el artículo";
                    else if (UserLanguage == CountryCode.TR)
                        return "Öğe yerleştirirken yuvaları kilitle";
                    else if (UserLanguage == CountryCode.DE)
                        return "Sperren Sie die Schlitze beim Platzieren des Objekts";
                    else if (UserLanguage == CountryCode.RU)
                        return "Блокировка быстрых слотов при размещении предмета";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sluit quickslots tijdens het plaatsen van objecten";
                    else
                        return "Lock quickslots when placing item";
                case "Config_AllowBuildOutside":
                    if (UserLanguage == CountryCode.FR)
                        return "Autoriser construction à l'extérieur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Permitir construir afuera";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dışarıda derlemeye izin ver";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lassen Sie draußen bauen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Разрешить строить снаружи";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sta buiten bouwen toe";
                    else
                        return "Allow build outside";
                case "Config_AllowPlaceOutside":
                    if (UserLanguage == CountryCode.FR)
                        return "Autoriser placement à l'extérieur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Dejar colocar afuera";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dışarıya yerleştirilmesine izin ver";
                    else if (UserLanguage == CountryCode.DE)
                        return "Draußen platzieren lassen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Разрешить размещение снаружи";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sta buiten plaatsen toe";
                    else
                        return "Allow to place outside";
                case "Config_EnablePlaceItems":
                    if (UserLanguage == CountryCode.FR)
                        return "Activer le placement d'objets";
                    else if (UserLanguage == CountryCode.ES)
                        return "Habilitar colocación de artículos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Öğe yerleşimini etkinleştir";
                    else if (UserLanguage == CountryCode.DE)
                        return "Aktivieren Sie die Artikelplatzierung";
                    else if (UserLanguage == CountryCode.RU)
                        return "Включить размещение предметов";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet objecten plaatsen aan";
                    else
                        return "Enable items placing";
                case "Config_EnablePlaceBatteries":
                    if (UserLanguage == CountryCode.FR)
                        return "Autoriser le placement de batteries";
                    else if (UserLanguage == CountryCode.ES)
                        return "Permitir la colocación de baterías";
                    else if (UserLanguage == CountryCode.TR)
                        return "Pillerin yerleştirilmesine izin ver";
                    else if (UserLanguage == CountryCode.DE)
                        return "Platzieren Sie die Batterien";
                    else if (UserLanguage == CountryCode.RU)
                        return "Включить размещение батарей";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet batterijen plaatsen aan";
                    else
                        return "Enable batteries placing";
                case "Config_EnableNewItems":
                    if (UserLanguage == CountryCode.FR)
                        return "Inclure les nouveaux objets";
                    else if (UserLanguage == CountryCode.ES)
                        return "Incluir nuevos objetos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yeni nesneler dahil et";
                    else if (UserLanguage == CountryCode.DE)
                        return "Neue Objekte einschließen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Включить новые объекты";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet nieuwe objecten aan";
                    else
                        return "Enable new items";
                case "Config_EnableNewFlora":
                    if (UserLanguage == CountryCode.FR)
                        return "Inclure les nouvelles plantes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Incluir nuevas plantas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yeni bitkileri dahil et";
                    else if (UserLanguage == CountryCode.DE)
                        return "Neue Pflanzen einschließen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Включить новые растения";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet nieuwe planten aan";
                    else
                        return "Enable new plants";
                case "Config_EnablePrecursorTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Activer l'onglet « Précurseurs »";
                    else if (UserLanguage == CountryCode.ES)
                        return "Habilitar la pestaña « Alienígenas »";
                    else if (UserLanguage == CountryCode.TR)
                        return "« Uzaylı » sekmesini etkinleştirme";
                    else if (UserLanguage == CountryCode.DE)
                        return "Aktivieren Sie die Registerkarte « Fremde »";
                    else if (UserLanguage == CountryCode.RU)
                        return "Включить вкладку « Архитекторов »";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet « precursor » tabblad aan";
                    else
                        return "Enable « Precursor » tab";
                case "Config_PrecursorKeysAll":
                    if (UserLanguage == CountryCode.FR)
                        return "Tablettes alien dans le fabricateur de déco";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tabletas alienígenas en el fabricador";
                    else if (UserLanguage == CountryCode.TR)
                        return "Üreticiye yabancı tabletler ekleyin";
                    else if (UserLanguage == CountryCode.DE)
                        return "Fügen Sie Alien-Tabletten im Hersteller";
                    else if (UserLanguage == CountryCode.RU)
                        return "Добавить скрижали в изготовитель декораций";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voeg normale alien tablets toe in de decoratie fabriceerder";
                    else
                        return "Add regular alien tablets in deco fabricator";
                case "Config_EnableRegularEggs":
                    if (UserLanguage == CountryCode.FR)
                        return "Œufs standard dans fabricateur de déco";
                    else if (UserLanguage == CountryCode.ES)
                        return "Agregue huevos regulares en el fabricador";
                    else if (UserLanguage == CountryCode.TR)
                        return "Fabrikatörde düzenli yumurta ekleyin";
                    else if (UserLanguage == CountryCode.DE)
                        return "Fügen Sie normale Eier in Hersteller hinzu";
                    else if (UserLanguage == CountryCode.RU)
                        return "Добавить яйца в изготовитель декораций";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voeg normale eieren toe in decoratie fabriceerder";
                    else
                        return "Add regular eggs in deco fabricator";
                case "Config_EggsDicoverySetting":
                    if (UserLanguage == CountryCode.FR)
                        return "Activer fabrication d'œufs";
                    else if (UserLanguage == CountryCode.ES)
                        return "Permitir elaboración de huevos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yumurta işçiliğini etkinleştir";
                    else if (UserLanguage == CountryCode.DE)
                        return "Erlaube das Basteln von Eiern";
                    else if (UserLanguage == CountryCode.RU)
                        return "Включить создание яиц";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet maken van normale eieren aan";
                    else
                        return "Enable crafting regular eggs";
                case "Config_EnableNutrientBlock":
                    if (UserLanguage == CountryCode.FR)
                        return "Bloc de nutriments dans fabricateur de décorations";
                    else if (UserLanguage == CountryCode.ES)
                        return "Bloque de nutrientes en el fabricador de decoraciones";
                    else if (UserLanguage == CountryCode.TR)
                        return "Fabrikatördeki besin bloğu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ernährungsblock im Dekorationshersteller";
                    else if (UserLanguage == CountryCode.RU)
                        return "Добавить питательный батончик в изготовитель декораций";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voeg voedingsblok toe in decoratie fabriceerder";
                    else
                        return "Add nutrient block in decorations fabricator";
                case "Config_EnableRegularAirSeeds":
                    if (UserLanguage == CountryCode.FR)
                        return "Graînes terrestres standardes dans fabricateur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Semillas de aire en el fabricador de semillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Flora fabrikatöründe düzenli hava tohumları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Regelmäßige Luftsamen im Pflanzenhersteller";
                    else if (UserLanguage == CountryCode.RU)
                        return "Добавить семена наземных растений в изготовитель растений";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voeg normale lucht zaden in flora fabriceerder toe";
                    else
                        return "Add regular air seeds in flora fabricator";
                case "Config_AddRegularAirSeedsWhenDiscovered":
                    if (UserLanguage == CountryCode.FR)
                        return "Activer fabrication de graînes terrestres";
                    else if (UserLanguage == CountryCode.ES)
                        return "Permitir elaboración de semillas de aire";
                    else if (UserLanguage == CountryCode.TR)
                        return "Üretim havası tohumunu etkinleştir";
                    else if (UserLanguage == CountryCode.DE)
                        return "Erlaube das Herstellen von Luftsamen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Разрешить создание семян наземных растений";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet maken van normale lucht zaden aan";
                    else
                        return "Enable crafting regular air seeds";
                case "Config_EnableRegularWaterSeeds":
                    if (UserLanguage == CountryCode.FR)
                        return "Graînes aquatiques standardes dans fabricateur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Semillas de agua regular en fabricador de semillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Flora fabrikatöründe düzenli su tohumları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Regelmäßige Wassersamen in Flora Hersteller";
                    else if (UserLanguage == CountryCode.RU)
                        return "Добавить семена водных в изготовитель растений";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voeg normale water zaden in flora fabriceerder toe";
                    else
                        return "Add regular water seeds in flora fabricator";
                case "Config_AddRegularWaterSeedsWhenDiscovered":
                    if (UserLanguage == CountryCode.FR)
                        return "Activer fabrication de graînes aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Permitir elaboración de semillas de agua";
                    else if (UserLanguage == CountryCode.TR)
                        return "Su tohumlarının işçiliğini etkinleştir";
                    else if (UserLanguage == CountryCode.DE)
                        return "Erlaube das Herstellen von Wassersamen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Разрешить создание семян водных растений";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet maken van normale water zaden aan";
                    else
                        return "Enable crafting regular water seeds";
                case "Config_EnableDiscoveryMode":
                    if (UserLanguage == CountryCode.FR)
                        return "Mode de découverte des objets";
                    else if (UserLanguage == CountryCode.ES)
                        return "Modo de descubrimiento de objetos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Nesne bulma modu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Objekterkennungsmodus";
                    else if (UserLanguage == CountryCode.RU)
                        return "Режим обнаружения объектов";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Objecten ontdekkings modus";
                    else
                        return "Items discovery mode";
                case "Config_EnableSofas":
                    if (UserLanguage == CountryCode.FR)
                        return "Inclure les canapés";
                    else if (UserLanguage == CountryCode.ES)
                        return "Incluir canapés";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kanepeleri dahil et";
                    else if (UserLanguage == CountryCode.DE)
                        return "Canapes einschließen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Добавить софы";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet sofa's aan";
                    else
                        return "Enable sofas";
                case "Config_BarBottle1_water":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur en eau de bouteille de bar 1";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor del agua de la botella bar 1";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar şişe 1'in su değeri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserwert der Riegelflasche 1";
                    else if (UserLanguage == CountryCode.RU)
                        return "ВОДА (Напиток 1)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Water waarde voor bar fles 1";
                    else
                        return "Water value for bar bottle 1";
                case "Config_BarBottle2_water":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur en eau de bouteille de bar 2";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor del agua de la botella bar 2";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar şişe 2'in su değeri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserwert der Riegelflasche 2";
                    else if (UserLanguage == CountryCode.RU)
                        return "ВОДА (Напиток 2)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Water waarde voor bar fles 2";
                    else
                        return "Water value for bar bottle 2";
                case "Config_BarBottle3_water":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur en eau de bouteille de bar 3";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor del agua de la botella bar 3";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar şişe 3'in su değeri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserwert der Riegelflasche 3";
                    else if (UserLanguage == CountryCode.RU)
                        return "ВОДА (Напиток 3)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Water waarde voor bar fles 3";
                    else
                        return "Water value for bar bottle 3";
                case "Config_BarBottle4_water":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur en eau de bouteille de bar 4";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor del agua de la botella bar 4";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar şişe 4'in su değeri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserwert der Riegelflasche 4";
                    else if (UserLanguage == CountryCode.RU)
                        return "ВОДА (Напиток 4)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Water waarde voor bar fles 4";
                    else
                        return "Water value for bar bottle 4";
                case "Config_BarBottle5_water":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur en eau de bouteille de bar 5";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor del agua de la botella bar 5";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar şişe 5'in su değeri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserwert der Riegelflasche 5";
                    else if (UserLanguage == CountryCode.RU)
                        return "ВОДА (Напиток 5)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Water waarde voor bar fles 5";
                    else
                        return "Water value for bar bottle 5";
                case "Config_BarFood1_nutrient":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur nutritive nourriture de bar 1";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor nutritivo para comida de bar 1";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar gıda için besin değeri 1";
                    else if (UserLanguage == CountryCode.DE)
                        return "Nährwert für Riegelfutter 1";
                    else if (UserLanguage == CountryCode.RU)
                        return "Питательная ценность рагу";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voedings waarde voor bar voedsel 1";
                    else
                        return "Nutrients value for bar food 1";
                case "Config_BarFood1_water":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur en eau de nourriture de bar 1";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor del agua para comida de bar 1";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar gıda için su değeri 1";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserwert für Bar Food 1";
                    else if (UserLanguage == CountryCode.RU)
                        return "Водная ценность рагу";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Water waarde voor bar voedsel 1";
                    else
                        return "Water value for bar food 1";
                case "Config_BarFood2_nutrient":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur nutritive nourriture de bar 2";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor nutritivo para comida de bar 2";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar gıda için besin değeri 2";
                    else if (UserLanguage == CountryCode.DE)
                        return "Nährwert für Riegelfutter 2";
                    else if (UserLanguage == CountryCode.RU)
                        return "Питательная ценность подноса с едой";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Voedings waarde voor bar voedsel 2";
                    else
                        return "Nutrient value for bar food 2";
                case "Config_BarFood2_water":
                    if (UserLanguage == CountryCode.FR)
                        return "Valeur en eau de nourriture de bar 2";
                    else if (UserLanguage == CountryCode.ES)
                        return "Valor del agua para comida de bar 2";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bar gıda için su değeri 2";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wasserwert für Bar Food 2";
                    else if (UserLanguage == CountryCode.RU)
                        return "Водная ценность подноса с едой";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Water waarde voor bar voedsel 2";
                    else
                        return "Water value for bar food 2";
                case "Config_AllowIndoorLongPlanterOutside":
                    if (UserLanguage == CountryCode.FR)
                        return "Autoriser long jardin d'intérieur à l'extérieur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Permitir maceta larga interior afuera";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dışarıda uzun ekici izin ver";
                    else if (UserLanguage == CountryCode.DE)
                        return "Erlauben Sie das Hinzufügen Pflanzgefäßen in Innenräumen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Разрешить строить длинную грядку вне базы";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sta binnen lange planter buiten toe"; // ?
                    else
                        return "Allow indoor long planter outside";
                case "Config_AllowOutdoorLongPlanterInside":
                    if (UserLanguage == CountryCode.FR)
                        return "Autoriser long jardin d'extérieur à l'intérieur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Permitir maceta larga al aire libre adentro";
                    else if (UserLanguage == CountryCode.TR)
                        return "İç mekanda uzun ekiciye izin ver";
                    else if (UserLanguage == CountryCode.DE)
                        return "Erlauben Sie das Hinzufügen Pflanzgefäßen im Freien";
                    else if (UserLanguage == CountryCode.RU)
                        return "Разрешить постройку уличной длинной грядки внутри базы";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Sta buiten lange planter binnen toe"; // ?
                    else
                        return "Allow outdoor long planter inside";
                case "Config_FixAquariumLighting":
                    if (UserLanguage == CountryCode.FR)
                        return "Améliorer éclairage des aquariums";
                    else if (UserLanguage == CountryCode.ES)
                        return "Fijar la iluminación de acuarios";
                    else if (UserLanguage == CountryCode.TR)
                        return "Akvaryum aydınlatmasını düzeltin";
                    else if (UserLanguage == CountryCode.DE)
                        return "Fix Aquarienbeleuchtung";
                    else if (UserLanguage == CountryCode.RU)
                        return "Исправить освещение аквариумов";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Fix aquarium verlichting";
                    else
                        return "Fix aquariums lighting";
                case "Config_GlowingAquariumGlass":
                    if (UserLanguage == CountryCode.FR)
                        return "Activer effet brillant sur aquariums";
                    else if (UserLanguage == CountryCode.ES)
                        return "Habilitar el efecto brillante del acuario";
                    else if (UserLanguage == CountryCode.TR)
                        return "Akvaryumda parlayan efekti etkinleştir";
                    else if (UserLanguage == CountryCode.DE)
                        return "Aktivieren Sie den Aquarium-Glüheffekt";
                    else if (UserLanguage == CountryCode.RU)
                        return "Включить эффект свечения аквариума";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Zet aquarium gloed effect aan";
                    else
                        return "Enable aquarium glowing effect";
                case "Config_PrecursorKeysResource":
                    if (UserLanguage == CountryCode.FR)
                        return "Ressource pour fabriquer tablette alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Recurso para fabricar tabletas alienígenas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı tableti hazırlamak için kaynak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ressource für Herstellung von Alien-Tablets";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тип ресурса для изготовления скрижалей пришельцев";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Middel om alien tablet te maken";
                    else
                        return "Resource to craft alien tablet";
                case "Config_PrecursorKeysResourceAmount":
                    if (UserLanguage == CountryCode.FR)
                        return "Quantité ressources pour fabriquer tablette alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cantidad de recursos para fabricar tabletas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı tableti hazırlamak için kaynak miktarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Menge an Ressourcen für Alien-Tablets";
                    else if (UserLanguage == CountryCode.RU)
                        return "Количество ресурса для создания скрижали пришельцев";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoeveelheid middelen om alien tablet te maken";
                    else
                        return "Amount of resources to craft alien tablet";
                case "Config_RelicRecipiesResource":
                    if (UserLanguage == CountryCode.FR)
                        return "Ressource pour fabriquer relique alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Recurso para fabricar reliquia alienígenas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kalıntıları için kaynak miktarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Handwerksressource für außerirdische Relikte";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тип ресурса для создания реликвии пришельцев";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Middel om alien relikwie te maken";
                    else
                        return "Resource to craft alien relic";
                case "Config_RelicRecipiesResourceAmount":
                    if (UserLanguage == CountryCode.FR)
                        return "Quantité ressources pour fabriquer relique alien";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cantidad de recursos para fabricar reliquia";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kalıntısı yapmak için kaynak miktarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Menge an Ressourcen, um Relikte herzustellen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Количество ресурса для создания реликвии пришельцев";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoeveelheid middelen om alien relikwie te maken";
                    else
                        return "Amount of resources to craft alien relic";
                case "Config_CreatureEggsResource":
                    if (UserLanguage == CountryCode.FR)
                        return "Ressource pour créer œufs";
                    else if (UserLanguage == CountryCode.ES)
                        return "Recurso para crear huevos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yaratık yumurtası yapmak için kaynak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ressource für Herstellung von Eiern";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тип ресурса для создания яиц";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Middel om eieren te maken";
                    else
                        return "Resource to craft eggs";
                case "Config_CreatureEggsResourceAmount":
                    if (UserLanguage == CountryCode.FR)
                        return "Quantité de ressources pour créer œuf";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cantidad de recursos para crear huevos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yaratık yumurtası yapmak için kaynak miktarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Menge an Ressourcen für Kreaturenei";
                    else if (UserLanguage == CountryCode.RU)
                        return "Количество ресурса для создания яйца";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoeveelheid middelen om eieren te maken";
                    else
                        return "Amount of resources to craft egg";
                case "Config_FloraRecipiesResource":
                    if (UserLanguage == CountryCode.FR)
                        return "Ressource pour créer graînes";
                    else if (UserLanguage == CountryCode.ES)
                        return "Recurso para crear semillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tohum oluşturmak için kaynak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ressource zum Erstellen von Samen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тип ресурса для создания семян";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Middelen om zaden te maken";
                    else
                        return "Resource to craft seeds";
                case "Config_FloraRecipiesResourceAmount":
                    if (UserLanguage == CountryCode.FR)
                        return "Quantité de ressources pour créer graîne";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cantidad de recursos para elaborar semillas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tohumculuk yapmak için kaynak miktarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Menge an Ressourcen für Saatgut";
                    else if (UserLanguage == CountryCode.RU)
                        return "Количество ресурса для создания семян";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoeveelheid middelen om zaden te maken";
                    else
                        return "Amount of resources to craft seed";
                case "Config_PurplePineconeDroppedResource":
                    if (UserLanguage == CountryCode.FR)
                        return "Ressource donnée par pomme de pin violette";
                    else if (UserLanguage == CountryCode.ES)
                        return "Recurso dado por la piña morada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mor çam kozalakları tarafından sağlanan kaynak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ressource von lila Tannenzapfen gegeben";
                    else if (UserLanguage == CountryCode.RU)
                        return "Pесурс, даваемый пурпурной шишкой";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Middel gegeven door paarse dennenappel";
                    else
                        return "Resource given by purple pinecone";
                case "Config_PurplePineconeDroppedResourceAmount":
                    if (UserLanguage == CountryCode.FR)
                        return "Nb ressources données par pomme de pin violette";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cantidad de recursos dados por la piña morada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Mor çam kozalakları tarafından sağlanan kaynak miktarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Menge Ressourcen von lila Tannenzapfen gegeben";
                    else if (UserLanguage == CountryCode.RU)
                        return "Количество ресурса, даваемого пурпурной шишкой";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Hoeveelheid middelen gegeven door paarse dennenappel";
                    else
                        return "Amount of resources given by purple pinecone";
                case "Config_GhostLeviatan_enable":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre de crique géant crée léviathans";
                    else if (UserLanguage == CountryCode.ES)
                        return "Leviatán se genera del árbol de la ensenada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Koyu ağaçtan Leviathan yumurtlama";
                    else if (UserLanguage == CountryCode.DE)
                        return "Leviathan, der vom Buchtenbaum laicht";
                    else if (UserLanguage == CountryCode.RU)
                        return "Гигантское дерево-укрытие создаёт призрачных левиафанов";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Grot boom spawnt ghost leviathan(s)";
                    else
                        return "Cove tree spawns ghost leviathan(s)";
                case "Config_GhostLeviatan_maxSpawns":
                    if (UserLanguage == CountryCode.FR)
                        return "Nombre de léviathans créés";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cantidad de leviatanes para desovar";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yumurtlanacak leviathans miktarı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Anzahl der zu laichenden Leviathaner";
                    else if (UserLanguage == CountryCode.RU)
                        return "Количество создаваемых левиафанов за раз";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Leviathans moeten spawnen voor het ei verdwijnt";
                    else
                        return "Leviatans to spawn before eggs disappear";
                case "Config_GhostLeviatan_timeBeforeFirstSpawn":
                    if (UserLanguage == CountryCode.FR)
                        return "Léviathan apparaît au bout de";
                    else if (UserLanguage == CountryCode.ES)
                        return "El primer leviatán se genera en";
                    else if (UserLanguage == CountryCode.TR)
                        return "İlk Leviathan doğuyor";
                    else if (UserLanguage == CountryCode.DE)
                        return "Der erste Leviathan erscheint in";
                    else if (UserLanguage == CountryCode.RU)
                        return "Первый левиафан появляется после";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Eerste leviathan spawn in";
                    else
                        return "First leviatan spawn in";
                case "Config_GhostLeviatan_spawnTimeRatio":
                    if (UserLanguage == CountryCode.FR)
                        return "Temps entre 2 création de léviathans";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tiempo entre dos engendros de leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "İki Leviathan doğuşu arasındaki zaman";
                    else if (UserLanguage == CountryCode.DE)
                        return "Zeit zwischen zwei Leviathan-Spawns";
                    else if (UserLanguage == CountryCode.RU)
                        return "Время между созданием двух левиафанов";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Tijd tussen 2 leviathan spawns";
                    else
                        return "Time between 2 leviatan spawns";
                case "Config_GhostLeviatan_health":
                    if (UserLanguage == CountryCode.FR)
                        return "Points de santé des léviathans créés";
                    else if (UserLanguage == CountryCode.ES)
                        return "Puntos de salud de leviatán engendrados";
                    else if (UserLanguage == CountryCode.TR)
                        return "Doğmuş leviathan sağlık noktaları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Spawn Leviathan Gesundheitspunkte";
                    else if (UserLanguage == CountryCode.RU)
                        return "Пункты здоровья создаваемых левиафанов";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Gespawnde leviathan levens punten";
                    else
                        return "Spawned leviatan health points";
                case "Config_HideDeepGrandReefDegasiBase":
                    if (UserLanguage == CountryCode.FR)
                        return "Masquer la structure de la base Degasi (500m)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Ocultar estructura: Hábitat de la Degasi (500m)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yapısını gizle: Degasi Yaşam Alanı (500m)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Verstecke die Struktur von: Degasi-Basis (500 m)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скрыть сооружения: Жилище «Дегази» (500 м)";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Verberg constructie van de Degasi habitat (500m)";
                    else
                        return "Hide structure of the Degasi Habitat (500m)";
                case "Config_OpenDecorationsModConfigurator":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez ici pour configurer";
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic aquí para configurar";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yapılandırmak için burayı tıklayın";
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken Sie hier zum Konfigurieren";
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите здесь, чтобы настроить";
                    else if (UserLanguage == CountryCode.NL)
                    	return "Klik hier om te configureren";
                    else
                        return "Click here to configure";
                default:
                    return "?";
            }
        }
    }
}
