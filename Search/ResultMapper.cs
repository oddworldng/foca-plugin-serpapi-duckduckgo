using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Foca.SerpApiDuckDuckGo.Search
{
    /// <summary>
    /// Extracts links from SerpApi DuckDuckGo organic_results[].link
    /// </summary>
    public static class ResultMapper
    {
        public static IEnumerable<string> ExtractLinks(JObject json)
        {
            if (json == null) yield break;
            var organic = json["organic_results"] as JArray;
            if (organic == null) yield break;
            foreach (var item in organic.OfType<JObject>())
            {
                var link = item["link"]?.ToString();
                if (!string.IsNullOrWhiteSpace(link)) yield return link.Trim();
            }
        }
    }
}


