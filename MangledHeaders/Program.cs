namespace MangledHeaders
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    ///     Defines the entry point for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Defines the entry point for the class.
        /// </summary>
        /// <param name="args">
        ///     Arguments passed in at application start.
        /// </param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     A method for constructing an <see cref="IWebHost"/> instance using the arguments provided in <paramref name="args"/>.
        /// </summary>
        /// <param name="args">
        ///     A collection of arguments to use when constructing a <see cref="IWebHostBuilder"/>.
        /// </param>
        /// <returns>
        ///     An <see cref="IWebHost"/> instance.
        /// </returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel();
    }
}
