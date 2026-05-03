using Entidades.Clases;
using Entidades.Enums;
using Entidades.Interfaces;
using Negocio.DTOs.Requests;
using Negocio.DTOs.Responses;
using Negocio.Helpers;
using Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public class ProcesadorTextoService : IProcesadorTextoService
    {
        private readonly IParametrosRepository _parametros;
        public ProcesadorTextoService(IParametrosRepository parametros)
        {
            _parametros = parametros;
        }

        public async Task<AnalizarTextoResponse> AnalizarTexto(AnalizarTextoRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Texto))
            {
                return new AnalizarTextoResponse
                {
                    CantidadGuiones = 0,
                    CantidadPalabras = 0,
                    CantidadEspaciosEnBlanco = 0
                }; ;
            }

            return await Task.Run(() =>
            {
                int guiones = 0;
                int palabras = 0;
                int espaciosEnBlanco = 0;
                bool flagPalabra = false;

                for (int i = 0; i < request.Texto.Length; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (char.IsWhiteSpace(request.Texto[i]))
                    {
                        espaciosEnBlanco++;
                        if (flagPalabra) {
                            palabras++;
                        }

                        flagPalabra = false;
                    }
                    else {
                        flagPalabra = true;

                        if (request.Texto[i] == '-' || request.Texto[i] == '_')
                        {
                            guiones++;
                        }
                    }
                }

                return new AnalizarTextoResponse { 
                    CantidadGuiones = guiones,
                    CantidadPalabras = palabras,
                    CantidadEspaciosEnBlanco = espaciosEnBlanco
                };
            }, cancellationToken);
        }
        public async Task<IEnumerable<OpcionTipoOrdenamiento>> GetOpcionesOrdenamiento()
        {
            return await _parametros.GetAllOpcionesOrdenamiento();
        }
        public async Task<OrdenarTextoResponse> OrdenarTexto(OrdenarTextoRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Texto)) {
                return new OrdenarTextoResponse { TextoOrdenado = string.Empty };
            }

            return await Task.Run(() =>
            {
                var palabras = request.Texto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                cancellationToken.ThrowIfCancellationRequested();

                switch (request.TipoOrdenamiento)
                {
                    case TipoOrdenamiento.AlfabeticoAscentente:
                        Array.Sort(palabras, StringComparer.InvariantCulture);
                        break;

                    case TipoOrdenamiento.AlfabeticoDescendente:
                        Array.Sort(palabras, (x, y) => StringComparer.InvariantCulture.Compare(y, x));
                        break;

                    case TipoOrdenamiento.Longitud:
                        Array.Sort(palabras, (x, y) => x.Length.CompareTo(y.Length));
                        break;
                }

                cancellationToken.ThrowIfCancellationRequested();
                string resultado = string.Join(" ", palabras);

                return new OrdenarTextoResponse { TextoOrdenado = resultado };

            }, cancellationToken);
        }
    }
}
