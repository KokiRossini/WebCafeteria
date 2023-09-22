using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class PaisEntityTypeConfigurations:EntityTypeConfiguration<Pais>
    {
        public PaisEntityTypeConfigurations()
        {
            ToTable("Paises");
            Property(p => p.NombrePais).HasColumnName("Pais");

        }
    }
}
