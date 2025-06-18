using System;

namespace DecorationsMod.Fixers
{
    public class uGUI_MainMenuFixer
    {
		public static bool OnErrorConfirmed_Prefix(bool confirmed, string saveGame)
		{
			// Load added notifications and outdoor ladder directions if needed.
			if (confirmed)
			{
				KnownTechFixer.LoadAddedNotifications(saveGame);
				ConstructableFixer.LoadLadderDirections(saveGame);
                GenericPlaceTool_PT.LoadPlacedByPlayerList(saveGame);
            }
			// Give back execution to origin function.
			return true;
		}

		public static bool LoadMostRecentSavedGame_Prefix()
		{
			SaveLoadManager.GameInfo gameInfo = null;
			string slotName = null;
			// Try get game info and slot name
			try
			{
				string[] activeSlotNames = SaveLoadManager.main.GetActiveSlotNames();
				long num = 0L;
				int i = 0;
				int num2 = activeSlotNames.Length;
				while (i < num2)
				{
					SaveLoadManager.GameInfo gameInfo2 = SaveLoadManager.main.GetGameInfo(activeSlotNames[i]);
					if (gameInfo2.dateTicks > num)
					{
						gameInfo = gameInfo2;
						num = gameInfo2.dateTicks;
						slotName = activeSlotNames[i];
					}
					i++;
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("WARNING: Exception caught while retrieving game info. Exception=[" + ex.ToString() + "]");
				gameInfo = null;
			}
			// Load added notifications and outdoor ladder directions if needed.
			if (gameInfo != null)
			{
				KnownTechFixer.LoadAddedNotifications(slotName);
				ConstructableFixer.LoadLadderDirections(slotName);
                GenericPlaceTool_PT.LoadPlacedByPlayerList(slotName);
            }
			// Give back execution to origin function.
			return true;
		}
	}
}
