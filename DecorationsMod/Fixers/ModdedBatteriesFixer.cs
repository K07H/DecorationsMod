using System.Collections.Generic;
using System.Reflection;

namespace DecorationsMod.Fixers
{
    internal static class ModdedBatteriesFixer
    {
        //private static readonly HashSet<TechType> compatibleTech
        private static readonly FieldInfo _powercellsCompatibleTech = typeof(PowerCellCharger).GetField("compatibleTech", BindingFlags.NonPublic | BindingFlags.Static);
        //private static readonly HashSet<TechType> compatibleTech
        private static readonly FieldInfo _batteriesCompatibleTech = typeof(BatteryCharger).GetField("compatibleTech", BindingFlags.NonPublic | BindingFlags.Static);

        internal static HashSet<TechType> PowercellsTechTypes() => (HashSet<TechType>)_powercellsCompatibleTech.GetValue(null);
        internal static HashSet<TechType> BatteriesTechTypes() => (HashSet<TechType>)_batteriesCompatibleTech.GetValue(null);
        internal static bool Chargeable(TechType techType) => PowercellsTechTypes().Contains(techType) || BatteriesTechTypes().Contains(techType);
    }
}
