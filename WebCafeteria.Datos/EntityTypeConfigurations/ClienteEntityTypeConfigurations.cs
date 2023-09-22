using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class ClienteEntityTypeConfigurations:EntityTypeConfiguration<Cliente>
    {
        public ClienteEntityTypeConfigurations()
        {
            ToTable("Clientes");
            Property(c => c.Id).HasColumnName("ClienteId");
            Property(c => c.Nombre).HasColumnName("NombreCliente");
            Property(c => c.TelefonoFijo).HasColumnName("TelFijo");
            Property(c => c.TelefonoMovil).HasColumnName("TelMovil");
    }

    }
}
