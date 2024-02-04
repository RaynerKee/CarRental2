using CarRental2.Server.IRepository;
using CarRental2.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleController(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var Articles = await _unitOfWork.Articles.GetAll();
            return Ok(Articles);
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var Article = await _unitOfWork.Articles.Get(q => q.Id == id);
            if (Article == null)
            {
                return NotFound();
            }
            return Ok(Article);
        }

        // PUT: api/Article/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article Article)
        {
            if (id != Article.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Articles.Update(Article);

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

        // POST: api/Article
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article Article)
        {
            await _unitOfWork.Articles.Insert(Article);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("GetArticle", new { id = Article.Id }, Article);
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var Article = await _unitOfWork.Articles.Get(q => q.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            await _unitOfWork.Articles.Delete(id);
            await _unitOfWork.Save(HttpContext);
            return NoContent();
        }


    }
}