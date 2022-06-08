
namespace QLDH
{
    partial class FormDangNhap
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
            this.label4 = new System.Windows.Forms.Label();
            this.btnTHOAT = new System.Windows.Forms.Button();
            this.btnDANGNHAP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMATKHAU = new System.Windows.Forms.TextBox();
            this.txtTAIKHOAN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(88, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(346, 26);
            this.label4.TabIndex = 26;
            this.label4.Text = "QUẢN LÝ CỬA HÀNG ĐỒNG HỒ";
            // 
            // btnTHOAT
            // 
            this.btnTHOAT.BackColor = System.Drawing.Color.Red;
            this.btnTHOAT.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnTHOAT.ForeColor = System.Drawing.Color.White;
            this.btnTHOAT.Location = new System.Drawing.Point(307, 300);
            this.btnTHOAT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTHOAT.Name = "btnTHOAT";
            this.btnTHOAT.Size = new System.Drawing.Size(167, 32);
            this.btnTHOAT.TabIndex = 25;
            this.btnTHOAT.Text = "THOÁT";
            this.btnTHOAT.UseVisualStyleBackColor = false;
            this.btnTHOAT.Click += new System.EventHandler(this.btnTHOAT_Click);
            // 
            // btnDANGNHAP
            // 
            this.btnDANGNHAP.BackColor = System.Drawing.Color.Blue;
            this.btnDANGNHAP.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnDANGNHAP.ForeColor = System.Drawing.Color.Transparent;
            this.btnDANGNHAP.Location = new System.Drawing.Point(56, 300);
            this.btnDANGNHAP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDANGNHAP.Name = "btnDANGNHAP";
            this.btnDANGNHAP.Size = new System.Drawing.Size(167, 32);
            this.btnDANGNHAP.TabIndex = 24;
            this.btnDANGNHAP.Text = "ĐĂNG NHẬP";
            this.btnDANGNHAP.UseVisualStyleBackColor = false;
            this.btnDANGNHAP.Click += new System.EventHandler(this.btnDANGNHAP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "MẬT KHẨU";
            // 
            // txtMATKHAU
            // 
            this.txtMATKHAU.Location = new System.Drawing.Point(214, 223);
            this.txtMATKHAU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMATKHAU.Name = "txtMATKHAU";
            this.txtMATKHAU.PasswordChar = '*';
            this.txtMATKHAU.Size = new System.Drawing.Size(220, 21);
            this.txtMATKHAU.TabIndex = 22;
            // 
            // txtTAIKHOAN
            // 
            this.txtTAIKHOAN.Location = new System.Drawing.Point(214, 142);
            this.txtTAIKHOAN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTAIKHOAN.Name = "txtTAIKHOAN";
            this.txtTAIKHOAN.Size = new System.Drawing.Size(220, 21);
            this.txtTAIKHOAN.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "TÀI KHOẢN";
            // 
            // FormDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 379);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTHOAT);
            this.Controls.Add(this.btnDANGNHAP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMATKHAU);
            this.Controls.Add(this.txtTAIKHOAN);
            this.Controls.Add(this.label2);
            this.Name = "FormDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ĐĂNG NHẬP";
            this.Load += new System.EventHandler(this.FormDangNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTHOAT;
        private System.Windows.Forms.Button btnDANGNHAP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMATKHAU;
        private System.Windows.Forms.TextBox txtTAIKHOAN;
        private System.Windows.Forms.Label label2;
    }
}