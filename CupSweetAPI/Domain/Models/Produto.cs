using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeStoreAPI.Domain.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }

        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public double Preco { get; set; }
        public int QuantidadeVendido { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }

        [StringLength(300)]
        public string? ImageUrl { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        public ICollection<Avaliacao>? Avaliacoes { get; set; }
    }
}
