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
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.Repositorios
{
    public class RepositorioTamaños : IRepositorioTamaños
    {
        private readonly CafeteriaDbContext _context;

        public RepositorioTamaños(CafeteriaDbContext context)
        {
            _context = context;
        }

        public void Agregar(Tamaño tamaño)
        {
            try
            {
                _context.Tamaños.Add(tamaño);
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
                var tamañoInDb = GetTamañoPorId(id);
                if (tamañoInDb == null)
                {
                    throw new Exception("Registro borrado por otro usuario");
                }
                _context.Entry(tamañoInDb).State = EntityState.Deleted;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(Tamaño tamaño)
        {
            try
            {
                //var tamañoInDb = GetTamañoPorId(tamaño.TamañoId);
                //if (tamañoInDb == null)
                //{
                //    throw new Exception("Registro borrado por otro usuario");
                //}
                _context.Entry(tamaño).State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Tamaño tamaño)
        {
            try
            {
                return _context.TamañosProductos.Any(t => t.TamañoId == tamaño.TamañoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Tamaño tamaño)
        {
            try
            {
                if (tamaño.TamañoId == 0)
                {
                    return _context.Tamaños.Any(t => t.NombreTamaño == tamaño.NombreTamaño);
                }
                return _context.Tamaños.Any(t => t.NombreTamaño == tamaño.NombreTamaño && t.TamañoId != tamaño.TamañoId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tamaño> Filtrar(Func<Tamaño, bool> predicado)
        {
            return _context.Tamaños
                .Where(predicado)
                .Select(t => new Tamaño
                {
                    TamañoId = t.TamañoId,
                    NombreTamaño = t.NombreTamaño
                }).ToList();
        }

        public Tamaño GetTamañoPorId(int id)
        {
            try
            {
                return _context.Tamaños
                    .SingleOrDefault(t => t.TamañoId == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tamaño> GetTamaños()
        {
            return _context.Tamaños.ToList();
        }

        public List<SelectListItem> GetTamañosDropDown()
        {
            var lista = GetTamaños();
            var dropDown = lista.Select(t => new SelectListItem()
            {
                Text = t.NombreTamaño,
                Value = t.TamañoId.ToString()
            }).ToList();
            return dropDown;
        }
    }
}
