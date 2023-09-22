using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos.TamañoProducto;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Servicios.Servicios;
using WebCafeteria.Web.App_Start;
using WebCafeteria.Web.ViewModels.Ciudad;
using WebCafeteria.Web.ViewModels.Productos;
using WebCafeteria.Web.ViewModels.TamañoProducto;

namespace WebCafeteria.Web.Controllers
{
    public class TamañoProductosController : Controller
    {
        // GET: TamañoProductos
        private readonly IServiciosTamañosProductos _servicios;
        private readonly IServiciosProductos _serviciosProductos;
        private readonly IServiciosTamaños _serviciosTamaños;
        private readonly IMapper _mapper;

        public TamañoProductosController(IServiciosTamañosProductos servicios, IServiciosProductos serviciosProductos, IServiciosTamaños serviciosTamaños)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosProductos = serviciosProductos;
            _serviciosTamaños = serviciosTamaños;
        }

        public ActionResult Index(int? page, int? pageSize)
        {
            List<TamañoProductoListDto> lista;
            if (User.IsInRole("Cliente"))
            {
                lista = _servicios.GetTamañosProductos(false);
            }
            else
            {
                lista = _servicios.GetTamañosProductos(true);
            }
            var listaVm = _mapper.Map<List<TamañoProductoListVm>>(lista);
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;
            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }
        public ActionResult Create()
        {
            var tamañosProductosVm = new TamañoProductoEditVm()
            {
                Productos = _serviciosProductos.GetProductosDropDown(),
                Tamaños = _serviciosTamaños.GetTamañosDropDown()
            };
            return View(tamañosProductosVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TamañoProductoEditVm tamañoProductoVm)
        {
            if (!ModelState.IsValid)
            {
                tamañoProductoVm.Productos = _serviciosProductos.GetProductosDropDown();
                tamañoProductoVm.Tamaños = _serviciosTamaños.GetTamañosDropDown();

                return View(tamañoProductoVm);
            }

            var tamañoProducto = _mapper.Map<TamañoProducto>(tamañoProductoVm);
            if (_servicios.Existe(tamañoProducto))
            {
                ModelState.AddModelError(string.Empty, "TamañoProducto existente!!!");
                tamañoProductoVm.Productos = _serviciosProductos.GetProductosDropDown();
                tamañoProductoVm.Tamaños = _serviciosTamaños.GetTamañosDropDown();

                return View(tamañoProductoVm);
            }
            _servicios.Guardar(tamañoProducto);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tamañosProductos = _servicios.GetTamañoProductoPorId(id.Value);
            if (tamañosProductos == null)
            {
                return HttpNotFound("Código de TamañoProducto inexistente!!!");
            }
            var tamañosProductosVm = _mapper.Map<TamañoProductoListVm>(tamañosProductos);
            return View(tamañosProductosVm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var tamañosProductos = _servicios.GetTamañoProductoPorId(id);
            var tamañosProductosVm = _mapper.Map<TamañoProductoListVm>(tamañosProductos);
            //Producto producto = _serviciosProductos.GetProductoPorId(tamañosProductos.ProductoId);
            try
            {
                if (!_servicios.EstaRelacionado(id))
                {
                    _servicios.Borrar(tamañosProductos.TamañoProductoId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "TamañoProducto relacionada... Baja denegada!!");
                    return View(tamañosProductosVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de ciudades");
                return View(tamañosProductosVm);

            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tamañoProducto = _servicios.GetTamañoProductoPorId(id.Value);
            if (tamañoProducto == null)
            {
                return HttpNotFound("Código del tamañoProducto inexistente!!!");
            }
            var tamañoProductoVm = _mapper.Map<TamañoProductoEditVm>(tamañoProducto);

            tamañoProductoVm.Productos = _serviciosProductos.GetProductosDropDown();
            tamañoProductoVm.Tamaños=_serviciosTamaños.GetTamañosDropDown();
            return View(tamañoProductoVm);

        }
        [HttpPost]
        
        public ActionResult Edit(TamañoProductoEditVm tamañoProductoVm)
        {
            if (!ModelState.IsValid)
            {
                tamañoProductoVm.Tamaños = _serviciosTamaños.GetTamañosDropDown();
                tamañoProductoVm.Productos = _serviciosProductos.GetProductosDropDown();
                return View(tamañoProductoVm);
            }
            try
            {
                var tamañoProducto = _mapper.Map<TamañoProducto>(tamañoProductoVm);
                if (!_servicios.Existe(tamañoProducto))
                {
                    _servicios.Guardar(tamañoProducto);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    tamañoProductoVm.Tamaños = _serviciosTamaños.GetTamañosDropDown();
                    tamañoProductoVm.Productos = _serviciosProductos.GetProductosDropDown();

                    ModelState.AddModelError(string.Empty, "Tamaño producto existente!!!");
                    return View(tamañoProductoVm);
                }
            }
            catch (Exception)
            {
                tamañoProductoVm.Tamaños = _serviciosTamaños.GetTamañosDropDown();
                tamañoProductoVm.Productos = _serviciosProductos.GetProductosDropDown();

                ModelState.AddModelError(string.Empty, "Tamaño producto existente!!!");
                return View(tamañoProductoVm);
            }
        }
        public ActionResult Details(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var tamañoProducto = _servicios.GetTamañoProductoPorId(id.Value);
            if (tamañoProducto == null)
            {
                return HttpNotFound("Cód. TamañoProducto inexistente!!!");
            }
            var tamañoProductoVm = _mapper.Map<TamañoProductoListVm>(tamañoProducto);
            return View(tamañoProductoVm);

        }



    }
}