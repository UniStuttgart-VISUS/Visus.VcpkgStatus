// <copyright file="PortFile.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2024 Visualisierungsinstitut der Universität Stuttgart.
// Licenced under the MIT licence. See LICENCE.txt.
// </copyright>
// <author>Christoph Müller</author>


namespace Visus.VcpkgStatus.DataModels {

    /// <summary>
    /// Represents the port description used by vcpkg.
    /// </summary>
    public sealed class PortFile {

        /// <summary>
        /// Gets or sets the description of the port.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version of the port.
        /// </summary>
        public int? PortVersion { get; set; }

        /// <summary>
        /// Gets or sets the version of the packaged library.
        /// </summary>
        public string Version { get; set; }
        
    }
}
