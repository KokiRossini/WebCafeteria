using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCafeteria.Entidades;
using WebCafeteria.Servicios;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Servicios.Servicios;
using WebCafeteria.Web.App_Start;
using WebCafeteria.Web.ViewModels;
using WebCafeteria.Web.ViewModels.Categoria;
using WebCafeteria.Web.ViewModels.Ciudad;
using WebCafeteria.Web.ViewModels.Pais;
using WebCafeteria.Web.ViewModels.Productos;

namespace WebCafeteria.Web.Controllers
{
    public class CategoriasController : Controller
    {
        // GET: Categorias
        private readonly IServiciosCategorias _servicios;
        private readonly IServiciosProductos _serviciosProductos;
        private readonly IServiciosProveedores _serviciosProveedores;
        private readonly IMapper _mapper;

        public CategoriasController(IServiciosCategorias servicios, IServiciosProductos serviciosProductos, IServiciosProveedores serviciosProveedores)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosProductos = serviciosProductos;
            _serviciosProveedores = serviciosProveedores;
        }

        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicios.GetCategorias();
            var listaVm=_mapper.Map<List<CategoriaListVm>>(lista);
            listaVm.ForEach(c => c.CantidadProductos = _serviciosProductos
                            .GetCantidad(p=>p.CategoriaId==c.CategoriaId));

