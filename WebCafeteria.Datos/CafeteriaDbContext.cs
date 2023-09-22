using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos
{
    public class CafeteriaDbContext:DbContext
    {
        public CafeteriaDbContext()
        {
            
        }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Tamaño> Tamaños { get; set; }
        public DbSet<TamañoProducto> TamañosProductos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ItemCarrito> Carrito { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CafeteriaDbContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
