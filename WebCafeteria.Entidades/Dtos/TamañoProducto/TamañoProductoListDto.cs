using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades.Dtos.TamañoProducto
{
    public class TamañoProductoListDto
    {
        public int TamañoProductoId { get; set; }
        public string NombreTamaño { get; set; }
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int UnidadesDisponibles { get; set; }
        public string Imagen { get; set; }
        public bool Suspendido { get; set; }


    }
}
