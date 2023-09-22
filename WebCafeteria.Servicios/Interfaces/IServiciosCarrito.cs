using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Servicios.Interfaces
{
    public interface IServiciosCarrito
    {
        List<ItemCarrito> GetCarrito(string user);
        void Guardar(ItemCarrito carritoTemp);
        void Borrar(string user, int tamañoProductoId);
        ItemCarrito GetItem(string user, int tamañoProductoId);
        int GetCantidad(string user);

    }
}
