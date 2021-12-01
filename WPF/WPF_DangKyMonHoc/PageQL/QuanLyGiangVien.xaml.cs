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
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc.Page
{
    /// <summary>
    /// Interaction logic for QuanLyGiangVien.xaml
    /// </summary>
    public partial class QuanLyGiangVien 
    {
		private Xuly_Chung xlc = new Xuly_Chung();
		private List<GiangVien> listgv= XLGiangVien.getAll();
		public QuanLyGiangVien()
        {
            InitializeComponent();
			getLoad();
        }

		public void getLoad()
		{
			List<GiangVien> list = XLGiangVien.getAll();
			if (list == null) MessageBox.Show("Lỗi tải Server!!!", "ERROR");
			listgiangvien.ItemsSource = list;

			List<Khoa> listkhoa = XLKhoa.getds();
			if (listkhoa == null) MessageBox.Show("Lỗi tải Lớp từ Server!", "ERROR");
			cmb_khoa.ItemsSource = listkhoa;

			//List<ChucVu> listcv = XLChucVu.dschucvu();
			//if (listcv == null) MessageBox.Show("Lỗi tải Lớp từ Server!", "ERROR");
			//cmb_chucvu.ItemsSource = listcv;
			//cmb_chucvu.SelectedValue = "GV        ";


			List<HocHam> listhh = xlc.GetHocHams();
			if (listhh == null) MessageBox.Show("Lỗi tải Lớp từ Server!", "ERROR");
			cmb_hocham.ItemsSource = listhh;
		}
		public void clean()
		{
			txt_ma.Text = "";
			txt_ten.Text = "";
			txt_dienthoai.Text = "";
			txt_email.Text = "";
			txt_cmnd.Text = "";
			txt_diachi.Text = "";
			date_ngaysinh.SelectedDate = null;
			//txt_matkhau.Text = "";
			btn_trangthai.IsChecked = false;

			txt_ma.IsReadOnly = false;
			//txt_matkhau.IsReadOnly =false;
			cmb_hocham.SelectedItem = null;
			//cmb_chucvu.SelectedItem = null;
			cmb_khoa.SelectedItem = null;
			listgiangvien.SelectedValue = null;
			imgHinh.Source = null;
		}
		private void btn_them(object sender, RoutedEventArgs e)
		{
            if (txt_ma.Text == null || txt_ten.Text == "" || txt_diachi.Text == "")
            {
				return;
            }
			HocHam hh = cmb_hocham.SelectedItem as HocHam;
			if (xlc.isValidEmail(txt_email.Text) == false)
			{
				MessageBox.Show("Email nhập sai! Vui lòng nhập chính xác. VD:abc@123.com", "Thông báo"); return;
			}
            if (xlc.checkdatengaysinhnv(date_ngaysinh.SelectedDate.Value) == false)
            {
				MessageBox.Show("Ngày Sinh nhập không chính xác! Vui lòng kiểm tra lại thông tin.", "Thông báo");return;
            }
			if (xlc.checkcmnd(txt_cmnd.Text.Trim()) == false)
			{
				MessageBox.Show("CMND nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			if (xlc.checksdt(txt_dienthoai.Text.Trim()) == false)
			{
				MessageBox.Show("Số điện thoại nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			GiangVien sv = new GiangVien
			{
				MaGv = txt_ma.Text,
				TenGv = txt_ten.Text,
				Sdt = txt_dienthoai.Text,
				Email = txt_email.Text,
				Cmnd = txt_cmnd.Text,
				Diachi = txt_diachi.Text,
				Matkhau = xlc.resetpass(date_ngaysinh.SelectedDate.Value),
				Ngaysinh = date_ngaysinh.SelectedDate.Value,
				MaChucVu = "GV",
				Phai = (isnam.IsChecked == true) ? true : false,
				Hinhanh = (imgHinh.Source==null)?"":txt_ma.Text.Trim(),
				Trangthai = btn_trangthai.IsChecked == true ? true : false,
				MaKhoa= cmb_khoa.SelectedValue.ToString(),
				Hocham=hh.tenHocHam
			};
			var result = XLGiangVien.PostThemGiangVien(sv);
			if (result == null)
			{
				MessageBox.Show("Thêm SINH VIÊN không thành công, Bạn kiểm tra lại dữ liệu nhập vào", "Thông báo");
				return;
			}
			if (imgHinh.Source != null)
			{
				BitmapImage bm = imgHinh.Source as BitmapImage;
				MemoryStream ms = bm.StreamSource as MemoryStream;
				//bool okihinh = xulyhocvien.themhinhhocvien(a.hinh, ms.ToArray());

				FileUpload x = new FileUpload
				{
					tenhinh = result.MaGv.Trim(),
					hinh = ms.ToArray(),
					name = "GiangVien"
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
			if (listgiangvien.SelectedItem == null) return;
			if (xlc.isValidEmail(txt_email.Text) == false)
			{
				MessageBox.Show("Email nhập sai! Vui lòng nhập chính xác. VD:abc@123.com", "Thông báo"); return;
			}
			if (xlc.checkdatengaysinhnv(date_ngaysinh.SelectedDate.Value) == false)
			{
				MessageBox.Show("Ngày Sinh nhập không chính xác! Vui lòng kiểm tra lại thông tin.", "Thông báo"); return;
			}
			if (xlc.checkcmnd(txt_cmnd.Text.Trim()) == false)
			{
				MessageBox.Show("CMND nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			if (xlc.checksdt(txt_dienthoai.Text.Trim()) == false)
			{
				MessageBox.Show("Số điện thoại nhập sai! Vui lòng nhập chính xác.", "Thông báo"); return;
			}
			GiangVien b = listgiangvien.SelectedItem as GiangVien;
			
			b.TenGv = txt_ten.Text;
			b.Sdt = txt_dienthoai.Text;
			b.Email = txt_email.Text;
			b.Cmnd = txt_cmnd.Text;
			b.Diachi = txt_diachi.Text;
			b.Ngaysinh = date_ngaysinh.SelectedDate.Value;
			b.Trangthai = btn_trangthai.IsChecked == true ? true : false;
			HocHam hh = cmb_hocham.SelectedItem as HocHam;
			b.Hocham = hh.tenHocHam;

            bool result = XLGiangVien.PutSuaTTGiangVien(b);

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
					tenhinh = b.MaGv.Trim(),
					hinh = ms.ToArray(),
					name = "GiangVien"
				};
				bool okihinh = XLAnh.puthinh(b.MaGv.Trim(), x);
				if (okihinh == false) MessageBox.Show("ERROR Write Image!!");
			}
			MessageBox.Show("Sửa thành công", "Thông báo");
			clean();
			getLoad();

		}
		private void btn_xoa(object sender, RoutedEventArgs e)
		{
			if (listgiangvien.SelectedItem == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
					XLAnh.deletehinh(listgiangvien.SelectedValue.ToString());
					bool kq = XLGiangVien.DeleteXoaGiangVien(listgiangvien.SelectedValue.ToString());
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
			if (listgiangvien.SelectedItem == null) return;
			GiangVien sv = listgiangvien.SelectedItem as GiangVien;
			txt_ma.Text = sv.MaGv.Trim();
			txt_ten.Text = sv.TenGv.Trim();
			txt_diachi.Text = sv.Diachi.Trim();
			txt_email.Text = sv.Email.Trim();
			txt_dienthoai.Text = sv.Sdt.Trim();
			txt_cmnd.Text = sv.Cmnd.Trim();
			date_ngaysinh.SelectedDate = sv.Ngaysinh;
			//txt_matkhau.Text = "*****";
			if (sv.Phai == true)
			{
				isnam.IsChecked = true;
			}
			//cmb_chucvu.SelectedValue = sv.MaChucVu;
			if (sv.Trangthai == true)
			{
				btn_trangthai.IsChecked = true;
			}
			else
			{
				btn_trangthai.IsChecked = false;
			}

			HocHam a = xlc.hienthihh(sv.Hocham.Trim());
			cmb_hocham.SelectedValue = a.ID;
			cmb_khoa.SelectedValue = sv.MaKhoa;

			txt_ma.IsReadOnly = true;
			//txt_matkhau.IsReadOnly = true;
			if (sv.Hinhanh == null)
			{
				imgHinh.Source = null;
			}
			else
			{
				byte[] buf = XLAnh.gethinh(sv.MaGv);
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
			listgiangvien.ItemsSource = listgv;
		}

        private void btn_reset(object sender, RoutedEventArgs e)
        {
			if (listgiangvien.SelectedItem == null) return;
			GiangVien gv = listgiangvien.SelectedItem as GiangVien;
			gv.Matkhau = xlc.resetpass(gv.Ngaysinh);
			bool check = XLGiangVien.PutSuaPasswordGiangVien(gv);
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
			string textTim = txt_timkiem.Text.Trim();
			List<GiangVien> list = listgv.FindAll(x => x.MaGv.Trim().StartsWith(textTim) || x.TenGv.Trim().StartsWith(textTim));
            if (list == null)
            {
				MessageBox.Show("Không tìm thấy dữ liệu cần tìm", "Thông báo");return;
            }
			listgiangvien.ItemsSource = null;
			listgiangvien.ItemsSource = list;
        }
    }
}
