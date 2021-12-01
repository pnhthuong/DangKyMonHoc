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
	/// Interaction logic for LopMH_SinhVien_Window.xaml
	/// </summary>
	public partial class LopMH_SinhVien_Window : Window
	{
		List<DSSV_LopMonHoc> list = new List<DSSV_LopMonHoc>();
		public LopMH_SinhVien_Window(string maLmh)
		{
			InitializeComponent();
			getload(maLmh);
			txtMaLopMh.Text = maLmh;
			txtMaLopMh.IsReadOnly = true;
		}
		public void getload(string ma)
        {
			list = XLLopMonHoc.getDSSV(ma);
            if (list.Count < 1)
            {
				MessageBox.Show("Lớp học chưa có sinh viên", "Thông báo");
            }
			listLopmh.ItemsSource = list;
        }

		private void btn_ThemSV(object sender, RoutedEventArgs e)
		{
			if (txtMSsv.Text == "") { MessageBox.Show("Vui lòng nhập Mã Sinh Viên", "Thông Báo");return; }
			DSSV_LopMonHoc svnew = new DSSV_LopMonHoc { MaLmh = txtMaLopMh.Text, Masv = txtMSsv.Text, TenSv = txtHoten.Text };
			var check = list.Find(x => x.Masv == svnew.Masv);
			if(check != null)
            {
				MessageBox.Show("Sinh Viên Đã có trong danh sách lớp", "Thông báo");
				return;
            }
			ThongBao result = XLLopMonHoc.PostSinhVienNew(svnew);
            if (result.kq == false)
            {
				MessageBox.Show(result.status);
				return;
            }
			MessageBox.Show("Thêm Sinh Viên Thành Công");
			getload(txtMaLopMh.Text);
        }

        private void btnxoa(object sender, RoutedEventArgs e)
        {
			var a = listLopmh.SelectedItem as DSSV_LopMonHoc;
			if (a == null) return;
			bool result = XLLopMonHoc.DeleteSinhVien(a);
            if (result == false)
            {
				MessageBox.Show("Xóa KHÔNG Thành Công", "Thông Báo");
				return;
            }
			MessageBox.Show("Xóa Thành Công", "Thông Báo");
			getload(txtMaLopMh.Text);
        }

        private void btnThoat(object sender, RoutedEventArgs e)
        {
			this.Close();
        }
    }
}
