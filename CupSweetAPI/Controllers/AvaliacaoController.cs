using CupcakeStoreAPI.Context;
using CupcakeStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CupcakeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Avaliacao>> Get()
        {
            return Ok(_context.Avaliacoes.Include(a => a.User).AsNoTracking().ToList());
        }

        [HttpGet("{id:int}", Name = "ObterAvaliacao")]
        public ActionResult<Avaliacao> GetById(int id)
        {
            var avaliacao = _context.Avaliacoes.AsNoTracking().FirstOrDefault(c => c.AvaliacaoId == id);

            if (avaliacao == null) return NotFound("Avaliação não encontrada.");

            return Ok(avaliacao);
        }

        [HttpPost]
        public ActionResult Post(Avaliacao? avaliacao)
        {
            if (avaliacao == null) return BadRequest("Avaliação inválida.");

            _context.Avaliacoes.Add(avaliacao);
            _context.SaveChanges();

            return CreatedAtRoute("ObterAvaliacao", new { id = avaliacao.AvaliacaoId }, avaliacao);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Avaliacao? avaliacao)
        {
            if (avaliacao == null) return BadRequest("Avaliação inválida");
            if (id != avaliacao.AvaliacaoId) return BadRequest("Ids não correspondentes");

            _context.Entry(avaliacao).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(avaliacao);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var avaliacao = _context.Avaliacoes.FirstOrDefault(c => c.AvaliacaoId == id);

            if (avaliacao == null) return NotFound("Avaliação não encontrada.");

            _context.Avaliacoes.Remove(avaliacao);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
