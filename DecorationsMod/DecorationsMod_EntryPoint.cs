using BepInEx;
using System;

namespace DecorationsMod
{
    [BepInPlugin("com.osubmarin.decorationsmod", "DecorationsMod", "2.0.3")]
#if SUBNAUTICA_NAUTILUS
    [BepInDependency("com.snmodding.nautilus")]
#else
    [BepInDependency("com.ahk1221.smlhelper")]
#endif
    [UnityEngine.DisallowMultipleComponent]
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
                    Logger.LogInfo(string.Format("ERROR: Exception caught! Message=[{0}] StackTrace=[{1}]", e.Message, e.StackTrace));
                    if (e.InnerException != null)
                        Logger.LogInfo(string.Format("ERROR: Inner exception => Message=[{0}] StackTrace=[{1}]", e.InnerException.Message, e.InnerException.StackTrace));
                }
                Logger.LogInfo(_success ? "INFO: Decorations mod initialized successfully." : "ERROR: Decorations mod initialization failed.");
            }
        }
    }
    /*
    public class QPatch
    {
        private static bool _success = true;

        public static void Patch()
        {
            Logger.Info("Initializing Decorations mod...");
            try { DecorationsMod.Patch(); }
            catch (Exception e)
            {
                _success = false;
                Logger.Log(string.Format("ERROR: Exception caught! Message=[{0}] StackTrace=[{1}]", e.Message, e.StackTrace));
                if (e.InnerException != null)
                    Logger.Log(string.Format("ERROR: Inner exception => Message=[{0}] StackTrace=[{1}]", e.InnerException.Message, e.InnerException.StackTrace));
            }
            if (_success)
                Logger.Info("Decorations mod initialized successfully.");
            else
                Logger.Error("Decorations mod initialization failed.");
        }
    }
    */
}
