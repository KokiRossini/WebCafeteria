using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCafeteria.Entidades.Entidades;
using WebCafeteria.Servicios.Interfaces;

namespace WebCafeteria.Servicios
{
    public class PaymentGateway : IGateway
    {
        private readonly BraintreeGateway _gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = "p82gwsw2zk2hscmt",
            PublicKey = "8rhq5tftprsh9h37",
            PrivateKey = "423c909e81e329795b2f8b941dc34121"
        };
        public PaymentResult ProcesarPago(CheckOut model)
        {
            var request = new TransactionRequest()
            {
                Amount = model.Venta.Total,
                CreditCard = new TransactionCreditCardRequest()
                {
                    Number = model.CardNumber,
                    CVV = model.CVV,
                    ExpirationMonth = model.Month.ToString(),
                    ExpirationYear = model.Year.ToString(),
                },
                Options = new TransactionOptionsRequest()
                {
                    SubmitForSettlement = true,
                },
            };

            var result = _gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                return new PaymentResult(result.Target.Id,
                    true, null);

            }
            return new PaymentResult(null, false, result.Message);
        }
    }
}
