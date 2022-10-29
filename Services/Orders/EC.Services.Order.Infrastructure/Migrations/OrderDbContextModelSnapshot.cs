﻿// <auto-generated />
using System;
using EC.Services.Order.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EC.Services.Order.Infrastructure.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EC.Services.Order.Domain.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AddressDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(240)");

                    b.Property<DateTime>("CDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CountyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OrderNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("OrderNo")
                        .IsUnique();

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("OrderNo"), false);

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EC.Services.Order.Domain.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("EC.Services.Order.Domain.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("EC.Services.Order.Domain.OrderAggregate.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EC.Services.Order.Domain.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
