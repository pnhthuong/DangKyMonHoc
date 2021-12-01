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
	/// Interaction logic for NamhocDKNH_Window.xaml
	/// </summary>
	public partial class NamhocDKNH_Window : Window
	{
        public NamhocDKNH_Window()
        {
			InitializeComponent();
			getLoad();
		}
		public void getLoad()
		{
			List<NamHocDkmh> list = Xuly_NamHocDKMH.getAllNamHocDkmh();

			if (list == null)
				MessageBox.Show("Lỗi tải Server!!!", "ERROR");
			listNamHocDKMH.ItemsSource = list;
		}
		public void clean()
		{
			txtMaNh.Text = "";
			txtTenNh.Text = "";
			txtMaNh.IsReadOnly = false;
		}
		private void btn_themNamhocDKMH_Click(object sender, RoutedEventArgs e)
		{
			string manh = txtMaNh.Text;
			string tennh = txtTenNh.Text;
			if (manh == "" || tennh == "")
			{
				MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
				return;
			}
			NamHocDkmh namhoc = new NamHocDkmh { MaNh = manh, TenNh = tennh };
			var result = Xuly_NamHocDKMH.PostThemNamHocDkmh(namhoc);
			if (result == false)
			{
				MessageBox.Show("Thêm năm học không thành công", "Thông báo");
				return;
			}
			MessageBox.Show("Thêm năm học thành công", "Thông báo");
			clean();
			getLoad();
		}

		private void btn_suaNamhocDKMH_Click(object sender, RoutedEventArgs e)
		{
			if (listNamHocDKMH.SelectedItem == null)
				return;
			NamHocDkmh namhoc = listNamHocDKMH.SelectedItem as NamHocDkmh;
			namhoc.TenNh = txtTenNh.Text;
			bool result = Xuly_NamHocDKMH.PutSuaThongTinNamHocDkmh(namhoc);

			if (result == false)
			{
				MessageBox.Show("Sửa không thành công !", "Thông báo");
				return;

			}
			MessageBox.Show("Sửa thành công", "Thông báo");
			clean();
			getLoad();
		}

		private void btn_xoaNamhocDKMH_Click(object sender, RoutedEventArgs e)
		{
			if (listNamHocDKMH.SelectedItem == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					bool kq = Xuly_NamHocDKMH.DeleteXoaNamHocDkmh(txtMaNh.Text);
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

		private void listNamHocDKMH_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listNamHocDKMH.SelectedItem == null)
				return;
			NamHocDkmh namhoc = listNamHocDKMH.SelectedItem as NamHocDkmh;
			txtMaNh.Text = namhoc.MaNh;
			txtTenNh.Text = namhoc.TenNh;
			txtMaNh.IsReadOnly = true;
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
