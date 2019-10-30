using System;
using Microsoft.AspNetCore.Builder;
using Yugen.Toolkit.Web.TokenProvider.Models;

//https://github.com/nbarbettini/SimpleTokenProvider
namespace Yugen.Toolkit.Web.TokenProvider
{
    /// <summary>
    /// Adds a token generation endpoint to an application pipeline.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="Middleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables token generation capabilities.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="options">A  <see cref="Options"/> that specifies options for the middleware.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseSimpleTokenProvider(this IApplicationBuilder app, Options options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<Middleware>(Microsoft.Extensions.Options.Options.Create(options));
        }
    }
}