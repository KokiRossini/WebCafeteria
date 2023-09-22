using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCafeteria.Entidades;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Web.App_Start;
using WebCafeteria.Web.ViewModels;
using WebCafeteria.Web.ViewModels.Ciudad;
using WebCafeteria.Web.ViewModels.Cliente;
using WebCafeteria.Web.ViewModels.Pais;
using WebCafeteria.Web.ViewModels.Productos;
using WebCafeteria.Web.ViewModels.Proveedor;

namespace WebCafeteria.Web.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IServiciosProveedores _servicios;
        private readonly IServiciosCiudades _serviciosCiudades;
        private readonly IServiciosProductos _serviciosProductos;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IMapper _mapper;

        public ProveedoresController(IServiciosProveedores servicios, IServiciosPaises serviciosPaises, IServiciosCiudades serviciosCiudades, IServiciosProductos serviciosProductos)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosPaises = serviciosPaises;
            _serviciosCiudades = serviciosCiudades;
            _serviciosProductos = serviciosProductos;
        }

        public ActionResult Index(int? page, int? pageSize, string SortBy = "Proveedor", string SearchBy = null)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            var lista = _servicios.GetProveedores();
            if (SearchBy != null)
            {
                lista = lista
                    .Where(c => c.NombreProveedor.Contains(SearchBy) || c.Pais.Contains(SearchBy))
                    .ToList();
            }

            var listaVm = _mapper.Map<List<ProveedorListVm>>(lista);
            if (SortBy == "Proveedor")
            {
                listaVm = listaVm.OrderBy(c => c.NombreProveedor).ToList();
            }
            else
            {
                listaVm = listaVm.OrderBy(c => c.Pais).ThenBy(c => c.Ciudad).ToList();

            }
            listaVm.ForEach(p => p.CantidadProductos = _serviciosProductos
                .GetCantidad(c => c.ProveedorId == p.ProveedorId));


            var proveedorVm = new ProveedorListSortVm
            {
                Proveedores = listaVm.ToPagedList(page.Value, pageSize.Value),
                Sorts = new Dictionary<string, string> {
                    {"Por Proveedor","Proveedor"},
                    {"Por País","Pais" }
                },
                SortBy = SortBy,
                SearchBy = SearchBy
            };

            return View(proveedorVm);
        }

        public ActionResult Create()
        {
            var proveedorVm=new ProveedorEditVm();
            proveedorVm.Paises=_serviciosPaises.GetPaisesDropDownList();
            proveedorVm.Ciudades = new List<SelectListItem>();
            return View(proveedorVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                return View(proveedorVm);
            }
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorVm);
                if (!_servicios.Existe(proveedor))
                {
                    _servicios.Guardar(proveedor);
                    TempData["Msg"] = "Registro agregado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                    proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);

                    return View(proveedorVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intentar agregar un Cliente");
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);

                return View(proveedorVm);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Cód. de proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorEditVm>(proveedor);
            proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedor.PaisId);
            return View(proveedorVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                return View(proveedorVm);
            }
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorVm);
                if (!_servicios.Existe(proveedor))
                {
                    _servicios.Guardar(proveedor);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                    proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                    return View(proveedorVm);
                }

            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Ciudades = _serviciosCiudades.GetCiudadesDropDownList(proveedorVm.PaisId);
                return View(proveedorVm);
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Cód. proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            return View(proveedorVm);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var proveedor = _servicios.GetProveedorPorId(id);
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            try
            {
                if (!_servicios.EstaRelacionado(proveedor))
                {
                    _servicios.Borrar(id);
                    TempData["Msg"] = "Proveedor borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registro relacionado... Baja denegada");
                    return View(proveedorVm);
                }
            }
            catch (System.Exception)
            {

                ModelState.AddModelError(string.Empty, "Error al intengar borrar un proveedor");
                return View(proveedorVm);

            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var proveedor = _servicios.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Código de proveedor inesistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            proveedorVm.CantidadProductos = _serviciosProductos
                .GetCantidad(c => c.ProveedorId == proveedorVm.ProveedorId);
            
            var proveedorDetailVm = new ProveedorDetailVm()
            {
                Proveedor = proveedorVm,
                Productos = _mapper.Map<List<ProductoListVm>>(
                _serviciosProductos.GetProductosPorProveedorId(proveedor.Id))
            };
            return View(proveedorDetailVm);
        }




        public JsonResult GetCities(int paisId)
        {
            var lista = _serviciosCiudades.GetCiudades(paisId);
            var ciudadesVm = _mapper.Map<List<CiudadListVm>>(lista);
            return Json(ciudadesVm);
        }

    }
}