using Entidades.Clases;
using Negocio.DTOs.Requests;
using Negocio.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
    public interface IProcesadorTextoService
    {
        Task<OrdenarTextoResponse> OrdenarTexto(OrdenarTextoRequest request, CancellationToken cancellationToken);
        Task<IEnumerable<OpcionTipoOrdenamiento>> GetOpcionesOrdenamiento();
        Task<AnalizarTextoResponse> AnalizarTexto(AnalizarTextoRequest request, CancellationToken cancellationToken);

    }
}
