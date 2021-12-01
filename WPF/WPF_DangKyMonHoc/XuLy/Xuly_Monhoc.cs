using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
	class Xuly_Monhoc
	{
		private static HttpClient hc = new HttpClient();
		private static Xuly_Chung xlc = new Xuly_Chung();
		public static List<MonHoc> getAllMonHoc()
		{
			string url = xlc.local() + "MonHoc";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<MonHoc>>();
			return list.Result.ToList();
		}
		public static bool PostMonHoc(MonHoc a)
		{
			try
			{
				string url = xlc.local() + "MonHoc";
				var kq = hc.PostAsJsonAsync(url, a);
				return kq.Result.IsSuccessStatusCode;

			}
			catch(Exception)
			{
				return false;
			}
			
		}
		public static bool DeleteMonhoc(string id)
		{
			try
			{
				string url = xlc.local() + "Monhoc/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}catch(Exception)
			{
				return false;
			}
		}
		public static bool PutSuaMonHoc(MonHoc a)
		{
			try
			{
				string url = xlc.local() + "MonHoc/";
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
