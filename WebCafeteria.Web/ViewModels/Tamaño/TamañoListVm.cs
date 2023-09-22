using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.Tamaño
{
    public class TamañoListVm
    {
        public int TamañoId { get; set; }
        [DisplayName ("Tamaño")]
        public string NombreTamaño { get; set; }
        [DisplayName("Cant. Productos")]
        public int CantidadTamañosProductos { get; set; }
    }
}