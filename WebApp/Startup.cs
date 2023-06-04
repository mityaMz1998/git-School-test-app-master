using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{
    public class Startup
    {
        // TODO 0: Something broken in ConfigureServices.
        // Don't focus attention on it right now, you will faced with problem in process of meeting the challenges TODOs
        // Pay attention to different possible solutions of the problem

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //!
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddSingleton<IAccountDatabase, AccountDatabaseStub>();
            services.AddSingleton<IAccountCache, AccountCache>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //!
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Get}/{id?}");
            });
        }
    }
}