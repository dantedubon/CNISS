using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.ModelBinding;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule
{
    public class AuthModule:NancyModule
    {

        public AuthModule(ITokenizer tokenizer, IAuthenticateUser authenticateUser):base("/auth")
        {
            Post["/movil/"] = parameters =>
            {
                const int nivelUserMovil = 2;
                var userRequest = this.Bind<UserRequest>();
                if (!userRequest.isValidForAuthenticate())
                {
                    return HttpStatusCode.Unauthorized;
                }
                var user = new User(userRequest.Id,userRequest.firstName,userRequest.secondName, userRequest.password,userRequest.mail, new RolNull());
                var validUser = authenticateUser.isValidUser(user,nivelUserMovil);
                if (!validUser)
                {
                    return HttpStatusCode.Unauthorized;
                }
                
                var userIdentity = new UserIdentityMovil(user);

                var token = tokenizer.Tokenize(userIdentity, Context);
                

                return new
                {
                    Token = token
                };

            };

            //Post["/{id}/{password}"] = x =>
            //{
            //    string userName = x.id;
            //    string password = x.password;

            //    var userIdentity = new UserIdentity() {UserName = userName, Claims = new List<String> {"admin"}};
            //    if (userName == "No Aceptado")
            //    {
            //        return HttpStatusCode.Unauthorized;
            //    }
            //    var token = tokenizer.Tokenize(userIdentity, Context);

            //    return new
            //    {
            //        Token = token,
            //    };
            //};

            Get["/validation"] = _ =>
            {
                this.RequiresClaims(new[] { "movil" });
                return Context.CurrentUser.UserName;
            };
        }
    }
}