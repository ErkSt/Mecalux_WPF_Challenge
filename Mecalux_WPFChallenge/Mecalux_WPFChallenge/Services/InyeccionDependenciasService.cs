using Entidades.Interfaces;
using Mecalux_WPFChallenge.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Negocio.Interfaces;
using Negocio.Services;
using Repositorio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecalux_WPFChallenge.Services
{
    public static class InyeccionDependenciasService
    {
        internal static void ConfigurarServicios(ServiceCollection services)
        {
            services.AddSingleton<IParametrosRepository, ParametrosRepositoryEnMemoria>();
            services.AddSingleton<IProcesadorTextoService, ProcesadorTextoService>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();
        }
    }
}
