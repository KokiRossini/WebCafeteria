﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCafeteria.Web.ViewModels.Carrito
{
    public class CarritoVm
    {
        public List<ItemCarritoVm> Items { get; set; }
        public string returnUrl { get; set; }


    }
}