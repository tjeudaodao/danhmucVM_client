using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace danhmucVM_client
{
    public partial class form_login : Form
    {
        dangnhap usdangnhap = new dangnhap();
        taotaikhoan ustaotaikhoan = new taotaikhoan();
        Thread chay3giay;
        //Thread khoidong;

        private ManualResetEvent dieukhien = new ManualResetEvent(true);

        public form_login()
        {
            InitializeComponent();
            
        }
        //void khoidongct()
        //{
        //    var con = ketnoisqlite.khoitao();

        //    var conmy = ketnoi.Instance();

        //    string ghinho = con.laygiatri_ghinho();
        //    string[] tentk = con.laytaikhoan();

        //    bool check = conmy.kiemtraTaikhoan(tentk[0], tentk[1]);
        //    if (ghinho != "OK")
        //    {
        //        hamload();
        //    }
        //    else if (ghinho == "OK" && !string.IsNullOrEmpty(tentk[0]) && !string.IsNullOrEmpty(tentk[1]) && check)
        //    {
        //        lbchaomung.Invoke(new MethodInvoker(delegate ()
        //        {
        //            lbchaomung.Text = "WELCOME ... " + tentk[0].ToUpper();
        //        }));
                
        //        chay3giay = new Thread(ham3giay);
        //        chay3giay.IsBackground = true;
        //        chay3giay.Start();
        //    }
        //    else
        //    {
        //        this.Invoke(new MethodInvoker(delegate ()
        //        {

        //            MessageBox.Show("Xem lại tài khoản và mật khẩu");
        //        }));
        //        usdangnhap.Location = new Point(150, 260);
        //        usdangnhap.Name = "usdangnhap";
        //        this.Controls.Add(usdangnhap);

        //        usdangnhap.Show();
        //        usdangnhap.BringToFront();
        //    }
        //}
        void hamload()
        {
            var con = ketnoisqlite.khoitao();
            string[] taikhoan = new string[2];
            taikhoan = con.laytaikhoan();


            if (string.IsNullOrEmpty(taikhoan[0]) || string.IsNullOrEmpty(taikhoan[1]))
            {
                ustaotaikhoan.Location = new Point(150, 260);
                ustaotaikhoan.Name = "ustaotaikhoan";
                this.Controls.Add(ustaotaikhoan);

                ustaotaikhoan.Show();
                ustaotaikhoan.BringToFront();
            }
            else
            {
                usdangnhap.Location = new Point(150, 260);
                usdangnhap.Name = "usdangnhap";
                this.Controls.Add(usdangnhap);

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
            try
            {
                //khoidong = new Thread(khoidongct);
                //khoidong.IsBackground = true;
                //khoidong.Start();
                if (!Directory.Exists(Application.StartupPath + @"\luuanh"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\luuanh");
                }
                var con = ketnoisqlite.khoitao();

                var conmy = ketnoi.Instance();

                string ghinho = con.laygiatri_ghinho();
                string[] tentk = con.laytaikhoan();

                bool check = conmy.kiemtraTaikhoan(tentk[0], tentk[1]);
                if (ghinho != "OK")
                {
                    hamload();
                }
                else if (ghinho == "OK" && !string.IsNullOrEmpty(tentk[0]) && !string.IsNullOrEmpty(tentk[1]) && check)
                {
                    lbchaomung.Text = "WELCOME ... " + tentk[0].ToUpper();


                    chay3giay = new Thread(ham3giay);
                    chay3giay.IsBackground = true;
                    chay3giay.Start();
                }
                else
                {
                        MessageBox.Show("Xem lại tài khoản và mật khẩu");
                 
                    usdangnhap.Location = new Point(150, 260);
                    usdangnhap.Name = "usdangnhap";
                    this.Controls.Add(usdangnhap);

                    usdangnhap.Show();
                    usdangnhap.BringToFront();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi kết nối mạng");
                return;
            }
            
        }
        void ham3giay()
        {
            pbavatar.Image = Properties.Resources.loading_meo;
            for (int i = 3; i > 0; i--)
            {
                lblogin.Invoke(new MethodInvoker(delegate ()
                {
                    lblogin.Text = "Đăng nhập trong ... " + i.ToString();
                }));
                Thread.Sleep(1000);
                dieukhien.WaitOne(Timeout.Infinite);
            }
            Program.moFrom = true;
            this.Invoke(new MethodInvoker(delegate ()
            {
                ((Form)this.TopLevelControl).Close();
            }));

            dieukhien.WaitOne(Timeout.Infinite);
        }
        private void btn_home_Click(object sender, EventArgs e)
        {
            
            dieukhien.Reset();
            usdangnhap.Location = new Point(150, 260);
            usdangnhap.Name = "usdangnhap";
            this.Controls.Add(usdangnhap);

            usdangnhap.Show();
            usdangnhap.BringToFront();
        }
    }
}
