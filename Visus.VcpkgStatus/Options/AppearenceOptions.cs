﻿// <copyright file="AppearenceOptions.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2024 Visualisierungsinstitut der Universität Stuttgart.
// Licenced under the MIT licence. See LICENCE.txt.
// </copyright>
// <author>Christoph Müller</author>


namespace Visus.VcpkgStatus.Options {

    /// <summary>
    /// Options controlling the appearence of the status badge.
    /// </summary>
    public sealed class AppearenceOptions {

        /// <summary>
        /// The name of the configuration section.
        /// </summary>
        public const string SectionName = "Appearence";

        /// <summary>
        /// Gets or sets the font family used for the badge.
        /// </summary>
        public string FontFamily { get; set; }
            = "Arial, Helvetica, sans-serif";

        /// <summary>
        /// Gets or sets the size of the font used for the badge.
        /// </summary>
        public float FontSize { get; set; } = 12.0f;

        /// <summary>
        /// Gets or sets the height of the badge.
        /// </summary>
        public float Height { get; set; } = 20.0f;

        /// <summary>
        /// Gets or sets the vcpkg logo.
        /// </summary>
        public string Logo { get; set; } =@"<svg xmlns=""http://www.w3.org/2000/svg"" x=""{0}"" y=""{1}"" width=""{2}px"" height=""{2}px"" viewBox=""0 0 16 16"" fill=""none"">
    <script xmlns="""" />
    <path d=""M7.27287 5.05296C3.94764 3.87505 1.57018 5.3753 0.192701 7.10237C0.140493 7.16604 0.0400939 7.12624 0.0521418 7.04268C0.132461 6.58504 0.321213 5.68967 0.690683 4.86991C2.19266 1.53913 6.18855 -0.0804928 8.56601 0.0030751C10.9435 0.086643 13.7908 1.6426 12.8029 3.83924C11.9314 5.7812 10.7507 6.28658 7.27287 5.05296Z""
        fill=""url(#paint0_linear)"" />
    <path d=""M4.77944 4.99812C4.77944 4.95434 4.74731 4.91455 4.69912 4.91455C3.59473 4.94241 1.24539 5.67462 0.0285453 7.88718C0.0245293 7.89514 0.0205128 7.9031 0.0205128 7.91504C-0.356989 10.5614 4.59872 12.2884 4.77944 4.99812Z""
        fill=""url(#paint1_linear)"" />
    <path d=""M8.74113 10.947C12.0664 12.1249 14.4438 10.6247 15.8213 8.89762C15.8735 8.83395 15.9739 8.87374 15.9618 8.95731C15.8815 9.41495 15.6928 10.3103 15.3233 11.1301C13.8253 14.4609 9.82946 16.0805 7.452 15.9969C5.07454 15.9133 2.22722 14.3574 3.21515 12.1607C4.0826 10.2228 5.26731 9.71738 8.74113 10.947Z""
        fill=""url(#paint2_linear)"" />
    <path d=""M11.2125 11.07C11.2125 11.1138 11.2447 11.1536 11.2928 11.1536C12.3972 11.1258 14.7546 10.3577 15.9715 8.14119C15.9755 8.13323 15.9795 8.12527 15.9795 8.11333C16.357 5.47099 11.3932 3.77974 11.2125 11.07Z""
        fill=""url(#paint3_linear)"" />
    <defs>
        <linearGradient id=""paint0_linear"" x1=""0.324315"" y1=""7.86759"" x2=""13.877"" y2=""-0.453912""
            gradientUnits=""userSpaceOnUse"">
            <stop stop-color=""#FC950B"" />
            <stop offset=""0.592076"" stop-color=""#F9C438"" />
        </linearGradient>
        <linearGradient id=""paint1_linear"" x1=""5.64274"" y1=""4.08734"" x2=""0.327581"" y2=""10.3649""
            gradientUnits=""userSpaceOnUse"">
            <stop stop-color=""#FC950B"" />
            <stop offset=""1"" stop-color=""#F9C438"" />
        </linearGradient>
        <linearGradient id=""paint2_linear"" x1=""17.2883"" y1=""9.94356"" x2=""2.09626"" y2=""14.7361""
            gradientUnits=""userSpaceOnUse"">
            <stop stop-color=""#FC950B"" />
            <stop offset=""0.612893"" stop-color=""#F9C438"" />
        </linearGradient>
        <linearGradient id=""paint3_linear"" x1=""14.1247"" y1=""13.4565"" x2=""12.6976"" y2=""2.10704""
            gradientUnits=""userSpaceOnUse"">
            <stop stop-color=""#FC950B"" />
            <stop offset=""1"" stop-color=""#F9C438"" />
        </linearGradient>
    </defs>
</svg>
";

        /// <summary>
        /// Gets or sets the path to the font for measuring.
        /// </summary>
        public string MeasureFont { get; set; } = "Fonts/arial.ttf";

        /// <summary>
        /// Gets or sets the colour of the background for the logo part of
        /// the badge.
        /// </summary>
        public string PrimaryBackground { get; set; } = "#444444";

        /// <summary>
        /// Gets or sets the colour of the text showing the port above the
        /// <see cref="PrimaryBackground"/>.
        /// </summary>
        public string PrimaryForeground { get; set; } = "#FFFFFF";

        /// <summary>
        /// Gets or sets the colour of the background on the right where the
        /// current version is displayed.
        /// </summary>
        public string SecondaryBackground { get; set; } = "#F9C438";

        /// <summary>
        /// Gets or sets the colour of the text showing the version above the
        /// <see cref="SecondaryBackground"/>.
        /// </summary>
        public string SecondaryForeground { get; set; } = "#000000";

        #region Public methods
        /// <summary>
        /// Gets the <see cref="Logo"/> with the specified dimensions.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public string GetLogo(float x, float y, float size) {
            return string.Format(this.Logo, x, y, size);
        }
        #endregion
    }
}
