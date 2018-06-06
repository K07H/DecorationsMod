namespace DecorationsMod
{
    public class QPatch
    {
        public static void Patch()
        {
            Logger.Log("Initializing Decorations mod.", null);
            DecorationsFabricatorModule.Patch();
            Logger.Log("Decorations mod initialized successfully.", null);
        }
    }
}
