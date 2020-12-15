using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.Service.Utility.RandomGeneration;
using WebCasino.Service.Utility.Wrappers;
using WebCasino.Web.Utilities;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (this.Environment.IsDevelopment())
            {
                services.AddDbContext<CasinoContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DeveloperConnection")));
            }

            else
            {
                services.AddDbContext<CasinoContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CasinoContext>()
                .AddDefaultTokenProviders();



            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IUserWrapper, UserWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAdminDashboard, AdminDashboardService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IGameService, GameService>();


            services.AddScoped<IRandomGenerator, RandomGenerator>();
            services.AddHttpClient<IHttpWrapper, HttpWrapper>();

            services.AddSingleton<IAPIRequester, APIRequester>();

            services.AddSingleton<ICurrencyRateApiService, CurrencyRateApiService>();

            services.AddSingleton<IDateWrapper, DateWrapper>();

            // Add application services.

            services.AddMvc();

            //TempData 
            services.AddSession();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.ImportantExceptionHandling();

            app.UseStaticFiles();

            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "admin",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}