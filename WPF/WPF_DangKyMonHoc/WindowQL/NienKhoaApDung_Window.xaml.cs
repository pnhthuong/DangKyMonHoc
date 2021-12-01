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
    /// Interaction logic for NienKhoaApDung_Window.xaml
    /// </summary>
    public partial class NienKhoaApDung_Window : Window
    {
		private List<NienKhoaCdk> listCDKNK = new List<NienKhoaCdk>();
        public NienKhoaApDung_Window(CongDangKy cdk)
        {
            InitializeComponent();
			hienthiTTCongDangKy(cdk);
			getload(cdk.MaCdk);
			//listCDKNK = Xuly_CongDangKy.getDSCDKNK(cdk.MaCdk);
			cmb_nienkhoa.ItemsSource = XLNienKhoa.getds();
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
        public void getload(string id)
        {
            listCDKNK = Xuly_MonHocNguyenVong.getDSCDKNK(id);
            if (listCDKNK == null)
            {
                MessageBox.Show("Danh sách Rỗng", "Thông báo");
            }
            list_cdknienkhoa.ItemsSource = listCDKNK;
        }

        private void btn_chon(object sender, RoutedEventArgs e)
        {
			NienKhoa nk = cmb_nienkhoa.SelectedItem as NienKhoa;
			if (nk == null) return;
			NienKhoaCdk input = new NienKhoaCdk();
			input.MaCdk = txtMaCDK.Text;
			input.MaNk = nk.MaNk.Trim();
            if (listCDKNK != null)
            {
                var check = listCDKNK.Find(x => x.MaNk.Trim() == nk.MaNk.Trim());
                if (check != null)
                {
					MessageBox.Show("Niên khóa đã được chọn trong danh sách!", "Thông báo");
					return;
                }
				listCDKNK.Add(input);
			}
			else
            {
				listCDKNK.Add(input);
			}

			list_cdknienkhoa.ItemsSource = null;
			list_cdknienkhoa.ItemsSource = listCDKNK;
		}

        private void btn_xoa(object sender, RoutedEventArgs e)
        {
			var a = list_cdknienkhoa.SelectedItems;
			if (a == null) return;
			foreach(var i in a)
            {
				NienKhoaCdk nk = i as NienKhoaCdk;
				listCDKNK.Remove(nk);
            }

			list_cdknienkhoa.ItemsSource = null;
			list_cdknienkhoa.ItemsSource = listCDKNK;
		}

        private void btn_luu(object sender, RoutedEventArgs e)
        {
            

			bool result = Xuly_MonHocNguyenVong.PostDSNKCDK(listCDKNK);
            if (result == false)
            {
				MessageBox.Show("Lưu thất bại. Vui lòng kiểm tra lại!", "Thông Báo");
				return;
            }
			MessageBox.Show("Lưu Thành Công", "Thông báo");

        }

        private void btn_Thoat(object sender, RoutedEventArgs e)
        {
			this.Close();
        }
    }
}
