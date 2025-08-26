using ImageMagick;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YtDlpThumbDemo
{
    public partial class Form1 : Form
    {
        private Process currentProcess = null;
        private bool logVisible;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.CellClick += dataGridView1_CellClick;

            logVisible = txtLog.Visible;

            btnAddToList.Enabled = true;

            comboMode.SelectedIndex = 0;
            comboMode.SelectedIndexChanged += comboMode_SelectedIndexChanged;

            txtOptions.Text = "bv*+ba/b";

            // Setup DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            var thumbCol = new DataGridViewImageColumn()
            {
                Name = "Thumbnail",
                Width = 120,
                MinimumWidth = 120,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dataGridView1.Columns.Add(thumbCol);
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Title", Width = 394 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Duration", Width = 150 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Status", Width = 412 });

            dataGridView1.RowTemplate.Height = 80;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dataGridView1.Columns["Thumbnail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Title"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Duration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            txtLog.Multiline = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.ReadOnly = true;
            txtLog.WordWrap = false;

            lblLastProgress.Text = "";

            SetupContextMenu();
        }

        #region Anteprima Miniatura
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Thumbnail"].Index)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value is Image img)
                {
                    Form imgForm = new Form
                    {
                        Text = "Preview Thumbnail",
                        Size = new Size(Math.Min(img.Width, 1000), Math.Min(img.Height, 800)),
                        StartPosition = FormStartPosition.CenterParent,
                        ShowIcon = false,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        MaximizeBox = false,
                        MinimizeBox = false
                    };

                    PictureBox pb = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        Image = img,
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
                    imgForm.Controls.Add(pb);
                    imgForm.ShowDialog();
                }
            }
        }
        #endregion

        #region Menu Contestuale Tasto Destro
        private void SetupContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            // Voce "Apri link" con icona
            var openLink = new ToolStripMenuItem("Open link")
            {
                Image = YtDlpThumb.Properties.Resources.IconOpenLink // icona da aggiungere alle risorse
            };
            openLink.Click += (s, e) =>
            {
                if (dataGridView1.CurrentRow?.Tag != null)
                {
                    string url = dataGridView1.CurrentRow.Tag.ToString();
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                    catch
                    {
                        MessageBox.Show("Unable to open link.");
                    }
                }
            };
            menu.Items.Add(openLink);

            // Voce "Elimina riga" con icona
            var deleteRow = new ToolStripMenuItem("Delete row")
            {
                Image = YtDlpThumb.Properties.Resources.iconDelete // icona da aggiungere alle risorse
            };
            deleteRow.Click += (s, e) =>
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    dataGridView1.Rows.RemoveAt(index);
                }
            };
            menu.Items.Add(deleteRow);

            // Mostra menu al click destro
            dataGridView1.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var hit = dataGridView1.HitTest(e.X, e.Y);
                    if (hit.RowIndex >= 0)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[hit.RowIndex].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[hit.RowIndex].Cells[0];
                        menu.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
            };
        }
        #endregion

        #region Mostra/Nascondi Log
        private void btnToggleLog_Click(object sender, EventArgs e)
        {
            logVisible = !logVisible;
            txtLog.Visible = logVisible;
            this.Height = logVisible ? 600 : 400;
            btnToggleLog.Text = logVisible ? "Hide Log" : "Show Log";
        }
        #endregion

        #region Aggiungi Link
        private async void btnAddToList_Click(object sender, EventArgs e)
        {
            string videoUrl = txtUrl.Text.Trim();
            if (string.IsNullOrEmpty(videoUrl))
            {
                MessageBox.Show("Inserisci un link.");
                return;
            }

            string json;
            try
            {
                json = await RunYtDlpInfoAsync(videoUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving information:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // non aggiunge nulla
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                MessageBox.Show("Error: yt-dlp did not return valid data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var root = doc.RootElement;

                    // Controlla che ci sia un titolo
                    if (!root.TryGetProperty("title", out JsonElement t) || string.IsNullOrEmpty(t.GetString()))
                    {
                        MessageBox.Show("Error: Title not found in video. Link not added..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // non aggiunge la riga
                    }

                    string title = t.GetString();
                    string durationStr = "N/A";
                    if (root.TryGetProperty("duration", out JsonElement dur))
                    {
                        double seconds = dur.GetDouble();
                        TimeSpan ts = TimeSpan.FromSeconds(seconds);
                        durationStr = $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
                    }

                    string thumbUrl = null;
                    if (root.TryGetProperty("thumbnails", out JsonElement thumbs) && thumbs.GetArrayLength() > 0)
                        thumbUrl = thumbs[0].GetProperty("url").GetString();
                    else if (root.TryGetProperty("thumbnail", out JsonElement thumb))
                        thumbUrl = thumb.GetString();

                    Image thumbImage = null;
                    if (!string.IsNullOrEmpty(thumbUrl))
                    {
                        try
                        {
                            thumbImage = await DownloadAndConvertImageAsync(thumbUrl);
                        }
                        catch { thumbImage = null; } // ignora errore immagine
                    }

                    // Ora aggiunge la riga, sicuro che c'è almeno il titolo
                    int rowIndex = dataGridView1.Rows.Add(thumbImage, title, durationStr, "Queued...");
                    dataGridView1.Rows[rowIndex].Tag = videoUrl;
                }
            }
            catch (JsonException)
            {
                MessageBox.Show("Error: Invalid JSON. Link not added..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // non aggiunge nulla
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // non aggiunge nulla
            }
        }


        #endregion

        #region Download Video
        private async void btnDownloadAll_Click(object sender, EventArgs e)
        {
            string mode = comboMode.SelectedItem?.ToString() ?? "Video";
            string extraOptions = txtOptions.Text.Trim();
            string outputFolder = Path.Combine(Environment.CurrentDirectory, "Downloads");
            Directory.CreateDirectory(outputFolder);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string url = row.Tag?.ToString();
                if (string.IsNullOrEmpty(url)) continue;

                row.Cells["Status"].Value = "In download...";
                await RunYtDlpDownloadAsync(url, mode, extraOptions, outputFolder, row);
                row.Cells["Status"].Value = "Completed!";
            }
        }

        private async Task<string> RunYtDlpInfoAsync(string url)
        {
            string ytdlpPath = Path.Combine(Application.StartupPath, "yt-dlp.exe"); // percorso completo

            if (!File.Exists(ytdlpPath))
            {
                MessageBox.Show($"The file yt-dlp.exe was not found in:\n{ytdlpPath}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "{}"; // restituisce JSON vuoto così il codice non va in crash
            }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = ytdlpPath,
                    Arguments = $"-j --no-playlist {url}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process p = Process.Start(psi))
                {
                    string output = await p.StandardOutput.ReadToEndAsync();
                    await Task.Run(() => p.WaitForExit());
                    return output;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while running yt-dlp:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "{}";
            }
        }

        private async Task<Image> DownloadAndConvertImageAsync(string url)
        {
            using var web = new System.Net.WebClient();
            byte[] data = await web.DownloadDataTaskAsync(url);

            using var image = new MagickImage(data);
            if (image.Format == MagickFormat.WebP)
                image.Format = MagickFormat.Jpeg;

            using var ms = new MemoryStream();
            image.Write(ms);
            ms.Position = 0;

            return Image.FromStream(ms);
        }

        private async Task RunYtDlpDownloadAsync(string url, string mode, string extraOptions, string outputFolder, DataGridViewRow row)
        {
            string ytdlpPath = Path.Combine(Application.StartupPath, "yt-dlp.exe"); // percorso completo

            if (!File.Exists(ytdlpPath))
            {
                MessageBox.Show($"The file yt-dlp.exe was not found in:\n{ytdlpPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                row.Cells["Status"].Value = "yt-dlp not found!";
                return;
            }

            string args = mode switch
            {
                "Video" => $"-o \"{outputFolder}\\%(title)s.%(ext)s\" -f {extraOptions} {url}",
                "Audio" => $"-o \"{outputFolder}\\%(title)s.%(ext)s\" -x {extraOptions} {url}",
                "Thumbnail" => $"-o \"{outputFolder}\\%(title)s\" --write-thumbnail --skip-download {extraOptions} {url}",
                _ => ""
            };

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = ytdlpPath,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                currentProcess = new Process { StartInfo = psi };

                currentProcess.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data) && e.Data.StartsWith("[download]") && e.Data.Contains("%"))
                    {
                        row.DataGridView.Invoke((Action)(() =>
                        {
                            row.Cells["Status"].Value = e.Data;
                            lblLastProgress.Text = e.Data;
                            txtLog.AppendText(e.Data + Environment.NewLine);
                        }));
                    }
                };

                currentProcess.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        row.DataGridView.Invoke((Action)(() =>
                        {
                            row.Cells["Status"].Value = e.Data;
                            lblLastProgress.Text = e.Data;
                            txtLog.AppendText(e.Data + Environment.NewLine);
                        }));
                    }
                };

                currentProcess.Start();
                currentProcess.BeginOutputReadLine();
                currentProcess.BeginErrorReadLine();

                await Task.Run(() => currentProcess.WaitForExit());
                currentProcess = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while downloading:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                row.Cells["Status"].Value = "Download error!";
                currentProcess = null;
            }
        }

        #endregion

        #region Stop Download
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (currentProcess == null) return;
            try
            {
                KillProcessAndChildren(currentProcess.Id);
                currentProcess = null;
                lblLastProgress.Text = "Download interrupted!";
                txtLog.AppendText("Download interrupted!" + Environment.NewLine);
            }
            catch (Exception ex) { MessageBox.Show("Error in interrupt: " + ex.Message); }
        }

        private void KillProcessAndChildren(int pid)
        {
            using (var searcher = new System.Management.ManagementObjectSearcher(
                $"Select * From Win32_Process Where ParentProcessID={pid}"))
            {
                foreach (var obj in searcher.Get())
                {
                    int childId = Convert.ToInt32(obj["ProcessID"]);
                    KillProcessAndChildren(childId);
                }
            }

            try
            {
                var proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch { }
        }
        #endregion

        #region Altri Bottoni
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;
            var result = MessageBox.Show("Do you really want to delete all links?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                txtLog.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtOptions.Text = "bv*+ba/b";
            comboMode.SelectedIndex = 0;
        }

        private void comboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboMode.SelectedItem?.ToString())
            {
                case "Video": txtOptions.Text = "bv*+ba/b"; break;
                case "Audio": txtOptions.Text = "--audio-format mp3 --audio-quality 0"; break;
                case "Thumbnail": txtOptions.Text = ""; break;
                default: txtOptions.Text = ""; break;
            }
        }
        #endregion

        private void btnResetArguments_Click(object sender, EventArgs e)
        {
            txtOptions.Text = "bv*+ba/b";
            comboMode.SelectedIndex = 0;
        }

        private void btnPasteLink_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                txtUrl.Text = Clipboard.GetText();
            }
            else
            {
                MessageBox.Show("There are no texts in the notes!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtUrl.Clear();
        }

        private void btnClearURL_Click(object sender, EventArgs e)
        {
            txtUrl.Clear();
        }
    }
}
