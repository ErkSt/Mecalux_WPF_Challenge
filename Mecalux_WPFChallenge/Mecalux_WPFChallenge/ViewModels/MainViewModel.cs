using Mecalux_WPFChallenge.Commands;
using Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecalux_WPFChallenge.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private object _currentViewModel;
        private readonly IProcesadorTextoService _textoService;

        public AsyncRelayCommand AbrirOrdenarTextoCommand { get; }
        public RelayCommand AbrirAnalizarTextoCommand { get; }

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainViewModel(IProcesadorTextoService textoService)
        {
            _textoService = textoService;
            AbrirOrdenarTextoCommand = new AsyncRelayCommand(AbrirOrdenarTexto);
            AbrirAnalizarTextoCommand = new RelayCommand(AbrirAnalizarTexto);
        }   

        private async Task AbrirOrdenarTexto()
        {
           _currentViewModel = new OrdenarTextoViewModel();
            return;
        }

        private void AbrirAnalizarTexto()
        {
           _currentViewModel = new AnalizarTextoViewModel();
        }
    }
}
