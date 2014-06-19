using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework.Constraints;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test.Module
{
    [Subject(typeof (GremioModuleDelete))]
    public class when_UserDeleteValidGremio_ShouldDeleteGremio
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandDeleteGremio _commandDelete;
        static RTNRequest _requestRTN;

        private Establish context = () =>
        {
            _requestRTN = new RTNRequest(){RTN = "08011985123960"};
            _commandDelete = Mock.Of<ICommandDeleteGremio>();
            Mock.Get(_commandDelete).Setup(x => x.isExecutable(Moq.It.IsAny<RTN>())).Returns(true);

            _browser = new Browser(x =>
            {
                x.Module<GremioModuleDelete>();
                x.Dependencies(_commandDelete);
            }
                
                );



        };

        private Because of = () =>
        {
            _response = _browser.DeleteSecureJson("/enterprise/gremio/", _requestRTN);
        };

        It should_delete_gremio = () => _response.StatusCode.ShouldEqual(HttpStatusCode.OK);
    }
}