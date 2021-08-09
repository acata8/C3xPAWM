﻿// <auto-generated />
using C3xPAWM.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace C3xPAWM.Migrations
{
    [DbContext(typeof(C3PAWMDbContext))]
    [Migration("20210809200158_InitialMigrations")]
    partial class InitialMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("C3xPAWM.Models.Entities.Corriere", b =>
                {
                    b.Property<int>("CorriereId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nominativo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("CorriereId");

                    b.ToTable("Corrieri");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Negozio", b =>
                {
                    b.Property<int>("NegozioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Citta")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Provincia")
                        .HasColumnType("TEXT");

                    b.Property<string>("Regione")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipologia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Token")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Via")
                        .HasColumnType("TEXT");

                    b.HasKey("NegozioId");

                    b.ToTable("Negozi");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Pacco", b =>
                {
                    b.Property<int>("PaccoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Citta")
                        .HasColumnType("TEXT");

                    b.Property<int>("CorriereId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NegozioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Provincia")
                        .HasColumnType("TEXT");

                    b.Property<string>("StatoPacco")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UtenteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Via")
                        .HasColumnType("TEXT");

                    b.HasKey("PaccoId");

                    b.HasIndex("CorriereId");

                    b.HasIndex("NegozioId");

                    b.HasIndex("UtenteId");

                    b.ToTable("Pacchi");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Pubblicita", b =>
                {
                    b.Property<int>("PubblicitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Attiva")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Durata")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NegozioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomeEvento")
                        .HasColumnType("TEXT");

                    b.HasKey("PubblicitaId");

                    b.HasIndex("NegozioId");

                    b.ToTable("Pubblicita");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Utente", b =>
                {
                    b.Property<int>("UtenteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("UtenteId");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Pacco", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.Corriere", "Corrieri")
                        .WithMany("Pacchi")
                        .HasForeignKey("CorriereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("C3xPAWM.Models.Entities.Negozio", "Negozio")
                        .WithMany("Pacchi")
                        .HasForeignKey("NegozioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("C3xPAWM.Models.Entities.Utente", "Utenti")
                        .WithMany("Pacchi")
                        .HasForeignKey("UtenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Corrieri");

                    b.Navigation("Negozio");

                    b.Navigation("Utenti");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Pubblicita", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.Negozio", "Negozio")
                        .WithMany("Pubblicita")
                        .HasForeignKey("NegozioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Negozio");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Corriere", b =>
                {
                    b.Navigation("Pacchi");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Negozio", b =>
                {
                    b.Navigation("Pacchi");

                    b.Navigation("Pubblicita");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Utente", b =>
                {
                    b.Navigation("Pacchi");
                });
#pragma warning restore 612, 618
        }
    }
}
