using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Datos.Interfaces;
using WebCafeteria.Datos;
using WebCafeteria.Entidades.Dtos.Venta;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;
using WebCafeteria.Servicios.Interfaces;
using System.Transactions;
using System.Data;

namespace WebCafeteria.Servicios.Servicios
{
    public class ServiciosVentas:IServiciosVentas
    {
        private readonly IRepositorioVentas _repositorio;
        private readonly IRepositorioDetalleVentas _repoDetalleVentas;
        private readonly IRepositorioTamañosProductos _repoTamañosProductos;
        private readonly IRepositorioCarritos _repoCarritos;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosVentas(IRepositorioVentas repositorio,
            IRepositorioDetalleVentas repoDetalleVentas,
            IRepositorioTamañosProductos repoTamañosProductos,
            IRepositorioCarritos repoCarritos,
            IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _repoDetalleVentas = repoDetalleVentas;
            _repoTamañosProductos = repoTamañosProductos;
            _repoCarritos = repoCarritos;
            _unitOfWork = unitOfWork;
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

        public List<DetalleVentaListDto> GetDetalleVenta(int ventaId)
        {
            try
            {
                return _repoDetalleVentas.GetDetalleVentas(ventaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> GetVentas()
        {
            try
            {
                return _repositorio.GetVentas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public VentaListDto GetVentaPorId(int id)
        {
            try
            {
                return _repositorio.GetVentaPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Venta venta, string user)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {

                    var ventaGuardar = new Venta()
                    {
                        ClienteId = venta.ClienteId,
                        FechaVenta = venta.FechaVenta,
                        TransaccionId = venta.TransaccionId,
                        Total = venta.Total
                    };
                    _repositorio.Agregar(ventaGuardar);
                    _unitOfWork.SaveChanges();
                    foreach (var item in venta.Detalles)
                    {
                        item.VentaId = ventaGuardar.VentaId;
                        _repoDetalleVentas.Agregar(item);
                        _repoTamañosProductos.ActualizarStock(item.TamañoProductoId, item.Cantidad);
                        _repoCarritos.Borrar(user, item.TamañoProductoId);

                    }
                    _unitOfWork.SaveChanges();
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private readonly IRepositorioVentas _repositorio;
        //private readonly IRepositorioDetalleVentas _repoDetalleVentas;
        //private readonly IRepositorioProductos _repoProductos;
        //private readonly IUnitOfWork _unitOfWork;

        //public ServiciosVentas(IRepositorioVentas repositorio,
        //    IRepositorioDetalleVentas repoDetalleVentas,
        //    IRepositorioProductos repoProductos,
        //    IUnitOfWork unitOfWork)
        //{
        //    _repositorio = repositorio;
        //    _repoDetalleVentas = repoDetalleVentas;
        //    _repoProductos = repoProductos;
        //    _unitOfWork = unitOfWork;
        //}

        //public int GetCantidad()
        //{
        //    try
        //    {
        //        return _repositorio.GetCantidad();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public List<DetalleVentaListDto> GetDetalleVenta(int ventaId)
        //{
        //    try
        //    {
        //        return _repoDetalleVentas.GetDetalleVentas(ventaId);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public VentaListDto GetVentaPorId(int id)
        //{
        //    try
        //    {
        //        return _repositorio.GetVentaPorId(id);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public List<VentaListDto> GetVentas()
        //{
        //    try
        //    {
        //        return _repositorio.GetVentas();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public void Guardar(Venta venta, string name)
        //{
        //    try
        //    {
        //        using (var transaction = new TransactionScope())
        //        {
        //            var ventaGuardar = new Venta()
        //            {
        //                ClienteId=venta.ClienteId,
        //                FechaVenta = venta.FechaVenta,
        //                Total = venta.Total
        //            };
        //            _repositorio.Agregar(ventaGuardar);
        //            _unitOfWork.SaveChanges();
        //            foreach (var item in venta.Detalles)
        //            {
        //                item.VentaId = ventaGuardar.VentaId;
        //                _repoDetalleVentas.Agregar(item);
        //                //_repoProductos.ActualizarStock(item.ProductoId, item.Cantidad);
        //            }
        //            _unitOfWork.SaveChanges();
        //            transaction.Complete();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
