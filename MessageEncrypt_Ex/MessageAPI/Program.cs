using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.Entities;
using MessageServices.Authentication;
using MessageServices.Authorization;
using MessageServices.HasingService;

namespace MessageAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Call UseServiceProviderFactory on the Host sub property 
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            // Call ConfigureContainer on the Host sub property 
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<MessageEncryptContext>().AsSelf();
                //MessageEncryptContext ms = new MessageEncryptContext();
                builder.RegisterType<HashingService>().As<IHashingService>();
                //IHashService hs = new HashService()
                builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
                //ILoginService ls = new LoginService()
                builder.RegisterType<LoginService>().As<ILoginService>();
                //ItokenService Is = new TokenService()
                builder.RegisterType<TokenService>().As<ITokenService>();
            });
            // Configure the HTTP request pipeline.
            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
