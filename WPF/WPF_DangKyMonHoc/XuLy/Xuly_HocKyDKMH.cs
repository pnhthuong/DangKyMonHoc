using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.Xuly
{
	class Xuly_HockyDKMH
	{
		private static HttpClient hc = new HttpClient();
		public static List<HocKyDkmh> getAllHocKyDKMH()
		{
			string url = @"https://localhost:44319/api/hocky_dkmh";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<HocKyDkmh>>();
			return list.Result.ToList();
		}
		public static bool PostThemHockyDKMH(HocKyDkmh hocky)
		{
			try
			{
				string url = @"https://localhost:44319/api/hocky_dkmh";
				var kq = hc.PostAsJsonAsync(url, hocky);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool DeleteXoaHockyDKMH(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/hocky_dkmh/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool PutSuaThongTinHocKyDKMH(HocKyDkmh hocky)
		{
			try
			{
				string url = @"https://localhost:44319/api/hocky_dkmh";
				var kq = hc.PutAsJsonAsync(url, hocky);
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

