using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;
using System.Web.Mvc;

namespace WebCafeteria.Datos.Interfaces
{
    public interface IRepositorioProductos
    {
        void Agregar(Producto producto);
        void Borrar(int id);
        void Editar(Producto producto);
        bool EstaRelacionado(Producto producto);
        bool Existe(Producto producto);
        Producto GetProductoPorId(int id);
        List<ProductoListDto> GetProductos();
        List<ProductoListDto> GetProductos(int id);
        List<ProductoListDto> Filtrar(Func<Producto, bool> predicado);
        int GetCantidad(Func<Producto, bool> predicado);
        List<SelectListItem> GetProductosDropDown();
        int GetCantidad();
        List<ProductoListDto> GetProductosPorProveedorId(int id);
        //List<ProductoListDto> GetProductosTamaño(int tamañoId);
        //void ActualizarStock(int productoId, int cantidad);
    }
}
