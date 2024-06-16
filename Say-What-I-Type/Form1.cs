using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;

namespace Say_What_I_Type
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synthesizer;
        private Dictionary<string, string> languages;
        private const string SettingsKey = "UserData";

        public Form1()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            LoadLanguages();
            LoadDataToListView();
            listView1.MouseDoubleClick += ListViewItemDoubleClicked;
            listView1.SelectedIndexChanged += ListViewSelectedIndexChanged;
        }

        /// <summary>
        /// Loads the available languages and their corresponding culture codes.
        /// </summary>
        private void LoadLanguages()
        {
            languages = new Dictionary<string, string>
            {
                {"English", "en-US"},
                {"Spanish", "es-ES"},
                {"Chinese", "zh-CN"},
                // ... Add other languages here
            };

            select_lang.Items.AddRange(languages.Keys.ToArray());
            select_lang.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the "Say" button.
        /// </summary>
        private void btn_say_Click(object sender, EventArgs e)
        {
            string textToSpeak = txt_say.Text;
            if (string.IsNullOrWhiteSpace(textToSpeak))
            {
                ShowMessageAndSpeak("Please say something to say");
                return;
            }

            SetSpeechSynthesizerLanguage();
            synthesizer.SpeakAsync(textToSpeak);
        }

        /// <summary>
        /// Sets the language of the speech synthesizer based on the selected language.
        /// </summary>
        private void SetSpeechSynthesizerLanguage()
        {
            var selectedLanguage = select_lang.SelectedItem.ToString();
            if (languages.ContainsKey(selectedLanguage))
            {
                var culture = languages[selectedLanguage];
                try
                {
                    synthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo(culture));
                }
                catch (Exception ex)
                {
                    ShowMessageAndSpeak($"Unable to find a suitable voice for the selected language. Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Shows a message box and speaks the message using the speech synthesizer.
        /// </summary>
        /// <param name="message">The message to be shown and spoken.</param>
        private void ShowMessageAndSpeak(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            synthesizer.SpeakAsync(message);
        }

        /// <summary>
        /// Loads the user data from the application settings and populates the ListView.
        /// </summary>
        private void LoadDataToListView()
        {
            try
            {
                listView1.Items.Clear();
                var userData = ConfigurationManager.AppSettings[SettingsKey];
                if (!string.IsNullOrEmpty(userData))
                {
                    var lines = userData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    var itemList = new List<ListViewItem>();

                    foreach (var line in lines)
                    {
                        var sb = new StringBuilder(line);
                        sb.Replace("{", "").Replace("}", "").Replace("\"", "");
                        var parts = sb.ToString().Split(new[] { "id :", "name:" }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 2)
                        {
                            var id = parts[0].Trim();
                            var name = parts[1].Trim();
                            itemList.Add(new ListViewItem(new[] { id, name }) { Tag = id });
                        }
                    }

                    listView1.Items.AddRange(itemList.ToArray());
                }
            }
            catch (Exception ex)
            {
                ShowMessageAndSpeak($"Error loading data: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the double-click event of a ListView item.
        /// </summary>
        private void ListViewItemDoubleClicked(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedItem = listView1.SelectedItems[0];
                var name = selectedItem.SubItems[1].Text;
                var id = selectedItem.Tag.ToString();
                OpenEditForm(id, name);
            }
        }

        /// <summary>
        /// Opens the edit form for the selected ListView item.
        /// </summary>
        /// <param name="idToEdit">The ID of the item to be edited.</param>
        /// <param name="nameToEdit">The name of the item to be edited.</param>
        private void OpenEditForm(string idToEdit, string nameToEdit)
        {
            using (var editForm = new FormDialogEdit())
            {
                editForm.IdToEdit = idToEdit;
                editForm.NameToEdit = nameToEdit;
                editForm.ShowDialog();
                LoadDataToListView();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ListView.
        /// </summary>
        private void ListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedItem = listView1.SelectedItems[0];
                var name = selectedItem.SubItems[1].Text;
                synthesizer.SpeakAsync(name);
            }
        }

        /// <summary>
        /// Handles the Click event of the "Add" button.
        /// </summary>
        private void btn_dropdow_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormDialog())
            {
                dialog.ShowDialog();
                LoadDataToListView();
            }
        }
    }

    public partial class FormDialog : Form
    {
        private const string SettingsKey = "UserData";

        public FormDialog()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string id = txt_id.Text.Trim();
            string name = txt_name.Text.Trim();

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter both an ID and a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var userData = ConfigurationManager.AppSettings[SettingsKey];
            var newData = $"{{\r\n\"id\": \"{id}\", \"name\": \"{name}\"\r\n}}";

            if (!string.IsNullOrEmpty(userData))
            {
                userData += $",\r\n{newData}";
            }
            else
            {
                userData = newData;
            }

            ConfigurationManager.AppSettings[SettingsKey] = userData;

            MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_say = new System.Windows.Forms.TextBox();
            this.btn_say = new System.Windows.Forms.Button();
            this.select_lang = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_dropdow = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(570, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text To Speech Softweva";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txt_say
            // 
            this.txt_say.BackColor = System.Drawing.Color.White;
            this.txt_say.ForeColor = System.Drawing.Color.Black;
            this.txt_say.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txt_say.Location = new System.Drawing.Point(15, 6);
            this.txt_say.Multiline = true;
            this.txt_say.Name = "txt_say";
            this.txt_say.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_say.Size = new System.Drawing.Size(523, 250);
            this.txt_say.TabIndex = 1;
            this.txt_say.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_say
            // 
            this.btn_say.BackColor = System.Drawing.Color.Lime;
            this.btn_say.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_say.ForeColor = System.Drawing.Color.White;
            this.btn_say.Location = new System.Drawing.Point(291, 262);
            this.btn_say.Name = "btn_say";
            this.btn_say.Size = new System.Drawing.Size(253, 59);
            this.btn_say.TabIndex = 2;
            this.btn_say.Text = "SAY WHAT I TYPE";
            this.btn_say.UseVisualStyleBackColor = false;
            this.btn_say.Click += new System.EventHandler(this.btn_say_Click);
            // 
            // select_lang
            // 
            this.select_lang.FormattingEnabled = true;
            this.select_lang.Location = new System.Drawing.Point(15, 277);
            this.select_lang.Name = "select_lang";
            this.select_lang.Size = new System.Drawing.Size(230, 33);
            this.select_lang.TabIndex = 3;
            this.select_lang.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(408, 432);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(155, 25);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Made By AlanC ";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(558, 374);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.select_lang);
            this.tabPage1.Controls.Add(this.txt_say);
            this.tabPage1.Controls.Add(this.btn_say);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(550, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Convert Text To Speech";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_dropdow);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(550, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Talk About Me";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // btn_dropdow
            // 
            this.btn_dropdow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_dropdow.AutoSize = true;
            this.btn_dropdow.Location = new System.Drawing.Point(510, 7);
            this.btn_dropdow.Name = "btn_dropdow";
            this.btn_dropdow.Size = new System.Drawing.Size(34, 35);
            this.btn_dropdow.TabIndex = 1;
            this.btn_dropdow.Text = "+";
            this.btn_dropdow.UseVisualStyleBackColor = true;
            this.btn_dropdow.Click += new System.EventHandler(this.btn_dropdow_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(544, 285);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Columns.Add("ID", 50);
            this.listView1.Columns.Add("Name", 200);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(570, 466);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "SAY WHAT I TYPE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

    public partial class FormDialogEdit : Form
    {
        public string IdToEdit { get; set; }
        public string NameToEdit { get; set; }

        private const string SettingsKey = "UserData";

        public FormDialogEdit()
        {
            InitializeComponent();
        }

        private void FormDialogEdit_