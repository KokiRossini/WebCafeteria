using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos;

namespace WebCafeteria.Datos.Repositorios
{
    public class RepositorioProductos : IRepositorioProductos
    {
        private readonly CafeteriaDbContext _context;
        public RepositorioProductos(CafeteriaDbContext context)
        {
            _context = context;
        }

        //public void ActualizarStock(int productoId, int cantidad)
        //{
        //    var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == productoId);
        //    productoInDb.UnidadesEnPedido -= cantidad;
        //    productoInDb.Stock -= cantidad;
        //    _context.Entry(productoInDb).State = EntityState.Modified;

        //}
        public void Agregar(Producto producto)
        {
            _context.Productos.Add(producto);
        }

        //public void ActualizarUnidadesEnPedido(int productoId, int cantidad)
        //{
        //    var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == productoId);
        //    productoInDb.UnidadesEnPedido += cantidad;
        //    _context.Entry(productoInDb).State = EntityState.Modified;
        //}


        public void Borrar(int id)
        {
            var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == id);
            if (productoInDb == null)
            {
                throw new Exception("Producto borrado por otro usuario");
            }
            _context.Entry(productoInDb).State = EntityState.Deleted;
        }

        public void Editar(Producto producto)
        {
            var productoInDb = _context.Productos.SingleOrDefault(p => p.ProductoId == producto.ProductoId);
            if (productoInDb == null)
            {
                throw new Exception("Producto borrado por otro usuario");
            }
            productoInDb.NombreProducto = producto.NombreProducto;
            productoInDb.CategoriaId = producto.CategoriaId;
            productoInDb.ProveedorId = producto.ProveedorId;
            //productoInDb.StockMinimo = producto.StockMinimo;
            //productoInDb.Stock = producto.Stock;
            //productoInDb.StockMinimo = producto.StockMinimo;
            //productoInDb.PrecioUnitario = producto.PrecioUnitario;
            productoInDb.Imagen = producto.Imagen;
            _context.Entry(productoInDb).State = EntityState.Modified;

        }


        public bool EstaRelacionado(Producto producto)
        {
            return false;
        }


        public bool Existe(Producto producto)
        {
            if (producto.ProductoId == 0)
            {
                return _context.Productos.Any(p => p.NombreProducto == producto.NombreProducto &&
                p.CategoriaId == producto.CategoriaId);
            }
            return _context.Productos.Any(p => p.NombreProducto == producto.NombreProducto &&
                p.CategoriaId == producto.CategoriaId && p.ProductoId != producto.ProductoId);

        }


        public List<ProductoListDto> Filtrar(Func<Producto, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(Func<Producto, bool> predicado)
        {
            return _context.Productos.Count(predicado);
        }

        public int GetCantidad()
        {
            return _context.Productos.Count();
        }

        public Producto GetProductoPorId(int id)
        {
            return _context.Productos.Include(p => p.Categoria)
                .Include(p => p.Proveedor).SingleOrDefault(p => p.ProductoId == id);
        }

        public List<ProductoListDto> GetProductos()
        {
            return _context.Productos.Include(p => p.Categoria)
                .Select(p => new ProductoListDto()
                {
                    ProductoId = p.ProductoId,
                    NombreProducto = p.NombreProducto,
                    Categoria = p.Categoria.NombreCategoria,
                    //PrecioUnitario = p.PrecioUnitario,
                    //UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Imagen = p.Imagen

                }).ToList();
        }

        public List<ProductoListDto> GetProductos(int categoriaId)
        {
            return _context.Productos.Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId)
                .Select(p => new ProductoListDto()
                {
                    ProductoId = p.ProductoId,
                    NombreProducto = p.NombreProducto,
                    Categoria = p.Categoria.NombreCategoria,
                    //PrecioUnitario = p.PrecioUnitario,
                    //UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Imagen = p.Imagen

                }).ToList();

        }

        public List<SelectListItem> GetProductosDropDown()
        {
            var lista = GetProductos();
            var dropDown = lista.Select(p => new SelectListItem()
            {
                Text = p.NombreProducto,
                Value = p.ProductoId.ToString()
            }).ToList();
            return dropDown;
        }

        public List<ProductoListDto> GetProductosPorProveedorId(int id)
        {
            return _context.Productos.Include(p => p.Proveedor)
                .Where(p => p.ProveedorId == id)
                .Select(p => new ProductoListDto()
                {
                    ProductoId = p.ProductoId,
                    NombreProducto = p.NombreProducto,
                    Categoria = p.Categoria.NombreCategoria,
                    //PrecioUnitario = p.PrecioUnitario,
                    //UnidadesDisponibles = p.Stock - p.UnidadesEnPedido,
                    Imagen = p.Imagen

                }).ToList();

        }
    }
}
