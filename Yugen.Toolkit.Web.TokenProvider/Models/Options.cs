using System;
using System.Threading.Tasks;

namespace Yugen.Toolkit.Web.TokenProvider.Models
{
    /// <summary>
    /// Provides options for <see cref="Middleware"/>.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// The relative request path to listen on.
        /// </summary>
        /// <remarks>The default path is <c>/token</c>.</remarks>
        public string Path { get; set; } = "/token";
        
        /// <summary>
        /// Resolves a user identity given a username and password.
        /// </summary>
        public Func<string, string, Task<string>> IdentityResolver { get; set; }
    }
}