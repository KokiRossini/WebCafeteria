using System.Collections.Generic;
using System.Linq;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Web.Models.Carrito
{
    public class Carrito
    {
        public List<ItemCarrito> Items { get; set; }
        public Carrito()
        {
            Items = new List<ItemCarrito>();
        }
        public int GetCantidad() => Items.Sum(i => i.Cantidad);

        public void AddToCart(ItemCarrito itemCarrito)
        {
            var itemInCarrito = Items
                .SingleOrDefault(i => i.TamañoProductoId == itemCarrito.TamañoProductoId);
            if (itemInCarrito == null)
            {
                Items.Add(itemCarrito);

            }
            else
            {
                itemInCarrito.Cantidad += itemCarrito.Cantidad;
            }
        }
        public List<ItemCarrito> GetItems() => Items;

        public void Clear()
        {
            Items.Clear();
        }

        public void RemoveFromCart(int tamañoProductoId)
        {
            var itemInCarrito = Items
                .SingleOrDefault(i => i.TamañoProductoId == tamañoProductoId);
            Items.Remove(itemInCarrito);
        }

        public decimal GetTotal()
        {
            return Items.Sum(i => i.PrecioTotal);
        }
    }
}
