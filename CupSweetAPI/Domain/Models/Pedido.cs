using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeStoreAPI.Domain.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key] public int PedidoId { get; set; }

        [Required] public DateTime DataPedido { get; set; }

        public ICollection<PedidoProduto>? Produtos { get; set; }

        [Required] public double ValorTotal { get; set; }

        [Required] public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        public ICollection<Pagamento>? Pagamentos { get; set; }

        [StringLength(80)]
        [Required]
        public string? UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ApplicationUser? User { get; set; }
    }
}
