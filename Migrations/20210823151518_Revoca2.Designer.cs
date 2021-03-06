// <auto-generated />
using System;
using C3xPAWM.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace C3xPAWM.Migrations
{
    [DbContext(typeof(C3PAWMDbContext))]
    [Migration("20210823151518_Revoca2")]
    partial class Revoca2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("C3xPAWM.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdRuolo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Proprietario")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Revocato")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ruolo")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Corriere", b =>
                {
                    b.Property<int>("CorriereId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nominativo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Proprietario")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProprietarioId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Revocato")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.HasKey("CorriereId");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Corriere");
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

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Proprietario")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProprietarioId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Provincia")
                        .HasColumnType("TEXT");

                    b.Property<string>("Regione")
                        .HasColumnType("TEXT");

                    b.Property<int>("Revocato")
                        .HasColumnType("INTEGER");

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

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Negozi");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Pacco", b =>
                {
                    b.Property<int>("PaccoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CorriereId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Destinazione")
                        .HasColumnType("TEXT");

                    b.Property<int>("NegozioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Partenza")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatoPacco")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UtenteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dataConsegna")
                        .HasColumnType("TEXT");

                    b.HasKey("PaccoId");

                    b.HasIndex("CorriereId");

                    b.HasIndex("NegozioId");

                    b.HasIndex("UtenteId");

                    b.ToTable("Pacco");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Corriere", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.ApplicationUser", "ProprietarioUser")
                        .WithMany("ProprietarioCorriere")
                        .HasForeignKey("ProprietarioId");

                    b.Navigation("ProprietarioUser");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Negozio", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.ApplicationUser", "ProprietarioUser")
                        .WithMany("ProprietarioNegozi")
                        .HasForeignKey("ProprietarioId");

                    b.Navigation("ProprietarioUser");
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.Pacco", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.Corriere", "Corriere")
                        .WithMany("Pacchi")
                        .HasForeignKey("CorriereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("C3xPAWM.Models.Entities.Negozio", "Negozio")
                        .WithMany("Pacchi")
                        .HasForeignKey("NegozioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("C3xPAWM.Models.Entities.ApplicationUser", "Utente")
                        .WithMany("Pacchi")
                        .HasForeignKey("UtenteId");

                    b.Navigation("Corriere");

                    b.Navigation("Negozio");

                    b.Navigation("Utente");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("C3xPAWM.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("C3xPAWM.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("C3xPAWM.Models.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Pacchi");

                    b.Navigation("ProprietarioCorriere");

                    b.Navigation("ProprietarioNegozi");
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
#pragma warning restore 612, 618
        }
    }
}
