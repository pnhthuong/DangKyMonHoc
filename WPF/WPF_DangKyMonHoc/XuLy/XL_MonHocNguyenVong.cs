using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XL_MonHocNguyenVong
    {
        private static HttpClient hc = new HttpClient();
        public static List<MonHocNguyenVong> getdsmhnv(string macdk)
        {
            try
            {
                string url = @"https://localhost:44319/api/monhocnguyenvong/"+macdk;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<MonHocNguyenVong>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static bool PostDSMonHocNguyenVong(List<MonHocNguyenVong> a)
        {
            try
            {
                string url = @"https://localhost:44319/api/monhocnguyenvong/DSMonHocNguyenVong";

                var kq = hc.PostAsJsonAsync(url, a);
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
