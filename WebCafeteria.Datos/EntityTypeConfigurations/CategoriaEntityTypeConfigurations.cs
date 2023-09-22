using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class CategoriaEntityTypeConfigurations:EntityTypeConfiguration<Categoria>
    {
        public CategoriaEntityTypeConfigurations()
        {
            ToTable("Categorias");
            Property(c => c.NombreCategoria).HasColumnName("Categoria");

        }

    }
}
