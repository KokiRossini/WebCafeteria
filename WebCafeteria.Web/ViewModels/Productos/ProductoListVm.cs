using System.ComponentModel;

namespace WebCafeteria.Web.ViewModels.Productos
{
    public class ProductoListVm
    {
        public int ProductoId { get; set; }
        [DisplayName("Producto")]
        public string NombreProducto { get; set; }
        public string Categoria { get; set; }
        [DisplayName("Cant. Tamaños")]
        public int CantidadTamañosProductos { get; set; }
        [DisplayName("Imágen")]
        public string Imagen { get; set; }


    }
}