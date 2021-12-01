using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
	class Xuly_MonHocNguyenVong
	{
		private static HttpClient hc = new HttpClient();
		private static Xuly_Chung xlc = new Xuly_Chung();
		public static List<CongDangKy> getAll()
		{
			string url = xlc.local() + "CongDangKy";
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<CongDangKy>>();
			return list.Result.ToList();
		}
		public static bool PostThemCongDangKy(CongDangKy a)
		{
			try
			{
				string url = xlc.local() + "CongDangKy";

				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch(Exception)
			{
				return false;
			}
		


		}
		public static bool DeleteXoaCongDangKy(string id)
		{
			try
			{

				string url = xlc.local() + "CongDangKy/" + id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}catch(Exception)
			{
				return false;
			}
		}
		public static bool PutSuaCongDangKy(CongDangKy a)
		{
			try
			{
				string url = xlc.local() + "CongDangKy";
				var kq = hc.PutAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}catch(Exception)
			{
				return false;
			}
		}

		public static List<MonHoc> getsearchmonhoc(string maNK, string hocky_ctdt)
        {
			string url = xlc.local() + "MonHocDuocMo/search/" + maNK.Trim()+"/"+hocky_ctdt.Trim();
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<MonHoc>>();
			return list.Result.ToList();
		}

		public static bool PostMonHocDuocMo(List<MonHocDuocMo> a)
		{
			try
			{
				string url = xlc.local() + "MonHocDuocMo";

				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}

		public static List<MonHocDuocMo> getAllmonhocduocmo(string id)
		{
			string url = xlc.local() + "MonHocDuocMo/"+id;
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<MonHocDuocMo>>();
			return list.Result.ToList();
		}
		public static List<NienKhoaCdk> getDSCDKNK(string maCDK)
		{
			string url = xlc.local() + "MonHocDuocMo/getdsCDKNK/" + maCDK.Trim() ;
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<NienKhoaCdk>>();
			return list.Result.ToList();
		}

		public static bool PostDSNKCDK(List<NienKhoaCdk> a)
		{
			try
			{
				string url = xlc.local() + "MonHocDuocMo/postdsCDKNK";

				var kq = hc.PostAsJsonAsync(url, a);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;

			}
			catch (Exception)
			{
				return false;
			}
		}
		//@https://localhost:44319/api/NienKhoa/NKCDK/c1
		public static List<NienKhoa> getDSNKvsNKCDK(string maCDK)
		{
			string url = xlc.local() + "NienKhoa/NKCDK/" + maCDK.Trim();
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false)
				return null;
			var ok = kq.Result.Content;
			var list = ok.ReadAsAsync<List<NienKhoa>>();
			return list.Result.ToList();
		}
	}
}
