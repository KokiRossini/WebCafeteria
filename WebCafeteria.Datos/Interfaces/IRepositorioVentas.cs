using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos.Venta;
using WebCafeteria.Entidades;

namespace WebCafeteria.Datos.Interfaces
{
    public interface IRepositorioVentas
    {
        List<VentaListDto> GetVentas();
        void Agregar(Venta venta);
        List<VentaListDto> Filtrar(Func<Venta, bool> predicado);
        VentaListDto GetVentaPorId(int id);
        int GetCantidad();
    }
}
