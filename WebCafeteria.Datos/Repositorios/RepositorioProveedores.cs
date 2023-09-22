using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos;

namespace WebCafeteria.Datos.Repositorios
{
    public class RepositorioProveedores : IRepositorioProveedores
    {
        private readonly CafeteriaDbContext _context;

        public RepositorioProveedores(CafeteriaDbContext context)
        {
            _context = context;
        }

        public void Agregar(Proveedor proveedor)
        {
            try
            {
                _context.Proveedores.Add(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int id)
        {
            try
            {
                var proveedorInDb = GetProveedorPorId(id);
                if (proveedorInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(proveedorInDb).State = EntityState.Deleted;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(Proveedor proveedor)
        {
            try
            {
                var proveedorInDb = GetProveedorPorId(proveedor.Id);
                if (proveedorInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                proveedorInDb.Nombre = proveedor.Nombre;
                proveedorInDb.Direccion = proveedor.Direccion;
                proveedorInDb.CiudadId = proveedor.CiudadId;
                proveedorInDb.CodPostal = proveedor.CodPostal;
                proveedorInDb.PaisId = proveedor.PaisId;
                _context.Entry(proveedorInDb).State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Proveedor proveedor)
        {
            try
            {
                return _context.Productos.Any(p => p.ProveedorId == proveedor.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                if (proveedor.Id == 0)
                {
                    return _context.Proveedores.Any(c => c.Nombre == proveedor.Nombre);
                }
                return _context.Proveedores.Any(c => c.Nombre == proveedor.Nombre && c.Id != proveedor.Id);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado)
        {
            return _context.Proveedores.Include(c => c.Pais)
                .Include(c => c.Ciudad)
                .Where(predicado)
                .Select(c => new ProveedorListDto
                {
                    ProveedorId = c.Id,
                    NombreProveedor = c.Nombre,
                    Pais = c.Pais.NombrePais,
                    Ciudad = c.Ciudad.NombreCiudad
                }).ToList();
        }

        public Proveedor GetProveedorPorId(int id)
        {
            try
            {
                return _context.Proveedores.Include(c => c.Pais)
                    .Include(c => c.Ciudad)
                    .SingleOrDefault(c => c.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> GetProveedores()
        {
            return _context.Proveedores.Include(c => c.Pais)
                .Include(c => c.Ciudad).Select(c => new ProveedorListDto
                {
                    ProveedorId = c.Id,
                    NombreProveedor = c.Nombre,
                    Pais = c.Pais.NombrePais,
                    Ciudad = c.Ciudad.NombreCiudad
                }).ToList();
        }

        public List<ProveedorListDto> GetProveedores(int paisId, int ciudadId)
        {
            try
            {
                return _context.Proveedores.Include(c => c.Pais)
                    .Include(c => c.Ciudad)
                    .Where(c => c.PaisId == paisId && c.CiudadId == ciudadId)
                    .Select(c => new ProveedorListDto
                    {
                        ProveedorId = c.Id,
                        NombreProveedor = c.Nombre,
                        Pais = c.Pais.NombrePais,
                        Ciudad = c.Ciudad.NombreCiudad
                    }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SelectListItem> GetProveedoresDropDownList()
        {
            var lista = GetProveedores();
            var dropDownProveedores = lista.Select(p => new SelectListItem()
            {
                Text = p.NombreProveedor,
                Value = p.ProveedorId.ToString()
            }).ToList();
            return dropDownProveedores;
        }
    }
}
