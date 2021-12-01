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
	/// Interaction logic for HocKyCTDT_Window.xaml
	/// </summary>
	public partial class HocKyCTDT_Window : Window
	{
		public HocKyCTDT_Window()
		{
			InitializeComponent();
		}

		public void getLoad()
		{

			List<HocKyCtdt> list = Xuly_HockyCTDT.getAllHocKyCTDT();
			if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
			listHocKyCTDT.ItemsSource = list;
		}
		public void clean()
		{
			txtMahk.IsReadOnly = false;
			txtTenhk.IsReadOnly = false;
			txtMahk.Text = "";
			txtTenhk.Text = "";
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			getLoad();
		}

		private void btnThem_Click(object sender, RoutedEventArgs e)
		{
			string mahk = txtMahk.Text;
			string tenhk = txtTenhk.Text;
			if (mahk == "" || tenhk == "")
			{
				MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
				return;
			}
			HocKyCtdt hocky = new HocKyCtdt { MaHk = mahk, TenHk = tenhk };
			var result = Xuly_HockyCTDT.PostThemHockyCTDT(hocky);
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
			if (listHocKyCTDT.SelectedItem == null)
				return;
			HocKyCtdt hk = listHocKyCTDT.SelectedItem as HocKyCtdt;
			hk.TenHk = txtTenhk.Text;
			bool result = Xuly_HockyCTDT.PutSuaThongTinHocKyCTDT(hk);

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
			if (listHocKyCTDT.SelectedItem == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					bool kq = Xuly_HockyCTDT.DeleteXoaHockyCTDT(txtMahk.Text);
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

		private void btnLamMoi_Click(object sender, RoutedEventArgs e)
		{
			clean();
		}

		private void btnThoat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void listHocKyCTDT_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listHocKyCTDT.SelectedItem == null) return;
			HocKyCtdt hk = listHocKyCTDT.SelectedItem as HocKyCtdt;
			txtMahk.Text = hk.MaHk;
			txtMahk.IsReadOnly = true;
			txtTenhk.Text = hk.TenHk;
		}
	}
}
