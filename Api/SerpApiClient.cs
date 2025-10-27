using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Foca.SerpApiDuckDuckGo.Api
{
    /// <summary>
    /// Minimal SerpApi client for DuckDuckGo engine.
    /// Handles rate limiting (RPM) and basic error handling.
    /// </summary>
    public class SerpApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly int _rpm;
        private readonly TimeSpan _timeout;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private DateTime _lastRequest = DateTime.MinValue;

        public SerpApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("FOCA-SerpApiDuckDuckGo/1.0");

            int rpm = 30;
            int timeoutSec = 20;
            try { rpm = int.Parse(ConfigurationManager.AppSettings["RequestsPerMinute"] ?? "30"); } catch { }
            try { timeoutSec = int.Parse(ConfigurationManager.AppSettings["SerpApiTimeoutSeconds"] ?? "20"); } catch { }
            _rpm = Math.Max(1, rpm);
            _timeout = TimeSpan.FromSeconds(Math.Max(5, timeoutSec));
            _httpClient.Timeout = _timeout;
        }

        public async Task<(bool ok, string error, JObject json)> TestConnectionAsync(string apiKey, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(apiKey)) return (false, "API Key no configurada", null);
            var url = $"https://serpapi.com/search.json?engine=duckduckgo&q=test&api_key={Uri.EscapeDataString(apiKey)}";
            return await GetAsync(url, ct);
        }

        public async Task<(bool ok, string error, JObject json)> SearchAsync(string apiKey, string query, string kl, int page, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(apiKey)) return (false, "API Key no configurada", null);
            var sb = new StringBuilder("https://serpapi.com/search.json?engine=duckduckgo");
            sb.Append("&q=").Append(Uri.EscapeDataString(query ?? string.Empty));
            if (!string.IsNullOrWhiteSpace(kl)) sb.Append("&kl=").Append(Uri.EscapeDataString(kl));
            if (page > 0) sb.Append("&start=").Append(page * 10); // DuckDuckGo step; SerpApi paginates by start index
            sb.Append("&api_key=").Append(Uri.EscapeDataString(apiKey));
            var url = sb.ToString();
            return await GetAsync(url, ct);
        }

        private async Task<(bool ok, string error, JObject json)> GetAsync(string url, CancellationToken ct)
        {
            await RespectRateLimitAsync(ct);
            try
            {
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                var res = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);
                var body = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                {
                    if (res.StatusCode == HttpStatusCode.Unauthorized || res.StatusCode == HttpStatusCode.Forbidden)
                        return (false, "Acceso denegado por SerpApi (401/403). Verifica la API Key o el plan.", null);
                    return (false, $"Error HTTP {(int)res.StatusCode}: {res.ReasonPhrase}", null);
                }
                try
                {
                    var json = JObject.Parse(body);
                    return (true, null, json);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return (false, "Respuesta inválida de SerpApi.", null);
                }
            }
            catch (TaskCanceledException)
            {
                return (false, "La solicitud a SerpApi expiró por timeout.", null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return (false, $"Error de red: {ex.Message}", null);
            }
        }

        private async Task RespectRateLimitAsync(CancellationToken ct)
        {
            // Simple RPM limiter: ensure at least 60/rpm seconds between requests
            var minDelta = TimeSpan.FromSeconds(60.0 / _rpm);
            await _semaphore.WaitAsync(ct);
            try
            {
                var now = DateTime.UtcNow;
                var elapsed = now - _lastRequest;
                if (elapsed < minDelta)
                {
                    var wait = minDelta - elapsed;
                    if (wait > TimeSpan.Zero)
                    {
                        await Task.Delay(wait, ct);
                    }
                }
                _lastRequest = DateTime.UtcNow;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Dispose()
        {
            try { _httpClient.Dispose(); } catch { }
            try { _semaphore.Dispose(); } catch { }
        }
    }
}


