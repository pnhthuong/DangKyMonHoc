using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDangKyController : ControllerBase
    {
        private DangKyMonHocContext _db = new DangKyMonHocContext();
        [HttpGet("{macdk}")]
        public IActionResult getDSPDK(string macdk)
        {
            var list = _db.PhieuDangKies.Where(x => x.MaCdk == macdk).ToList();
            List<PhieuDangKy_Custom> listpdk = new List<PhieuDangKy_Custom>();

            foreach (var a in list)
            {
                var sv = _db.SinhViens.Find(a.MaSv);
                listpdk.Add(new PhieuDangKy_Custom
                {
                    MaPdk = a.MaPdk,
                    MaSv = a.MaSv,
                    TenSv = sv.TenSv,
                    Ngaylap = a.Ngaylap,
                    MaCdk = a.MaCdk
                });
            }
            return Ok(listpdk);
        }
        [HttpGet("{pdksinhvien}/{chitiet}/{masv}/{macdk}")]
        public IActionResult getpdksinhvien(string masv, string macdk)
        {
            if (masv == "" || macdk == "") return BadRequest();
            var resultsearch = _db.PhieuDangKies.Where(x => x.MaSv == masv && macdk == x.MaCdk).FirstOrDefault();
            if (resultsearch == null) return Ok(null);
            List<ChiTietPdk> chiTietPdks = _db.ChiTietPdks.Where(x => x.MaPdk == resultsearch.MaPdk).ToList();
            return Ok(chiTietPdks);

        }

        [HttpPost("{postnewPDK}")]
        public IActionResult postpdksinhvien(PhieuDangKy phieuDangKy)
        {
            _db.PhieuDangKies.Add(phieuDangKy);
            _db.SaveChanges();
            return Ok();
        }

        [HttpGet("{PDKSV}/{masv}/{macdk}")]
        public IActionResult getPDKSV(string masv, string macdk)
        {
            if (masv == "" || macdk == "") return BadRequest();
            var resultsearch = _db.PhieuDangKies.Where(x => x.MaSv == masv && macdk == x.MaCdk).FirstOrDefault();

            return Ok(resultsearch);
        }
        [HttpGet("{PDKSV}/{ChiTiet}/{SinhVien}/{masv}/{macdk}")]
        public IActionResult getCTPDKSV(string masv, string macdk)
        {
            if (masv == "" || macdk == "") return BadRequest();
            var resultsearch = _db.PhieuDangKies.Where(x => x.MaSv == masv && macdk == x.MaCdk).FirstOrDefault();
            List<MonHocDuocMoCustom> list = new List<MonHocDuocMoCustom>();
            List<ChiTietPdk> ctpdksinhvien = null;
            if (resultsearch != null)
            {
                ctpdksinhvien = _db.ChiTietPdks.Where(x => x.MaPdk == resultsearch.MaPdk).ToList();
            }

            if (ctpdksinhvien != null)
            {
                foreach (var a in ctpdksinhvien)
                {
                    MonHocDuocMo b = _db.MonHocDuocMos.Where(x => x.MaMh == a.MaMh && x.MaCdk == macdk).FirstOrDefault();
                    if (b != null)
                    {
                        MonHoc c = _db.MonHocs.Find(b.MaMh);
                        MonHocDuocMoCustom monHoc = new MonHocDuocMoCustom
                        {
                            MaCdk = macdk,
                            MaMh = b.MaMh,
                            hesohp = c.HesoHp,
                            sotinchi = c.Sotinchi,
                            Soluong = b.Soluong,
                            TenMh = c.TenMh,
                            Trangthai = a.Trangthai,
                        };
                        list.Add(monHoc);
                    }
                    else
                    {
                        MonHoc c = _db.MonHocs.Find(a.MaMh);
                        MonHocDuocMoCustom monHoc = new MonHocDuocMoCustom
                        {
                            MaCdk = macdk,
                            MaMh = a.MaMh,
                            hesohp = c.HesoHp,
                            sotinchi = c.Sotinchi,
                            Soluong = 0,
                            TenMh = c.TenMh,
                            Trangthai = a.Trangthai,
                        };
                        list.Add(monHoc);
                    }
                }
                return Ok(list);
            }
            return Ok();
        }
        [HttpPut("{putPDKSV}")]
        public IActionResult putPDKSV(PhieuDangKy pdk)
        {
            List<ChiTietPdk> list = _db.ChiTietPdks.Where(x => x.MaPdk == pdk.MaPdk).ToList();
            List<ChiTietPdk> listput = pdk.ChiTietPdks.ToList();
            var checkPDK = _db.PhieuDangKies.Where(x => x.MaPdk == pdk.MaPdk && x.MaSv == pdk.MaSv && x.MaCdk == pdk.MaCdk).First();
            if (checkPDK == null) return BadRequest();
            checkPDK.Ngaylap = pdk.Ngaylap;
            checkPDK.Ngaychinhsua = pdk.Ngaychinhsua;
            foreach (var a in listput)
            {
                var b = list.Find(x => x.MaMh == a.MaMh);
                if (b != null)
                {
                    list.Remove(b);
                    continue;
                }
                _db.ChiTietPdks.Add(a);
            }
            if (list.Count > 0)
            {
                foreach (var a in list)
                {
                    _db.ChiTietPdks.Remove(a);
                }
            }
            _db.SaveChanges();
            return Ok();
        }
        [HttpGet("{chitietpdk}/{id}")]
        public IActionResult getChitietpdk(int id)
        {
            var list = _db.ChiTietPdks.Where(x => x.MaPdk == id).ToList();
            if (list == null)
                return BadRequest();
            return Ok(list);
        }
    }
}
