using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.Repositorios
{
    public class RepositorioCarritos : IRepositorioCarritos
    {
        private readonly CafeteriaDbContext _context;

        public RepositorioCarritos(CafeteriaDbContext context)
        {
            _context = context;
        }

        public void Guardar(ItemCarrito carritoTemp)
        {
            var itemInDb = GetItem(carritoTemp.UserName,
                carritoTemp.TamañoProductoId);

            if (itemInDb != null)
            {
                itemInDb.Cantidad += carritoTemp.Cantidad;
                _context.Entry(itemInDb).State = EntityState.Modified;

            }
            else
            {
                _context.Carrito.Add(carritoTemp);

            }

        }


        public void Borrar(string user, int tamañoProductoId)
        {
            var itemInDb = GetItem(user, tamañoProductoId);
            if (itemInDb != null)
            {
                _context.Entry(itemInDb).State = EntityState.Deleted;
            }
        }


        public int GetCantidad(string user)
        {
            return _context.Carrito.Count(c => c.UserName == user);
        }

        public List<ItemCarrito> GetCarrito(string user)
        {
            return _context.Carrito
               .Where(c => c.UserName == user).ToList();
        }

        public ItemCarrito GetItem(string user, int tamañoProductoId)
        {
            return _context.Carrito
                        .SingleOrDefault(i => i.UserName == user && i.TamañoProductoId == tamañoProductoId);

        }
    }
}
