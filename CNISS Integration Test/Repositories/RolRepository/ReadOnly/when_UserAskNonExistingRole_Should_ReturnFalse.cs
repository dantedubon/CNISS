using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.RolRepository.ReadOnly
{
    [Subject(typeof (RolRepositoryReadOnly))]
    public class when_UserAskNonExistingRole_Should_ReturnFalse
    {
        static IRolRepositoryReadOnly _repository;
        static Rol _expectedRol;
        static bool _existRol;
        static InMemoryDatabaseTest _databaseTest;
        static ISession _session;

         Establish context = () =>
        {
            _databaseTest = new InMemoryDatabaseTest(typeof(Rol).Assembly);
            _databaseTest.openSession();

            _session = _databaseTest.session;

            _repository = new RolRepositoryReadOnly(_session);
            _expectedRol = Builder<Rol>.CreateNew().Build();

           

        };

         Because of = () =>
         {
             _existRol = _repository.exists(_expectedRol.idKey);

         };

        It should_return_false = () => _existRol.Should().BeFalse();
    }
}
