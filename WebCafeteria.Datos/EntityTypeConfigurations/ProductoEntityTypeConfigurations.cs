﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades;

namespace WebCafeteria.Datos.EntityTypeConfigurations
{
    public class ProductoEntityTypeConfigurations:EntityTypeConfiguration<Producto>
    {
        public ProductoEntityTypeConfigurations()
        {
            ToTable("Productos");
        }
    }
}
