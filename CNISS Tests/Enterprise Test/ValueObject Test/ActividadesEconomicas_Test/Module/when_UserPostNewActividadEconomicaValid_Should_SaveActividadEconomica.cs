using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.ActividadesEconomicas_Test.Module
{
    [Subject(typeof(ActividadEconomicaModuleInsert))]
    public class when_UserPostNewActividadEconomicaValid_Should_SaveActividadEconomica
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ActividadEconomicaRequest actividadEconomicaRequest;
        private static ActividadEconomica _expectedActividadEconomica;
        private static ICommandInsertIdentity<ActividadEconomica> _command; 

        private Establish context = () =>
        {
            actividadEconomicaRequest = new ActividadEconomicaRequest()
            {
                descripcion = "actividad",
                auditoriaRequest = new AuditoriaRequest()
                {
                    usuarioCreo = "usuarioCreo",
                    fechaCreo = new DateTime(2014,8,1),
                    fechaModifico = new DateTime(2014,8,1),
                    usuarioModifico = "usuarioModifico"
                }
            };

             _command = Mock.Of<ICommandInsertIdentity<ActividadEconomica>>();
            _expectedActividadEconomica = getActividadEconomica(actividadEconomicaRequest);


            _browser = new Browser(
                x =>
                {
                    x.Module<ActividadEconomicaModuleInsert>();
                    x.Dependencies(_command);
                }
                
                );

        };

        private Because of = () =>
        {
            _response = _browser.PostSecureJson("/enterprise/actividades", actividadEconomicaRequest);
        };

        private It should_insert_actividad = () => Mock.Get(_command).Verify(x => x.execute(Moq.It.Is<ActividadEconomica>(z => z.descripcion == _expectedActividadEconomica.descripcion)));

        private static ActividadEconomica getActividadEconomica(ActividadEconomicaRequest actividadEconomicaRequest)
        {
            var actividad = new ActividadEconomica(actividadEconomicaRequest.descripcion)
            {
                auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    actividadEconomicaRequest.auditoriaRequest.usuarioCreo,
                    actividadEconomicaRequest.auditoriaRequest.fechaCreo,
                    actividadEconomicaRequest.auditoriaRequest.usuarioModifico,
                    actividadEconomicaRequest.auditoriaRequest.fechaModifico

                    )
            };
            return actividad;
        }
    }
}