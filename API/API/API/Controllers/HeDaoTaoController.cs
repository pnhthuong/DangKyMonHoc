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
    public class HeDaoTaoController : ControllerBase
    {
        private readonly DangKyMonHocContext _db;

        public HeDaoTaoController(DangKyMonHocContext dangKyMonHocContext)
        {
            _db = dangKyMonHocContext;
        }

        [HttpGet]
        public IEnumerable<HeDaoTao> getlistHeDaoTao()
        {
            return _db.HeDaoTaos.ToList();
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<HeDaoTao>> getDetailHeDaoTao(string id)
        {
            var detail = await _db.HeDaoTaos.FindAsync(id.Trim());
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<ActionResult<HeDaoTao>> postHeDaoTao(HeDaoTao cv)
        {
            var checkCV = await _db.HeDaoTaos.FindAsync(cv.MaDt.Trim());
            if (checkCV != null) return NotFound();
            _db.HeDaoTaos.Add(cv);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HeDaoTao>> deleteHeDaoTao(string id)
        {
            var checkCV = await _db.HeDaoTaos.FindAsync(id.Trim());
            if (checkCV == null) return NotFound();
            var checkRelationshipCTDT = _db.ChuongTrinhDaoTaos.Where(n => n.MaDt.Contains(id)).Count();
            if ( checkRelationshipCTDT > 0) return NotFound();
            _db.HeDaoTaos.Remove(checkCV);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HeDaoTao>> putHeDaoTao(HeDaoTao cv)
        {
            var checkCV = await _db.HeDaoTaos.FindAsync(cv.MaDt.Trim());
            if (checkCV == null) return NotFound();

            checkCV.TenDt = cv.TenDt;
            _db.HeDaoTaos.Update(checkCV);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
