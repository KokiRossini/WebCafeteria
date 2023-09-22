using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.Proveedor
{
    public class ProveedorListVm
    {
        public int ProveedorId { get; set; }
        [DisplayName("Proveedor")]

        public string NombreProveedor { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        [DisplayName ("Cant. Productos")]
        public int CantidadProductos { get; set; }


    }
}