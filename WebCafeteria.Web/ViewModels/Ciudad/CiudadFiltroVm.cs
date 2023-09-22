using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCafeteria.Web.ViewModels.Productos;

namespace WebCafeteria.Web.ViewModels.Ciudad
{
    public class CiudadFiltroVm
    {
        public List<SelectListItem> Paises { get; set; }
        public IPagedList<CiudadListVm> Ciudades { get; set; }
        public int? PaisFiltro { get; set; }

    }
}