using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf_DangKyMonHoc.Models;
using Wpf_DangKyMonHoc.Xuly;
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc.WindowQL
{
    /// <summary>
    /// Interaction logic for Window_CTCTDT_Chinhsua.xaml
    /// </summary>
    public partial class Window_CTCTDT_Chinhsua : Window
    {
        private List<ChiTietCtdt> listct = new List<ChiTietCtdt>();
        private List<MonHoc> listmh = Xuly_Monhoc.getAllMonHoc();
        //public void inputmonhoc()
        //{
        //    listmh.Add(new MonHoc { MaMh = "123", HesoHp = 2, Sotinchi = 2, Thuchanh = false, MaKhoi = "CS01", TenMh = "abc" });
        //    listmh.Add(new MonHoc { MaMh = "456", HesoHp = 3, Sotinchi = 3, Thuchanh = false, MaKhoi = "CS02", TenMh = "bcd" });
        //}
        //public void inputctmh()
        //{
        //    listct.Add(new ChiTietCtdt { Id=1, MaMh="123",MaCtdt="123",MaHk="123" });
        //    listct.Add(new ChiTietCtdt { Id = 1, MaMh = "123", MaCtdt = "123", MaHk = "123" });

        //}
        public Window_CTCTDT_Chinhsua(ChuongTrinhDaoTao input)
        {
            InitializeComponent();
            getloadPage(input);
            getload();
            listct = XL_CTCTDT.getdsCTCTDT(input.MaCtdt);
            list_ctctdt.ItemsSource = listct;
        }
        public void getloadPage(ChuongTrinhDaoTao input)
        {
            txt_ma.Text = input.MaCtdt;
            txt_ten.Text = input.TenCtdt;
            txt_nienkhoa.Text = input.NienKhoa;
            txt_tinchi.Text = input.TongSoTinChi.ToString();
            HeDaoTao h = Xuly_HDT.getctHDT(input.MaDt);
            Nganh n = XLNganh.getct(input.MaNganh);
            txt_hdt.Text = h.TenDt;
            txt_nganh.Text = n.TenNganh;
        }
        public void getload()
        {

            List<MonHoc> listmh = Xuly_Monhoc.getAllMonHoc();
            if (listmh.Count < 1) { MessageBox.Show("Không thể truyền dữ liệu từ Server", "Thông Báo"); return; }
            list_monhoc.ItemsSource = listmh;

            

        }
        public void getloadchitiet()
        {
            list_ctctdt.ItemsSource = null;
            if (listct.Count < 1) { MessageBox.Show("Danh sách chi tiết Chương trình đào tạo rỗng!", "Thông Báo"); return; }
            list_ctctdt.ItemsSource = listct;
        }
        private void btn_luu(object sender, RoutedEventArgs e)
        {
            if (listct.Count < 0)
            {
                MessageBox.Show("Danh sách chi tiết chương trình đào tạo rỗng! Không thể thêm dữ liệu.", "Thông báo");
                return;
            }

            bool rerult = XL_CTCTDT.putCTCTDT(listct);
            if (rerult == false)
            {
                MessageBox.Show("Không thể lưu dữ liệu! Vui lòng kiểm tra lại dữ liệu.", "Thông báo");
                return;
            }
            MessageBox.Show("Hoàn tất quá trình Chỉnh sửa chương trình đào tạo", "Thông báo");
            this.Close();
        }

        private void btn_Tim(object sender, RoutedEventArgs e)
        {
            if (txt_tkMonhoc.Text == null) { getload(); }
           List<MonHoc> listtk = listmh.FindAll(x => x.MaMh.Contains(txt_tkMonhoc.Text) || x.TenMh.Contains(txt_tkMonhoc.Text));
            
            list_monhoc.ItemsSource = null;
            list_monhoc.ItemsSource = listtk;
        }

        private void btn_them(object sender, RoutedEventArgs e)
        {
            var listselect = list_monhoc.SelectedItems;
            if (listselect.Count < 1)
            {
                MessageBox.Show("Chưa chọn môn học để thêm vào chi tiết chương trình đào tạo!", "Thông báo");
                return;
            }
            foreach (var check in listselect)
            {
                MonHoc ct = check as MonHoc;
                ChiTietCtdt ls = listct.Find(x => x.MaMh.Contains(ct.MaMh));
                if (ls != null)
                {
                    MessageBox.Show("Môn học: '" + ct.TenMh + "' đã có trong danh sách chi tiết", "Thông báo");
                    return;
                }
            }
            string mactdt = txt_ma.Text.Trim();
            var n = new HocKy_CTCTDT();
            n.ShowDialog();
            string hocky = n.gethocky();
            if (hocky == null)
            {
                MessageBox.Show("Bạn chưa chọn học kỳ cho môn học", "Thông báo");
                return;
            }
            foreach (var a in listselect)
            {
                MonHoc b = a as MonHoc;
                listct.Add(new ChiTietCtdt { MaCtdt = mactdt, MaMh = b.MaMh, MaHk = hocky.Trim() });

            }

            getloadchitiet();
            MessageBox.Show("Thành Công");
        }



        private void btn_xoa(object sender, RoutedEventArgs e)
        {
            var listselected = list_ctctdt.SelectedItems;
            if (listselected.Count < 1)
            {
                MessageBox.Show("Chưa chọn phần tử để xóa", "Thông báo");
                return;
            }
            foreach (var a in listselected)
            {
                ChiTietCtdt b = a as ChiTietCtdt;
                bool result = listct.Remove(b);

                if (result == true)
                {
                    MessageBox.Show("Đã xóa");
                }
            }
            getloadchitiet();
            MessageBox.Show("Xóa thành công");

        }
    }
}

