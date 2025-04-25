using Microsoft.EntityFrameworkCore;
using Laboratorio_projeto.Models;
using System.Collections.Generic;

namespace CrudUser.Models
{
    public class Conexao : DbContext
    {
        public Conexao(DbContextOptions<Conexao> options) : base(options)
        {

        }
        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<PessoasExames> PessoasExames { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoas>()
                .HasMany(p => p.ExamesMarcados)
                .WithOne(e => e.Pessoa)
                .HasForeignKey(e => e.CPF);
        }
    }

}