using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.Categoria
{
    public class CategoriaListVm
    {
        public int CategoriaId { get; set; }

        [DisplayName("Categoria")]
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        [DisplayName("Cant. Productos")]

        public int CantidadProductos { get; set; }

    }
}