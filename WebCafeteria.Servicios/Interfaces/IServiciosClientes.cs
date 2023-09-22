
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos.Cliente;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Servicios.Interfaces
{
    public interface IServiciosClientes
    {
        void Borrar(int id);
        bool EstaRelacionado(Cliente cliente);
        bool Existe(Cliente cliente);
        Cliente GetClientePorId(int id);
        List<ClienteListDto> GetClientes();
        List<ClienteListDto> GetClientes(int paisId, int ciudadId);
        void Guardar(Cliente cliente);
        List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado);
        int GetCantidad();
        Cliente GetClientePorEmail(string name);

    }
}
