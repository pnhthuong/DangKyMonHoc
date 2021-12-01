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
	/// Interaction logic for HocKyDKMH_Window.xaml
	/// </summary>
	public partial class HocKyDKMH_Window : Window
	{
		public HocKyDKMH_Window()
		{
			InitializeComponent();
		}

		public void getLoad()
		{
			List<HocKyDkmh> list = Xuly_HockyDKMH.getAllHocKyDKMH();
			if (list == null)
				MessageBox.Show("Lỗi tải Server!!!", "ERROR");
			listHocKyDKMH.ItemsSource = list;
		}
		public void clean()
		{
			txtMaHK.IsReadOnly = false;
			txtMaHK.Text = "";
			txtTenHK.Text = "";
		}

		private void listHocKyDKMH_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listHocKyDKMH.SelectedItem == null)
				return;
			HocKyDkmh hocky = listHocKyDKMH.SelectedItem as HocKyDkmh;
			txtMaHK.Text = hocky.MaHk;
			txtTenHK.Text = hocky.TenHk;
			txtMaHK.IsReadOnly = true;

		}

		private void btn_themHocKyDKMH_Click(object sender, RoutedEventArgs e)
		{
			string mahk = txtMaHK.Text;
			string tenhk = txtTenHK.Text;
			if (mahk == "" || tenhk == "")
			{
				MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
				return;
			}
			HocKyDkmh hocky = new HocKyDkmh { MaHk = mahk, TenHk = tenhk };
			var result = Xuly_HockyDKMH.PostThemHockyDKMH(hocky);
			if (result == false)
			{
				MessageBox.Show("Thêm học kỳ không thành công", "Thông báo");
				return;
			}
			MessageBox.Show("Thêm học kỳ thành công", "Thông báo");
			clean();
			getLoad();
		}

		private void btn_suaHocKyDKMH_Click(object sender, RoutedEventArgs e)
		{
			if (listHocKyDKMH.SelectedItem == null)
				return;
			HocKyDkmh hk = listHocKyDKMH.SelectedItem as HocKyDkmh;
			hk.TenHk = txtTenHK.Text;
			bool result = Xuly_HockyDKMH.PutSuaThongTinHocKyDKMH(hk);

			if (result == false)
			{
				MessageBox.Show("Sửa không thành công !", "Thông báo");
				return;

			}
			MessageBox.Show("Sửa thành công", "Thông báo");
			clean();
			getLoad();
		}

		private void btn_xoaHocKyDKMH_Click(object sender, RoutedEventArgs e)
		{
			if (listHocKyDKMH.SelectedItem == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					bool kq = Xuly_HockyDKMH.DeleteXoaHockyDKMH(txtMaHK.Text);
					if (kq == false)
					{
						MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
						return;
					}
					MessageBox.Show("Xóa Thành Công", "Thông Báo");
					clean();
					getLoad();
					break;
			}
		}

		private void btn_lammoi_Click(object sender, RoutedEventArgs e)
		{
			clean();
		}

		private void btn_thoat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			getLoad();
		}
	}
}
