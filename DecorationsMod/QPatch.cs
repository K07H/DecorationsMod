using System;

namespace DecorationsMod
{
    public class QPatch
    {
        public static void Patch()
        {
            Logger.Log("Initializing Decorations mod.", null);
            try
            {
                DecorationsFabricatorModule.Patch();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught" + (!String.IsNullOrEmpty(e.Message) ? " Message=[" + e.Message + "]" : ""));
                Console.WriteLine("StackTrace=[" + e.StackTrace + "]");
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception caught" + ((!String.IsNullOrEmpty(e.InnerException.Message)) ? " Message=[" + e.InnerException.Message + "]" : ""));
                    Console.WriteLine("Inner stackTrace=[" + e.InnerException.StackTrace + "]");
                }
            }
            Logger.Log("Decorations mod initialized successfully.", null);
        }
    }
}
