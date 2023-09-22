using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCafeteria.Web.ViewModels.Productos
{
    public class ProductoFiltroVm
    {
        public List<SelectListItem> Categorias { get; set; }
        public IPagedList<ProductoListVm> Productos { get; set; }
        public int? CategoriaFiltro { get; set; }

    }
}