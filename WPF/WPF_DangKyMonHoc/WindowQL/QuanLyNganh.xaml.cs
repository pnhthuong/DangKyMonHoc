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
    public partial class QuanLyNganh : Window
    {
        public QuanLyNganh()
        {
            InitializeComponent();
        }

        public void getload()
        {
            List<Nganh> list = XLNganh.getds();
            if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
            listNganh.ItemsSource = list;
            List<Khoa> listcmb = XLKhoa.getds();
            if (listcmb == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
            cmb_Khoa.ItemsSource = listcmb;
        }
        
        public void clean()
        {
            txt_maNganh.IsReadOnly = false;
            txt_maNganh.Text = "";
            txt_tenNganh.Text = "";
            cmb_Khoa.SelectedItem = null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getload();
           
        }

        private void btn_them(object sender, RoutedEventArgs e)
        {
            string ma = txt_maNganh.Text;
            string ten = txt_tenNganh.Text;
            Khoa khoa = cmb_Khoa.SelectedItem as Khoa ;
            if (ma == "" || ten == ""||cmb_Khoa.SelectedItem==null)
            {
                MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
                return;
            }
            Nganh k = new Nganh { MaNganh = ma, TenNganh = ten, MaKhoa = khoa.MaKhoa };
            var result = XLNganh.post(k);
            if (result == false)
            {
                MessageBox.Show("Thêm Ngành không thành công", "Thông báo");
                return;
            }
            MessageBox.Show("Thêm Thành Công", "Thông Báo");
            clean();
            getload();

        }

        private void listNganh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listNganh.SelectedItem == null) return;
            Nganh cv = listNganh.SelectedItem as Nganh;
            txt_maNganh.Text = cv.MaNganh;
            txt_maNganh.IsReadOnly = true;
            txt_tenNganh.Text = cv.TenNganh;
            cmb_Khoa.SelectedValue=cv.MaKhoa;
        }

        private void btn_sua(object sender, RoutedEventArgs e)
        {
            if (listNganh.SelectedItem==null) { return; }
            Nganh n = new Nganh
            {
                MaKhoa = cmb_Khoa.SelectedValue.ToString(),
                TenNganh = txt_tenNganh.Text,
                MaNganh = txt_maNganh.Text
            };

            var result = XLNganh.put(n);
            if (result == false)
            {
                MessageBox.Show("Chỉnh sửa không thành công! Vui lòng kiểm tra lại.", "Thông Báo");
                return;
            }
            MessageBox.Show("Chình sửa thành công!", "Thông báo");
            clean();
            getload();
        }

        private void btn_xoa(object sender, RoutedEventArgs e)
        {
            if (listNganh.SelectedItem==null) { return; }
            var a = listNganh.SelectedItem as Nganh;
            MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    var kq = XLNganh.delete(a.MaNganh);
                    if (kq == false)
                    {
                        MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    MessageBox.Show("Xóa Thành Công", "Thông Báo");
                    clean();
                    getload();
                    break;
            }
        }

        private void btn_lammoi(object sender, RoutedEventArgs e)
        {
            List<Khoa> listcmb = XLKhoa.getds();
            if (listcmb == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
            cmb_Khoa.ItemsSource = listcmb;
            txt_maNganh.Text = "";
            txt_tenNganh.Text = "";
            listNganh.SelectedItem = null;
            clean();
        }

        private void btn_thoat(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Khoa(object sender, RoutedEventArgs e)
        {
            var n = new QuanLyKhoa();
            n.ShowDialog();

        }
    }
}
