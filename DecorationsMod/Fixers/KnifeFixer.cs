using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class KnifeFixer
    {
        public static TechType purplePineConeTechType = TechType.None;

        //private void GiveResourceOnDamage(GameObject target, bool isAlive, bool wasAlive)
        public static void GiveResourceOnDamage_Postfix(GameObject target, bool isAlive, bool wasAlive)
        {
            TechType techType = CraftData.GetTechType(target);
            if ((int)techType == (int)purplePineConeTechType && purplePineConeTechType != TechType.None) // If it's our custom purple pinecone
            {
#if DEBUG_KNIFE
                Logger.Debug("Entering custom purple pinecone event. techType=[" + (int)techType + "][" + techType.AsString(false) + "] purplePineConeTechType=[" + (int)purplePineConeTechType + "][" + purplePineConeTechType.AsString(false) + "]");
#endif
#if SUBNAUTICA
                HarvestType harvestTypeFromTech = CraftData.GetHarvestTypeFromTech(techType);
#else
                HarvestType harvestTypeFromTech = TechData.GetHarvestType(techType);
#endif
                if ((harvestTypeFromTech == HarvestType.DamageAlive && wasAlive) || (harvestTypeFromTech == HarvestType.DamageDead && !isAlive))
                    CraftData.AddToInventory(ConfigSwitcher.PurplePineconeDroppedResource, ConfigSwitcher.PurplePineconeDroppedResourceAmount, false, false); // This will add PurplePineconeDroppedResourceAmount of PurplePineconeDroppedResource in player's inventory
            }
        }
    }
}
