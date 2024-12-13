using libAPI.Data;
using libAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookLoansController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/BookLoans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookLoan>>> GetBookLoans()
        {
            return await _context.BookLoans.ToListAsync();
        }

        // GET: api/BookLoans/1
        [HttpGet("{id}")]
        public async Task<ActionResult<BookLoan>> GetBookLoan(int id)
        {
            var bookLoan = await _context.BookLoans.FindAsync(id);

            if (bookLoan == null)
            {
                return NotFound();
            }

            return bookLoan;
        }

        // PUT: api/BookLoans/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookLoan(int id, BookLoan bookLoan)
        {
            if (id != bookLoan.LoanID)
            {
                return BadRequest();
            }

            _context.Entry(bookLoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookLoanExists(id))
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

        // POST: api/BookLoans
        [HttpPost]
        public async Task<ActionResult<BookLoan>> PostBookLoan(BookLoan bookLoan)
        {
            _context.BookLoans.Add(bookLoan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookLoan", new { id = bookLoan.LoanID }, bookLoan);
        }

        // DELETE: api/BookLoans/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookLoan>> DeleteBookLoan(int id)
        {
            var bookLoan = await _context.BookLoans.FindAsync(id);
            if (bookLoan == null)
            {
                return NotFound();
            }

            _context.BookLoans.Remove(bookLoan);
            await _context.SaveChangesAsync();

            return bookLoan;
        }

        private bool BookLoanExists(int id)
        {
            return _context.BookLoans.Any(e => e.LoanID == id);
        }
    }
}
