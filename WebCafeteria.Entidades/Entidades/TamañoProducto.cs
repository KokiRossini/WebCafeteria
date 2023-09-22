using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades.Entidades
{
    public class TamañoProducto
    {
        public int TamañoProductoId { get; set; }
        public int TamañoId { get; set; }
        public int ProductoId { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public int UnidadesEnPedido { get; set; }
        public bool Suspendido { get; set; }


        public Tamaño tamaño { get; set; }
        public Producto producto { get; set; }
        //public int UnidadesDisponibles() => Stock - UnidadesEnPedido;

    }
}
