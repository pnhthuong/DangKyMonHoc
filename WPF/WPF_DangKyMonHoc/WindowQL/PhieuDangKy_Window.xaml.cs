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
    /// Interaction logic for PhieuDangKy_Window.xaml
    /// </summary>
    public partial class PhieuDangKy_Window : Window
    {
        private List<PhieuDangKy_Custom> list = new List<PhieuDangKy_Custom>();

        public PhieuDangKy_Window(CongDangKy cdk)
        {
            InitializeComponent();
           
            hienthiTTCongDangKy(cdk);
            getload(cdk.MaCdk);
        }
        public void hienthiTTCongDangKy(CongDangKy a)
        {
            Xuly_Chung xlc = new Xuly_Chung();
            List<HocKyDkmh> listHKDKMH = Xuly_HockyDKMH.getAllHocKyDKMH();
            List<NamHocDkmh> listNamHocDKMH = Xuly_NamHocDKMH.getAllNamHocDkmh();
            if (listHKDKMH == null || listNamHocDKMH == null)
                MessageBox.Show("Lỗi server !!!", "ERROR");
            else
            {
                cboHocKy.ItemsSource = listHKDKMH;
                cboNamHoc.ItemsSource = listNamHocDKMH;
                cboTrangThai.ItemsSource = xlc.getTrangThaiCong();
            }
            txtMaCDK.Text = a.MaCdk;
            txtTenCDK.Text = a.TenCdk;
            dpThoiGianMo.SelectedDate = a.ThoigianBatDau;

            dpThoiGianDong.SelectedDate = a.ThoigianKetThuc;
            cboHocKy.SelectedValue = a.MaHk;
            cboNamHoc.SelectedValue = a.MaNh;
            cboTrangThai.SelectedValue = a.Trangthai;
            txtMaCDK.IsReadOnly = true;
            txtTenCDK.IsReadOnly = true;
        }
        public void getload(string macdk)
        {
             list = XL_PhieuDangKy.getdspdk(macdk);

            if (list == null)
            {
                MessageBox.Show("Không tìm thấy tất cả các phiếu đăng ký", "Thông Báo");return;
            }
            listPhieuDangky.ItemsSource = list;
        }
        private void btn_Tim(object sender, RoutedEventArgs e)
        {
            if (txt_tkMaSV.Text == "")
            {
                getload(txtMaCDK.Text);
                return;
            }
            List<PhieuDangKy_Custom> listtk = list.FindAll(x => x.MaSv.Contains(txt_tkMaSV.Text));

            listPhieuDangky.ItemsSource = null;
            listPhieuDangky.ItemsSource = listtk;
        }

		private void btnChiTiet_Click(object sender, RoutedEventArgs e)
		{
            if (listPhieuDangky.SelectedItem == null)
			{
                MessageBox.Show("Chưa chọn Phiếu đăng ký môn học", "Thông báo");
                return;

            }
            PhieuDangKy_Custom a = listPhieuDangky.SelectedItem as PhieuDangKy_Custom;
			var pdk = new XemChiTietPDK_Window(a);
			pdk.Show();
		}
	}
}
