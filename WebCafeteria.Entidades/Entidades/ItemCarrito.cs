using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades.Entidades
{
    public class ItemCarrito
    {
        public int ItemCarritoId { get; set; }
        public int TamañoProductoId { get; set; }
        public string UserName { get; set; }
        public string NombreProducto { get; set; }
        public string NombreTamaño { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal => Cantidad * PrecioUnitario;
    }
}
