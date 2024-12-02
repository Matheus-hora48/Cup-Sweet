using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeStoreAPI.Domain.Models
{
    [Table("PedidoProdutos")]
    public class PedidoProduto
    {
        [Key]
        public int Id { get; set; }

        [Required] public int Quantidade { get; set; }
        [Required] public decimal ValorTotal { get; set; }
        [Required] public int ProdutoId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Produto? Produto { get; set; }
        [Required] public int PedidoId { get; set; }
        [JsonIgnore]
        public Pedido? Pedido { get; set; }

    }
}
