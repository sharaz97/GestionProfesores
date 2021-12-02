using GestionProfesores.Api.Authentication;
using GestionProfesores.Model.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GestionProfesores.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            AddDbContextToService(services);
            AddBasicAuthenticationToService(services);
        }

        void AddBasicAuthenticationToService(IServiceCollection services) => services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        void AddDbContextToService(IServiceCollection services) => services.AddDbContext<GestionProfesoresContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GestionProfesoresContext gestionProfesoresContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureDb(gestionProfesoresContext);
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        void ConfigureDb(GestionProfesoresContext gestionProfesoresContext)
        {
            if(!gestionProfesoresContext.Database.EnsureCreated())
            {
                return;
            }
            gestionProfesoresContext.Seed();
        }
    }
}