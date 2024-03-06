﻿// <auto-generated />
using System;
using EventPad.Pay.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventPad.Pay.Context.Migrations.PgSql.Migrations
{
    [DbContext(typeof(PayDbContext))]
    partial class PayDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventPad.Pay.Context.Entities.EventAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("event_accounts", "pay");
                });

            modelBuilder.Entity("EventPad.Pay.Context.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<int>("EventAccountId")
                        .HasColumnType("integer");

                    b.Property<Guid>("Ticket")
                        .HasMaxLength(16)
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DateTime")
                        .IsUnique();

                    b.HasIndex("EventAccountId");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.HasIndex("UserAccountId");

                    b.ToTable("transactions", "pay");
                });

            modelBuilder.Entity("EventPad.Pay.Context.Entities.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("user_accounts", "pay");
                });

            modelBuilder.Entity("EventPad.Pay.Context.Entities.Transaction", b =>
                {
                    b.HasOne("EventPad.Pay.Context.Entities.EventAccount", "EventAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("EventAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventPad.Pay.Context.Entities.UserAccount", "UserAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventAccount");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("EventPad.Pay.Context.Entities.EventAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("EventPad.Pay.Context.Entities.UserAccount", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
