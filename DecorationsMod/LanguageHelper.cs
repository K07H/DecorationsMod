using System;

namespace DecorationsMod
{
    public static class LanguageHelper
    {
        public static RegionHelper.CountryCode UserLanguage = RegionHelper.GetCountryCode();

        public static string GetFriendlyWord(string word)
        {
            switch (word)
            {
                case "DecorationsFabricatorName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Fabricateur de décorations";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Fabricador de decoraciones";
                    else
                        return "Decorations fabricator";
                case "DecorationsFabricatorDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un fabricateur permettant de produire des objets décoratifs.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un Fabricador para producir artículos de decoración.";
                    else
                        return "A fabricator to produce decoration items.";
                case "Posters":
                    return "Posters";
                case "LabElements":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Éléments de laboratoire";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Elementos de laboratorio";
                    else
                        return "Laboratory elements";
                case "GlassContainers":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Conteneur d'échantillons inutiles";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Contenedor de muestra inútiles";
                    else
                        return "Useless glass containers";
                case "NonFunctionalAnalyzers":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Analyseurs non-fonctionnels";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Analizadores no funcionales";
                    else
                        return "Non-functional analyzers";
                case "LabFurnitures":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Mobilier de laboratoire";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Muebles de laboratorio";
                    else
                        return "Lab furnitures";
                case "WallMonitors":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Ordinateurs muraux";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Computadoras de pared";
                    else
                        return "Wall computers";
                case "CircuitBoxes":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Boîtes de circuits";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Cajas de circuitos";
                    else
                        return "Circuits boxes";
                case "IndoorStuff":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Éléments d'intérieur";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Elementos interiores";
                    else
                        return "Indoor elements";
                case "Toys":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Jouets";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Juguetes";
                    else
                        return "Toys";
                case "Accessories":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Accessoires";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Accesorios";
                    else
                        return "Accessories";
                case "LabContainer4Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Conteneur d'échantillons cylindrique inversé";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Contenedor de muestra cilíndrico invertido";
                    else
                        return "Inverted cylindrical sample container";
                case "LabContainer4Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un conteneur d'échantillons cylindrique inversé, probablement inutile.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un contenedor de muestra cilíndrico invertido, probablemente inútil.";
                    else
                        return "An inverted cylindrical sample container, probably useless.";
                case "LabTubeName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Étagères tubulaires de laboratoire (non-fonctionnelles)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Estantes tubulares de laboratorio (no funcionales)";
                    else
                        return "Tubular laboratory shelves (non-functional)";
                case "LabTubeDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Des étagères tubulaires pour stocker des échantillons.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Estantes tubulares para almacenar muestras.";
                    else
                        return "Tubular shelves for storing samples.";
                case "LabCartName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Chariot de laboratoire (non-fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Carro de laboratorio (no-funcional)";
                    else
                        return "Lab cart (non-functional)";
                case "LabCartDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un chariot à échantillons de laboratoire.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un carro de muestra de laboratorio.";
                    else
                        return "A laboratory sample cart.";
                case "LabShelfName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Étagères de laboratoire (non-fonctionnelles)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Estantes de laboratorio (no funcionales)";
                    else
                        return "Laboratory shelves (non-functional)";
                case "LabShelfDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Des étagères pour stocker des échantillons.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Estantes para almacenar muestras.";
                    else
                        return "Shelves for storing samples.";
                case "WallMonitor1Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Moniteur mural";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Monitor de pared";
                    else
                        return "Wall monitor";
                case "WallMonitor1Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un moniteur mural (doit être relié à un serveur pour fonctionner).";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un monitor de pared (debe estar conectado a un servidor para funcionar).";
                    else
                        return "A wall monitor (must be connected to a server to work).";
                case "WallMonitor2Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Ordinateur mural simple";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Computadora de pared simple";
                    else
                        return "Simple wall computer";
                case "WallMonitor2Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un petit ordinateur mural simple (doit être relié à un serveur pour fonctionner).";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una computadora pequeña y sencilla montada en la pared (debe estar conectada a un servidor para funcionar).";
                    else
                        return "A small, simple wall-mounted computer (must be connected to a server to function).";
                case "WallMonitor3Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Ordinateur mural";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Computadora de pared";
                    else
                        return "Wall computer";
                case "WallMonitor3Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un ordinateur mural performant (doit être relié à un serveur pour fonctionner).";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una poderosa computadora montada en la pared (debe estar conectada a un servidor para funcionar).";
                    else
                        return "A powerful wall-mounted computer (must be connected to a server to function).";
                case "CircuitBox1Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Boîte de circuits";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Caja de circuitos";
                    else
                        return "Circuits box";
                case "CircuitBox1Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une boîte de circuits simple (permet la mise sous tension des appareils électriques).";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una caja de circuitos simple (permite la alimentación de dispositivos eléctricos).";
                    else
                        return "A simple circuit box (allows powering of electrical devices).";
                case "CircuitBox2Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Relai de connectivité";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Relé de conectividad";
                    else
                        return "Connectivity relay";
                case "CircuitBox2Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un relai permettant l'interconnexion des équipements.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un relevo para la interconexión de equipos.";
                    else
                        return "A relay for the interconnection of equipment.";
                case "CircuitBox3Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Relai électrique haute tension";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Relé eléctrico de alta tensión";
                    else
                        return "High voltage electrical relay";
                case "CircuitBox3Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un composant permettant l'acheminement de grandes quantités d'énergie.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un componente que permite el transporte de grandes cantidades de energía.";
                    else
                        return "A component allowing the transport of large amounts of energy.";
                case "SpecimenAnalyzerName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Analyseur de spécimen";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Analizador de muestras";
                    else
                        return "Specimen analyzer";
                case "SpecimenAnalyzerDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Étudie des spécimens pour en déduire des schémas de fabrication.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Estudie especímenes para deducir patrones de fabricación.";
                    else
                        return "Study specimens to deduce patterns of manufacture.";
                case "SmallEmperorName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée d'empereur léviathan";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Muñeca de leviatán emperador";
                    else
                        return "Emperor leviathan doll";
                case "SmallEmperorDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cette poupée d'empereur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Esta muñeca emperador leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else
                        return "This emperor leviathan doll was created from observations made on 4546B.";
                case "MarlaCatName":
                    return "Marla";
                case "MarlaCatDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Le chat de EatMyDiction.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "El gato de Eat My Diction.";
                    else
                        return "Eat My Diction's cat.";
                case "MarkiDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une poupée peu commune";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un muñeco inusual";
                    else
                        return "An unusual doll";
                case "MarkiDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une poupée peu commune.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un muñeco inusual.";
                    else
                        return "An unusual doll.";
                case "JackSepticEyeName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Fosse sceptique de Jack";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Tanque de JackSeptic";
                    else
                        return "Jack's Septic Tank";
                case "JackSepticEyeDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un objet peu commun.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un objeto inusual.";
                    else
                        return "An unusual item.";
                case "LeviathanDolls":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupées de léviathans";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Muñecas Leviatán";
                    else
                        return "Leviathan dolls";
                case "GhostLeviathanDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de léviathan fantôme";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Fantasma leviatán muñeca";
                    else
                        return "Ghost leviathan doll";
                case "GhostLeviathanDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cette poupée de léviathan fantôme a été créée à partir des observations faites sur 4546B.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Esta muñeca fantasma leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else
                        return "This ghost leviathan doll was created from observations made on 4546B.";
                case "ReaperLeviathanDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de faucheur léviathan";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Segador leviatán muñeca";
                    else
                        return "Reaper leviathan doll";
                case "ReaperLeviathanDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cette poupée de faucheur léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Esta muñeca segador leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else
                        return "This reaper leviathan doll was created from observations made on 4546B.";
                case "SeaDragonDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de dragon des mers léviathan";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Dragon marino leviatán muñeca";
                    else
                        return "Sea dragon leviathan doll";
                case "SeaDragonDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cette poupée de dragon des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Esta muñeca dragon marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else
                        return "This sea dragon leviathan doll was created from observations made on 4546B.";
                case "SeaTreaderDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de pèlerin des mers léviathan";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Caminante marino leviatán muñeca";
                    else
                        return "Sea treader leviathan doll";
                case "SeaTreaderDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cette poupée de pèlerin des mers léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Esta muñeca caminante marino leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else
                        return "This sea treader leviathan doll was created from observations made on 4546B.";
                case "ReefBackDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de reefback léviathan";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Portarrecifes leviatán muñeca";
                    else
                        return "Reefback leviathan doll";
                case "ReefBackDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cette poupée de reefback léviathan a été créée à partir des observations faites sur 4546B.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Esta muñeca portarrecifes leviatán fue creada a partir de observaciones hechas en 4546B.";
                    else
                        return "This reefback leviathan doll was created from observations made on 4546B.";
                case "CuddleFishDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de câlineur";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Muñeca pez monada";
                    else
                        return "Cuddlefish doll";
                case "CuddleFishDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cette poupée de câlineur a été créée à partir des observations faites sur 4546B.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Esta muñeca pez monada fue creada a partir de observaciones hechas en 4546B.";
                    else
                        return "This cuddlefish doll was created from observations made on 4546B.";
                case "ReactorLampName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Lampe";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Lámpara";
                    else
                        return "Lamp";
                case "ReactorLampDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une lampe customisable.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una lámpara personalizable.";
                    else
                        return "A customizable lamp.";
                case "SeamothDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de seamoth";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Muñeca seamoth";
                    else
                        return "Seamoth doll";
                case "SeamothDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une miniature décorative du seamoth.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una miniatura decorativa de seamoth.";
                    else
                        return "A decorative miniature of the seamoth.";
                case "ExosuitDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Poupée de combinaison PRAWN";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Muñeca traje PRAWN";
                    else
                        return "PRAWN suit doll";
                case "ExosuitDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une miniature décorative de la combinaison PRAWN.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una miniatura decorativa de traje PRAWN.";
                    else
                        return "A decorative miniature of the PRAWN suit.";
                case "ForkLiftDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Chariot élévateur (non-fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Carretilla elevadora (no funcionales)";
                    else
                        return "Forklift (non-functional)";
                case "ForkLiftDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un chariot élévateur décoratif.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una carretilla elevadora decorativa.";
                    else
                        return "A decorative forklift.";
                case "DrinksAndFood":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Boissons & nourriture";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Bebidas & comida";
                    else
                        return "Drinks & food";
                case "BarBottleName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Bouteille de bar";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Botella de bar";
                    else
                        return "Bar bottle";
                case "BarBottleDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une bouteille contenant un délicieux breuvage.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una botella que contiene una deliciosa bebida.";
                    else
                        return "A bottle containing a delicious beverage.";
                case "BarCup2Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Gobelet";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Taza";
                    else
                        return "Cup";
                case "BarCup2Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un gobelet fait de titane.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una taza hecha de titanio.";
                    else
                        return "A cup made of titanium.";
                case "BarCup1Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Petit gobelet";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Pequeña taza";
                    else
                        return "Small cup";
                case "BarCup1Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un petit gobelet fait de titane.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una pequeña taza hecha de titanio.";
                    else
                        return "A small cup made of titanium.";
                case "BarFood1Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Petit plat";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Plato pequeño";
                    else
                        return "Small meal";
                case "BarFood1Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un plat à base de poisson.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un plato hecho de pescado.";
                    else
                        return "A meal made of fish.";
                case "BarFood2Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Plateau repas";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Bandeja de comida";
                    else
                        return "Meal tray";
                case "BarFood2Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un repas complet et équilibré.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una comida completa y equilibrada.";
                    else
                        return "A complete and balanced meal.";
                case "BarNapkinsName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Serviettes de table";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Servilletas";
                    else
                        return "Napkins";
                case "BarNapkinsDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Des serviettes de table en maille de fibre.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Servilletas en malla de fibra.";
                    else
                        return "Napkins made of fiber mesh.";
                case "LabRobotArmName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Bras robot (non-fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Brazo robótico (no funcional)";
                    else
                        return "Robot arm (non-functional)";
                case "LabRobotArmDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un bras robot de laboratoire (non-fonctionnel).";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un brazo robótico de laboratorio (no funcional).";
                    else
                        return "A laboratory robot arm (non-functional).";
                case "ReaperSkullDollName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Crâne de faucheur léviathan";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Cráneo de segador leviatán";
                    else
                        return "Reaper leviathan skull";
                case "ReaperSkullDollDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une réplique de crâne d'un faucheur léviathan.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una réplica del cráneo de un segador leviatán.";
                    else
                        return "A replica of a reaper leviathan skull.";
                case "LampTooltip":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cliquez pour ajuster la portée, ou :" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour changer la couleur du néon" + Environment.NewLine +
                               "Maintenez 'I' et cliquez pour changer l'intensité" + Environment.NewLine +
                               "Maintenez 'R' et cliquez pour changer le niveau de rouge" + Environment.NewLine +
                               "Maintenez 'G' et cliquez pour changer le niveau de vert" + Environment.NewLine +
                               "Maintenez 'B' et cliquez pour changer le niveau de bleu" + Environment.NewLine;
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Haga clic para ajustar el alcance, o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar el color del neón" + Environment.NewLine +
                               "Mantenga 'I' y haga clic para cambiar la intensidad" + Environment.NewLine +
                               "Mantenga 'R' y haga clic para cambiar los niveles rojos" + Environment.NewLine +
                               "Mantenga 'G' y haga clic para cambiar los niveles verdes" + Environment.NewLine +
                               "Mantenga 'B' y haga clic para cambiar los niveles azules" + Environment.NewLine;
                    else
                        return "Click to adjust light range, or:" + Environment.NewLine +
                               "Hold 'E' and click to change neon tube color" + Environment.NewLine +
                               "Hold 'I' and click to change intensity" + Environment.NewLine +
                               "Hold 'R' and click to change red levels" + Environment.NewLine +
                               "Hold 'G' and click to change green levels" + Environment.NewLine +
                               "Hold 'B' and click to change blue levels" + Environment.NewLine;
                case "SwitchSeamothModel":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Changer le modèle";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Cambiar el modelo";
                    else
                        return "Switch model";
                case "SwitchExosuitModel":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cliquez pour changer le modèle de bras gauche, ou:" + Environment.NewLine +
                               "Maintenez 'E' et cliquez pour change le modèle de bras droit" + Environment.NewLine;
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Haga clic para cambiar el modelo de brazo izquierdo, o:" + Environment.NewLine +
                               "Mantenga 'E' y haga clic para cambiar el modelo de brazo derecho" + Environment.NewLine;
                    else
                        return "Click to change left arm model, or:" + Environment.NewLine +
                               "Hold 'E' and click to change right arm model" + Environment.NewLine;
                case "AdjustItemSize":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Cliquez pour modifier la taille";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Haga clic para cambiar el tamaño";
                    else
                        return "Click to adjust size";
                case "CargoBox1aName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Caisse de chargement renforcée";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Caja de carga reforzada";
                    else
                        return "Reinforced cargo crate";
                case "CargoBox1aDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une caisse de chargement renforcée permettant le transport des marchandises.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una caja de carga reforzada que permite el transporte de mercancías.";
                    else
                        return "A reinforced cargo crate made for the transport of goods.";
                case "CargoBox1bName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Caisse de chargement";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Caja de carga";
                    else
                        return "Cargo crate";
                case "CargoBox1bDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une caisse de chargement permettant le transport des marchandises.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una caja de carga que permite el transporte de mercancías.";
                    else
                        return "A cargo crate made for the transport of goods.";
                case "CargoBox1DmgName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Caisse de chargement endommagée";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Caja de carga dañada";
                    else
                        return "Damaged cargo crate";
                case "CargoBox1DmgDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Une caisse de chargement en piteux état.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una caja de carga en mal estado.";
                    else
                        return "An unusable damaged cargo crate.";
                case "Folder1Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Documents";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Documentos";
                    else
                        return "Documents";
                case "Folder1Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un dossier contenant divers documents.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Una carpeta que contiene varios documentos.";
                    else
                        return "A folder containing various documents.";
                case "ClipboardName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Presse-papiers";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Portapapeles";
                    else
                        return "Clipboard";
                case "ClipboardDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un presse-papiers contenant divers documents.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un portapapeles que contiene varios documentos.";
                    else
                        return "A clipboard containing various documents.";
                case "PenName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Stylo";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Lápice";
                    else
                        return "Pen";
                case "PenDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un stylo Alterra.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un lápice Alterra.";
                    else
                        return "An Alterra pen.";
                case "PenHolderName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Porte-stylo";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Portalápices";
                    else
                        return "Pen holder";
                case "PenHolderDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un porte-stylo Alterra.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un portalápices Alterra.";
                    else
                        return "An Alterra pen holder.";
                case "PaperTrashName":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Papiers froissés";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Papeles arrugados";
                    else
                        return "Crumpled papers";
                case "PaperTrashDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Des documents inutiles.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Documentos innecesarios";
                    else
                        return "Unnecessary documents.";
                case "SofaStr1Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Petit banc (décoratif et fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Pequeño banco (decorativo y funcional)";
                    else
                        return "Small bench (decorative and functional)";
                case "SofaStr1Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un petit banc : Esthétique et pratique pour se reposer.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un pequeño banco: Estético y práctico para un descanso.";
                    else
                        return "A small bench: Aesthetic and practical to rest.";
                case "SofaStr2Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Banc moyen (décoratif et fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Mediano banco (decorativo y funcional)";
                    else
                        return "Medium bench (decorative and functional)";
                case "SofaStr2Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un banc moyen : Esthétique et pratique pour se reposer.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un mediano banco: Estético y práctico para un descanso.";
                    else
                        return "A medium bench: Aesthetic and practical to rest.";
                case "SofaStr3Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Banc large (décoratif et fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Banco grande (decorativo y funcional)";
                    else
                        return "Long bench (decorative and functional)";
                case "SofaStr3Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un banc large : Esthétique et pratique pour se reposer.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un grande banco: Estético y práctico para un descanso.";
                    else
                        return "A long bench: Aesthetic and practical to rest.";
                case "SofaCorner1Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Petit angle de banc (décoratif et fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Pequeño ángulo de banco (decorativo y funcional)";
                    else
                        return "Small bench angle (decorative and functional)";
                case "SofaCorner1Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un petit angle de banc : Esthétique et pratique pour se reposer.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un pequeño ángulo de banco: Estético y práctico para un descanso.";
                    else
                        return "A small bench angle: Aesthetic and practical to rest.";
                case "SofaCorner2Name":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Angle de banc (décoratif et fonctionnel)";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Ángulo de banco (decorativo y funcional)";
                    else
                        return "Bench angle (decorative and functional)";
                case "SofaCorner2Description":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un angle de banc : Esthétique et pratique pour se reposer.";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un ángulo de banco: Estético y práctico para un descanso.";
                    else
                        return "A bench angle: Aesthetic and practical to rest.";
                default:
                    return "?";
            }
        }
    }
}
