using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Foca.SerpApiDuckDuckGo.Api;
using Foca.SerpApiDuckDuckGo.Config;
using Foca.SerpApiDuckDuckGo.Db;
using Foca.SerpApiDuckDuckGo.Search;
using Newtonsoft.Json.Linq;

namespace Foca.SerpApiDuckDuckGo.Ui
{
    public partial class SearchForm : Form
    {
        private CancellationTokenSource _cts;
        private List<string> _results = new List<string>();

        public SearchForm()
        {
            InitializeComponent();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            string kl = ConfigurationManager.AppSettings["DefaultRegionKl"] ?? "es-es";
            txtKl.Text = kl;
            chkListExtensions.Items.Clear();
            var defaults = new[] { "pdf", "doc", "docx", "xls", "xlsx", "ppt", "pptx", "odt", "ods", "odp" };
            foreach (var ext in defaults) chkListExtensions.Items.Add(ext, true);
            btnIncorporarExistente.Enabled = false;
            btnIncorporarNuevo.Enabled = false;
            btnExportar.Enabled = false;
            btnClose.Visible = false;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            await RunSearchAsync(_cts.Token);
        }

        private string ResolveApiKey()
        {
            var env = SerpApiSettings.ResolveApiKey();
            if (!string.IsNullOrWhiteSpace(env)) return env;
            var local = SerpApiConfigStore.Load()?.SerpApiKey;
            return local;
        }

        private async Task RunSearchAsync(CancellationToken ct)
        {
            btnBuscar.Enabled = false;
            lstResults.Items.Clear();
            _results.Clear();
            lblCount.Text = "0 resultados";

            try
            {
                var apiKey = ResolveApiKey();
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    MessageBox.Show("Configura la API Key de SerpApi primero.", "Búsqueda avanzada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var domain = txtRootUrl.Text;
                var selectedExts = chkListExtensions.CheckedItems.Cast<object>().Select(o => o.ToString()).ToArray();
                var query = QueryBuilder.Build(domain, selectedExts);
                var kl = txtKl.Text?.Trim();
                int maxResults = 200;
                try { maxResults = int.Parse(ConfigurationManager.AppSettings["MaxResults"] ?? "200"); } catch { }

                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (var client = new SerpApiClient())
                {
                    for (int page = 0; _results.Count < maxResults; page++)
                    {
                        ct.ThrowIfCancellationRequested();
                        var (ok, error, json) = await client.SearchAsync(apiKey, query, kl, page, ct);
                        if (!ok)
                        {
                            MessageBox.Show(error ?? "Error de búsqueda", "Búsqueda avanzada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        var links = ResultMapper.ExtractLinks(json).ToList();
                        if (links.Count == 0) break; // no more results
                        foreach (var link in links)
                        {
                            if (seen.Add(link))
                            {
                                _results.Add(link);
                                lstResults.Items.Add(link);
                                if (_results.Count >= maxResults) break;
                            }
                        }
                        lblCount.Text = _results.Count + " resultados";
                        if (links.Count < 10) break; // likely end
                    }
                }

                // Resumen final alineado con Excel plugin (mensaje claro)
                lblCount.Text = $"Se han encontrado {_results.Count} resultados";
                btnIncorporarExistente.Enabled = _results.Count > 0;
                btnIncorporarNuevo.Enabled = _results.Count > 0;
                btnExportar.Enabled = _results.Count > 0;
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la búsqueda: {ex.Message}", "Búsqueda avanzada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBuscar.Enabled = true;
            }
        }

        private async void btnIncorporarExistente_Click(object sender, EventArgs e)
        {
            if (_results.Count == 0) return;
            using (var dlg = new SelectProjectForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var info = dlg.SelectedProject;
                    if (info == null) return;
                    var inserter = new DbInserter();
                    var (ins, dup) = await inserter.InsertUrlsAsync(info.Id, _results.ToArray());
                    MessageBox.Show($"Total: {_results.Count}\nInsertadas nuevas: {ins}\nDuplicadas: {dup}", "Incorporación a proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Tras finalizar, mostrar solo Cerrar
                    FinalizeFlowUI();
                }
            }
        }

        private async void btnIncorporarNuevo_Click(object sender, EventArgs e)
        {
            if (_results.Count == 0) return;
            using (var dlg = new NewProjectForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var name = dlg.ProjectName;
                    var notes = dlg.ProjectNotes;
                    var inserter = new DbInserter();
                    int projectId = await inserter.CreateProjectAsync(name, notes);
                    var (ins, dup) = await inserter.InsertUrlsAsync(projectId, _results.ToArray());
                    MessageBox.Show($"Proyecto creado (Id {projectId}).\nTotal: {_results.Count}\nInsertadas nuevas: {ins}\nDuplicadas: {dup}", "Incorporación a nuevo proyecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Tras finalizar, mostrar solo Cerrar
                    FinalizeFlowUI();
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_results.Count == 0) return;
            using (var sfd = new SaveFileDialog())
            {
                var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                sfd.Filter = "CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
                sfd.FileName = $"{stamp}_SerpApiDuckDuckGo_results.csv";
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllLines(sfd.FileName, _results);
                        MessageBox.Show("Exportación completada.", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"No se pudo exportar: {ex.Message}", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FinalizeFlowUI()
        {
            try
            {
                btnBuscar.Enabled = false;
                btnIncorporarExistente.Enabled = false;
                btnIncorporarNuevo.Enabled = false;
                btnExportar.Enabled = false;
                btnClose.Visible = true;
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try { this.Close(); } catch { }
        }
    }
}


