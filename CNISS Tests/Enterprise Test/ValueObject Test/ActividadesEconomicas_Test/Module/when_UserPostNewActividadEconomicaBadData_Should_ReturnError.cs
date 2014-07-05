using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.ActividadesEconomicas_Test.Module
{
    [Subject(typeof (ActividadEconomicaModuleInsert))]
    public class when_UserPostNewActividadEconomicaBadData_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ActividadEconomicaRequest actividadEconomicaRequest;

        private Establish context = () =>
        {
          actividadEconomicaRequest = new ActividadEconomicaRequest();
            var command = Mock.Of<ICommandInsertIdentity<ActividadEconomica>>();
           


            _browser = new Browser(
                x =>
                {
                    x.Module<ActividadEconomicaModuleInsert>();
                    x.Dependencies(command);
                }
                
                );


        };

        private Because of = () =>
        {
            _response = _browser.PostSecureJson("/enterprise/actividades", actividadEconomicaRequest);
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}