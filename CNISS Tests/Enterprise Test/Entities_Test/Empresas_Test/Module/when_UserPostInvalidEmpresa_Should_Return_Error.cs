using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof (EmpresaModuleInsert))]
    public class when_UserPostEmpresaWithInvalidData_Should_Return_Error
    {

        static Browser _browser;
        static BrowserResponse _response;
        static ICommandInsertIdentity<Empresa> _commandInsert;
        static EmpresaRequest _request;
        private Establish context = () =>
        {
            _request = new EmpresaRequest();
            _commandInsert = Mock.Of<ICommandInsertIdentity<Empresa>>();
            var fileGetter = Mock.Of<IFileGetter>();

            _browser = new Browser(
                x =>
                {
                    x.Module<EmpresaModuleInsert>();
                    x.Dependencies(_commandInsert,fileGetter);
                }
                );

        };

        private Because of = () => { _response = _browser.PostSecureJson("enterprise/", _request); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}