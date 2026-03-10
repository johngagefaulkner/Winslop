using System.Diagnostics;
using System.Text;

namespace Winslop.Help
{
    internal static class FeatureHelp
    {
        // URL to the oline docs (features + plugins live here)
        public const string FeaturesDocUrl =
            "https://github.com/builtbybel/Winslop/blob/main/docs/features.md";

        public static void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        /// <summary>
        /// Builds the documentation URL for a feature, including a GitHub-style anchor.
        /// Feature: anchor from feature.HelpAnchorId() (stable ID used by docs generator).
        /// </summary>
        public static string GetFeatureUrl(FeatureBase feature)
        {
            // If no feature is given, fall back to the document root.
            if (feature == null) return FeaturesDocUrl;

            // FeatureHelp anchors should come from stable IDs so docs links survive display-name edits.
            return FeaturesDocUrl + "#" + ToGitHubAnchor(feature.HelpAnchorId());
        }

        // Plugin: anchor from plugin title (usually node.Text)
        public static string GetPluginUrl(string pluginTitle)
        {
            // If we don't have a title, fall back to the document root.
            if (string.IsNullOrWhiteSpace(pluginTitle)) return FeaturesDocUrl;

            return FeaturesDocUrl + "#" + ToGitHubAnchor(pluginTitle);
        }

        /// <summary>
        /// Converts a heading into a GitHub-like anchor (slug).
        /// Example:
        ///   "Don't Show Copilot in Taskbar" -> "dont-show-copilot-in-taskbar"
        ///
        /// Rules:
        /// - Lowercase
        /// - Remove apostrophes (Don't -> dont)
        /// - Replace any non-alphanumeric run with a single '-'
        /// - Trim leading/trailing '-'
        /// </summary>
        private static string ToGitHubAnchor(string heading)
        {
            if (string.IsNullOrWhiteSpace(heading))
                return string.Empty;

            var sb = new StringBuilder(heading.Length);
            bool lastWasDash = false;

            foreach (char ch in heading.Trim().ToLowerInvariant())
            {
                // drop apostrophes: don't -> dont
                if (ch == '\'' || ch == '’' || ch == '´')
                    continue;

                if (char.IsLetterOrDigit(ch))
                {
                    sb.Append(ch);
                    lastWasDash = false;
                }
                else
                {
                    // Any non-alphanumeric character becomes a separator.
                    // Collapse multiple separators into a single dash.
                    if (!lastWasDash)
                    {
                        sb.Append('-');
                        lastWasDash = true;
                    }
                }
            }

            return sb.ToString().Trim('-');
        }
    }
}