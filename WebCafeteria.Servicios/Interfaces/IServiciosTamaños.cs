using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos;
using WebCafeteria.Entidades;
using WebCafeteria.Entidades.Entidades;
using System.Web.Mvc;

namespace WebCafeteria.Servicios.Interfaces
{
    public interface IServiciosTamaños
    {
        void Borrar(int id);
        bool EstaRelacionado(Tamaño tamaño);
        bool Existe(Tamaño tamaño);
        void Guardar(Tamaño tamaño);
        Tamaño GetTamañoPorId(int id);
        List<Tamaño> GetTamaños();
        List<Tamaño> Filtrar(Func<Tamaño, bool> predicado);
        List<SelectListItem> GetTamañosDropDown();
    }
}
