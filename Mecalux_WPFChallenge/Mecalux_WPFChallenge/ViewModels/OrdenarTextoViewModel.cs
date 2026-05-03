using Entidades.Clases;
using Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecalux_WPFChallenge.ViewModels
{
    public class OrdenarTextoViewModel : ViewModelBase
    {
        private IProcesadorTextoService _procesadorTexto;
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

        public OrdenarTextoViewModel(IProcesadorTextoService service) {
            _procesadorTexto = service;
            _ = LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            var datos = await _procesadorTexto.GetOpcionesOrdenamiento();

            TiposOrdenamiento.Clear();
            foreach (var item in datos)
            {
                TiposOrdenamiento.Add(item);
            }
        }
    }
}
