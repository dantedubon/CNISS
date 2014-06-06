using System;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;
using It = Machine.Specifications.It;

namespace CNISS_Integration_Test
{
    class when_insert_rol_direct_to_DB
    {
         static ISession _session;
         static Rol _expectedRol;
         static Object id;
         Establish context = () =>
        {
            _session = SessionFactoryTest.OpenSession();
            _expectedRol = Builder<Rol>.CreateNew().Build();

           

        };

         Because of = () => id = _session.Save(_expectedRol);



         It should_save_object = () =>
         {
             var _rolFromDataBase = _session.Get<Rol>(id);
             _expectedRol.ShouldBeEquivalentTo(_rolFromDataBase);
         };



    }
}
