using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Beneficiario_Test.Modules
{
    [Subject(typeof (BeneficiarioModuleInsert))]
    public class when_UserPostNewBeneficiarioWithInvalidData_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static BeneficiarioRequest _beneficiarioRequest;

        private Establish context = () =>
        {
            _beneficiarioRequest = new BeneficiarioRequest();

            var command = Mock.Of<ICommandInsertIdentity<Beneficiario>>();

            _browser = new Browser(
                x =>
                {
                    x.Module<BeneficiarioModuleInsert>();
                    x.Dependencies(command);
                }
                
                );
        };

        private Because of = () =>
        {
            _response = _browser.PostSecureJson("/enterprise/beneficiarios", _beneficiarioRequest);
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}