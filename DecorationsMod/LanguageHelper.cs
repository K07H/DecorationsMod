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
                        return "brazo robótico (no funcional)";
                    else
                        return "Robot arm (non-functional)";
                case "LabRobotArmDescription":
                    if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.FR)
                        return "Un bras robot de laboratoire (non-fonctionnel).";
                    else if (LanguageHelper.UserLanguage == RegionHelper.CountryCode.ES)
                        return "Un brazo robótico de laboratorio (no funcional).";
                    else
                        return "A laboratory robot arm (non-functional).";
                default:
                    return "?";
            }
        }
    }
}
