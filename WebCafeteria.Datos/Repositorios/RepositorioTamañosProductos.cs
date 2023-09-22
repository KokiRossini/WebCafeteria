using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos.TamañoProducto;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.Repositorios
{
    public class RepositorioTamañosProductos : IRepositorioTamañosProductos
    {
        private readonly CafeteriaDbContext _context;

        public RepositorioTamañosProductos(CafeteriaDbContext context)
        {
            _context = context;
        }

        public void ActualizarStock(int id, int cantidad)
        {
            var tamañoProductoInDb = _context.TamañosProductos.SingleOrDefault(p => p.TamañoProductoId == id);
            tamañoProductoInDb.UnidadesEnPedido -= cantidad;
            tamañoProductoInDb.Stock -= cantidad;
            _context.Entry(tamañoProductoInDb).State = EntityState.Modified;
        }

        public void ActualizarUnidadesEnPedido(int id, int cantidad)
        {
            var tamañoProductoInDb = _context.TamañosProductos.SingleOrDefault(p => p.TamañoProductoId == id);
            tamañoProductoInDb.UnidadesEnPedido += cantidad;
            _context.Entry(tamañoProductoInDb).State = EntityState.Modified;
        }

        public void Agregar(TamañoProducto tamañoProducto)
        {
            try
            {
                _context.TamañosProductos.Add(tamañoProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int id)
        {
            try
            {
                var tamañoProductoInDb = GetTamañoProductoPorId(id);
                if (tamañoProductoInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(tamañoProductoInDb).State = EntityState.Deleted;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(TamañoProducto tamañoProducto)
        {

            try
            {
                var tamañoProductoInDb = GetTamañoProductoPorId(tamañoProducto.TamañoProductoId);
                if (tamañoProductoInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                //tamañoProductoInDb.producto = null;
                tamañoProductoInDb.ProductoId = tamañoProducto.ProductoId;
                tamañoProductoInDb.Stock = tamañoProducto.Stock;
                tamañoProductoInDb.TamañoId = tamañoProducto.TamañoId;
                tamañoProductoInDb.PrecioUnitario = tamañoProducto.PrecioUnitario;
                tamañoProductoInDb.Suspendido = tamañoProducto.Suspendido;

                _context.Entry(tamañoProductoInDb).State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool EstaRelacionado(int id)
        {
            try
            {
                return _context.DetalleVentas.Any(dv => dv.TamañoProductoId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TamañoProducto tamañoProducto)
        {
            try
            {
                if (tamañoProducto.TamañoProductoId == 0)
                {
                    return _context.TamañosProductos.Any(tp => tp.ProductoId == tamañoProducto.ProductoId && tp.TamañoId == tamañoProducto.TamañoId );
                }
                return _context.TamañosProductos.Any(tp => (tp.ProductoId == tamañoProducto.ProductoId && tp.TamañoId == tamañoProducto.TamañoId) && tp.PrecioUnitario == tamañoProducto.PrecioUnitario && tp.Stock == tamañoProducto.Stock && tp.Suspendido==tamañoProducto.Suspendido);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TamañoProductoListDto> Filtrar(Func<TamañoProducto, bool> predicado)
        {
            return _context.TamañosProductos.Include(tp => tp.producto)
                .Include(tp => tp.tamaño)
                .Where(predicado)
                .Select(c => new TamañoProductoListDto
                {
                    TamañoProductoId = c.TamañoProductoId,
                    NombreTamaño = c.tamaño.NombreTamaño,
                    NombreProducto = c.producto.NombreProducto,
                    Stock = c.Stock - c.UnidadesEnPedido,
                    PrecioUnitario = c.PrecioUnitario,
                    Imagen=c.producto.Imagen
                    
                    

                }).ToList();
        }

        public int GetCantidadProductos(Func<TamañoProducto, bool> predicado)
        {
            return _context.TamañosProductos.Count(predicado);
        }

        public int GetCantidadTamaños(Func<TamañoProducto, bool> predicado)
        {
            return _context.TamañosProductos.Count(predicado);
        }

        public List<TamañoProductoListDto> GetProductosPorTamaño(int tamañoId)
        {
            return _context.TamañosProductos
                //.Include(tp => tp.tamaño)
                .Include(tp => tp.producto)
                .Where(tp=>tp.TamañoId==tamañoId)
                .Select(c => new TamañoProductoListDto
                {
                    TamañoProductoId = c.TamañoProductoId,
                    //NombreTamaño = c.tamaño.NombreTamaño,
                    NombreProducto = c.producto.NombreProducto,
                    //Stock = c.Stock,
                    PrecioUnitario = c.PrecioUnitario,
                    //Suspendido=c.Suspendido

                }).OrderBy(p => p.NombreProducto).ToList();
        }

        public TamañoProducto GetTamañoProductoPorId(int id)
        {
            try
            {
                return _context.TamañosProductos.Include(tp => tp.producto)
                .Include(tp => tp.tamaño)
                    .SingleOrDefault(tp => tp.TamañoProductoId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TamañoProductoListDto> GetTamañosPorProducto(int productoId)
        {
            return _context.TamañosProductos
                .Include(tp => tp.tamaño)
                //.Include(tp => tp.producto)
                .Where(tp => tp.ProductoId == productoId)
                .Select(c => new TamañoProductoListDto
                {
                    TamañoProductoId = c.TamañoProductoId,
                    NombreTamaño = c.tamaño.NombreTamaño,
                    //NombreProducto = c.producto.NombreProducto,
                    Stock = c.Stock,
                    //Suspendido = c.Suspendido
                    //PrecioUnitario = c.PrecioUnitario
                }).OrderBy(p => p.NombreTamaño).ToList();
        }

        public List<TamañoProductoListDto> GetTamañosProductos(bool todos)
        {
            IQueryable<TamañoProductoListDto> query = _context.TamañosProductos.Include(tp => tp.tamaño)
                .Include(tp => tp.producto)
                .Select(c => new TamañoProductoListDto
                {
                    TamañoProductoId = c.TamañoProductoId,
                    NombreTamaño = c.tamaño.NombreTamaño,
                    NombreProducto = c.producto.NombreProducto,
                    Stock = c.Stock - c.UnidadesEnPedido,
                    PrecioUnitario = c.PrecioUnitario,
                    Suspendido = c.Suspendido,
                    Imagen = c.producto.Imagen
                });
            if (!todos)
            {
                query = query.Where(p=>p.Stock > 0 && p.Suspendido == false);
            }
            return query.ToList();
            //return _context.TamañosProductos.Include(tp => tp.tamaño)
            //    .Include(tp => tp.producto)
            //    .Select(c => new TamañoProductoListDto
            //    {
            //        TamañoProductoId = c.TamañoProductoId,
            //        NombreTamaño = c.tamaño.NombreTamaño,
            //        NombreProducto = c.producto.NombreProducto,
            //        Stock = c.Stock - c.UnidadesEnPedido,
            //        PrecioUnitario = c.PrecioUnitario,
            //        Suspendido = c.Suspendido,
            //        Imagen = c.producto.Imagen
            //    }).OrderBy(p => p.NombreTamaño).ToList();

        }

        public List<TamañoProductoListDto> GetTamañosProductos(int tamañoId, int productoId)
        {
            try
            {
                return _context.TamañosProductos.Include(tp => tp.tamaño)
                .Include(tp => tp.producto)
                    .Where(tp => tp.TamañoId == tamañoId && tp.ProductoId == productoId)
                    .Select(c => new TamañoProductoListDto
                    {
                        TamañoProductoId = c.TamañoProductoId,
                        NombreTamaño = c.tamaño.NombreTamaño,
                        NombreProducto = c.producto.NombreProducto,
                        Stock=c.Stock-c.UnidadesEnPedido,
                        PrecioUnitario = c.PrecioUnitario,
                        Imagen= c.producto.Imagen
                        
                    }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
