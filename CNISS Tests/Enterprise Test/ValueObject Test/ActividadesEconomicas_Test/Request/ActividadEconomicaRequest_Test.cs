using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using NUnit.Framework;
using Remotion.Linq.Parsing;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.ActividadesEconomicas_Test.Request
{
    [TestFixture]
    public class ActividadEconomicaRequest_Test
    {
        private object[] badDataForPost;
        private object[] badDataForPut;

        public ActividadEconomicaRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    "",getAuditoriaRequest()
                }, 
                 new object[]
                {
                    "actividad",null
                }, 
                 new object[]
                {
                    "actividad",new AuditoriaRequest()
                }
            };

            badDataForPut = new object[]
            {
                new object[]
                {
                    null, "actividad", getAuditoriaRequest()
                },
                new object[]
                {
                    Guid.NewGuid(),"",getAuditoriaRequest()
                }, 
                 new object[]
                {
                    Guid.NewGuid(),"actividad",null
                }, 
                 new object[]
                {
                    Guid.NewGuid(),"actividad",new AuditoriaRequest()
                }
            };
        }


        [TestCaseSource("badDataForPut")]
        public void isValidPut_invalidData_returnFalse(Guid idGuid,string descripcion, AuditoriaRequest auditoriaRequest)
        {
            var actividad = new ActividadEconomicaRequest()
            {
                guid = idGuid,
                descripcion = descripcion,
                auditoriaRequest = auditoriaRequest
            };

            var respuesta = actividad.isValidPut();

            Assert.IsFalse(respuesta);
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidData_returnFalse(string descripcion, AuditoriaRequest auditoriaRequest)
        {

            var actividad = new ActividadEconomicaRequest()
            {
                descripcion = descripcion,
                auditoriaRequest = auditoriaRequest
            };

            var respuesta = actividad.isValidPost();

            Assert.IsFalse(respuesta);

        }

        private AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest()
            {
                fechaCreo = new DateTime(2014,8,2),
                usuarioCreo = "usuarioCreo",
                fechaModifico = new DateTime(2014,8,2),
                usuarioModifico = "usuarioModifico"
            };
        }
        
    }
}