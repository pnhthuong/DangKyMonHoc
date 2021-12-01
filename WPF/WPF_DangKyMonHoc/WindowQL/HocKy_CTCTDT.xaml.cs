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
    /// Interaction logic for HocKy_CTCTDT.xaml
    /// </summary>
    public partial class HocKy_CTCTDT : Window
    {
        private string hk = null;
        public HocKy_CTCTDT()
        {
            InitializeComponent();
            getload();
            
        }
        public void getload()
        {
            List<HocKyCtdt> list = Xuly_HockyCTDT.getAllHocKyCTDT();
            if (list.Count < 1) { MessageBox.Show("Không thể tải Học kỳ Chương trình đào tạo!", "Thông báo"); return; }
            cmb_hocky.ItemsSource = list;
        }
        public string gethocky()
        {
            return hk;
        }

        private void btn_OK(object sender, RoutedEventArgs e)
        {
            if (cmb_hocky.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn học kỳ", "Thông báo");
                return;
            }   
            hk = cmb_hocky.SelectedValue.ToString();
            this.Close();
        }

        private void btn_thoat(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
