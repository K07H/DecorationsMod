using Harmony;
using System;
using System.Collections.Generic;

namespace DecorationsMod.Fixers
{
    [HarmonyPatch(typeof(Inventory))]
    [HarmonyPatch("CanDropItemHere")]
    public class InventoryFixer
    {
        internal static readonly List<string> _plants = new List<string>(new string [46] {
            "Fern2",
            "Fern4",
            "JungleTree1",
            "JungleTree2",
            "LandPlant1",
            "LandPlant2",
            "LandPlant3",
            "LandPlant4",
            "LandPlant5",
            "LandTree1",
            "TropicalPlant1a",
            "TropicalPlant1b",
            "TropicalPlant2a",
            "TropicalPlant2b",
            "TropicalPlant3a",
            "TropicalPlant3b",
            "TropicalPlant6a",
            "TropicalPlant6b",
            "TropicalPlant7a",
            "TropicalPlant7b",
            "TropicalPlant10a",
            "TropicalPlant10b",
            "CoveTree1",
            "CrabClawKelp1",
            "CrabClawKelp2",
            "CrabClawKelp3",
            "FloatingStone1",
            "GreenReeds1",
            "GreenReeds6",
            "LostRiverPlant2",
            "LostRiverPlant4",
            "PlantMiddle11",
            "PyroCoral1",
            "PyroCoral2",
            "PyroCoral3",
            "BlueCoralTubes1",
            "BrownCoralTubes1",
            "BrownCoralTubes2",
            "BrownCoralTubes3",
            "SmallDeco3",
            "SmallDeco10",
            "SmallDeco11",
            "SmallDeco13",
            "SmallDeco14",
            "SmallDeco15Red",
            "SmallDeco17Purple"
        });
        
        //public static bool CanDropItemHere(Pickupable item, bool notify = false)
        public static bool CanDropItemHere_Prefix(bool __result, Pickupable item, bool notify = false)
        {
            bool isPlant = false;

#if DEBUG_DROP_ITEM
            Logger.Log("DEBUG: Entering CanDropItemHere_Prefix() for item name=[" + item.gameObject.name + "]");
#endif

            // Check if current item is a plant
            Plantable plant = item.gameObject.GetComponent<Plantable>();
            if (plant != null)
                isPlant = true;
            else
            {
#if DEBUG_DROP_ITEM
                Logger.Log("DEBUG: A) Cannot find plant");
#endif
                GrownPlant grownPlant = item.gameObject.GetComponent<GrownPlant>();
                if (grownPlant != null)
                    isPlant = true;
#if DEBUG_DROP_ITEM
                else
                    Logger.Log("DEBUG: B) Cannot find grownplant");
#endif
            }

            if (isPlant)
            {
                // Check if current plant is one of our custom plants
                PrefabIdentifier id = item.gameObject.GetComponent<PrefabIdentifier>();
                if (id != null)
                {
                    if (!String.IsNullOrEmpty(id.ClassId) && _plants.Contains(id.ClassId))
                    {
                        // If yes, set result value to false (cannot drop item) and return false to
                        // prevent original function from being called
                        __result = false;
                        return false;
                    }
#if DEBUG_DROP_ITEM
                    else
                        Logger.Log("DEBUG: E) Cannot find class ID");
                }
                else
                    Logger.Log("DEBUG: D) Cannot find prefab ID");
            }
            else
                Logger.Log("DEBUG: C) Item is not a plant");
#else
                }
            }
#endif

            // Return true to call original function
            return true;
        }
    }
}
