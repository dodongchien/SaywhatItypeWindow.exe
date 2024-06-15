using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Say_What_I_Type
{
    public partial class FormDialog : Form
    {
        private const string FilePath = "data.txt";

        public FormDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            string input = txt_add_item.Text;
            // Tạo ID duy nhất (sử dụng thời gian hiện tại)
            string id = "item_" + DateTime.Now.Ticks.ToString();

            // Lưu dữ liệu vào tệp
            SaveData(id, input);

            MessageBox.Show("Add data successfully.", "Edit Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveData(string id, string name)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    sw.WriteLine($"{{id : \"{id}\",name:\"{name}\"}}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

