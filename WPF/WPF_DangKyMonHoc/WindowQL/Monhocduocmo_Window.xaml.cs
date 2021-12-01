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
using Wpf_DangKyMonHoc.XuLy;

namespace Wpf_DangKyMonHoc.WindowQL
{
	/// <summary>
	/// Interaction logic for Monhocduocmo_Window.xaml
	/// </summary>
	public partial class Monhocduocmo_Window : Window
	{
		private List<MonHocDuocMo> listmonhocduocmo = new List<MonHocDuocMo>();
		private string maNKApDung = null;
		public Monhocduocmo_Window(CongDangKy cdk)
		{
			InitializeComponent();
			getLoad(cdk.MaCdk);
			hienthiTTCongDangKy(cdk);
			loadmonhocduocmo(cdk.MaCdk);
		}
		public void loadmonhocduocmo(string id)
        {
			listmonhocduocmo = Xuly_MonHocNguyenVong.getAllmonhocduocmo(id);
            if (listmonhocduocmo == null)
            {
				MessageBox.Show("Cổng đào tạo chưa có môn học được mở", "Thông báo");
            }
			list_mhdm.ItemsSource = listmonhocduocmo;
        } 

		public void hienthiTTCongDangKy(CongDangKy a)
        {
			Xuly_Chung xlc = new Xuly_Chung();
			List<HocKyDkmh> listHKDKMH = Xuly_HockyDKMH.getAllHocKyDKMH();
			List<NamHocDkmh> listNamHocDKMH = Xuly_NamHocDKMH.getAllNamHocDkmh();
			if (listHKDKMH == null || listNamHocDKMH == null)
				MessageBox.Show("Lỗi server !!!", "ERROR");
			else
			{
				cboHocKy.ItemsSource = listHKDKMH;
				cboNamHoc.ItemsSource = listNamHocDKMH;
				cboTrangThai.ItemsSource = xlc.getTrangThaiCong();
			}
			txtMaCDK.Text = a.MaCdk;
			txtTenCDK.Text = a.TenCdk;
			dpThoiGianMo.SelectedDate = a.ThoigianBatDau;

			dpThoiGianDong.SelectedDate = a.ThoigianKetThuc;
			cboHocKy.SelectedValue = a.MaHk;
			cboNamHoc.SelectedValue = a.MaNh;
			cboTrangThai.SelectedValue = a.Trangthai;
			txtMaCDK.IsReadOnly = true;
			txtTenCDK.IsReadOnly = true;
		}

		public void getLoad(string cdk)
		{
			List<NienKhoa> listnienkhoa = Xuly_MonHocNguyenVong.getDSNKvsNKCDK(cdk);
			if (listnienkhoa.Count < 1) MessageBox.Show("Không thể truyền tải Niên khóa!", "Thông báo");
			cmb_nienkhoa.ItemsSource = listnienkhoa;

			List<HocKyCtdt> listhocky = Xuly_HockyCTDT.getAllHocKyCTDT();
			if(listhocky.Count<1) MessageBox.Show("Không thể truyền tải Học Kỳ!", "Thông báo");
			cmb_hockyctdt.ItemsSource = listhocky;

			
		}

        private void btn_tim(object sender, RoutedEventArgs e)
        {
			NienKhoa nk = cmb_nienkhoa.SelectedItem as NienKhoa;
			HocKyCtdt hk = cmb_hockyctdt.SelectedItem as HocKyCtdt;
			if(nk==null||hk==null)
            {
				MessageBox.Show("Bạn vui lòng chọn cả 2 thuộc tính: Niên Khóa Và Học Kỳ để lọc dữ liệu", "Thông báo");return;
            }

			List<MonHoc> listmh = Xuly_MonHocNguyenVong.getsearchmonhoc(nk.MaNk, hk.MaHk);
			if (listmh==null)
			{
				MessageBox.Show("Không tìm thấy!", "Thông báo");
				return;
			}
			list_monhoc.ItemsSource = null;
			list_monhoc.ItemsSource = listmh;
			maNKApDung = nk.MaNk;

			cmb_nienkhoa.SelectedItem = null;
			cmb_hockyctdt.SelectedItem = null;
        }

