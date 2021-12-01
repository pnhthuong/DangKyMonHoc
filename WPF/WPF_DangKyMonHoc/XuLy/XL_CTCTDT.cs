using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XL_CTCTDT
    {
        private static HttpClient hc = new HttpClient();
        public static List<ChiTietCtdt> getdsCTCTDT(string id)
        {
            try
            {
                string url = @"https://localhost:44319/api/CTCTDT/"+id;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<ChiTietCtdt>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static bool postlistctctdt(List<ChiTietCtdt> ls)
        {
            try
            {
                string url = @"https://localhost:44319/api/CTCTDT";
                var kq = hc.PostAsJsonAsync(url, ls);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool putCTCTDT(List<ChiTietCtdt> cv)
        {
            try
            {
                string url = @"https://localhost:44319/api/ctctdt/";
                var kq = hc.PutAsJsonAsync(url, cv);
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
