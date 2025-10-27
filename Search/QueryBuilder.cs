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

        /// <summary>
        /// Devuelve los segmentos de ruta del input si el usuario pegó una URL con ruta.
        /// </summary>
        public static List<string> GetPathSegments(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input)) return new List<string>();
                string host = NormalizeToDomain(input);
                string tld = null;
                try { tld = (host ?? string.Empty).Split('.').LastOrDefault(); } catch { tld = null; }

                var langCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                { "es", "en", "fr", "pt", "it", "de", "ca", "eu", "gl", "va" };
                string path = null;
                if (input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    input.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    path = new Uri(input).AbsolutePath;
                }
                else
                {
                    var idx = input.IndexOf('/');
                    if (idx >= 0) path = input.Substring(idx);
                }
                if (string.IsNullOrWhiteSpace(path)) return new List<string>();
                int minLen = 4;
                try
                {
                    var loaded = Foca.SerpApiDuckDuckGo.Config.SerpApiConfigStore.Load();
                    if (loaded != null) minLen = Math.Max(0, loaded.MinInurlSegmentLength);
                }
                catch { }

                return path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => s.Trim())
                           // Longitud mínima configurable para evitar idiomas/códigos cortos (es, en, us, etc.)
                           .Where(s => s.Length >= minLen && !string.Equals(s, tld, StringComparison.OrdinalIgnoreCase) && !langCodes.Contains(s))
                           .ToList();
            }
            catch { return new List<string>(); }
        }

        public static string Build(string domain, IEnumerable<string> extensions)
        {
            var d = NormalizeToDomain(domain);
            var list = (extensions ?? Enumerable.Empty<string>()).Where(e => !string.IsNullOrWhiteSpace(e))
                .Select(e => e.Trim().ToLower()).Distinct().ToList();
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(d)) parts.Add($"site:\"{d}\"");
            // Si la entrada tiene ruta (p. ej. https://www.mjusticia.gob.es/es/ciudadania/empleo-publico)
            // añadimos inurl: por segmentos para restringir mejor la búsqueda
            try
            {
                var segs = GetPathSegments(domain);
                if (segs.Count > 0)
                {
                    foreach (var seg in segs)
                    {
                        var s = seg.Trim();
                        if (s.Length > 0) parts.Add($"inurl:\"{s}\"");
                    }
                }
            }
            catch { }
            if (list.Count > 0)
            {
                // Combine filetypes with OR to broaden results
                var types = string.Join(" OR ", list.Select(e => $"filetype:{e}"));
                parts.Add(types);
            }
            return string.Join(" ", parts);
        }

        public static string BuildGoogle(string domain, IEnumerable<string> extensions)
        {
            // En Google el dork es similar: site:"host" + inurl:"seg" + filetype:ext
            return Build(domain, extensions);
        }

        /// <summary>
        /// Comprueba si la URL pertenece exactamente al host indicado (dominio o subdominio exacto).
        /// No incluye otros subdominios.
        /// </summary>
        public static bool IsUrlInDomain(string url, string domain)
        {
            var d = NormalizeToDomain(domain);
            if (string.IsNullOrWhiteSpace(d)) return true; // sin filtro
            try
            {
                var host = new Uri(url).Host;
                return host.Equals(d, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Comprueba si la URL contiene todos los segmentos de ruta indicados (orden indiferente).
        /// </summary>
        public static bool UrlPathContainsSegments(string url, IList<string> segments)
        {
            if (segments == null || segments.Count == 0) return true;
            try
            {
                var path = new Uri(url).AbsolutePath.ToLowerInvariant();
                foreach (var s in segments)
                {
                    if (!path.Contains((s ?? string.Empty).ToLowerInvariant())) return false;
                }
                return true;
            }
            catch { return false; }
        }
    }
}


