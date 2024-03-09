// <copyright file="RequestOptions.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2024 Visualisierungsinstitut der Universität Stuttgart.
// Licenced under the MIT licence. See LICENCE.txt.
// </copyright>
// <author>Christoph Müller</author>


namespace Visus.VcpkgStatus.Options {

    /// <summary>
    /// Configures the requests the application makes to determine the latest
    /// version of the port.
    /// </summary>
    public sealed class RequestOptions {

        /// <summary>
        /// The name of the configuration section.
        /// </summary>
        public const string SectionName = "Requests";

        #region Public properties
        /// <summary>
        /// Gets or sets the template for searching the port file.
        /// </summary>
        public string RequestTemplate { get; set; }
            = "https://raw.githubusercontent.com/microsoft/vcpkg/master/ports/{0}/vcpkg.json";
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the URL for the specified port.
        /// </summary>
        /// <param name="port">The name of the port to retrieve.</param>
        /// <returns>The URL to request for the port file.</returns>
        public string GetUrl(string port) {
            return string.Format(this.RequestTemplate, port);
        }
        #endregion
    }
}
