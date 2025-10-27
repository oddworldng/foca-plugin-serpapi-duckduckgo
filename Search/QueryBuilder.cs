using System;
using System.Collections.Generic;
using System.Linq;

namespace Foca.SerpApiDuckDuckGo.Search
{
    /// <summary>
    /// Builds DuckDuckGo query with site:domain and filetype filters.
    /// </summary>
    public static class QueryBuilder
    {
        public static string NormalizeToDomain(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            input = input.Trim();
            if (input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                input.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var uri = new Uri(input);
                    return uri.Host;
                }
                catch { return input; }
            }
            // Remove path if user pasted something like domain.com/path
            var slash = input.IndexOf('/');
            if (slash > 0) input = input.Substring(0, slash);
            return input;
        }

        public static string Build(string domain, IEnumerable<string> extensions)
        {
            var d = NormalizeToDomain(domain);
            var list = (extensions ?? Enumerable.Empty<string>()).Where(e => !string.IsNullOrWhiteSpace(e))
                .Select(e => e.Trim().ToLower()).Distinct().ToList();
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(d)) parts.Add($"site:{d}");
            if (list.Count > 0)
            {
                // Combine filetypes with OR to broaden results
                var types = string.Join(" OR ", list.Select(e => $"filetype:{e}"));
                parts.Add(types);
            }
            return string.Join(" ", parts);
        }
    }
}


