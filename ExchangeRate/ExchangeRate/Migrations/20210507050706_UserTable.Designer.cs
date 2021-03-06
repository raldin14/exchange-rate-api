// <auto-generated />
using System;
using ExchangeRate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExchangeRate.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210507050706_UserTable")]
    partial class UserTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExchangeRate.Data.Models.CurrencyExchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ISO_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Purchase")
                        .HasColumnType("real");

                    b.Property<float>("Sale")
                        .HasColumnType("real");

                    b.Property<DateTime>("Today_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CurrenciesExchange");
                });
#pragma warning restore 612, 618
        }
    }
}
