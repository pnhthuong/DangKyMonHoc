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
	/// Interaction logic for Page_QLChuongTrinhDaoTao.xaml
	/// </summary>
	public partial class Page_QLChuongTrinhDaoTao 
	{
		private Xuly_Chung xlc = new Xuly_Chung();
		private List<Nganh> listnganh = new List<Nganh>();
		private List<HeDaoTao> listhdh = new List<HeDaoTao>();
		public Page_QLChuongTrinhDaoTao()
		{
			InitializeComponent();
			getload();
		}

        private void textnumberinput(object sender, TextCompositionEventArgs e)
        {
			xlc.textNumber(e);
        }
		public void clean()
        {
			txt_ma.Text = null;
			txt_ten.Text = null;
			txt_nienkhoa.Text = null;
			txt_tinchi.Text = null;
			cmb_hedaotao.SelectedValue = null;
			cmb_nganh.SelectedValue = null;
			txt_ma.IsReadOnly = false;
			list_ctdt.SelectedItem = null;
        }
		public void getload()
        {
			listhdh = Xuly_HDT.getAllHeDaoTao();
			if(listhdh==null) { MessageBox.Show("Không thể truyền tải dữ liệu Hệ Đào Tạo!", "Thông Báo");return; }
			cmb_hedaotao.ItemsSource = listhdh;

			listnganh = XLNganh.getds();
			if (listnganh == null) { MessageBox.Show("Không thể truyền tải dữ liệu Ngành!", "Thông Báo"); return; }
			cmb_nganh.ItemsSource = listnganh;

			List<ChuongTrinhDaoTao> listctdt = XL_CTDT.getdsCTDT();
            if (listctdt == null) { MessageBox.Show("Không thể truyền tải dữ liệu Chương trình đào tạo!", "Thông báo");return; }
			list_ctdt.ItemsSource = listctdt;
		}

        private void btn_them(object sender, RoutedEventArgs e)
        {
			if(txt_ma.Text==""||txt_ten.Text==""||txt_nienkhoa.Text==""||txt_tinchi.Text==""||int.Parse(txt_tinchi.Text)<1||cmb_hedaotao.SelectedItem==null||cmb_nganh.SelectedItem==null)
            {
				MessageBox.Show("Bạn chưa nhập đầy đủ dữ liệu!", "Thông báo");
				return;
            }
			ChuongTrinhDaoTao ct = new ChuongTrinhDaoTao();
			ct.MaCtdt = txt_ma.Text;
			ct.TenCtdt = txt_ten.Text;
			ct.NienKhoa = txt_nienkhoa.Text;
			ct.TongSoTinChi = int.Parse(txt_tinchi.Text);
			ct.MaDt = cmb_hedaotao.SelectedValue.ToString();
			ct.MaNganh = cmb_nganh.SelectedValue.ToString();

			bool result = XL_CTDT.post(ct);
			if(result==false)
            {
				MessageBox.Show("Không thể thêm chương trình đào tạo! Vui lòng kiểm tra lại dữ liệu.", "Thông báo");
				return;
            }
			MessageBox.Show("Thêm dữ liệu thành công", "Thông báo");
			clean();
			getload();

			
			var newwindow = new CTCTDT_Window(ct);
			newwindow.ShowDialog();
			
		}

        private void btn_sua(object sender, RoutedEventArgs e)
        {
			if (txt_ma.Text == "" || txt_ten.Text == "" || txt_nienkhoa.Text == "" || txt_tinchi.Text == "" || cmb_hedaotao.SelectedItem == null || cmb_nganh.SelectedItem == null)
			{
				MessageBox.Show("Bạn chưa nhập đầy đủ dữ liệu!", "Thông báo");
				return;
			}
			ChuongTrinhDaoTao ct = list_ctdt.SelectedItem as ChuongTrinhDaoTao;
			ct.TenCtdt = txt_ten.Text;
			ct.TongSoTinChi = int.Parse(txt_tinchi.Text);

			bool result = XL_CTDT.put(ct);
			if (result == false)
			{
				MessageBox.Show("Không thể Chỉnh sửa chương trình đào tạo! Vui lòng kiểm tra lại dữ liệu.", "Thông báo");
				return;
			}
			MessageBox.Show("Chỉnh sửa dữ liệu thành công", "Thông báo");
			clean();
			getload();
			return;
		}

        private void btn_xoa(object sender, RoutedEventArgs e)
        {
			ChuongTrinhDaoTao ct = list_ctdt.SelectedItem as ChuongTrinhDaoTao;
			if (ct == null) return;
			MessageBoxResult result = MessageBox.Show("Bạn có chắn chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo);
			switch (result)
			{
				case MessageBoxResult.No:
					break;
				case MessageBoxResult.Yes:
                    bool resultserver = XL_CTDT.delete(ct.MaCtdt);
                    if (resultserver == false)
                    {
                        MessageBox.Show("Không thể Xóa chương trình đào tạo! Vui lòng kiểm tra lại dữ liệu.", "Thông báo");
                        return;
                    }
                    MessageBox.Show("Xóa dữ liệu thành công", "Thông báo");
                    clean();
                    getload();
                    break;
			}
		}

        private void list_ctdt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var ct = list_ctdt.SelectedItem as ChuongTrinhDaoTao;
			if (ct == null) return;
			txt_ma.Text = ct.MaCtdt;
			txt_ten.Text = ct.TenCtdt;
			txt_nienkhoa.Text = ct.NienKhoa;
			txt_tinchi.Text = ct.TongSoTinChi.ToString();
			cmb_hedaotao.SelectedValue = ct.MaDt;
			cmb_nganh.SelectedValue = ct.MaNganh;

			txt_ma.IsReadOnly = true;
			txt_nienkhoa.IsReadOnly = true;
			cmb_nganh.IsReadOnly = true;
			cmb_hedaotao.IsReadOnly = true;
        }

        private void btn_detail(object sender, RoutedEventArgs e)
        {

			ChuongTrinhDaoTao ct = list_ctdt.SelectedItem as ChuongTrinhDaoTao;
            if (ct == null)
            {
				MessageBox.Show("Vui lòng chọn Chương trình đào tạo!", "Thông báo");return;
            }
			var n = new Window_CTCTDT_Chinhsua(ct);
			n.Show();

			clean();
        }
    }
}
