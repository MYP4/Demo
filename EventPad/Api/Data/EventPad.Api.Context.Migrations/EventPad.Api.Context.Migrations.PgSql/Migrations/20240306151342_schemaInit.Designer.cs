﻿// <auto-generated />
using System;
using EventPad.Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventPad.Api.Context.Migrations.PgSql.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20240306151342_schemaInit")]
    partial class schemaInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventPad.Api.Context.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Account")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("AdminId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("events", "main");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.EventPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("event_photos", "main");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.SpecificEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<int?>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<bool>("Private")
                        .HasColumnType("boolean");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("TicketCount")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time without time zone");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("specific_events", "main");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Feedback")
                        .HasColumnType("text");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<int>("SpecificEventId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SpecificEventId");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("event_tickets", "main");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Account")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<double>("Ratings")
                        .HasColumnType("double precision");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.Event", b =>
                {
                    b.HasOne("EventPad.Api.Context.Entities.User", "Admin")
                        .WithMany("Events")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.EventPhoto", b =>
                {
                    b.HasOne("EventPad.Api.Context.Entities.Event", "Event")
                        .WithMany("Photos")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.SpecificEvent", b =>
                {
                    b.HasOne("EventPad.Api.Context.Entities.Event", "Event")
                        .WithMany("SpecificEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.Ticket", b =>
                {
                    b.HasOne("EventPad.Api.Context.Entities.SpecificEvent", "SpecificEvent")
                        .WithMany("Tickets")
                        .HasForeignKey("SpecificEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventPad.Api.Context.Entities.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SpecificEvent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.Event", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("SpecificEvents");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.SpecificEvent", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("EventPad.Api.Context.Entities.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
