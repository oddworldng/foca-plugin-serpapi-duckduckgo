using System;
using System.Configuration;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Foca.SerpApiDuckDuckGo.Api;
using Foca.SerpApiDuckDuckGo.Config;

namespace Foca.SerpApiDuckDuckGo.Ui
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            var env = SerpApiSettings.ResolveApiKey();
            var local = SerpApiConfigStore.Load()?.SerpApiKey;
            txtApiKey.Text = string.IsNullOrWhiteSpace(env) ? local : env;
            lblPriority.Text = "Prioridad de lectura: 1) SERPAPI_API_KEY  2) config.json";
            toolTip1.SetToolTip(lblPriority, "La variable de entorno SERPAPI_API_KEY tiene prioridad sobre config.json en %APPDATA%\\FOCA\\Plugins\\SerpApiDuckDuckGo\\config.json");
        }

        private async void btnProbar_Click(object sender, EventArgs e)
        {
            await TestAsync();
        }

        private async Task TestAsync()
        {
            btnProbar.Enabled = false;
            try
            {
                using (var client = new SerpApiClient())
                {
                    var (ok, error, _) = await client.TestConnectionAsync(txtApiKey.Text);
                    if (ok)
                    {
                        MessageBox.Show("Conexión correcta con SerpApi.", "Configuración de SerpApi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(error ?? "No se pudo validar la API Key.", "Configuración de SerpApi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al probar conexión: {ex.Message}", "Configuración de SerpApi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProbar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var env = SerpApiSettings.ResolveApiKey();
                if (!string.IsNullOrWhiteSpace(env))
                {
                    var r = MessageBox.Show("Existe SERPAPI_API_KEY definida en el entorno. ¿Deseas guardar también en config.json? (SERPAPI_API_KEY tendrá prioridad)",
                        "Configuración de SerpApi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.No)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                }
                SerpApiConfigStore.Save(new SerpApiSettings { SerpApiKey = txtApiKey.Text?.Trim() });
                MessageBox.Show("Configuración guardada correctamente.", "Configuración de SerpApi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo guardar la configuración: {ex.Message}", "Configuración de SerpApi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


