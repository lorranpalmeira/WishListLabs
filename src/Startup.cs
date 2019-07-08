using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WishListLabs.Config;
using WishListLabs.Models;
using WishListLabs.Repository;

namespace WishListLabs
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

            // MongoDb Config
            ContextMongoDb.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            ContextMongoDb.DatabaseName = Configuration.GetSection("MongoConnection:Database").Value;
            ContextMongoDb.IsSSL = Convert.ToBoolean(this.Configuration.GetSection("MongoConnection:IsSSL").Value);
            // End MongoDb Config


            //Dependency Injection
            services.AddSingleton<IContextMongoDb, ContextMongoDb>();
            services.AddTransient<IBaseRepository<Product>, BaseRepository<Product>>( (ctx) =>
            {
                var context = ctx.GetService<IContextMongoDb>();
                return new BaseRepository<Product>(context.Product);
            });
            
            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>((ctx) =>
            {
                var context = ctx.GetService<IContextMongoDb>();
                return new BaseRepository<User>(context.User);
            });

            services.AddTransient<IBaseRepository<Wishes>, BaseRepository<Wishes>>((ctx) =>
            {
                var context = ctx.GetService<IContextMongoDb>();
                return new BaseRepository<Wishes>(context.Wishes); 
            });       

            services.AddTransient<IWishesRepository,WishesRepository>((ctx) =>
            {
                var context = ctx.GetService<IContextMongoDb>();
                return new WishesRepository(context.Wishes, context.Product); 
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
