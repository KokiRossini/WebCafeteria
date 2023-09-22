using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Servicios.Servicios;
using WebCafeteria.Web.App_Start;
using WebCafeteria.Web.ViewModels;
using WebCafeteria.Web.ViewModels.Ciudad;
using WebCafeteria.Web.ViewModels.Productos;

namespace WebCafeteria.Web.Controllers
{
    public class CiudadesController : Controller
    {
        // GET: Ciudades
        private readonly IServiciosCiudades _servicios;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IMapper _mapper;


        public CiudadesController(IServiciosCiudades servicios, IServiciosPaises serviciosPaises)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosPaises = serviciosPaises;
        }

        public ActionResult Index(int? PaisFiltro, int? page, int? pageSize)
        {
            List<CiudadListDto> lista;
            if (PaisFiltro == null)
            {
                lista = _servicios.GetCiudades();
            }
            else
            {
                lista = _servicios.GetCiudades(PaisFiltro.Value);
            }
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;

            //var lista = _servicios.GetProductos();
            var listaVm = _mapper.Map<List<CiudadListVm>>(lista);
            var ciudadVm = new CiudadFiltroVm
            {
                PaisFiltro = PaisFiltro,
                Paises = _serviciosPaises.GetPaisesDropDownList(),
                Ciudades = listaVm.ToPagedList(page.Value, pageSize.Value)
            };


            return View(ciudadVm);

            //List<CiudadListDto> lista;
            //if (PaisFiltro == null)
            //{
            //    lista = _servicios.GetCiudades();
            //}
            //else
            //{
            //    lista = _servicios.GetCiudades(PaisFiltro.Value);
            //}
            //page = page ?? 1;
            //pageSize = pageSize ?? 10;
            //ViewBag.PageSize = pageSize;

            ////var lista =_servicios.GetCiudades();
            //var listaVm = _mapper.Map<List<CiudadListVm>>(lista);
            //var ciudadVm = new CiudadFiltroVm
            //{
            //    PaisFiltro = PaisFiltro,
            //    Paises = _serviciosPaises.GetPaisesDropDownList(),
            //    Ciudades = listaVm.ToPagedList(page.Value, pageSize.Value)
            //};

            //return View(ciudadVm);
        }
        public ActionResult Create()
        {
            //var listaPaises = _serviciosPaises.GetPaises();
            //var dropDownPaises = listaPaises.Select(p => new SelectListItem()
            //{
            //    Text = p.NombrePais,
            //    Value = p.PaisId.ToString()
            //}).ToList();

            //var ciudadVm = new CiudadEditVm()
            //{
            //    Paises = dropDownPaises
            //};
            var ciudadVm = new CiudadEditVm()
            {
                Paises = _serviciosPaises.GetPaisesDropDownList()
            };

            return View(ciudadVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Paises=_serviciosPaises.GetPaisesDropDownList();
                return View(ciudadVm);
            }
            var ciudad = _mapper.Map<Ciudad>(ciudadVm);
            if (_servicios.Existe(ciudad))
            {
                ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                return View(ciudadVm);
            }
            _servicios.Guardar(ciudad);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ciudad = _servicios.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
            return View(ciudadVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var ciudad = _servicios.GetCiudadPorId(id);
            var ciudadVm = _mapper.Map<CiudadListVm>(ciudad);
            try
            {
                if (!_servicios.EstaRelacionada(ciudad))
                {
                    _servicios.Borrar(ciudad.CiudadId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ciudad relacionada... Baja denegada!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de ciudades");
                return View(ciudadVm);

            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ciudad = _servicios.GetCiudadPorId(id.Value);
            if (ciudad == null)
            {
                return HttpNotFound("Código de ciudad inexistente!!!");
            }
            var ciudadVm = _mapper.Map<CiudadEditVm>(ciudad);


            ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            return View(ciudadVm);

        }
        [HttpPost]
        public ActionResult Edit(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                return View(ciudadVm);
            }
            try
            {
                var ciudad = _mapper.Map<Ciudad>(ciudadVm);
                if (!_servicios.Existe(ciudad))
                {
                    _servicios.Guardar(ciudad);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                    ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                    return View(ciudadVm);
                }
            }
            catch (Exception)
            {
                ciudadVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                ModelState.AddModelError(string.Empty, "Ciudad existente!!!");
                return View(ciudadVm);
            }
        }


    }
}