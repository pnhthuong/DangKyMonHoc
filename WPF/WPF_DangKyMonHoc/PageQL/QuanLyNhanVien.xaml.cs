using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Wpf_DangKyMonHoc.Page
{
    /// <summary>
    /// Interaction logic for QuanLyNhanVien.xaml
    /// </summary>
    public partial class QuanLyNhanVien 
    {
		private readonly Xuly_Chung xlc = new Xuly_Chung();
		private List<NhanVien> listnv = XLNhanVien.getAll();
		public QuanLyNhanVien()
        {
            InitializeComponent();
			getLoad();
        }

		public void getLoad()
		{
			List<NhanVien> list = XLNhanVien.getAll();
			if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
			listnhanvien.ItemsSource = list;

			List<ChucVu> listcv = XLChucVu.dschucvu();
			if (listcv == null) MessageBox.Show("Lỗi tải Lớp từ Server!", "ERROR");
			cmb_chucvu.ItemsSource = listcv;
		}
		public void clean()
		{
			txt_manv.Text = "";
			txt_tennv.Text = "";
			txt_sdt.Text = "";
			txt_email.Text = "";
			txt_cmnd.Text = "";
			txt_diachi.Text = "";
			date_ngaysinh.SelectedDate = null;
			//txt_matkhau.Text = "";
			txt_manv.IsReadOnly = false;
			txt_matkhau.IsReadOnly = false;
			isnam.IsChecked = true;
			cmb_chucvu.SelectedItem = null;
			btn_trangthai.IsChecked = false;
			listnhanvien.SelectedItem = null;
			imgHinh.Source = null;
		}
		private void btn_them(object sender, RoutedEventArgs e)
		{
			if (txt_manv.Text == "" || txt_cmnd.Text == "" || txt_tennv.Text == "" || txt_diachi.Text == "" || txt_email.Text == "" || txt_sdt.Text == "" || date_ngaysinh.SelectedDate == null || cmb_chucvu.SelectedItem == null)
			{
				MessageBox.Show("Vui lòng điền đầy đủ thông tin !", "Thông báo");
				return;
			}
			if (xlc.isValidEmail(txt_email.Text)==false)
            {
				MessageBox.Show("Email nhập sai! Vui lòng nhập chính xác. VD:abc@123.com", "Thông báo");return;
            }
			if (xlc.checkdatengaysinhnv(date_ngaysinh.SelectedDate.Value) == false)
			{
				MessageBox.Show("Ngày Sinh nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
            if (xlc.checkcmnd(txt_cmnd.Text.Trim()) == false)
            {
				MessageBox.Show("CMND nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			if (xlc.checksdt(txt_sdt.Text.Trim()) == false)
			{
				MessageBox.Show("Số điện thoại nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			NhanVien sv = new NhanVien
			{
				MaNv = txt_manv.Text,
				TenNv = txt_tennv.Text,
				Sdt = txt_sdt.Text,
				Email = txt_email.Text,
				Cmnd = txt_cmnd.Text,
				Diachi = txt_diachi.Text,
				Matkhau = xlc.resetpass(date_ngaysinh.SelectedDate.Value),
				Ngaysinh = date_ngaysinh.SelectedDate.Value,
				MaChucVu=cmb_chucvu.SelectedValue.ToString(),
				Phai = (isnam.IsChecked == true) ? true : false,
				Hinhanh =(imgHinh.Source==null)?"": txt_manv.Text,
				Trangthai = btn_trangthai.IsChecked == true ? true : false
			};
			var result = XLNhanVien.PostThemNhanVien(sv);
			if (result == null)
			{
				MessageBox.Show("Thêm dữ liệu không thành công, Bạn kiểm tra lại dữ liệu nhập vào", "Thông báo");
				return;
			}
			if (imgHinh.Source != null)
			{
				BitmapImage bm = imgHinh.Source as BitmapImage;
				MemoryStream ms = bm.StreamSource as MemoryStream;
				//bool okihinh = xulyhocvien.themhinhhocvien(a.hinh, ms.ToArray());

				FileUpload x = new FileUpload
				{
					tenhinh = result.MaNv.Trim(),
					hinh = ms.ToArray(),
					name = "NhanVien"
				};
				bool okihinh = XLAnh.posthinh(x);
				if (okihinh == false) MessageBox.Show("ERROR Write Image!!");
			}

			MessageBox.Show("Thêm thành công", "Thông báo");
			clean();
			getLoad();
		}

		private void btn_sua(object sender, RoutedEventArgs e)
		{
			if (listnhanvien.SelectedItem==null)
			{
				return;
			}
			if (xlc.isValidEmail(txt_email.Text) == false)
			{
				MessageBox.Show("Email nhập sai! Vui lòng nhập chính xác. VD:abc@123.com", "Thông báo"); return;
			}
			if (xlc.checkdatengaysinhnv(date_ngaysinh.SelectedDate.Value) == false)
			{
				MessageBox.Show("Ngày Sinh nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			if (xlc.checkcmnd(txt_cmnd.Text.Trim()) == false)
			{
				MessageBox.Show("CMND nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			if (xlc.checksdt(txt_sdt.Text.Trim()) == false)
			{
				MessageBox.Show("Số điện thoại nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			NhanVien sv = listnhanvien.SelectedItem as NhanVien;
			sv.TenNv = txt_tennv.Text;
			sv.Sdt = txt_sdt.Text;
			sv.Email = txt_email.Text;
			sv.Cmnd = txt_cmnd.Text;
			sv.Diachi = txt_diachi.Text;
			sv.Ngaysinh = date_ngaysinh.SelectedDate.Value;
			
			sv.Phai = (isnam.IsChecked == true) ? true : false;
			//Hinhanh = "",
			sv.Trangthai = btn_trangthai.IsChecked == true ? true : false;

            bool result = XLNhanVien.PutSuaTTNhanVien(sv);

			if (result == false)
			{
				MessageBox.Show("Sửa không thành công !", "Thông báo");
				return;

			}
			
			if (imgHinh.Source != null)
			{
				BitmapImage bm = imgHinh.Source as BitmapImage;
				MemoryStream ms = bm.StreamSource as MemoryStream;
				//bool okihinh = xulyhocvien.themhinhhocvien(a.hinh, ms.ToArray());

				FileUpload x = new FileUpload
				{
					tenhinh = sv.MaNv.Trim(),
					hinh = ms.ToArray(),
					name = "NhanVien"
				};
				bool okihinh = XLAnh.puthinh(sv.MaNv.Trim(), x);
				if (okihinh == false) MessageBox.Show("ERROR Write Image!!");
			}
			MessageBox.Show("Sửa thành công", "Thông báo");
			clean();
			getLoad();

		}
		private void btn_xoa(object sender, RoutedEventArgs e)
		{
			if (listnhanvien.SelectedItem == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					XLAnh.deletehinh(listnhanvien.SelectedValue.ToString());
					bool kq =XLNhanVien.DeleteXoaNhanVien(listnhanvien.SelectedValue.ToString());
					if (kq == false)
					{
						MessageBox.Show("Không thể xóa dữ liệu do đã được sử dụng ở chức năng khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information); break;
					}
					
					MessageBox.Show("Xóa Thành Công", "Thông Báo");
					clean();
					getLoad();
					break;
			}
		}

		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
		{
			xlc.textNumber(e);
		}

        private void listnhanvien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if (listnhanvien.SelectedItem == null) return;
			NhanVien sv = listnhanvien.SelectedItem as NhanVien;
			txt_manv.Text = sv.MaNv.Trim();
			txt_tennv.Text = sv.TenNv.Trim();
			txt_diachi.Text = sv.Diachi.Trim();
			txt_email.Text = sv.Email.Trim();
			txt_sdt.Text = sv.Sdt.Trim();
			txt_cmnd.Text = sv.Cmnd.Trim();
			date_ngaysinh.SelectedDate = sv.Ngaysinh;
			//txt_matkhau.Text = "*****";
			if (sv.Phai == true)
			{
				isnam.IsChecked = true;
			}
            else
            {
				isnu.IsChecked = true;
            }
			cmb_chucvu.SelectedValue=sv.MaChucVu;
			if (sv.Trangthai == true)
			{ 
				btn_trangthai.IsChecked = true; 
			}
			else
			{
				btn_trangthai.IsChecked = false;
			}

			txt_manv.IsReadOnly = true;
			//txt_matkhau.IsReadOnly = true;
			if (sv.Hinhanh == null)
			{
				imgHinh.Source = null;
			}
			else
			{
				byte[] buf = XLAnh.gethinh(sv.Hinhanh);
				if (buf == null) MessageBox.Show("Không có hình trên hệ thống!", "Thông báo");
				else
				{
					MemoryStream ms = new MemoryStream(buf);
					BitmapImage bm = new BitmapImage();
					bm.BeginInit();
					bm.StreamSource = ms;
					bm.EndInit();
					imgHinh.Source = bm;
				}

			}
		}

        private void btn_lammoi(object sender, RoutedEventArgs e)
        {
			clean();
			listnhanvien.ItemsSource = listnv;
        }

        private void textnumberinput(object sender, TextCompositionEventArgs e)
        {
			xlc.textNumber(e);
        }

        private void btn_reset(object sender, RoutedEventArgs e)
        {
			if (listnhanvien.SelectedItem == null) return;
			NhanVien nv = listnhanvien.SelectedItem as NhanVien;
			nv.Matkhau = xlc.resetpass(nv.Ngaysinh);
			bool check = XLNhanVien.PutSuaPasswordNhanVien(nv);
			if (check == false)
			{
				MessageBox.Show("Không thể Reset lại mật khẩu! Kiểm tra lại thông tin.", "Thông báo");
				return;
			}
			MessageBox.Show("Reset thành công.", "Thông báo");
			clean();
			getLoad();
		}

        private void btn_chonanh(object sender, RoutedEventArgs e)
        {
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == true)
			{
				FileStream f = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
				byte[] buf = new byte[f.Length];
				f.Read(buf, 0, (int)f.Length);
				f.Close();
				MemoryStream ms = new MemoryStream(buf);
				BitmapImage bm = new BitmapImage();
				bm.BeginInit();
				bm.StreamSource = ms;
				bm.EndInit();
				imgHinh.Source = bm;
			}
		}

        private void btn_tim(object sender, RoutedEventArgs e)
        {
			string texttim = txt_timkiem.Text.Trim();
			List<NhanVien> list = listnv.FindAll(x => x.MaNv.Trim().StartsWith(texttim) || x.TenNv.Trim().StartsWith(texttim));
			if(list==null)
            {
				MessageBox.Show("Không tìm thấy dữ liệu cần tìm", "Thông báo");return;

            }
			listnhanvien.ItemsSource = null;
			listnhanvien.ItemsSource = list;
        }

        private void btn_chucvu(object sender, RoutedEventArgs e)
        {
			var n = new QuanLyChucVu();
			n.ShowDialog();
			List<ChucVu> listcv = XLChucVu.dschucvu();
			if (listcv == null) MessageBox.Show("Lỗi tải Lớp từ Server!", "ERROR");
			cmb_chucvu.ItemsSource = listcv;
		}

        private void link_chucvu(object sender, RoutedEventArgs e)
        {
			var n = new QuanLyChucVu();
			n.ShowDialog();
			List<ChucVu> listcv = XLChucVu.dschucvu();
			if (listcv == null) MessageBox.Show("Lỗi tải Lớp từ Server!", "ERROR");
			cmb_chucvu.ItemsSource = listcv;
		}
    }
}
