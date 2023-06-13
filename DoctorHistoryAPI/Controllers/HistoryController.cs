using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoctorHistoryAPI.Data;
using DoctorHistoryAPI.Models;

namespace DoctorHistoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly APIDBCONTEXT _context;

        public HistoryController(APIDBCONTEXT context)
        {
            _context = context;
        }

        // GET: api/History
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocHistory>>> GetdocHistory()
        {
          if (_context.docHistory == null)
          {
              return NotFound();
          }
            return await _context.docHistory.ToListAsync();
        }

        // GET: api/History/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocHistory>> GetDocHistory(int id)
        {
          if (_context.docHistory == null)
          {
              return NotFound();
          }
            var docHistory = await _context.docHistory.FindAsync(id);

            if (docHistory == null)
            {
                return NotFound();
            }

            return docHistory;
        }

        // PUT: api/History/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocHistory(int id, DocHistory docHistory)
        {
            if (id != docHistory.DoctorId)
            {
                return BadRequest();
            }

            _context.Entry(docHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/History
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocHistory>> PostDocHistory(DocHistory docHistory)
        {
          if (_context.docHistory == null)
          {
              return Problem("Entity set 'APIDBCONTEXT.docHistory'  is null.");
          }
            _context.docHistory.Add(docHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocHistory", new { id = docHistory.DoctorId }, docHistory);
        }

        // DELETE: api/History/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocHistory(int id)
        {
            if (_context.docHistory == null)
            {
                return NotFound();
            }
            var docHistory = await _context.docHistory.FindAsync(id);
            if (docHistory == null)
            {
                return NotFound();
            }

            _context.docHistory.Remove(docHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocHistoryExists(int id)
        {
            return (_context.docHistory?.Any(e => e.DoctorId == id)).GetValueOrDefault();
        }
    }
}
