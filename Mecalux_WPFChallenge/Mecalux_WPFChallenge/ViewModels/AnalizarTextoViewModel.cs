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
    public class AnalizarTextoViewModel : ViewModelBase
    {
        private IProcesadorTextoService _procesadorTexto;
        public AsyncRelayCommand AnalizarTextoCommand { get; }

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

        private int _guiones;
        public int Guiones { 
        get => _guiones;
            set { 
                _guiones = value;
                RaisePropertyChanged(nameof(Guiones));
            }
        }

        private int _espaciosEnBlanco;
        public int EspaciosEnBlanco
        {
            get => _espaciosEnBlanco;
            set
            {
                _espaciosEnBlanco = value;
                RaisePropertyChanged(nameof(EspaciosEnBlanco));
            }
        }   

        private int _palabras;
        public int Palabras
        {
            get => _palabras;
            set
            {
                _palabras = value;
                RaisePropertyChanged(nameof(Palabras));
            }
        }   

        public AnalizarTextoViewModel(IProcesadorTextoService service)
        {
            _procesadorTexto = service;
            AnalizarTextoCommand = new AsyncRelayCommand(AnalizarTexto);
        }

        private async Task AnalizarTexto()
        {
            AnalizarTextoRequest request = new AnalizarTextoRequest
            {
                Texto = Texto
            };

            AnalizarTextoResponse response = await _procesadorTexto.AnalizarTexto(request, default);

            Guiones = response.CantidadGuiones;
            EspaciosEnBlanco = response.CantidadEspaciosEnBlanco;
            Palabras = response.CantidadPalabras;
        }
    }
}
