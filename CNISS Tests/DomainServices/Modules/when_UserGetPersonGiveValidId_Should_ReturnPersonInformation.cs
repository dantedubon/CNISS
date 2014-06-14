using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.DomainServices.Modules
{
    [Subject(typeof (PersonRNPModule))]
    public class when_UserGetPersonGiveValidId_Should_ReturnPersonInformation
    {
         static Browser _browser;
         static PersonRNP _personResponse;
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
             _personResponse =
                 _browser.GetSecureJson("/enterprise/Person/id=" + _personExpected.Id).Body.DeserializeJson<PersonRNP>();
         };

         It should_return_PersonInformation = () => _personResponse.ShouldBeEquivalentTo(_personExpected);
    }
}