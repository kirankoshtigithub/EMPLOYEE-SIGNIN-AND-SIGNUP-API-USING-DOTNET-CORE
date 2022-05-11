using EMPLOYEE_SIGNING_AND_SIGNUP_API.DataAccesslayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EMPLOYEE_SIGNING_AND_SIGNUP_API
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

            services.AddControllers();
            services.AddScoped<IAuthDL, AuthDL>(); // Added_Dependancy_Injection Kiran_Koshti
            services.AddSwaggerGen();// Added Swagger Kiran_Koshti
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SAMPLE_API_MANAGER v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();// Added Swagger Kiran_Koshti

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API v1");
                options.RoutePrefix = string.Empty;
            });// Added Swagger Kiran_Koshti


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
