using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCafeteria.Web.ViewModels.Tamaño;
using WebCafeteria.Web.ViewModels.TamañoProducto;

namespace WebCafeteria.Web.ViewModels.Productos
{
    public class ProductoDetailsVm
    {
        public ProductoListVm Producto { get; set; }
        public List<TamañoProductoListVm> Detalles { get; set; }

    }
}