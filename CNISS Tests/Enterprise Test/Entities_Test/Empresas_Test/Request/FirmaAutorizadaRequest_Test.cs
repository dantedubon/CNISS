using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Request
{
    [TestFixture]
    public class FirmaAutorizadaRequest_Test
    {

        private object[] badDataForPostFicha;

        public FirmaAutorizadaRequest_Test()
        {
            badDataForPostFicha = new object[]
            {
                new object[]
                {
                    Guid.Empty,getUserRequest()
                }, 
                new object[]
                {
                    Guid.NewGuid(),new UserRequest()
                },

            };
        }


        [TestCaseSource("badDataForPostFicha")]
        public void isValidPostForFichaSupervision_InvalidData_ReturnFalse(Guid id, UserRequest user)
        {
            var firma = new FirmaAutorizadaRequest()
            {
                IdGuid = id,
                userRequest = user

            };

            var response = firma.isValidPostForFichaSupervision();

            Assert.IsFalse(response);
        }

        private UserRequest getUserRequest()
        {
            return new UserRequest()
            {
                Id ="DRCD",
                password = "XXX"
            };
        }
    }
}