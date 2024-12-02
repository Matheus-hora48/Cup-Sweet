using CupcakeStoreAPI.Context;
using CupcakeStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CupcakeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnderecoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Endereco>> Get()
        {
            return Ok(_context.Enderecos.AsNoTracking().ToList());
        }

        [HttpGet("usuario/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<Endereco>> GetByUser(string? id)
        {
            if (id is null or "")
                return BadRequest("Id não pode ser nulo");

            return Ok(_context.Enderecos.Where(e => e.UserId == id).AsNoTracking().ToList());
        }

        [HttpGet("{id:int}", Name = "ObterEndereco")]
        [Authorize]
        public ActionResult<Endereco> GetById(int id)
        {
            var endereco = _context.Enderecos.AsNoTracking().FirstOrDefault(c => c.EnderecoId == id);

            if (endereco == null) return NotFound("Endereço não encontrada.");

            return Ok(endereco);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post(Endereco? endereco)
        {
            if (endereco == null) return BadRequest("Endereço inválido.");

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return CreatedAtRoute("ObterEndereco", new { id = endereco.EnderecoId }, endereco);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult Put(int id, Endereco? endereco)
        {
            if (endereco == null) return BadRequest("Endereço inválido");
            if (id != endereco.EnderecoId) return BadRequest("Ids não correspondentes");

            _context.Entry(endereco).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(endereco);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(c => c.EnderecoId == id);

            if (endereco == null) return NotFound("Endereço não encontrado.");

            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
