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
using System.Diagnostics;
namespace Say_What_I_Type
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synthesizer;
        private Dictionary<string, string> languages;
        private const string FilePath = "data.txt";
        private bool isSpeaking = false; // Biến trạng thái để theo dõi trạng thái nói

        public Form1()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted; // Đăng ký sự kiện khi hoàn thành nói
            LoadLanguages();
            LoadDataToListView();
            listView1.MouseDoubleClick += new MouseEventHandler(listView1_MouseDoubleClick);
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            select_lang.Items.Add("Add Voice");
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e) { }

        private void btn_say_Click(object sender, EventArgs e)
        {
            if (isSpeaking)
            {
                // Nếu đang nói thì dừng lại và đổi nhãn nút về "Say"
                synthesizer.SpeakAsyncCancelAll();
                isSpeaking = false;
                btn_say.Text = "SAY WHAT I TYPE";
            }
            else
            {
                string textToSpeak = txt_say.Text;
                // Nếu không có nội dung thì không làm gì
                if (string.IsNullOrWhiteSpace(textToSpeak))
                {
                    string text = "Please say something to say";
                    synthesizer.SpeakAsync(text);
                    return;
                }

                // Kiểm tra nếu không có ngôn ngữ nào được chọn
                if (select_lang.SelectedItem == null && select_lang.Items.Count > 0)
                {
                    select_lang.SelectedIndex = 0; // Chọn ngôn ngữ đầu tiên
                }

                // Lấy ngôn ngữ đã chọn
                var selectedVoice = select_lang.SelectedItem.ToString();
                foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    string voiceName = $"{info.Name} ({info.Gender}, {info.Age})";
                    if (voiceName == selectedVoice)
                    {
                        synthesizer.SelectVoice(info.Name);
                        break;
                    }
                }

                // Đọc ra nội dung và đổi nhãn nút thành "Stop"
                synthesizer.SpeakAsync(textToSpeak);
                isSpeaking = true;
                btn_say.Text = "Stop";
            }
        }

        private void Synthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            // Khi hoàn thành nói, đổi nhãn nút về "Say"
            isSpeaking = false;
            btn_say.Text = "SAY WHAT I TYPE";
        }

        private void LoadLanguages()
        {
            foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                string voiceName = $"{info.Name} ({info.Gender}, {info.Age})";
                select_lang.Items.Add(voiceName);
            }

            // Nếu có ngôn ngữ nào thì chọn ngôn ngữ đầu tiên
            if (select_lang.Items.Count > 0)
            {
                select_lang.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                string selectedLanguage = select_lang.SelectedItem.ToString();

                if (selectedLanguage == "Add Voice")
                {
                try
                {
                    // Mở phần "Time & Language" của ứng dụng Settings
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "ms-settings:speech",
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở phần Time & Language. Lỗi: " + ex.Message);
                }
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.saywhatitype.com/";
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở trang web. Lỗi: " + ex.Message);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e) { }

        private void tabPage1_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e) { }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string name = selectedItem.SubItems[1].Text;
                // Lấy ngôn ngữ đã chọn
                var selectedVoice = select_lang.SelectedItem.ToString();
                foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    string voiceName = $"{info.Name} ({info.Gender}, {info.Age})";
                    if (voiceName == selectedVoice)
                    {
                        synthesizer.SelectVoice(info.Name);
                        break;
                    }
                }
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
            FormDialogEdit editForm = new FormDialogEdit
            {
                IdToEdit = idToEdit,
                NameToEdit = nameToEdit
            };

            // Hiển thị form
            editForm.ShowDialog();
            LoadDataToListView();
        }

        private void btn_dropdow_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormDialog())
            {
                dialog.ShowDialog();
            }
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

                            ListViewItem item = new ListViewItem
                            {
                                Text = name,
                                Tag = id
                            };
                            item.SubItems.Add(name);

                            listView1.Items.Add(item);
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
