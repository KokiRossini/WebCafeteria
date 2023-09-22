using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCafeteria.Web.ViewModels.Ciudad;

namespace WebCafeteria.Web.ViewModels.Pais
{
    public class PaisDetailVm
    {
        public PaisListVm Pais { get; set; }
        public List<CiudadListVm> Ciudades { get; set; }

    }
}