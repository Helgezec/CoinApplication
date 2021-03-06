// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoinApplication.Data.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoinApplication.Models.ExchangeOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ExchangeOperations");
                });

            modelBuilder.Entity("CoinApplication.Models.ExchangeOperationItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("ExchangeOperationId")
                        .HasColumnType("int");

                    b.Property<int>("MoneyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExchangeOperationId");

                    b.HasIndex("MoneyId");

                    b.ToTable("ExchangeOperationItem");
                });

            modelBuilder.Entity("CoinApplication.Models.Money", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Acceptable")
                        .HasColumnType("bit");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("DenominationValue")
                        .HasColumnType("int");

                    b.Property<int>("MaxAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Monies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Acceptable = false,
                            Amount = 100,
                            DenominationValue = 50,
                            MaxAmount = 20
                        },
                        new
                        {
                            Id = 2,
                            Acceptable = false,
                            Amount = 100,
                            DenominationValue = 100,
                            MaxAmount = 10
                        },
                        new
                        {
                            Id = 3,
                            Acceptable = true,
                            Amount = 0,
                            DenominationValue = 5,
                            MaxAmount = 100
                        },
                        new
                        {
                            Id = 4,
                            Acceptable = true,
                            Amount = 0,
                            DenominationValue = 10,
                            MaxAmount = 50
                        });
                });

            modelBuilder.Entity("CoinApplication.Models.ExchangeOperationItem", b =>
                {
                    b.HasOne("CoinApplication.Models.ExchangeOperation", "ExchangeOperation")
                        .WithMany()
                        .HasForeignKey("ExchangeOperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoinApplication.Models.Money", "Money")
                        .WithMany()
                        .HasForeignKey("MoneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExchangeOperation");

                    b.Navigation("Money");
                });
#pragma warning restore 612, 618
        }
    }
}
