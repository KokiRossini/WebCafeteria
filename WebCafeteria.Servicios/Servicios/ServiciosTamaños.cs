using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Datos;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Servicios.Interfaces;
using System.Web.Mvc;

namespace WebCafeteria.Servicios.Servicios
{
    public class ServiciosTamaños : IServiciosTamaños
    {
        private readonly IRepositorioTamaños _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosTamaños(IRepositorioTamaños repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int id)
        {
            try
            {
                _repositorio.Borrar(id);
                _unitOfWork.SaveChanges();
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
                return _repositorio.EstaRelacionado(tamaño);
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
                return _repositorio.Existe(tamaño);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tamaño> Filtrar(Func<Tamaño, bool> predicado)
        {
            try
            {
                return _repositorio.Filtrar(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Tamaño GetTamañoPorId(int id)
        {
            try
            {
                return _repositorio.GetTamañoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tamaño> GetTamaños()
        {
            try
            {
                return _repositorio.GetTamaños();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetTamañosDropDown()
        {
            try
            {
                return _repositorio.GetTamañosDropDown();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Tamaño tamaño)
        {
            try
            {
                if (tamaño.TamañoId == 0)
                {
                    _repositorio.Agregar(tamaño);
                }
                else
                {
                    _repositorio.Editar(tamaño);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
