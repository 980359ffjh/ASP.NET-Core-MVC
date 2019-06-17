using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RongProject1.Entities;
using RongProject1.Services;

namespace RongProject1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json")  // connect to appsettings.json file
                        .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<RongDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Test")));
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<RongDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Error!")
                });
            }
            //app.UseDefaultFiles();    // can change localhost:xxxxx to index.html's content
            //app.UseStaticFiles();   // to read myself .html file(localhost:xxxxx/index.html)
            app.UseFileServer();    // including the above two function

            //app.UseWelcomePage("/welcome"); // 依照"/"所下的字串才會進入welcome page，沒有輸入預設就會進入
            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/welcome"
            //});

            //app.Run(async (context) =>
            //{
            //    var message = greeter.GetGreeting();
            //    await context.Response.WriteAsync(message);
            //});

            //app.UseMvcWithDefaultRoute();

            app.UseNodeModules(env.ContentRootPath);

            //使用 Identity
#pragma warning disable CS0618 // 類型或成員已經過時
            //app.UseIdentity();
#pragma warning restore CS0618 // 類型或成員已經過時
            app.UseAuthentication();

            //to use mvc controller
            app.UseMvc(ConfigureRoutes);

            //app.Run(context => context.Response.WriteAsync("Maybe something is error, so not found!"));
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // HomeController's Home / Index -> controller / action
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
