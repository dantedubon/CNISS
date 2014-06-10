﻿using System;
using Autofac;
using CNISS.AutenticationDomain.Application.Comandos;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Ports.Output.Database;
using CNISS.CommonDomain.Application;
using Nancy.Cryptography;

namespace CNISS.Bootstraper
{
    public class ConfigureUserDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                {
                    builder.RegisterType<UserRepositoryReadOnly>().As<IUserRepositoryReadOnly>();
                    builder.RegisterType<UserRepositoryCommands>().As<IUserRepositoryCommands>();
                    builder.RegisterType<CommandInsertUser>().As<ICommandInsertIdentity<User>>();
                    builder.Register(c => new UserKeyGenerator(new RandomKeyGenerator())).As<IKeyGenerator>();
                    builder.RegisterType<DefaultHmacProvider>().As<IHmacProvider>();
                    builder.RegisterType<CryptoService>().As<ICryptoService>();
                    builder.RegisterType<CommandUpdateUser>().As<ICommandUpdateIdentity<User>>();
                    builder.RegisterType<CommandDeleteUser>().As<ICommandDeleteIdentity<User>>();



                };
            }
        }
    }
}