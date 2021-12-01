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
using Wpf_DangKyMonHoc.Page;
using Wpf_DangKyMonHoc.PageQL;
using Wpf_DangKyMonHoc.WindowQL;

namespace Wpf_DangKyMonHoc
{
	/// <summary>
	/// Interaction logic for AfterLoginNV.xaml
	/// </summary>
	public partial class AfterLoginNV : Window
	{
		private string nv = null;
		public AfterLoginNV(string manv)
		{
			InitializeComponent();
			nv = manv;
		}

		private void btnDoimatkhau_Click(object sender, RoutedEventArgs e)
		{
			var n = new MatKhau_Window(nv);
			n.ShowDialog();
		}

		private void btnDangXuat_Click(object sender, RoutedEventArgs e)
		{
			var n = new DangNhapWindow();
			this.Close();
			n.ShowDialog();
		}

		private void btnQuanLySinhVien_Click(object sender, RoutedEventArgs e)
		{
			ContentArea.Content = new QuanLySinhVien();
		}

		private void btnQlGiangVien_Click(object sender, RoutedEventArgs e)
		{
			ContentArea.Content = new QuanLyGiangVien();
		}

		private void btnQlLop_Click(object sender, RoutedEventArgs e)
		{
			ContentArea.Content = new QuanLyLop();
		
		}

		private void btnQlMonHoc_Click(object sender, RoutedEventArgs e)
		{
			ContentArea.Content = new Page_QLMonhoc();
		}

		private void btnQlCongDangKy_Click(object sender, RoutedEventArgs e)
		{
			ContentArea.Content = new Page_QLCongDangKy();
		}

		private void btnCTDT_Click(object sender, RoutedEventArgs e)
		{
			ContentArea.Content = new Page_QLChuongTrinhDaoTao();
		}

		private void btnChucVu_Click(object sender, RoutedEventArgs e)
		{
			var n = new QuanLyChucVu();
			n.ShowDialog();
		}

		private void btnKhoa_Click(object sender, RoutedEventArgs e)
		{
			var n = new QuanLyKhoa();
			n.ShowDialog();
		}

		private void btnNganh_Click(object sender, RoutedEventArgs e)
		{
			var n = new QuanLyNganh();
			n.ShowDialog();
		}

		private void btnNienKhoa_Click(object sender, RoutedEventArgs e)
		{
			var n = new QuanLyNienKhoa();
			n.ShowDialog();
		}

		private void btnNamHoc_Click(object sender, RoutedEventArgs e)
		{
			var n = new NamhocDKNH_Window();
			n.ShowDialog();
		}

		private void btnHocKyDKMH_Click(object sender, RoutedEventArgs e)
		{
			var n = new HocKyDKMH_Window();
			n.ShowDialog();
		}

		

		private void btnKhoiKienThuc_Click(object sender, RoutedEventArgs e)
		{
			var n = new Khoikienthuc_Window();
			n.ShowDialog();
		}

		private void btnHeDaoTao_Click(object sender, RoutedEventArgs e)
		{
			var n = new Hedaotao_Window();
			n.ShowDialog();
		}

		private void btnHocKyCTDT_Click(object sender, RoutedEventArgs e)
		{
			var n = new HocKyCTDT_Window();
			n.ShowDialog();
		}

		private void btnLopMh_Click(object sender, RoutedEventArgs e)
		{
			ContentArea.Content = new PageQLLop_MonHoc();
		}
	}
}
