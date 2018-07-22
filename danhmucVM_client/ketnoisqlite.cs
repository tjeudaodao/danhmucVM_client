using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;

namespace danhmucVM_client
{
    class ketnoisqlite
    {
        #region khoitao
        public static SQLiteConnection connec = null;
        public ketnoisqlite()
        {
            string chuoiketnoi = "Data Source=dbdangnhap.db;version=3;new=false";
            connec = new SQLiteConnection(chuoiketnoi);
        }

        private static ketnoisqlite _khoitao = null;
        public static ketnoisqlite khoitao()
        {
            if (_khoitao == null)
            {
                _khoitao = new ketnoisqlite();
            }
            return _khoitao;
        }

        public void Open()
        {
            if (connec.State != ConnectionState.Open)
            {
                connec.Open();
            }
        }
        public void Close()
        {
            if (connec.State != ConnectionState.Closed)
            {
                connec.Close();
            }
        }
        #endregion

        #region xu ly
        public string laytentaikhoan()
        {
            string h = null;
            string sql = "select taikhoan from dangnhap";
            Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, connec);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                h = dtr[0].ToString();
            }
            Close();
            return h;
        }
        public string[] laytaikhoan()
        {
            string[] h = new string[2];
            string sql = "select taikhoan,matkhau from dangnhap";
            Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, connec);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                    h[0] = dtr[0].ToString();
                    h[1] = dtr[1].ToString();
             
            }
            Close();
            return h;
        }
        public void updatetaikhoan(string taikhoan,string matkhau)
        {
            string sql = "update dangnhap set taikhoan = '" + taikhoan + "' , matkhau = '" + matkhau + "'";
            Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, connec);
            cmd.ExecuteNonQuery();
            Close();
        }
        public string laygiatri_ghinho()
        {
            string h = null;
            string sql = "select ghinho from dangnhap";
            Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, connec);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            if (dtr.Read())
            {
                h = dtr[0].ToString();
            }
            return h;
        }
        public void update_ghinho(string thaydoi)
        {
            string sql = "update dangnhap set ghinho = '" + thaydoi + "'";
            Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, connec);
            cmd.ExecuteNonQuery();
            Close();
        }
        #endregion
    }
}
