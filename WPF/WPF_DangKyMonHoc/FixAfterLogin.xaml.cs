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
	/// Interaction logic for FixAfterLogin.xaml
	/// </summary>
	public partial class FixAfterLogin : Window
	{
        private string nv = null;
		public FixAfterLogin(string manv)
		{
			InitializeComponent();
            nv = manv;
		}

        private void btnQuanLySinhVien_Click(object sender, RoutedEventArgs e)
        {
			ContentArea.Content = new QuanLySinhVien();
        }

        private void btnQuanLyNhanVien(object sender, RoutedEventArgs e)
        {
			ContentArea.Content = new QuanLyNhanVien();
		}

        private void btnGiangVien(object sender, RoutedEventArgs e)
        {
			ContentArea.Content = new QuanLyGiangVien();
		}

        private void btnquanlyLop(object sender, RoutedEventArgs e)
        {
			ContentArea.Content = new QuanLyLop();
		}

        private void btnquanlyMonHoc(object sender, RoutedEventArgs e)
        {
			ContentArea.Content = new Page_QLMonhoc();
		}

        private void btnCongDangKy(object sender, RoutedEventArgs e)
        {
			ContentArea.Content = new Page_QLCongDangKy();
		}

        private void btnCTDT(object sender, RoutedEventArgs e)
        {
			ContentArea.Content = new Page_QLChuongTrinhDaoTao();
		}

        private void btnchucvu(object sender, RoutedEventArgs e)
        {
			var n = new QuanLyChucVu();
			n.ShowDialog();
        }

        private void btnKhoa(object sender, RoutedEventArgs e)
        {
			var n = new QuanLyKhoa();
			n.ShowDialog();
        }

        private void btnNganh(object sender, RoutedEventArgs e)
        {
			var n = new QuanLyNganh();
			n.ShowDialog();
        }

        private void btnNienKhoa(object sender, RoutedEventArgs e)
        {
            var n = new QuanLyNienKhoa();
            n.ShowDialog();
        }

        private void btnNamHocDKMH(object sender, RoutedEventArgs e)
        {
            var n = new Wpf_DangKyMonHoc.WindowQL.NamhocDKNH_Window();
            n.ShowDialog();
        }

        

        private void btnHocKyDKMH(object sender, RoutedEventArgs e)
        {
            var n = new HocKyDKMH_Window();
            n.ShowDialog();
        }

        private void btnKhoiKT(object sender, RoutedEventArgs e)
        {
            var n = new Khoikienthuc_Window();
            n.ShowDialog();
        }

        private void btnHeDaoTao(object sender, RoutedEventArgs e)
        {
            var n = new Hedaotao_Window();
            n.ShowDialog();
        }

        private void btnhockyctdt(object sender, RoutedEventArgs e)
        {
            var n = new HocKyCTDT_Window();
            n.ShowDialog();
        }

        private void btn_exit(object sender, RoutedEventArgs e)
        {
            var n = new DangNhapWindow(); this.Close(); n.ShowDialog();
            
        }

        private void btn_doimatkhau(object sender, RoutedEventArgs e)
        {
            var n = new MatKhau_Window(nv);
            n.ShowDialog();
        }

		private void btnLopMonHoc_Click(object sender, RoutedEventArgs e)
		{
            ContentArea.Content = new PageQLLop_MonHoc();
		}
	}
}
