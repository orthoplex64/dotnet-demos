namespace MangledHeaders
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Defines methods for configuring the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">
        ///     An <see cref="IConfiguration"/> instance to use for configuring the application.
        /// </param>
        /// <param name="loggerFactory">
        ///     An <see cref="ILoggerFactory"/> instance to use for logging from the application.
        /// </param>
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this.LoggerFactory = loggerFactory;
        }

        /// <summary>
        ///     Gets an <see cref="IConfiguration"/> instance to use for configuring the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        private ILoggerFactory LoggerFactory { get; }

        /// <summary>
        ///     Configures the services in use for the application.
        /// </summary>
        /// <param name="services">
        ///     An <see cref="IServiceCollection"/> instance to use to configure the services.
        /// </param>
        /// <remarks>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(builder =>
                    builder
                        .AddDebug()
                        .AddConsole())
                .AddHeaderPropagation(options =>
                {
                    options.Headers.Add("X-Some-Header");
                })
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                });

            services
                .AddHttpClient("header-propagating-client")
                .AddHeaderPropagation();
        }

        /// <summary>
        ///     Configures the application using the provided <see cref="IApplicationBuilder"/> instance.
        /// </summary>
        /// <param name="app">
        ///     An <see cref="IApplicationBuilder"/> instance to configure.
        /// </param>
        /// <param name="env">
        ///     An <see cref="IHostingEnvironment"/> instance to look up environment information.
        /// </param>
        /// <remarks>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .Use(async (context, next) => await next.Invoke())
                .UseHeaderPropagation()
                .UseMvc();
        }
    }
}
