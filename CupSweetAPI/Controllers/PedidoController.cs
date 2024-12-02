using System.Net;
using CupcakeStoreAPI.Context;
using CupcakeStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CupcakeStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "ObterPedido")]
        [Authorize]
        public ActionResult<Pedido> GetById(int id)
        {
            var pedido = _context.Pedidos
                .Include(p => p.Produtos)!
                .ThenInclude(p => p.Produto)
                .Include(p => p.Endereco)
                .Include(p => p.Pagamentos)
                .AsNoTracking()
                .FirstOrDefault(p => p.PedidoId == id);

            if (pedido == null) return NotFound("Pedido não encontrado.");

            return Ok(pedido);
        }

        [HttpGet("usuario/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<Pedido>> GetByUser(string id)
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Produtos)!
                .ThenInclude(pp => pp.Produto)
                .Include(p => p.Endereco)
                .Include(p => p.Pagamentos)
                .Where(p => p.UserId == id)
                .AsNoTracking()
                .ToList();

            if (pedidos.Count == 0) return NotFound("Usuario nao possui pedidos");

            return Ok(pedidos);
        }

        [HttpPost]
        [Authorize]
        public ActionResult NovoPedido(Pedido pedido)
        {
            if (pedido.Produtos is { Count: 0 }) return BadRequest("Pedido sem produtos");

            var endereco = _context.Enderecos.AsNoTracking().FirstOrDefault(e => e.EnderecoId == pedido.EnderecoId);
            if (endereco is null) return BadRequest("Pedido sem endereco");

            if (pedido.Pagamentos is { Count: 0 }) return BadRequest("Pagamentos invalidos");

            string produtosSemEstoque = string.Empty;
            foreach (var item in pedido.Produtos!)
            {
                var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

                if (produto is null) return BadRequest($"Produto de id {item.ProdutoId} não existe.");

                if (produto.Estoque <= item.Quantidade)
                    produtosSemEstoque += $"\n Estoque insuficiente item: {produto.Nome}";

                item.ValorTotal = (decimal)(item.Quantidade * produto.Preco);
            }

            if (produtosSemEstoque != string.Empty)
                return BadRequest($"Alguns produtos possuem pedencias: {produtosSemEstoque}");

            pedido.DataPedido = DateTime.Now;
            pedido.ValorTotal = pedido.Produtos.Aggregate(0, (double acc, PedidoProduto x) => acc + (double)x.ValorTotal);
            
            var totalPagamento = pedido.Pagamentos!.Aggregate(0, (double acc, Pagamento x) => acc + x.Valor);
            if (Math.Abs(totalPagamento - pedido.ValorTotal) > 0.03)
                return BadRequest("Parcelamento diferente do valor total.");

            try
            {
                _context.Pedidos.Add(pedido);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Forbid(e.ToString());
            }

            return CreatedAtRoute("ObterPedido", new { id = pedido.PedidoId }, pedido);
        }
    }
}
