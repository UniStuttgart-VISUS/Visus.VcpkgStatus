// <copyright file="StatusBadgeController.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2024 Visualisierungsinstitut der Universität Stuttgart.
// Licenced under the MIT licence. See LICENCE.txt.
// </copyright>
// <author>Christoph Müller</author>

using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Visus.VcpkgStatus.Options;


namespace Visus.VcpkgStatus.DataModels {

    /// <summary>
    /// Represents a VCPKG status badge.
    /// </summary>
    public sealed class StatusBadge {

        #region Public constructors
        /// <summary>
        /// Initialises the instance.
        /// </summary>
        /// <param name="port">The name of the port.</param>
        /// <param name="version">The latest version of the port.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public StatusBadge(string port, string version) {
            this.Port = port
                ?? throw new ArgumentNullException(nameof(port));
            this.Version = version
                ?? throw new ArgumentNullException(nameof(version));
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the current version of the port.
        /// </summary>
        public string Version { get; set; }
        #endregion

        /// <summary>
        /// Converts the badge to an SVG string.
        /// </summary>
        /// <param name="appearence"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string ToSvg(AppearenceOptions appearence) {
            _ = appearence
                ?? throw new ArgumentNullException(nameof(appearence));

            var portSize = 0.0f;
            var versionSize = 0.0f;
            try {
                var fonts = new FontCollection();
                var font = fonts.Add(appearence.MeasureFont)
                    .CreateFont(appearence.FontSize);
                Measure(this.Port, this.Version, font,
                    out portSize, out versionSize);
            } catch {
                // If everything fails, use the hardcoded width.
                Measure(this.Port, this.Version,
                    out portSize, out versionSize);
            }

            // Spacing between elements.
            var spacing = 3;
            // Begin of primary colour box.
            var primaryBegin = 0.0f;
            // Begin of port name.
            var portBegin = spacing + 14 + spacing;
            // End of primary colour box.
            var primaryEnd = portBegin + portSize;
            // Begin of version text.
            var versionBegin = primaryEnd + spacing;
            // End of secondary colour box.
            var secondaryEnd = versionBegin + versionSize + spacing;

            return
$"""
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<svg xmlns="http://www.w3.org/2000/svg"
    style="shape-rendering: geometricPrecision; image-rendering: optimizeQuality; fill-rule: evenodd; clip-rule: evenodd"
    width="163"
    height="{appearence.Height}"
    fill="None">
    <g font-family="{string.Join(",", appearence.FontFamily)}" font-size="{appearence.FontSize}" fill="#000000">
        <rect x="{primaryBegin.ToString(CultureInfo.InvariantCulture)}" y="0" height="{appearence.Height.ToString(CultureInfo.InvariantCulture)}" width="{(primaryEnd - primaryBegin).ToString(CultureInfo.InvariantCulture)}" rx="2.5" ry="2.5" stroke-width="0" fill="{appearence.PrimaryBackground}" />
        <rect x="{(primaryEnd - 2.5).ToString(CultureInfo.InvariantCulture)}" y="0" height="{appearence.Height.ToString(CultureInfo.InvariantCulture)}" width="2.5" stroke-width="0" fill="{appearence.PrimaryBackground}" />
        <rect x="{primaryEnd.ToString(CultureInfo.InvariantCulture)}" y="0" height="{appearence.Height.ToString(CultureInfo.InvariantCulture)}" width="{(secondaryEnd - primaryEnd).ToString(CultureInfo.InvariantCulture)}" rx="2.5" ry="2.5" stroke-width="0" fill="{appearence.SecondaryBackground}" />
        <rect x="{primaryEnd.ToString(CultureInfo.InvariantCulture)}" y="0" height="{appearence.Height.ToString(CultureInfo.InvariantCulture)}" width="2.5" stroke-width="0" fill="{appearence.SecondaryBackground}" />
        <text x="{portBegin.ToString(CultureInfo.InvariantCulture)}" y="14" fill="{appearence.PrimaryForeground}">{this.Port}</text>
        <text x="{versionBegin.ToString(CultureInfo.InvariantCulture)}" y="14" fill="{appearence.SecondaryForeground}">v{this.Version}</text>
    </g>
    <g>{appearence.GetLogo(3, 2, 14)}</g>
</svg>
""";
        }

