using CupcakeStoreAPI.Domain.Models;
using CupcakeStoreAPI.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CupcakeStoreAPI.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Avaliacao>()
                .HasOne(a => a.User) // Relacionamento com User
                .WithMany()          // Não há necessidade de navegação inversa aqui
                .HasForeignKey(a => a.UserId) // Configura a chave estrangeira
                .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata
        }
    }
}
