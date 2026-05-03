using Entidades.Enums;


namespace Negocio.DTOs.Requests
{
    public sealed class OrdenarTextoRequest
    {
        public TipoOrdenamiento TipoOrdenamiento { get; set; }
        public string Texto { get; set; }
    }
}
