﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCafeteria.Entidades
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }
        public string CodPostal { get; set; }

        public Pais Pais { get; set; }
        public Ciudad Ciudad { get; set; }

    }
}
