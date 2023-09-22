using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCafeteria.Web.ViewModels.TamañoProducto;

namespace WebCafeteria.Web.ViewModels.Tamaño
{
    public class TamañoDetailsVm
    {
        public TamañoListVm Tamaño { get; set; }
        public List<TamañoProductoListVm> Detalles { get; set; }
    }
}