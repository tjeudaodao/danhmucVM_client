using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Drawing.Imaging;
using Microsoft.Office.Interop;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace danhmucVM_client
{
    class hamtao
    {
        #region khoitao class
        public hamtao()
        {

        }
        private static hamtao _khoitao = null;
        public static hamtao Khoitao()
        {
            if (_khoitao == null)
            {
                _khoitao = new hamtao();
            }
            return _khoitao;
        }
        #endregion
        #region danhmuc
        static List<laythongtin> luuthongtin = new List<laythongtin>();
        static string duongdanluufileexcel = null;

        // ham chuyen doi dinh dang ngay tu string sang dang so co the + -
        public string chuyendoingayvedangso(string ngaydangDDMMYYYY)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(ngaydangDDMMYYYY, "dd/MM/yyyy", null);
                return dt.ToString("yyyyMMdd");
            }
            catch (Exception)
            {

                return "Loi";
            }

        }

        
        
        public bool Xuatfileexcel(DataTable dt, string ngaybatdau, string ngayketthuc, string tongma)
        {
            bool bl = true;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                Random rd = new Random();
                int songaunhien = rd.Next(1, 100);
                saveDialog.Filter = "Excel (.xlsx)|*.xlsx";
                saveDialog.FileName = "Thống kê hàng từ ngày - " + ngaybatdau + " đến ngày - " + ngayketthuc + " -vs" + songaunhien.ToString();
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    duongdanluufileexcel = exportFilePath;
                    var newFile = new FileInfo(exportFilePath);
                    using (var package = new ExcelPackage(newFile))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("hts");
                        worksheet.Cells["A1"].Value = "Tổng số mã: " + tongma;
                        worksheet.Cells["A3"].LoadFromDataTable(dt, true);
                        worksheet.Column(1).AutoFit();
                        worksheet.Column(2).AutoFit();
                        worksheet.Column(3).AutoFit();
                        worksheet.Column(4).AutoFit();
                        worksheet.Column(5).AutoFit();

                        worksheet.Column(6).AutoFit();
                        package.Save();
                        package.Dispose();
                    }
                    bl = true;
                }
                else
                {
                    bl = false;
                }
            }
            return bl;
        }
        public void mofileexcelvualuu()
        {
            if (duongdanluufileexcel != null)
            {
                var app = new excel.Application();

                excel.Workbooks book = app.Workbooks;
                excel.Workbook sh = book.Open(duongdanluufileexcel);
                app.Visible = true;
                //sh.PrintOutEx();
            }

        }
        public void taovainfileexcel(DataTable dt, string tongma,string ngaybatdau,string ngayketthuc)
        {
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet worksheet = ExcelPkg.Workbook.Worksheets.Add("hts");

            worksheet.Cells["A1:C1"].Merge = true;
            worksheet.Cells["A2:C2"].Merge = true;
            worksheet.Cells["A1"].Value = "Danh mục VM";
            worksheet.Cells["A2"].Value = "Từ ngày: " + ngaybatdau + " - " + ngayketthuc;

            worksheet.Cells["A3"].Value = "Tổng mã:";
            worksheet.Cells["B3"].Value = tongma;
            worksheet.Cells["A5"].LoadFromDataTable(dt, true,OfficeOpenXml.Table.TableStyles.Light1);

            worksheet.Column(1).Width = 10;
            worksheet.Column(2).Width = 13;
            worksheet.Column(3).Width = 10;


            //worksheet.Cells[worksheet.Dimension.End.Row + 1, 1].Value = "Tổng sản phẩm:";
            //worksheet.Cells[worksheet.Dimension.End.Row, 2].Value = tongsp;

            var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
            var cellFont = allCells.Style.Font;
            cellFont.SetFromFont(new Font("Calibri", 10));

            worksheet.PrinterSettings.LeftMargin = 0.2M / 2.54M;
            worksheet.PrinterSettings.RightMargin = 0.2M / 2.54M;
            worksheet.PrinterSettings.TopMargin = 0.2M / 2.54M;
            worksheet.Protection.IsProtected = false;
            worksheet.Protection.AllowSelectLockedCells = false;
            if (File.Exists("hts.xlsx"))
            {
                File.Delete("hts.xlsx");

            }
            ExcelPkg.SaveAs(new FileInfo("hts.xlsx"));
            ExcelPkg.Dispose();

            var app = new excel.Application();

            excel.Workbooks book = app.Workbooks;
            excel.Workbook sh = book.Open(Path.GetFullPath("hts.xlsx"));
            //app.Visible = true;
            sh.PrintOutEx();
            app.Quit();
            Marshal.FinalReleaseComObject(app);
            Marshal.FinalReleaseComObject(book);
        }
        #endregion
    }
}
