namespace MangledHeaders.Controllers
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    ///     The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly HttpClient httpClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="configuration">
        ///     The top-level <see cref="IConfiguration"/> instance.
        /// </param>
        /// <param name="logger">
        ///     An injected <see cref="ILogger{T}"/> instance.
        /// </param>
        /// <param name="httpClientFactory">
        ///     An injected <see cref="IHttpClientFactory"/> instance.
        /// </param>
        public HomeController(
            IConfiguration configuration,
            ILogger<HomeController> logger,
            IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.httpClient = httpClientFactory.CreateClient("header-propagating-client");
        }

        /// <summary>
        ///     The only view.
        /// </summary>
        /// <returns>
        ///     A view with a table of request statuses.
        /// </returns>
        [HttpGet("")]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        ///     Returns the request headers using /echoheaders.
        /// </summary>
        /// <returns>
        ///     Request headers as JSON in response body.
        /// </returns>
        [HttpGet("echoheadersproxy")]
        public ActionResult EchoHeadersProxy()
        {
            return this.Ok(this.httpClient.GetStringAsync($"http://{this.Request.Host}/echoheaders").Result);
        }

        /// <summary>
        ///     Returns the request headers.
        /// </summary>
        /// <returns>
        ///     Request headers as JSON in response body.
        /// </returns>
        [HttpGet("echoheaders")]
        public ActionResult EchoHeaders()
        {
            return this.Ok(new Dictionary<string, StringValues>(this.Request.Headers));
        }
    }
}
