using System;
using static DecorationsMod.RegionHelper;

namespace DecorationsMod
{
    public static class LanguageHelper
    {
        public static CountryCode UserLanguage = RegionHelper.GetCountryCode();

        public static string GetFriendlyGrowingTooltip(int progress)
        {
            if (UserLanguage == CountryCode.FR)
                return "Croissance : " + progress + "%";
            else if (UserLanguage == CountryCode.ES)
                return "Crecimiento: " + progress + "%";
            else if (UserLanguage == CountryCode.TR)
                return "Büyüme: " + progress + "%";
            else if (UserLanguage == CountryCode.DE)
                return "Wachstum:" + progress + "%";
            else if (UserLanguage == CountryCode.RU)
                return "Рост:" + progress + "%";
            else
                return "Growth: " + progress + "%";
        }

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
                        return "Не работающие анализаторы";
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
                        return "Лабораторная мебель";
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
                        return "Настенные мониторы";
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
                    else
                        return "Electronics";
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
                        return "Реле высоковольтные";
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
                        return "Канцелярия";
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
                    else
                        return "Accessories";
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
                        return "Левиафана";
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
                        return "Растения Амфибии";
                    else
                        return "Amphibious plants";
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
                        return "Стеклянный контейнер для образцов, возможно бесполезен.";
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
                        return "Контейнер для образцов (открыт)";
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
                        return "Лабораторная полка";
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
                        return "Декоративная лабораторная полка для образцов.";
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
                        return "Лабораторный столик";
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
                        return "Декоративный лабораторный столик для образцов и инструментов.";
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
                    else
                        return "Simple wall computer";
                case "WallMonitor2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit ordinateur mural simple (doit être relié à un serveur pour fonctionner).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una computadora pequeña y sencilla montada en la pared (debe estar conectada a un servidor para funcionar).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük, basit duvara monte edilen bilgisayar (kullanılması için bir sunucuya bağlanılması gerekiyor).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein kleiner, einfacher an der Wand befestigter Computer (muss mit einem Server verbunden sein, um zu funktionieren).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенный портативный компьютер (можно подключить к серверу для работы).";
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
                        return "Блок цепи";
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
                        return "Коммутационный узел для подключения устройств, питающихся от сети.";
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
                        return "Реле выдерживающее большую нагрузку.";
                    else
                        return "A component allowing the transport of large amounts of energy.";
                case "SpecimenAnalyzerName":
                    if (UserLanguage == CountryCode.FR)
                        return "Analyseur de spécimen";
                    else if (UserLanguage == CountryCode.ES)
                        return "Analizador de muestras";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnek İnceleyici";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probenanalysator";
                    else if (UserLanguage == CountryCode.RU)
                        return "Анализатор образцов";
                    else
                        return "Specimen analyzer";
                case "SpecimenAnalyzerDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Étudie des spécimens pour en déduire des schémas de fabrication.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estudie especímenes para deducir patrones de fabricación.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Üretim kalıplarını anlamak için örnekler üzerinde çalışır.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Gerät zur Untersuchung von Proben, um Herstellungsmuster abzuleiten.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Позволяет изучать образцы для их выведения.";
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
                        return "Кукла «Императора Левиафана»";
                    else
                        return "Emperor leviathan doll";
                case "SmallEmperorDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée d'empereur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca emperador leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu Deniz İmparatoru oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Seeimperator-Puppe wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Была создана для наблюдения в 4546B.";
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
                        return "El gato de Eat My Diction.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Eat My Diction'un kedisi.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Friss-meine-Aussprache-Katze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Котенок для какой-о дикции.";
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
                    else
                        return "An unusual doll.";
                case "JackSepticEyeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneur de JackSepticEye";
                    else if (UserLanguage == CountryCode.ES)
                        return "JackSepticEye Tanque";
                    else if (UserLanguage == CountryCode.TR)
                        return "JackSepticEye Tüpü";
                    else if (UserLanguage == CountryCode.DE)
                        return "Jacks Klärgrube";
                    else if (UserLanguage == CountryCode.RU)
                        return "Маринованный глаз Джека";
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
                        return "Какой-то странный предмет.";
                    else
                        return "An unusual item.";
                case "LeviathanDolls":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupées de léviathans";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñecas Leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Canavar oyuncakları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Leviathan-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Куклы Левиафана";
                    else
                        return "Leviathan dolls";
                case "GhostLeviathanDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de léviathan fantôme";
                    else if (UserLanguage == CountryCode.ES)
                        return "Fantasma leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hayalet canavar oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Puppe eines Phantom-Leviathans";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Призрачного Левиафана";
                    else
                        return "Ghost leviathan doll";
                case "GhostLeviathanDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de léviathan fantôme a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca fantasma leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu hayalet canavar oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Phantom-Leviathans wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Была создана для наблюдения в 4546B.";
                    else
                        return "This ghost leviathan doll was created from observations made on 4546B.";
                case "ReaperLeviathanDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de faucheur léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Segador leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı canavar oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Cheliceratops-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Жнеца Левиафана";
                    else
                        return "Reaper leviathan doll";
                case "ReaperLeviathanDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de faucheur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca segador leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu tırpanlı canavar oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Cheliceratops wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Была создана для наблюдения в 4546B.";
                    else
                        return "This reaper leviathan doll was created from observations made on 4546B.";
                case "SeaDragonDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de dragon des mers léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Dragon marino leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz ejderhası oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seedrachen-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Морского Дракона Левиафана";
                    else
                        return "Sea dragon leviathan doll";
                case "SeaDragonDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de dragon des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca dragon marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu deniz ejderhası oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Seedrachen wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Была создана для наблюдения в 4546B.";
                    else
                        return "This sea dragon leviathan doll was created from observations made on 4546B.";
                case "SeaTreaderDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de pèlerin des mers léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caminante marino leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz gezgini oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seewanderer-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Морского Топтуна";
                    else
                        return "Sea treader leviathan doll";
                case "SeaTreaderDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de pèlerin des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca caminante marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu deniz gezgini oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Seewanderers wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Была создана для наблюдения в 4546B.";
                    else
                        return "This sea treader leviathan doll was created from observations made on 4546B.";
                case "ReefBackDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de reefback léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Portarrecifes leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Resif devi oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Riffrücken-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Рифоспина";
                    else
                        return "Reefback leviathan doll";
                case "ReefBackDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de reefback léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca portarrecifes leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu resif devi oyuncağı 4546B'deki gözlemlerle yapıldı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Riffrückens wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Была создана для наблюдения в 4546B.";
                    else
                        return "This reefback leviathan doll was created from observations made on 4546B.";
                case "CuddleFishDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de câlineur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeca pez monada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sevimli balık oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Knuddelfisch-Puppe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Ласки";
                    else
                        return "Cuddlefish doll";
                case "CuddleFishDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de câlineur a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca pez monada fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu sevimli balık oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Diese Puppe eines Knuddelfischs wurde wurde anhand der Begutachtung einer Probe von 4546B geschaffen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Была создана для наблюдения в 4546B.";
                    else
                        return "This cuddlefish doll was created from observations made on 4546B.";
                case "ReactorLampName":
                    if (UserLanguage == CountryCode.FR)
                        return "Lampe";
                    else if (UserLanguage == CountryCode.ES)
                        return "Lámpara";
                    else if (UserLanguage == CountryCode.TR)
                        return "Lamba";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lampe";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лампа";
                    else
                        return "Lamp";
                case "ReactorLampDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une lampe customisable.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una lámpara personalizable.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Özelleştirilebilir lamba.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine anpassbare Lampe.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лампа с настройками.";
                    else
                        return "A customizable lamp.";
                case "SeamothDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouet Seamoth";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguete Seamoth";
                    else if (UserLanguage == CountryCode.TR)
                        return "Seamoth oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Seemotten-Spielzeug";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла Мотылька";
                    else
                        return "Seamoth toy";
                case "SeamothDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une miniature décorative du seamoth.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura decorativa de seamoth.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Minyatür seamoth oyuncağı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine dekorative Miniatur der Seemotte";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративная кукла для интерьера.";
                    else
                        return "A decorative miniature of the seamoth.";
                case "ExosuitDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouet combinaison PRAWN";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguete traje PRAWN";
                    else if (UserLanguage == CountryCode.TR)
                        return "Suban giysi oyuncağı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Krebsanzug-Spielzeug";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кукла костюма КРАБ";
                    else
                        return "PRAWN suit toy";
                case "ExosuitDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une miniature décorative de la combinaison PRAWN.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura decorativa de traje PRAWN.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Minyatür suban giysi oyuncağı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine dekorative Miniatur des Krebs-Anzuges.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративная кукла для интерьера.";
                    else
                        return "A decorative miniature of the PRAWN suit.";
                case "ForkLiftDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Chariot élévateur (non-fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Carretilla elevadora (no funcionales)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Forklift (yararsız)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Gabelstapler (nicht funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный погрузчик";
                    else
                        return "Forklift (non-functional)";
                case "ForkLiftDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un chariot élévateur décoratif.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una carretilla elevadora decorativa.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekoratif forklift.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein dekorativer Gabelstapler.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Техника для декорации.";
                    else
                        return "A decorative forklift.";
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
                        return "Бутылка содержащий вкусный напиток.";
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
                        return "Кружка";
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
                        return "Eine Tasse aus Titanium.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Кружка сделанная из Титана.";
                    else
                        return "A cup made of titanium.";
                case "BarCup1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit gobelet";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeña taza";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bardak";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Tasse";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стопка";
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
                        return "Стопка сделанная из Титана.";
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
                        return "Малая порция еды";
                    else
                        return "Small meal";
                case "BarFood1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un plat à base de poisson.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un plato hecho de pescado.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Balıktan yapılmış bir yemek.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Mahlzeit aus Fisch.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Еда сделанная из рыбы.";
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
                        return "Лоток с едой";
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
                        return "Комплексный обеда для полноценного питания.";
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
                    else
                        return "Napkins";
                case "BarNapkinsDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des serviettes de table en maille de fibre.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Servilletas en malla de fibra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Fiber örgüden yapılmış peçete.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Servietten aus Fasergewebe.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Салфетки сделанные из микроволокон.";
                    else
                        return "Napkins made of fiber mesh.";
                case "LabRobotArmName":
                    if (UserLanguage == CountryCode.FR)
                        return "Bras robot (non-fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Brazo robótico (no funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız Robot Kolu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Roboterarm (nicht funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторный робот";
                    else
                        return "Robot arm (non-functional)";
                case "LabRobotArmDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un bras robot de laboratoire (non-fonctionnel).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un brazo robótico de laboratorio (no funcional).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar robotu kolu (yararsız).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Laborroboterarm (nicht funktionsfähig).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Лабораторный робот для декорации и атмосферы вашего жилья.";
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
                        return "Череп Жнеца Левиафана";
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
                        return "Инсталляция черепа одного из хищников.";
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
                               "Mantenga 'I' y haga clic para cambiar la intensidad del neón." + Environment.NewLine +
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
                               "Удерживайте 'B' выберете уровень голубого." + Environment.NewLine +
                               "Удерживайте 'F' выберете уровень яркости." + Environment.NewLine +
                               "Удерживайте 'I' выберете уровень интенсивность цвета." + Environment.NewLine +
                               "Удерживайте 'T' выберете уровень интенсивности неоновой трубки." + Environment.NewLine +
                               "Удерживайте 'E' выберете и измените цвет неоновой трубки." + Environment.NewLine;
                    else
                        return "Click to turn on/off, or:" + Environment.NewLine +
                               "Hold 'R' and click to change red levels." + Environment.NewLine +
                               "Hold 'G' and click to change green levels." + Environment.NewLine +
                               "Hold 'B' and click to change blue levels." + Environment.NewLine +
                               "Hold 'F' and click to adjust light range." + Environment.NewLine +
                               "Hold 'I' and click to change intensity." + Environment.NewLine +
                               "Hold 'T' and click to change neon tube intensity." + Environment.NewLine +
                               "Hold 'E' and click to change neon tube color." + Environment.NewLine;
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
                    else
                        return "Switch model";
                case "SwitchExosuitModel":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour changer le modèle de bras gauche, ou:" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour change le modèle de bras droit" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para cambiar el modelo de brazo izquierdo, o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar el modelo de brazo derecho" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Sol kol modelini değiştirmek için sol tıklayın," + Environment.NewLine +
                               "Sağ kol modelini değiştirmek için 'E' tuşuna basarken sol tıklayın" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken, um das linke Armmodell zu ändern, oder:" + Environment.NewLine +
                               "'E' drücken und klicken, um das rechte Armmodell zu wechseln." + Environment.NewLine;
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите, чтобы изменить:" + Environment.NewLine +
                               "Удерживайте 'E' и выберите, чтобы изменить модель." + Environment.NewLine;
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
                        return "Удерживайте 'E' и выберите, чтобы изменить размер";
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
                               "Удерживайте 'E' и выберите, чтобы изменить размер" + Environment.NewLine;
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
                    else
                        return "Reinforced cargo crate";
                case "CargoBox1aDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement renforcée permettant le transport des marchandises.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga reforzada que permite el transporte de mercancías.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İyi şeyleri taşımak için güçlendirilmiş kargo kutusu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine durch Stahlplatten verstärkte Frachtkiste für den Transport von Gütern.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Используется для защиты груза от внешних воздействий.";
                    else
                        return "A reinforced cargo crate made for the transport of goods.";
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
                    else
                        return "Cargo crate";
                case "CargoBox1bDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement permettant le transport des marchandises.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga que permite el transporte de mercancías.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İyi şeyleri taşımak için kargo kutusu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Frachtkiste für den Transport von Gütern.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Используется для транспортировки грузов.";
                    else
                        return "A cargo crate made for the transport of goods.";
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
                    else
                        return "Damaged cargo crate";
                case "CargoBox1DmgDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement en piteux état.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga en mal estado.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kullanılamayan hasarlı kargo kutusu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine unbrauchbar beschädigte Frachtkiste.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Поврежденный грузовой ящик.";
                    else
                        return "An unusable damaged cargo crate.";
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
                        return "Планше";
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
                        return "И снова какая-то бумаг с бюрократией.";
                    else
                        return "A clipboard containing various documents.";
                case "PenName":
                    if (UserLanguage == CountryCode.FR)
                        return "Stylo";
                    else if (UserLanguage == CountryCode.ES)
                        return "Lápice";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kalem";
                    else if (UserLanguage == CountryCode.DE)
                        return "Stift";
                    else if (UserLanguage == CountryCode.RU)
                        return "Ручка";
                    else
                        return "Pen";
                case "PenDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un stylo Alterra.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un lápice Alterra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Alterra kalemi.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Alterra-Stift.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Первый раз в первый класс.";
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
                    else
                        return "Pen holder";
                case "PenHolderDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un porte-stylo Alterra.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un portalápices Alterra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Alterra kalem tutucu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Alterra-Stifthalter.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Можно напихать всяких ручек и карандашей и засыпать все это скрепками.";
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
                    else
                        return "Unnecessary documents.";
                case "SofaStr1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit banc (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeño banco (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Bank (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Малый диван";
                    else
                        return "Small bench (decorative and functional)";
                case "SofaStr1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit banc : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un pequeño banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine kleine Bank: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
                    else
                        return "A small bench: Aesthetic and practical to rest.";
                case "SofaStr2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Banc moyen (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Mediano banco (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Orta boyutlu oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Mittelgroße Bank (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Средний диван";
                    else
                        return "Medium bench (decorative and functional)";
                case "SofaStr2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un banc moyen : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un mediano banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Orta boyutlu bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Bank mittlerer Größe: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
                    else
                        return "A medium bench: Aesthetic and practical to rest.";
                case "SofaStr3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Banc large (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Banco grande (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Lange Bank (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Большой диван";
                    else
                        return "Long bench (decorative and functional)";
                case "SofaStr3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un banc large : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un grande banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine große Bank: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
                    else
                        return "A long bench: Aesthetic and practical to rest.";
                case "SofaCorner2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Angle de banc (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Ángulo de banco (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Açılı oturak (dekoratif ve işlevsel)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Bank-Eckelement (dekorativ und funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Угловой диван";
                    else
                        return "Bench angle (decorative and functional)";
                case "SofaCorner2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un angle de banc : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un ángulo de banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Açılı bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Bankecke mit Winkel: Ästhetisch und praktisch zum Ausruhen.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Очень удобное место для отдыха.";
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
                    else
                        return "Customizable picture frame";
                case "CustomPictureFrameDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un cadre mural personnalisable, où vous pouvez envoyer une photo issue des galeries de PDA compatibles.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Marco de pared personalizables, puede cargar la imagen deseada desde las galerías de imágenes de una PDA compatible.";
                    else if (UserLanguage == CountryCode.TR)
                        return "PDA'nın fotoğraf albümünden istenilen görselin eklenebildiği, duvara asılan çerçevedir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein an der Wand befestigter, kundengerechter Rahmen. Laden Sie Ihr gewünschtes Bild von einer PDA-kompatiblen Fotogalerie hoch.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Настенная рамка для различных изображений и надписей.";
                    else
                        return "Wall-mounted customizable frame, upload desired image from compatible PDA photo galleries.";
                case "CustomPictureFrameTooltip":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour placer une image, ou:" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour ajuster la taille" + Environment.NewLine +
                               "Maintenez 'R' et cliquez pour tourner le cadre photo" + Environment.NewLine +
                               "Maintenez 'F' et cliquez pour changer de cadre" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para establecer la imagen o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para ajustar el tamaño" + Environment.NewLine +
                               "Mantenga 'R' y haga clic para girar el marco de la imagen" + Environment.NewLine +
                               "Mantenga 'F' y haga clic para cambiar el marco" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Görüntüyü ayarlamak için tıklayın veya:" + Environment.NewLine +
                               "Boyutu ayarlamak için 'E' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine +
                               "Fotoğraf çerçevesini çevirmek için 'R' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine +
                               "Çerçeveyi değiştirmek için 'F' tuşunu basılı tutun ve sol tıklayın" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.DE)
                        return "Klicken, um das Bild festzulegen, oder:" + Environment.NewLine +
                               "'E' drücken und klicken, um die Größe anzupassen." + Environment.NewLine +
                               "'R' drücken und klicken, um den Bilderrahmen zu drehen." + Environment.NewLine +
                               "'F' drücken und klicken, um den Rahmen zu ändern." + Environment.NewLine;
                    else if (UserLanguage == CountryCode.RU)
                        return "Нажмите, чтобы изменить:" + Environment.NewLine +
                               "Удерживайте 'E' чтобы отрегулировать размер." + Environment.NewLine +
                               "Удерживайте 'R' чтобы повернуть рамку." + Environment.NewLine +
                               "Удерживайте 'F' чтобы изменить форму рамки." + Environment.NewLine;
                    else
                        return "Click to set picture, or:" + Environment.NewLine +
                               "Hold 'E' and click to adjust size" + Environment.NewLine +
                               "Hold 'R' and click to rotate picture frame" + Environment.NewLine +
                               "Hold 'F' and click to change frame border" + Environment.NewLine;
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
                        return "Многолетнее инопланетное дерево.";
                    else
                        return "An alien land tree variety with interesting properties.";
                case "JungleTree1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre alien (A)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena (A)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı ağacı (A)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Außerirdischer Baum (A)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Дерево Alpha";
                    else
                        return "Alien tree (A)";
                case "JungleTree1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen alpha d'une variété d'arbre terrestre alien.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muestra alfa de una variedad de árbol terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara ağacının alfa örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Alpha-Probe einer fremden Landbaumsorte.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Альфа-образец разновидности инопланетного дерева.";
                    else
                        return "Alpha specimen of an alien land tree variety.";
                case "JungleTree2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Arbre alien (B)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Árbol alienígena (B)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı ağacı (B)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Außerirdischer Baum (B)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Дерево Beta";
                    else
                        return "Alien tree (B)";
                case "JungleTree2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen bêta d'une variété d'arbre terrestre alien.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Beta espécimen de una variedad de árbol terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara ağacının beta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beta-Probe einer fremden Landbaumsorte.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Бета-образец разновидности инопланетного дерева.";
                    else
                        return "Beta specimen of an alien land tree variety.";
                case "LandPlant1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante alien bioluminescente (A)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena bioluminiscente (A)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Biyolüminesan uzaylı bitkisi (A)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Biolumineszente Alienpflanze (A)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Alpha";
                    else
                        return "Bioluminescent alien plant (A)";
                case "LandPlant1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen alpha d'une variété de plante terrestre alien aux propriétés intéressantes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen alfa de una variedad de plantas terrestres alienígenas con propiedades interesantes.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İlginç özelliklere sahip uzaylı kara ağacının alfa örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Alpha-Probe einer fremden Landpflanzensorte mit interessanten Eigenschaften.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Альфа-образец разновидности инопланетного растения.";
                    else
                        return "Alpha specimen of an alien land plant variety with interesting properties.";
                case "LandPlant2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante alien bioluminescente (B)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta alienígena bioluminiscente (B)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Biyolüminesan uzaylı bitkisi (B)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Biolumineszente Alienpflanze (B)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Бета";
                    else
                        return "Bioluminescent alien plant (B)";
                case "LandPlant2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen bêta d'une variété de plante terrestre alien aux propriétés intéressantes.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Beta espécimen alfa de una variedad de plantas terrestres alienígenas con propiedades interesantes.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İlginç özelliklere sahip uzaylı kara ağacının beta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beta-Probe einer fremden Landpflanzensorte mit interessanten Eigenschaften.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Бета-образец разновидности инопланетного растения.";
                    else
                        return "Beta specimen of an alien land plant variety with interesting properties.";
                case "LandPlant3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen de plante terrestre alien (A)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de planta terrestre alienígena (A)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisi örneği (A)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer außerirdischen Landpflanze (A)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Gamma";
                    else
                        return "Specimen of alien land plant (A)";
                case "LandPlant3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire alpha de plante terrestre alien.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen vulgar alfa de planta terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisinin kaba bir alfa örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Alpha-Probe einer gewöhnlichen außerirdischen Landpflanze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Гамма-образец разновидности инопланетного растения.";
                    else
                        return "Alpha specimen of a vulgar alien land plant.";
                case "LandPlant4Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen de plante terrestre alien (B)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de planta terrestre alienígena (B)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisi örneği (B)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer außerirdischen Landpflanze (B)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Дельта";
                    else
                        return "Specimen of alien land plant (B)";
                case "LandPlant4Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire bêta de plante terrestre alien.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Especie Vulgar beta de planta terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisinin kaba bir beta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Beta-Probe einer gewöhnlichen außerirdischen Landpflanze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Дельта-образец разновидности инопланетного растения.";
                    else
                        return "Beta specimen of a vulgar alien land plant.";
                case "LandPlant5Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen de plante terrestre alien (C)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de planta terrestre alienígena (C)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisi örneği (C)";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe einer außerirdischen Landpflanze (C)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Эпсилон";
                    else
                        return "Specimen of alien land plant (C)";
                case "LandPlant5Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire thêta de plante terrestre alien.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen theta de vulgar planta terrestre alienígena.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzaylı kara bitkisinin teta örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Gamma-Probe einer gewöhnlichen außerirdischen Landpflanze.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Эпсилон-образец разновидности инопланетного растения.";
                    else
                        return "Theta specimen of a vulgar alien land plant.";
                case "TropicalPlantName":
                    if (UserLanguage == CountryCode.FR)
                        return "Une plante alien tropicale";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una planta alienígena tropical";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı bitki";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropische Alienpflanze";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропический куст";
                    else
                        return "A tropical alien plant";
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
                    else
                        return "Specimen of a vulgar tropical alien plant.";
                case "TropicalTreeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Un arbre alien tropical";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un árbol alienígena tropical";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropik bir uzaylı ağacı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tropischer Alienbaum";
                    else if (UserLanguage == CountryCode.RU)
                        return "Тропическое дерево";
                    else
                        return "A tropical alien tree";
                case "TropicalTreeDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Spécimen vulgaire d'arbre alien tropicale.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Espécimen de vulgar árbol alienígena tropical.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tropikal uzaylı bitkisinin örneği.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Probe eines gewöhnlichen außerirdischen Tropenbaums.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Образец инопланетного тропического дерева.";
                    else
                        return "Specimen of a vulgar tropical alien tree.";
                case "FernName":
                    if (UserLanguage == CountryCode.FR)
                        return "Fougère";
                    else if (UserLanguage == CountryCode.ES)
                        return "Helecho";
                    else if (UserLanguage == CountryCode.TR)
                        return "Eğreltiotu";
                    else if (UserLanguage == CountryCode.DE)
                        return "Farn";
                    else if (UserLanguage == CountryCode.RU)
                        return "Папоротник";
                    else
                        return "Fern";
                case "FernDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une fougère standard.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un helecho estándar.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Standart eğrelti.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein außerirdischer Farn.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Обычный папоротник и даже не курится.";
                    else
                        return "A standard fern.";
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
                        return "Клешня Краба";
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
                    else
                        return "A blue-tipped kelp species which tends to grow in or near to acidic brine pools on the ocean floor.";
                case "PyroCoralName":
                    if (UserLanguage == CountryCode.FR)
                        return "Corail de feu";
                    else if (UserLanguage == CountryCode.ES)
                        return "Coral Flamígero";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ateş Mercanı";
                    else if (UserLanguage == CountryCode.DE)
                        return "Feuerkoralle";
                    else if (UserLanguage == CountryCode.RU)
                        return "Магнетический коралл";
                    else
                        return "Pyrocoral";
                case "PyroCoralDescription":
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
                        return "Гигантское дерево Игло";
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
                        return "Клон, сделанный из огромного дерева, встреченного в глубокой бухте (единственная в своем роде, встречающаяся на планете). Может быть посажен на суше и под водой.";
                    else
                        return "Clone made from a vast tree encountered in a deep cove (the only one of its kind encountered on the planet). Can be planted on land and under water.";
                case "DisplayCoveTreeEggs":
                    if (UserLanguage == CountryCode.FR)
                        return "Afficher/masquer les oeufs";
                    else if (UserLanguage == CountryCode.ES)
                        return "Mostrar/ocultar huevos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yumurtaları göster/sakla";
                    else if (UserLanguage == CountryCode.DE)
                        return "Zeige/Verstecke Eier";
                    else if (UserLanguage == CountryCode.RU)
                        return "Скрыть/Показать семена";
                    else
                        return "Show/hide eggs";
                case "FloatingStoneName":
                    if (UserLanguage == CountryCode.FR)
                        return "Gousse d'ancrage";
                    else if (UserLanguage == CountryCode.ES)
                        return "Orbe anclado";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz Mayını";
                    else if (UserLanguage == CountryCode.DE)
                        return "Leuchtglobus";
                    else if (UserLanguage == CountryCode.RU)
                        return "Плавающий камень";
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
                        return "Пятнолистник";
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
                    else
                        return "A specimen of alien reeds.";
                case "BrineLilyName":
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
                    else
                        return "Brine Lily";
                case "BrineLilyDescription":
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
                    else
                        return "These lily-like plants have evolved to take advantage of the relative density of the acidic brine pools encountered near the ocean floor to float safely on the surface.";
                case "LostRiverPlantName":
                    if (UserLanguage == CountryCode.FR)
                        return "Plante de rivière perdue";
                    else if (UserLanguage == CountryCode.ES)
                        return "Planta de río perdida";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kayıp nehir bitkisi";
                    else if (UserLanguage == CountryCode.DE)
                        return "Tiefseepflanze";
                    else if (UserLanguage == CountryCode.RU)
                        return "Растение Потерянной реки";
                    else
                        return "Lost river plant";
                case "LostRiverPlantDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un curieux spécimen de plante trouvée dans la rivière perdue.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un curioso espécimen de planta encontrado en el río perdido.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kayıp nehirde ilginç bir bitki örneği bulundu.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein kurioses Pflanzenexemplar, das im verlorenen Fluss gefunden wurde.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Любопытный образец растения. Найден в русле потерянной реки.";
                    else
                        return "A curious plant specimen found in the lost river.";
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
                    else
                        return "Alien decorative mushrooms";
                case "SmallDeco3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Une variétée décorative de petits champignons alien. Peut être plantée sur terre et sous l'eau.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una variedad decorativa de pequeños hongos alienígenas. Se puede plantar en tierra y bajo el agua.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük uzaylı mantarların dekoratif çeşitliliği. Karada ve su altında ekilebilir.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Eine Vielzahl von dekorativen außerirdischen Pilzen. Kann zu Land und unter Wasser gepflanzt werden.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративное разнообразие маленьких чужеродных грибов. Могут быть посажены на суше и под водой.";
                    else
                        return "A decorative variety of small alien mushrooms. Can be planted on land and under water.";
                case "BrownCoralTubesName":
                    if (UserLanguage == CountryCode.FR)
                        return "Tubes de Corail d'argile";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tubos de coral embarrados";
                    else if (UserLanguage == CountryCode.TR)
                        return "Toprak mercanı boruları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Irdene Korallenröhren";
                    else if (UserLanguage == CountryCode.RU)
                        return "Коралловые трубы";
                    else
                        return "Earthen coral tubes";
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
                        return "Фуксевидная шишка";
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
                        return "Фиолетовый коралл";
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
                    else
                        return "Leviathan Skeletal Remains";
                case "DecorativeLockerName":
                    if (UserLanguage == CountryCode.FR)
                        return "Casier";
                    else if (UserLanguage == CountryCode.ES)
                        return "Armario";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dolap";
                    else if (UserLanguage == CountryCode.DE)
                        return "Wandspind (nicht funktional)";
                    else if (UserLanguage == CountryCode.RU)
                        return "Шкафчик";
                    else
                        return "Locker";
                case "DecorativeLockerDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un casier de stockage.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un armario de almacenamiento.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir depolama dolabı.";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein dekorativer Wandspind.";
                    else if (UserLanguage == CountryCode.RU)
                        return "Декоративный шкафчик.";
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
                    else
                        return "Edible air plants";
                case "DecorativeBigAirTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Grandes plantes terrestres";
                    else if (UserLanguage == CountryCode.ES)
                        return "Grandes plantas terrestres";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük karasal bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Große Landpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Крупные наземные растения";
                    else
                        return "Big air plants";
                case "DecorativeSmallAirTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Petites plantes terrestres";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeñas plantas terrestres";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük karasal bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Landpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Малые наземные растения";
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
                    else
                        return "Decorative aquatic plants";
                case "DecorativeBushesWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Buissons aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Arbustos de agua";
                    else if (UserLanguage == CountryCode.TR)
                        return "Su çalıları";
                    else if (UserLanguage == CountryCode.DE)
                        return "Aquatische Büsche";
                    else if (UserLanguage == CountryCode.RU)
                        return "Водные кусты";
                    else
                        return "Aquatic bushes";
                case "RegularSmallWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Petites plantes aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeñas plantas acuáticas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük sucul bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Kleine Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Малые водные растения";
                    else
                        return "Small aquatic plants";
                case "DecorativeBigWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Grandes plantes aquatiques";
                    else if (UserLanguage == CountryCode.ES)
                        return "Grandes plantas acuáticas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük su bitkileri";
                    else if (UserLanguage == CountryCode.DE)
                        return "Große Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Крупные водные растения";
                    else
                        return "Big aquatic plants";
                case "FunctionalBigWaterTab":
                    if (UserLanguage == CountryCode.FR)
                        return "Plantes aquatiques diverses";
                    else if (UserLanguage == CountryCode.ES)
                        return "Varias plantas acuáticas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Çeşitli sucul bitkiler";
                    else if (UserLanguage == CountryCode.DE)
                        return "Verschiedene Wasserpflanzen";
                    else if (UserLanguage == CountryCode.RU)
                        return "Различные водные растения";
                    else
                        return "Diverse aquatic plants";
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
                        return "Долгожитель";
                    else
                        return "Long planter";
                case "LongPlanterDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une longue jardinière (fonctionnelle).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una maceta larga (funcional).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun bir ekici (fonksiyonel).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein langer Pflanzentopf (funktional).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Длинный плантатор (функциональный).";
                    else
                        return "A long planter (functional).";
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
                    else
                        return "Stool";
                case "BarStoolDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un tabouret (fonctionnel).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un taburete (funcional).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir tabure (fonksiyonel).";
                    else if (UserLanguage == CountryCode.DE)
                        return "Ein Hocker (funktional).";
                    else if (UserLanguage == CountryCode.RU)
                        return "Стул (Функциональная).";
                    else
                        return "A stool (functional).";
                default:
                    return "?";
            }
        }
    }
}
