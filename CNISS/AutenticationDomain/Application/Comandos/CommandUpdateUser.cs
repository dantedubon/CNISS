using System;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Application.Comandos
{
    public class CommandUpdateUser : CommandUpdateIdentity<User>
    {
        private readonly ICryptoService _cryptoService;

        public CommandUpdateUser(ICryptoService cryptoService, IUserRepositoryCommands repository, Func<IUnitOfWork> unitOfWork)
            : base(repository, unitOfWork)
        {
            _cryptoService = cryptoService;
        }




        public override void execute(User user)
        {
            user.Password = _cryptoService.getEncryptedText(user.Password);
            user.UserKey = _cryptoService.getKey();
            base.execute(user);

        }

    }
}