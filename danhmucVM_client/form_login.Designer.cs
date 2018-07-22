namespace danhmucVM_client
{
    partial class form_login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_login));
            this.pbavatar = new System.Windows.Forms.PictureBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btn_home = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblogin = new System.Windows.Forms.Label();
            this.lbchaomung = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbavatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbavatar
            // 
            this.pbavatar.Location = new System.Drawing.Point(310, 344);
            this.pbavatar.Name = "pbavatar";
            this.pbavatar.Size = new System.Drawing.Size(219, 144);
            this.pbavatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbavatar.TabIndex = 2;
            this.pbavatar.TabStop = false;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.White;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnclose.Image = ((System.Drawing.Image)(resources.GetObject("btnclose.Image")));
            this.btnclose.Location = new System.Drawing.Point(753, 2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(45, 41);
            this.btnclose.TabIndex = 1;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btn_home
            // 
            this.btn_home.BackColor = System.Drawing.Color.White;
            this.btn_home.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_home.Image = ((System.Drawing.Image)(resources.GetObject("btn_home.Image")));
            this.btn_home.Location = new System.Drawing.Point(701, 543);
            this.btn_home.Name = "btn_home";
            this.btn_home.Size = new System.Drawing.Size(87, 45);
            this.btn_home.TabIndex = 1;
            this.btn_home.UseVisualStyleBackColor = false;
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(153, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(501, 248);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblogin
            // 
            this.lblogin.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblogin.Location = new System.Drawing.Point(310, 505);
            this.lblogin.Name = "lblogin";
            this.lblogin.Size = new System.Drawing.Size(219, 27);
            this.lblogin.TabIndex = 3;
            this.lblogin.Text = "-";
            this.lblogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbchaomung
            // 
            this.lbchaomung.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbchaomung.Location = new System.Drawing.Point(310, 300);
            this.lbchaomung.Name = "lbchaomung";
            this.lbchaomung.Size = new System.Drawing.Size(219, 27);
            this.lbchaomung.TabIndex = 3;
            this.lbchaomung.Text = "-";
            this.lbchaomung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // form_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lbchaomung);
            this.Controls.Add(this.lblogin);
            this.Controls.Add(this.pbavatar);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btn_home);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "form_login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VM_login";
            this.Load += new System.EventHandler(this.form_login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbavatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_home;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.PictureBox pbavatar;
        private System.Windows.Forms.Label lblogin;
        private System.Windows.Forms.Label lbchaomung;
    }
}