using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database.Mappings;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
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
