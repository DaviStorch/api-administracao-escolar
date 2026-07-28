using Microsoft.EntityFrameworkCore;
using apiControleAluno.Models;

namespace apiControleAluno.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.ToTable("alunos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(150).IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(150).IsRequired();
            entity.Property(e => e.Curso).HasColumnName("curso").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Matricula).HasColumnName("matricula").HasMaxLength(20).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Matricula).IsUnique();
        });
    }
}