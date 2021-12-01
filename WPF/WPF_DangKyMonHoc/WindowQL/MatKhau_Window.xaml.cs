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

namespace Wpf_DangKyMonHoc.WindowQL
{
    /// <summary>
    /// Interaction logic for MatKhau_Window.xaml
    /// </summary>
    public partial class MatKhau_Window : Window
    {
        private string ma = null;
        public MatKhau_Window(string manv)
        {
            InitializeComponent();
            ma = manv;
        }

        private void btn_Thoat(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Luu(object sender, RoutedEventArgs e)
        {
            if( txt_mkmoi.Password.Length == 0|| txt_mknl.Password.Length == 0)
            {
                MessageBox.Show("Điền đầy đủ thông tin mật khẩu", "Thông báo");
                return;
            }
            NhanVien nv = XL_Login.GetNhanVien(ma);
            if(txt_mkmoi.Password.Equals(txt_mknl.Password)==false)
            {
                MessageBox.Show("Xác nhận mật khẩu mới không trùng khớp", "Thông báo");
                return;
            }
            nv.Matkhau = txt_mknl.Password;
            bool a = XL_Login.putloginnv(nv);
            if (a == false)
            {
                MessageBox.Show("Thay đổi mật khẩu không thành công", "Thông báo");
                return;
            }
            
            MessageBox.Show("Thành Công");
            this.Close();
        }
    }
}
