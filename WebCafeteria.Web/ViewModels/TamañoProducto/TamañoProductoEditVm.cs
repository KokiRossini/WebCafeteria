using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCafeteria.Web.ViewModels.TamañoProducto
{
    public class TamañoProductoEditVm
    {
        public int TamañoProductoId { get; set; }

        [DisplayName("Tamaño")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tamaño")]
        public int TamañoId { get; set; }

        [DisplayName("Producto")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto")]
        public int ProductoId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock mal ingresado")]
        public int Stock { get; set; }
        public bool Suspendido { get; set; }


        [DisplayName("Precio Unit.")]
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.10, 10000, ErrorMessage = "Favor de ingresar un {0} entre {1} y{2}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]


        public decimal PrecioUnitario { get; set; }
        public List<SelectListItem> Productos { get; set; }
        public List<SelectListItem> Tamaños { get; set; }




    }
}