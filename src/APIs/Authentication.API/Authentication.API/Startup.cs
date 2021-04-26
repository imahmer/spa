using Authentication.API.DomainModels.Config;
using Authentication.API.DomainModels.Entities.Identity;
using Authentication.API.Infrastructure.Configuration;
using Authentication.API.Infrastructure.Services;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Authentication.API
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddTransient<IProfileService, ProfileService>();

            services.AddIdentity<User, Role>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            //EnsureSeedData(services);
            services.AddControllersWithViews();



            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
           .AddDeveloperSigningCredential()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryIdentityResources(Config.IdentityResources)
           .AddAspNetIdentity<User>();
            services.AddAuthentication()
                .AddIdentityServerJwt();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        public static void EnsureSeedData(IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var user = userMgr.FindByNameAsync("ahmer").Result;
                    if (user == null)
                    {
                        user = new User
                        {
                            EmployeeId = 1,
                            UserName = "ahmer",
                            UserPassword = "Default@1",
                            FirstName = "Ahmer",
                            MiddleName = "Ali",
                            LastName = "Ahsan",
                            Address1 = "Loreum ispum address",
                            Email = "imahmer@outlook.com",
                            NormalizedEmail = "IMAHMER@OUTLOOK.COM",
                        };
                        var result = userMgr.CreateAsync(user, "Default@1").Result;
                        if (!result.Succeeded)
                        {
                            throw new System.Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(user, new Claim[]{
                        new Claim("EmployeeId", user.EmployeeId.ToString()),
                        new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(JwtClaimTypes.Email, user.Email),
                        //new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                        if (!result.Succeeded)
                        {
                            throw new System.Exception(result.Errors.First().Description);
                        }
                    }
                }
            }
        }
    }
}
