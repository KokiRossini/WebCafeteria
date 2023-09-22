using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos.Venta;
using System.Data.Entity;

namespace WebCafeteria.Datos.Repositorios
{
    public class RepositorioVentas : IRepositorioVentas
    {
        private readonly CafeteriaDbContext _context;

        public RepositorioVentas(CafeteriaDbContext context)
        {
            _context = context;
        }

        public void Agregar(Venta venta)
        {
            _context.Ventas.Add(venta);
        }

        public List<VentaListDto> Filtrar(Func<Venta, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad()
        {
            return _context.Ventas.Count();
        }
        //TODO: Reparar el include con clientes
        public VentaListDto GetVentaPorId(int id)
        {
            ////        var ventas = _context.Ventas
            ////.OrderBy(v => v.FechaVenta)
            ////.ToList(); // Cargar ventas sin cargar Cliente de inmediato

            ////        var ventasListDto = ventas.Select(v => new VentaListDto
            ////        {
            ////            VentaId = v.VentaId,
            ////            FechaVenta = v.FechaVenta,
            ////            Cliente = v.Cliente.Nombre,
            ////            Total = v.Total,
            ////        }).SingleOrDefault(v=>v.VentaId==id);

            ////        return ventasListDto;

            return _context.Ventas
                .Include(v => v.Cliente)
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    FechaVenta = v.FechaVenta,
                    Cliente = v.Cliente.Nombre,
                    Total = v.Total,
                }).SingleOrDefault(v => v.VentaId == id);

        }
        ////public List<VentaListDto> GetVentas()
        ////{
        ////    var ventas = _context.Ventas
        ////        .OrderBy(v => v.FechaVenta)
        ////        .ToList(); // Cargar ventas sin cargar Cliente de inmediato
        ////    var clientes= _context.Clientes.ToList();

        ////    var ventasListDto = ventas.Select(v => new VentaListDto
        ////    {
        ////        VentaId = v.VentaId,
        ////        FechaVenta = v.FechaVenta,
        ////        Cliente = v.Cliente.Nombre,
        ////        Total = v.Total,
        ////    }).ToList();

        ////    return ventasListDto;
        ////}

        public List<VentaListDto> GetVentas()
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .OrderBy(v => v.FechaVenta)
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    FechaVenta = v.FechaVenta,
                    Cliente = v.Cliente.Nombre,
                    Total = v.Total,
                }).ToList();
        }
    }
}
