using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLGiangVien
    {
		private static HttpClient hc = new HttpClient();
		private static Xuly_Chung xlc = new Xuly_Chung();
		public static List<GiangVien> getAll()
		{
			string url = xlc.local() + "GiangVien";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<GiangVien>>();
			return list.Result.ToList();
		}
		public static GiangVien PostThemGiangVien(GiangVien a)
		{
			try
			{
				string url = xlc.local() + "GiangVien";
				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				var ok = kq.Result.Content;
				if (kq.Result.IsSuccessStatusCode)
				{
					var list = ok.ReadAsAsync<GiangVien>();
					return list.Result;
				}

				return null;

			}
			catch (Exception)
			{
				return null;
			}
		}
		public static bool DeleteXoaGiangVien(string id)
		{
			try
			{
				string url = xlc.local() + "GiangVien/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool PutSuaTTGiangVien(GiangVien a)
		{
			try
			{
				string url = xlc.local() + "GiangVien/" + a.MaGv;
				var kq = hc.PutAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;

			}
		}

		public static bool PutSuaPasswordGiangVien(GiangVien a)
		{
			try
			{
				string url = xlc.local() + "GiangVien/Password/" + a.MaGv;
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
