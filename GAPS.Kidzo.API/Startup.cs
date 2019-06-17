using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.Base.Repository;
using HelloParent.Base.Repository.Interfaces;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using HelloParent.Services;
using HelloParent.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GAPS.Kidzo.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            AppSettings.Initialize(env);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("HelloParentPolicy",
                    //builder => builder.WithOrigins("https://localhost:44381")
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowCredentials()
                                      .AllowAnyHeader());
            });
          
            services.AddMvc();

            #region Add Dependency
            //// Service ///
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookTranscationService, BookTranscationService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IMapperService, MapperService>();
            services.AddTransient<IMapperService, MapperService>();

            /// Repository ///

            services.AddTransient(typeof(IRepository<>), typeof(MongoRepository<>));
            services.AddTransient<IBookRepository, BookRepository>();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("HelloParentPolicy");
            app.UseMvc();
        }
    }
}
