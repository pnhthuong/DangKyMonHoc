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
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc.WindowQL
{
    /// <summary>
    /// Interaction logic for CT_MonHocDuocMo_Window.xaml
    /// </summary>
    public partial class CT_MonHocDuocMo_Window : Window
    {
        public int soluong ;
        public bool trangthai;
        public CT_MonHocDuocMo_Window()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Xuly_Chung xlc = new Xuly_Chung();
            xlc.textNumber(e);
        }

        private void btn_OK(object sender, RoutedEventArgs e)
        {
            if(txt_soluong.Text==""||int.Parse(txt_soluong.Text)<0)
            {
                MessageBox.Show("Vui lòng nhập chính xác số lượng", "Thông báo");
                return;
            }

            soluong = int.Parse(txt_soluong.Text);
            trangthai = (btn_trangthai.IsChecked == true) ? true : false;
            this.Close();
        }

        private void btn_thoat(object sender, RoutedEventArgs e)
        {
            soluong = 0;
            this.Close();
        }
    }
}
