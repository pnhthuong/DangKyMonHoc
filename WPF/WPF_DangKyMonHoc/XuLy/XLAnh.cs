using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLAnh
    {
		private static HttpClient hc = new HttpClient();
		
		public static byte[] gethinh(string ma)
		{
			string url = @"https://localhost:44319/api/FileUploads/" + ma;
			var kq = hc.GetAsync(url);
			kq.Wait();
			if (kq.Result.IsSuccessStatusCode == false) return null;
			var list = kq.Result.Content.ReadAsAsync<byte[]>();
			list.Wait();
			return list.Result;
		}

		public static bool posthinh(FileUpload x)
        {
            try
            {
				string url = @"https://localhost:44319/api/FileUploads";
				var kq = hc.PostAsJsonAsync<FileUpload>(url,x);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
            catch (Exception)
            {
				return false;
            }
        }

		public static bool deletehinh(string id)
		{
			try
			{
				string url = @"https://localhost:44319/api/FileUploads/"+id;
				var kq = hc.DeleteAsync(url);
				kq.Wait();
				return kq.Result.IsSuccessStatusCode;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool puthinh(string masv,FileUpload x)
		{
			try
			{
				string url = @"https://localhost:44319/api/FileUploads/"+masv;
				var kq = hc.PutAsJsonAsync<FileUpload>(url, x);
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
