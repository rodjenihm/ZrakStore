using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using ZrakStore.Core.Config;
using ZrakStore.Data.Repositories;
using ZrakStore.Data.Utilities;
using ZrakStore.Services;

namespace ZrakStore.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtConfigSection = Configuration.GetSection("JwtConfig");
            services.Configure<JwtConfig>(jwtConfigSection);

            var connectionString = new DapperConnectionString(Configuration.GetConnectionString("AuthDb"));
            services.AddSingleton(connectionString);

            services.AddScoped<IAsyncUserRepository, DapperUserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAsyncRoleRepository, DapperRoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();


            var appSettings = jwtConfigSection.Get<JwtConfig>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            //services.AddAuthentication(configureOptions =>
            //{
            //    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(configureOptions =>
            //{
            //    configureOptions.RequireHttpsMetadata = false;
            //    configureOptions.SaveToken = true;
            //    configureOptions.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidIssuer = appSettings.Issuer,
            //        ValidAudience = appSettings.Audience,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //    };
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(configureOptions =>
                {
                    configureOptions.Cookie.Name = "Cookie.Zrak";
                    configureOptions.LoginPath = "/User/Login";
                    configureOptions.AccessDeniedPath = "/Home/AccessDenied";
                });

            services.AddCors();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            //app.UseStatusCodePages(async context =>
            //{
            //    var response = context.HttpContext.Response;

            //    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            //    {
            //        response.Redirect("/User/Login");
            //    }

            //    if (response.StatusCode == (int)HttpStatusCode.Forbidden)
            //    {
            //        response.Redirect("/Home/NotAuthorized");
            //    }

            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
