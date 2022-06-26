
using AutoMapper;
using Infrastructure.DependencyInjectionExtensions;
using Infrastructure.Domain.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.WellKnownNames;

namespace WebApi
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
            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Api Documentation",
                    Description = "This is the api documentation",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "",
                        Email = "",
                        Url = ""
                    }
                });
            });


            services.AddAutoMapperWithProfiles();
            services.AddEntitiesServicesAndRepositories();
            services.AddCustomApplicationServices();

            //Unit of Work Concrete Implementation Configuration
            services.AddDbContext<UnitOfWorkContainer>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(AppSettings.DefaultConnectionString), sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(30);
                    sqlServerOptions.EnableRetryOnFailure(3);
                }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(options => {
                options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Check out api documentation");
            });
        }
    }
}
