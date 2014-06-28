using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (EmpleoModuleInsert))]
    public class when_UserPostInvalidEmpleo_Should_ReturnError
    {

        static Browser _browser;
        static BrowserResponse _response;
        static ICommandInsertIdentity<Empleo> _commandInsert;
        static EmpleoRequest _request;


        private Establish context = () =>
        {
            
            _request = new EmpleoRequest();

            _commandInsert = Mock.Of<ICommandInsertIdentity<Empleo>>();
            _browser = new Browser(
                x =>
                {
                    x.Module<EmpleoModuleInsert>();
                    x.Dependencies(_commandInsert);
                }

                
                );

        };

        private Because of = () => { _response = _browser.PostSecureJson("/enterprise/empleos", _request); };

        It should_return_error = () => _response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
    }
}