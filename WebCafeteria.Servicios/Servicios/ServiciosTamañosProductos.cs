using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Datos;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Dtos.TamañoProducto;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Servicios.Interfaces;
using WebCafeteria.Entidades.Dtos;

namespace WebCafeteria.Servicios.Servicios
{
    public class ServiciosTamañosProductos : IServiciosTamañosProductos
    {
        private readonly IRepositorioTamañosProductos _repositorio;
        private readonly IUnitOfWork _unitOfWork;
        public ServiciosTamañosProductos(IRepositorioTamañosProductos repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public void ActualizarUnidadesEnPedido(int id, int cantidad)
        {
            try
            {
                _repositorio.ActualizarUnidadesEnPedido(id, cantidad);
                _unitOfWork.SaveChanges();
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
                _repositorio.Borrar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(int id)
        {
            try
            {
                return _repositorio.EstaRelacionado(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TamañoProducto tamañoProducto)
        {
            try
            {
                return _repositorio.Existe(tamañoProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TamañoProductoListDto> Filtrar(Func<TamañoProducto, bool> predicado)
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

        public int GetCantidadProductos(Func<TamañoProducto, bool> predicado)
        {
            try
            {
                return _repositorio.GetCantidadProductos(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidadTamaños(Func<TamañoProducto, bool> predicado)
        {
            try
            {
                return _repositorio.GetCantidadTamaños(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TamañoProductoListDto> GetProductosPorTamaño(int tamañoId)
        {
            try
            {
                return _repositorio.GetProductosPorTamaño(tamañoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TamañoProducto GetTamañoProductoPorId(int id)
        {
            try
            {
                return _repositorio.GetTamañoProductoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TamañoProductoListDto> GetTamañosPorProducto(int productoId)
        {
            try
            {
                return _repositorio.GetTamañosPorProducto(productoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TamañoProductoListDto> GetTamañosProductos(bool todos)
        {
            try
            {
                return _repositorio.GetTamañosProductos(todos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TamañoProductoListDto> GetTamañosProductos(int productoId, int tamañoId)
        {
            try
            {
                return _repositorio.GetTamañosProductos(productoId, tamañoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(TamañoProducto tamañoProducto)
        {
            try
            {
                if (tamañoProducto.TamañoProductoId == 0)
                {
                    _repositorio.Agregar(tamañoProducto);
                }
                else
                {
                    _repositorio.Editar(tamañoProducto);
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
