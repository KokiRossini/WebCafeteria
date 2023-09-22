using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Servicios;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Servicios.Servicios;
using WebCafeteria.Utilidades;
using WebCafeteria.Web.App_Start;
using WebCafeteria.Web.ViewModels.Ciudad;
using WebCafeteria.Web.ViewModels.Productos;
using WebCafeteria.Web.ViewModels.Tamaño;
using WebCafeteria.Web.ViewModels.TamañoProducto;

namespace WebCafeteria.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IServiciosProductos _servicios;
        private readonly IServiciosTamañosProductos _serviciosTamañoProductos;
        private readonly IServiciosProveedores _serviciosProveedores;
        private readonly IServiciosCategorias _serviciosCategorias;
        private readonly IMapper _mapper;

        public ProductosController(IServiciosProductos servicios, IServiciosCategorias serviciosCategorias, IServiciosProveedores serviciosProveedores, IServiciosTamañosProductos serviciosTamañoProductos)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosCategorias = serviciosCategorias;
            _serviciosProveedores = serviciosProveedores;
            _serviciosTamañoProductos = serviciosTamañoProductos;

        }

        // GET: Producto
        public ActionResult Index(int? CategoriaFiltro, int? page, int? pageSize)
        {
            List<ProductoListDto> lista;
            if (CategoriaFiltro==null)
            {
                lista = _servicios.GetProductos();
            }
            else
            {
                lista=_servicios.GetProductos(CategoriaFiltro.Value);
            }
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;

            //var lista = _servicios.GetProductos();
            var listaVm = _mapper.Map<List<ProductoListVm>>(lista);
            listaVm.ForEach(t => t.CantidadTamañosProductos = _serviciosTamañoProductos
                .GetCantidadTamaños(p => p.ProductoId == t.ProductoId));
            var productoVm = new ProductoFiltroVm
            {
                CategoriaFiltro = CategoriaFiltro,
                Categorias=_serviciosCategorias.GetCategoriasDropDownList(),
                Productos=listaVm.ToPagedList(page.Value, pageSize.Value)
            };

            
            return View(productoVm);
        }
        public ActionResult Create()
        {
            var productoVm = new ProductoEditVm()
            {
                Categorias = _serviciosCategorias.GetCategoriasDropDownList(),
                Proveedores = _serviciosProveedores.GetProveedoresDropDownList()
            };
            return View(productoVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoEditVm productoVm)
        {
            var producto = _mapper.Map<Producto>(productoVm);

            if (!ModelState.IsValid)
            {
                productoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                return View(productoVm);
            }
            if (_servicios.Existe(producto))
            {

                productoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                ModelState.AddModelError(string.Empty, "Producto existente!!!");
                return View(productoVm);
            }
            if (productoVm.imagenFile != null)
            {
                string extension = Path.GetExtension(productoVm.imagenFile.FileName);
                string filename = Guid.NewGuid().ToString();

                var file = $"{filename}{extension}";
                var response = FileHelper.UploadPhoto(productoVm.imagenFile, WC.ProductosImagenesFolder, file);
                producto.Imagen = file;
            }

            _servicios.Guardar(producto);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var producto = _servicios.GetProductoPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código del producto inexistente!!!");
            }
            var productoVm = _mapper.Map<ProductoListVm>(producto);
            return View(productoVm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var producto = _servicios.GetProductoPorId(id);
            var productoVm = _mapper.Map<ProductoListVm>(producto);
            try
            {
                if (!_servicios.EstaRelacionado(producto))
                {
                    _servicios.Borrar(producto.ProductoId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Producto relacionado... Baja denegada!!");
                    return View(productoVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de ciudades");
                return View(productoVm);

            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var producto = _servicios.GetProductoPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de producto inexistente!!!");
            }
            var productoVm = _mapper.Map<ProductoEditVm>(producto);


            productoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
            productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

            return View(productoVm);

        }
        [HttpPost]
        public ActionResult Edit(ProductoEditVm productoVm)
        {
            if (!ModelState.IsValid)
            {
                productoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                return View(productoVm);
            }
            try
            {
                var producto = _mapper.Map<Producto>(productoVm);
                if (!_servicios.Existe(producto))
                {
                    if (productoVm.imagenFile != null)
                    {
                        string extension = Path.GetExtension(productoVm.imagenFile.FileName);
                        string filename = Guid.NewGuid().ToString();

                        var file = $"{filename}{extension}";
                        var response = FileHelper.UploadPhoto(productoVm.imagenFile, WC.ProductosImagenesFolder, file);
                        producto.Imagen = file;
                    }

                    _servicios.Guardar(producto);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    productoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                    productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                    ModelState.AddModelError(string.Empty, "Producto existente!!!");
                    return View(productoVm);
                }
            }
            catch (Exception)
            {
                productoVm.Categorias = _serviciosCategorias.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                ModelState.AddModelError(string.Empty, "Error al editar el Prodcto!!!");
                return View(productoVm);
            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var producto = _servicios.GetProductoPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de producto inesistente!!!");
            }
            var productoVm = _mapper.Map<ProductoListVm>(producto);
            productoVm.CantidadTamañosProductos = _serviciosTamañoProductos
                            .GetCantidadTamaños(p => p.ProductoId == productoVm.ProductoId);
            var ListaTamaños = _serviciosTamañoProductos.GetTamañosPorProducto(producto.ProductoId);
            var productoDetailsVm = new ProductoDetailsVm()
            {
                Producto = productoVm,
                Detalles = _mapper.Map<List<TamañoProductoListVm>>(ListaTamaños)
            };
            return View(productoDetailsVm);
        }





    }
}
