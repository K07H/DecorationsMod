using Harmony;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    [HarmonyPatch(typeof(Pickupable))]
    [HarmonyPatch("DisableColliders")]
    public class LayerFixer
    {
        internal static readonly string[] _customItems = {
            "BarBottle1",
            "BarBottle2",
            "BarBottle3",
            "BarBottle4",
            "BarBottle5",
            "BarCup1",
            "BarCup2",
            "BarFood1",
            "BarFood2",
            "BarNapkins",
            "benzene",
            "biodome_lab_containers_close_01",
            "biodome_lab_containers_close_02",
            "biodome_lab_containers_tube_01",
            "CircuitBox2",
            "CircuitBox2b",
            "CircuitBox2c",
            "CircuitBox2d",
            "CircuitBox3",
            "CircuitBox3b",
            "CircuitBox3c",
            "CircuitBox3d",
            "Clipboard",
            "Coffee",
            "CuddleFishDoll",
            "DecorationLabTube",
            "descent_arcade_gorgetoy_01",
            "descent_plaza_shelf_cap_02",
            "descent_plaza_shelf_cap_03",
            "discovery_lab_props_01",
            "discovery_lab_props_02",
            "discovery_lab_props_03",
            "docking_luggage_01_bag4",
            "Folder1",
            "Folder3",
            "GhostLeviathanDoll",
            "Goldglove_car_02",
            "hydrochloricacid",
            "LabCart",
            "LabCart",
            "LabContainer4",
            "LabRobotArm",
            "LabShelf",
            "PaperTrash",
            "Pen",
            "PenHolder",
            "polyaniline",
            "Poster",
            "poster_aurora",
            "poster_exosuit_01",
            "poster_exosuit_02",
            "poster_kitty",
            "PrecursorIonCrystal",
            "PrecursorKey_Purple",
            "PrecursorKey_Blue",
            "PrecursorKey_Orange",
            "PrecursorKey_Red",
            "PrecursorKey_White",
            "ReaperLeviathanDoll",
            "ReaperSkullDoll",
            "ReaperSkullDoll",
            "ReefBackDoll",
            "SeaDragonDoll",
            "SeaEmperorDoll",
            "SeaTreaderDoll",
            "starship_souvenir"
        };

        public static void DisableColliders_Prefix(Pickupable __instance)
        {
            // Search for colliders having wrong layer ID
            foreach (Collider collider in __instance.GetComponentsInChildren<Collider>())
            {
                if (collider.gameObject.layer != LayerID.Default)
                {
                    foreach (string customItem in _customItems)
                    {
                        // Check if this item belongs to the Decorations Mod
                        if (__instance.name.StartsWith(customItem))
                        {
                            // Fix layout
                            collider.gameObject.layer = LayerID.Default;
                            break;
                        }
                    }
                }
            }
        }
    }
}
