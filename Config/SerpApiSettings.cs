using System;

namespace Foca.SerpApiDuckDuckGo.Config
{
    /// <summary>
    /// SerpApi settings model. API key is read from env var first, then local store.
    /// </summary>
    public class SerpApiSettings
    {
        public string SerpApiKey { get; set; }

        public static string ResolveApiKey(Func<string> appConfigReader = null)
        {
            // 1) Environment variable has priority
            var fromEnv = Environment.GetEnvironmentVariable("SERPAPI_API_KEY");
            if (!string.IsNullOrWhiteSpace(fromEnv)) return fromEnv.Trim();

            // 2) Local persisted config (handled by SerpApiConfigStore)
            return null; // let the caller read from store
        }
    }
}


