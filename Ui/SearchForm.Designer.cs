namespace Foca.SerpApiDuckDuckGo.Ui
{
    partial class SearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblRootUrl;
        private System.Windows.Forms.TextBox txtRootUrl;
        private System.Windows.Forms.Label lblExtensions;
        private System.Windows.Forms.CheckedListBox chkListExtensions;
        private System.Windows.Forms.Label lblKl;
        private System.Windows.Forms.TextBox txtKl;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnIncorporarExistente;
        private System.Windows.Forms.Button btnIncorporarNuevo;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtQueryPreview;
        private System.Windows.Forms.CheckBox chkRestrictPath;
        private System.Windows.Forms.Label lblEngine;
        private System.Windows.Forms.ComboBox cmbEngine;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblRootUrl = new System.Windows.Forms.Label();
            this.txtRootUrl = new System.Windows.Forms.TextBox();
            this.lblExtensions = new System.Windows.Forms.Label();
            this.chkListExtensions = new System.Windows.Forms.CheckedListBox();
            this.lblKl = new System.Windows.Forms.Label();
            this.txtKl = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnIncorporarExistente = new System.Windows.Forms.Button();
            this.btnIncorporarNuevo = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtQueryPreview = new System.Windows.Forms.TextBox();
            this.chkRestrictPath = new System.Windows.Forms.CheckBox();
            this.lblEngine = new System.Windows.Forms.Label();
            this.cmbEngine = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblRootUrl
            // 
            this.lblRootUrl.AutoSize = true;
            this.lblRootUrl.Location = new System.Drawing.Point(12, 15);
            this.lblRootUrl.Name = "lblRootUrl";
            this.lblRootUrl.Size = new System.Drawing.Size(55, 13);
            this.lblRootUrl.TabIndex = 0;
            this.lblRootUrl.Text = "URL raíz:";
            // 
            // txtRootUrl
            // 
            this.txtRootUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootUrl.Location = new System.Drawing.Point(90, 12);
            this.txtRootUrl.Name = "txtRootUrl";
            this.txtRootUrl.Size = new System.Drawing.Size(458, 20);
            this.txtRootUrl.TabIndex = 1;
            // 
            // lblExtensions
            // 
            this.lblExtensions.AutoSize = true;
            this.lblExtensions.Location = new System.Drawing.Point(12, 45);
            this.lblExtensions.Name = "lblExtensions";
            this.lblExtensions.Size = new System.Drawing.Size(67, 13);
            this.lblExtensions.TabIndex = 2;
            this.lblExtensions.Text = "Extensiones:";
            // 
            // chkListExtensions
            // 
            this.chkListExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListExtensions.CheckOnClick = true;
            this.chkListExtensions.Location = new System.Drawing.Point(90, 45);
            this.chkListExtensions.Name = "chkListExtensions";
            this.chkListExtensions.Size = new System.Drawing.Size(458, 94);
            this.chkListExtensions.TabIndex = 3;
            // 
            // lblKl
            // 
            this.lblKl.AutoSize = true;
            this.lblKl.Location = new System.Drawing.Point(12, 152);
            this.lblKl.Name = "lblKl";
            this.lblKl.Size = new System.Drawing.Size(74, 13);
            this.lblKl.TabIndex = 4;
            this.lblKl.Text = "Región (kl):";
            // 
            // txtKl
            // 
            this.txtKl.Location = new System.Drawing.Point(90, 149);
            this.txtKl.Name = "txtKl";
            this.txtKl.Size = new System.Drawing.Size(120, 20);
            this.txtKl.TabIndex = 5;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Location = new System.Drawing.Point(554, 10);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 23);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lstResults
            // 
            this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResults.FormattingEnabled = true;
            this.lstResults.IntegralHeight = false;
            this.lstResults.Location = new System.Drawing.Point(15, 182);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(629, 210);
            this.lstResults.TabIndex = 7;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(12, 405);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(69, 13);
            this.lblCount.TabIndex = 8;
            this.lblCount.Text = "0 resultados";
            // 
            // btnIncorporarExistente
            // 
            this.btnIncorporarExistente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncorporarExistente.Location = new System.Drawing.Point(330, 400);
            this.btnIncorporarExistente.Name = "btnIncorporarExistente";
            this.btnIncorporarExistente.Size = new System.Drawing.Size(150, 23);
            this.btnIncorporarExistente.TabIndex = 9;
            this.btnIncorporarExistente.Text = "Incorporar a proyecto";
            this.btnIncorporarExistente.UseVisualStyleBackColor = true;
            this.btnIncorporarExistente.Click += new System.EventHandler(this.btnIncorporarExistente_Click);
            // 
            // btnIncorporarNuevo
            // 
            this.btnIncorporarNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncorporarNuevo.Location = new System.Drawing.Point(486, 400);
            this.btnIncorporarNuevo.Name = "btnIncorporarNuevo";
            this.btnIncorporarNuevo.Size = new System.Drawing.Size(158, 23);
            this.btnIncorporarNuevo.TabIndex = 10;
            this.btnIncorporarNuevo.Text = "Incorporar a nuevo proyecto";
            this.btnIncorporarNuevo.UseVisualStyleBackColor = true;
            this.btnIncorporarNuevo.Click += new System.EventHandler(this.btnIncorporarNuevo_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(554, 39);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(90, 23);
            this.btnExportar.TabIndex = 11;
            this.btnExportar.Text = "Exportar CSV";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(561, 400);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtQueryPreview
            // 
            this.txtQueryPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryPreview.Location = new System.Drawing.Point(90, 172);
            this.txtQueryPreview.Name = "txtQueryPreview";
            this.txtQueryPreview.ReadOnly = true;
            this.txtQueryPreview.Size = new System.Drawing.Size(554, 20);
            this.txtQueryPreview.TabIndex = 13;
            // 
            // chkRestrictPath
            // 
            this.chkRestrictPath.AutoSize = true;
            this.chkRestrictPath.Checked = true;
            this.chkRestrictPath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestrictPath.Enabled = false;
            this.chkRestrictPath.Location = new System.Drawing.Point(219, 151);
            this.chkRestrictPath.Name = "chkRestrictPath";
            this.chkRestrictPath.Size = new System.Drawing.Size(151, 17);
            this.chkRestrictPath.TabIndex = 14;
            this.chkRestrictPath.Text = "Restringir a ruta indicada";
            this.chkRestrictPath.UseVisualStyleBackColor = true;
            // 
            // lblEngine
            // 
            this.lblEngine.AutoSize = true;
            this.lblEngine.Location = new System.Drawing.Point(330, 152);
            this.lblEngine.Name = "lblEngine";
            this.lblEngine.Size = new System.Drawing.Size(52, 13);
            this.lblEngine.TabIndex = 15;
            this.lblEngine.Text = "Buscador:";
            // 
            // cmbEngine
            // 
            this.cmbEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEngine.FormattingEnabled = true;
            this.cmbEngine.Items.AddRange(new object[] {
            "DuckDuckGo",
            "Google"});
            this.cmbEngine.Location = new System.Drawing.Point(388, 149);
            this.cmbEngine.Name = "cmbEngine";
            this.cmbEngine.Size = new System.Drawing.Size(121, 21);
            this.cmbEngine.TabIndex = 16;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 435);
            this.Controls.Add(this.cmbEngine);
            this.Controls.Add(this.lblEngine);
            this.Controls.Add(this.chkRestrictPath);
            this.Controls.Add(this.txtQueryPreview);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnIncorporarNuevo);
            this.Controls.Add(this.btnIncorporarExistente);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtKl);
            this.Controls.Add(this.lblKl);
            this.Controls.Add(this.chkListExtensions);
            this.Controls.Add(this.lblExtensions);
            this.Controls.Add(this.txtRootUrl);
            this.Controls.Add(this.lblRootUrl);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buscar documentos (SerpApi/DuckDuckGo)";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}


