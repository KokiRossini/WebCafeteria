using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos.Cliente;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Datos.Interfaces
{
    public interface IRepositorioClientes
    {
        void Agregar(Cliente cliente);
        void Borrar(int id);
        void Editar(Cliente cliente);
        bool EstaRelacionado(Cliente cliente);
        bool Existe(Cliente cliente);
        Cliente GetClientePorId(int id);
        Cliente GetClientePorEmail(string email);
        List<ClienteListDto> GetClientes();
        List<ClienteListDto> GetClientes(int paisId, int ciudadId);
        List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado);
        int GetCantidad();

    }
}
