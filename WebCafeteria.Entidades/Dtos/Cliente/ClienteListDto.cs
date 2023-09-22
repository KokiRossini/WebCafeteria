using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades.Dtos.Cliente
{
    public class ClienteListDto
    {
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }

    }
}
