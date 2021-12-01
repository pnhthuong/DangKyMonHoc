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

namespace Wpf_DangKyMonHoc.WindowQL
{
    /// <summary>
    /// Interaction logic for testChiTietSinhVien.xaml
    /// </summary>
    public partial class testChiTietSinhVien : Window
    {
        public SinhVien sinhvien = new SinhVien();
        public testChiTietSinhVien(SinhVien sv) 
        {
            InitializeComponent();
            sinhvien = sv;
            getload();
        }
        public void getload()
        {
            txt.Text = sinhvien.TenSv;
        }
    }
}
