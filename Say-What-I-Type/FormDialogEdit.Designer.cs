using System.Windows.Forms;

namespace Say_What_I_Type
{
    partial class FormDialogEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogEdit));
            this.txt_edit_item = new System.Windows.Forms.TextBox();
            this.btn_cancel_edit = new System.Windows.Forms.Button();
            this.btn_submit_edit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_edit_item
            // 
            this.txt_edit_item.Location = new System.Drawing.Point(12, 12);
            this.txt_edit_item.Multiline = true;
            this.txt_edit_item.Name = "txt_edit_item";
            this.txt_edit_item.Size = new System.Drawing.Size(326, 43);
            this.txt_edit_item.TabIndex = 0;
            // 
            // btn_cancel_edit
            // 
            this.btn_cancel_edit.AutoSize = true;
            this.btn_cancel_edit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel_edit.Location = new System.Drawing.Point(165, 77);
            this.btn_cancel_edit.Name = "btn_cancel_edit";
            this.btn_cancel_edit.Size = new System.Drawing.Size(75, 26);
            this.btn_cancel_edit.TabIndex = 1;
            this.btn_cancel_edit.Text = "cancel";
            this.btn_cancel_edit.UseVisualStyleBackColor = true;
            this.btn_cancel_edit.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_submit_edit
            // 
            this.btn_submit_edit.AutoSize = true;
            this.btn_submit_edit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_submit_edit.Location = new System.Drawing.Point(263, 77);
            this.btn_submit_edit.Name = "btn_submit_edit";
            this.btn_submit_edit.Size = new System.Drawing.Size(75, 26);
            this.btn_submit_edit.TabIndex = 2;
            this.btn_submit_edit.Text = "submit";
            this.btn_submit_edit.UseVisualStyleBackColor = true;
            this.btn_submit_edit.Click += new System.EventHandler(this.btn_submit_edit_Click);
            // 
            // FormDialogEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 170);
            this.Controls.Add(this.btn_submit_edit);
            this.Controls.Add(this.btn_cancel_edit);
            this.Controls.Add(this.txt_edit_item);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDialogEdit";
            this.Text = "FormDialogEdit";
            this.Load += new System.EventHandler(this.FormDialogEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_edit_item;
        private System.Windows.Forms.Button btn_cancel_edit;
        private System.Windows.Forms.Button btn_submit_edit;
    }
}