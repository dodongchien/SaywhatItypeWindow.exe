using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;


namespace Say_What_I_Type
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synthesizer;
        private Dictionary<string, string> languages;
        public Form1()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            LoadLanguages();
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
    }
}
