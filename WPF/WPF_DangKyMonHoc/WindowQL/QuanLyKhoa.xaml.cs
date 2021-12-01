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
    /// Interaction logic for QuanLyKhoa.xaml
    /// </summary>
    public partial class QuanLyKhoa : Window
    {
        public QuanLyKhoa()
        {
            InitializeComponent();
        }

        public void getload()
        {
            List<Khoa> list = XLKhoa.getds();
            if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
            listKhoa.ItemsSource = list;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getload();
        }
        public void clean()
        {
            txt_maKhoa.IsReadOnly = false;
            txt_maKhoa.Text = "";
            txt_tenKhoa.Text = "";
            txt_viettat.Text = "";
        }
        private void btn_them(object sender, RoutedEventArgs e)
        {
            string ma = txt_maKhoa.Text;
            string ten = txt_tenKhoa.Text;
            string vv = txt_viettat.Text;
            if (ma == "" || ten == ""|| vv=="")
            {
                MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
                return;
            }
            Khoa k = new Khoa { MaKhoa= ma, TenKhoa=ten, TenVietTat=vv };
            var result = XLKhoa.post(k);
            if (result == false)
            {
                MessageBox.Show("Thêm Khoa không thành công! Mã bị trùng hoặc quá ký tự cho phép.", "Thông báo");
                return;
            }
            MessageBox.Show("Thêm Thành Công", "Thông Báo");
            clean();
            getload();
        }

        private void btn_xoa(object sender, RoutedEventArgs e)
        {
            if (listKhoa.SelectedItem==null) { return; }
            MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    var kq = XLKhoa.delete(txt_maKhoa.Text);
                    if (kq == false)
                    {
                        MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);return;
                    }
                    MessageBox.Show("Xóa Thành Công", "Thông Báo");
                    clean();
                    getload();
                    break;
            }
        }

        private void listKhoa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listKhoa.SelectedItem == null) return;
            Khoa cv =listKhoa.SelectedItem as Khoa;
            txt_maKhoa.Text = cv.MaKhoa.Trim();
            txt_maKhoa.IsReadOnly = true;
            txt_tenKhoa.Text = cv.TenKhoa.Trim();
            txt_viettat.Text = cv.TenVietTat.Trim();
        }

        private void btn_sua(object sender, RoutedEventArgs e)
        {
            if (listKhoa.SelectedItem==null) { return; }
            Khoa cv = new Khoa();
            cv.MaKhoa = txt_maKhoa.Text;
            cv.TenKhoa = txt_tenKhoa.Text;
            cv.TenVietTat = txt_viettat.Text;
            var result = XLKhoa.put(cv);
            if (result == false)
            {
                MessageBox.Show("Chỉnh sửa không thành công! Vui lòng kiểm tra lại.", "Thông Báo");
                return;
            }
            MessageBox.Show("Chình sửa thành công!", "Thông báo");
            clean();
            getload();
        }

        private void btn_lamoi(object sender, RoutedEventArgs e)
        {

            txt_maKhoa.IsReadOnly = false;
            txt_maKhoa.Text = "";
            txt_tenKhoa.Text = "";
            txt_viettat.Text = "";
        }

        private void btn_thoat(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
