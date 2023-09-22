using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos.Repositorios;
using WebCafeteria.Datos;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;
using WebCafeteria.Servicios.Interfaces;
using System.Web.Mvc;
using WebCafeteria.Datos.Interfaces;

namespace WebCafeteria.Servicios.Servicios
{
    public class ServiciosProductos:IServiciosProductos
    {
        private readonly IRepositorioProductos _repositorio;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosProductos(IRepositorioProductos repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        //public void ActualizarUnidadesEnPedido(int productoId, int cantidad)
        //{
        //    try
        //    {
        //        _repositorio.ActualizarUnidadesEnPedido(productoId, cantidad);
        //        _unitOfWork.SaveChanges();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

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

        public bool EstaRelacionado(Producto producto)
        {
            try
            {
                return _repositorio.EstaRelacionado(producto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Producto producto)
        {
            try
            {
                return _repositorio.Existe(producto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductoListDto> Filtrar(Func<Producto, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> GetProductosDropDown()
        {
            try
            {
                return _repositorio.GetProductosDropDown();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Producto GetProductoPorId(int id)
        {
            try
            {
                return _repositorio.GetProductoPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductoListDto> GetProductos()
        {
            try
            {
                return _repositorio.GetProductos();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ProductoListDto> GetProductos(int id)
        {
            try
            {
                return _repositorio.GetProductos(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Producto producto)
        {
            try
            {
                if (producto.ProductoId == 0)
                {
                    _repositorio.Agregar(producto);
                }
                else
                {
                    _repositorio.Editar(producto);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Producto, bool> predicado)
        {
            try
            {
                return _repositorio.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductoListDto> GetProductosPorProveedorId(int id)
        {
            try
            {
                return _repositorio.GetProductosPorProveedorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public List<ProductoListDto> GetProductosTamaño(int tamañoId)
        //{
        //    try
        //    {
        //        return _repositorio.GetProductosTamaño(tamañoId);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
