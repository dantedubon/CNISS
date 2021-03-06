﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Request
{
    [TestFixture]
    public class ComprobantePagoRequest_Test
    {

        public Object[] badRequestForPost;

        public ComprobantePagoRequest_Test()
        {
            badRequestForPost = new object[]
            {
                new object[]
                {
                    0.0m,2000m,3000m,new DateTime(2014,4,1)
                },
                
                new object[]
                {
                    200.0m,200.0m,2000m,null
                },


            };
        }



        [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_returnFalse(decimal percepciones, decimal deducciones, decimal bonificaciones, DateTime fechaPago)
        {
            var comprobante = new ComprobantePagoRequest()
            {
                deducciones = deducciones,
                fechaPago = fechaPago,
                sueldoNeto = percepciones,
                bonificaciones = bonificaciones
            };

            var respuesta = comprobante.isValidPost();

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isValid_validData_returnTrue()
        {
            var comprobante = new ComprobantePagoRequest()
            {
                deducciones = 2.0m,
                fechaPago = new DateTime(2014,1,1),
                sueldoNeto = 3.0m,
                bonificaciones = 3.0m
            };

            var respuesta = comprobante.isValidPost();

            Assert.IsTrue(respuesta);
        }
    }
}