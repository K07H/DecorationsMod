using System.Globalization;

namespace DecorationsMod.Fixers
{
    public static class AutoLoadModFixer
    {
		//public void BeginAsyncSceneLoad(string sceneName)
		public static bool BeginAsyncSceneLoad_Prefix(string sceneName)
        {
			if (string.Compare("Main", sceneName, true, CultureInfo.InvariantCulture) == 0)
			{
				KnownTechFixer.LoadAddedNotifications(null);
				ConstructableFixer.LoadLadderDirections(null);
				GenericPlaceTool_PT.LoadPlacedByPlayerList(null);
            }
			// Give back execution to origin function.
			return true;
        }
	}
}
