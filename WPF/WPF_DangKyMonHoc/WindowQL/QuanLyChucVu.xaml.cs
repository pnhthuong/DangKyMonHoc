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
    /// Interaction logic for QuanLyChucVu.xaml
    /// </summary>
    public partial class QuanLyChucVu : Window
    {
        public QuanLyChucVu()
        {
            InitializeComponent();
        }
        public void getload()
        {
            List<ChucVu> list = XLChucVu.dschucvu();
            if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
            listChucVu.ItemsSource = list;
        }
        public void clean()
        {
            txt_macv.IsReadOnly = false;
            txt_macv.Text = "";
            txt_tencv.Text = "";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getload();
        }

        private void btn_themChucVu(object sender, RoutedEventArgs e)
        {
            string ma = txt_macv.Text;
            string ten = txt_tencv.Text;
            if(ma==""||ten=="")
            {
                MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo");
                return;
            }
            ChucVu cv = new ChucVu { MaChucVu = ma, TenChucVu = ten };
            var result = XLChucVu.postChucVu(cv);
            if(result==false)
            {
                MessageBox.Show("Thêm Chức Vụ không thành công", "Thông báo");
                return;
            }
            MessageBox.Show("Thêm Thành Công", "Thông Báo");
            clean();
            getload();
            
        }

        private void listChucVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listChucVu.SelectedItem == null) return;
            ChucVu cv = listChucVu.SelectedItem as ChucVu;
            txt_macv.Text = cv.MaChucVu;
            txt_macv.IsReadOnly = true;
            txt_tencv.Text = cv.TenChucVu;
        }

        private void btn_suaChucVu(object sender, RoutedEventArgs e)
        {
            if (listChucVu.SelectedItem==null) { return; }
            ChucVu cv = new ChucVu();
            cv.MaChucVu = txt_macv.Text;
            cv.TenChucVu = txt_tencv.Text;

            var result = XLChucVu.putChucVu(cv);
            if (result == false)
            {
                MessageBox.Show("Chỉnh sửa không thành công! Vui lòng kiểm tra lại.", "Thông Báo");
                return;
            }
            MessageBox.Show("Chình sửa thành công!", "Thông báo");
            clean();
            getload();
        }

        private void btn_xoaChucVu(object sender, RoutedEventArgs e)
        {
            if (listChucVu.SelectedItem ==null) { return; }
            MessageBoxResult result=MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    var kq = XLChucVu.deleteChucVu(txt_macv.Text);
                    if(kq==false)
                    {
                        MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo",MessageBoxButton.OK, MessageBoxImage.Information); return;
                    }
                    MessageBox.Show("Xóa Thành Công", "Thông Báo");
                    clean();
                    getload();
                    break;
            }
        }

        private void btn_lammoi(object sender, RoutedEventArgs e)
        {
            clean();
        }

        private void btn_thoat(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
