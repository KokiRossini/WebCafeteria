using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels
{
    public class PaisListVm
    {
        public int PaisId { get; set; }

        [DisplayName("País")]
        public string NombrePais { get; set; }
        [DisplayName ("Cant. Paises")]
        public int CantidadCiudades { get; set; }

    }
}