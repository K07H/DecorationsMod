using BepInEx;
using System;

namespace DecorationsMod
{
    [BepInPlugin("com.osubmarin.decorationsmod", "DecorationsMod", "2.1.0")]
    [BepInDependency("com.snmodding.nautilus")]
    public class DecorationsMod_EntryPoint : BaseUnityPlugin
    {
        private static bool _initialized = false;
        private static bool _success = true;

        public static BepInEx.Logging.ManualLogSource _logger = null;

        private void Awake()
        {
            _logger = Logger;
            if (!_initialized)
            {
                _initialized = true;
                Logger.LogInfo("INFO: Initializing Decorations mod...");
                try { DecorationsMod.Patch(); }
                catch (Exception e)
                {
                    _success = false;
                    Logger.LogError(string.Format("ERROR: Exception caught! Message=[{0}] StackTrace=[{1}]", e.Message, e.StackTrace));
                    if (e.InnerException != null)
                        Logger.LogError(string.Format("ERROR: Inner exception => Message=[{0}] StackTrace=[{1}]", e.InnerException.Message, e.InnerException.StackTrace));
                }
                if (_success)
                    Logger.LogInfo("INFO: Decorations mod initialized successfully.");
                else
                    Logger.LogError("ERROR: Decorations mod initialization failed.");
            }
        }
    }
}
