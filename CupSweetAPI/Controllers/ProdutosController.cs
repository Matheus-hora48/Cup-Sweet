using CupcakeStoreAPI.Context;
using CupcakeStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CupcakeStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.AsNoTracking().ToList();

            if (produtos.Count == 0) return NotFound("Produtos não encontrados.");

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = _context.Produtos.Include(p => p.Avaliacoes)!.ThenInclude(a => a.User).AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            if (produto == null) return NotFound("Produto não encontrado.");

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto? produto)
        {
            if (produto == null) return BadRequest("Produto inválido.");

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }


        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto? produto)
        {
            if (produto == null) return BadRequest("Produto inválido.");
            if (id != produto.ProdutoId) return BadRequest("Ids não correspondentes.");

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto == null) return NotFound("Produto não encontrado.");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
