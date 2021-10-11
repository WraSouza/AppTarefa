using AppTarefa.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTarefa.Banco
{
    public class BancoContext : DbContext
    {
        public DbSet<Tarefas> Tarefas { get; set; }

        public BancoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Constantes.CaminhoDoBanco}");
        }
    }
}
