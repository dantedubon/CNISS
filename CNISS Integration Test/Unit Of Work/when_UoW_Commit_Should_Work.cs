﻿using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Unit_Of_Work
{
    [Subject("Unit Of Work")]
    class when_UoW_Commit_Should_Work
    {
         
         static InFileDataBaseTest _databaseTest;
         static ISessionFactory _sessionFactory;
         static Rol _expectedRol;
         static Rol _resultRol;

         Establish context = () =>
        {
            _databaseTest = new InFileDataBaseTest();
            _sessionFactory = _databaseTest.sessionFactory;
            
            _expectedRol = Builder<Rol>.CreateNew().Build();


        };

        Because of = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession())  )
            {
                
                uow.Session.Save(_expectedRol);
                uow.commit();
               
            }
        };

         It rol_shoul_be_saved = () =>
         {
             using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
             {
                _resultRol = uow.Session.Get<Rol>(_expectedRol.Id); 
             }

             _resultRol.ShouldBeEquivalentTo(_expectedRol);
         };

    }
}
