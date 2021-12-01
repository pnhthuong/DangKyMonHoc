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
	/// Interaction logic for TaoLopMH_Window.xaml
	/// </summary>
	public partial class TaoLopMH_Window : Window
	{
		public TaoLopMH_Window(CongDangKy cdk)
		{
			InitializeComponent();
			txtMaCDK.Text = cdk.MaCdk.Trim();
			getload(cdk.MaCdk);
		}
		public void getload(string cdk)
        {
			var list = XLLopMonHoc.getdsmhdm(cdk);
			if (list == null) {
				MessageBox.Show("Danh sách môn học được mở rỗng");
				return;
			}
			
			list_monhocduocmo.ItemsSource = list;
        }

        private void list_monhocduocmo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var a = list_monhocduocmo.SelectedItem as MonHocDuocMoCus;
			if (a == null) return;
			txtMaMh.Text = a.MaMh;
        }

        private void btn_Thoat(object sender, RoutedEventArgs e)
        {
			this.Close();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (list_monhocduocmo.SelectedItem == null)
            {
				MessageBox.Show("Vui lòng chọn môn học cần mở lớp", "Thông báo");
				return;
            }
			var a = list_monhocduocmo.SelectedItem as MonHocDuocMoCus;
			if (a.trangthaitaolop == true)
			{
				MessageBox.Show("Môn Học đã tạo lớp", "Thông báo");
				return;
			}
			if (txtSiso.Text == ""||int.Parse(txtSiso.Text)<1)
            {
				MessageBox.Show("Vui lòng nhập đúng số lượng sinh viên của từng lớp muốn mở", "Thông báo");
				return;
            }
			
			Lop_MonHoc_Custom lop = new Lop_MonHoc_Custom();
			lop.MaCdk = a.MaCdk;
			lop.MaMh = txtMaMh.Text;
			lop.Siso = int.Parse(txtSiso.Text);
			bool result = XLLopMonHoc.PostLopMonHoc(lop);
            if (result == false)
            {
				MessageBox.Show("Tạo lớp không thành công", "Thông Báo");
				return;
            }
			MessageBox.Show("Tạo lớp môn học thành công", "Thông bao");
			getload(txtMaCDK.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
			Xuly_Chung xlc = new Xuly_Chung();
			xlc.textNumber(e);
		}
	}
}
