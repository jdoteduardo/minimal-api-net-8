using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Categoria
            modelBuilder.Entity<Categoria>()
                .HasKey(c => c.CategoriaId);

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nome)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Descricao)
                .HasMaxLength(150)
                .IsRequired();

            // Produto
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.ProdutoId);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(p => p.Descricao)
                .HasMaxLength(150);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Imagem)
                .HasMaxLength(100);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(14, 2);

            // Login
            modelBuilder.Entity<User>()
                .HasKey(c => c.UserId);

            modelBuilder.Entity<User>()
                .Property(c => c.UserName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(c => c.Password)
                .HasMaxLength(200)
                .IsRequired();

            // Relacionamento
            modelBuilder.Entity<Produto>()
                .HasOne<Categoria>(p => p.Categoria)
                    .WithMany(p => p.Produtos)
                        .HasForeignKey(p => p.CategoriaId);
        }
    }
}
