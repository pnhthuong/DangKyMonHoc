using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.Xuly
{
	class Xuly_HockyCTDT
	{
		private static HttpClient hc = new HttpClient();
		public static List<HocKyCtdt> getAllHocKyCTDT()
		{
			string url = @"https://localhost:44319/api/hocky_ctdt";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<HocKyCtdt>>();
			return list.Result.ToList();
		}
		public static bool PostThemHockyCTDT(HocKyCtdt hocky)
		{
			try
			{
				string url = @"https://localhost:44319/api/hocky_ctdt";
				var kq = hc.PostAsJsonAsync(url, hocky);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool DeleteXoaHockyCTDT(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/hocky_ctdt/"+id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch(Exception)
			{
				return false;
			}
		}
		public static bool PutSuaThongTinHocKyCTDT(HocKyCtdt hocky)
		{
			try
			{
				string url = @"https://localhost:44319/api/hocky_ctdt" ;
				var kq = hc.PutAsJsonAsync(url,hocky);
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
