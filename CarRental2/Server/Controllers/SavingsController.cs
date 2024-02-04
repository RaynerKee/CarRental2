using CarRental2.Server.IRepository;
using CarRental2.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SavingController(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<IActionResult> GetSavings()
        {
            var Savings = await _unitOfWork.Savings.GetAll();
            return Ok(Savings);
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaving(int id)
        {
            var Saving = await _unitOfWork.Savings.Get(q => q.Id == id);
            if (Saving == null)
            {
                return NotFound();
            }
            return Ok(Saving);
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaving(int id, Saving Saving)
        {
            if (id != Saving.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Savings.Update(Saving);

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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Saving>> PostSaving(Saving Saving)
        {
            await _unitOfWork.Savings.Insert(Saving);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetSaving", new { id = Saving.Id }, Saving);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaving(int id)
        {
            var Saving = await _unitOfWork.Savings.Get(q => q.Id == id);

            if (Saving == null)
            {
                return NotFound();
            }
            await _unitOfWork.Savings.Delete(id);
            await _unitOfWork.Save(HttpContext);
            return NoContent();
        }


    }
}
