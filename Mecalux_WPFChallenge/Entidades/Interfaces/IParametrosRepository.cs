using Entidades.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IParametrosRepository
    {
        Task<IEnumerable<OpcionTipoOrdenamiento>> GetAllOpcionesOrdenamiento();
    }
}
