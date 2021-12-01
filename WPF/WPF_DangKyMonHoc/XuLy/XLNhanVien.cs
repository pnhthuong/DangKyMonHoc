using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLNhanVien
    {
		private static HttpClient hc = new HttpClient();
		private static Xuly_Chung xlc = new Xuly_Chung();
		public static List<NhanVien> getAll()
		{
			string url = xlc.local() + "NhanVien";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<NhanVien>>();
			return list.Result.ToList();
		}
		public static NhanVien PostThemNhanVien(NhanVien a)
		{
			try
			{
				string url = xlc.local() + "NhanVien";
				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				var ok = kq.Result.Content;
				if (kq.Result.IsSuccessStatusCode)
				{
					var list = ok.ReadAsAsync<NhanVien>();
					return list.Result;
				}

				return null;

			}
			catch (Exception)
			{
				return null;
			}
		}
		public static bool DeleteXoaNhanVien(string id)
		{
			try
			{
				string url = xlc.local() + "NhanVien/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool PutSuaTTNhanVien(NhanVien a)
		{
			try
			{
				string url = xlc.local() + "NhanVien/" + a.MaNv;
				var kq = hc.PutAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;

			}
		}

		public static bool PutSuaPasswordNhanVien(NhanVien a)
		{
			try
			{
				string url = xlc.local() + "NhanVien/Password/" + a.MaNv;
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
