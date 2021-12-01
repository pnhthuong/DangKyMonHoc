using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.Xuly
{
	class Xuly_NamHocDKMH
	{
		private static HttpClient hc = new HttpClient();
		public static List<NamHocDkmh> getAllNamHocDkmh()
		{
			string url = @"https://localhost:44319/api/namhocdkmh";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<NamHocDkmh>>();
			return list.Result.ToList();
		}
		public static bool PostThemNamHocDkmh(NamHocDkmh namhoc)
		{
			try
			{
				string url = @"https://localhost:44319/api/namhocdkmh";
				var kq = hc.PostAsJsonAsync(url, namhoc);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool DeleteXoaNamHocDkmh(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/namhocdkmh/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool PutSuaThongTinNamHocDkmh(NamHocDkmh namhoc)
		{
			try
			{
				string url = @"https://localhost:44319/api/namhocdkmh/"+namhoc.MaNh;
				var kq = hc.PutAsJsonAsync(url, namhoc);
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
