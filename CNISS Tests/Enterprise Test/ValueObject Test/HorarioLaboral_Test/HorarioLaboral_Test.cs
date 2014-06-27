using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.HorarioLaboral_Test
{
    [TestFixture]
    public class HorarioLaboral_Test
    {
        private Object[] badDataForPost;

        public HorarioLaboral_Test()
        {
            badDataForPost = new[]
            {
                new object[]
                {
                    new HoraRequest(), getHoraRequest(),getDiasLaborablesRequest()
                },

                 new object[]
                {
                    getHoraRequest(), new HoraRequest(),getDiasLaborablesRequest()
                },
                 new object[]
                {
                    getHoraRequest(), getHoraRequest(),new DiasLaborablesRequest()
                },
                new object[]
                {
                    null, getHoraRequest(),new DiasLaborablesRequest()
                },
                 new object[]
                {
                    getHoraRequest(), null,new DiasLaborablesRequest()
                },
                 new object[]
                {
                    getHoraRequest(), getHoraRequest(),null
                },

            };
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidData_returnsFalse(HoraRequest horaEntrada, HoraRequest horaSalida, DiasLaborablesRequest dias)
        {
            var horario = new HorarioLaboralRequest()
            {
                horaEntrada = horaEntrada,
                horaSalida = horaSalida,
                diasLaborablesRequest = dias
            };

            var respuesta = horario.isValidPost();

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isValidPost_goodData_returnsTrue()
        {
            var horario = new HorarioLaboralRequest()
            {
                horaEntrada = getHoraRequest(),
                horaSalida = getHoraRequest(),
                diasLaborablesRequest = getDiasLaborablesRequest()
            };

            var respuesta = horario.isValidPost();

            Assert.IsTrue(respuesta);
        }

        private HoraRequest getHoraRequest()
        {
            return new HoraRequest(){hora = 1,minutos = 2,parte = "AM"};
        }

        private DiasLaborablesRequest getDiasLaborablesRequest()
        {
            return new DiasLaborablesRequest()
            {
                lunes = true
            };
        }
    }
}