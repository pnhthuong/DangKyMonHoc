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
    public class MonHocNguyenVongController : ControllerBase
    {
        private DangKyMonHocContext _db = new DangKyMonHocContext();

        [HttpGet("{macdk}")]
        public IActionResult getdsmhNguyenVong(string macdk)
        {
            var list = _db.PhieuDangKies.Where(x => x.MaCdk == macdk).ToList();
            List<MonHocNguyenVong> listmhnv = new List<MonHocNguyenVong>();
            foreach(var a in list)
            {
                List<ChiTietPdk> listctpdk = _db.ChiTietPdks.Where(x => x.MaPdk == a.MaPdk && x.Trangthai == false).ToList();
                foreach(var b in listctpdk)
                {
                    var mhnv = listmhnv.Find(x => x.maMH == b.MaMh);
                    var mh = _db.MonHocs.Find(b.MaMh);
                    if (mhnv == null)
                    {
                        listmhnv.Add(new MonHocNguyenVong { maCDK = a.MaCdk, maMH = b.MaMh, tenMH = mh.TenMh, soluong = 1, TrangThai = false });
                    }
                    else
                    {
                        mhnv.soluong++;
                    }
                }
            }
            return Ok(listmhnv);
        }
        [HttpPost("{DSMonHocNguyenVong}")]
        public IActionResult postMonhocNV([FromBody] List<MonHocNguyenVong> list)
        {
            if (list == null) return BadRequest();
            foreach(var a in list)
            {
                MonHocDuocMo mh = new MonHocDuocMo
                {
                    MaCdk = a.maCDK,
                    MaMh = a.maMH,
                    Soluong = a.soluong,
                    Trangthai = false,
                    NkapDung = "NV"
                };
                _db.MonHocDuocMos.Add(mh);
                //var listctpdk = _db.PhieuDangKies.Join(_db.ChiTietPdks, pdk => pdk.MaPdk, ctpdk => ctpdk.MaPdk, (pdk, ctpdk) => new ChiTietPdk
                //{
                //    Id = ctpdk.Id,
                //    MaMh = ctpdk.MaMh,
                //    MaPdk = ctpdk.MaPdk,
                //    Trangthai = ctpdk.Trangthai
                //}).Where(x => x.Trangthai == false||x.MaPdkNavigation.MaCdk==a.maCDK).ToList();
                var listctpdk = _db.ChiTietPdks.Where(x => x.MaPdkNavigation.MaCdk == a.maCDK && x.Trangthai == false&&x.MaMh==a.maMH).ToList();
                foreach(var ct in listctpdk)
                {
                    ct.Trangthai = true;
                }    
            }


            _db.SaveChanges();
            return Ok();
        }
    }
}
