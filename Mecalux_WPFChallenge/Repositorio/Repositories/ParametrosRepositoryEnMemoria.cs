using Entidades.Clases;
using Entidades.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositories
{
    public class ParametrosRepositoryEnMemoria : IParametrosRepository
    {
        public Task<IEnumerable<OpcionTipoOrdenamiento>> GetAllOpcionesOrdenamiento()
        {
            var opciones = new List<OpcionTipoOrdenamiento>
            {
                new OpcionTipoOrdenamiento { Tipo = TipoOrdenamiento.AlfabeticoAscentente, Descripcion = "Alfabetico Ascendente" },
                new OpcionTipoOrdenamiento { Tipo = TipoOrdenamiento.AlfabeticoDescendente, Descripcion = "Alfabetico Descendente" },
                new OpcionTipoOrdenamiento { Tipo = TipoOrdenamiento.Longitud, Descripcion = "Por Longitud" },
            };

            return Task.FromResult<IEnumerable<OpcionTipoOrdenamiento>>(opciones);
        }
    }
}
