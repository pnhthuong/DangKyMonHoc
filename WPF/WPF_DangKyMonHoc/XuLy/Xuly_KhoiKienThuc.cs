using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.Xuly
{
	class Xuly_KhoiKienThuc
	{
		private static HttpClient hc = new HttpClient();
		public static List<KhoiKienThuc> getAllKhoiKienThuc()
		{
			string url = @"https://localhost:44319/api/Khoikienthuc";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<KhoiKienThuc>>();
			return list.Result.ToList();
		}
		public static bool PostThemKhoiKienThuc(KhoiKienThuc KhoiKienThuc)
		{
			try
			{
				string url = @"https://localhost:44319/api/Khoikienthuc";
				var kq = hc.PostAsJsonAsync(url, KhoiKienThuc);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool DeleteXoaKhoiKienThuc(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/Khoikienthuc/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool PutSuaThongTinKhoiKienThuc(KhoiKienThuc KhoiKienThuc)
		{
			try
			{
				string url = @"https://localhost:44319/api/KhoiKienThuc/" ;
				var kq = hc.PutAsJsonAsync(url, KhoiKienThuc);
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
