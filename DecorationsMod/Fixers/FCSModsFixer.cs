using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecorationsMod.Fixers
{
    public class FCSModsFixer
    {
		// Fix for Deep Driller
		public static bool IsAllowedToAdd_Driller_Prefix(ref bool __result, Pickupable pickupable, bool verbose)
		{
			__result = false;
			TechType techType = pickupable.GetTechType();
			if (techType == TechType.PowerCell || techType == TechType.PrecursorIonPowerCell)
				__result = true;
#if SUBNAUTICA
			else if (CraftData.GetEquipmentType(techType) == EquipmentType.PowerCellCharger)
#elif BELOWZERO
			else if (TechData.GetEquipmentType(techType) == EquipmentType.PowerCellCharger)
#endif
				__result = true;
			else
				ErrorMessage.AddMessage(Language.main.Get("DD_OnlyPowercellsAllowed"));
			return false;
		}

		// Fix for PowerCell Socket
		public static bool IsAllowedToAdd_Powercell_Prefix(ref bool __result, Pickupable pickupable, bool verbose)
		{
			__result = false;
			TechType techType = pickupable.GetTechType();
			if (techType == TechType.PowerCell || techType == TechType.PrecursorIonPowerCell)
				__result = true;
#if SUBNAUTICA
			else if (CraftData.GetEquipmentType(techType) == EquipmentType.PowerCellCharger)
#elif BELOWZERO
			else if (TechData.GetEquipmentType(techType) == EquipmentType.PowerCellCharger)
#endif
				__result = true;
			else
				ErrorMessage.AddMessage(Language.main.Get("DD_OnlyPowercellsAllowed"));
			return false;
		}
	}
}
