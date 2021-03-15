using Excepticon.AspNetCore;
using Excepticon.Extensions;
using inventory.Interfaces;
using inventory.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using static inventory.Middleware.Middleware;

namespace inventory
{
    /// <summary>
    /// Start 12-03-2021
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///  Constructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api Form Inventory",
                    Description = "Api For Inventory",
                    TermsOfService = new Uri("https://inventory.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael velez",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/rafael-velez-alvarez-a9644514/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://creativecommons.org/choose/?lang=us"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //string connectionString = Configuration.GetConnectionString("default");
            //IServiceCollection serviceCollection = services.AddDbContext<inventoryDBContext>(c => c.UseSqlServer(connectionString));
            IServiceCollection serviceCollection = services.AddDbContext<inventoryDBContext>(c => c.UseInMemoryDatabase(databaseName: "inventory"));

            services.AddScoped<iRepository, Repository<inventoryDBContext>>();
            services.AddExcepticon();
            services.AddControllers().AddNewtonsoftJson(options =>
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "inventory v1"));
            }

            // Hook in the global error-handling middleware
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            //// Middleware to report exceptions.
            app.UseExcepticon();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
