using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int? ProveedorId { get; set; }
        public int CategoriaId { get; set; }
        public string Imagen { get; set; }
        public Proveedor Proveedor { get; set; }
        public Categoria Categoria { get; set; }

    }
}
