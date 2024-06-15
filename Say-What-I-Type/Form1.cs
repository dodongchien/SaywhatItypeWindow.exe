using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.Xml.Linq;

namespace Say_What_I_Type
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synthesizer;
        private Dictionary<string, string> languages;
        private const string FilePath = "data.txt";
        public Form1()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            LoadLanguages();
            LoadDataToListView();
            listView1.MouseDoubleClick += new MouseEventHandler(listView1_MouseDoubleClick);
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btn_say_Click(object sender, EventArgs e)
        {
            string textToSpeak = txt_say.Text;
            // Nếu không có nội dung thì không làm gì
            if (string.IsNullOrWhiteSpace(textToSpeak))
            {
                string text = "Please say something to say";
                synthesizer.SpeakAsync(text);
                return;
            }

            // Lấy ngôn ngữ đã chọn từ comboBox1
            var selectedLanguage = select_lang.SelectedItem.ToString();
            if (languages.ContainsKey(selectedLanguage))
            {
                var culture = languages[selectedLanguage];
                try
                {
                    Console.WriteLine(culture);
                    // Tìm giọng nói phù hợp với văn hóa (culture)
                    synthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new CultureInfo(culture));

                    Console.WriteLine("vào");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tìm thấy giọng nói phù hợp cho ngôn ngữ đã chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Đọc ra nội dung
            synthesizer.SpeakAsync(textToSpeak);
        }
        private void LoadLanguages()
        {
            // Danh sách 50 ngôn ngữ phổ biến
            languages = new Dictionary<string, string>
            {
                { "English", "en-US" },
                { "Spanish", "es-ES" },
                { "Chinese", "zh-CN" },
                { "Hindi", "hi-IN" },
                { "Arabic", "ar-SA" },
                { "Portuguese", "pt-PT" },
                { "Bengali", "bn-IN" },
                { "Russian", "ru-RU" },
                { "Japanese", "ja-JP" },
                { "Punjabi", "pa-IN" },
                { "German", "de-DE" },
                { "Korean", "ko-KR" },
                { "French", "fr-FR" },
                { "Turkish", "tr-TR" },
                { "Vietnamese", "vi-VN" },
                { "Italian", "it-IT" },
                { "Thai", "th-TH" },
                { "Dutch", "nl-NL" },
                { "Greek", "el-GR" },
                { "Czech", "cs-CZ" },
                { "Swedish", "sv-SE" },
                { "Polish", "pl-PL" },
                { "Finnish", "fi-FI" },
                { "Hebrew", "he-IL" },
                { "Hungarian", "hu-HU" },
                { "Indonesian", "id-ID" },
                { "Malay", "ms-MY" },
                { "Norwegian", "no-NO" },
                { "Romanian", "ro-RO" },
                { "Slovak", "sk-SK" },
                { "Danish", "da-DK" },
                { "Filipino", "fil-PH" },
                { "Croatian", "hr-HR" },
                { "Bulgarian", "bg-BG" },
                { "Lithuanian", "lt-LT" },
                { "Latvian", "lv-LV" },
                { "Slovenian", "sl-SI" },
                { "Estonian", "et-EE" },
                { "Serbian", "sr-RS" },
                { "Ukrainian", "uk-UA" },
                { "Urdu", "ur-PK" },
                { "Farsi", "fa-IR" },
                { "Tamil", "ta-IN" },
                { "Telugu", "te-IN" },
                { "Marathi", "mr-IN" },
                { "Gujarati", "gu-IN" },
                { "Kannada", "kn-IN" },
                { "Malayalam", "ml-IN" }
            };

            foreach (var language in languages)
            {
                select_lang.Items.Add(language.Key);
            }

            // Chọn ngôn ngữ đầu tiên làm mặc định
            if (select_lang.Items.Count > 0)
            {
                select_lang.SelectedIndex = 0;
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.saywhatitype.com/";

            // Mở trang web trong trình duyệt mặc định của hệ thống
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở trang web. Lỗi: " + ex.Message);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string name = selectedItem.SubItems[1].Text;
                synthesizer.SpeakAsync(name);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0]; // Sửa đổi từ 1 thành 0
                string name = selectedItem.SubItems[1].Text;
                string id = selectedItem.Tag.ToString();
                OpenEditForm(id, name);
            }
        }

        private void OpenEditForm(string idToEdit, string nameToEdit)
        {
            // Tạo một instance của FormDialogEdit
            FormDialogEdit editForm = new FormDialogEdit();

            // Thiết lập giá trị cho thuộc tính NameToEdit
            editForm.IdToEdit = idToEdit;
            editForm.NameToEdit = nameToEdit;

            // Hiển thị form
            editForm.ShowDialog();
            LoadDataToListView();
        }
        private void btn_dropdow_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormDialog())
                dialog.ShowDialog();
            LoadDataToListView();
        }

        private void LoadDataToListView()
        {
            try
            {
                listView1.Items.Clear();
                if (File.Exists(FilePath))
                {
                    string[] lines = File.ReadAllLines(FilePath);
                    foreach (string line in lines)
                    {
                        // Loại bỏ các ký tự thừa và cắt chuỗi để lấy id và name
                        string cleanedLine = line.Replace("{", "").Replace("}", "").Replace("\"", "");
                        string[] parts = cleanedLine.Split(new string[] { "id :", "name:" }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 2)
                        {
                            string id = parts[0].Trim(); // ID
                            string name = parts[1].Trim(); // Tên

                            ListViewItem item = new ListViewItem();
                            item.Text = name; // Set tên làm Text của ListViewItem
                            item.SubItems.Add(name); // Thêm ID vào SubItems (nếu cần)
                            item.Tag = id; // Gắn Tag là ID

                            listView1.Items.Add(item); // Thêm ListViewItem vào ListView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
