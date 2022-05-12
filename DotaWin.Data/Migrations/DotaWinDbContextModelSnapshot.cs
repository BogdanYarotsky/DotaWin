﻿// <auto-generated />
using System;
using DotaWin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DotaWin.Data.Migrations
{
    [DbContext(typeof(DotaWinDbContext))]
    partial class DotaWinDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DbItemDbUpdate", b =>
                {
                    b.Property<int>("ItemsId")
                        .HasColumnType("integer");

                    b.Property<int>("UpdatesId")
                        .HasColumnType("integer");

                    b.HasKey("ItemsId", "UpdatesId");

                    b.HasIndex("UpdatesId");

                    b.ToTable("DbItemDbUpdate");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbHero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("UpdateId")
                        .HasColumnType("integer");

                    b.Property<double>("Winrate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("UpdateId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbHeroItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("HeroId")
                        .HasColumnType("integer");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Matches")
                        .HasColumnType("integer");

                    b.Property<int>("UpdateId")
                        .HasColumnType("integer");

                    b.Property<double>("Winrate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("HeroId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UpdateId");

                    b.ToTable("HeroItems");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("TechnicalName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Patch")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DailyUpdates");
                });

            modelBuilder.Entity("DbItemDbUpdate", b =>
                {
                    b.HasOne("DotaWin.Data.Models.DbItem", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotaWin.Data.Models.DbUpdate", null)
                        .WithMany()
                        .HasForeignKey("UpdatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbHero", b =>
                {
                    b.HasOne("DotaWin.Data.Models.DbUpdate", "Update")
                        .WithMany("Heroes")
                        .HasForeignKey("UpdateId");

                    b.Navigation("Update");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbHeroItem", b =>
                {
                    b.HasOne("DotaWin.Data.Models.DbHero", "Hero")
                        .WithMany("HeroItems")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotaWin.Data.Models.DbItem", "Item")
                        .WithMany("HeroItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotaWin.Data.Models.DbUpdate", "Update")
                        .WithMany("HeroItems")
                        .HasForeignKey("UpdateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");

                    b.Navigation("Item");

                    b.Navigation("Update");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbHero", b =>
                {
                    b.Navigation("HeroItems");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbItem", b =>
                {
                    b.Navigation("HeroItems");
                });

            modelBuilder.Entity("DotaWin.Data.Models.DbUpdate", b =>
                {
                    b.Navigation("HeroItems");

                    b.Navigation("Heroes");
                });
#pragma warning restore 612, 618
        }
    }
}
