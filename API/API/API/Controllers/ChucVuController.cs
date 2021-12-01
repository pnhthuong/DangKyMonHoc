using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly DangKyMonHocContext _db;

        public ChucVuController(DangKyMonHocContext dangKyMonHocContext)
        {
            _db = dangKyMonHocContext;
        }

        [HttpGet]
        public IEnumerable<ChucVu> getlistchucvu()
        {
            return _db.ChucVus.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChucVu>> getDetailChucVu(string id)
        {
            var detail = await _db.ChucVus.FindAsync(id.Trim());
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<ActionResult<ChucVu>> postChucVu(ChucVu cv)
        {
            var checkCV = await _db.ChucVus.FindAsync(cv.MaChucVu.Trim());
            if (checkCV != null) return NotFound();
            _db.ChucVus.Add(cv);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ChucVu>> deleteChucVu(string id)
        {
            var checkCV = await _db.ChucVus.FindAsync(id.Trim());
            if (checkCV == null) return NotFound();
            var checkRelationshipNV = _db.NhanViens.Where(n => n.MaChucVu.Contains(id)).Count();
            var checkRelationshipGV = _db.GiangViens.Where(n => n.MaChucVu.Contains(id)).Count();
            if (checkRelationshipGV > 0 || checkRelationshipNV > 0) return NotFound();
            _db.ChucVus.Remove(checkCV);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ChucVu>> putChucVu(ChucVu cv)
        {
            var checkCV = await _db.ChucVus.FindAsync(cv.MaChucVu.Trim());
            if (checkCV == null) return NotFound();

            checkCV.TenChucVu = cv.TenChucVu;
            _db.ChucVus.Update(checkCV);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
