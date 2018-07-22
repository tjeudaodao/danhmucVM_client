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
    public partial class taotaikhoan : UserControl
    {
        public taotaikhoan()
        {
            InitializeComponent();
        }
        void hamDangky()
        {
            if (!string.IsNullOrEmpty(txttaikhoan.Text) && !string.IsNullOrEmpty(txtmatkhau.Text))
            {
                var conmy = ketnoi.Instance();
                var conlite = ketnoisqlite.khoitao();

                string tk = conmy.kiemtraTaikhoan(txttaikhoan.Text);
                if (tk == null)
                {
                    conmy.taotaikhoanmoi(txttaikhoan.Text, txtmatkhau.Text);
                    conlite.updatetaikhoan(txttaikhoan.Text, txtmatkhau.Text);
                    MessageBox.Show("Tạo tài khoản thành công\nTên tài khoản là: \"" + txttaikhoan.Text + "\"");
                }
                else
                {
                    MessageBox.Show("Tên tài khoản: \" " + tk + " \" đã tồn tại\nChọn tên khác");
                    txttaikhoan.Clear();
                    txtmatkhau.Clear();
                    txttaikhoan.Focus();
                }
            }
            else
            {
                MessageBox.Show("Không được để trống trường dữ liệu nào");
                txttaikhoan.Clear();
                txtmatkhau.Clear();
                txttaikhoan.Focus();
            }
        }
        private void btntaotaikhoan_Click(object sender, EventArgs e)
        {
            hamDangky();
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
                btntaotaikhoan.Focus();
            }
        }
    }
}
