using System;

namespace Foca.SerpApiDuckDuckGo.Config
{
    /// <summary>
    /// SerpApi settings model. API key is read from env var first, then local store.
    /// </summary>
    public class SerpApiSettings
    {
        public string SerpApiKey { get; set; }
        // Longitud mínima de segmentos usados en inurl:"segmento" (por defecto 4)
        public int MinInurlSegmentLength { get; set; } = 4;
        // Máximo de resultados a recoger (0 = ilimitado)
        public int MaxResults { get; set; } = 0;
        // Máximo de páginas a consultar (0 = ilimitado)
        public int MaxPagesPerSearch { get; set; } = 10;
        // Retardo entre páginas en ms (para evitar 429). 0 = sin retardo
        public int DelayBetweenPagesMs { get; set; } = 0;

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