            page = page ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageSize = pageSize;
            if (User.IsInRole("Admin"))
            {
                return View(listaVm.ToPagedList(page.Value, pageSize.Value));

            }
            return View("ReadOnlyIndex", listaVm.ToPagedList(page.Value, pageSize.Value));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaEditVm categoriaVm)
        {
            if (ModelState.IsValid)
            {
                var categoria = _mapper.Map<Categoria>(categoriaVm);
                if (_servicios.Existe(categoria))
                {
                    ModelState.AddModelError(string.Empty, "Categoria existente!!!");
                    return View(categoriaVm);
                }
                _servicios.Guardar(categoria);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");

            }
            else
            {
                return View(categoriaVm);
            }
        }
        public ActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var categoria = _servicios.GetCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Codigo de categoria erroneo!!");
            }
            var categoriaVm = _mapper.Map<CategoriaListVm>(categoria);
            return View(categoriaVm);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var categoria = _servicios.GetCategoriaPorId(id);
            if (_servicios.EstaRelacionado(categoria))
            {
                var categoriaVm = _mapper.Map<CategoriaListVm>(categoria);
                ModelState.AddModelError(string.Empty, "Categoria relacionado... Baja denegada!!!");
                return View(categoriaVm);
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
            var categoria = _servicios.GetCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Codigo de categoria erroneo!!");
            }
            var categoriaVm = _mapper.Map<CategoriaEditVm>(categoria);
            return View(categoriaVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaEditVm categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaVm);
            }
            var categoria = _mapper.Map<Categoria>(categoriaVm);
            if (_servicios.Existe(categoria))
            {
                ModelState.AddModelError(string.Empty, "Categoria existente!!!");
                return View(categoriaVm);
            }
            _servicios.Guardar(categoria);
            TempData["Msg"] = "Registro editado satifactoriamente!!!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var categoria = _servicios.GetCategoriaPorId(id.Value);
            if (categoria == null)
            {
                return HttpNotFound("Código de categoria inesistente!!!");
            }

            var categoriaVm = _mapper.Map<CategoriaListVm>(categoria);
            categoriaVm.CantidadProductos = _serviciosProductos
                .GetCantidad(p => p.CategoriaId == categoria.CategoriaId);
            var categoriaDetailVm = new CategoriaDetailVm()
            {
                Categoria = categoriaVm,
                Productos = _mapper.Map<List<ProductoListVm>>(
                _serviciosProductos.GetProductos(categoria.CategoriaId))
            };
            if (User.IsInRole("Admin"))
            {
                return View(categoriaDetailVm);
            }
            return View("ReadOnlyDetails",categoriaDetailVm);
        }
        public ActionResult AddProducto(int? categoriaId)
        {
            if (categoriaId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var categoria = _servicios.GetCategoriaPorId(categoriaId.Value);
            if (categoria == null)
            {
                return HttpNotFound("Código de la categoria inesistente!!!");
            }
            var productoVm = new ProductoEditVm()
            {
                CategoriaAnteriorId=categoriaId.Value,
                Categorias = _servicios.GetCategoriasDropDownList(),
                Proveedores=_serviciosProveedores.GetProveedoresDropDownList(),
            };
            return View(productoVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProducto(ProductoEditVm productoVm)
        {
            if (!ModelState.IsValid)
            {
                productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                return View(productoVm);
            }
            try
            {
                var producto = _mapper.Map<Producto>(productoVm);
                if (!_serviciosProductos.Existe(producto))
                {
                    _serviciosProductos.Guardar(producto);
                    return RedirectToAction($"Details/{producto.CategoriaId}");
                }
                else
                {

                    productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                    productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                    ModelState.AddModelError(string.Empty, "Producto existente!!!");
                    return View(productoVm);
                }
            }
            catch (Exception)
            {

                productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                ModelState.AddModelError(string.Empty, "Error al insertar Producto !!!");
                return View(productoVm);

            }
        }
        public ActionResult DeleteProducto(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var producto = _serviciosProductos.GetProductoPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de producto inesistente!!!");
            }
            var productoVm = _mapper.Map<ProductoEditVm>(producto);
            productoVm.CategoriaAnteriorId = producto.CategoriaId;

            productoVm.Categorias = _servicios.GetCategoriasDropDownList();
            productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
            return View(productoVm);

        }
        [HttpPost, ActionName("DeleteProducto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductoConfirm(int id)
        {
            var producto = _serviciosProductos.GetProductoPorId(id);
            var productoVm = _mapper.Map<ProductoEditVm>(producto);
            try
            {
                if (!_serviciosProductos.EstaRelacionado(producto))
                {
                    _serviciosProductos.Borrar(id);
                    return RedirectToAction($"Details/{producto.CategoriaId}");
                }
                else
                {

                    productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                    productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                    return View(productoVm);
                }
            }
            catch (Exception)
            {

                productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un producto");
                return View(productoVm);

            }
        }
        public ActionResult EditProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var producto = _serviciosProductos.GetProductoPorId(id.Value);
            if (producto == null)
            {
                return HttpNotFound("Código de producto inesistente!!!");
            }
            var productoVm = _mapper.Map<ProductoEditVm>(producto);
            productoVm.CategoriaAnteriorId = producto.CategoriaId;
            productoVm.Categorias = _servicios.GetCategoriasDropDownList();
            productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
            return View(productoVm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProducto(ProductoEditVm productoVm)
        {
            if (!ModelState.IsValid)
            {
                productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                ModelState.AddModelError(string.Empty, "Categoria no valida!!!");

                return View(productoVm);

            }
            try
            {
                var producto = _mapper.Map<Producto>(productoVm);
                if (!_serviciosProductos.Existe(producto))
                {
                    _serviciosProductos.Guardar(producto);
                    productoVm.CategoriaAnteriorId = productoVm.CategoriaId;

                    return RedirectToAction($"Details/{productoVm.CategoriaAnteriorId}");
                }
                else
                {
                    productoVm.CategoriaAnteriorId = producto.CategoriaId;

                    productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                    productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                    ModelState.AddModelError(string.Empty, "Producto existente!!!");
                    return View(productoVm);

                }
            }
            catch (Exception)
            {
                productoVm.CategoriaAnteriorId = productoVm.CategoriaId;

                productoVm.Categorias = _servicios.GetCategoriasDropDownList();
                productoVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                ModelState.AddModelError(string.Empty, "Error al editar el Producto !!!");
                return View(productoVm);
            }
        }







    }
}