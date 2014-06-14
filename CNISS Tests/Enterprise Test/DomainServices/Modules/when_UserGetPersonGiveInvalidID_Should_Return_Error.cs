using CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.DomainServices.Modules
{
    [Subject(typeof (PersonRNPModule))]
    public class when_UserGetPersonGiveInvalidID_Should_Return_Error
    {
        static Browser _browser;
         static BrowserResponse _response;
        static PersonRNP _personExpected;
        Establish context = () =>
        {
            _personExpected = Builder<PersonRNP>.CreateNew().Build();
            var repository = Mock.Of<IPersonRNPRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.get(Moq.It.IsAny<string>())).Returns(_personExpected);

            _browser = new Browser(x =>
            {
                x.Module<PersonRNPModule>();
                x.Dependencies(repository);
            }

                );


        };

         Because of = () =>
         {
             _response = _browser.GetSecureJson("/enterprise/Person/id=");

         };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}