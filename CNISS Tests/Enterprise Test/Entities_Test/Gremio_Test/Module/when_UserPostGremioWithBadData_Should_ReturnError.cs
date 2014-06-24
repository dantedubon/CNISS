using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test.Module
{
    [Subject(typeof (GremioModuleInsert))]
    public class when_UserPostGremioWithBadData_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandInsertIdentity<Gremio> _commandInsert;
        static GremioRequest _request;

         Establish context = () =>
         {
            _request = new GremioRequest();
              _commandInsert = Mock.Of<ICommandInsertIdentity<Gremio>>();

             _browser = new Browser(
                 x =>
                 {
                     x.Module<GremioModuleInsert>();
                     x.Dependencies(_commandInsert);
                 });
         };

        private Because of = () => { _response = _browser.PostSecureJson("enterprise/gremio", _request); };


        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}