namespace DecorationsMod.Fixers
{
    public class IngameMenuFixer
    {
        public static void SaveGame_Postfix()
        {
            KnownTechFixer.SaveAddedNotifications();
            ConstructableFixer.SaveLadderDirections();
        }

        public static void QuitGame_Postfix(bool quitToDesktop)
        {
            if (GameModeUtils.IsPermadeath())
            {
                KnownTechFixer.SaveAddedNotifications();
                ConstructableFixer.SaveLadderDirections();
            }
        }
    }
}
