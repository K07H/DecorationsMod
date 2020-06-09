namespace DecorationsMod.Fixers
{
	public class MainMenuLoadButtonFixer
    {
		public static bool Load_Prefix(MainMenuLoadButton __instance)
		{
			// Load added notifications if needed.
			if (!__instance.IsEmpty())
				KnownTechFixer.LoadAddedNotifications(__instance.saveGame);
			// Give back execution to original function.
			return true;
		}
	}
}
