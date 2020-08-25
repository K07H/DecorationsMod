namespace DecorationsMod.Fixers
{
    public class IngameMenuFixer
    {
        public static void SaveGame_Postfix()
        {
            KnownTechFixer.SaveAddedNotifications();
            ConstructableFixer.SaveLadderDirections();
        }
    }
}
