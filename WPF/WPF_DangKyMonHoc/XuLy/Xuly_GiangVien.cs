using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.Xuly
{
	class Xuly_GiangVien
	{
		private static HttpClient hc = new HttpClient();
		public static List<GiangVien> getAllGV()
		{
			string url = @"https://localhost:44319/api/giangvien";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<GiangVien>>();
			return list.Result.ToList();
		}
		public static bool PostThemGiangVien(GiangVien a)
		{
			try
			{
				string url = @"https://localhost:44319/api/giangvien";
				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch(Exception)
			{
				return false;
			}
		}
		public static bool DeleteXoaGiangVien(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/giangvien/"+id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch(Exception)
			{
				return false;
			}
		}
		public static bool PutSuaTTGiangVien(GiangVien a)
		{
			try
			{
				string url = @"https://localhost:44319/api/giangvien";
				var kq = hc.PutAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch(Exception)
			{
				return false;	

			}
		}
	}
}
