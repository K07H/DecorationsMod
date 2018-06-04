namespace DecorationsFabricator
{
    public class QPatch
    {
        public static void Patch()
        {
            Logger.Log("Initializing Decorations Fabricator mod.", null);
            DecorationsFabricatorModule.Patch();
            Logger.Log("Decorations Fabricator mod initialized successfully.", null);
        }
    }
}
