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
using Wpf_DangKyMonHoc.Xuly;
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc
{
	/// <summary>
	/// Interaction logic for Page_QLMonhoc.xaml
	/// </summary>
	public partial class Page_QLMonhoc
	{
		private Xuly_Chung xlc = new Xuly_Chung();
		private List<MonHoc> listMH= Xuly_Monhoc.getAllMonHoc();
		public Page_QLMonhoc()
		{
			InitializeComponent();
			getload();
		}
		public void getload()
		{
			List<MonHoc> list = Xuly_Monhoc.getAllMonHoc();
			if (list == null)
				MessageBox.Show("Không có dữ liệu", "Thông báo");
			listMonhoc.ItemsSource = list;
			List<KhoiKienThuc> listKkt = Xuly_KhoiKienThuc.getAllKhoiKienThuc();
			if (listKkt == null)
				MessageBox.Show("Khối kiến thức không có dữ liệu", "Thông báo");
			cboKhoiKienThuc.ItemsSource = listKkt;
			CboThucHanh.ItemsSource = xlc.getThuocTinh();

		}
		public void Clear()
		{
			txtMamonHoc.Clear();
			txtTenMonhoc.Clear();
			txtSotinchi.Clear();
			txtHesoHP.Clear();
			txtMonSH.Clear();
			txtMonTQ.Clear();
			txtMamonHoc.IsReadOnly = false;
			cboKhoiKienThuc.SelectedItem = null;
			CboThucHanh.SelectedItem = null;
			listMonhoc.SelectedItem = null;
			txtMamonHoc.IsReadOnly = false;
		}

		private void listMonhoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listMonhoc.SelectedItem == null)
				return;
			MonHoc a = listMonhoc.SelectedItem as MonHoc;
			txtMamonHoc.Text = a.MaMh;
			txtTenMonhoc.Text = a.TenMh;
			txtSotinchi.Text = a.Sotinchi.ToString();
			txtHesoHP.Text = a.HesoHp.ToString();
			txtMonSH.Text = a.MaSh;
			txtMonTQ.Text = a.MaTq;
			cboKhoiKienThuc.SelectedValue = a.MaKhoi;
			CboThucHanh.SelectedValue = a.Thuchanh;
			txtMamonHoc.IsReadOnly = true;
		}

		private void btnThemMonHoc_Click(object sender, RoutedEventArgs e)
		{
			if (txtMamonHoc.Text == "" || txtHesoHP.Text == "" || txtTenMonhoc.Text == "" || txtSotinchi.Text == "" || cboKhoiKienThuc.SelectedItem == null || CboThucHanh.SelectedItem == null)

			{
				MessageBox.Show("Vui lòng điền đầy đủ thông tin !", "Thông báo");
				return;
			}
			BatBuoc batbuoc = CboThucHanh.SelectedItem as BatBuoc;
			MonHoc a = new MonHoc
			{
				MaMh = txtMamonHoc.Text,
				TenMh = txtTenMonhoc.Text,
				Sotinchi = (byte)int.Parse(txtSotinchi.Text),
				HesoHp = (byte)int.Parse(txtHesoHP.Text),
				Thuchanh = batbuoc.ID,
				MaKhoi = cboKhoiKienThuc.SelectedValue.ToString(),
				MaSh = txtMonSH.Text,
				MaTq = txtMonTQ.Text

			};

			bool kq = Xuly_Monhoc.PostMonHoc(a);
			if (kq == true)
			{
				MessageBox.Show("Thêm môn học thành công !", "Thông báo");
				Clear();
				getload();
			}
			if (kq == false)
			{
				MessageBox.Show("Dữ liệu đang không đúng hoặc hệ thống có lỗi !", "ERROR");
			}
		}

		private void btnSuaMonHoc_Click(object sender, RoutedEventArgs e)
		{
			if (listMonhoc.SelectedItem == null)
				return;
			if (txtMamonHoc.Text == "" || txtHesoHP.Text == "" || txtTenMonhoc.Text == "" || txtSotinchi.Text == "" || cboKhoiKienThuc.SelectedItem == null || CboThucHanh.SelectedItem == null)
			{
				MessageBox.Show("Vui lòng điền đầy đủ thông tin !", "Thông báo");
				return;
			}
			BatBuoc batbuoc = new BatBuoc();
			MonHoc a = listMonhoc.SelectedItem as MonHoc;
			a.TenMh = txtTenMonhoc.Text;
			a.Sotinchi = (byte)int.Parse(txtSotinchi.Text);
			a.HesoHp = (byte)int.Parse(txtHesoHP.Text);
			a.Thuchanh = batbuoc.ID;
			a.MaKhoi = cboKhoiKienThuc.SelectedValue.ToString();
			a.MaSh = txtMonSH.Text;
			a.MaTq = txtMonTQ.Text;
			bool kq = Xuly_Monhoc.PutSuaMonHoc(a);
			if (kq == true)
			{
				MessageBox.Show("Chỉnh sửa thành công", "Thông báo");
				Clear();
				getload();
			}
			else
				MessageBox.Show("Không thể chỉnh sửa", "ERROR");
		}

		private void btnXoaMonHoc_Click(object sender, RoutedEventArgs e)
		{
			if (listMonhoc.SelectedItem == null)
				return;
			MonHoc a = listMonhoc.SelectedItem as MonHoc;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					bool kq = Xuly_Monhoc.DeleteMonhoc(a.MaMh);
					if (kq == false)
					{
						MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information); break;
					}
					MessageBox.Show("Xóa Thành Công", "Thông Báo");
					Clear();
					getload();
					break;
			}


		}

		private void btnLammoi_Click(object sender, RoutedEventArgs e)
		{
			Clear();
		}

        private void txtSotinchi_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
			xlc.textNumber(e);
		}

        private void txtHesoHP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
			xlc.textNumber(e);
		}

        private void btn_KhoiKienThuc(object sender, RoutedEventArgs e)
        {
			var n = new Khoikienthuc_Window();
			n.ShowDialog();
			List<KhoiKienThuc> listKkt = Xuly_KhoiKienThuc.getAllKhoiKienThuc();
			if (listKkt == null)
				MessageBox.Show("Khối kiến thức không có dữ liệu", "Thông báo");
			cboKhoiKienThuc.ItemsSource = listKkt;
		}

        private void btn_Tim(object sender, RoutedEventArgs e)
        {
			if (txt_tkMonhoc.Text == null) { getload(); }
			List<MonHoc> listtk = listMH.FindAll(x => x.MaMh.Contains(txt_tkMonhoc.Text));
			listMonhoc.ItemsSource = null;
            listMonhoc.ItemsSource = listtk;
		}
    }
}
