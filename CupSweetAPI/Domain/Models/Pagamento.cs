using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CupcakeStoreAPI.Domain.Enums;

namespace CupcakeStoreAPI.Domain.Models
{
    [Table("Pagamentos")]
    public class Pagamento
    {
        [Key]
        public int PagamentoId { get; set; }

        [Required]
        public DateTime DataPagamento { get; set; }
        [Required]
        [Column(TypeName = "integer")]
        public FormasPagamento FormaPagamento { get; set; }

        [Required]
        [Column(TypeName = "numeric(12,2)")]
        public double Valor { get; set; }

        public int PedidoId { get; set; }
        [JsonIgnore] public Pedido? Pedido { get; set; }
    }
}
