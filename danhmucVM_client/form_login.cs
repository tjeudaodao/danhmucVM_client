using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace danhmucVM_client
{
    public partial class form_login : Form
    {
        dangnhap usdangnhap = new dangnhap();
        taotaikhoan ustaotaikhoan = new taotaikhoan();

        public form_login()
        {
            InitializeComponent();

            

            usdangnhap.Location = new Point(150, 260);
            usdangnhap.Name = "usdangnhap";
            this.Controls.Add(usdangnhap);
            
            ustaotaikhoan.Location = new Point(150, 260);
            ustaotaikhoan.Name = "ustaotaikhoan";
            this.Controls.Add(ustaotaikhoan);

            usdangnhap.Hide();
            ustaotaikhoan.Hide();

            var con = ketnoisqlite.khoitao();
            string[] taikhoan = new string[2];
            taikhoan = con.laytaikhoan();

           
            if (string.IsNullOrEmpty(taikhoan[0]) || string.IsNullOrEmpty(taikhoan[1]))
            {
                ustaotaikhoan.Show();
                ustaotaikhoan.BringToFront();
            }
            else
            {
                usdangnhap.Show();
                usdangnhap.BringToFront();
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void form_login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
