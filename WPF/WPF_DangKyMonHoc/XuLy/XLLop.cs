using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLLop
    {
		private static HttpClient hc = new HttpClient();
		public static List<Lop> get()
		{
			Xuly_Chung xlc = new Xuly_Chung();
			string url = xlc.local()+"lop";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<Lop>>();
			return list.Result.ToList();
		}

		public static bool Post(Lop a)
		{
			try
			{
				string url = @"https://localhost:44319/api/lop";
				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool Delete(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/lop/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool Put(Lop a)
		{
			try
			{
				string url = @"https://localhost:44319/api/lop/"+a.MaLop;
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
