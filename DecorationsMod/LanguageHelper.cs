using System;
using static DecorationsMod.RegionHelper;

namespace DecorationsMod
{
    public static class LanguageHelper
    {
        public static CountryCode UserLanguage = RegionHelper.GetCountryCode();

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
                    else
                        return "Decorations fabricator";
                case "DecorationsFabricatorDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un fabricateur permettant de produire des objets décoratifs.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un Fabricador para producir artículos de decoración.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekorasyon eşyaları üretmek için bir üretici.";
                    else
                        return "A fabricator to produce decoration items.";
                case "Posters":
                    if (UserLanguage == CountryCode.TR)
                        return "Posterler";
                    else
                        return "Posters";
                case "LabElements":
                    if (UserLanguage == CountryCode.FR)
                        return "Éléments de laboratoire";
                    else if (UserLanguage == CountryCode.ES)
                        return "Elementos de laboratorio";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar eşyaları";
                    else
                        return "Laboratory elements";
                case "GlassContainers":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneur d'échantillons inutiles";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedor de muestra inútiles";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız cam kavanoz";
                    else
                        return "Useless glass containers";
                case "NonFunctionalAnalyzers":
                    if (UserLanguage == CountryCode.FR)
                        return "Analyseurs non-fonctionnels";
                    else if (UserLanguage == CountryCode.ES)
                        return "Analizadores no funcionales";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız inceleyici";
                    else
                        return "Non-functional analyzers";
                case "LabFurnitures":
                    if (UserLanguage == CountryCode.FR)
                        return "Mobilier de laboratoire";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muebles de laboratorio";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar Eşyaları";
                    else
                        return "Lab furnitures";
                case "WallMonitors":
                    if (UserLanguage == CountryCode.FR)
                        return "Ordinateurs muraux";
                    else if (UserLanguage == CountryCode.ES)
                        return "Computadoras de pared";
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvar bilgisayarları";
                    else
                        return "Wall computers";
                case "CircuitBoxes":
                    if (UserLanguage == CountryCode.FR)
                        return "Boîtes de circuits";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cajas de circuitos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sigorta";
                    else
                        return "Circuits boxes";
                case "IndoorStuff":
                    if (UserLanguage == CountryCode.FR)
                        return "Éléments d'intérieur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Elementos interiores";
                    else if (UserLanguage == CountryCode.TR)
                        return "İç eşyalar";
                    else
                        return "Indoor elements";
                case "Toys":
                    if (UserLanguage == CountryCode.FR)
                        return "Jouets";
                    else if (UserLanguage == CountryCode.ES)
                        return "Juguetes";
                    else if (UserLanguage == CountryCode.TR)
                        return "Oyuncaklar";
                    else
                        return "Toys";
                case "Accessories":
                    if (UserLanguage == CountryCode.FR)
                        return "Accessoires";
                    else if (UserLanguage == CountryCode.ES)
                        return "Accesorios";
                    else if (UserLanguage == CountryCode.TR)
                        return "Aksesuarlar";
                    else
                        return "Accessories";
                case "LabContainer4Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Conteneur d'échantillons cylindrique inversé";
                    else if (UserLanguage == CountryCode.ES)
                        return "Contenedor de muestra cilíndrico invertido";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ters örnek kavanozu";
                    else
                        return "Inverted cylindrical sample container";
                case "LabContainer4Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un conteneur d'échantillons cylindrique inversé, probablement inutile.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un contenedor de muestra cilíndrico invertido, probablemente inútil.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Ters çevrilmiş bir örnek kavanozu, muhtemelen yararsız.";
                    else
                        return "An inverted cylindrical sample container, probably useless.";
                case "LabTubeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Étagères tubulaires de laboratoire (non-fonctionnelles)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes tubulares de laboratorio (no funcionales)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Boru şeklinde raflar (yararsız)";
                    else
                        return "Tubular laboratory shelves (non-functional)";
                case "LabTubeDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des étagères tubulaires pour stocker des échantillons.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes tubulares para almacenar muestras.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnekleri depolamak için boru şeklinde raflar.";
                    else
                        return "Tubular shelves for storing samples.";
                case "LabCartName":
                    if (UserLanguage == CountryCode.FR)
                        return "Chariot de laboratoire (non-fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Carro de laboratorio (no-funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labatuvar arabası (yararsız)";
                    else
                        return "Lab cart (non-functional)";
                case "LabCartDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un chariot à échantillons de laboratoire.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un carro de muestra de laboratorio.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir örnek arabası.";
                    else
                        return "A laboratory sample cart.";
                case "LabShelfName":
                    if (UserLanguage == CountryCode.FR)
                        return "Étagères de laboratoire (non-fonctionnelles)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes de laboratorio (no funcionales)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar rafı (yararsız)";
                    else
                        return "Laboratory shelves (non-functional)";
                case "LabShelfDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des étagères pour stocker des échantillons.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estantes para almacenar muestras.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnekleri depolamak için raf.";
                    else
                        return "Shelves for storing samples.";
                case "WallMonitor1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Moniteur mural";
                    else if (UserLanguage == CountryCode.ES)
                        return "Monitor de pared";
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvar monitörü";
                    else
                        return "Wall monitor";
                case "WallMonitor1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un moniteur mural (doit être relié à un serveur pour fonctionner).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un monitor de pared (debe estar conectado a un servidor para funcionar).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bir duvar monitörü (kullanılması için bir sunucuya bağlanılması gerekiyor).";
                    else
                        return "A wall monitor (must be connected to a server to work).";
                case "WallMonitor2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Ordinateur mural simple";
                    else if (UserLanguage == CountryCode.ES)
                        return "Computadora de pared simple";
                    else if (UserLanguage == CountryCode.TR)
                        return "Basit duvar monitörü";
                    else
                        return "Simple wall computer";
                case "WallMonitor2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit ordinateur mural simple (doit être relié à un serveur pour fonctionner).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una computadora pequeña y sencilla montada en la pared (debe estar conectada a un servidor para funcionar).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük, basit duvara monte edilen bilgisayar (kullanılması için bir sunucuya bağlanılması gerekiyor).";
                    else
                        return "A small, simple wall-mounted computer (must be connected to a server to function).";
                case "WallMonitor3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Ordinateur mural";
                    else if (UserLanguage == CountryCode.ES)
                        return "Computadora de pared";
                    else if (UserLanguage == CountryCode.TR)
                        return "Duvar bilgisayarı";
                    else
                        return "Wall computer";
                case "WallMonitor3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un ordinateur mural performant (doit être relié à un serveur pour fonctionner).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una poderosa computadora montada en la pared (debe estar conectada a un servidor para funcionar).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Güçlü duvara monte edilen bilgisayar (kullanılması için bir sunucuya bağlanılması gerekiyor).";
                    else
                        return "A powerful wall-mounted computer (must be connected to a server to function).";
                case "CircuitBox1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Boîte de circuits";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de circuitos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sigorta";
                    else
                        return "Circuits box";
                case "CircuitBox1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Une boîte de circuits simple (permet la mise sous tension des appareils électriques).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de circuitos simple (permite la alimentación de dispositivos eléctricos).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Basit sigorta (elektrikli eşyalara güç iletimi sağlar).";
                    else
                        return "A simple circuit box (allows powering of electrical devices).";
                case "CircuitBox2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Relai de connectivité";
                    else if (UserLanguage == CountryCode.ES)
                        return "Relé de conectividad";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bağlantı Rölesi";
                    else
                        return "Connectivity relay";
                case "CircuitBox2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un relai permettant l'interconnexion des équipements.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un relevo para la interconexión de equipos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Eşyaların ara iletişimi için röle.";
                    else
                        return "A relay for the interconnection of equipment.";
                case "CircuitBox3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Relai électrique haute tension";
                    else if (UserLanguage == CountryCode.ES)
                        return "Relé eléctrico de alta tensión";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yüksek voltajlı elektrik rölesi";
                    else
                        return "High voltage electrical relay";
                case "CircuitBox3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un composant permettant l'acheminement de grandes quantités d'énergie.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un componente que permite el transporte de grandes cantidades de energía.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyük enerjileri transfer etmek için kullanılan kutu.";
                    else
                        return "A component allowing the transport of large amounts of energy.";
                case "SpecimenAnalyzerName":
                    if (UserLanguage == CountryCode.FR)
                        return "Analyseur de spécimen";
                    else if (UserLanguage == CountryCode.ES)
                        return "Analizador de muestras";
                    else if (UserLanguage == CountryCode.TR)
                        return "Örnek İnceleyici";
                    else
                        return "Specimen analyzer";
                case "SpecimenAnalyzerDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Étudie des spécimens pour en déduire des schémas de fabrication.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Estudie especímenes para deducir patrones de fabricación.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Üretim kalıplarını anlamak için örnekler üzerinde çalışır.";
                    else
                        return "Study specimens to deduce patterns of manufacture.";
                case "SmallEmperorName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée d'empereur léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeca de leviatán emperador";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz İmparatoru oyuncağı";
                    else
                        return "Emperor leviathan doll";
                case "SmallEmperorDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée d'empereur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca emperador leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu Deniz İmparatoru oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else
                        return "This emperor leviathan doll was created from observations made on 4546B.";
                case "MarlaCatName":
                    return "Marla";
                case "MarlaCatDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Le chat de EatMyDiction.";
                    else if (UserLanguage == CountryCode.ES)
                        return "El gato de Eat My Diction.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Eat My Diction'un kedisi.";
                    else
                        return "Eat My Diction's cat.";
                case "MarkiDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Une poupée peu commune";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un muñeco inusual";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız oyuncak";
                    else
                        return "An unusual doll";
                case "MarkiDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une poupée peu commune.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un muñeco inusual.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız bir oyuncak.";
                    else
                        return "An unusual doll.";
                case "JackSepticEyeName":
                    if (UserLanguage == CountryCode.FR)
                        return "Fosse sceptique de Jack";
                    else if (UserLanguage == CountryCode.ES)
                        return "Tanque de JackSeptic";
                    else if (UserLanguage == CountryCode.TR)
                        return "JackSepticEye Tüpü";
                    else
                        return "Jack's Septic Tank";
                case "JackSepticEyeDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un objet peu commun.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un objeto inusual.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız bir eşya.";
                    else
                        return "An unusual item.";
                case "LeviathanDolls":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupées de léviathans";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñecas Leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Canavar oyuncakları";
                    else
                        return "Leviathan dolls";
                case "GhostLeviathanDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de léviathan fantôme";
                    else if (UserLanguage == CountryCode.ES)
                        return "Fantasma leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hayalet canavar oyuncağı";
                    else
                        return "Ghost leviathan doll";
                case "GhostLeviathanDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de léviathan fantôme a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca fantasma leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu hayalet canavar oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else
                        return "This ghost leviathan doll was created from observations made on 4546B.";
                case "ReaperLeviathanDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de faucheur léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Segador leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı canavar oyuncağı";
                    else
                        return "Reaper leviathan doll";
                case "ReaperLeviathanDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de faucheur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca segador leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu tırpanlı canavar oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else
                        return "This reaper leviathan doll was created from observations made on 4546B.";
                case "SeaDragonDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de dragon des mers léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Dragon marino leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz ejderhası oyuncağı";
                    else
                        return "Sea dragon leviathan doll";
                case "SeaDragonDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de dragon des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca dragon marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu deniz ejderhası oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else
                        return "This sea dragon leviathan doll was created from observations made on 4546B.";
                case "SeaTreaderDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de pèlerin des mers léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caminante marino leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Deniz gezgini oyuncağı";
                    else
                        return "Sea treader leviathan doll";
                case "SeaTreaderDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de pèlerin des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca caminante marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu deniz gezgini oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else
                        return "This sea treader leviathan doll was created from observations made on 4546B.";
                case "ReefBackDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de reefback léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Portarrecifes leviatán muñeca";
                    else if (UserLanguage == CountryCode.TR)
                        return "Resif devi oyuncağı";
                    else
                        return "Reefback leviathan doll";
                case "ReefBackDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de reefback léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca portarrecifes leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu resif devi oyuncağı 4546B'deki gözlemlerle yapıldı";
                    else
                        return "This reefback leviathan doll was created from observations made on 4546B.";
                case "CuddleFishDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de câlineur";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeca pez monada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Sevimli balık oyuncağı";
                    else
                        return "Cuddlefish doll";
                case "CuddleFishDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Cette poupée de câlineur a été créée à partir des observations faites sur 4546B.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Esta muñeca pez monada fue creada a partir de observaciones hechas en 4546B.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bu sevimli balık oyuncağı 4546B'deki gözlemlerle yapıldı.";
                    else
                        return "This cuddlefish doll was created from observations made on 4546B.";
                case "ReactorLampName":
                    if (UserLanguage == CountryCode.FR)
                        return "Lampe";
                    else if (UserLanguage == CountryCode.ES)
                        return "Lámpara";
                    else if (UserLanguage == CountryCode.TR)
                        return "Lamba";
                    else
                        return "Lamp";
                case "ReactorLampDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une lampe customisable.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una lámpara personalizable.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Özelleştirilebilir lamba.";
                    else
                        return "A customizable lamp.";
                case "SeamothDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de seamoth";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeca seamoth";
                    else if (UserLanguage == CountryCode.TR)
                        return "Seamoth Oyuncağı";
                    else
                        return "Seamoth doll";
                case "SeamothDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une miniature décorative du seamoth.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura decorativa de seamoth.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Minyatür seamoth oyuncağı.";
                    else
                        return "A decorative miniature of the seamoth.";
                case "ExosuitDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Poupée de combinaison PRAWN";
                    else if (UserLanguage == CountryCode.ES)
                        return "Muñeca traje PRAWN";
                    else if (UserLanguage == CountryCode.TR)
                        return "Suban giysi oyuncağı";
                    else
                        return "PRAWN suit doll";
                case "ExosuitDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une miniature décorative de la combinaison PRAWN.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una miniatura decorativa de traje PRAWN.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Minyatür suban giysi oyuncağı.";
                    else
                        return "A decorative miniature of the PRAWN suit.";
                case "ForkLiftDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Chariot élévateur (non-fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Carretilla elevadora (no funcionales)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Forklift (yararsız)";
                    else
                        return "Forklift (non-functional)";
                case "ForkLiftDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un chariot élévateur décoratif.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una carretilla elevadora decorativa.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dekoratif forklift.";
                    else
                        return "A decorative forklift.";
                case "DrinksAndFood":
                    if (UserLanguage == CountryCode.FR)
                        return "Boissons & nourriture";
                    else if (UserLanguage == CountryCode.ES)
                        return "Bebidas & comida";
                    else if (UserLanguage == CountryCode.TR)
                        return "İçecek & Yiyecekler";
                    else
                        return "Drinks & food";
                case "BarBottleName":
                    if (UserLanguage == CountryCode.FR)
                        return "Bouteille de bar";
                    else if (UserLanguage == CountryCode.ES)
                        return "Botella de bar";
                    else if (UserLanguage == CountryCode.TR)
                        return "Şişe";
                    else
                        return "Bar bottle";
                case "BarBottleDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une bouteille contenant un délicieux breuvage.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una botella que contiene una deliciosa bebida.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Lezzetli bir içecek barındıran şişe.";
                    else
                        return "A bottle containing a delicious beverage.";
                case "BarCup2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Gobelet";
                    else if (UserLanguage == CountryCode.ES)
                        return "Taza";
                    else if (UserLanguage == CountryCode.TR)
                        return "Bardak";
                    else
                        return "Cup";
                case "BarCup2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un gobelet fait de titane.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una taza hecha de titanio.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Titanyum bardak.";
                    else
                        return "A cup made of titanium.";
                case "BarCup1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit gobelet";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeña taza";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bardak";
                    else
                        return "Small cup";
                case "BarCup1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit gobelet fait de titane.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una pequeña taza hecha de titanio.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük titanyum bardak.";
                    else
                        return "A small cup made of titanium.";
                case "BarFood1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit plat";
                    else if (UserLanguage == CountryCode.ES)
                        return "Plato pequeño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük yemek";
                    else
                        return "Small meal";
                case "BarFood1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un plat à base de poisson.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un plato hecho de pescado.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Balıktan yapılmış bir yemek.";
                    else
                        return "A meal made of fish.";
                case "BarFood2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Plateau repas";
                    else if (UserLanguage == CountryCode.ES)
                        return "Bandeja de comida";
                    else if (UserLanguage == CountryCode.TR)
                        return "1 tepsi yemek";
                    else
                        return "Meal tray";
                case "BarFood2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un repas complet et équilibré.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una comida completa y equilibrada.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dengeli bir öğün.";
                    else
                        return "A complete and balanced meal.";
                case "BarNapkinsName":
                    if (UserLanguage == CountryCode.FR)
                        return "Serviettes de table";
                    else if (UserLanguage == CountryCode.ES)
                        return "Servilletas";
                    else if (UserLanguage == CountryCode.TR)
                        return "Peçete";
                    else
                        return "Napkins";
                case "BarNapkinsDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des serviettes de table en maille de fibre.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Servilletas en malla de fibra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Fiber örgüden yapılmış peçete.";
                    else
                        return "Napkins made of fiber mesh.";
                case "LabRobotArmName":
                    if (UserLanguage == CountryCode.FR)
                        return "Bras robot (non-fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Brazo robótico (no funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Yararsız Robot Kolu";
                    else
                        return "Robot arm (non-functional)";
                case "LabRobotArmDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un bras robot de laboratoire (non-fonctionnel).";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un brazo robótico de laboratorio (no funcional).";
                    else if (UserLanguage == CountryCode.TR)
                        return "Labaratuvar robotu kolu (yararsız).";
                    else
                        return "A laboratory robot arm (non-functional).";
                case "ReaperSkullDollName":
                    if (UserLanguage == CountryCode.FR)
                        return "Crâne de faucheur léviathan";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cráneo de segador leviatán";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı canavar kafatası";
                    else
                        return "Reaper leviathan skull";
                case "ReaperSkullDollDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une réplique de crâne d'un faucheur léviathan.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una réplica del cráneo de un segador leviatán.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Tırpanlı canavar kafatasının bir replikası.";
                    else
                        return "A replica of a reaper leviathan skull.";
                case "LampTooltip":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour ajuster la portée, ou :" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour changer la couleur du néon" + Environment.NewLine +
                               "Maintenez 'I' et cliquez pour changer l'intensité" + Environment.NewLine +
                               "Maintenez 'R' et cliquez pour changer le niveau de rouge" + Environment.NewLine +
                               "Maintenez 'G' et cliquez pour changer le niveau de vert" + Environment.NewLine +
                               "Maintenez 'B' et cliquez pour changer le niveau de bleu" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para ajustar el alcance, o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar el color del neón" + Environment.NewLine +
                               "Mantenga 'I' y haga clic para cambiar la intensidad" + Environment.NewLine +
                               "Mantenga 'R' y haga clic para cambiar los niveles rojos" + Environment.NewLine +
                               "Mantenga 'G' y haga clic para cambiar los niveles verdes" + Environment.NewLine +
                               "Mantenga 'B' y haga clic para cambiar los niveles azules" + Environment.NewLine;
                    else if (UserLanguage == CountryCode.TR)
                        return "Işık menzilini değiştirmek için tıklayın, ya da:" + Environment.NewLine +
                               "Neon tüp rengini değiştirmek için 'E' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Yoğunluğu değiştirmek için 'I' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Kırmızı rengi değiştirmek için 'R' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Yeşil rengi değiştirmek için 'G' tuşuna basarken sol tıklayın." + Environment.NewLine +
                               "Mavi rengi değiştirmek için 'B' tuşuna basarken sol tıklayın." + Environment.NewLine;
                    else
                        return "Click to adjust light range, or:" + Environment.NewLine +
                               "Hold 'E' and click to change neon tube color" + Environment.NewLine +
                               "Hold 'I' and click to change intensity" + Environment.NewLine +
                               "Hold 'R' and click to change red levels" + Environment.NewLine +
                               "Hold 'G' and click to change green levels" + Environment.NewLine +
                               "Hold 'B' and click to change blue levels" + Environment.NewLine;
                case "SwitchSeamothModel":
                    if (UserLanguage == CountryCode.FR)
                        return "Changer le modèle";
                    else if (UserLanguage == CountryCode.ES)
                        return "Cambiar el modelo";
                    else if (UserLanguage == CountryCode.TR)
                        return "Model değiştir";
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
                               "Sağ kol modelini değiştirmek için 'E' tuşuna basarken sol tıklayın." + Environment.NewLine;
                    else
                        return "Click to change left arm model, or:" + Environment.NewLine +
                               "Hold 'E' and click to change right arm model" + Environment.NewLine;
                case "AdjustItemSize":
                    if (UserLanguage == CountryCode.FR)
                        return "Cliquez pour modifier la taille";
                    else if (UserLanguage == CountryCode.ES)
                        return "Haga clic para cambiar el tamaño";
                    else if (UserLanguage == CountryCode.TR)
                        return "Büyüklüğü ayarlamak için tıklayın";
                    else
                        return "Click to adjust size";
                case "CargoBox1aName":
                    if (UserLanguage == CountryCode.FR)
                        return "Caisse de chargement renforcée";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de carga reforzada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Güçlendirilimiş Kargo Kutusu";
                    else
                        return "Reinforced cargo crate";
                case "CargoBox1aDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement renforcée permettant le transport des marchandises.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga reforzada que permite el transporte de mercancías.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İyi şeyleri taşımak için güçlendirilmiş kargo kutusu.";
                    else
                        return "A reinforced cargo crate made for the transport of goods.";
                case "CargoBox1bName":
                    if (UserLanguage == CountryCode.FR)
                        return "Caisse de chargement";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de carga";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kargo Kutusu";
                    else
                        return "Cargo crate";
                case "CargoBox1bDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement permettant le transport des marchandises.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga que permite el transporte de mercancías.";
                    else if (UserLanguage == CountryCode.TR)
                        return "İyi şeyleri taşımak için kargo kutusu.";
                    else
                        return "A cargo crate made for the transport of goods.";
                case "CargoBox1DmgName":
                    if (UserLanguage == CountryCode.FR)
                        return "Caisse de chargement endommagée";
                    else if (UserLanguage == CountryCode.ES)
                        return "Caja de carga dañada";
                    else if (UserLanguage == CountryCode.TR)
                        return "Hasarlı kargo kutusu";
                    else
                        return "Damaged cargo crate";
                case "CargoBox1DmgDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Une caisse de chargement en piteux état.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una caja de carga en mal estado.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kullanılamayan hasarlı kargo kutusu.";
                    else
                        return "An unusable damaged cargo crate.";
                case "Folder1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Documents";
                    else if (UserLanguage == CountryCode.ES)
                        return "Documentos";
                    else if (UserLanguage == CountryCode.TR)
                        return "Dosya";
                    else
                        return "Documents";
                case "Folder1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un dossier contenant divers documents.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Una carpeta que contiene varios documentos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Önemli dökümanlar içeren bir dosya.";
                    else
                        return "A folder containing various documents.";
                case "ClipboardName":
                    if (UserLanguage == CountryCode.FR)
                        return "Presse-papiers";
                    else if (UserLanguage == CountryCode.ES)
                        return "Portapapeles";
                    else if (UserLanguage == CountryCode.TR)
                        return "Klipli dosya";
                    else
                        return "Clipboard";
                case "ClipboardDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un presse-papiers contenant divers documents.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un portapapeles que contiene varios documentos.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Önemli dökümanlar içeren klipli dosya";
                    else
                        return "A clipboard containing various documents.";
                case "PenName":
                    if (UserLanguage == CountryCode.FR)
                        return "Stylo";
                    else if (UserLanguage == CountryCode.ES)
                        return "Lápice";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kalem";
                    else
                        return "Pen";
                case "PenDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un stylo Alterra.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un lápice Alterra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Alterra kalemi.";
                    else
                        return "An Alterra pen.";
                case "PenHolderName":
                    if (UserLanguage == CountryCode.FR)
                        return "Porte-stylo";
                    else if (UserLanguage == CountryCode.ES)
                        return "Portalápices";
                    else if (UserLanguage == CountryCode.TR)
                        return "Kalem tutucu";
                    else
                        return "Pen holder";
                case "PenHolderDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Un porte-stylo Alterra.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un portalápices Alterra.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Alterra kalem tutucu.";
                    else
                        return "An Alterra pen holder.";
                case "PaperTrashName":
                    if (UserLanguage == CountryCode.FR)
                        return "Papiers froissés";
                    else if (UserLanguage == CountryCode.ES)
                        return "Papeles arrugados";
                    else if (UserLanguage == CountryCode.TR)
                        return "Buruşuk kağıt";
                    else
                        return "Crumpled papers";
                case "PaperTrashDescription":
                    if (UserLanguage == CountryCode.FR)
                        return "Des documents inutiles.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Documentos innecesarios";
                    else if (UserLanguage == CountryCode.TR)
                        return "Önemsiz dökümanlar.";
                    else
                        return "Unnecessary documents.";
                case "SofaStr1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit banc (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeño banco (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük oturak (dekoratif ve işlevsel)";
                    else
                        return "Small bench (decorative and functional)";
                case "SofaStr1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit banc : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un pequeño banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else
                        return "A small bench: Aesthetic and practical to rest.";
                case "SofaStr2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Banc moyen (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Mediano banco (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Orta boyutlu oturak (dekoratif ve işlevsel)";
                    else
                        return "Medium bench (decorative and functional)";
                case "SofaStr2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un banc moyen : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un mediano banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Orta boyutlu bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else
                        return "A medium bench: Aesthetic and practical to rest.";
                case "SofaStr3Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Banc large (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Banco grande (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun oturak (dekoratif ve işlevsel)";
                    else
                        return "Long bench (decorative and functional)";
                case "SofaStr3Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un banc large : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un grande banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Uzun bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else
                        return "A long bench: Aesthetic and practical to rest.";
                case "SofaCorner1Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Petit angle de banc (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Pequeño ángulo de banco (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük açılı Oturak (dekoratif ve işlevsel)";
                    else
                        return "Small bench angle (decorative and functional)";
                case "SofaCorner1Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un petit angle de banc : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un pequeño ángulo de banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Küçük bir açılı oturak: Estetik ve dinlenmek için kullanışlı.";
                    else
                        return "A small bench angle: Aesthetic and practical to rest.";
                case "SofaCorner2Name":
                    if (UserLanguage == CountryCode.FR)
                        return "Angle de banc (décoratif et fonctionnel)";
                    else if (UserLanguage == CountryCode.ES)
                        return "Ángulo de banco (decorativo y funcional)";
                    else if (UserLanguage == CountryCode.TR)
                        return "Açılı oturak (dekoratif ve işlevsel)";
                    else
                        return "Bench angle (decorative and functional)";
                case "SofaCorner2Description":
                    if (UserLanguage == CountryCode.FR)
                        return "Un angle de banc : Esthétique et pratique pour se reposer.";
                    else if (UserLanguage == CountryCode.ES)
                        return "Un ángulo de banco: Estético y práctico para un descanso.";
                    else if (UserLanguage == CountryCode.TR)
                        return "Açılı bir oturak: Estetik ve dinlenmek için kullanışlı.";
                    else
                        return "A bench angle: Aesthetic and practical to rest.";
                default:
                    return "?";
            }
        }
    }
}
