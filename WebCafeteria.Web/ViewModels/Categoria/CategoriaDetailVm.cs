using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCafeteria.Web.ViewModels.Productos;

namespace WebCafeteria.Web.ViewModels.Categoria
{
    public class CategoriaDetailVm
    {
        public CategoriaListVm Categoria { get; set; }
        public List<ProductoListVm> Productos { get; set; }
    }
}