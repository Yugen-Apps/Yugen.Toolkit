using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Options = UwpCommunity.Web.TokenProvider.Models.Options;

namespace UwpCommunity.Web.TokenProvider
{
    /// <summary>
    /// Token generator middleware component which is added to an HTTP pipeline.
    /// This class is not created by application code directly,
    /// instead it is added by calling the <see cref="AppBuilderExtensions.UseSimpleTokenProvider"/>
    /// extension method.
    /// </summary>
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly Options _options;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        public Middleware(
            RequestDelegate next,
            IOptions<Options> options,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<Middleware>();

            _options = options.Value;
            ThrowIfInvalidOptions(_options);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
                return _next(context);

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            _logger.LogInformation("Handling request: " + context.Request.Path);

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var identity = await _options.IdentityResolver(username, password);
            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            var token = JwtTokenHandler.BuildJwt(username);

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(token, _serializerSettings));
        }

        private static void ThrowIfInvalidOptions(Options options)
        {
            if (string.IsNullOrEmpty(options.Path))
                throw new ArgumentNullException(nameof(Options.Path));

            if (options.IdentityResolver == null)
                throw new ArgumentNullException(nameof(Options.IdentityResolver));
        }
    }
}