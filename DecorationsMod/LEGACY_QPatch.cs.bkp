using System;

namespace DecorationsMod
{
    public class QPatch
    {
        private static bool _success = true;

        public static void Patch()
        {
            Logger.Log("INFO: Initializing Decorations mod...");
            try { DecorationsMod.Patch(); }
            catch (Exception e)
            {
                _success = false;
                Logger.Log(string.Format("ERROR: Exception caught! Message=[{0}] StackTrace=[{1}]", e.Message, e.StackTrace));
                if (e.InnerException != null)
                    Logger.Log(string.Format("ERROR: Inner exception => Message=[{0}] StackTrace=[{1}]", e.InnerException.Message, e.InnerException.StackTrace));
            }
            if (_success)
                Logger.Log("INFO: Decorations mod initialized successfully.");
            else
                Logger.Log("ERROR: Decorations mod initialization failed.");
        }
    }
}
