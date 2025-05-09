﻿// <auto-generated />
using System;
using FabrikaYonetimSistemi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FabrikaYonetimSistemi.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241218081303_UpdateRelationship")]
    partial class UpdateRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FactoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FactoryId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Factory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Factories");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.MaterialTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityChange")
                        .HasColumnType("int");

                    b.Property<int>("StorageMaterialId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelId");

                    b.HasIndex("StorageMaterialId");

                    b.ToTable("MaterialTransactions");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Personnel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Role")
                        .HasColumnType("bit");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Personnels");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.StorageMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StorageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("StorageId");

                    b.ToTable("StorageMaterial");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Building", b =>
                {
                    b.HasOne("FabrikaYonetimSistemi.Entity.Entities.Factory", "Factory")
                        .WithMany("Buildings")
                        .HasForeignKey("FactoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.MaterialTransaction", b =>
                {
                    b.HasOne("FabrikaYonetimSistemi.Entity.Entities.Personnel", "Personnel")
                        .WithMany("MaterialTransactions")
                        .HasForeignKey("PersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FabrikaYonetimSistemi.Entity.Entities.StorageMaterial", "StorageMaterial")
                        .WithMany("MaterialTransactions")
                        .HasForeignKey("StorageMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personnel");

                    b.Navigation("StorageMaterial");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Storage", b =>
                {
                    b.HasOne("FabrikaYonetimSistemi.Entity.Entities.Building", "Building")
                        .WithMany("Storages")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.StorageMaterial", b =>
                {
                    b.HasOne("FabrikaYonetimSistemi.Entity.Entities.Material", "Material")
                        .WithMany("StorageMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FabrikaYonetimSistemi.Entity.Entities.Storage", "Storage")
                        .WithMany("StorageMaterials")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Building", b =>
                {
                    b.Navigation("Storages");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Factory", b =>
                {
                    b.Navigation("Buildings");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Material", b =>
                {
                    b.Navigation("StorageMaterials");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Personnel", b =>
                {
                    b.Navigation("MaterialTransactions");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.Storage", b =>
                {
                    b.Navigation("StorageMaterials");
                });

            modelBuilder.Entity("FabrikaYonetimSistemi.Entity.Entities.StorageMaterial", b =>
                {
                    b.Navigation("MaterialTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
