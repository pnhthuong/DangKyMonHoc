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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_DangKyMonHoc.Models;
using Wpf_DangKyMonHoc.WindowQL;
using Wpf_DangKyMonHoc.Xuly;
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc
{
	/// <summary>
	/// Interaction logic for Page_QLCongDangKy.xaml
	/// </summary>
	public partial class Page_QLCongDangKy
	{
		//private object monhocduocmo_Window;

		public void getLoad()
		{
			Xuly_Chung xlc = new Xuly_Chung();
			List<CongDangKy> listCDK = Xuly_MonHocNguyenVong.getAll();
			List<HocKyDkmh> listHKDKMH = Xuly_HockyDKMH.getAllHocKyDKMH();
			List<NamHocDkmh> listNamHocDKMH = Xuly_NamHocDKMH.getAllNamHocDkmh();
			if (listCDK == null || listHKDKMH == null || listNamHocDKMH == null)
				MessageBox.Show("Lỗi server !!!", "ERROR");
			else
			{
				listCongDangKy.ItemsSource = listCDK;
				cboHocKy.ItemsSource = listHKDKMH;
				cboNamHoc.ItemsSource = listNamHocDKMH;
				cboTrangThai.ItemsSource = xlc.getTrangThaiCong();
			}
		}
		public void getClean()
		{
			txtMaCDK.Clear();
			txtTenCDK.Clear();
			dpThoiGianDong.SelectedDate = null;
			dpThoiGianMo.SelectedDate = null;
			cboHocKy.SelectedItem = null;
			cboNamHoc.SelectedItem = null;
			cboTrangThai.SelectedItem = null;
			listCongDangKy.SelectedItem = null;
			txtMaCDK.IsReadOnly = false;
		}

		public Page_QLCongDangKy()
		{
			InitializeComponent();
			getLoad();
		}

		private void listCDK_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listCongDangKy.SelectedItem == null)
				return;
			CongDangKy a = listCongDangKy.SelectedItem as CongDangKy;
			txtMaCDK.Text = a.MaCdk;
			txtTenCDK.Text = a.TenCdk;
			dpThoiGianMo.SelectedDate = a.ThoigianBatDau;
			dpThoiGianDong.SelectedDate = a.ThoigianKetThuc;
			cboHocKy.SelectedValue = a.MaHk;
			cboNamHoc.SelectedValue = a.MaNh;
			cboTrangThai.SelectedValue = a.Trangthai;
			txtMaCDK.IsReadOnly = true;
		}

		private void btnLamMoi_Click(object sender, RoutedEventArgs e)
		{
			getClean();
		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			CongDangKy a = listCongDangKy.SelectedItem as CongDangKy;
			if (a == null)
			{
				MessageBox.Show("Vui lòng chọn đối tượng cần thao tác", "Thông báo");
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					bool kq = Xuly_MonHocNguyenVong.DeleteXoaCongDangKy(a.MaCdk);
					if (kq == false)
					{
						MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information); break;
					}
					MessageBox.Show("Xóa Thành Công", "Thông Báo");
					getClean();
					getLoad();
					break;

			}
		}

		private void btnSua_Click(object sender, RoutedEventArgs e)
		{
			if (listCongDangKy.SelectedItem == null)
				return;
			CongDangKy a = listCongDangKy.SelectedItem as CongDangKy;
			BatBuoc batbuoc = cboTrangThai.SelectedItem as BatBuoc;
			a.TenCdk = txtTenCDK.Text;
			a.ThoigianBatDau = dpThoiGianMo.SelectedDate;
			a.ThoigianKetThuc = dpThoiGianDong.SelectedDate;
			a.MaHk = cboHocKy.SelectedValue.ToString();
			a.MaNh = cboNamHoc.SelectedValue.ToString();
			a.Trangthai = batbuoc.ID;
			bool kq = Xuly_MonHocNguyenVong.PutSuaCongDangKy(a);
			if (kq == true)
			{
				MessageBox.Show("Chỉnh sửa thành công !", "Thông báo");
				getClean();
				getLoad();
			}
			else
				MessageBox.Show("Chỉnh sửa thất bại !", "Thông báo");
		}

		private void btnThem_Click(object sender, RoutedEventArgs e)
		{
			if (txtMaCDK.Text == "" || txtTenCDK.Text == "" || dpThoiGianDong.SelectedDate == null || dpThoiGianMo.SelectedDate == null || cboHocKy.SelectedItem == null || cboNamHoc.SelectedItem == null || cboTrangThai.SelectedItem == null)
			{
				MessageBox.Show("Vui lòng điền đầy đủ thông tin !", "Thông báo");
				return;
			}
			BatBuoc batbuoc = cboTrangThai.SelectedItem as BatBuoc;
			CongDangKy a = new CongDangKy();
			a.MaCdk = txtMaCDK.Text;
			a.TenCdk = txtMaCDK.Text;
			a.ThoigianBatDau = dpThoiGianMo.SelectedDate;
			a.ThoigianKetThuc = dpThoiGianDong.SelectedDate;
			a.MaHk = cboHocKy.SelectedValue.ToString();
			a.MaNh = cboNamHoc.SelectedValue.ToString();
			a.Trangthai = batbuoc.ID;
			Xuly_Chung xlc = new Xuly_Chung();
			if (xlc.checkthoigiancdk(dpThoiGianMo.SelectedDate.Value, dpThoiGianDong.SelectedDate.Value) == false)
			{
				MessageBox.Show("Thời Gian đóng mở cổng đăng ký không hợp lý", "Thông báo"); return;
			}
			bool kq = Xuly_MonHocNguyenVong.PostThemCongDangKy(a);
			if (kq == true)
			{
				MessageBox.Show("Thêm thành công !", "Thông báo");
				getClean();
				getLoad();
			}
			else
				MessageBox.Show("Thêm thất bại !", "Thông báo");
		}

		private void btn_duocmo(object sender, RoutedEventArgs e)
		{
			CongDangKy c = listCongDangKy.SelectedItem as CongDangKy;
			if (c == null)
			{
				MessageBox.Show("Bạn chưa chọn cổng đăng ký môn học!", "Thông báo");
				return;
			}
			var n = new Monhocduocmo_Window(c);
			n.Show();
		}

		private void btn_apdungnienkhoa(object sender, RoutedEventArgs e)
		{
			CongDangKy c = listCongDangKy.SelectedItem as CongDangKy;
			if (c == null)
			{
				MessageBox.Show("Bạn chưa chọn cổng đăng ký môn học!", "Thông báo");
				return;
			}
			var n = new NienKhoaApDung_Window(c);
			n.Show();
		}

		private void btn_MHNguyenVong(object sender, RoutedEventArgs e)
		{
			CongDangKy c = listCongDangKy.SelectedItem as CongDangKy;
			if (c == null)
			{
				MessageBox.Show("Bạn chưa chọn cổng đăng ký môn học!", "Thông báo");
				return;
			}
			var n = new MHNguyenVong_Window(c);
			n.Show();
		}

		private void btn_PhieuDangKy(object sender, RoutedEventArgs e)
		{
			CongDangKy c = listCongDangKy.SelectedItem as CongDangKy;
			if (c == null)
			{
				MessageBox.Show("Bạn chưa chọn cổng đăng ký môn học!", "Thông báo");
				return;
			}
			var n = new PhieuDangKy_Window(c);
			n.Show();
		}

		private void btn_Taolopmh(object sender, RoutedEventArgs e)
		{
			CongDangKy c = listCongDangKy.SelectedItem as CongDangKy;
			if (c == null)
			{
				MessageBox.Show("Bạn chưa chọn cổng đăng ký môn học!", "Thông báo");
				return;
			}
			var n = new TaoLopMH_Window(c);
			n.Show();
		}

		private void btn_hocky_DKMH(object sender, RoutedEventArgs e)
		{
			var n = new HocKyDKMH_Window();
			n.ShowDialog();
			List<HocKyDkmh> listHKDKMH = Xuly_HockyDKMH.getAllHocKyDKMH();
			cboHocKy.ItemsSource = listHKDKMH;

		}

		private void btn_NamHoc_DKMH(object sender, RoutedEventArgs e)
		{
			var n = new NamhocDKNH_Window();
			n.ShowDialog();
			List<NamHocDkmh> listNamHocDKMH = Xuly_NamHocDKMH.getAllNamHocDkmh();
			cboNamHoc.ItemsSource = listNamHocDKMH;
		}
	}
}
