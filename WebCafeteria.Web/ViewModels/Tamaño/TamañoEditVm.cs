﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.Tamaño
{
    public class TamañoEditVm
    {
        public int TamañoId { get; set; }

        [DisplayName("Tamaño")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreTamaño { get; set; }

    }
}