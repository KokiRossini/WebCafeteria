using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class TamañosProductosEntityTypeConfigurations:EntityTypeConfiguration<TamañoProducto>
    {
        public TamañosProductosEntityTypeConfigurations()
        {
            ToTable("TamañosProductos");
            Property(c => c.Stock).HasColumnName("Stock");

        }
    }
}
