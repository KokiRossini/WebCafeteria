using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class ProveedorEntityTypeConfigurations:EntityTypeConfiguration<Proveedor>
    {
        public ProveedorEntityTypeConfigurations()
        {
            ToTable("Proveedores");
            Property(p => p.Id).HasColumnName("ProveedorId");
            Property(p => p.Nombre).HasColumnName("RazonSocial");
        }
    }
}
