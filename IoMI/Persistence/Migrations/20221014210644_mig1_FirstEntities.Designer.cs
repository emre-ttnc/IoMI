// <auto-generated />
using System;
using IoMI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IoMI.Persistence.Migrations
{
    [DbContext(typeof(IoMIDbContext))]
    [Migration("20221014210644_mig1_FirstEntities")]
    partial class mig1_FirstEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GasMeterGasMeterInspection", b =>
                {
                    b.Property<Guid>("GasMeterId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InspectionsId")
                        .HasColumnType("uuid");

                    b.HasKey("GasMeterId", "InspectionsId");

                    b.HasIndex("InspectionsId");

                    b.ToTable("GasMeterGasMeterInspection");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InspectionEntities.GasMeterInspection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("InspectionDate")
                        .HasColumnType("date");

                    b.Property<string>("InspectorId")
                        .HasColumnType("text");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("InspectorId");

                    b.ToTable("GasMeterInspections");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InspectionEntities.ScaleInspection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("InspectionDate")
                        .HasColumnType("date");

                    b.Property<string>("InspectorId")
                        .HasColumnType("text");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("InspectorId");

                    b.ToTable("ScaleInspections");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InstrumentEntities.GasMeter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Brand")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LastInspectionYear")
                        .HasColumnType("integer");

                    b.Property<string>("MaxFlowRate")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("TypeOrModel")
                        .HasColumnType("text");

                    b.Property<string>("UserOfInstrumentId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserOfInstrumentId");

                    b.ToTable("GasMeters");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InstrumentEntities.Scale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccuracyClass")
                        .HasColumnType("text");

                    b.Property<string>("Brand")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LastInspectionYear")
                        .HasColumnType("integer");

                    b.Property<string>("MaximumCapacity")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("TypeOrModel")
                        .HasColumnType("text");

                    b.Property<string>("UserOfInstrumentId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserOfInstrumentId");

                    b.ToTable("Scales");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.UserEntities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RegistryCode")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("IoMI.Domain.Entities.UserEntities.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ScaleScaleInspection", b =>
                {
                    b.Property<Guid>("InspectionsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ScalesId")
                        .HasColumnType("uuid");

                    b.HasKey("InspectionsId", "ScalesId");

                    b.HasIndex("ScalesId");

                    b.ToTable("ScaleScaleInspection");
                });

            modelBuilder.Entity("GasMeterGasMeterInspection", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.InstrumentEntities.GasMeter", null)
                        .WithMany()
                        .HasForeignKey("GasMeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoMI.Domain.Entities.InspectionEntities.GasMeterInspection", null)
                        .WithMany()
                        .HasForeignKey("InspectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InspectionEntities.GasMeterInspection", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", "Inspector")
                        .WithMany()
                        .HasForeignKey("InspectorId");

                    b.Navigation("Inspector");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InspectionEntities.ScaleInspection", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", "Inspector")
                        .WithMany()
                        .HasForeignKey("InspectorId");

                    b.Navigation("Inspector");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InstrumentEntities.GasMeter", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", "UserOfInstrument")
                        .WithMany()
                        .HasForeignKey("UserOfInstrumentId");

                    b.Navigation("UserOfInstrument");
                });

            modelBuilder.Entity("IoMI.Domain.Entities.InstrumentEntities.Scale", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", "UserOfInstrument")
                        .WithMany()
                        .HasForeignKey("UserOfInstrumentId");

                    b.Navigation("UserOfInstrument");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.UserEntities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScaleScaleInspection", b =>
                {
                    b.HasOne("IoMI.Domain.Entities.InspectionEntities.ScaleInspection", null)
                        .WithMany()
                        .HasForeignKey("InspectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoMI.Domain.Entities.InstrumentEntities.Scale", null)
                        .WithMany()
                        .HasForeignKey("ScalesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
