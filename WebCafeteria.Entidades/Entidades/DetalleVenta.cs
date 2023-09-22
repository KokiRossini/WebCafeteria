using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Entidades
{
    public class DetalleVenta
    {
        public int DetalleVentaId { get; set; }
        public int VentaId { get; set; }
        public int TamañoProductoId { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }

        public TamañoProducto TamañoProducto { get; set; }

    }
}
