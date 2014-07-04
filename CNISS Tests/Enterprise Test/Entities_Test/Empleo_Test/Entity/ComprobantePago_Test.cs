using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Entities;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Entity
{
    [TestFixture]
    public class ComprobantePago_Test
    {
        [Test]
        public void constructor_calculoTotal_retornaTotalCorrecto()
        {
            var fechaPago = new DateTime(1984, 12, 1);
            var percepciones = 5000M;
            var deducciones = 1000M;
            var bonificiones = 1000M;

            var totalEsperado = 5000M;

            var comprobante = new ComprobantePago(fechaPago, deducciones, percepciones, bonificiones);

            Assert.AreEqual(totalEsperado,comprobante.total);
        }
    }
}