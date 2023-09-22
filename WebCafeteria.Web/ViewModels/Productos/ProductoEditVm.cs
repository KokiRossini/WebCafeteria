using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCafeteria.Web.ViewModels.Productos
{
    public class ProductoEditVm
    {
        public int ProductoId { get; set; }
        [DisplayName("Producto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreProducto { get; set; }
        [DisplayName("Categoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoria")]
        public int CategoriaId { get; set; }
        [DisplayName("Proveedor")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoria")]

        public int ProveedorId { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        [DisplayName("Imagen")]
        public HttpPostedFileBase imagenFile { get; set; }

        public int CategoriaAnteriorId { get; set; }

        public List<SelectListItem> Categorias { get; set;}
        public List<SelectListItem> Proveedores { get; set; }

    }
}