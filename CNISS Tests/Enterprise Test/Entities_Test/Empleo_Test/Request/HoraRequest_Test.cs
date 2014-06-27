using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Request
{
    [TestFixture]
    public class HoraRequest_Test
    {
        private Object[] badDataForPost;

        public HoraRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    13,0,"AM"
                },
                 new object[]
                {
                    0,0,"AM"
                },
                 new object[]
                {
                    1,60,"AM"
                },

                 new object[]
                {
                    1,-1,"AM"
                },

                new object[]
                {
                    1,1,"XX"
                },

            };
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidData_returnsFalse(int hora, int minutos, string parte)
        {
            var horaRequest = new HoraRequest() {hora = hora, minutos = minutos, parte = parte};

            var result = horaRequest.isValidPost();

            Assert.IsFalse(result);
        }

        [Test]
        public void isValidPost_validDataWithPartPM_returnsTrue()
        {
            var hora = 1;
            var minutos = 1;
            var parte = "PM";
            var horaRequest = new HoraRequest() { hora = hora, minutos = minutos, parte = parte };

            var result = horaRequest.isValidPost();

            Assert.IsTrue(result);
        }
        [Test]
        public void isValidPost_validDataWithPartAM_returnsTrue()
        {
            var hora = 1;
            var minutos = 1;
            var parte = "AM";
            var horaRequest = new HoraRequest() { hora = hora, minutos = minutos, parte = parte };

            var result = horaRequest.isValidPost();

            Assert.IsTrue(result);
        }
    }
}