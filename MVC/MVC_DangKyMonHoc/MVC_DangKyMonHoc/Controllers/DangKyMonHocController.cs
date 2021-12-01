using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_DangKyMonHoc.Helper;
using MVC_DangKyMonHoc.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace MVC_DangKyMonHoc.Controllers
{
	public class DangKyMonHocController : Controller
	{
		APIHelper api = new APIHelper();
		
		public List<MonHocDuocMoCustom> getlisttempMHDM()
        {
			return SessionHelper.getObject<List<MonHocDuocMoCustom>>(HttpContext.Session, "listtempMHDM");
        }
		public void setlisttempMHDM(List<MonHocDuocMoCustom> list)
        {
			if (list.Count < 1) return;
			SessionHelper.setObject(HttpContext.Session, "listtempMHDM", list);
        }
		public async Task<IActionResult> Index()
		{
			
			List<MonHocDuocMoCustom> monhoc = new List<MonHocDuocMoCustom>();
			List<MonHocDuocMoCustom> listdkmhsv = new List<MonHocDuocMoCustom>();
			List<ChiTietPdk> pdksinhvien = new List<ChiTietPdk>();
			HttpClient client = api.Intial();
			SinhVien sinhvien = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login") ;
            if (sinhvien == null)
            {
				return View("../Home/Login");
            }
			string masv = sinhvien.MaSv;
			HttpResponseMessage res = await client.GetAsync("/api/MonHoc/DSMonHoc/DuocMo/"+masv);
			if (res.IsSuccessStatusCode)
			{
				var result = res.Content.ReadAsStringAsync().Result;
				monhoc = JsonConvert.DeserializeObject<List<MonHocDuocMoCustom>>(result);
				SessionHelper.setObject(HttpContext.Session, "monhocduocmo", monhoc);
				setlisttempMHDM(monhoc);
			}
            else
            {
				ViewBag.error = "Hiện chưa phải là thời gian đăng ký môn học";
				return View();
            }

			HttpResponseMessage ress = await client.GetAsync("/api/PhieuDangKy/pdksinhvien/chitiet/" + masv+"/"+monhoc[0].MaCdk);
			if (ress.IsSuccessStatusCode)
			{
				var kq = ress.Content.ReadAsStringAsync().Result;
				pdksinhvien = JsonConvert.DeserializeObject<List<ChiTietPdk>>(kq);
				SessionHelper.setObject(HttpContext.Session, "phieudangkysinhvien", pdksinhvien);
			}
			HttpResponseMessage resss = await client.GetAsync("/api/phieudangky/PDKSV/chitiet/sinhvien/" + masv + "/" + monhoc[0].MaCdk);
			if (resss.IsSuccessStatusCode)
			{
				var ketqua = resss.Content.ReadAsStringAsync().Result;
				listdkmhsv = JsonConvert.DeserializeObject<List<MonHocDuocMoCustom>>(ketqua);
                if (listdkmhsv != null)
                {
					List<MonHocDuocMoCustom> listtemp = getlisttempMHDM();
					foreach (var a in listdkmhsv)
					{
						if (listtemp.Find(x => x.MaMh == a.MaMh) == null)
						{
							listtemp.Add(a);
						}
					}
					setlisttempMHDM(listtemp);
				}
			}
			if (listdkmhsv != null)
            {
				foreach (var a in monhoc)
				{
					var b = listdkmhsv.Find(x => x.MaMh == a.MaMh);
					if (b == null)
					{
						a.dadangky = false;
						continue;
					}
                    else
                    {
						a.dadangky = true;
						listdkmhsv.Remove(b);
					}
				}
                if (listdkmhsv.Count > 0)
                {
					foreach(var a in listdkmhsv)
                    {
                        if (a.Trangthai == false)
                        {
							a.chuthich = "NV";
                        }
						a.dadangky = true;
						monhoc.Add(a);
                    }
                }
			}
						
			return View(monhoc);
		}

		[HttpPost]
		public async Task<IActionResult> DKMH()
        {
			List<ChiTietPdk> listCTPDK = SessionHelper.getObject<List<ChiTietPdk>>(HttpContext.Session, "phieudangkysinhvien");
			List<MonHocDuocMoCustom> listMHDuocMo = SessionHelper.getObject<List<MonHocDuocMoCustom>>(HttpContext.Session, "monhocduocmo");
			List<MonHocDuocMoCustom> temp = new List<MonHocDuocMoCustom>();
			List<MonHocDuocMoCustom> listtemp = getlisttempMHDM(); 
			foreach(var a in listtemp)
            {
				if (Request.Form[a.MaMh] == "true"||Request.Form[a.MaMh]=="false")
				{
					if(Request.Form[a.MaMh] == "true")
                    {
						a.Trangthai = true;
                    }
                    else
                    {
						a.Trangthai = false;
					}
					temp.Add(a);
				}
			}
            if (listCTPDK == null)
            {
				SinhVien sinhvien = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
				PhieuDangKy pdk = new PhieuDangKy();
				pdk.MaCdk = listMHDuocMo[0].MaCdk;
				pdk.MaSv = sinhvien.MaSv;
				pdk.Ngaylap = DateTime.Now;
				foreach(var a in temp)
                {
					ChiTietPdk ct = new ChiTietPdk();
					ct.MaMh = a.MaMh;
					ct.MaPdk = pdk.MaPdk;
					ct.Trangthai = a.Trangthai;

					pdk.ChiTietPdks.Add(ct);
                }
				HttpClient client = api.Intial();
                HttpContent httpcontent = new StringContent(JsonConvert.SerializeObject(pdk), Encoding.UTF8, "application/json");

                HttpResponseMessage respone = await client.PostAsync("/api/PhieuDangKy/postnewPDK", httpcontent);

                if (respone.IsSuccessStatusCode)
                {
					ViewBag.note = "Đăng ký thành công";
					return View("../Home/Index");
				}
                else
                {
                    ViewBag.note = "Thực hiện việc đăng ký môn học thất bại !";
                    return View("../Home/Index");
				}
            }

			HttpClient client1 = api.Intial();
			SinhVien sinhvien1 = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
			string masv1 = sinhvien1.MaSv;
			HttpResponseMessage ress = await client1.GetAsync("/api/PhieuDangKy/PDKSV/" + masv1+"/"+ listMHDuocMo[0].MaCdk);
			var result1 = ress.Content.ReadAsStringAsync().Result;
			PhieuDangKy pdksv = JsonConvert.DeserializeObject<PhieuDangKy>(result1);

			if(pdksv.Ngaychinhsua.GetValueOrDefault()==null)
            {
				pdksv.Ngaylap = pdksv.Ngaychinhsua.GetValueOrDefault();
				pdksv.Ngaychinhsua = DateTime.Now;
			}
			pdksv.Ngaychinhsua = DateTime.Now;
			foreach(var mh in temp)
            {
				pdksv.ChiTietPdks.Add(new ChiTietPdk
				{
					MaPdk = pdksv.MaPdk,
					MaMh = mh.MaMh,
					Trangthai = mh.Trangthai
				});
            }
			HttpContent httpcontent1 = new StringContent(JsonConvert.SerializeObject(pdksv), Encoding.UTF8, "application/json");

			HttpResponseMessage respone1 = await client1.PutAsync("/api/PhieuDangKy/postnewPDK", httpcontent1);

			if (respone1.IsSuccessStatusCode)
			{
				ViewBag.note = "Chỉnh sửa thành công";
				return View("../Home/Index");
			}
			ViewBag.note = "Thực hiện việc chỉnh sửa môn học thất bại !";
			return View("../Home/Index");

		}
        public async Task<String> TKMonHoc(string id)
        {
            MonHocDuocMoCustom monhoc = new MonHocDuocMoCustom();
            HttpClient client = api.Intial();
            HttpResponseMessage res = await client.GetAsync("/api/DangKyMH/TimKiem/" + id);
			List<MonHocDuocMoCustom> listtemp = getlisttempMHDM();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                monhoc = JsonConvert.DeserializeObject<MonHocDuocMoCustom>(result);
				if (monhoc == null) return null;
				listtemp.Add(monhoc);
				setlisttempMHDM(listtemp);
                //SessionHelper.SetObjectAsJson(HttpContext.Session, "monhoc", monhoc);

            }
			monhoc.chuthich = "NV";
           
            return JsonConvert.SerializeObject(monhoc);
        }

        public async Task<String> TKMonHocDuocMo(string id)
        {
            MonHocDuocMoCustom monhoc = new MonHocDuocMoCustom();
			List<MonHocDuocMoCustom> listtemp = getlisttempMHDM();
			HttpClient client = api.Intial();
            SinhVien sv = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
            List<MonHocDuocMoCustom> monHocDuocMo = SessionHelper.getObject<List<MonHocDuocMoCustom>>(HttpContext.Session, "monhocduocmo");
            var checkfrist=monHocDuocMo.Find(x => x.MaMh.Trim() == id);
            if(checkfrist != null)
            {
				Thongbao tt = new Thongbao();  tt.TrangthaiTT = true; tt.Noidung = "Môn Học Đã Có Trong Danh Sách" ;
                return JsonConvert.SerializeObject(tt);
			} 

            HttpResponseMessage res = await client.GetAsync("/api/DangKyMH/TimKiem/monhocduocmo/" + id+"/"+monHocDuocMo[0].MaCdk+"/"+sv.MaSv);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                monhoc = JsonConvert.DeserializeObject<MonHocDuocMoCustom>(result);
				listtemp.Add(monhoc);
				setlisttempMHDM(listtemp);
				//SessionHelper.SetObjectAsJson(HttpContext.Session, "monhoc", monhoc);

			}
            else
            {
				if (res.StatusCode.ToString() == "404")
				{
					Thongbao tt = new Thongbao(); tt.TrangthaiTT = true; tt.Noidung = "Không Tìm Thấy Môn Học";
					return JsonConvert.SerializeObject(tt);
				}
				else
				{
					var abc = res.Content.ReadAsStringAsync().Result;
					Thongbao tt = new Thongbao(); tt.TrangthaiTT = true; tt.Noidung = abc;
					return JsonConvert.SerializeObject(tt);
				}
			}
            
			return JsonConvert.SerializeObject(monhoc);
        }

        public async Task<string> KiemTraSongHanh([FromBody]List<string> l)
        {
			List<MonHocDuocMoCustom> list = SessionHelper.getObject<List<MonHocDuocMoCustom>>(HttpContext.Session, "monhocduocmo");
			List<Thongbao> list_ThongBao = new List<Thongbao>();
			foreach (var a in l)
            {
				var check_mh =  list.Find(x => x.MaMh.Trim() == a);
				if (check_mh == null||check_mh.MaSH==null) continue;
				var check_MHSH = l.Find(x => x== check_mh.MaSH.Trim());
                if (check_MHSH == null)
                {
					list_ThongBao.Add(new Thongbao { TrangthaiTT = true, Noidung = "Môn Học " + check_mh.MaMh + " có môn học song hành là: " + check_mh.MaSH + ". Bạn vui lòng kiểm tra lại đăng ký." });
                }
            }
            if (list_ThongBao.Count > 0)
            {
				return JsonConvert.SerializeObject(list_ThongBao);
			}
			return JsonConvert.SerializeObject(new Thongbao { TrangthaiTT=false, Noidung="Chính Xác"}); 
        }

	}
}
