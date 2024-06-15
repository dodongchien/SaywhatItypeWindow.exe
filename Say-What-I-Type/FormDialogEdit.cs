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
    public partial class FormDialogEdit : Form
    {
        public string NameToEdit { get; set; }
        public string IdToEdit { get; set; }

        private const string FilePath = "data.txt";
        public FormDialogEdit()
        {
            InitializeComponent();
        }


        private void FormDialogEdit_Load(object sender, EventArgs e)
        {
            txt_edit_item.Text = NameToEdit;    
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_submit_edit_Click(object sender, EventArgs e)
        {
            string id = IdToEdit;
            string newName = txt_edit_item.Text;

            EditData(id, newName);
        }
        private void EditData(string id, string newName)
        {
            try
            {
                // Đọc dữ liệu từ tệp
                List<string> data = File.ReadAllLines(FilePath).ToList();

                // Tìm mục cần sửa
                int index = -1;
                for (int i = 0; i < data.Count; i++)
                {
                    string line = data[i];
                    // Loại bỏ các ký tự thừa và cắt chuỗi để lấy id và name
                    string cleanedLine = line.Replace("{", "").Replace("}", "").Replace("\"", "");
                    string[] parts = cleanedLine.Split(new string[] { "id :", "name:" }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 2)
                    {
                        string existingId = parts[0].Trim();
                        string existingName = parts[1].Trim();

                        if (existingId == id)
                        {
                            // Cập nhật dữ liệu mới
                            string updatedLine = $"{{id : \"{id}\",name:\"{newName}\"}}";
                            data[i] = updatedLine;

                            // Ghi lại dữ liệu vào tệp
                            File.WriteAllLines(FilePath, data);

                            MessageBox.Show("Data updated successfully.", "Edit Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return; // Thoát khỏi hàm sau khi đã cập nhật thành công
                        }
                    }
                }

                // Nếu không tìm thấy id tương ứng
                MessageBox.Show($"Item with ID {id} not found.", "Edit Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
