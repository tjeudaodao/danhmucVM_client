using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace danhmucVM_client
{
    public partial class dangnhap : UserControl
    {
        public dangnhap()
        {
            InitializeComponent();
            var consqlite = ketnoisqlite.khoitao();
            string[] tk = consqlite.laytaikhoan();
            txttaikhoan.Text = tk[0];
            txtmatkhau.Text = tk[1];

            btndangnhap.Select();
            btndangnhap.Focus();
        }

        private void txttaikhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtmatkhau.Focus();
            }
        }

        private void txtmatkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btndangnhap.Focus();
            }
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            Program.moFrom = true;
            ((Form)this.TopLevelControl).Close();
        }

        private void chb_ghinho_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_ghinho.Checked)
            {
                var consqlie = ketnoisqlite.khoitao();
                consqlie.update_ghinho("OK");
            }
            else
            {
                var consqlie = ketnoisqlite.khoitao();
                consqlie.update_ghinho("NOTOK");
            }
        }

        private void dangnhap_Load(object sender, EventArgs e)
        {
            var consqlie = ketnoisqlite.khoitao();
            if (consqlie.laygiatri_ghinho() == "OK")
            {
                chb_ghinho.Checked = true;
            }
            else chb_ghinho.Checked = false;
        }
    }
}
