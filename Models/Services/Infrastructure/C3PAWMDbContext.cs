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
        public virtual DbSet<Pacco> Pacchi { get; set; }
        public virtual DbSet<Pubblicita> Pubblicita { get; set; }  
        public virtual DbSet<Utente> Utenti { get; set; }  
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
            });

            modelBuilder.Entity<Pubblicita>(entity => {
                    entity.ToTable("Pubblicita");
                    entity.HasKey(p => p.PubblicitaId);

                entity.HasOne(p => p.Negozio)
                .WithMany(p => p.Pubblicita)
                .HasForeignKey(p=>p.NegozioId);
            });


            modelBuilder.Entity<Pacco>(entity => {
                    entity.ToTable("Pacchi");
                    entity.HasKey(p => p.PaccoId);

                  entity.HasOne(p => p.Negozio)
                    .WithMany(p => p.Pacchi)
                    .HasForeignKey(p=>p.NegozioId);  

                 entity.HasOne(p => p.Utenti)
                    .WithMany(p => p.Pacchi)
                    .HasForeignKey(p=>p.UtenteId);

                entity.HasOne(p => p.Corrieri)
                    .WithMany(p => p.Pacchi)
                    .HasForeignKey(p=>p.CorriereId);

                entity.Property(pacco => pacco.StatoPacco)
                .HasConversion(tipo => tipo.ToString(), 
                                tipo => (StatoPacco)Enum.Parse(typeof(StatoPacco), tipo));

            });

            modelBuilder.Entity<Utente>(entity => {
                                entity.ToTable("Utenti");
                                entity.HasKey(p => p.UtenteId);
                                
                     entity
                        .Property(utente => utente.Categoria)
                        .HasConversion(categoria => categoria.ToString(), 
                                        categoria => (Categoria)Enum.Parse(typeof(Categoria), categoria)); 
                                
            });

             modelBuilder.Entity<Corriere>(entity => {
                                entity.ToTable("Corrieri");
                                entity.HasKey(p => p.CorriereId);
                                
                     entity
                        .Property(corriere => corriere.Categoria)
                        .HasConversion(categoria => categoria.ToString(), 
                                        categoria => (Categoria)Enum.Parse(typeof(Categoria), categoria)); 
                                
            });

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
