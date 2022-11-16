﻿// <auto-generated />
using EC.Services.LangResourceAPI.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EC.Services.LangResourceAPI.Migrations
{
    [DbContext(typeof(LangResourceDbContext))]
    partial class LangResourceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EC.Services.LangResourceAPI.Entities.Lang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("Code"), false);

                    b.ToTable("Langs");
                });

            modelBuilder.Entity("EC.Services.LangResourceAPI.Entities.LangResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("LangId")
                        .HasColumnType("int");

                    b.Property<string>("MessageCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("LangId");

                    b.HasIndex("MessageCode");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("MessageCode"), false);

                    b.ToTable("LangResources");
                });

            modelBuilder.Entity("EC.Services.LangResourceAPI.Entities.LangResource", b =>
                {
                    b.HasOne("EC.Services.LangResourceAPI.Entities.Lang", "Lang")
                        .WithMany("LangResources")
                        .HasForeignKey("LangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lang");
                });

            modelBuilder.Entity("EC.Services.LangResourceAPI.Entities.Lang", b =>
                {
                    b.Navigation("LangResources");
                });
#pragma warning restore 612, 618
        }
    }
}
