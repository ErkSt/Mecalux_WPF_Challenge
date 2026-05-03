using System;
using System.Collections.Generic;
using System.Windows;
using Mecalux_WPFChallenge.Services;
using Mecalux_WPFChallenge.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Negocio.Services;
using Repositorio.Repositories;

namespace Mecalux_WPFChallenge
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App() {
            ServiceCollection services = new ServiceCollection();
            InyeccionDependenciasService.ConfigurarServicios(services);
            ServiceProvider = services.BuildServiceProvider();
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var parametrosRepository = new ParametrosRepository();
            //var procesadorTextoService = new ProcesadorTextoService(parametrosRepository);

            //var mainWindow = new MainWindow();
            //var mainViewModel = new MainViewModel(procesadorTextoService);

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
