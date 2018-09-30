using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using Tulpep.NotificationWindow;
using System.Globalization;
using AnhLuu = danhmucVM_client.Properties.Resources;
using System.Threading;
using System.IO;

namespace danhmucVM_client
{
    public partial class Formchinh : Form
    {

        System.Media.SoundPlayer phatduocban = new System.Media.SoundPlayer(Properties.Resources.duocban);
        System.Media.SoundPlayer phatchuaduocban = new System.Media.SoundPlayer(Properties.Resources.chuaban);
        private static bool phatAM = true;
        public void phatAMTHANH_BAN()
        {
            if (phatAM)
            {
                phatduocban.Play();
            }
        }
        public void phatAMTHANH_KOBAN()
        {
            if (phatAM)
            {
                phatchuaduocban.Play();
            }
        }
        string duongdanchuaanh = Application.StartupPath + @"\luuanh\";
        static string ngaychonbandau = null;
        static int sodongchon = 0;
        static bool phathaykhongphat = true;

        private ManualResetEvent dieukhienthread = new ManualResetEvent(true);
        Thread tudongloadanh;
        Thread capnhatanhmoi;
        Thread LoadLandau;
        
        public Formchinh()
        {
            InitializeComponent();
            if (!Directory.Exists(duongdanchuaanh))
            {
                Directory.CreateDirectory(duongdanchuaanh);
            }
            LoadLandau = new Thread(loadLandautien);
            LoadLandau.IsBackground = true;
            LoadLandau.Start();

            tudongloadanh = new Thread(hamtudongloadanh);
            tudongloadanh.IsBackground = true;
            tudongloadanh.Start();

            capnhatanhmoi = new Thread(taianhstuserver);
            capnhatanhmoi.IsBackground = true;
            capnhatanhmoi.Start();
        }
        void loadLandautien()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    var con = ketnoi.Instance();
                    ngaychonbandau = con.layngayganhat();

