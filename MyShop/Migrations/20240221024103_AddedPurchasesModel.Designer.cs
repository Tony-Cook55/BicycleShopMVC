﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyShop.Models;

#nullable disable

namespace MyShop.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20240221024103_AddedPurchasesModel")]
    partial class AddedPurchasesModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyShop.Models.Bicycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Year")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Bicycles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Trek",
                            Color = "Black",
                            ImageFileName = "trek-fx1-black.jpg",
                            Model = "FX 1",
                            Price = 450.00m,
                            Type = "Hybrid",
                            Year = 2022
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Giant",
                            Color = "Red",
                            ImageFileName = "giant-tcr-red.jpg",
                            Model = "TCR Advanced Pro 1 Disc",
                            Price = 3500.00m,
                            Type = "Road",
                            Year = 2023
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Specialized",
                            Color = "Blue",
                            ImageFileName = "specialized-rockhopper-blue.jpg",
                            Model = "Rockhopper Expert 29",
                            Price = 1100.00m,
                            Type = "Mountain",
                            Year = 2022
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Cannondale",
                            Color = "White",
                            ImageFileName = "cannondale-synapse-white.jpg",
                            Model = "Synapse Carbon 105",
                            Price = 2000.00m,
                            Type = "Road",
                            Year = 2023
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Trek",
                            Color = "Green",
                            ImageFileName = "trek-marlin-green.jpg",
                            Model = "Marlin 5",
                            Price = 550.00m,
                            Type = "Mountain",
                            Year = 2022
                        });
                });

            modelBuilder.Entity("MyShop.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BicycleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BicycleId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("MyShop.Models.Purchase", b =>
                {
                    b.HasOne("MyShop.Models.Bicycle", "Bicycle")
                        .WithMany()
                        .HasForeignKey("BicycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bicycle");
                });
#pragma warning restore 612, 618
        }
    }
}