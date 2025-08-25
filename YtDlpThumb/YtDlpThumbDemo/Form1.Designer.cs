namespace YtDlpThumbDemo
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.ComboBox comboMode;
        private System.Windows.Forms.TextBox txtOptions;
        private System.Windows.Forms.Button btnAddToList;
        private System.Windows.Forms.Button btnDownloadAll;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLastProgress;
        private System.Windows.Forms.Button btnToggleLog;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.comboMode = new System.Windows.Forms.ComboBox();
            this.txtOptions = new System.Windows.Forms.TextBox();
            this.btnAddToList = new System.Windows.Forms.Button();
            this.btnDownloadAll = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLastProgress = new System.Windows.Forms.Label();
            this.btnToggleLog = new System.Windows.Forms.Button();
            this.lblUrlStatus = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnResetArguments = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnPasteLink = new System.Windows.Forms.Button();
            this.btnClearURL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(7, 14);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(369, 22);
            this.txtUrl.TabIndex = 0;
            // 
            // comboMode
            // 
            this.comboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMode.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMode.Items.AddRange(new object[] {
            "Video",
            "Audio",
            "Thumbnail"});
            this.comboMode.Location = new System.Drawing.Point(467, 13);
            this.comboMode.Name = "comboMode";
            this.comboMode.Size = new System.Drawing.Size(282, 21);
            this.comboMode.TabIndex = 1;
            // 
            // txtOptions
            // 
            this.txtOptions.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOptions.Location = new System.Drawing.Point(434, 46);
            this.txtOptions.Name = "txtOptions";
            this.txtOptions.Size = new System.Drawing.Size(315, 22);
            this.txtOptions.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtOptions, "Arguments");
            // 
            // btnAddToList
            // 
            this.btnAddToList.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToList.Location = new System.Drawing.Point(755, 11);
            this.btnAddToList.Name = "btnAddToList";
            this.btnAddToList.Size = new System.Drawing.Size(100, 25);
            this.btnAddToList.TabIndex = 3;
            this.btnAddToList.Text = "ADD Link";
            this.btnAddToList.UseVisualStyleBackColor = true;
            this.btnAddToList.Click += new System.EventHandler(this.btnAddToList_Click);
            // 
            // btnDownloadAll
            // 
            this.btnDownloadAll.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadAll.Location = new System.Drawing.Point(967, 11);
            this.btnDownloadAll.Name = "btnDownloadAll";
            this.btnDownloadAll.Size = new System.Drawing.Size(118, 57);
            this.btnDownloadAll.TabIndex = 4;
            this.btnDownloadAll.Text = "Download";
            this.btnDownloadAll.UseVisualStyleBackColor = true;
            this.btnDownloadAll.Click += new System.EventHandler(this.btnDownloadAll_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(861, 11);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 25);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.Location = new System.Drawing.Point(6, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1079, 274);
            this.dataGridView1.TabIndex = 6;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLog.Location = new System.Drawing.Point(8, 363);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1068, 194);
            this.txtLog.TabIndex = 7;
            this.txtLog.Visible = false;
            // 
            // lblLastProgress
            // 
            this.lblLastProgress.AutoSize = true;
            this.lblLastProgress.Location = new System.Drawing.Point(12, 570);
            this.lblLastProgress.Name = "lblLastProgress";
            this.lblLastProgress.Size = new System.Drawing.Size(0, 13);
            this.lblLastProgress.TabIndex = 8;
            // 
            // btnToggleLog
            // 
            this.btnToggleLog.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleLog.Location = new System.Drawing.Point(861, 45);
            this.btnToggleLog.Name = "btnToggleLog";
            this.btnToggleLog.Size = new System.Drawing.Size(100, 23);
            this.btnToggleLog.TabIndex = 9;
            this.btnToggleLog.Text = "Show Logs";
            this.btnToggleLog.UseVisualStyleBackColor = true;
            this.btnToggleLog.Click += new System.EventHandler(this.btnToggleLog_Click);
            // 
            // lblUrlStatus
            // 
            this.lblUrlStatus.AutoSize = true;
            this.lblUrlStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrlStatus.Location = new System.Drawing.Point(3, -1);
            this.lblUrlStatus.Name = "lblUrlStatus";
            this.lblUrlStatus.Size = new System.Drawing.Size(66, 13);
            this.lblUrlStatus.TabIndex = 10;
            this.lblUrlStatus.Text = "lblUrlStatus";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAll.Location = new System.Drawing.Point(755, 45);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(100, 23);
            this.btnClearAll.TabIndex = 11;
            this.btnClearAll.Text = "Clear List";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnResetArguments
            // 
            this.btnResetArguments.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetArguments.Location = new System.Drawing.Point(433, 11);
            this.btnResetArguments.Name = "btnResetArguments";
            this.btnResetArguments.Size = new System.Drawing.Size(28, 25);
            this.btnResetArguments.TabIndex = 12;
            this.btnResetArguments.Text = "R";
            this.btnResetArguments.UseVisualStyleBackColor = true;
            this.btnResetArguments.Click += new System.EventHandler(this.btnResetArguments_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("contextMenuStrip.BackgroundImage")));
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // btnPasteLink
            // 
            this.btnPasteLink.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasteLink.Location = new System.Drawing.Point(6, 45);
            this.btnPasteLink.Name = "btnPasteLink";
            this.btnPasteLink.Size = new System.Drawing.Size(403, 25);
            this.btnPasteLink.TabIndex = 14;
            this.btnPasteLink.Text = "Paste Link";
            this.btnPasteLink.UseVisualStyleBackColor = true;
            this.btnPasteLink.Click += new System.EventHandler(this.btnPasteLink_Click);
            // 
            // btnClearURL
            // 
            this.btnClearURL.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearURL.Location = new System.Drawing.Point(381, 12);
            this.btnClearURL.Name = "btnClearURL";
            this.btnClearURL.Size = new System.Drawing.Size(28, 25);
            this.btnClearURL.TabIndex = 15;
            this.btnClearURL.Text = "R";
            this.btnClearURL.UseVisualStyleBackColor = true;
            this.btnClearURL.Click += new System.EventHandler(this.btnClearURL_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1092, 361);
            this.Controls.Add(this.btnClearURL);
            this.Controls.Add(this.btnPasteLink);
            this.Controls.Add(this.btnResetArguments);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.lblUrlStatus);
            this.Controls.Add(this.btnToggleLog);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.comboMode);
            this.Controls.Add(this.txtOptions);
            this.Controls.Add(this.btnAddToList);
            this.Controls.Add(this.btnDownloadAll);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLastProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.99D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YtDlpThumb";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblUrlStatus;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnResetArguments;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Button btnPasteLink;
        private System.Windows.Forms.Button btnClearURL;
    }
}
