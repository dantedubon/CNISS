using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.DiasLaborables_Test
{
    [TestFixture]
    public class DiasLaborablesRequest_Test
    {
        private Object[] goodDataForPost;

        public DiasLaborablesRequest_Test()
        {
            goodDataForPost = new object[]
            {
                new object[]
                {
                    true,false,false,false,false,false,false
                },
                new object[]
                {
                    false,true,false,false,false,false,false
                },
                new object[]
                {
                    false,false,true,false,false,false,false
                },
                new object[]
                {
                    false,false,false,true,false,false,false
                },
                new object[]
                {
                    false,false,false,false,true,false,false
                },
                new object[]
                {
                    false,false,false,false,false,true,false
                },
                new object[]
                {
                    false,false,false,false,false,false,true
                },
            };
        }

        [TestCaseSource("goodDataForPost")]
        public void isValidPost_goodData_returnsTrue(bool lunes, bool martes, bool miercoles, bool jueves, bool viernes, bool sabado, bool domingo)
        {
            var dias = new DiasLaborablesRequest()
            {
                lunes = lunes,
                martes = martes,
                miercoles = miercoles,
                jueves = jueves,
                viernes = viernes,
                sabado = sabado,
                domingo = domingo,
            };

            var resultado = dias.isValidPost();
            Assert.IsTrue(resultado);
        }

        [Test]
        public void isValidPost_invalidData_returnsFalse()
        {
            var dias = new DiasLaborablesRequest();
            var resultado = dias.isValidPost();

            Assert.IsFalse(resultado);
        }


    }
}