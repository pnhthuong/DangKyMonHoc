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

namespace Wpf_DangKyMonHoc.WindowQL
{
	/// <summary>
	/// Interaction logic for Khoikienthuc_Window.xaml
	/// </summary>
	public partial class Khoikienthuc_Window : Window
	{
		public Khoikienthuc_Window()
		{
			InitializeComponent();
		}

		public void getLoad()
		{
			List<KhoiKienThuc> list = Xuly_KhoiKienThuc.getAllKhoiKienThuc();
			if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
			listKhoiKienThuc.ItemsSource = list;
		}
		public void clean()
		{
			txtKhoiKienThuc.IsReadOnly = false;
			txtKhoiKienThuc.Text = "";
			txtTenChuyenMon.Text = "";
			txtTenKhoiKienThuc.Text = "";
			rdoBatbuoc.IsChecked = true;
		}

		private void listKhoiKienThuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listKhoiKienThuc.SelectedItem == null)
				return;
			KhoiKienThuc kkt = listKhoiKienThuc.SelectedItem as KhoiKienThuc;
			txtKhoiKienThuc.Text = kkt.MaKhoi;
			txtTenKhoiKienThuc.Text = kkt.TenKhoi;
			txtTenChuyenMon.Text = kkt.TenChuyenMon;
			if (kkt.Batbuoc == true)
				rdoBatbuoc.IsChecked = true;
			else
				rdoKhongBatBuoc.IsChecked = true;
			txtKhoiKienThuc.IsReadOnly = true;
		}

		private void btnthem_Click(object sender, RoutedEventArgs e)
		{
			string makhoikt = txtKhoiKienThuc.Text;
			string tenkhoikt = txtTenKhoiKienThuc.Text;
			string tenchuyenmon = txtTenChuyenMon.Text;
			if (makhoikt == "" || tenchuyenmon == "" || tenkhoikt == "")
			{
				MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
				return;
			}
			KhoiKienThuc a = new KhoiKienThuc { MaKhoi = makhoikt, TenKhoi = tenkhoikt, TenChuyenMon = tenchuyenmon, Batbuoc = rdoBatbuoc.IsChecked };
			var result = Xuly_KhoiKienThuc.PostThemKhoiKienThuc(a);
			if (result == false)
			{
				MessageBox.Show("Thêm học kỳ không thành công", "Thông báo");
				return;
			}
			MessageBox.Show("Thêm học kỳ thành công", "Thông báo");
			clean();
			getLoad();

		}

		private void btnSua_Click(object sender, RoutedEventArgs e)
		{
			if (listKhoiKienThuc.SelectedItem == null)
				return;
			KhoiKienThuc kkt = listKhoiKienThuc.SelectedItem as KhoiKienThuc;
			kkt.TenKhoi = txtTenKhoiKienThuc.Text;
			kkt.TenChuyenMon = txtTenChuyenMon.Text;
			kkt.Batbuoc = rdoBatbuoc.IsChecked;
			var result = Xuly_KhoiKienThuc.PutSuaThongTinKhoiKienThuc(kkt);
			if (result == false)
			{
				MessageBox.Show("Sửa không thành công !", "Thông báo");
				return;

			}
			MessageBox.Show("Sửa thành công", "Thông báo");
			clean();
			getLoad();


		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			if (listKhoiKienThuc.SelectedItem == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					bool kq = Xuly_KhoiKienThuc.DeleteXoaKhoiKienThuc(txtKhoiKienThuc.Text);
					if (kq == false)
					{
						MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);return;
					}
					MessageBox.Show("Xóa Thành Công", "Thông Báo");
					clean();
					getLoad();
					break;


			}
		}

		private void btnLammoi_Click(object sender, RoutedEventArgs e)
		{
			clean();
		}

		private void btnThoat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void rdoKhongBatBuoc_Click(object sender, RoutedEventArgs e)
		{

		}

		private void rdoBatbuoc_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			getLoad();
		}
	}
}
