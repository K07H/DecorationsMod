using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine.UI;

namespace DecorationsMod.Fixers
{
    public class uGUI_CraftNodeFixer
    {
        private static readonly FieldInfo _view = typeof(uGUI_CraftNode).GetField("view", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void CreateIcon_Postfix(uGUI_CraftNode __instance)
        {
            if (_view != null && __instance.action == TreeAction.Expand)
            {
                var cm = (uGUI_CraftingMenu)_view.GetValue(__instance);
                if (cm != null && __instance.icon != null)
                {
                    // If current node belongs to one of our custom fabricators
                    if (cm.id == "DecorationsFabricator" && DecorationNodes.Contains(__instance.id))
                        __instance.icon.SetBackgroundColors(DNormal, DHover, DPressed);
                    else if (cm.id == "FloraFabricator" && FloraNodes.Contains(__instance.id))
                        __instance.icon.SetBackgroundColors(FNormal, FHover, FPressed);
                }
            }
        }

        // Decorations fabricator colors
        private static readonly UnityEngine.Color DNormal = new UnityEngine.Color(0.76863f, 0.23137f, 0.36863f);
        private static readonly UnityEngine.Color DHover = new UnityEngine.Color(0.60784f, 0.18039f, 0.60392f);
        private static readonly UnityEngine.Color DPressed = new UnityEngine.Color(0.76863f, 0.23137f, 0.43137f);

        // Flora fabricator colors
        private static readonly UnityEngine.Color FNormal = new UnityEngine.Color(0f, 0.56863f, 0.23922f);
        private static readonly UnityEngine.Color FHover = new UnityEngine.Color(0.2f, 0.70196f, 0f);
        private static readonly UnityEngine.Color FPressed = new UnityEngine.Color(0.28235f, 1f, 0f);

        /// <summary>Contains crafting node IDs of the decorations fabricator.</summary>
        private static List<string> DecorationNodes = new List<string>(new string[26]
        {
            "LabElements",
            "Electronics",
            "DrinksAndFood",
            "Precursor",
            "EggsTab",
            "LeviathansTab",
            "OfficeSupplies",
            "ToysAndAccessories",
            "NonFunctionalAnalyzers",
            "OpenedGlassContainers",
            "GlassContainers",
            "LabFurnitures",
            "WallMonitors",
            "CircuitBoxes",
            "SeamothFragments",
            "PrecursorWarperParts",
            "PrecursorWeapons",
            "PrecursorRelics",
            "PrecursorKeys",
            "DmgCreatureEggsTab",
            "NonDmgCreatureEggsTab",
            "LeviathanDolls",
            "SkeletonsParts",
            "Toys",
            "Posters",
            "Accessories"
        });

        /// <summary>Contains crafting node IDs of the flora fabricator.</summary>
        private static List<string> FloraNodes = new List<string>(new string[19]
        {
            "AirSeedsTab", // ConfigSwitcher.UseFlatScreenResolution
            "WaterSeedsTab", // ConfigSwitcher.UseFlatScreenResolution
            "PlantAirTab",
            "TreeAirTab",
            "TropicalPlantTab",
            "RegularAirSeedsTab",
            "RegularWaterSeedsTab",
            "PlantWaterTab",
            "TreeWaterTab",
            "AmphibiousPlantsTab",
            "CoralWaterTab",
            "EdibleRegularAirTab",
            "DecorativeBigAirTab",
            "DecorativeSmallAirTab",
            "DecorativeBushesWaterTab",
            "RegularSmallWaterTab",
            "DecorativeBigWaterTab",
            "FunctionalBigWaterTab",
            "RedGrassesTab"
        });
    }
}
