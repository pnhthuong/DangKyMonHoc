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
    public class Lop_MonHocController : ControllerBase
    {
        private DangKyMonHocContext _db = new DangKyMonHocContext();
        [HttpPost("{LapDSLopMonHoc}")]
        public IActionResult postTaoDSLopMonHoc(Lop_Monhoc_Custom l)
        {
            var cus = l.MaCdk.Trim() + "_" + l.MaMh.Trim();
            var checkinital = _db.LopMonHocs.Where(x => x.MaLmh.StartsWith(cus)).ToList();
            if (checkinital.Count >0) return BadRequest("Môn Học đã được tạo");
            var list = _db.PhieuDangKies
                .Join(_db.ChiTietPdks,
                phieudangky => phieudangky.MaPdk,
                chitiet => chitiet.MaPdk,
                (phieudangky, chitiet) => new
                {
                    MaPDK = phieudangky.MaPdk,
                    MaMH = chitiet.MaMh,
                    MaSV = phieudangky.MaSv,
                    MaCDK = phieudangky.MaCdk
                }
                ).Where(x => x.MaCDK == l.MaCdk && x.MaMH == l.MaMh).ToList();
            if (list == null) return BadRequest("Hiện chưa có Sinh Viên đăng ký môn học này");
            int dem = 1;
            int sl = 0;
            int demslconlai = 0;
            if (list.Count > 0)
            {
                for (int i = l.Siso; i <= list.Count-1; i = i + l.Siso)
                {
                    LopMonHoc lop = new LopMonHoc();
                    lop.MaLmh = l.MaCdk.Trim() + "_" + l.MaMh.Trim() + "_" + dem;
                    MonHoc mh = _db.MonHocs.Find(l.MaMh);
                    lop.TenLmh = mh.TenMh+"_"+dem;
                    lop.MaMh = l.MaMh;
                    lop.MaCdk = l.MaCdk;
                    lop.Siso = l.Siso;

                    for (int j = sl; j <= i; j++)
                    {
                        if (list[j] != null)
                        {
                            var check=list[j];
                            lop.BangDiems.Add(new BangDiem
                            {
                                MaLmh = lop.MaLmh,
                                Masv = list[j].MaSV,
                                Trangthai = true
                            });
                        }
                        else
                        {
                            break;
                        }
                        sl = j+1;
                    }
                    demslconlai += i+1;
                    dem++;
                    _db.LopMonHocs.Add(lop);
                }
                if (list.Count - demslconlai > 0)
                {
                    LopMonHoc lop = new LopMonHoc();
                    lop.MaLmh = l.MaCdk.Trim() + "_" + l.MaMh.Trim() + "_" + dem;
                    MonHoc mh = _db.MonHocs.Find(l.MaMh);
                    lop.TenLmh = mh.TenMh + "_" + dem;
                    lop.MaMh = l.MaMh;
                    lop.MaCdk = l.MaCdk;
                    lop.Siso = l.Siso;
                    for (int j = demslconlai ; j < list.Count; j++)
                    {
                        if (list[j] != null)
                        {
                            lop.BangDiems.Add(new BangDiem
                            {
                                MaLmh = lop.MaLmh,
                                Masv = list[j].MaSV,
                                Trangthai = true
                            });
                        }
                        else
                        {
                            break;
                        }
                    }
                    _db.LopMonHocs.Add(lop);
                }
            }
            _db.SaveChanges();
            return Ok();
        }
        [HttpGet("{dsmhdm}/{macdk}")]
        public IActionResult getdsmhdm(string macdk)
        {
            var cdk = _db.CongDangKies.Find(macdk.Trim());
            if (cdk == null) return BadRequest("Không tìm thấy cổng đăng ký");
            var listdsmhdm = _db.MonHocDuocMos.Where(x => x.MaCdk == cdk.MaCdk).ToList();
            List<MonHocDuocMoCus> list = new List<MonHocDuocMoCus>();
            foreach(var a in listdsmhdm)
            {
                var b = _db.PhieuDangKies.Join(_db.ChiTietPdks, pdk => pdk.MaPdk, chitiet => chitiet.MaPdk, (pdk, chitiet) => new
                {
                    MaCDK = pdk.MaCdk,
                    Mamh = chitiet.MaMh
                }).Where(x => x.MaCDK == cdk.MaCdk && x.Mamh == a.MaMh).ToList();
                var cus = a.MaCdk.Trim() + "_" + a.MaMh.Trim();
                var checkinital = _db.LopMonHocs.Where(x => x.MaLmh.StartsWith(cus)).ToList();
                if (checkinital.Count > 0)
                {
                    list.Add(new MonHocDuocMoCus
                    {
                        Id = a.Id,
                        MaCdk = a.MaCdk,
                        MaMh = a.MaMh,
                        Trangthai = a.Trangthai,
                        Soluong = b.Count,
                        NkapDung = a.NkapDung,
                        trangthaitaolop = true
                    }) ;
                }
                else
                {
                    list.Add(new MonHocDuocMoCus
                    {
                        Id = a.Id,
                        MaCdk = a.MaCdk,
                        MaMh = a.MaMh,
                        Trangthai = a.Trangthai,
                        Soluong = b.Count,
                        NkapDung = a.NkapDung,
                        trangthaitaolop = false
                    });
                }    
                
            }
            return Ok(list);
        }
        [HttpGet("DSCDK/Combox")]
        public IActionResult getdscdkcombo()
        {
            return Ok(_db.CongDangKies.ToList());
        }
        [HttpGet("DSLMH/{macdk}")]
        public IActionResult getDSLMH(String macdk)
        {
            return Ok(_db.LopMonHocs.Where(x => x.MaCdk == macdk.Trim()).ToList());
        }
        [HttpGet("DSSV/{malmh}")]
        public IActionResult getDSSV(string malmh)
        {
            var a = _db.BangDiems.Join(_db.SinhViens, bd => bd.Masv, sv => sv.MaSv, (bd, sv) => new DSSV_LopMonHoc
            {
                Id = bd.Id,
                Masv = bd.Masv,
                TenSv = sv.TenSv,
                MaLmh = bd.MaLmh
            }).Where(x => x.MaLmh == malmh.Trim()).ToList();
            return Ok(a);
        }
        //Thêm sinh vien vào lớp môn học
        [HttpPost("ThemSinhVien")]
        public IActionResult postSVInLMH(DSSV_LopMonHoc sv)
        {
            string macdk = sv.MaLmh.Substring(0, 9);
            string mamh = sv.MaLmh.Substring(10, 7);
            var check_DK = _db.PhieuDangKies.Join(_db.ChiTietPdks, pdk => pdk.MaPdk, ct => ct.MaPdk, (pdk, ct) => new
            {
                Masv = pdk.MaSv,
                MaMh = ct.MaMh,
                MaCDK = pdk.MaCdk,

            }).Where(x => x.Masv == sv.Masv && x.MaCDK == macdk && x.MaMh == mamh).FirstOrDefault();
            if (check_DK == null) return BadRequest("SINH VIÊN CHƯA ĐĂNG KÝ MÔN HỌC NÀY");
            string malop = sv.MaLmh.Substring(0, 17);
            var checksv = _db.LopMonHocs.Join(_db.BangDiems, lmh => lmh.MaLmh, bd => bd.MaLmh, (lmh, bd) => new
            {
                MaLMH = lmh.MaLmh,
                MaSV = bd.Masv
            }).Where(x => x.MaLMH.StartsWith(malop) && x.MaSV == sv.Masv).FirstOrDefault();
            if (checksv != null) return BadRequest("Sinh Viên Đã có trong lớp:" + checksv.MaLMH);
            var check_lmh = _db.LopMonHocs.Find(sv.MaLmh);
            var dssvlmh = _db.BangDiems.Where(x => x.MaLmh == check_lmh.MaLmh).ToList();
            if (check_lmh.Siso == dssvlmh.Count) return BadRequest("Lớp học đầy");
            BangDiem bdsvNew = new BangDiem();
            bdsvNew.MaLmh = sv.MaLmh;
            bdsvNew.Masv = sv.Masv;
            bdsvNew.Trangthai = true;
            _db.BangDiems.Add(bdsvNew);
            _db.SaveChanges();
            return Ok();
        }
        [HttpDelete("XoaSinhVien/{malmh}/{masv}")]
        public IActionResult delete(string malmh, string masv)
        {
            var a = _db.BangDiems.Where(x => x.MaLmh == malmh && x.Masv == masv).FirstOrDefault();
            _db.BangDiems.Remove(a);
            _db.SaveChanges();

            return Ok();
        }
    }
}
