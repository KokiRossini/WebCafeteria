using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Entidades.Dtos.TamañoProducto;

namespace WebCafeteria.Datos.Interfaces
{
    public interface IRepositorioTamañosProductos
    {
        void Agregar(TamañoProducto tamañoProducto);
        void Borrar(int id);
        void Editar(TamañoProducto tamañoProducto);
        bool EstaRelacionado(int id);
        bool Existe(TamañoProducto tamañoProducto);
        TamañoProducto GetTamañoProductoPorId(int id);
        List<TamañoProductoListDto> GetTamañosProductos(bool todos);
        List<TamañoProductoListDto> GetTamañosProductos(int tamañoId, int productoId);
        List<TamañoProductoListDto> Filtrar(Func<TamañoProducto, bool> predicado);
        int GetCantidadProductos(Func<TamañoProducto, bool> predicado);
        List<TamañoProductoListDto> GetProductosPorTamaño(int tamañoId);
        int GetCantidadTamaños(Func<TamañoProducto, bool> predicado);
        List<TamañoProductoListDto> GetTamañosPorProducto(int productoId);
        void ActualizarUnidadesEnPedido(int id, int cantidad);
        void ActualizarStock(int id, int cantidad);

    }
}
