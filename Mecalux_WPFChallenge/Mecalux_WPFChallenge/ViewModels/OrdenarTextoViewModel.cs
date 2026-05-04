using Entidades.Clases;
using Mecalux_WPFChallenge.Commands;
using Negocio.DTOs.Requests;
using Negocio.DTOs.Responses;
using Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecalux_WPFChallenge.ViewModels
{
    public class OrdenarTextoViewModel : FormularioBase
    {
        private IProcesadorTextoService _procesadorTexto;
        public AsyncRelayCommand OrdenarTextoCommand { get; }

        public ObservableCollection<OpcionTipoOrdenamiento> TiposOrdenamiento { get; } = new ObservableCollection<OpcionTipoOrdenamiento>();

        private OpcionTipoOrdenamiento _tipoOrdenamiento;
        public OpcionTipoOrdenamiento TipoOrdenamiento
        {
            get => _tipoOrdenamiento;
            set
            {
                if (_tipoOrdenamiento == value) return;
                _tipoOrdenamiento = value;
                RaisePropertyChanged(nameof(TipoOrdenamiento));
                ValidarTipo();
            }
        }

        private string _texto;
        public string Texto
        {
            get => _texto;
            set
            {
                _texto = value;
                RaisePropertyChanged(nameof(Texto));
            }
        }

        private string _textoOrdenado;
        public string TextoOrdenado
        {
            get => _textoOrdenado;
            set
            {
                _textoOrdenado = value;
                RaisePropertyChanged(nameof(TextoOrdenado));
            }
        }

        public OrdenarTextoViewModel(IProcesadorTextoService service)
        {
            _procesadorTexto = service;
            OrdenarTextoCommand = new AsyncRelayCommand(OrdenarTextoAsync);
            _ = LoadDataAsync();
        }

        private void ValidarTipo()
        {
            ClearErrors(nameof(TipoOrdenamiento));
            if (_tipoOrdenamiento == null) {
                AddError(nameof(TipoOrdenamiento), "El tipo es obligatorio.");
            }
        }

        public async Task OrdenarTextoAsync()
        {
            ValidarTipo();
            if (HasErrors)
                return;

            OrdenarTextoRequest request = new OrdenarTextoRequest
            {
                Texto = Texto,
                TipoOrdenamiento = TipoOrdenamiento.Tipo
            };

            OrdenarTextoResponse response = await _procesadorTexto.OrdenarTexto(request, default);
            TextoOrdenado = response.TextoOrdenado;

        }
        public async Task LoadDataAsync()
        {
            var datos = await _procesadorTexto.GetOpcionesOrdenamiento();

            TiposOrdenamiento.Clear();
            foreach (var item in datos)
            {
                TiposOrdenamiento.Add(item);
            }

            if (_tipoOrdenamiento == null && TiposOrdenamiento.Count > 0)
            {
                TipoOrdenamiento = TiposOrdenamiento[0];
            }
        }
    }
}
