using System;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Application.Comandos
{
    public class CommandInsertUser : CommandInsertIdentity<User>
    {
        private ICryptoService _cryptoService;

        public CommandInsertUser(ICryptoService cryptoService, IUserRepositoryCommands repository, Func<IUnitOfWork> unitOfWork)
            : base(repository, unitOfWork)
        {
            _cryptoService = cryptoService;
        }

       
    

        public override void execute(User user)
        {
            user.password = _cryptoService.getEncryptedText(user.password);
            user.userKey = _cryptoService.getKey();
            base.execute(user);

        }

    }
}