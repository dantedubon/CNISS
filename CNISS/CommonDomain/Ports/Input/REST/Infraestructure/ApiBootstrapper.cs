using System;
using CNISS.Bootstraper;
using Nancy;
using Nancy.Conventions;

namespace CNISS.CommonDomain.Ports.Input.REST.Infraestructure
{
    public class ApiBootstrapper : Bootstrapper
    {
        public ApiBootstrapper()
        {
            addBootstrapperTask(new ConfigureRoleDependencies());
            addBootstrapperTask(new ConfigureUserDependencies());
            addBootstrapperTask(new ConfigureDataBase());
        }
        protected override void RequestStartup(Autofac.ILifetimeScope container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
        {

            pipelines.AfterRequest.AddItemToEndOfPipeline(AddCorsHeaders());

            pipelines.OnError.AddItemToEndOfPipeline((ctx, err) =>
                HandleExceptions(err, ctx)
                );

            base.RequestStartup(container, pipelines, context);
        }

        static Response HandleExceptions(Exception err, NancyContext ctx)
        {
            if (ctx.Response != null) return ctx.Response;
            ctx.Response = new Response() { };
            AddCorsHeaders()(ctx);

            return ctx.Response;
        }

        static Action<NancyContext> AddCorsHeaders()
        {
            return x =>
            {
                var response = x.Response;
                response.WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                response.WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                response.WithHeader("Access-Control-Max-Age", "1728000");
                response.WithHeader("Access-Control-Allow-Origin", "*");
            };
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("App"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Assets"));
        }
    }
}