                    datag1.Invoke(new MethodInvoker(delegate ()
                    {
                        datag1.DataSource = con.laythongtinngayganhat(ngaychonbandau);
                    }));
                    lbtongma.Invoke(new MethodInvoker(delegate ()
                    {
                        lbtongma.Text = datag1.Rows.Count.ToString();
                    }));
                    lbtrangthai.Invoke(new MethodInvoker(delegate ()
                    {
                        lbtrangthai.Text = "File cập nhật gần nhất: -->> "+con.laytenFile();
                    }));
                    Thread.Sleep(300000);
                }
                
            }
            catch (Exception ex)
            {
                NotificationHts("Có vấn đề");
                lbtrangthai.Invoke(new MethodInvoker(delegate ()
                {
                    lbtrangthai.Text = ex.ToString();
                }));
               
            }
        }
        void taianhstuserver()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    ftp ftpcapnhat = new ftp(@"ftp://27.72.29.28", "vm_kho", "2785");
                    string[] danhsachfileanh_SV = ftpcapnhat.directoryListSimple("/danhmuchangmoi/Danhmuc27lvl/bin/Release/luuanh/");
                    string[] danhsachfile = Directory.GetFiles(Application.StartupPath + @"\luuanh\");
                    //Console.WriteLine(danhsachfileanh_SV[1]);
                    for (int i = 0; i < danhsachfileanh_SV.Length; i++)
                    {
                        if (!File.Exists(duongdanchuaanh + danhsachfileanh_SV[i]))
                        {
                            ftpcapnhat.download("/danhmuchangmoi/Danhmuc27lvl/bin/Release/luuanh/" + danhsachfileanh_SV[i], duongdanchuaanh + danhsachfileanh_SV[i]);
                        }
                    }
                    Thread.Sleep(600000);
                }
            }
            catch (Exception)
            {

                return;
            }
        }
        void hamtudongloadanh()
        {
            while (true)
            {
                Thread.Sleep(2000);
                string[] tonghopanh = Directory.GetFiles(Application.StartupPath + @"\luuanh\");
                for (int i = 0; i < tonghopanh.Length; i++)
                {
                    pbanhsanpham.Invoke(new MethodInvoker(delegate ()
                    {
                        pbanhsanpham.ImageLocation = tonghopanh[i];
                    }));
                    lbmahang.Invoke(new MethodInvoker(delegate ()
                    {
                        lbmahang.Text = Path.GetFileNameWithoutExtension(tonghopanh[i]);
                    }));

                    Thread.Sleep(2000);

                    dieukhienthread.WaitOne(Timeout.Infinite);
                }
            }

        }
        /// <summary>
        /// 
        /// cac ham phuc vu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Formchinh_Load(object sender, EventArgs e)
        {
            var con = ketnoisqlite.khoitao();
            string tentk = con.laytentaikhoan();
            ketnoi.SettenaCottrunghang(tentk);
            this.Text = "Danh mục VM -- Tài khoản : " + tentk.ToUpper();
        }
        void laythongtinvaolabel(string mahang)
        {
            var conlite = ketnoi.Instance();
            var ham = hamtao.Khoitao();
            List<laythongtin> laytt = new List<laythongtin>();
            laytt = conlite.loclaythongtin1ma(mahang);
            string kiemtra = conlite.Kiemtra("matong", "hangduocban", mahang);
            if (kiemtra != null)
            {
                foreach (laythongtin tt in laytt)
                {
                    lbmotasanpham.Text = tt.Motamaban + " - " + tt.Chudemaban + " - " + tt.Ghichu;
                    lbngayban.Text = tt.Ngayduocban;
                    DateTime dt1 = DateTime.ParseExact(tt.Ngayduocban, "dd/MM/yyyy", null);
                    if (dt1 <= DateTime.Now)
                    {
                        lbduocbanhaychua.Text = "Được bán";
                        phatAMTHANH_BAN();
                    }
                    else
                    {
                        lbdatrunghaychua.Text = "Chưa được bán";
                        phatAMTHANH_KOBAN();
                    }
                    string trunghang = conlite.laythongtintrunghang(mahang);
                    if (trunghang == null)
                    {
                        lbdatrunghaychua.Text = "Chưa trưng bán";
                    }
                    else
                    {
                        lbdatrunghaychua.Text = trunghang;
                    }
                    loadanh(mahang);
                }
            }
            else if (kiemtra == null)
            {
                lbdatrunghaychua.Text = "Chưa trưng bán";
                lbduocbanhaychua.Text = "Chưa được bán";
                phatAMTHANH_KOBAN();
                loadanh(mahang);
            }

        }
        void loadanh(string tenanh)
        {
            if (File.Exists(duongdanchuaanh + tenanh + ".png"))
            {
                dungphatanh();
                pbanhsanpham.ImageLocation = duongdanchuaanh + tenanh + ".png";
                lbmahang.Text = tenanh;

            }
            else
            {
                pbanhsanpham.Image = Properties.Resources.bombs;
            }
        }

        void updatetrunghangthanhdatrung()
        {
            try
            {
                var con = ketnoi.Instance();
                if (datag1.SelectedRows.Count > 0)
                {
                    string matong = null;
                    foreach (DataGridViewRow row in datag1.SelectedRows)
                    {
                        matong = row.Cells[0].Value.ToString();
                        con.updatedatrunghangthanhdatrung(matong);
                        sodongchon = datag1.SelectedRows.Count;
                        NotificationHts("Vừa cập nhật : " + sodongchon.ToString() + " mã hàng");
                    }
                    if (ngaychonbandau == null)
                    {
                        ngaychonbandau = con.layngayganhat();
                    }
                    datag1.DataSource = con.laythongtinkhichonngay(ngaychonbandau);
                }
            }
            catch (Exception ex)
            {
                NotificationHts("Có vấn đề");
                lbtrangthai.Text = ex.ToString();
            }
            

        }
        void updatetrunghangthanhchuatrung()
        {
            try
            {
                var con = ketnoi.Instance();
                if (datag1.SelectedRows.Count > 0)
                {
                    string matong = null;
                    foreach (DataGridViewRow row in datag1.SelectedRows)
                    {
                        matong = row.Cells[0].Value.ToString();
                        con.updatetrunghangthanhchuatrung(matong);
                        sodongchon = datag1.SelectedRows.Count;
                        NotificationHts("Vừa cập nhật : " + sodongchon.ToString() + " mã hàng");
                    }
                    if (ngaychonbandau == null)
                    {
                        ngaychonbandau = con.layngayganhat();
                    }
                    datag1.DataSource = con.laythongtinkhichonngay(ngaychonbandau);
                }
            }
            catch (Exception ex)
            {
                NotificationHts("Có vấn đề");
                lbtrangthai.Text = ex.ToString();
            }
            

        }
        void NotificationHts(string noidung)
        {
            PopupNotifier pop = new PopupNotifier();
            pop.TitleText = "Thông báo";
            pop.ContentText = "\" " + noidung + " \"";
            pop.Image = Properties.Resources.totoro1;
            pop.IsRightToLeft = false;
            pop.TitleColor = System.Drawing.Color.Lime;
            pop.TitleFont = new System.Drawing.Font("Comic Sans MS", 10, System.Drawing.FontStyle.Underline);
            pop.BodyColor = System.Drawing.Color.DimGray;
            pop.Size = new System.Drawing.Size(380, 130);
            pop.ImageSize = new System.Drawing.Size(100, 100);
            pop.ImagePadding = new Padding(15);
            pop.ContentColor = System.Drawing.Color.White;
            pop.ContentFont = new System.Drawing.Font("Comic Sans MS", 12, System.Drawing.FontStyle.Bold);
            pop.Delay = 3500;
            pop.BorderColor = System.Drawing.Color.DimGray;
            pop.HeaderHeight = 1;
            pop.Popup();
        }

        void updatesoluongtrenbang()
        {
            lbtongma.Text = datag1.Rows.Count.ToString();
        }
        #region Thao tac xu kien
        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(txtbarcode.Text))
                    {
                        var consql = ketnoi.Instance();
                        string masp = consql.laymasp(txtbarcode.Text);
                        laythongtinvaolabel(masp);

                        //lbmahang.Text = masp;
                        loadanh(masp);
                        txtbarcode.Clear();
                        txtbarcode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                txtbarcode.Clear();
                txtbarcode.Focus();
                lbmahang.Text = "Mã hàng";
                lbmotasanpham.Text = "Mô tả sản phẩm";
                lbngayban.Text = "Ngày bán";
                lbduocbanhaychua.Text = "Chưa được bán";
                lbdatrunghaychua.Text = "";
                //phatchuaduocban.Play();
                lbtrangthai.Text = ex.ToString();
                NotificationHts("Có vấn đề Barcode rồi \nXem lại.");
            }

        }

        private void txtmatong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var consqlite = ketnoi.Instance();
                datag1.DataSource = consqlite.loctheotenmatong(txtmatong.Text);
                string mau = @"\d{1}\w{2}\d{2}[SWAC]\d{3}";
                if (Regex.IsMatch(txtmatong.Text, mau))
                {
                    laythongtinvaolabel(txtmatong.Text);
                }
                updatesoluongtrenbang();
            }
            catch (Exception ex)
            {

                lbtrangthai.Text = ex.ToString();
            }
        }
        private void pbxoamatong_Click(object sender, EventArgs e)
        {
            txtmatong.Clear();
            txtmatong.Focus();
            updatesoluongtrenbang();
        }
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                var month = sender as MonthCalendar;
                DateTime ngaychon = month.SelectionStart;
                ngaychonbandau = month.SelectionStart.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                var con = ketnoi.Instance();
                datag1.DataSource = con.laythongtinkhichonngay(ngaychonbandau);
                updatesoluongtrenbang();
                dateTimePicker1.Value = ngaychon;
                dateTimePicker2.Value = ngaychon;
            }
            catch (Exception ex)
            {

                lbtrangthai.Text = ex.ToString();
            }

        }

       
        private void btndatrunghang_Click(object sender, EventArgs e)
        {
            try
            {
                updatetrunghangthanhdatrung();
            }
            catch (Exception ex)
            {

                lbtrangthai.Text = ex.ToString();
            }

        }

        private void btnchuatrunghang_Click(object sender, EventArgs e)
        {
            try
            {

                updatetrunghangthanhchuatrung();
            }
            catch (Exception ex)
            {

                lbtrangthai.Text = ex.ToString();
            }
        }
        private void datag1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = datag1.Rows[e.RowIndex];
                lbmahang.Text = row.Cells[0].Value.ToString();
                lbmotasanpham.Text = row.Cells[1].Value.ToString() + " - " + row.Cells[2].Value.ToString() + " - " + row.Cells[3].Value.ToString();
                lbngayban.Text = row.Cells[4].Value.ToString();
                lbdatrunghaychua.Text = row.Cells[5].Value.ToString();
                if (lbdatrunghaychua.Text == null || lbdatrunghaychua.Text == "")
                {
                    lbdatrunghaychua.Text = "Chưa trưng bán";
                }

                DateTime dt1 = DateTime.ParseExact(lbngayban.Text, "dd/MM/yyyy", null);
                if (dt1 <= DateTime.Now)
                {
                    lbduocbanhaychua.Text = "Được bán";
                    phatAMTHANH_BAN();
                }
                else
                {
                    lbduocbanhaychua.Text = "Chưa được bán";
                    phatAMTHANH_KOBAN();
                }
                loadanh(lbmahang.Text);
            }
            catch (Exception ex)
            {

                lbtrangthai.Text = ex.ToString();
            }

        }
        private void btnXuatIn_Click(object sender, EventArgs e)
        {
            try
            {
                string ngaybatdau = dateTimePicker1.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                string ngayketthuc = dateTimePicker2.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                var ham = hamtao.Khoitao();
                ngaybatdau = ham.chuyendoingayvedangso(ngaybatdau);
                ngayketthuc = ham.chuyendoingayvedangso(ngayketthuc);
                var con = ketnoi.Instance();
                DataTable dt = new DataTable();
                dt = con.laythongtinkhoangngay(ngaybatdau, ngayketthuc);
                string tongsoma = con.tongmatrongkhoangngaychon(ngaybatdau, ngayketthuc);
                DialogResult dlog = MessageBox.Show("Có muốn lưu file excel không hay in luôn. \nNhấn 'YES' sẽ lưu file và 'NO' sẽ in luôn","IN LUÔN ?",MessageBoxButtons.YesNo);
                if (dlog == DialogResult.Yes)
                {
                    ham.Xuatfileexcel(dt, ngaybatdau, ngayketthuc, tongsoma);
                    ham.taovainfileexcel(con.laythongtinIn(ngaybatdau, ngayketthuc), tongsoma, ngaybatdau, ngayketthuc);

                    PopupNotifier popexcel = new PopupNotifier();
                    popexcel.TitleText = "Thông báo";
                    popexcel.ContentText = "Vừa xuất file excel \nClick vào đây để mở file";
                    popexcel.IsRightToLeft = false;
                    popexcel.Image = Properties.Resources.excel;
                    popexcel.TitleColor = System.Drawing.Color.Navy;
                    popexcel.TitleFont = new System.Drawing.Font("Comic Sans MS", 12, System.Drawing.FontStyle.Underline);
                    popexcel.BodyColor = System.Drawing.Color.DimGray;
                    popexcel.Size = new System.Drawing.Size(380, 130);
                    popexcel.ImageSize = new System.Drawing.Size(100, 100);
                    popexcel.ImagePadding = new Padding(15);
                    popexcel.ContentColor = System.Drawing.Color.White;
                    popexcel.ContentFont = new System.Drawing.Font("Comic Sans MS", 15, System.Drawing.FontStyle.Bold);
                    popexcel.Delay = 3500;
                    popexcel.BorderColor = System.Drawing.Color.DimGray;
                    popexcel.HeaderHeight = 1;
                    popexcel.Click += Popexcel_Click;
                    popexcel.Popup();
                }
                else
                {
                    ham.taovainfileexcel(con.laythongtinIn(ngaybatdau, ngayketthuc), tongsoma, ngaybatdau, ngayketthuc);

                   
                }
                //if (ham.Xuatfileexcel(dt, ngaybatdau, ngayketthuc, tongsoma))
                //{
                //    ham.taovainfileexcel(con.laythongtinIn(ngaybatdau, ngayketthuc), tongsoma,ngaybatdau,ngayketthuc);

                //    PopupNotifier popexcel = new PopupNotifier();
                //    popexcel.TitleText = "Thông báo";
                //    popexcel.ContentText = "Vừa xuất file excel \nClick vào đây để mở file";
                //    popexcel.IsRightToLeft = false;
                //    popexcel.Image = Properties.Resources.excel;
                //    popexcel.TitleColor = System.Drawing.Color.Navy;
                //    popexcel.TitleFont = new System.Drawing.Font("Comic Sans MS", 12, System.Drawing.FontStyle.Underline);
                //    popexcel.BodyColor = System.Drawing.Color.DimGray;
                //    popexcel.Size = new System.Drawing.Size(380, 130);
                //    popexcel.ImageSize = new System.Drawing.Size(100, 100);
                //    popexcel.ImagePadding = new Padding(15);
                //    popexcel.ContentColor = System.Drawing.Color.White;
                //    popexcel.ContentFont = new System.Drawing.Font("Comic Sans MS", 15, System.Drawing.FontStyle.Bold);
                //    popexcel.Delay = 3500;
                //    popexcel.BorderColor = System.Drawing.Color.DimGray;
                //    popexcel.HeaderHeight = 1;
                //    popexcel.Click += Popexcel_Click;
                //    popexcel.Popup();

                //}


            }
            catch (Exception ex)
            {

                lbtrangthai.Text = ex.ToString();
            }

        }

        private void Popexcel_Click(object sender, EventArgs e)
        {
            var ham = hamtao.Khoitao();
            ham.mofileexcelvualuu();
        }

        private void pbphatanh_Click(object sender, EventArgs e)
        {
            if (phathaykhongphat)
            {
                pbphatanh.Image = Properties.Resources.play;
                phathaykhongphat = !phathaykhongphat;
                pbanhsanpham.Image = Properties.Resources.totoro1;
                lbmahang.Text = "Totoro";
                dieukhienthread.Reset();
            }
            else
            {
                pbphatanh.Image = Properties.Resources.pause;
                phathaykhongphat = !phathaykhongphat;
                dieukhienthread.Set();
            }
        }
        void dungphatanh()
        {
            if (phathaykhongphat)
            {
                pbphatanh.Image = Properties.Resources.play;
                phathaykhongphat = !phathaykhongphat;
                dieukhienthread.Reset();
            }
        }


        
        #endregion

        private void pbAMTHANH_Click(object sender, EventArgs e)
        {
            phatAM = !phatAM;
            if (phatAM)
            {
                pbAMTHANH.Image = Properties.Resources.speaker;
            }
            else
            {
                pbAMTHANH.Image = Properties.Resources.mute;
            }
        }

        
    }
}
