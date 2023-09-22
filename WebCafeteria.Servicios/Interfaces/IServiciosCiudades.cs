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
    public interface IServiciosCiudades
    {
        List<CiudadListDto> GetCiudades();
        bool Existe(Ciudad ciudad);
        void Guardar(Ciudad ciudad);
        bool EstaRelacionada(Ciudad ciudad);
        Ciudad GetCiudadPorId(int ciudadId);
        void Borrar(int ciudadId);
        List<CiudadListDto> GetCiudades(int paisId);
        List<CiudadListDto> Filtrar(Func<Ciudad, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<CiudadListDto> GetCiudadesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Ciudad, bool> predicado);
        List<SelectListItem> GetCiudadesDropDownList(int paisId);
    }
}
