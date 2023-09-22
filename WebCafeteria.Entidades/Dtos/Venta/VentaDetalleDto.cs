using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades.Dtos.Venta
{
    public class VentaDetalleDto
    {
        public VentaListDto venta { get; set; }
        public List<DetalleVentaListDto> detalleVenta { get; set; }
    }
}
