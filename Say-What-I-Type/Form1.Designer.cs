using System.Drawing;
using System.Windows.Forms;

namespace Say_What_I_Type
{
    partial class Form1
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
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(544, 285);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
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
 

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_say;
        private System.Windows.Forms.Button btn_say;
        private System.Windows.Forms.ComboBox select_lang;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_dropdow;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}

