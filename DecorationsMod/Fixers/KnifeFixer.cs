using Harmony;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    [HarmonyPatch(typeof(Knife))]
    [HarmonyPatch("GiveResourceOnDamage")]
    public class KnifeFixer
    {
        public static TechType purplePineConeTechType = TechType.None;

        //private void GiveResourceOnDamage(GameObject target, bool isAlive, bool wasAlive)
        public static void GiveResourceOnDamage_Postfix(GameObject target, bool isAlive, bool wasAlive)
        {
            TechType techType = CraftData.GetTechType(target);
            if ((int)techType == (int)purplePineConeTechType) // If it's our custom purple pinecone
            {
#if DEBUG_KNIFE
                Logger.Log("DEBUG: Entering custom purple pinecone event. techType=[" + (int)techType + "][" + techType.AsString(false) + "] purplePineConeTechType=[" + (int)purplePineConeTechType + "][" + purplePineConeTechType.AsString(false) + "]");
#endif
                HarvestType harvestTypeFromTech = CraftData.GetHarvestTypeFromTech(techType);
                if ((harvestTypeFromTech == HarvestType.DamageAlive && wasAlive) || (harvestTypeFromTech == HarvestType.DamageDead && !isAlive))
                    CraftData.AddToInventory(TechType.Salt, 1, false, false); // Add one salt in player's inventory
            }
        }
    }
}
