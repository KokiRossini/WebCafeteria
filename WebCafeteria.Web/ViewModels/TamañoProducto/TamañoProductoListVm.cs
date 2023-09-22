using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.TamañoProducto
{
    public class TamañoProductoListVm
    {
        public int TamañoProductoId { get; set; }
        [DisplayName ("Tamaño")]
        public string NombreTamaño { get; set; }
        [DisplayName("Producto")]
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Suspendido { get; set; }

        public string Imagen { get; set; }


    }
}