using System;
using System.IO;
using System.Windows;
using Serilog;

namespace Standoff_Service
{
    public partial class App : Application
    {
        public App()
        {
            string currentDateTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nuclear Materials", "Logs");
            string logFilePath = Path.Combine(logDirectory, $"log_{currentDateTime}.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath)
                .CreateLogger();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                Log.Fatal((Exception)ex.ExceptionObject, "Unhandled exception");
            };

            DispatcherUnhandledException += (s, ex) =>
            {
                Log.Fatal(ex.Exception, "Unhandled exception in UI thread");
            };

            Log.Information("The application is running");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("The application quits");

            base.OnExit(e);

            Log.CloseAndFlush();
        }
    }
}
