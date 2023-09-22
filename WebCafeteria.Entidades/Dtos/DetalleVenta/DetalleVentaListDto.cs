using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades.Dtos
{
    public class DetalleVentaListDto
    {
        public int DetalleVentaId { get; set; }
        public string Producto { get; set; }
        public string Tamaño { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
