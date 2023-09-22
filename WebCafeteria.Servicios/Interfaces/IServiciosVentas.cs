using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos.Venta;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;

namespace WebCafeteria.Servicios.Interfaces
{
    public interface IServiciosVentas
    {
        int GetCantidad();
        List<DetalleVentaListDto> GetDetalleVenta(int ventaId);
        VentaListDto GetVentaPorId(int id);
        List<VentaListDto> GetVentas();
        void Guardar(Venta venta, string name);
    }
}
