using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades.Dtos
{
    public class ProveedorListDto
    {
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }

    }
}
