using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LiveHappy.Infrastructure;
using LiveHappy.Domain.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using LiveHappy.Infrastructure.Services;
using LiveHappy.Infrastructure.Policies;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using LiveHappy.Infrastructure.Hubs;

namespace LiveHappy.Application
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DevConnection"),
                    x => x.MigrationsAssembly("LiveHappy.Infrastructure")));

            services.AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Home/NotFoundPage";
            });

            services.AddAuthorization(options => {
                options.AddPolicy("AdminPolicy",
                    builder => builder.RequireRole("Admin"));
                options.AddPolicy("FeaturePolicy",
                    builder => builder.RequireRole("Admin", "VIP"));
                options.AddPolicy("ReadPolicy",
                    builder => builder.RequireRole("Admin", "VIP", "Member"));
                options.AddPolicy("AdultPolicy",
                        policy => policy.Requirements.Add(new MinimumAgeRequirement(18)));
            });

            // Move them to container and migrate to Ninject
            services.AddScoped<SignInManager<User>, SignInManager<User>>();
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddScoped<IEmailSender, EmailSender>();

            // Add Swagger

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "experiment",
                    template: "experiments/{experimentName?}",
                    defaults: new { controller = "Home", action = "Experiment" })
                .MapRoute(
                    name: "blog",
                    template: "blog/{**articlePath}",
                    defaults: new { controller = "Home", action = "Blog" })
                .MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
