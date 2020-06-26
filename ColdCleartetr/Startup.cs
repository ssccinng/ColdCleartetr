using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ColdCleartetr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("https://tetr.io").WithMethods("POST", "OPTION").WithHeaders("Content-Type") ;
                });
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("Policy1",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://example.com",
            //                                "http://www.contoso.com");
            //        });

            //    options.AddPolicy("AnotherPolicy",
            //        builder =>
            //        {
            //            builder.WithOrigins("https://tetr.io")

            //                                .AllowAnyMethod();
            //        });
            //});
            services.AddControllers();
                //.ConfigureApiBehaviorOptions(options =>
                //{
                //    options.SuppressConsumesConstraintForFormFileParameters = true;
                //    options.SuppressInferBindingSourcesForParameters = true;
                //    options.SuppressModelStateInvalidFilter = true;
                //    options.SuppressMapClientErrors = true;
                //    options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                //        "https://httpstatuses.com/404";
                //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            //app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
