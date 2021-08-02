using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApi.Models
{
    public class ContextoBd : DbContext
    {
        public virtual DbSet<Pessoa> Pessoas { get; set; }

        public ContextoBd()
        {

        }

        public ContextoBd(DbContextOptions<ContextoBd> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.pessoaId);

                entity.Property(e => e.Nome).IsRequired().HasMaxLength(120);
                entity.Property(e => e.CPF).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(60);
                entity.Property(e => e.UF).IsRequired().HasMaxLength(2);
                entity.HasIndex(e => e.Email)
                    .HasName("UQ_pessoa_Email")
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
