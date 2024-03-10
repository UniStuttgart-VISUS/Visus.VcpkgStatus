// <copyright file="StatusBadgeController.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2024 Visualisierungsinstitut der Universität Stuttgart.
// Licenced under the MIT licence. See LICENCE.txt.
// </copyright>
// <author>Christoph Müller</author>

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Visus.VcpkgStatus.DataModels;
using Visus.VcpkgStatus.Options;
using Visus.VcpkgStatus.Properties;


namespace Visus.VcpkgStatus.Controllers {

    /// <summary>
    /// Generates a status badge for a VCPKG port.
    /// </summary>
    [ApiController]
    [Route("/")]
    public class StatusBadgeController : ControllerBase {

        #region Public constructors
        public StatusBadgeController(
                IHttpClientFactory clientFactory,
                IOptions<AppearenceOptions> appearenceOptions,
                IMemoryCache cache,
                IOptions<CachingOptions> cachingOptions,
                IOptions<RequestOptions> requestOptions,
                ILogger<StatusBadgeController> logger) {
            this._appearenceOptions = appearenceOptions?.Value
                ?? throw new ArgumentNullException(nameof(appearenceOptions));
            this._cache = cache
                ?? throw new ArgumentNullException(nameof(cache));
            this._cachingOptions = cachingOptions?.Value
                ?? throw new ArgumentNullException(nameof(cachingOptions));
            this._clientFactory = clientFactory
                ?? throw new ArgumentNullException(nameof(clientFactory));
            this._logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            this._requestOptions = requestOptions?.Value
                ?? throw new ArgumentNullException(nameof(requestOptions));
        }
        #endregion

        /// <summary>
        /// Gets the status badge for the given port.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        [HttpGet("{port}")]
        [Produces("image/svg+xml")]
        public async Task<IActionResult> GetAsync(string port) {
            if (string.IsNullOrWhiteSpace(port)) {
                return this.BadRequest();
            }

            if (!this._cache.TryGetValue(port, out string? retval)) {
                var client = this._clientFactory.CreateClient();
                var url = this._requestOptions.GetUrl(port);
                this._logger.LogInformation(Resources.InfoRequestPortFile,
                    url, port);
                var portFile = await client.GetFromJsonAsync<PortFile>(url);
                if (portFile == null) {
                    return this.BadRequest();
                }

                var badge = new StatusBadge(port, portFile.Version);
                retval = badge.ToSvg(this._appearenceOptions);

                this._cache.Set(port, retval, this._cachingOptions);
            }

            return this.Content(retval, "image/svg+xml");
        }

        #region Private fields
        private readonly AppearenceOptions _appearenceOptions;
        private readonly IMemoryCache _cache;
        private readonly CachingOptions _cachingOptions;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<StatusBadgeController> _logger;
        private readonly RequestOptions _requestOptions;
        #endregion
    }
}
