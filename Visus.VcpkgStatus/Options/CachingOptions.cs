// <copyright file="CachingOptions.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2024 Visualisierungsinstitut der Universität Stuttgart.
// Licenced under the MIT licence. See LICENCE.txt.
// </copyright>
// <author>Christoph Müller</author>

using Microsoft.Extensions.Caching.Memory;
using System;


namespace Visus.VcpkgStatus.Options {

    /// <summary>
    /// Configures the in-memory caching for badges.
    /// </summary>
    public sealed class CachingOptions {

        /// <summary>
        /// The name of the configuration section.
        /// </summary>
        public const string SectionName = "Caching";

        /// <summary>
        /// Convert <see cref="CachingOptions"/> to
        /// <see cref="MemoryCacheEntryOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        public static implicit operator MemoryCacheEntryOptions(
                CachingOptions options)
            => new MemoryCacheEntryOptions() {
                AbsoluteExpirationRelativeToNow = options.ExpireAfter
            };

        /// <summary>
        /// Gets or sets the time after which a cached badge expires.
        /// </summary>
        public TimeSpan ExpireAfter { get; set; } = TimeSpan.FromMinutes(1);

    }
}
