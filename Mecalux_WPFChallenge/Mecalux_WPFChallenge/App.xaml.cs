using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Mecalux_WPFChallenge.ViewModels;
using Negocio.Services;
using Repositorio.Repositories;

namespace Mecalux_WPFChallenge
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var parametrosRepository = new ParametrosRepository();
            var procesadorTextoService = new ProcesadorTextoService(parametrosRepository);

            var mainWindow = new MainWindow();
            var mainViewModel = new MainViewModel(procesadorTextoService);

            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }
    }
}
