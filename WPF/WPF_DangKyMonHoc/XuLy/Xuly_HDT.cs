using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.Xuly
{
	class Xuly_HDT
	{
		private static HttpClient hc = new HttpClient();
		public static List<HeDaoTao> getAllHeDaoTao()
		{
			string url = @"https://localhost:44319/api/hedaotao";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<HeDaoTao>>();
			return list.Result.ToList();
		}
		public static HeDaoTao getctHDT(string id)
		{
			string url = @"https://localhost:44319/api/hedaotao/"+id;
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<HeDaoTao>();
			return list.Result;
		}
		public static bool PostThemHeDaoTao(HeDaoTao hedaotao)
		{
			try
			{
				string url = @"https://localhost:44319/api/hedaotao";
				var kq = hc.PostAsJsonAsync(url, hedaotao);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool DeleteXoaHeDaoTao(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/hedaotao/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool PutSuaThongTinHeDaoTao(HeDaoTao hedaotao)
		{
			try
			{
				string url = @"https://localhost:44319/api/hedaotao/"+hedaotao.MaDt;
				var kq = hc.PutAsJsonAsync(url, hedaotao);
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
