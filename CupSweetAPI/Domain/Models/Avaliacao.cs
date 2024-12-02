using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeStoreAPI.Domain.Models
{
    [Table("Avaliacao")]
    public class Avaliacao
    {
        [Key] public int AvaliacaoId { get; set; }
        [Required] public int Nota { get; set; }
        [StringLength(500)] public string? Comentario { get; set; }

        public int ProdutoId { get; set; }

        [JsonIgnore] public Produto? Produto { get; set; }

        [StringLength(60)]
        [Required]
        public string? UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ApplicationUser? User { get; set; }
    }
}
