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
	/// Interaction logic for Hedaotao_Window.xaml
	/// </summary>
	public partial class Hedaotao_Window : Window
	{
		public Hedaotao_Window()
		{
			InitializeComponent();
		}

		public void getLoad()
		{
			List<HeDaoTao> list = Xuly_HDT.getAllHeDaoTao();
			if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
			listHDT.ItemsSource = list;
		}
		public void clean()
		{
			txtMadt.IsReadOnly = false;
			txtMadt.Text = "";
			txtTenDt.Text = "";
		}
		private void btn_themHDT_Click(object sender, RoutedEventArgs e)
		{
			string madt = txtMadt.Text;
			string tendt = txtTenDt.Text;
			if (madt == "" || tendt == "")
			{
				MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
				return;
			}
			HeDaoTao hdt = new HeDaoTao { MaDt = madt, TenDt = tendt };
			var result = Xuly_HDT.PostThemHeDaoTao(hdt);
			if (result == false)
			{
				MessageBox.Show("Thêm hệ đào tạo không thành công", "Thông báo");
				return;
			}
			MessageBox.Show("Thêm hệ đào tạo thành công", "Thông báo");
			clean();
			getLoad();
		}

		private void btn_suaHocKyHDT_Click(object sender, RoutedEventArgs e)
		{
			if (listHDT.SelectedItem == null)
				return;
			HeDaoTao hdt = listHDT.SelectedItem as HeDaoTao;
			hdt.TenDt = txtTenDt.Text;
			bool result = Xuly_HDT.PutSuaThongTinHeDaoTao(hdt);

			if (result == false)
			{
				MessageBox.Show("Sửa không thành công !", "Thông báo");
				return;

			}
			MessageBox.Show("Sửa thành công", "Thông báo");
			clean();
			getLoad();

		}



		private void btn_xoaHocKyHDT_Click(object sender, RoutedEventArgs e)
		{
			if (listHDT.SelectedItem == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					bool kq = Xuly_HDT.DeleteXoaHeDaoTao(txtMadt.Text);
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

		private void btn_lammoi_Click(object sender, RoutedEventArgs e)
		{
			clean();
		}

		private void btn_thoat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void listHDT_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listHDT.SelectedItem == null)
				return;
			HeDaoTao hdt = listHDT.SelectedItem as HeDaoTao;
			txtMadt.Text = hdt.MaDt;
			txtTenDt.Text = hdt.TenDt;
			txtMadt.IsReadOnly = true;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			getLoad();
		}
	}
}
