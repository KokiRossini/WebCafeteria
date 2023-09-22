using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class DetalleVentaEntityTypeConfigurations:EntityTypeConfiguration<DetalleVenta>
    {
        public DetalleVentaEntityTypeConfigurations()
        {
            ToTable("DetalleVentas");
        }
    }
}
