using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCafeteria.Web.ViewModels.DetalleVenta;

namespace WebCafeteria.Web.ViewModels.Venta
{
    public class VentaDetailVm
    {
        public VentaListVm Venta { get; set; }
        public List<DetalleVentaListVm> Detalles { get; set; }
    }
}