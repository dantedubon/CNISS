using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.TipoEmpleo_Test.Module
{
    [Subject(typeof (TipoEmpleoModuleInsert))]
    public class when_UserPostNewTipoEmpleoInvalidData_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static TipoEmpleoRequest _tipoEmpleoRequest;

        private Establish context = () =>
        {
            _tipoEmpleoRequest = new TipoEmpleoRequest();

            var command = Mock.Of<ICommandInsertIdentity<TipoEmpleo>>();

            _browser = new Browser(
                x =>
                {
                    x.Module<TipoEmpleoModuleInsert>();
                    x.Dependencies(command);
                }
                
                );
        };

        private Because of = () => { _response = _browser.PostSecureJson("/enterprise/tipoEmpleo", _tipoEmpleoRequest); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}