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
using Wpf_DangKyMonHoc.Xuly;
using System.Linq;

namespace Wpf_DangKyMonHoc.WindowQL
{
	/// <summary>
	/// Interaction logic for XemChiTietPDK_Window.xaml
	/// </summary>
	public partial class XemChiTietPDK_Window : Window
	{
		private List<ChiTietPdk> listchitiet = new List<ChiTietPdk>();
		PhieuDangKy_Custom pdk = new PhieuDangKy_Custom();
		PhieuDangKy pdk_b = new PhieuDangKy();
		public XemChiTietPDK_Window(PhieuDangKy_Custom a)
		{
			InitializeComponent();
			getLoadPhieuDK(a.MaPdk);
			GetLoadPage(a);
			getLoadMonHocDuocmo(a.MaCdk);
		}
		public void getLoadPhieuDK(int mapdk)
		{
			pdk_b.ChiTietPdks = XL_PhieuDangKy.GetChiTietPdks(mapdk);
			if(pdk_b.ChiTietPdks == null)
			{
				MessageBox.Show("Không có chi tiết phiếu đăng ký", "Thông báo");
				return;
			}
			listCTPDK.ItemsSource = pdk_b.ChiTietPdks;
		}
		public void GetLoadPage(PhieuDangKy_Custom a)
		{
			txtMaSv.Text = a.MaSv.ToString();
			txtMaSv.IsReadOnly = true;
			txtTenSv.Text = a.TenSv.ToString();
			txtTenSv.IsReadOnly = true;
			txtMaPdk.Text = a.MaPdk.ToString();
			txtMaPdk.IsReadOnly = true;
			dpNgaydk.SelectedDate = a.Ngaylap;
			pdk = a;
		}
		public void getLoadMonHocDuocmo(string id)
		{
			var list = Xuly_MonHocNguyenVong.getAllmonhocduocmo(id);
			if(list==null)
			{
				MessageBox.Show("Không có môn học được mở", "Thông báo");
				return;
			}
			list_monhocduocmo.ItemsSource = list;
		}

		private void btnThem_Click(object sender, RoutedEventArgs e)
		{
			var l_chon = list_monhocduocmo.SelectedItems;
			if(l_chon==null)
			{
				MessageBox.Show("Bạn chưa chọn môn học cần thêm !", "Thông báo");
				return;
			}
			foreach(var a in l_chon)
			{
				MonHocDuocMo check = a as MonHocDuocMo;
				var b = pdk_b.ChiTietPdks.SingleOrDefault(x => x.MaMh == check.MaMh);
				if(b!=null)
				{
					MessageBox.Show("Môn học đã được đăng ký!", "Thông báo");
					return;
				}
			}
			foreach(var a in l_chon)
			{
				MonHocDuocMo b = a as MonHocDuocMo;
				pdk_b.ChiTietPdks.Add(new ChiTietPdk
				{
					MaMh=b.MaMh,
					MaPdk=pdk.MaPdk,
					Trangthai=b.Trangthai
				});
			}

			listCTPDK.ItemsSource = null;
			listCTPDK.ItemsSource = pdk_b.ChiTietPdks;

		}

		private void btnXoa_Click(object sender, RoutedEventArgs e)
		{
			var listxoa = listCTPDK.SelectedItems;
			foreach(var a in listxoa)
			{
				ChiTietPdk b = a as ChiTietPdk;
				pdk_b.ChiTietPdks.Remove(b);
			}
			listCTPDK.ItemsSource = null;
			listCTPDK.ItemsSource = pdk_b.ChiTietPdks;
		}

		private void btnLuu_Click(object sender, RoutedEventArgs e)
		{
			if (pdk_b.ChiTietPdks.Count < 1)
			{
				MessageBox.Show("Danh sách môn học trong phiếu đăng ký đang rỗng ! Vui lòng kiểm tra lại");
				return;
			}
			pdk_b.MaPdk = pdk.MaPdk;
			pdk_b.MaSv = pdk.MaSv;
			pdk_b.MaCdk = pdk.MaCdk;
			pdk_b.Ngaylap = pdk.Ngaylap;
			bool result = XL_PhieuDangKy.PutPhieuDangKy(pdk_b);
			if(result==false)
			{
				MessageBox.Show("Cập nhật dữ liệu không thành công !", "Thông báo");
				return;
			}
			else
			{
				MessageBox.Show("Dữ liệu được chỉnh sửa thành công", "Thông báo");
				this.Close();
			}


		}
	}
}
