using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Dtos;

namespace WebCafeteria.Entidades
{
    public class Ciudad
    {
        public int CiudadId { get; set; }
        public string NombreCiudad { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
        public CiudadListDto ToCiudadListDto()
        {
            return new CiudadListDto()
            {
                CiudadId = CiudadId,
                NombreCiudad = NombreCiudad,
                NombrePais = Pais.NombrePais
            };
        }
    }
}
