using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCafeteria.Web.ViewModels.Ciudad;
using WebCafeteria.Web.ViewModels.Productos;

namespace WebCafeteria.Web.ViewModels.Proveedor
{
    public class ProveedorDetailVm
    {
        public ProveedorListVm Proveedor { get; set; }
        public List<ProductoListVm> Productos { get; set; }

    }
}