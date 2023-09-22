using System;

namespace WebCafeteria.Entidades.Dtos.Venta
{
    public class VentaListDto
    {
        public int VentaId { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }

        //public Cliente Cliente { get; set; }
    }
}
