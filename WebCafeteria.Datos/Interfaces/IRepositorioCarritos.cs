using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.Interfaces
{
    public interface IRepositorioCarritos
    {
        List<ItemCarrito> GetCarrito(string user);
        void Guardar(ItemCarrito carritoTemp);
        ItemCarrito GetItem(string user, int tamañoProductoId);
        int GetCantidad(string user);
        void Borrar(string user, int tamañoProductoId);

    }
}
