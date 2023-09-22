using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Servicios;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Web.App_Start;
using WebCafeteria.Web.ViewModels;
using WebCafeteria.Web.ViewModels.Pais;
using WebCafeteria.Web.ViewModels.Tamaño;
using WebCafeteria.Web.ViewModels.TamañoProducto;

namespace WebCafeteria.Web.Controllers
{
    public class TamañosController : Controller
    {
        // GET: Tamaños
        private readonly IServiciosTamaños _servicios;
        private readonly IServiciosTamañosProductos _serviciosTamañoProductos;
        private readonly IMapper _mapper;

        public TamañosController(IServiciosTamaños servicios, IServiciosTamañosProductos serviciosTamañoProductos)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosTamañoProductos = serviciosTamañoProductos;
        }


        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicios.GetTamaños();
            var listaVm = _mapper.Map<List<TamañoListVm>>(lista);
            listaVm.ForEach(t => t.CantidadTamañosProductos = _serviciosTamañoProductos
                            .GetCantidadProductos(p => p.TamañoId == t.TamañoId));

            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;
            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TamañoEditVm tamañoVm)
        {
            if (ModelState.IsValid)
            {
                var tamaño = _mapper.Map<Tamaño>(tamañoVm);
                if (_servicios.Existe(tamaño))
                {
                    ModelState.AddModelError(string.Empty, "Tamaño existente!!!");
                    return View(tamañoVm);
                }
                _servicios.Guardar(tamaño);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");

            }
            else
            {
                return View(tamañoVm);
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var tamaño = _servicios.GetTamañoPorId(id.Value);
            if (tamaño==null)
            {
                return HttpNotFound("Codigo de tamaño erroneo");
            }
            var tamañoVm = _mapper.Map<TamañoListVm>(tamaño);
            return View(tamañoVm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var tamaño = _servicios.GetTamañoPorId(id);
            if (_servicios.EstaRelacionado(tamaño))
            {
                var tamañoVm = _mapper.Map<TamañoListVm>(tamaño);
                ModelState.AddModelError(string.Empty, "Tamaño relacionado... Baja denegada!!!");
                return View(tamañoVm);
            }
            _servicios.Borrar(id);
            TempData["Msg"] = "Registro borrado satifactoriamente!!!";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var tamaño = _servicios.GetTamañoPorId(id.Value);
            if (tamaño == null)
            {
                return HttpNotFound("Codigo de tamaño erroneo!!");
            }
            var tamañoVm = _mapper.Map<TamañoEditVm>(tamaño);
            return View(tamañoVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TamañoEditVm tamañoVm)
        {
            if (!ModelState.IsValid)
            {
                return View(tamañoVm);
            }
            var tamaño = _mapper.Map<Tamaño>(tamañoVm);
            if (_servicios.Existe(tamaño))
            {
                ModelState.AddModelError(string.Empty, "Tamaño existente!!!");
                return View(tamañoVm);
            }
            _servicios.Guardar(tamaño);
            TempData["Msg"] = "Registro editado satifactoriamente!!!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var tamaño = _servicios.GetTamañoPorId(id.Value);
            if (tamaño == null)
            {
                return HttpNotFound("Código de tamaño inesistente!!!");
            }
            var tamañoVm = _mapper.Map<TamañoListVm>(tamaño);
            tamañoVm.CantidadTamañosProductos = _serviciosTamañoProductos
                            .GetCantidadProductos(p => p.TamañoId == tamañoVm.TamañoId);
            //var tamañoProductoVm = _serviciosTamañoProductos.GetTamañoProductoPorId(id.Value);
            var ListaProductos = _serviciosTamañoProductos.GetProductosPorTamaño(tamaño.TamañoId);
            var tamañoDetailsVm = new TamañoDetailsVm()
            {
                Tamaño = tamañoVm,
                Detalles = _mapper.Map<List<TamañoProductoListVm>>(ListaProductos)
            };
            return View(tamañoDetailsVm);
        }


    }
}