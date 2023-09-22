using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.Carrito
{
    public class ItemCarritoVm
    {
        public int TamañoProductoId { get; set; }

        [DisplayName("Producto")]
        public string NombreProducto { get; set; }
        [DisplayName("Tamaño")]
        public string NombreTamaño { get; set; }
        public int Cantidad { get; set; }

        [DisplayName("P. Unit.")]
        public decimal PrecioUnitario { get; set; }

        [DisplayName("P. Total")]
        public decimal PrecioTotal => Cantidad * PrecioUnitario;

    }
}