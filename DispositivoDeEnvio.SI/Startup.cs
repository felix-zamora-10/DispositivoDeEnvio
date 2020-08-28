using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DispositivoDeEnvio.SI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            ConfiguraPlataformaCruzada(services);
            AgregaInyeccionDeDependencias(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CORS_SECURITY_CONFIGURATION");
            app.UseMvc();
        }

        private static void ConfiguraPlataformaCruzada(IServiceCollection services) {
            services.AddCors(builder => builder
            .AddPolicy("CORS_SECURITY_CONFIGURATION",
            policyBuilder => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));
        }

        private static void AgregaInyeccionDeDependencias(IServiceCollection services) {
            var inyeccionDeDependencias = new InyeccionDeDependencias(services);
            inyeccionDeDependencias.InyectaDependencias();
        }
    }
}
