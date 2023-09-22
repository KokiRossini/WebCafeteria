using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos;
using System.Data.Entity;

namespace WebCafeteria.Datos.Repositorios
{
    public class RepositorioDetalleVentas : IRepositorioDetalleVentas
    {
        private readonly CafeteriaDbContext _context;
        public RepositorioDetalleVentas(CafeteriaDbContext context)
        {
            _context = context;
        }
        public void Agregar(DetalleVenta detalle)
        {
            _context.DetalleVentas.Add(detalle);
        }

        public void Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public void Editar(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public bool EstaRelacionado(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public bool Existe(DetalleVenta detalle)
        {
            throw new NotImplementedException();
        }

        public DetalleVenta GetDetalleVentaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetalleVentaListDto> GetDetalleVentas(int ventaId)
        {
            //throw new NotImplementedException();
            ////TODO:No me toma el Include
            ///
            return _context.DetalleVentas.Include(d => d.TamañoProducto)
                .Where(d => d.VentaId == ventaId)
                .Select(d => new DetalleVentaListDto()
                {
                    DetalleVentaId = d.DetalleVentaId,
                    Producto = d.TamañoProducto.producto.NombreProducto,
                    Tamaño=d.TamañoProducto.tamaño.NombreTamaño,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,

                }).ToList();
            //return _context.DetalleVentas
            //    .Include(d=>d.Producto)
            //    .Include(d => d.TamañoProducto)
            //    .Where(d => d.VentaId == ventaId)
            //    .Select(d => new DetalleVentaListDto()
            //    {
            //        DetalleVentaId = d.DetalleVentaId,
            //        Producto = d.Producto.NombreProducto,
            //        TamañoProducto=d.TamañoProducto.tamaño.NombreTamaño,
            //        Cantidad = d.Cantidad,
            //        PrecioUnitario = d.PrecioUnitario,

            //    }).ToList();
        }
    }
}
