namespace DecorationsMod.Fixers
{
    public static class IngameMenuQuitConfirmationFixer
    {
        //public void OnYes()
        public static bool OnYes_Prefix()
        {
            if (GameModeUtils.IsPermadeath())
            {
                KnownTechFixer.SaveAddedNotifications();
                ConstructableFixer.SaveLadderDirections();
            }
            return true; // Give back execution to origin function.
        }
    }
}
