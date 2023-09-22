using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.DetalleVenta
{
    public class DetalleVentaListVm
    {
        public int DetalleVentaId { get; set; }
        public string Producto { get; set; }
        public string Tamaño { get; set; }
        [DisplayName("P. Unit.")]
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

    }
}