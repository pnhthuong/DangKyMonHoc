using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLSinhVien
    {
		private static HttpClient hc = new HttpClient();
		private static Xuly_Chung xlc = new Xuly_Chung();
		public static List<SinhVien> getAll()
		{
			string url = xlc.local()+"SinhVien";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<SinhVien>>();
			return list.Result.ToList();
		}
		public static SinhVien PostThemSinhVien(SinhVien a)
		{
			try
			{
				string url = xlc.local()+"SinhVien";
				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				var ok = kq.Result.Content;
                if (kq.Result.IsSuccessStatusCode)
                {
					var list = ok.ReadAsAsync<SinhVien>();
					return list.Result;
				}

				return null;

			}
			catch (Exception)
			{
				return null;
			}
		}
		public static bool DeleteXoaSinhVien(string id)
		{
			try
			{
				string url = xlc.local()+"SinhVien/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool PutSuaTTSinhVien(SinhVien a)
		{
			try
			{
				string url = xlc.local()+"SinhVien/"+a.MaSv;
				var kq = hc.PutAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;

			}
		}

		public static bool PutSuaPassword(SinhVien a)
		{
			try
			{
				string url = xlc.local() + "SinhVien/Password/" + a.MaSv;
				var kq = hc.PutAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;

			}
		}
	}
}
