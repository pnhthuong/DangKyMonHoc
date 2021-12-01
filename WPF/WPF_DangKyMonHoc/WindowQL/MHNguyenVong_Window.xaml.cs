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
    /// Interaction logic for MHNguyenVong_Window.xaml
    /// </summary>
    public partial class MHNguyenVong_Window : Window
    {
        public MHNguyenVong_Window(CongDangKy cdk)
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
			List<MonHocNguyenVong> list = XL_MonHocNguyenVong.getdsmhnv(macdk);
            if (list == null)
            {
				MessageBox.Show("Không có môn học nguyên vọng", "Thông báo");return;
            }
			list_monhocnguyenvong.ItemsSource = list;
        }
		private void btn_Pheduyet(object sender, RoutedEventArgs e)
        {
			var select = list_monhocnguyenvong.SelectedItems;
			List<MonHocNguyenVong> lis = new List<MonHocNguyenVong>();
			foreach(var a in select)
            {
				MonHocNguyenVong b = a as MonHocNguyenVong;
				lis.Add(b);
            }
			

			bool result = XL_MonHocNguyenVong.PostDSMonHocNguyenVong(lis);
            if (result == false)
            {
				MessageBox.Show("Phê Duyệt Không Thành Công", "Thông Báo"); return;
            }

			getload(txtMaCDK.Text);
        }

        private void btn_thoat(object sender, RoutedEventArgs e)
        {
			this.Close();
        }
    }
}
