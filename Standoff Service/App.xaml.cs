using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Serilog;
using Serilog.Events;

namespace Standoff_Service
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string currentDateTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"log_{currentDateTime}.txt")
                .CreateLogger();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                Log.Fatal((Exception)ex.ExceptionObject, "Необработанное исключение");
            };

            DispatcherUnhandledException += (s, ex) =>
            {
                Log.Fatal(ex.Exception, "Необработанное исключение в UI потоке");
            };

            Log.Information("Приложение запущено");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("Приложение завершает работу");

            base.OnExit(e);

            Log.CloseAndFlush();
        }
    }
}
