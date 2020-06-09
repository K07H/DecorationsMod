using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DecorationsModConfigurator
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");
            DispatcherUnhandledException += (s, e) =>
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
            TaskScheduler.UnobservedTaskException += (s, e) =>
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            string message = "Unhandled exception (" + (!string.IsNullOrEmpty(source) ? source : "") + ")";
            try
            {
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
            }
            catch (Exception ex)
            {
                Logger.Log("Exception in LogUnhandledException: Ex=[" + ex.ToString() + "]");
            }
            finally
            {
                Logger.Log("Unhandled exception: Msg=[" + message.ToString() + "] Ex=[" + exception.ToString() + "]");
            }
        }
    }
}
