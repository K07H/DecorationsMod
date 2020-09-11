namespace DecorationsMod.Fixers
{
	public class MainMenuLoadButtonFixer
    {
		public static bool Load_Prefix(MainMenuLoadButton __instance)
		{
			// Load added notifications and outdoor ladder directions if needed.
			if (!__instance.IsEmpty())
			{
				KnownTechFixer.LoadAddedNotifications(__instance.saveGame);
				ConstructableFixer.LoadLadderDirections(__instance.saveGame);
			}
			// Give back execution to origin function.
			return true;
		}
	}
}
