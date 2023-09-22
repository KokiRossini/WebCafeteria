using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Entidades;

namespace WebCafeteria.Servicios.Interfaces
{
    public interface IGateway
    {
        PaymentResult ProcesarPago(CheckOut model);
    }
}
