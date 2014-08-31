using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.ActividadesEconomicas_Test.Module
{
    [Subject(typeof(ActividadEconomicaModuleUpdate))]
    public class when_UserPutActividadEconomicaNotExists_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ActividadEconomicaRequest actividadEconomicaRequest;
        private static ActividadEconomica _expectedActividadEconomica;
        private static ICommandUpdateIdentity<ActividadEconomica> _command;

        private Establish context = () =>
        {
            actividadEconomicaRequest = new ActividadEconomicaRequest()
            {
                guid = Guid.NewGuid(),
                descripcion = "actividad",
                auditoriaRequest = new AuditoriaRequest()
                {
                    usuarioCreo = "usuarioCreo",
                    fechaCreo = new DateTime(2014, 8, 1),
                    fechaModifico = new DateTime(2014, 8, 1),
                    usuarioModifico = "usuarioModifico"
                }
            };

            _command = Mock.Of<ICommandUpdateIdentity<ActividadEconomica>>();
            
            _expectedActividadEconomica = getActividadEconomica(actividadEconomicaRequest);

            Mock.Get(_command).Setup(x => x.isExecutable(_expectedActividadEconomica)).Returns(false);

            _browser = new Browser(
                x =>
                {
                    x.Module<ActividadEconomicaModuleUpdate>();
                    x.Dependencies(_command);
                }

                );

        };

        private Because of = () =>
        {
            _response = _browser.PutSecureJson("/enterprise/actividades", actividadEconomicaRequest);
        };

         It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static ActividadEconomica getActividadEconomica(ActividadEconomicaRequest actividadEconomicaRequest)
        {
            var actividad = new ActividadEconomica(actividadEconomicaRequest.descripcion)
            {
                Auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    actividadEconomicaRequest.auditoriaRequest.usuarioCreo,
                    actividadEconomicaRequest.auditoriaRequest.fechaCreo,
                    actividadEconomicaRequest.auditoriaRequest.usuarioModifico,
                    actividadEconomicaRequest.auditoriaRequest.fechaModifico

                    )
            };
            actividad.Id = actividadEconomicaRequest.guid;
            return actividad;
        }
    }
}