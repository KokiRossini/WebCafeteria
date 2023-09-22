using AutoMapper;
using Microsoft.Owin.Security;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCafeteria.Entidades.Dtos.Venta;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Web.App_Start;
using WebCafeteria.Web.ViewModels.DetalleVenta;
using WebCafeteria.Web.ViewModels.Venta;

namespace WebCafeteria.Web.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        private readonly IServiciosVentas _servicios;
        private readonly IMapper _mapper;

        public VentasController(IServiciosVentas servicios)
        {
            _servicios = servicios;
            _mapper= AutoMapperConfig.Mapper;
        }

        public ActionResult Index(int? page, int? pageSize)
        {
            var lista=_servicios.GetVentas();
            var listaVm = _mapper.Map<List<VentaListVm>>(lista);
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;
            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }
        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var venta = _servicios.GetVentaPorId(id.Value);
            if (venta == null)
            {
                return HttpNotFound("Código no encontrado");
            }
            var detalle = _servicios.GetDetalleVenta(id.Value);
            var detalleVm = _mapper.Map<List<DetalleVentaListVm>>(detalle);
            var ventaVm = new VentaDetailVm
            {
                Venta = _mapper.Map<VentaListVm>(venta),
                Detalles = detalleVm
            };
            return View(ventaVm);
        }
    }
}