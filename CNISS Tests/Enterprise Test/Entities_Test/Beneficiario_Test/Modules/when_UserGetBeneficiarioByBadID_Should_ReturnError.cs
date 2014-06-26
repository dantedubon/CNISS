using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Beneficiario_Test.Modules
{
    [Subject(typeof (BeneficiarioModuleQuery))]
    public class when_UserGetBeneficiarioByBadID_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IdentidadRequest _requestId;
        private Establish context = () =>
        {
           _requestId = new IdentidadRequest(){identidad = ""};

           var repository = Mock.Of<IBeneficiarioRepositoryReadOnly>();


           _browser = new Browser(
               x =>
               {
                   x.Module<BeneficiarioModuleQuery>();
                   x.Dependencies(repository);
               }

               );


 
        };

        private Because of = () => { _response = _browser.GetSecureJson("/enterprise/beneficiarios/id=", _requestId); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}