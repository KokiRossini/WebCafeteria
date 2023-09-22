using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;
using System.Web.Mvc;

namespace WebCafeteria.Servicios.Interfaces
{
    public interface IServiciosProveedores
    {
        void Borrar(int id);
        bool EstaRelacionado(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        void Guardar(Proveedor proveedor);
        Proveedor GetProveedorPorId(int id);
        List<ProveedorListDto> GetProveedores();
        List<ProveedorListDto> GetProveedores(int paisId, int ciudadId);
        List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado);
        List<SelectListItem> GetProveedoresDropDownList();
    }
}
