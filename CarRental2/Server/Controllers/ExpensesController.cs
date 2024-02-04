using CarRental2.Server.IRepository;
using CarRental2.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseController(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var Expenses = await _unitOfWork.Expenses.GetAll();
            return Ok(Expenses);
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var Expense = await _unitOfWork.Expenses.Get(q => q.Id == id);
            if (Expense == null)
            {
                return NotFound();
            }
            return Ok(Expense);
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense Expense)
        {
            if (id != Expense.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Expenses.Update(Expense);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {

                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense Expense)
        {
            await _unitOfWork.Expenses.Insert(Expense);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetExpense", new { id = Expense.Id }, Expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var Expense = await _unitOfWork.Expenses.Get(q => q.Id == id);

            if (Expense == null)
            {
                return NotFound();
            }
            await _unitOfWork.Expenses.Delete(id);
            await _unitOfWork.Save(HttpContext);
            return NoContent();
        }


    }
}
