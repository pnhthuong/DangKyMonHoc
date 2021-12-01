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
    public class MonHocDuocMoController : ControllerBase
    {
        private readonly DangKyMonHocContext _db = new DangKyMonHocContext();
        public IEnumerable<MonHocDuocMoCustom> getAllDSMH()
        {
            List<MonHocDuocMoCustom> ds = new List<MonHocDuocMoCustom>();
            foreach (var item in _db.MonHocDuocMos.ToList())
            {
                foreach (var monhoc in _db.MonHocs.ToList())
                {
                    if (item.MaMh == monhoc.MaMh)
                    {
                        ds.Add(new MonHocDuocMoCustom
                        {
                            MaMh = item.MaMh,
                            Soluong = item.Soluong,
                            Trangthai = item.Trangthai,
                            TenMh = monhoc.TenMh,
                            MaCdk = item.MaCdk,
                            hesohp = monhoc.HesoHp,
                            sotinchi = monhoc.Sotinchi,
                            
                        });
                    }
                }

            }
            return ds;
        }
        [HttpGet("{id}")]
        public IEnumerable<MonHocDuocMo> getlistmonhocduocmo(string id)
        {
            var list = _db.MonHocDuocMos.Where(x => x.MaCdk == id).ToList();
            return list.ToList();
        }

        [HttpGet("{search}/{maNK}/{hocky_ctdt}")]
        public async Task<ActionResult<IEnumerable<MonHoc>>> getListMonHoc(string maNK, string hocky_ctdt)
        {
            var nienkhoa = await _db.NienKhoas.FindAsync(maNK);
            if (nienkhoa == null) return NotFound();
            var ctdt = _db.ChuongTrinhDaoTaos.Find(nienkhoa.MaCtdt);
            var hocky = _db.HocKyCtdts.Find(hocky_ctdt);
            List<ChiTietCtdt> ctdts = _db.ChiTietCtdts.Where(x => x.MaCtdt == ctdt.MaCtdt && x.MaHk == hocky.MaHk).ToList();
            if (ctdts.Count < 1) return NotFound();
            List<MonHoc> monHocs = new List<MonHoc>();
            foreach (var a in ctdts)
            {
                MonHoc b = _db.MonHocs.Find(a.MaMh);
                if (b == null) continue;
                monHocs.Add(b);
            }
            return monHocs.ToList();
        }

        [HttpPost]
        public IActionResult postDSMonhocDuocMo([FromBody] List<MonHocDuocMo> list)
        {
            List<MonHocDuocMo> listdb = _db.MonHocDuocMos.ToList();
            //Kiểm tra trong danh sách môn học được mở có tồn tại cổng đăng ký trong list không?
            var checknew = listdb.Where(x => x.MaCdk == list[0].MaCdk).ToList();
            // kiểm tra đó có phải là cổng đào tạo mới không?
            if (checknew == null)
            {
                foreach (var a in list)
                {
                    _db.Add(a);
                }
                _db.SaveChanges();
                return Ok();
            }
            //so sánh danh sách post và danh sách database nếu trùng thì cập nhật lại số lượng và trạng thái. nếu không tìm thấy thì add new vào database
            foreach (var a in list)
            {
                MonHocDuocMo b = checknew.Find(x => x.MaMh == a.MaMh);
                if (b != null)
                {
                    b.Soluong = a.Soluong;
                    b.Trangthai = a.Trangthai;
                    _db.MonHocDuocMos.Update(b);
                    checknew.Remove(b);
                }
                else
                {
                    _db.MonHocDuocMos.Add(a);
                }
            }
            //kiểm tra xem danh sách trong database còn hay không? nếu còn thì xóa đi tất cả 
            if (checknew.Count > 0)
            {
                foreach (var a in checknew)
                {
                    _db.MonHocDuocMos.Remove(a);
                }
            }
            _db.SaveChanges();
            return Ok();
        }

        [HttpGet("{getdsCDKNK}/{id}")]
        public IEnumerable<NienKhoaCdk> getdsCDKNK(string id)
        {
            var list = _db.NienKhoaCdks.Where(x => x.MaCdk == id).ToList();
            return list;
        }

        

        [HttpPost("{postdsCDKNK}")]
        public IActionResult postdsCDKNK([FromBody] List<NienKhoaCdk> list)
        {
            var listdb = _db.NienKhoaCdks.Where(x => x.MaCdk == list[0].MaCdk).ToList();
            if (listdb == null)
            {
                foreach(var a in list)
                {
                    _db.NienKhoaCdks.Add(a);
                }
                _db.SaveChanges();
                return Ok();
            }

            foreach(var a in list)
            {
                var b = listdb.Find(x => x.MaCdk == a.MaCdk && x.MaNk == a.MaNk);
                if (b == null)
                {
                    _db.NienKhoaCdks.Add(a);
                }
                listdb.Remove(b);
            }
            if (listdb.Count > 0)
            {
                foreach(var a in listdb)
                {
                    _db.NienKhoaCdks.Remove(a);
                }
            }
            _db.SaveChanges();
            return Ok();
        }


        
    }

}
