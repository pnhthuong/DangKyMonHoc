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
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc
{
	/// <summary>
	/// Interaction logic for DangNhapWindow.xaml
	/// </summary>
	public partial class DangNhapWindow : Window
	{
		public string manv = null;
		public DangNhapWindow()
		{
			InitializeComponent();
		}

		private void btnDangNhap_Click(object sender, RoutedEventArgs e)
		{
            if (txt_tendangnhap.Text == "" || txt_Password.Password.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ Tên đăng nhập và Mật khẩu để truy cập hệ thống", "Thông báo");
                return;
            }
            NhanVien nv = XL_Login.postloginnv(new NhanVien { MaNv = txt_tendangnhap.Text, Matkhau = txt_Password.Password.Trim() });
			if (nv == null)
			{
				MessageBox.Show("Đăng nhập không thành công, vui lòng kiểm tra lại thông tin !", "Thông báo");
				return;
			}
			else
			{
				if (nv.MaChucVu.Trim() =="TP")
				{
					manv = txt_tendangnhap.Text;
					FixAfterLogin b = new FixAfterLogin(manv);

					b.Show();
					this.Close();
				}
				else
				{
					manv = txt_tendangnhap.Text;
					AfterLoginNV c = new AfterLoginNV(manv);

					c.Show();
					this.Close();
				}
			}
			
		}

		private void btnThoat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
