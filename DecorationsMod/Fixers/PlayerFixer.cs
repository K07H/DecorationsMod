using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class PlayerFixer
    {
        private static bool lastBiomeWasDeepGrandReef = false;
        private static float lastTimeCheck = Time.time;

        public static void CalculateBiome_Postfix(ref string __result)
        {
            if (__result != null && __result.Contains("deepGrandReef"))
            {
                if (!lastBiomeWasDeepGrandReef || (Time.time > lastTimeCheck + 20.0f))
                {
                    lastTimeCheck = Time.time;
                    lastBiomeWasDeepGrandReef = true;
                    ConfigOptions.HideDegasiBase();
                }
            }
            else
                lastBiomeWasDeepGrandReef = false;
        }
    }
}
