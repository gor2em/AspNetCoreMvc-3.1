using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using glory.BookStore.Data;
using glory.BookStore.Models;
using glory.BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace glory.BookStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //all the features and despendencies...

            //services.AddMvc();//controller model kullanýlabilir.
            services.AddControllersWithViews();//controller model view kullanýlabilir.

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));  //connection string hard code silindi #82
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>(); //#91


            //#95 identity options
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            });

            //#99 redirect user login page
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = _configuration["Application:LoginPath"];
            });

#if DEBUG
            //this code will learn only in the debug environment.
            //this will work in the production environment.
            services.AddRazorPages().AddRazorRuntimeCompilation();//3.1.3 ekledin. packages> runtimecompilation

            //.AddViewOptions(option =>
            // {
            //     option.HtmlHelperOptions.ClientValidationEnabled = false; //client side tarafý false yapar lakin buna gerek yok.
            // });


#endif



            //dependency injection??
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccounRepository>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //http line
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();//statik dosyalar için... image geldi


            app.UseRouting(); //routing enable 
                              //routing #76

            app.UseAuthentication();//identity #91
            app.UseAuthorization();//#98





            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapDefaultControllerRoute(); //defaullt home index controllerine gider.

                //kendi route umuz
                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    pattern: "bookApp/{controller=home}/{action=index}/{id?}");
            });


        }
    }
}
