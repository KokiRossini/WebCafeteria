using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Entidades.Dtos.TamañoProducto;

namespace WebCafeteria.Servicios.Interfaces
{
    public interface IServiciosTamañosProductos
    {
        void Borrar(int id);
        bool EstaRelacionado(int id);
        bool Existe(TamañoProducto tamañoProducto);
        void Guardar(TamañoProducto tamañoProducto);
        TamañoProducto GetTamañoProductoPorId(int id);
        List<TamañoProductoListDto> GetTamañosProductos(bool todos);
        List<TamañoProductoListDto> GetTamañosProductos(int productoId, int tamañoId);
        List<TamañoProductoListDto> Filtrar(Func<TamañoProducto, bool> predicado);
        int GetCantidadProductos(Func<TamañoProducto, bool> predicado);
        List<TamañoProductoListDto> GetProductosPorTamaño(int tamañoId);
        int GetCantidadTamaños(Func<TamañoProducto, bool> value);
        List<TamañoProductoListDto> GetTamañosPorProducto(int productoId);
        void ActualizarUnidadesEnPedido(int id, int cantidad);

    }
}
