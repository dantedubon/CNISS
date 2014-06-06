using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace CNISS_Integration_Test.Repositories.RolRepository.ReadOnly
{
    [TestFixture]
    class RolRepositoryReadOnlyTest : InMemoryDatabaseTest
    {
        public RolRepositoryReadOnlyTest()
            : base(typeof(Rol).Assembly)
        {
            openSession();
        }

        [TestCase]
        public void getAll_RetornaTodosRoles()
        {
            //Arrange

            IRolRepositoryReadOnly _repository = new RolRepositoryReadOnly(session);
            IEnumerable<Rol> _expectedRols = Builder<Rol>.CreateListOfSize(10).Build();
            IEnumerable<Rol> _resultRols;


            using (var tx = session.BeginTransaction())
            {
                foreach (var expectedRol in _expectedRols)
                {
                    session.Save(expectedRol);
                }

                tx.Commit();
            }

            session.Clear();


            _resultRols = _repository.getAll();




            _resultRols.ShouldAllBeEquivalentTo(_expectedRols);



        }
    }

}