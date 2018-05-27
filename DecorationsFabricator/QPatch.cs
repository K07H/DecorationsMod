namespace DecorationsFabricator
{
    using UnityEngine;

    public class QPatch
    {

        public static void Patch()
        {
            // Retrieve original game objects
            DecorationsFabricatorModule.OriginalToyCarObject = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/Goldglove_car_02");
            DecorationsFabricatorModule.OriginalPosterObject = Resources.Load<GameObject>("WorldEntities/Environment/Wrecks/Poster");
            DecorationsFabricatorModule.OriginalPosterAuroraObject = Resources.Load<GameObject>("WorldEntities/Environment/Wrecks/poster_aurora");
            DecorationsFabricatorModule.OriginalPosterExosuit1Object = Resources.Load<GameObject>("WorldEntities/Environment/Wrecks/poster_exosuit_01");
            DecorationsFabricatorModule.OriginalPosterExosuit2Object = Resources.Load<GameObject>("WorldEntities/Environment/Wrecks/poster_exosuit_02");
            DecorationsFabricatorModule.OriginalPosterKittyObject = Resources.Load<GameObject>("WorldEntities/Environment/Wrecks/poster_kitty");
            DecorationsFabricatorModule.OriginalLabContainerObject = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_01");
            DecorationsFabricatorModule.OriginalLabContainer2Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_close_02");
            DecorationsFabricatorModule.OriginalLabContainer3Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_tube_01");
            DecorationsFabricatorModule.OriginalLabContainer4Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/biodome_lab_containers_tube_02");
            DecorationsFabricatorModule.OriginalLabEquipment1Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_01");
            DecorationsFabricatorModule.OriginalLabEquipment2Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_02");
            DecorationsFabricatorModule.OriginalLabEquipment3Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/discovery_lab_props_03");
            DecorationsFabricatorModule.OriginalCap1Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_02");
            DecorationsFabricatorModule.OriginalCap2Object = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_plaza_shelf_cap_03");
            DecorationsFabricatorModule.OriginalStarshipSouvenirObject = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/starship_souvenir");
            DecorationsFabricatorModule.OriginalArcadeGorgetoyObject = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/descent_arcade_gorgetoy_01");
            DecorationsFabricatorModule.OriginalLuggageBagObject = Resources.Load<GameObject>("WorldEntities/Doodads/Debris/Wrecks/Decoration/docking_luggage_01_bag4");

            DecorationsFabricatorModule.Patch();
        }
    }
}
