using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Query;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.Parentesco_Test.Module
{
    [Subject(typeof (ParentescoModuleQuery))]
    public class when_UserGetAllParentesco_Should_ReturnAllParentesco
    {
        private static Browser _browser;
        private static IEnumerable<Parentesco> _expectedParentescos;
        private static IEnumerable<Parentesco> _responseParentescos; 

        private Establish context = () =>
        {
            _expectedParentescos = Builder<Parentesco>.CreateListOfSize(10).Build();
            var repository = Mock.Of<IParentescoReadOnlyRepository>();
            Mock.Get(repository).Setup(x => x.getAll()).Returns(_expectedParentescos);

            _browser = new Browser(
                x =>
                {
                    x.Module<ParentescoModuleQuery>();
                    x.Dependencies(repository);
                }
                );

        };

        private Because of = () =>
        {
            _responseParentescos =
                _browser.GetSecureJson("/enterprise/beneficiarios/parentescos")
                    .Body.DeserializeJson<IEnumerable<Parentesco>>();
        };

        It should_return_all_parentesco = () => _responseParentescos.ShouldBeEquivalentTo(_expectedParentescos);
    }

   
}