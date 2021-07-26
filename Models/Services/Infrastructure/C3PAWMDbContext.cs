using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

#nullable disable

namespace C3xPAWM.Models.Services.Infrastructure
{
    public partial class C3PAWMDbContext : DbContext
    {
        
    
        public C3PAWMDbContext(DbContextOptions<C3PAWMDbContext> options)
            : base(options)
        {
        }

       

        public virtual DbSet<Indirizzo> Indirizzi { get; set; }
        public virtual DbSet<Negozio> Negozi { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Negozio>(entity => {
                entity.ToTable("Negozi");
                entity.HasKey(negozio => negozio.NegozioId);

                entity.Property(negozio => negozio.Tipologia)
                .HasConversion(tipo => tipo.ToString(), 
                                tipo => (Tipologia)Enum.Parse(typeof(Tipologia), tipo));

                entity.Property(negozio => negozio.Categoria)
                .HasConversion(categoria => categoria.ToString(), 
                                categoria => (Categoria)Enum.Parse(typeof(Categoria), categoria));
                                
                entity.HasMany(negozio => negozio.Indirizzi)
                .WithOne(indirizzo => indirizzo.Negozio)
                .HasForeignKey(indirizzo => indirizzo.IndirizzoId);
            });

            modelBuilder.Entity<Indirizzo>(entity => {
                 entity.ToTable("Indirizzi");
                 entity.HasKey(indirizzo => indirizzo.IndirizzoId);
             });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