        #region Private class methods
        private static bool TryMeasure(string port, string version,
                string fontFamily, float fontSize,
                out float portSize, out float versionSize) {
            try {
                var font = SystemFonts.CreateFont(fontFamily, fontSize);
                Measure(port, version, font, out portSize, out versionSize);
                return true;
            } catch {
                portSize = 0;
                versionSize = 0;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Measure(string port, string version, Font font,
                out float portSize, out float versionSize) {
            var opts = new TextOptions(font) { Dpi = 92.0f };
            portSize = TextMeasurer.MeasureSize(port, opts).Width;
            versionSize = TextMeasurer.MeasureSize(version, opts).Width;
        }

        private static void Measure(string port, string version,
                out float portSize, out float versionSize) {
            #region Lookup table from https://github.com/dustinmoris/CI-BuildStats/blob/18c41ea721b1b4f15b7c1ec201566ef24a8474f6/src/BuildStats/TextSize.fs
            Dictionary<char, float> widths = new() {
                { 'a', 7 },
                { 'b', 7 },
                { 'c', 6 },
                { 'd', 7 },
                { 'e', 7 },
                { 'f', 3 },
                { 'g', 7 },
                { 'h', 7 },
                { 'i', 3 },
                { 'j', 3 },
                { 'k', 6 },
                { 'l', 3 },
                { 'm', 10 },
                { 'n', 7 },
                { 'o', 7 },
                { 'p', 7 },
                { 'q', 7 },
                { 'r', 4 },
                { 's', 6 },
                { 't', 3 },
                { 'u', 7 },
                { 'v', 6 },
                { 'w', 9 },
                { 'x', 6 },
                { 'y', 6 },
                { 'z', 6 },
                { 'A', 8 },
                { 'B', 8 },
                { 'C', 9 },
                { 'D', 9 },
                { 'E', 8 },
                { 'F', 8 },
                { 'G', 9 },
                { 'H', 9 },
                { 'I', 3 },
                { 'J', 6 },
                { 'K', 8 },
                { 'L', 7 },
                { 'M', 10 },
                { 'N', 9 },
                { 'O', 10 },
                { 'P', 8 },
                { 'Q', 10 },
                { 'R', 9 },
                { 'S', 8 },
                { 'T', 8 },
                { 'U', 9 },
                { 'V', 8 },
                { 'W', 12 },
                { 'X', 8 },
                { 'Y', 8 },
                { 'Z', 8 },
                { '0', 7 },
                { '1', 7 },
                { '2', 7 },
                { '3', 7 },
                { '4', 7 },
                { '5', 7 },
                { '6', 7 },
                { '7', 7 },
                { '8', 7 },
                { '9', 7 },
                { '-', 4 },
                { '_', 7 },
                { '~', 7 },
                { '.', 3 },
                { '!', 4 },
                { '*', 5 },
                { '\'', 3 },
                { '(', 4 },
                { ')', 4 },
                { '[', 4 },
                { ']', 4 },
                { ';', 3 },
                { ':', 3 },
                { '@', 12 },
                { '=', 7 },
                { '+', 7 },
                { '$', 7 },
                { ',', 3 },
                { '#', 7 },
                { '?', 7 },
                { '/', 4 },
                { '&', 8 },
                { ' ', 7 }
            };
            #endregion

            portSize = 0;
            versionSize = 0;

            foreach (var c in port) {
                if (widths.TryGetValue(c, out var width)) {
                    portSize += width;
                } else {
                    portSize += 7;
                }
            }

            foreach (var c in version) {
                if (widths.TryGetValue(c, out var width)) {
                    versionSize += width;
                } else {
                    versionSize += 7;
                }
            }

            portSize *= 1.2f;
            versionSize *= 1.2f;
        }
        #endregion
    }
}
