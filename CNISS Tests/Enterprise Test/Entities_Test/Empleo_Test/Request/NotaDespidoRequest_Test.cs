using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CsQuery.Engine.PseudoClassSelectors;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Request
{
    [TestFixture]
    public class NotaDespidoRequest_Test
    {
        private object[] badDataForPost;

        public NotaDespidoRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    "",DateTime.Now.Date,Guid.NewGuid(),getSupervisorRequest(),getFirmaAutorizadaRequest(),getAuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                }, 
                new object[]
                {
                    "GPS",null,Guid.NewGuid(),getSupervisorRequest(),getFirmaAutorizadaRequest(),getAuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                }, 
                new object[]
                {
                    "GPS",DateTime.Now.Date,null,getSupervisorRequest(),getFirmaAutorizadaRequest(),getAuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                },

                new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),new SupervisorRequest(),getFirmaAutorizadaRequest(),getAuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                },

                 new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),getSupervisorRequest(),new FirmaAutorizadaRequest(),getAuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                },
                 new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),getSupervisorRequest(),getFirmaAutorizadaRequest(),new AuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                },
                new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),getSupervisorRequest(),getFirmaAutorizadaRequest(),getAuditoriaRequest(),new MotivoDespidoRequest(), Guid.NewGuid()
                },
                new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),getSupervisorRequest(),getFirmaAutorizadaRequest(),getAuditoriaRequest(),getMotivoDespidoRequest(), null
                },
                new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),null,getFirmaAutorizadaRequest(),getAuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                },
                new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),getSupervisorRequest(),null,getAuditoriaRequest(),getMotivoDespidoRequest(), Guid.NewGuid()
                },
                new object[]
                {
                    "GPS",DateTime.Now.Date,Guid.NewGuid(),getSupervisorRequest(),getFirmaAutorizadaRequest(),getAuditoriaRequest(),null, Guid.NewGuid()
                },
            };
        }

        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidData_returnFalse(string posicionGps, DateTime fechaDespido, Guid imagenNota, SupervisorRequest supervisor, FirmaAutorizadaRequest firma, AuditoriaRequest auditoria, MotivoDespidoRequest motivoDespido, Guid empleoId)
        {
            var notaDespido = new NotaDespidoRequest()
            {
                fechaDespido = fechaDespido,
                auditoriaRequest = auditoria,
                firmaAutorizadaRequest = firma,
                supervisorRequest = supervisor,
                imagenNotaDespido = imagenNota,
                motivoDespidoRequest = motivoDespido,
                posicionGPS = posicionGps,
                empleoId = empleoId
                
            };

            var respuesta = notaDespido.isValidPost();

            Assert.IsFalse(respuesta);

        }


        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var notaDespido = new NotaDespidoRequest()
            {
                fechaDespido = DateTime.Now.Date,
                auditoriaRequest = getAuditoriaRequest(),
                firmaAutorizadaRequest = getFirmaAutorizadaRequest(),
                supervisorRequest = getSupervisorRequest(),
                imagenNotaDespido = Guid.NewGuid(),
                motivoDespidoRequest = getMotivoDespidoRequest(),
                posicionGPS = "posicionGPS",
                empleoId = Guid.NewGuid()

            };

            var respuesta = notaDespido.isValidPost();

            Assert.IsTrue(respuesta);
        }

        private MotivoDespidoRequest getMotivoDespidoRequest()
        {
            return new MotivoDespidoRequest()
            {
                IdGuid = Guid.NewGuid()
            };
        }

        private FirmaAutorizadaRequest getFirmaAutorizadaRequest()
        {
            return new FirmaAutorizadaRequest()
            {
                IdGuid = Guid.NewGuid(),
                userRequest = getUserRequest()

            };
        }


        private AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest()
            {
                fechaCreo = new DateTime(2014, 8, 2),
                usuarioCreo = "usuarioCreo",
                fechaModifico = new DateTime(2014, 8, 2),
                usuarioModifico = "usuarioModifico"
            };
        }
        private SupervisorRequest getSupervisorRequest()
        {
            return new SupervisorRequest()
            {
                guid = Guid.NewGuid(),
                userRequest = new UserRequest()
                {
                    Id = "User",

                    password = "xxxx",

                }
            };

        }



        private UserRequest getUserRequest()
        {
            return new UserRequest()
            {
                Id = "User",
                password = "xxxx",

            };
        }
    }
}