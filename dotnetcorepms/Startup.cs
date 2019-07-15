using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcorepms.Filters;
using dotnetcorepms.Interfaces;
using dotnetcorepms.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dotnetcorepms
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //if (env.IsDevelopment())
            //{
            //    // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            //    builder.AddUserSecrets<Startup>();
            //}

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new CustomExceptionFilterAttribute());
                options.ReturnHttpNotAcceptable = true;
                // options.OutputFormatters = xml
            })
            .AddJsonOptions(options =>
            {
                //For Maintaining Json Format 
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            // For FileUpload
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
                x.ValueLengthLimit = int.MaxValue; //not recommended value
                x.MemoryBufferThreshold = Int32.MaxValue;
            });

            // For Setting Session Timeout
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            //Getting Connection String From Database
            var connection = Configuration.GetConnectionString("DatabaseConnection");

            // UseRowNumberForPaging for Using Skip and Take in .Net Core
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection, b => b.UseRowNumberForPaging()));

            services.AddTransient<IUsers, UsersRepo>();
            services.AddTransient<IRoles, RolesRepo>();
            services.AddTransient<ILogin, LoginRepo>();
            services.AddTransient<IForum, ForumRepo>();
            services.AddTransient<IDocuments, DocumentsRepo>();
            services.AddTransient<INotes, NotesRepo>();
            services.AddTransient<ICommon, CommonRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // Using Static Files
            app.UseStaticFiles();
            // Enabling Session
            app.UseSession();
            // Routing
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
