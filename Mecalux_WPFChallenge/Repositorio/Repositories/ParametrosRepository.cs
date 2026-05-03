using Entidades.Clases;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio.Repositories
{
    public class ParametrosRepository : IParametrosRepository
    {
        public Task<IEnumerable<OpcionTipoOrdenamiento>> GetAllOpcionesOrdenamiento()
        {
            throw new NotImplementedException();
        }
    }
}
