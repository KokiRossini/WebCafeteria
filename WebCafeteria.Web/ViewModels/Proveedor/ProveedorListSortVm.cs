using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCafeteria.Web.ViewModels.Cliente;

namespace WebCafeteria.Web.ViewModels.Proveedor
{
    public class ProveedorListSortVm
    {
        public IPagedList<ProveedorListVm> Proveedores { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }
        public string SearchBy { get; set; }

    }
}