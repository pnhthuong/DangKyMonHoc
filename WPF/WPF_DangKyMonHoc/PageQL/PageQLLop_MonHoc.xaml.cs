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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_DangKyMonHoc.Models;
using Wpf_DangKyMonHoc.WindowQL;
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc.PageQL
{
	/// <summary>
	/// Interaction logic for PageQLLop_MonHoc.xaml
	/// </summary>
	public partial class PageQLLop_MonHoc 
	{
		public PageQLLop_MonHoc()
		{
			InitializeComponent();
			getload();
		}
		public void getload()
        {
			List<CongDangKy> list = XLLopMonHoc.getCDKCombo();
            if (list == null) { MessageBox.Show("Không thể tải cổng đăng ký", "Thông Báo"); }
			cmb_CDK.ItemsSource = list;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var a = cmb_CDK.SelectedItem as CongDangKy;
			if (a == null) return;
			List<LopMonHoc> list = XLLopMonHoc.getDSLMHfromMa(a.MaCdk);
            if (list == null)
            {
				MessageBox.Show("Cổng đăng ký chưa có lớp môn học", "Thông báo");
				return;
            }
			listLopmh.ItemsSource = list;
        }

        private void listLopmh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var a = listLopmh.SelectedItem as LopMonHoc;
			if (a == null) return;
			txtMaCDK.Text = a.MaCdk;
			txtMaLopMh.Text = a.MaLmh;
			txtTenLopMh.Text = a.TenLmh;
			txtSiSo.Text = a.Siso.ToString();
			txtMaMh.Text = a.MaMh;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			var a = listLopmh.SelectedItem as LopMonHoc;
            if (a == null)
            {
				MessageBox.Show("Bạn cần chọn Lớp Môn Học", "Thông Báo"); return;
            }
			var show = new LopMH_SinhVien_Window(a.MaLmh);
			show.ShowDialog();
        }
    }
}
