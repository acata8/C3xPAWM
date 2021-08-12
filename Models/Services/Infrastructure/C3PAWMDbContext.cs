using System;
using Microsoft.EntityFrameworkCore;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace C3xPAWM.Models.Services.Infrastructure
{
    public partial class C3PAWMDbContext : IdentityDbContext<ApplicationUser>
    {
        
    
        public C3PAWMDbContext(DbContextOptions<C3PAWMDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Negozio> Negozi { get; set; }
        public virtual DbSet<Pubblicita> Pubblicita { get; set; }  
        public virtual DbSet<Corriere> Corrieri { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Negozio>(entity => {
                entity.ToTable("Negozi");
                entity.HasKey(negozio => negozio.NegozioId);

                entity.Property(negozio => negozio.Tipologia)
                .HasConversion(tipo => tipo.ToString(), 
                                tipo => (Tipologia)Enum.Parse(typeof(Tipologia), tipo));

                entity.Property(negozio => negozio.Categoria)
                .HasConversion(categoria => categoria.ToString(), 
                                categoria => (Categoria)Enum.Parse(typeof(Categoria), categoria));


                entity.HasOne(p => p.ProprietarioUser)
                    .WithMany(p => p.ProprietarioNegozi)
                    .HasForeignKey(p=>p.ProprietarioId);    
            });


             modelBuilder.Entity<Corriere>(entity => {
                entity.ToTable("Corriere");
                entity.HasKey(c => c.CorriereId);

               
                entity.Property(c => c.Categoria)
                .HasConversion(categoria => categoria.ToString(), 
                                categoria => (Categoria)Enum.Parse(typeof(Categoria), categoria));
                                
                entity.HasOne(p => p.ProprietarioUser)
                    .WithMany(p => p.ProprietarioCorriere)
                    .HasForeignKey(p=>p.ProprietarioId);    
            });

            modelBuilder.Entity<Pubblicita>(entity => {
                    entity.ToTable("Pubblicita");
                    entity.HasKey(p => p.PubblicitaId);

                entity.HasOne(p => p.Negozio)
                .WithMany(p => p.Pubblicita)
                .HasForeignKey(p=>p.NegozioId);
            });

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
