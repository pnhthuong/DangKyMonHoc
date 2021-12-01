using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XL_PhieuDangKy
    {
        private static HttpClient hc = new HttpClient();
        public static List<PhieuDangKy_Custom> getdspdk(string macdk)
        {
            try
            {
                string url = @"https://localhost:44319/api/phieudangky/" + macdk;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<PhieuDangKy_Custom>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static List<ChiTietPdk> GetChiTietPdks(int mapdk)
		{
			
                string url = @"https://localhost:44319/api/phieudangky/chitietpdk/" + mapdk;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false)
                    return null;
                var list = kq.Result.Content.ReadAsAsync<List<ChiTietPdk>>();
                list.Wait();
                return list.Result.ToList();


			
		}
        public static bool PutPhieuDangKy(PhieuDangKy pdk)
		{
            try
            {
                string url = @"https://localhost:44319/api/phieudangky/putPDKSV";
                var kq = hc.PutAsJsonAsync(url, pdk);
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
