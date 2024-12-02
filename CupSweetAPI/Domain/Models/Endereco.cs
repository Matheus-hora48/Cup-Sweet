using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeStoreAPI.Domain.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key] public int EnderecoId { get; set; }

        [Required]
        [StringLength(150)]
        public required string Rua { get; set; }

        [Required]
        [StringLength(100)]
        public required string Bairro { get; set; }

        [StringLength(300)]
        public string? Complemento { get; set; }

        [Required]
        [StringLength(15)]
        public required string Numero { get; set; }

        [Required]
        [StringLength(50)]
        public required string Cidade { get; set; }

        [Required]
        [StringLength(20)]
        public required string Cep { get; set; }

        [Required]
        [StringLength(2)]
        public required string Estado { get; set; }

        [Required]
        [StringLength(80)]
        public string? UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ApplicationUser? User { get; set; }
    }
}
