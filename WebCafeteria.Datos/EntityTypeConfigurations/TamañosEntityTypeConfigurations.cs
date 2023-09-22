using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class TamañosEntityTypeConfigurations:EntityTypeConfiguration<Tamaño>
    {
        public TamañosEntityTypeConfigurations()
        {
            ToTable("Tamaños");
            Property(c => c.NombreTamaño).HasColumnName("Tamaño");

        }
    }
}
