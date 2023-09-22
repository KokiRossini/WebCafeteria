using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Entidades.Dtos.Cliente;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Servicios.Interfaces;

namespace WebCafeteria.Servicios.Servicios
{
    public class ServiciosClientes:IServiciosClientes
    {
        private readonly IRepositorioClientes _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosClientes(IRepositorioClientes repositorio, IUnitOfWork unitOfWork)
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

        public bool EstaRelacionado(Cliente cliente)
        {
            try
            {
                return _repositorio.EstaRelacionado(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Cliente cliente)
        {
            try
            {
                return _repositorio.Existe(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado)
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

        public Cliente GetClientePorEmail(string mail)
        {
            try
            {
                return _repositorio.GetClientePorEmail(mail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cliente GetClientePorId(int id)
        {
            try
            {
                return _repositorio.GetClientePorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> GetClientes()
        {
            try
            {
                return _repositorio.GetClientes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> GetClientes(int paisId, int ciudadId)
        {
            try
            {
                return _repositorio.GetClientes(paisId, ciudadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Cliente cliente)
        {
            try
            {
                if (cliente.Id == 0)
                {
                    _repositorio.Agregar(cliente);
                }
                else
                {
                    _repositorio.Editar(cliente);
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