        private void btn_Them(object sender, RoutedEventArgs e)
        {
			var l_chonmonhoc = list_monhoc.SelectedItems;
            if (l_chonmonhoc.Count <1)
            {
				MessageBox.Show("Bạn chưa chọn môn học được mở!", "Thông báo");
				return;
            }
			foreach(var check in l_chonmonhoc)
            {
				MonHoc a = check as MonHoc;
				var b=listmonhocduocmo.Find(x => x.MaMh == a.MaMh);
                if (b != null)
                {
					MessageBox.Show("Môn Học đã được thêm vào trong danh sách", "Thông báo");
					return;
                }
            }
			var n = new CT_MonHocDuocMo_Window();
			n.ShowDialog();
            if (n.soluong == 0) { return; }
			foreach(var a in l_chonmonhoc)
            {
				MonHoc b = a as MonHoc;
				listmonhocduocmo.Add(new MonHocDuocMo
				{
					MaCdk = txtMaCDK.Text.Trim(),
					MaMh = b.MaMh,
					Soluong = n.soluong,
					Trangthai = n.trangthai,
					NkapDung=maNKApDung
				});
            }

			list_mhdm.ItemsSource = null;
			list_mhdm.ItemsSource = listmonhocduocmo;
        }

        private void btn_xoa(object sender, RoutedEventArgs e)
        {
			var listxoamonhocduocmo = list_mhdm.SelectedItems;
			foreach(var a in listxoamonhocduocmo)
            {
				MonHocDuocMo b = a as MonHocDuocMo;
				listmonhocduocmo.Remove(b);
            }
			
			list_mhdm.ItemsSource = null;
			list_mhdm.ItemsSource = listmonhocduocmo;
		}

        private void list_mhdm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			MonHocDuocMo clickmh = list_mhdm.SelectedItem as MonHocDuocMo;
			if (clickmh == null) return;
			txt_id.Text = clickmh.Id.ToString();
			txt_macdk.Text = clickmh.MaCdk;
			txt_soluong.Text = clickmh.Soluong.ToString();
			txt_mamh.Text = clickmh.MaMh;
			btn_trangthai.IsChecked = clickmh.Trangthai;
			txt_id.IsReadOnly = true;
			txt_macdk.IsReadOnly = true;
			txt_mamh.IsReadOnly = true;
		}
		public void clean()
        {
			txt_id.Text = null;
			txt_macdk.Text = null;
			txt_soluong.Text = null;
			txt_mamh.Text = null;
			btn_trangthai.IsChecked = false;
			txt_id.IsReadOnly = false;
			txt_macdk.IsReadOnly = false;
			txt_mamh.IsReadOnly = false;
		}
        private void btn_sua(object sender, RoutedEventArgs e)
        {
			MonHocDuocMo clickmh = list_mhdm.SelectedItem as MonHocDuocMo;
			if (clickmh == null) return;
			clickmh.Soluong = int.Parse(txt_soluong.Text);
			clickmh.Trangthai = (btn_trangthai.IsChecked == true) ? true : false;

			var a =listmonhocduocmo.Find(x => x.MaMh == clickmh.MaMh);
            if (a != null)
            {
				listmonhocduocmo.Remove(a);
				listmonhocduocmo.Add(clickmh);
            }
            else
            {
				listmonhocduocmo.Add(clickmh);
			}

			clean();
			list_mhdm.ItemsSource = null;
			list_mhdm.ItemsSource = listmonhocduocmo;
		}

        private void btn_luu(object sender, RoutedEventArgs e)
        {
            if (listmonhocduocmo.Count < 1)
            {
				MessageBox.Show("Danh sách môn học được mở rỗng!", "Thông báo");
				return;
            }

			bool result = Xuly_MonHocNguyenVong.PostMonHocDuocMo(listmonhocduocmo);
            if (result == false)
            {
				MessageBox.Show("Không thể lưu dữ liệu!", "Thông báo");
				return;
            }
			MessageBox.Show("Dữ liệu đã lưu thành công", "Thông báo");
			this.Close();
        }
    }
}
