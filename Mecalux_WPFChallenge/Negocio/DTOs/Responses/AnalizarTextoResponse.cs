using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs.Responses
{
    public class AnalizarTextoResponse
    {
        public int CantidadGuiones { get; set; }
        public int CantidadPalabras { get; set; }
        public int CantidadEspaciosEnBlanco { get; set; }
    }
}
